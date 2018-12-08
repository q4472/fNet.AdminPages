using FNet.AdminPages.Models;
using Nskd;
using System;
using System.IO;
using System.Web.Mvc;

namespace FNet.AdminPages.Controllers
{
    public class F0Controller : Controller
    {
        public Object Index()
        {
            Object v = null;
            F0Model m = new F0Model();
            StreamReader reader = new StreamReader(Request.InputStream, Request.ContentEncoding);
            String body = reader.ReadToEnd();
            if (!String.IsNullOrWhiteSpace(body))
            {
                if (body[0] == '{')
                {
                    RequestPackage rqp = RequestPackage.ParseRequest(Request.InputStream, Request.ContentEncoding);
                    m.Get(rqp.SessionId, rqp);
                }
                else if (body[0] == 's' && body.Length == 46)
                {
                    if (Guid.TryParse(body.Substring(10, 36), out Guid sessionId))
                    {
                        m.Get(sessionId, null);
                    }
                }
            }
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