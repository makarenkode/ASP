using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using PagedList;

namespace AutomobileEnterprise.Models
{
    public class ManualRequest
    {
        [DisplayName("Query")]
        public string Query { get; set; }

        public List<string> ColumnNames { get; set; }
        public IPagedList<IDictionary<string, object>> SelectResult { get; set; }
        public string ErrorMessage { get; set; }
        public int RecordsAffected { get; set; }
    }
}