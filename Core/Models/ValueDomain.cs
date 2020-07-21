namespace StatisticsPoland.VtlProcessing.Core.Models
{
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
        /// Gets or sets the type of the data.
        /// </summary>
        public BasicDataType DataType { get; }

        /// <summary>
        /// Gets or sets the signature of value domain.
        /// </summary>
        public string Signature { get; }
    }
}