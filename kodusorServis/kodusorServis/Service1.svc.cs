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
    public class Service1 : IService1
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
                        return "Kayıt başarılı";
                    }
                    else
                        return "Kayıt başarısız";
                }
            }
            catch (Exception ex)
            {
                return "başarısız - " + ex.Message;
            }
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

        public List<Kullanicilar> listele()
        {
            List<Kullanicilar> kul = new List<Kullanicilar>();
            Kullanicilar k;
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                foreach (var item in db.Kullanicilar)
                {
                    k = new Kullanicilar();
                    k.Adi = item.Adi;
                    k.Soyadi = item.Soyadi;
                    k.Mail = item.Mail;
                    k.Parola = item.Parola;
                    kul.Add(k);
                }
            }
            
            return kul;
        }
    }
}
