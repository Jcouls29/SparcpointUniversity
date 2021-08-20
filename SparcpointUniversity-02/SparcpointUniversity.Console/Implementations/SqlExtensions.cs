using System.Collections.Generic;
using System.Data;
using static Dapper.SqlMapper;

namespace SparcpointUniversity.Console
{
    internal static class SqlExtensions
    {
        public static DataTable ToDataTable(this Dictionary<string, string> value)
        {
            DataTable ut = new DataTable("[dbo].[StringKeyValueList]");

            ut.Columns.Add("Key", typeof(string));
            ut.Columns.Add("Value", typeof(string));

            foreach (var kv in value)
                ut.Rows.Add(kv.Key, kv.Value);

            return ut;
        }

        public static ICustomQueryParameter ToSqlParameter(this Dictionary<string, string> value)
            => value.ToDataTable().AsTableValuedParameter("[dbo].[StringKeyValueList]");
    }
}
