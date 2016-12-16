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
            servis = new KodusorServisClient();
            List<SoruListesi> sorular = servis.SorulariListele(0).ToList();
            servis.Close();
            return View(sorular);
        }

        public JsonResult kayit(Kullanicilar k)
        {
            servis = new KodusorServisClient();
            string sonuc = servis.KayitOl(k);
            servis.Close();
            return Json(sonuc);
        }

        public ActionResult Soru(int? id)
        {
            return View(id);
        }




        public ActionResult Giris()
        {
            if (Request.Cookies["KullaniciKimligi"] != null)
            {
                return RedirectToAction("Index", "Profil");
            }
            return View();
        }

        public JsonResult GirisKontrol(string mail, string parola)
        {
            servis = new KodusorServisClient();
            var kullanici = servis.GirisYap(mail, parola);

            if (kullanici != 0)
            {
                Session["kullaniciID"] = kullanici;
                return Json("+");
            }
            else
            {
                return Json("-");
            }
        }

        
    }
}