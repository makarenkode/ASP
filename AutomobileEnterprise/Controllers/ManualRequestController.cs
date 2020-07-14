using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomobileEnterprise.Extensions;
using AutomobileEnterprise.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PagedList;

namespace AutomobileEnterprise.Controllers
{
    [OutputCache(CacheProfile = "defaultCacheProfile")]
    public class ManualRequestController : Controller
    {

        private ManualRequestHelper _manualRequestHelper = new ManualRequestHelper(new ApplicationDbContext());

        private int PageSize = 4;

        public ActionResult Index([Bind(Include = "")] ManualRequest manualRequest, string query, int? pageNum)
        {
            int numOfPage = (pageNum ?? 1);

            if (manualRequest == null)
            {
                manualRequest = new ManualRequest();
                manualRequest.SelectResult = new List<IDictionary<string, object>>().ToPagedList(numOfPage, PageSize);
            }

            manualRequest.Query = query;

            if (manualRequest.SelectResult == null)
            {
                manualRequest.SelectResult = new List<IDictionary<string, object>>().ToPagedList(numOfPage, PageSize);
            }

            if (!string.IsNullOrEmpty(manualRequest.Query))
            {
                try
                {
                    var result = _manualRequestHelper.DynamicListFromSql(manualRequest.Query);

                    manualRequest.SelectResult = result.ToPagedList(numOfPage, PageSize);
                    manualRequest.RecordsAffected = _manualRequestHelper.RecordsAffected;
                    manualRequest.ColumnNames = _manualRequestHelper.ColumnNames;
                }
                catch (Exception e)
                {
                    manualRequest.ErrorMessage = e.Message;
                    return View(manualRequest);
                }


            }

            return View(manualRequest);
        }

    }
}