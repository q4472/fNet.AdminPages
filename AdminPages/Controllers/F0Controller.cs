using FNet.AdminPages.Models;
using Nskd;
using System;
using System.Web.Mvc;

namespace FNet.AdminPages.Controllers
{
    public class F0Controller : Controller
    {
        public Object Index(Guid SessionId)
        {
            Object v = null;
            F0Model m = new F0Model();
            m.Get(SessionId);
            v = PartialView("~/Views/F0/Index.cshtml", m);
            return v;
        }
        public Object ApplyFilter()
        {
            Object v = null;
            RequestPackage rqp = RequestPackage.ParseRequest(Request.InputStream, Request.ContentEncoding);
            F0Model m = new F0Model();
            m.ApplyFilter(rqp);
            v = PartialView("~/Views/F0/Table.cshtml", m);
            return v;
        }
        public Object Save()
        {
            Object v = "FNet.AdminPages.Controllers.F0Controller.Save()";
            RequestPackage rqp = RequestPackage.ParseRequest(Request.InputStream, Request.ContentEncoding);
            F0Model m = new F0Model();
            m.Save(rqp);
            return v;
        }
    }
}