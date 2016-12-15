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
            IletisimBilgileriListesi ib;
            var zxc = db.Kullanicilar;
            foreach (var item in zxc)
            {
                ib = NesneDuzenle.IletisimBilgisiOlustur(item.IletisimBilgileri);
                kullaniciListesi k = new kullaniciListesi()
                {
                    Adi = item.Adi,
                    Soyadi = item.Soyadi,
                    DogumTarihi = Convert.ToDateTime(item.DogumTarihi),
                    Mail = item.Mail,
                    Parola = item.Parola,
                    ProfilFoto = item.ProfilFoto,
                    Hakkimda = item.Hakkimda,
                    IletisimBilgileriID = Convert.ToInt32(item.IletisimBilgileriID),
                    IletisimBilgileri = ib
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
                var favoriSorular = (from fs in db.FavoriSorular
                                     where fs.KullaniciID == kullaniciID
                                     select fs).ToList();

                foreach (var item in favoriSorular)
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
                        if (kontrol)
                            etiketler.Add(etiket);
                    }
                }
            }
            return etiketler;
        }

        public bool KullaniciBilgileriGuncelle(Kullanicilar kullanici, IletisimBilgileri iletisimBilgileri)
        {
            //KONTROL EDİLECEK !!!
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    var kul = (from k in db.Kullanicilar
                               where k.KullaniciID == kullanici.KullaniciID
                               select k).SingleOrDefault();

                    var iletisimB = (from b in db.IletisimBilgileri
                                     where b.IletisimBilgileriID == kul.IletisimBilgileriID
                                     select b).SingleOrDefault();
                    
                    if (iletisimB == null)
                    {
                        db.IletisimBilgileri.Add(iletisimBilgileri);
                        kul.IletisimBilgileri = iletisimBilgileri;
                    }
                    else
                    {
                        iletisimB.CepTel = iletisimBilgileri.CepTel;
                        iletisimB.Github = iletisimBilgileri.Github;
                        iletisimB.Linkedin = iletisimBilgileri.Linkedin;
                        iletisimB.Twitter = iletisimBilgileri.Twitter;
                        iletisimB.Website = iletisimBilgileri.Website;
                    }
                    kul.Hakkimda = kullanici.Hakkimda;
                    kul.Adi = kullanici.Adi;
                    kul.DogumTarihi = kullanici.DogumTarihi;
                    kul.Mail = kullanici.Mail;
                    kul.ProfilFoto = kullanici.ProfilFoto;
                    kul.Soyadi = kullanici.Soyadi;
                    
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ParolaDegistir(int kullaniciID, string parola)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    var kul = (from k in db.Kullanicilar
                               where k.KullaniciID == kullaniciID
                               select k).SingleOrDefault();

                    kul.Parola = parola;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public kullaniciListesi KullaniciBilgileriniGetir(int kullaniciID)
        {
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                var kul = (from k in db.Kullanicilar
                           where k.KullaniciID == kullaniciID
                           select k).SingleOrDefault();

                if (kul != null)
                    return NesneDuzenle.KullaniciOlustur(kul);
                else
                    return null;
            }
        }

        public bool SoruEkle(int kullaniciID, Sorular soru, List<Etiketler> etiketler)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    var kul = (from k in db.Kullanicilar
                               where k.KullaniciID == soru.KullaniciID
                               select k).FirstOrDefault();
                    
                    NesneDuzenle.EtiketEkle(etiketler);
                    //soru.KullaniciID = kullaniciID;
                    db.Sorular.Add(soru);
                    kul.Sorular.Add(soru);
                    db.SaveChanges();
                    SoruEtiket se;
                    foreach (var item in etiketler)
                    {
                        foreach (var e in db.Etiketler)
                        {
                            if (item.EtiketAdi == e.EtiketAdi)
                            {
                                se = new SoruEtiket()
                                {
                                    EtiketID = e.EtiketID,
                                    SoruID = soru.SoruID
                                };
                                db.SoruEtiket.Add(se);
                            }
                        }
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SoruSil(int kullaniciID, int soruID)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    var kul = (from k in db.Kullanicilar
                               where k.KullaniciID == kullaniciID
                               select k).FirstOrDefault();

                    var soru = (from s in db.Sorular
                                where s.SoruID == soruID
                                select s).FirstOrDefault();

                    kul.Sorular.Remove(soru);
                    foreach (var item in db.Cevaplar)
                    {
                        if (item.SoruID == soruID)
                            db.Cevaplar.Remove(item);
                    }
                    foreach (var item in db.SoruEtiket)
                    {
                        if (item.SoruID == soruID)
                            db.SoruEtiket.Remove(item);
                    }
                    foreach (var item in db.FavoriSorular)
                    {
                        if (item.SoruID == soruID)
                            db.FavoriSorular.Remove(item);
                    }
                    db.Sorular.Remove(soru);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CevapEkle(Cevaplar cevap)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    db.Cevaplar.Add(cevap);
                    var kul = (from k in db.Kullanicilar
                               where k.KullaniciID == cevap.KullaniciID
                               select k).SingleOrDefault();
                    kul.Cevaplar.Add(cevap);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool YorumEkle(Yorum yourum)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    var cevap = (from c in db.Cevaplar
                                 where c.CevapID == yourum.CevapID
                                 select c).SingleOrDefault();

                    var kul = (from k in db.Kullanicilar
                               where k.KullaniciID == yourum.KullaniciID
                               select k).SingleOrDefault();

                    db.Yorum.Add(yourum);
                    kul.Yorum.Add(yourum);
                    cevap.Yorum.Add(yourum);

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
