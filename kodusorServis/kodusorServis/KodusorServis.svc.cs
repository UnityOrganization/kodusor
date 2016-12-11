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

        public List<SoruListesi> SorulariListele()
        {
            List<SoruListesi> sorular = new List<SoruListesi>();
            SoruListesi soru;
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                var soruListesi = db.Sorular.OrderByDescending(s => s.Tarih);

                foreach (var item in soruListesi)
                {
                    soru = new SoruListesi()
                    {
                        SoruID = item.SoruID,
                        Baslik = item.Baslik,
                        Icerik = item.Icerik,
                        Tarih = Convert.ToDateTime(item.Tarih),
                        KullaniciID = item.KullaniciID,
                        OnayCevapID = Convert.ToInt32(item.OnayCevapID),
                        BegeniSayisi = Convert.ToInt32(item.BegeniSayisi)
                    };
                    sorular.Add(soru);
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
                    DogumTarihi = item.DogumTarihi.ToString(),
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
    }
}
