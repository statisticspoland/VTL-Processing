namespace StatisticsPoland.VtlProcessing.Core.Models.Types
{
    /// <summary>
    /// The VTL 2.0 basic data types.
    /// </summary>
    public enum BasicDataType
    {
        /// <summary>
        /// The type not determined (the null value).
        /// </summary>
        None = 0,

        /// <summary>
        /// The number without a fractional component.
        /// </summary>
        Integer = 1,

        /// <summary>
        /// The fixed or floating point number.
        /// </summary>
        Number = 2,

        /// <summary>
        /// The character sequence.
        /// </summary>
        String = 3,

        /// <summary>
        /// The boolean value, possible true or false.
        /// </summary>
        Boolean = 4,

        /// <summary>
        /// The date range.
        /// </summary>
        Time = 5,

        /// <summary>
        /// The single date.
        /// </summary>
        Date = 6,

        /// <summary>
        /// Time intervals having a regular duration.
        /// </summary>
        TimePeriod = 7,

        /// <summary>
        /// The length of a time interval.
        /// </summary>
        Duration = 8
    }
}
