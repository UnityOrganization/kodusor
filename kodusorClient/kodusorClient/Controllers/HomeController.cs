using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kodusorClient.kodusorServis;
using kodusorClient.ViewModel;

namespace kodusorClient.Controllers
{
    public class HomeController : Controller
    {
        KodusorServisClient servis;
        KullaniciModel kullaniciModeli;
        // GET: Home
        public ActionResult Index()
        {
            servis = new KodusorServisClient();
            var sorular = servis.SorulariListele(0).ToList();
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



        public ActionResult Soru(int id)
        {
            kullaniciModeli = new KullaniciModel();
            servis = new KodusorServisClient();
            
            kullaniciModeli.Soru = servis.SoruGetir(id);
            if (Session["kullaniciID"] != null)
            {
                int kulID = Convert.ToInt32(Session["kullaniciID"]);
                kullaniciModeli.Kullanici = servis.KullaniciBilgileriniGetir(kulID);
            }
            return View(kullaniciModeli);
        }
        //[HttpPost]
        //public ActionResult FavoriEkle(int? id)
        //{
        //    servis = new KodusorServisClient();
        //    int kulID = Convert.ToInt32(Session["kullaniciID"]);
        //    FavoriCevaplar favoriCevap = new FavoriCevaplar()
        //    {
        //        KullaniciID = kulID,
        //        CevapID = (int)id
        //    };
        //    servis.CevabiFavoriyeEkle(favoriCevap);
        //    var sonuc = servis.CevabiFavoriyeEkle(favoriCevap);
        //    return RedirectToAction("Index", "Home");
        //    //return Json("");
        //}
    }
}