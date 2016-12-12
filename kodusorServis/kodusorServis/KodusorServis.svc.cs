using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using kodusorServis.Models;

namespace kodusorServis
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IKodusorServis
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string KayitOl(Kullanicilar kullanici)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    var kul = (from k in db.Kullanicilar
                               where k.Mail == kullanici.Mail
                               select k).SingleOrDefault();

                    if (kul == null)
                    {
                        db.Kullanicilar.Add(kullanici);
                        db.SaveChanges();
                        return "+";
                    }
                    else
                        return "-";
                }
            }
            catch (Exception)
            {
                return "--";
            }
        }

        public List<SoruListesi> SorulariListele(int id)
        {
            List<SoruListesi> sorular = new List<SoruListesi>();
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                IEnumerable<object> soruListesi = null;
                if (id == 0)
                    soruListesi = db.Sorular.OrderByDescending(s => s.Tarih);
                else
                    soruListesi = db.Sorular.Where(s => s.KullaniciID == id).OrderByDescending(s => s.Tarih);
                
                foreach (var item in soruListesi)
                {                    
                    sorular.Add(NesneDuzenle.SoruOlustur((Sorular)item));
                }
            }
            return sorular;
        }

        public List<kullaniciListesi> KullanicilariListele()
        {

            kodusorDBEntities db = new kodusorDBEntities();
            List<kullaniciListesi> kul = new List<kullaniciListesi>();
            var zxc = db.Kullanicilar;
            foreach (var item in zxc)
            {
                kullaniciListesi k = new kullaniciListesi()
                {
                    Adi = item.Adi,
                    Soyadi = item.Soyadi,
                    DogumTarihi = Convert.ToDateTime(item.DogumTarihi),
                    Mail = item.Mail,
                    Parola = item.Parola,
                    ProfilFoto = item.ProfilFoto,
                    Hakkimda = item.Hakkimda,
                };

                kul.Add(k);
            }
            return kul;
        }

        public int GirisYap(string mail, string parola)
        {
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                var kul = (from k in db.Kullanicilar
                           where k.Mail == mail && k.Parola == parola
                           select k).SingleOrDefault();
                if (kul != null)
                    return kul.KullaniciID;
                else
                    return 0;
            }
        }

        public List<CevapListesi> KullaniciCevapları(int kullaniciID)
        {
            List<CevapListesi> cevaplar = new List<CevapListesi>();
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                var kul = (from k in db.Kullanicilar
                           where k.KullaniciID == kullaniciID
                           select k).SingleOrDefault();

                foreach (var item in kul.Cevaplar)
                {
                    cevaplar.Add(NesneDuzenle.CevapOlustur(item));
                }
            }
            return cevaplar;
        }

        public List<SoruListesi> FavoriSorular(int kullaniciID)
        {
            List<SoruListesi> sorular = new List<SoruListesi>();
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                var kul = (from k in db.Kullanicilar
                           where k.KullaniciID == kullaniciID
                           select k).SingleOrDefault();
                foreach (var item in kul.FavoriSorular)
                {
                    sorular.Add(NesneDuzenle.SoruOlustur(item.Sorular));
                }
            }
            return sorular;
        }

        public List<CevapListesi> FavoriCevaplar(int kullaniciID)
        {
            List<CevapListesi> cevaplar = new List<CevapListesi>();
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                var kul = (from k in db.Kullanicilar
                           where k.KullaniciID == kullaniciID
                           select k).SingleOrDefault();
                foreach (var item in kul.FavoriCevaplar)
                {
                    cevaplar.Add(NesneDuzenle.CevapOlustur(item.Cevaplar));
                }
            }
            return cevaplar;
        }

        public List<EtiketListesi> KullanicininEtiketleri(int kullaniciID)
        {
            List<EtiketListesi> etiketler = new List<EtiketListesi>();
            bool kontrol = true;
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                var kul = (from k in db.Kullanicilar
                           where k.KullaniciID == kullaniciID
                           select k).SingleOrDefault();

                foreach (var s in kul.Sorular)
                {
                    foreach (var e in s.SoruEtiket)
                    {
                        EtiketListesi etiket = NesneDuzenle.EtiketOlustur(e);
                        foreach (var item in etiketler)
                        {
                            if (item.EtiketID == etiket.EtiketID)
                            {
                                kontrol = false;
                                break;
                            }
                            else
                                kontrol = true;
                        }
                        if(kontrol)
                            etiketler.Add(etiket);
                    }
                }
            }
            return etiketler;
        }
    }
}
