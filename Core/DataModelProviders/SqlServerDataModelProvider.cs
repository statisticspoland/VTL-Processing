namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders
{
    using Microsoft.Data.SqlClient;
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The TSQL server VTL 2.0 data model.
    /// </summary>
    public class SqlServerDataModelProvider : DataModelProviderBase
    {
        private readonly DataStructureResolver _dsResolver;
        private readonly string _connStr;
        private readonly IEnvironmentMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlServerDataModelProvider"/> class.
        /// </summary>
        /// <param name="rootModel">The root data model.</param>
        /// <param name="dsResolver">The data structure resolver.</param>
        /// <param name="connectionString">The TSQL Server connection string.</param>
        /// <param name="mapper">The environment names mapper.</param>
        public SqlServerDataModelProvider(IDataModelProvider rootModel, DataStructureResolver dsResolver, string connectionString, IEnvironmentMapper mapper)
            : base(rootModel)
        {
            this._dsResolver = dsResolver;
            this._connStr = connectionString;
            this._mapper = mapper;
        }

        public override IDataStructure GetDatasetStructure(string datasetName)
        {
            string @namespace;
            this.SplitDatasetName(datasetName, out @namespace, out datasetName);

            if (!this._mapper.Mapping.ContainsKey(@namespace)) 
                return null;

            SqlAddress sqlAddress = new SqlAddress(this._mapper.Mapping[@namespace].Split('.'));
            using (SqlConnection conn = new SqlConnection(this._connStr))
            {
                if (sqlAddress.Server != null && sqlAddress.Server != conn.DataSource) return null;
                if (sqlAddress.Database != null && conn.Database != string.Empty && sqlAddress.Database != conn.Database) return null;

                Server server = new Server(new ServerConnection(conn));
                string dbName = sqlAddress.Database.In(string.Empty, null) ? conn.Database : sqlAddress.Database;
                string schema = sqlAddress.Schema.In(string.Empty, null) ? "dbo" : sqlAddress.Schema;
                string tableName = $"{sqlAddress.TablePrefix}{datasetName}";

                if (server.Databases.Contains(dbName))
                {
                    IDataStructure structure = this._dsResolver();
                    Database database = server.Databases[dbName];
                    Table table = null;

                    if (database.Tables.Contains(tableName, schema))
                        table = database.Tables[tableName, schema];

                    if (table == null) 
                        return null;

                    List<StructureComponent> identifiers = new List<StructureComponent>();
                    List<StructureComponent> measures = new List<StructureComponent>();
                    List<StructureComponent> viralAttributes = new List<StructureComponent>();
                    List<StructureComponent> nonViralAttributes = new List<StructureComponent>();

                    foreach (Column column in table.Columns)
                    {
                        StructureComponent comp = new StructureComponent(this.MapTypes(column.DataType.SqlDataType), column.Name);

                        if (column.ExtendedProperties.Contains("vtl_component_role"))
                        {
                            switch (column.ExtendedProperties["vtl_component_role"].Value.ToString())
                            {
                                case "identifier":
                                    comp.ComponentType = ComponentType.Identifier;
                                    identifiers.Add(comp);
                                    break;
                                case "attribute":
                                case "attribute.nonviral":
                                    comp.ComponentType = ComponentType.NonViralAttribute;
                                    nonViralAttributes.Add(comp);
                                    break;
                                case "attribute.viral":
                                    comp.ComponentType = ComponentType.ViralAttribute;
                                    viralAttributes.Add(comp);
                                    break;
                                case "measure":
                                default:
                                    comp.ComponentType = ComponentType.Measure;
                                    measures.Add(comp);
                                    break;
                            }
                        }
                        else
                        {
                            if (column.ExtendedProperties.Contains("vtl_time_type"))
                            {
                                switch (column.ExtendedProperties["vtl_time_type"].Value.ToString())
                                {
                                    case "time":
                                        comp.ValueDomain = new ValueDomain(BasicDataType.Time);
                                        break;
                                    case "date":
                                        comp.ValueDomain = new ValueDomain(BasicDataType.Date);
                                        break;
                                    case "time_period":
                                        comp.ValueDomain = new ValueDomain(BasicDataType.TimePeriod);
                                        break;
                                    case "duration":
                                        comp.ValueDomain = new ValueDomain(BasicDataType.Duration);
                                        break;
                                    default: break;
                                }
                            }

                            if (column.InPrimaryKey)
                            {
                                comp.ComponentType = ComponentType.Identifier;
                                identifiers.Add(comp);
                            }
                            else
                            {
                                comp.ComponentType = ComponentType.Measure;
                                measures.Add(comp);
                            }
                        }
                    }

                    structure.Identifiers = identifiers;
                    structure.Measures = measures;
                    structure.ViralAttributes = viralAttributes;
                    structure.NonViralAttributes = nonViralAttributes;

                    return structure;
                }

                return null;
            }
        }

        /// <summary>
        /// Maps a TSQL data type to a VTL 2.0 data type.
        /// </summary>
        /// <param name="sqlDataType">The type of the SQL data.</param>
        /// <returns>The basic data type of VTL 2.0.</returns>
        private BasicDataType MapTypes(SqlDataType sqlDataType)
        {
            switch (sqlDataType)
            {
                case SqlDataType.BigInt:
                case SqlDataType.Int:
                case SqlDataType.TinyInt:
                    return BasicDataType.Integer;
                case SqlDataType.Numeric:
                case SqlDataType.Decimal:
                    return BasicDataType.Number;
                case SqlDataType.Bit:
                    return BasicDataType.Boolean;
                case SqlDataType.Char:
                case SqlDataType.NChar:
                case SqlDataType.NText:
                case SqlDataType.NVarChar:
                case SqlDataType.NVarCharMax:
                case SqlDataType.Text:
                case SqlDataType.VarChar:
                case SqlDataType.VarCharMax:
                    return BasicDataType.String;
                case SqlDataType.Date:
                case SqlDataType.DateTime:
                case SqlDataType.DateTime2:
                case SqlDataType.SmallDateTime:
                case SqlDataType.Time:
                case SqlDataType.Timestamp:
                    return BasicDataType.Date;
                default: 
                    throw new NotSupportedException($"Cannot map SQL data type {sqlDataType} to VTL data model");
            }
        }
    }
}
