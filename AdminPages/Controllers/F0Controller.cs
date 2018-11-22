using FNet.AdminPages.Models;
using System;
using System.Web.Mvc;

namespace FNet.AdminPages.Controllers
{
    public class F0Controller : Controller
    {
        public Object Index(Guid SessionId)
        {
            Object v = null;
            F0Model m = new F0Model(SessionId);
            v = PartialView("~/Views/F0/Index.cshtml", m);
            return v;
        }
    }
}