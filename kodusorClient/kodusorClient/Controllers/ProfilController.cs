using kodusorClient.kodusorServis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kodusorClient.ViewModel;

namespace kodusorClient.Controllers
{
    public class ProfilController : Controller
    {
        KodusorServisClient servis;
        KullaniciModel kullaniciModeli;
        // GET: Profil
        public ActionResult Index()
        {
            if (Session["kullaniciID"] != null)
            {
                kullaniciModeli = new KullaniciModel();
                servis = new KodusorServisClient();
                int kulID = Convert.ToInt32(Session["kullaniciID"]);
                kullaniciModeli.Kullanici = servis.KullaniciBilgileriniGetir(kulID);
                kullaniciModeli.SoruListesi = servis.SorulariListele(kulID).ToList();
                kullaniciModeli.CevapListesi = servis.KullaniciCevapları(kulID).ToList();
                kullaniciModeli.EtiketListesi = servis.KullanicininEtiketleri(kulID).ToList();
                kullaniciModeli.FavoriSorular = servis.FavoriSorular(kulID).ToList();
                kullaniciModeli.FavoriCevaplar = servis.FavoriCevaplar(kulID).ToList();
                return View(kullaniciModeli);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SoruSor()
        {
            if (Session["kullaniciID"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public JsonResult SoruyuKaydet(Sorular soru, List<Etiketler> etiketler)
        {
            servis = new KodusorServisClient();
            int kulID = Convert.ToInt32(Session["kullaniciID"]);
            return Json(servis.SoruEkle(kulID, soru, etiketler.ToArray()));
        }
        
        public JsonResult ParolaGuncelle(string eskiParola, string yeniParola)
        {
            servis = new KodusorServisClient();
            int kulID = Convert.ToInt32(Session["kullaniciID"]);
            var kul = servis.KullaniciBilgileriniGetir(kulID);
            if(kul.Parola == eskiParola)
            {
                if (servis.ParolaDegistir(kulID, yeniParola))
                    return Json("+");
                else
                    return Json("-");
            }
            else
                return Json("Parolanızı yanlış girdiniz!");
        }

        public JsonResult Cikis()
        {
            Session["kullaniciID"] = null;
            return Json("+");
        }
    }
}