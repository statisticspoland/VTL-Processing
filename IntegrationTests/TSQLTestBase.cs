namespace VtlProcessing.IntegrationTests.TSQL
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging;
    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.DataModel;
    using StatisticsPoland.VtlProcessing.DataModel.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Target.TSQL;
    using Target.TSQL.Infrastructure;
    using VtlProcessing.IntegrationTests.TSQL.Properties;

    public class TSQLTestBase
    {
        protected ITreeGenerator frontEnd;
        protected ISchemaModifiersApplier middleEnd;
        protected ServiceProvider provider;
        protected ErrorCollectorProvider errCollectorProvider;

        public TSQLTestBase(string schema)
        {
            string connectionString = Resources.TestDbConnectionString;
            Dictionary<string, string> sqlMapping = new Dictionary<string, string>()
            {
                { "ns", $"[{schema}]." },
            };

            IServiceCollection services = new ServiceCollection();
            services.AddVtlProcessing((configure) =>
            {
                configure.DefaultNamespace = "ns";
                configure.AddSqlServerModel(connectionString, sqlMapping);
            });

            services.AddTsqlTarget((configure) =>
            {
                configure.AddEnvMapper(new DictionaryEnvMapper(sqlMapping));
            });

            this.errCollectorProvider = new ErrorCollectorProvider();
            services.AddLogging((configure) =>
            {
                configure.AddProvider(this.errCollectorProvider);
            });

            this.provider = services.BuildServiceProvider();

            this.frontEnd = provider.GetFrontEnd();
            this.middleEnd = provider.GetMiddleEnd();
        }

        protected string TranslateVtl(string source, out List<Exception> errors)
        {
            ITransformationSchema schema = this.frontEnd.BuildTransformationSchema(source);
            this.middleEnd.Process(schema);

            ITargetRenderer tsqlRenderer = this.provider.GetService<TsqlTargetRenderer>();

            string sql = tsqlRenderer.Render(schema);

            errors = new List<Exception>();
            foreach (ErrorCollector errCollector in this.errCollectorProvider.ErrorCollectors)
            {
                errors.AddRange(errCollector.Errors);
            }

            return sql;
        }

        protected DataTable RunSql(string sql, string objectToSelect)
        {
            string objSelector = objectToSelect.Contains('@') ? string.Empty : " * FROM";
            DataTable result = new DataTable();
            SqlCommand cmd;
            SqlDataReader reader;

            using (SqlConnection conn = new SqlConnection(Resources.TestDbConnectionString))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = $"{Environment.NewLine}{sql}{Environment.NewLine}SELECT{objSelector} {objectToSelect}";
                reader = cmd.ExecuteReader();
                
                foreach (string colName in reader.GetColumnSchema().Select(col => col.ColumnName))
                {
                    result.Columns.Add(colName);
                }

                if (result.Columns.Count == 1 && result.Columns[0].ColumnName == "Column1") result.Columns[0].ColumnName = "value";

                while(reader.Read())
                {
                    List<object> objs = new List<object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        objs.Add(reader[i]);
                    }

                    result.LoadDataRow(objs.ToArray(), false);
                }

                Debug.WriteLine($"\n{cmd.CommandText}\n");
            }

            return result;
        }

        protected void SqlFillData(string table, IEnumerable<object[]> data)
        {
            SqlCommand cmd;
            StringBuilder sb = new StringBuilder();
            string value = string.Empty;

            sb.AppendLine($"DELETE FROM {table}");

            foreach(object[] row in data)
            {
                sb.Append($"INSERT INTO {table} VALUES (");

                foreach (object field in row)
                {
                    if (field == null) value = "NULL, ";
                    else if (field.GetType() == typeof(string)) value = $"'{field.ToString()}', ";
                    else if (decimal.TryParse(field.ToString(), out _)) value = $"{field.ToString().Replace(",", ".")}, ";
                    else value = $"'{field.ToString()}', ";

                    sb.Append(value);
                }

                sb.AppendLine(")");
            }

            string cmdText = sb.ToString().Replace(", )", ")");
            using (SqlConnection conn = new SqlConnection(Resources.TestDbConnectionString))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = cmdText;
                cmd.ExecuteReader();
            }
        }
    }
}
