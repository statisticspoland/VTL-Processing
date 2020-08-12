namespace StatisticsPoland.VtlProcessing.Core.Models
{
    using Newtonsoft.Json;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;

    /// <summary>
    /// The VTL 2.0 value domain representation.
    /// </summary>
    public class ValueDomain
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueDomain"/> class.
        /// </summary>
        /// <param name="dataType">The type of the data.</param>
        /// <param name="signature">The domain signature.</param>
        public ValueDomain(BasicDataType dataType, string signature = null)
        {
            this.DataType = dataType;
            this.Signature = signature ?? dataType.GetName().ToLower();
            if (this.Signature == "none") this.Signature = "null";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueDomain"/> class <b>for value domains of basic data types.</b>.
        /// </summary>
        /// <param name="signature">The domain signature.</param>
        [JsonConstructor]
        public ValueDomain(string signature)
        {
            this.Signature = signature;
            switch (signature)
            {
                case "integer_default": this.DataType = BasicDataType.Integer; break;
                case "number_default": this.DataType = BasicDataType.Number; break;
                case "string_default": this.DataType = BasicDataType.String; break;
                case "boolean_default": this.DataType = BasicDataType.Boolean; break;
                case "time_default": this.DataType = BasicDataType.Time; break;
                case "date_default": this.DataType = BasicDataType.Date; break;
                case "timeperdiod_default": this.DataType = BasicDataType.TimePeriod; break;
                case "duration_default": this.DataType = BasicDataType.Duration; break;
                case "null_default": this.DataType = BasicDataType.None; break;
                default: break;
            }
        }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        public BasicDataType DataType { get; }

        /// <summary>
        /// Gets or sets the signature of value domain.
        /// </summary>
        public string Signature { get; }
    }
}