namespace StatisticsPoland.VtlProcessing.Service.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The translation response.
    /// </summary>
    public class TranslationResponse
    {
        private readonly List<Exception> _exceptions;
        private readonly string _result;

        /// <summary>
        /// Initialzes a new instance of the <see cref="TranslationResponse"/> class.
        /// </summary>
        /// <param name="exceptions">The translation errors collection.</param>
        public TranslationResponse(IEnumerable<Exception> exceptions)
        {
            this._exceptions = new List<Exception>();
            this._exceptions.AddRange(exceptions);
        }

        /// <summary>
        /// Initialzes a new instance of the <see cref="TranslationResponse"/> class.
        /// </summary>
        /// <param name="exception">The translation error.</param>
        public TranslationResponse(ArgumentException exception)
        {
            this._exceptions = new List<Exception>();
            this._exceptions.Add(exception);
        }

        /// <summary>
        /// Initialzes a new instance of the <see cref="TranslationResponse"/> class.
        /// </summary>
        /// <param name="result">The result of the translation.</param>
        public TranslationResponse(string result)
        {
            this._exceptions = new List<Exception>();
            this._result = result;
        }

        /// <summary>
        /// Gets the value indicating whether there are translation errors.
        /// </summary>
        public bool AreErrors => this._exceptions.Count > 0;

        /// <summary>
        /// Gets the translation errors.
        /// </summary>
        public List<Exception> Exceptions => this._exceptions;

        /// <summary>
        /// Gets the result of the translation.
        /// </summary>
        public string Result => this._result;
    }
}
