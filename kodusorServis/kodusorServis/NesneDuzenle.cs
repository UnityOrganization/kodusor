using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kodusorServis.Models;

namespace kodusorServis
{
    public static class NesneDuzenle
    {
        private static kodusorDBEntities db = new kodusorDBEntities();
        private static EtiketListesi Etiket { get; set; }
        private static SoruListesi Soru { get; set; }
        private static kullaniciListesi Kullanici { get; set; }
        private static List<EtiketListesi> etiketler;
        private static CevapListesi Cevap { get; set; }
        private static List<SoruListesi> sorular;
        private static IletisimBilgileriListesi Iletisim { get; set; }
        private static List<CevapListesi> cevaplar { get; set; }
        private static YorumListesi yorum;
        private static List<YorumListesi> yorumlar { get; set; }

        public static EtiketListesi EtiketOlustur(SoruEtiket etiket)
        {
            Etiket = new EtiketListesi()
            {
                EtiketAdi = etiket.Etiketler.EtiketAdi,
                EtiketID = etiket.EtiketID
            };
            return Etiket;
        }

        public static kullaniciListesi KullaniciOlustur(Kullanicilar kullanici)
        {
            Kullanici = new kullaniciListesi()
            {
                Adi = kullanici.Adi,
                DogumTarihi = Convert.ToDateTime(kullanici.DogumTarihi),
                Hakkimda = kullanici.Hakkimda,
                KullaniciID = kullanici.KullaniciID,
                Mail = kullanici.Mail,
                Parola = kullanici.Parola,
                ProfilFoto = kullanici.ProfilFoto,
                Soyadi = kullanici.Soyadi,
                IletisimBilgileri = IletisimBilgisiOlustur(kullanici.IletisimBilgileri)
            };
            return Kullanici;
        }

        public static SoruListesi SoruOlustur(Sorular soru)
        {
            etiketler = new List<EtiketListesi>();
            foreach (var item in soru.SoruEtiket)
            {
                etiketler.Add(EtiketOlustur(item));
            }
            cevaplar = new List<CevapListesi>();
            foreach (var item in soru.Cevaplar)
            {
                cevaplar.Add(CevapOlustur(item));
            }

            Soru = new SoruListesi()
            {
                SoruID = soru.SoruID,
                Baslik = soru.Baslik,
                Icerik = soru.Icerik,
                Tarih = Convert.ToDateTime(soru.Tarih),
                Kullanici = KullaniciOlustur(soru.Kullanicilar),
                OnayCevapID = Convert.ToInt32(soru.OnayCevapID),
                BegeniSayisi = Convert.ToInt32(soru.BegeniSayisi),
                Etiketler = etiketler,
                CevapSayisi = soru.Cevaplar.Count,
                Cevaplar = cevaplar
            };
            return Soru;
        }

        public static CevapListesi CevapOlustur (Cevaplar cevap)
        {
            yorumlar = new List<YorumListesi>();
            foreach (var item in cevap.Yorum)
            {
                yorumlar.Add(YorumOlustur(item));
            }
            
            Cevap = new CevapListesi()
            {
                CevapID = cevap.CevapID,
                KullaniciID = cevap.KullaniciID,
                SoruID = cevap.SoruID,
                Cevap = cevap.Cevap,
                BegeniSayisi = Convert.ToInt32(cevap.BegeniSayisi),
                Tarih = Convert.ToDateTime(cevap.Tarih),
                YorumListesi = yorumlar
            };
            return Cevap;
        }

        public static IletisimBilgileriListesi IletisimBilgisiOlustur(IletisimBilgileri iletisimBilgileri)
        {
            Iletisim = new IletisimBilgileriListesi()
            {
                Ceptel = iletisimBilgileri.CepTel,
                Github = iletisimBilgileri.Github,
                IletisimBilgileriID = iletisimBilgileri.IletisimBilgileriID,
                Linkedin = iletisimBilgileri.Linkedin,
                Twitter = iletisimBilgileri.Twitter,
                Website = iletisimBilgileri.Website
            };
            return Iletisim;
        }

        public static void EtiketEkle(List<Etiketler> etiketler)
        {
            using (kodusorDBEntities db = new kodusorDBEntities())
            {
                bool durum;
                foreach (var item in etiketler)
                {
                    durum = true;
                    foreach (var e in db.Etiketler)
                    {
                        if (e.EtiketAdi == item.EtiketAdi)
                        {
                            durum = false;
                            break;
                        }
                    }
                    if (durum)
                        db.Etiketler.Add(item);
                }
                db.SaveChanges();
            }
        }

        public static YorumListesi YorumOlustur(Yorum y)
        {
            yorum = new YorumListesi()
            {
                Yorum = y.Yorum1,
                Tarih = y.Tarih,
                YorumID = y.YorumID
            };
            return yorum;
        }
    }
}