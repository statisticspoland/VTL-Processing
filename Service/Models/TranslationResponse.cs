namespace StatisticsPoland.VtlProcessing.Service.Models
{
    using System;
    using System.Collections.Generic;
    public class TranslationResponse
    {
        private readonly List<Exception> _exceptions;
        private readonly string _result;

        public TranslationResponse(IEnumerable<Exception> exceptions)
        {
            _exceptions = new List<Exception>();
            _exceptions.AddRange(exceptions);
        }

        public TranslationResponse(ArgumentException exception)
        {
            _exceptions = new List<Exception>();
            _exceptions.Add(exception);
        }

        public TranslationResponse(string result)
        {
            _exceptions = new List<Exception>();
            _result = result;
        }

        public bool AreErrors { get { return _exceptions.Count > 0; } }
        public List<Exception> Exceptions { get { return _exceptions; }  }
        public string Result { get { return _result; } }
    }
}
