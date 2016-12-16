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

        public JsonResult SoruyuKaydet(Sorular soru, ICollection<Etiketler> etiketler)
        {
            servis = new KodusorServisClient();
            int kulID = Convert.ToInt32(Session["kullaniciID"]);
            //if (servis.SoruEkle(kulID, soru, etiketler.ToArray()))
            //   return Json("+");
            //else
            return Json("-");
        }

        //public ActionResult SoruyuKaydet(Sorular soru, List<Etiketler> etiketler)
        //{
        //    servis = new KodusorServisClient();
        //    int kulID = Convert.ToInt32(Session["kullaniciID"]);
        //    servis.SoruEkle(kulID, soru, etiketler.ToArray());
        //    return RedirectToAction("Index", "Profil");
        //}

        public ActionResult KullaniciGuncelle()
        {
            if (Session["kullaniciID"] != null)
            {
                servis = new KodusorServisClient();
                int kulID = Convert.ToInt32(Session["kullaniciID"]);
                var kullanici = servis.KullaniciBilgileriniGetir(kulID);
                return View(kullanici);
            }
            return RedirectToAction("Index", "Home");
        }

        public JsonResult BilgileriGuncelle(Kullanicilar kullanici)
        {
            servis = new KodusorServisClient();
            if (servis.KullaniciBilgileriGuncelle(kullanici))
                return Json("+");
            else
                return Json("-");
        }



        public JsonResult Cikis()
        {
            Session["kullaniciID"] = null;
            return Json("+");
        }
    }
}