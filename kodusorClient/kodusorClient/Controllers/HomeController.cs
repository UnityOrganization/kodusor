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

        [HttpPost]
        public ActionResult SoruAra(string aranacakSoru)
        {
            servis = new KodusorServisClient();
            var sorular = servis.SoruAra(aranacakSoru);
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
                kullaniciModeli.FavoriSorular = servis.FavoriSorular(kulID).ToList();
                kullaniciModeli.FavoriCevaplar = servis.FavoriCevaplar(kulID).ToList();
            }
            return View(kullaniciModeli);
        }

        public JsonResult SoruyuFavEkle(int soruID)
        {
            servis = new KodusorServisClient();
            int kulID = Convert.ToInt32(Session["kullaniciID"]);
            FavoriSorular favoriSoru = new FavoriSorular()
            {
                KullaniciID = kulID,
                SoruID = soruID
            };
            if (servis.SoruyuFavoriyeEkle(favoriSoru))
                return Json("soru Favoriye eklendi");
            else
                return Json("hata");
        }

        public JsonResult CevabıFavEkle(int cevapID)
        {
            servis = new KodusorServisClient();
            int kulID = Convert.ToInt32(Session["kullaniciID"]);
            FavoriCevaplar favoriCevap = new FavoriCevaplar()
            {
                KullaniciID = kulID,
                CevapID = cevapID
            };
            if (servis.CevabiFavoriyeEkle(favoriCevap))
                return Json("cevap favoriye eklendi");
            else
                return Json("hata");
        }

        public JsonResult CevapVer(Cevaplar cevap)
        {
            servis = new KodusorServisClient();
            int kulID = Convert.ToInt32(Session["kullaniciID"]);
            cevap.KullaniciID = kulID;
            cevap.Tarih = DateTime.Now;
            if (servis.CevapEkle(cevap))
                return Json("Cevabınız kayıt edildi");
            else
                return Json("hata");
        }

        public JsonResult YorumYap(Yorum yorum)
        {
            servis = new KodusorServisClient();
            int kulID = Convert.ToInt32(Session["kullaniciID"]);
            yorum.KullaniciID = kulID;
            yorum.Tarih = DateTime.Now;
            if (servis.YorumEkle(yorum))
                return Json("yorumunuz kaydedildi");
            else
                return Json("hata");
        }

        public JsonResult SoruBegen(int soruID)
        {
            servis = new KodusorServisClient();
            if (servis.SoruBegen(soruID))
                return Json("+");
            else
                return Json("-");
        }

        public JsonResult SoruBegenme(int soruID)
        {
            servis = new KodusorServisClient();
            if (servis.SoruBegenme(soruID))
                return Json("+");
            else
                return Json("-");
        }

        public JsonResult CevapBegen(int cevapID)
        {
            servis = new KodusorServisClient();
            if (servis.CevapBegen(cevapID))
                return Json("+");
            else
                return Json("-");
        }

        public JsonResult CevapBegenme(int cevapID)
        {
            servis = new KodusorServisClient();
            if (servis.CevapBegenme(cevapID))
                return Json("+");
            else
                return Json("-");
        }

        public JsonResult CevapOnayla(int soruID, int cevapID)
        {
            servis = new KodusorServisClient();
            if (servis.CevabıOnayla(soruID, cevapID))
                return Json("+");
            else
                return Json("-");
        }
    }
}