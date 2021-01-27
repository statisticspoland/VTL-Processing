using System;
using System.Collections.Generic;
using System.Linq;

namespace StatisticsPoland.VtlProcessing.DataModel.Infrastructure
{
    internal class SqlAddress
    {
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
                    default: throw new Exception("Too many items in address array.");
                }
            }
        }

        public string TablePrefix { get; }

        public string Schema { get; }

        public string Database { get; }

        public string Server { get; }
    }
}
