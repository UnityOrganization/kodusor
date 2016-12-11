using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kodusorClient.kodusorServis;

namespace kodusorClient.Controllers
{
    public class HomeController : Controller
    {
        KodusorServisClient servis;

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult kayit(Kullanicilar k)
        {
            servis = new KodusorServisClient();
            servis.Open();
            string sonuc = servis.KayitOl(k);
            servis.Close();
            return Json(sonuc);
        }
    }
}