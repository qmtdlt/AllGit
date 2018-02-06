using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class PreViewPdfController : Controller
    {
        //
        // GET: /PreViewPdf/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetPdf()
        {
            return new FilePathResult("F:/工作/华视网聚系统/HSWJWorkSpace/svn2667/NFine.Web/wr/contract/2018/2/2/201802021500597532.pdf", "application/pdf");
        }

    }
}
