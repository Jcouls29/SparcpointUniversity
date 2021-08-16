using System.Collections.Generic;
using System.Data;
using static Dapper.SqlMapper;

namespace SparcpointUniversity.Console
{
    internal static class SqlExtensions
    {
        public static ICustomQueryParameter ToSqlParameter(this Dictionary<string, string> value)
        {
            DataTable ut = new DataTable("StringKeyValueList");

            ut.Columns.Add("Key", typeof(string));
            ut.Columns.Add("Value", typeof(string));

            foreach (var kv in value)
                ut.Rows.Add(kv.Key, kv.Value);

            return ut.AsTableValuedParameter("[dbo].[StringKeyValueList]");
        }
    }
}
