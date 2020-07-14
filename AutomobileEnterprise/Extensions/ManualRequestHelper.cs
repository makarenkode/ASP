using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
using AutomobileEnterprise.Models;

namespace AutomobileEnterprise.Extensions
{
    public class ManualRequestHelper
    {
        public int RecordsAffected { get; private set; }

        public List<string> ColumnNames { get; } = new List<string>();

        private readonly ApplicationDbContext _db;

        public ManualRequestHelper(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<IDictionary<string, object>> DynamicListFromSql(string query)
        {
            ColumnNames.Clear();
            using (var cmd = _db.Database.Connection.CreateCommand())
            {
                cmd.CommandText = query;
                if (cmd.Connection.State != ConnectionState.Open) { cmd.Connection.Open(); }

                using (var dataReader = cmd.ExecuteReader())
                {
                    RecordsAffected = dataReader.RecordsAffected;
                    while (dataReader.Read())
                    {
                        if (!ColumnNames.Any())
                        {
                            for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                            {
                                ColumnNames.Add(dataReader.GetName(fieldCount));
                            }
                        }
                        var row = new ExpandoObject() as IDictionary<string, object>;
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                        {
                            row.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
                        }
                        yield return row;
                    }
                }
            }
        }
    }
}