namespace StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The TSQL address representation.
    /// </summary>
    internal class SqlAddress
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlAddress"/> class.
        /// </summary>
        /// <param name="address">The TSQL address string representation.</param>
        public SqlAddress(string[] address)
        {
            List<string> addressList = address.ToList();
            addressList.Reverse();

            for (int i = 0; i < addressList.Count; i++)
            {
                switch (i)
                {
                    case 0: this.TablePrefix = addressList[i].Replace("[", string.Empty).Replace("]", string.Empty); break;
                    case 1: this.Schema = addressList[i].Replace("[", string.Empty).Replace("]", string.Empty); break;
                    case 2: this.Database = addressList[i].Replace("[", string.Empty).Replace("]", string.Empty); break;
                    case 3: this.Server = addressList[i].Replace("[", string.Empty).Replace("]", string.Empty); break;
                    default: throw new ArgumentOutOfRangeException("address", "Too many items in address array.");
                }
            }
        }

        /// <summary>
        /// Gets the table prefix of the TSQL address.
        /// </summary>
        public string TablePrefix { get; }

        /// <summary>
        /// Gets the schema name of the TSQL address.
        /// </summary>
        public string Schema { get; }

        /// <summary>
        /// Gets the database name of the TSQL address.
        /// </summary>
        public string Database { get; }

        /// <summary>
        /// Gets the server name of the TSQL address.
        /// </summary>
        public string Server { get; }
    }
}
