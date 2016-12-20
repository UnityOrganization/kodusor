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
        //
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
                        kullanici.ProfilFoto = "http://www.excavationmadopal.com/wp-content/uploads/2015/02/avatar.jpg";
                        kullanici.DogumTarihi = DateTime.Now;
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
        //
        public List<SoruListesi> SorulariListele(int id)
        {
            List<SoruListesi> sorular = new List<SoruListesi>();
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                IEnumerable<object> soruListesi = null;
                if (id == 0)
                    soruListesi = db.Sorular.OrderByDescending(s => s.Tarih);
                else
                    soruListesi = db.Sorular.OrderByDescending(s => s.Tarih).Where(s => s.KullaniciID == id);

                foreach (var item in soruListesi)
                {
                    sorular.Add(NesneDuzenle.SoruOlustur((Sorular)item));
                }
            }
            return sorular;
        }
        //
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
        //
        public List<CevapListesi> KullaniciCevapları(int kullaniciID)
        {
            List<CevapListesi> cevaplar = new List<CevapListesi>();
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                var kulCevaplar = db.Cevaplar.OrderByDescending(kc => kc.Tarih).Where(kc => kc.KullaniciID == kullaniciID);
                foreach (var item in kulCevaplar)
                {
                    cevaplar.Add(NesneDuzenle.CevapOlustur(item));
                }
            }
            return cevaplar;
        }
        //
        public List<SoruListesi> FavoriSorular(int kullaniciID)
        {
            List<SoruListesi> sorular = new List<SoruListesi>();
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                var favoriSorular = db.FavoriSorular.OrderByDescending(fs => fs.Sorular.Tarih).Where(fs => fs.KullaniciID == kullaniciID).ToList();

                foreach (var item in favoriSorular)
                {
                    sorular.Add(NesneDuzenle.SoruOlustur(item.Sorular));
                }
            }
            return sorular;
        }
        //
        public List<CevapListesi> FavoriCevaplar(int kullaniciID)
        {
            List<CevapListesi> cevaplar = new List<CevapListesi>();
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                var favoriCevaplar = db.FavoriCevaplar.OrderByDescending(fc => fc.Cevaplar.Tarih).Where(fc => fc.KullaniciID == kullaniciID).ToList();

                foreach (var item in favoriCevaplar)
                {
                    cevaplar.Add(NesneDuzenle.CevapOlustur(item.Cevaplar));
                }
            }
            return cevaplar;
        }
        //
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
        //
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
        //
        public bool SoruEkle(int kullaniciID, Sorular soru, List<Etiketler> etiketler)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    NesneDuzenle.EtiketEkle(etiketler);
                    soru.KullaniciID = kullaniciID;
                    soru.Tarih = DateTime.Now;
                    soru.BegeniSayisi = 0;

                    db.Sorular.Add(soru);
                    db.SaveChanges();

                    var kul = (from k in db.Kullanicilar
                               where k.KullaniciID == soru.KullaniciID
                               select k).FirstOrDefault();
                    kul.Sorular.Add(soru);
                    
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
        //
        public bool CevapEkle(Cevaplar cevap)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    cevap.BegeniSayisi = 0;
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
        //
        public bool YorumEkle(Yorum yorum)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    var cevap = (from c in db.Cevaplar
                                 where c.CevapID == yorum.CevapID
                                 select c).SingleOrDefault();

                    var kul = (from k in db.Kullanicilar
                               where k.KullaniciID == yorum.KullaniciID
                               select k).SingleOrDefault();

                    db.Yorum.Add(yorum);
                    kul.Yorum.Add(yorum);
                    cevap.Yorum.Add(yorum);

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //
        public bool SoruyuFavoriyeEkle(FavoriSorular favoriSorular)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    var favorisoru = (from fs in db.FavoriSorular
                                      where fs.KullaniciID == favoriSorular.KullaniciID && fs.SoruID == favoriSorular.SoruID
                                      select fs).SingleOrDefault();

                    var kul = (from k in db.Kullanicilar
                               where k.KullaniciID == favoriSorular.KullaniciID
                               select k).SingleOrDefault();

                    var soru = (from s in db.Sorular
                                where s.SoruID == favoriSorular.SoruID
                                select s).SingleOrDefault();

                    if (favorisoru == null)
                    {
                        kul.FavoriSorular.Add(favoriSorular);
                        soru.FavoriSorular.Add(favoriSorular);
                    }
                    else
                    {
                        kul.FavoriSorular.Remove(favorisoru);
                        soru.FavoriSorular.Remove(favorisoru);
                        db.FavoriSorular.Remove(favorisoru);
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
        //
        public bool CevabiFavoriyeEkle(FavoriCevaplar favoriCevaplar)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    var favoricevap = (from fc in db.FavoriCevaplar
                                      where fc.KullaniciID == favoriCevaplar.KullaniciID && fc.CevapID == favoriCevaplar.CevapID
                                      select fc).SingleOrDefault();

                    var kul = (from k in db.Kullanicilar
                               where k.KullaniciID == favoriCevaplar.KullaniciID
                               select k).SingleOrDefault();

                    var cevap = (from c in db.Cevaplar
                                where c.CevapID == favoriCevaplar.CevapID
                                select c).SingleOrDefault();

                    if (favoricevap == null)
                    {
                        kul.FavoriCevaplar.Add(favoriCevaplar);
                        cevap.FavoriCevaplar.Add(favoriCevaplar);
                    }
                    else
                    {
                        kul.FavoriCevaplar.Remove(favoricevap);
                        cevap.FavoriCevaplar.Remove(favoricevap);
                        db.FavoriCevaplar.Remove(favoricevap);
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

        public bool CevabıOnayla(int soruID, int cevapID)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    var soru = (from s in db.Sorular
                                where s.SoruID == soruID
                                select s).SingleOrDefault();

                    if (soru.OnayCevapID == cevapID)
                        soru.OnayCevapID = null;
                    else
                        soru.OnayCevapID = cevapID;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public SoruListesi SoruGetir(int soruID)
        {
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                var soru = (from s in db.Sorular
                            where s.SoruID == soruID
                            select s).SingleOrDefault();

                if (soru != null)
                    return NesneDuzenle.SoruOlustur(soru);
                else
                    return null;
            }
        }

        public bool SoruBegen(int soruID)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    var soru = (from s in db.Sorular
                                where s.SoruID == soruID
                                select s).SingleOrDefault();

                    soru.BegeniSayisi += 1;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SoruBegenme(int soruID)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    var soru = (from s in db.Sorular
                                where s.SoruID == soruID
                                select s).SingleOrDefault();

                    soru.BegeniSayisi -= 1;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CevapBegen(int cevapID)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    var cevap = (from c in db.Cevaplar
                                where c.CevapID == cevapID
                                select c).SingleOrDefault();

                    cevap.BegeniSayisi += 1;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CevapBegenme(int cevapID)
        {
            try
            {
                using (kodusorDBEntities db = new kodusorDBEntities())
                {
                    var cevap = (from c in db.Cevaplar
                                 where c.CevapID == cevapID
                                 select c).SingleOrDefault();

                    cevap.BegeniSayisi -= 1;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<SoruListesi> SoruAra(string baslik)
        {
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                List<SoruListesi> arananSorular = new List<SoruListesi>();
                var sorular = (from s in db.Sorular
                               where s.Baslik.Contains(baslik)
                               orderby s.Tarih descending
                               select s).ToList();
                foreach (var item in sorular)
                {
                    arananSorular.Add(NesneDuzenle.SoruOlustur(item));
                }
                return arananSorular;
            }
        }
    }
}
