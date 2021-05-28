namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders.Models
{
    using Org.Sdmxsource.Sdmx.Api.Constants;
    using Org.Sdmxsource.Sdmx.Api.Factory;
    using Org.Sdmxsource.Sdmx.Api.Manager.Parse;
    using Org.Sdmxsource.Sdmx.Api.Model;
    using Org.Sdmxsource.Sdmx.Api.Model.Objects;
    using Org.Sdmxsource.Sdmx.Api.Model.Objects.Base;
    using Org.Sdmxsource.Sdmx.Api.Model.Objects.DataStructure;
    using Org.Sdmxsource.Sdmx.Api.Util;
    using Org.Sdmxsource.Sdmx.SdmxObjects.Model.Objects.DataStructure;
    using Org.Sdmxsource.Sdmx.Structureparser.Manager.Parsing;
    using Org.Sdmxsource.Util.Io;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    public class DataModelSdmx : DataModel
    {
        private readonly IStructureParsingManager _structureParsingManager;
        private readonly IReadableDataLocationFactory _dataLocationFactory;
        private readonly IReadableDataLocation _readableDataLocation;

        private DataModelSdmx(IDataModel rootModel, string namespaceName)
            : base(rootModel)
        {
            this.Namespace = namespaceName;

            this._structureParsingManager = new StructureParsingManager();
            this._dataLocationFactory = new ReadableDataLocationFactory();
        }

        public DataModelSdmx(IDataModel rootModel, string namespaceName, FileInfo structureFile)
            : this(rootModel, namespaceName)
        {
            this._readableDataLocation = this._dataLocationFactory.GetReadableDataLocation(structureFile);
        }

        public DataModelSdmx(IDataModel rootModel, string namespaceName, Uri structureUri)
            : this(rootModel, namespaceName)
        {
            this._readableDataLocation = this._dataLocationFactory.GetReadableDataLocation(structureUri);
        }

        public DataModelSdmx(IDataModel rootModel, string namespaceName, string strUrl)
            : this(rootModel, namespaceName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
            HttpClient.DefaultProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            request.Method = "GET";
            request.Accept = "application/xml";
            WebResponse response = request.GetResponse();

            this._readableDataLocation = this._dataLocationFactory.GetReadableDataLocation(response.GetResponseStream());
        }

        public DataModelSdmx(IDataModel rootModel, string namespaceName, byte[] bytes)
            : this(rootModel, namespaceName)
        {
            this._readableDataLocation = this._dataLocationFactory.GetReadableDataLocation(bytes);
        }

        public DataModelSdmx(IDataModel rootModel, string namespaceName, Stream stream)
            : this(rootModel, namespaceName)
        {
            this._readableDataLocation = this._dataLocationFactory.GetReadableDataLocation(stream);
        }

        private ISet<IMaintainableObject> GetMaintainableObjects()
        {
            IStructureWorkspace workspace;
            using (IReadableDataLocation rdl = this._readableDataLocation)
            {
                workspace = this._structureParsingManager.ParseStructures(rdl);
            }

            ISdmxObjects sdmxObjects = workspace.GetStructureObjects(false);

            ISet<IMaintainableObject> maintainable = sdmxObjects.GetAllMaintainables();

            return maintainable;
        }

        public override IDataStructure GetDatasetStructure(string datasetName)
        {

            string[] split = datasetName.Split(@"\");
            switch (split.Length)
            {
                case 1:
                    if (this.DefaultNamespace != this.Namespace) return null;
                    break;
                case 2:
                    if (split[0] != this.Namespace) return null;
                    datasetName = split[1];
                    break;
                default: throw new ArgumentOutOfRangeException("datasetName", $"Invalid DataSet identifier: {datasetName}");
            }

            ISet<IMaintainableObject> maintainable = this.GetMaintainableObjects();

            DataStructureObjectCore datastructure = (DataStructureObjectCore)maintainable.SingleOrDefault(m => m.StructureType.StructureType == "Data Structure Definition" && m.Id == datasetName);

            if (datastructure == null)
                return null;

            DataStructure dataStructure = new DataStructure();

            foreach (IDimension dimension in datastructure.DimensionList.Dimensions)
            {
                BasicDataType type = dimension.Representation?.TextFormat != null ? this.mappingSDMX(dimension.Representation.TextFormat.TextType.EnumType) : BasicDataType.String;
                dataStructure.Identifiers.Add(new StructureComponent(type, dimension.Id, ComponentType.Identifier));
            }

            IPrimaryMeasure primaryMeasure = datastructure.PrimaryMeasure;
            BasicDataType measureType = primaryMeasure.Representation?.TextFormat != null ? this.mappingSDMX(primaryMeasure.Representation.TextFormat.TextType.EnumType) : BasicDataType.Number;
            dataStructure.Measures.Add(new StructureComponent(measureType, primaryMeasure.Id, ComponentType.Measure));

            foreach (IAttributeObject attribute in datastructure.AttributeList.Attributes)
            {
                BasicDataType type = attribute.Representation?.TextFormat != null ? this.mappingSDMX(attribute.Representation.TextFormat.TextType.EnumType) : BasicDataType.String;
                dataStructure.ViralAttributes.Add(new StructureComponent(type, attribute.Id, ComponentType.ViralAttribute));
            }

            return dataStructure;
        }

        private BasicDataType mappingSDMX(TextEnumType type)
        {
            BasicDataType basicDataType = type switch
            {
                TextEnumType.Null => BasicDataType.None,
                TextEnumType.Alpha => BasicDataType.String,
                TextEnumType.Alphanumeric => BasicDataType.String,
                TextEnumType.AttachmentConstraintReference => BasicDataType.None,
                TextEnumType.BasicTimePeriod => BasicDataType.Date,
                TextEnumType.String => BasicDataType.String,
                TextEnumType.BigInteger => BasicDataType.Integer,
                TextEnumType.Integer => BasicDataType.Integer,
                TextEnumType.Long => BasicDataType.Integer,
                TextEnumType.Short => BasicDataType.Integer,
                TextEnumType.Decimal => BasicDataType.Number,
                TextEnumType.Float => BasicDataType.Number,
                TextEnumType.Double => BasicDataType.Number,
                TextEnumType.Boolean => BasicDataType.Boolean,
                TextEnumType.DateTime => BasicDataType.Date,
                TextEnumType.Date => BasicDataType.Date,
                TextEnumType.Time => BasicDataType.String,
                TextEnumType.Year => BasicDataType.String,
                TextEnumType.Month => BasicDataType.String,
                TextEnumType.Numeric => BasicDataType.String,
                TextEnumType.Day => BasicDataType.String,
                TextEnumType.MonthDay => BasicDataType.String,
                TextEnumType.YearMonth => BasicDataType.String,
                TextEnumType.Duration => BasicDataType.Duration,
                TextEnumType.Uri => BasicDataType.String,
                TextEnumType.Timespan => BasicDataType.String,
                TextEnumType.Count => BasicDataType.Integer,
                TextEnumType.DataSetReference => BasicDataType.None,
                TextEnumType.InclusiveValueRange => BasicDataType.Number,
                TextEnumType.ExclusiveValueRange => BasicDataType.Number,
                TextEnumType.Incremental => BasicDataType.Number,
                TextEnumType.ObservationalTimePeriod => BasicDataType.Time,
                TextEnumType.KeyValues => BasicDataType.None,
                TextEnumType.TimePeriod => BasicDataType.TimePeriod,
                TextEnumType.GregorianDay => BasicDataType.Date,
                TextEnumType.GregorianTimePeriod => BasicDataType.Date,
                TextEnumType.GregorianYear => BasicDataType.Date,
                TextEnumType.GregorianYearMonth => BasicDataType.Date,
                TextEnumType.ReportingDay => BasicDataType.TimePeriod,
                TextEnumType.ReportingMonth => BasicDataType.TimePeriod,
                TextEnumType.ReportingQuarter => BasicDataType.TimePeriod,
                TextEnumType.ReportingSemester => BasicDataType.TimePeriod,
                TextEnumType.ReportingTimePeriod => BasicDataType.TimePeriod,
                TextEnumType.ReportingTrimester => BasicDataType.TimePeriod,
                TextEnumType.ReportingWeek => BasicDataType.TimePeriod,
                TextEnumType.ReportingYear => BasicDataType.TimePeriod,
                TextEnumType.StandardTimePeriod => BasicDataType.Time,
                TextEnumType.TimesRange => BasicDataType.Time,
                TextEnumType.IdentifiableReference => BasicDataType.None,
                TextEnumType.Xhtml => BasicDataType.None,
                _ => throw new ArgumentOutOfRangeException("type")
            };

            return basicDataType;
        }
    }
}
