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
        kodusorServis.Service1Client s;

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult KayitOl()
        {
            return View();
        }
        public JsonResult kayit(Kullanicilar kul)
        {
            //;
            //bool durum = s.KayitOl(kullanici);
            //s.Close();
            //if (durum)
            //    return Json("+");
            //else
            //    return Json("-");
            s = new kodusorServis.Service1Client();
            var sonuc = s.KayitOl(kul);
            if (sonuc == true)
                return Json("+");
            else return Json("-");
            s.Close();

        }
        public ActionResult Login()
        {
            return View();
        }
        


    }
}