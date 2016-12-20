using System;
using System.Linq;
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

        public JsonResult SoruAra(string aranacakSoru)
        {
            servis = new KodusorServisClient();
            var sorular = servis.SoruAra(aranacakSoru);
            servis.Close();
            return Json(sorular);
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
        
        public JsonResult SoruyuFavEkle(int soruID)
        {
            servis = new KodusorServisClient();
            int kulID = Convert.ToInt32(Session["kullaniciID"]);
            FavoriSorular favoriSoru = new FavoriSorular()
            {
                KullaniciID = kulID,
                SoruID = soruID
            };
            return Json(servis.SoruyuFavoriyeEkle(favoriSoru));
        }

        public JsonResult CevabiFavEkle(int cevapID)
        {
            servis = new KodusorServisClient();
            int kulID = Convert.ToInt32(Session["kullaniciID"]);
            FavoriCevaplar favoriCevap = new FavoriCevaplar()
            {
                KullaniciID = kulID,
                CevapID = cevapID
            };
            return Json(servis.CevabiFavoriyeEkle(favoriCevap));
        }

        public JsonResult CevapVer(Cevaplar cevap)
        {
            servis = new KodusorServisClient();
            int kulID = Convert.ToInt32(Session["kullaniciID"]);
            cevap.KullaniciID = kulID;
            cevap.Tarih = DateTime.Now;
            return Json(servis.CevapEkle(cevap));
        }

        public JsonResult YorumYap(Yorum yorum)
        {
            servis = new KodusorServisClient();
            int kulID = Convert.ToInt32(Session["kullaniciID"]);
            yorum.KullaniciID = kulID;
            yorum.Tarih = DateTime.Now;
            return Json(servis.YorumEkle(yorum));
        }

        public JsonResult SoruBegen(int soruID)
        {
            servis = new KodusorServisClient();
            return Json(servis.SoruBegen(soruID));
        }

        public JsonResult SoruBegenme(int soruID)
        {
            servis = new KodusorServisClient();
            return Json(servis.SoruBegenme(soruID));
        }

        public JsonResult CevapBegen(int cevapID)
        {
            servis = new KodusorServisClient();
            return Json(servis.CevapBegen(cevapID));
        }

        public JsonResult CevapBegenme(int cevapID)
        {
            servis = new KodusorServisClient();
            return Json(servis.CevapBegenme(cevapID));
        }

        public JsonResult CevapOnayla(int soruID, int cevapID)
        {
            servis = new KodusorServisClient();
            return Json(servis.CevabıOnayla(soruID, cevapID));
        }
    }
}