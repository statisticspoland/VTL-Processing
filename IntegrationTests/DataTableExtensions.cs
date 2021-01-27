using System;
using System.Data;
using System.Linq;

namespace VtlProcessing.IntegrationTests.TSQL
{
    public static class DataTableExtensions
    {
        public static bool EqualsTo(this DataTable instance, DataTable obj)
        {
            if (instance.Columns.Count != obj.Columns.Count || instance.Rows.Count != obj.Rows.Count) 
                return false;

            for (int i = 0; i < obj.Columns.Count; i++)
            {
                DataColumn col = instance.Columns.Cast<DataColumn>().FirstOrDefault(c =>
                    c.ColumnName == obj.Columns[i].ColumnName);
                
                if (col == null) 
                    return false;

                for (int j = 0; j < obj.Rows.Count; j++)
                {
                    string instanceField = instance.Rows[j].Field<string>(instance.Columns[instance.Columns.IndexOf(col)]);
                    string objField = obj.Rows[j].Field<string>(obj.Columns[i]);
                    TryParseToCorrectNumberStr(ref instanceField);
                    TryParseToCorrectNumberStr(ref objField);

                    if (instanceField != objField)
                        return false;
                }
            }

            return true;
        }

        private static void TryParseToCorrectNumberStr(ref string source)
        {
            if (decimal.TryParse(source, out _) && (source.Contains(".") || source.Contains(",")))
            {
                source = source.Replace(',', '.');
                string fraction = source.Split('.')[1];
                while (fraction.Length > 0 && fraction.Last() == '0')
                {
                    fraction = fraction.Remove(fraction.Length - 1);
                }

                if (fraction.Length != 0) fraction = $",{fraction}";
                source = $"{source.Split('.')[0]}{fraction}";
            }
        }
    }
}
