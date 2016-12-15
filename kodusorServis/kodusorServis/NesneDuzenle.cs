﻿using System;
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
                Soyadi = kullanici.Soyadi
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
                CevapSayisi = soru.Cevaplar.Count
            };
            return Soru;
        }

        public static CevapListesi CevapOlustur (Cevaplar cevap)
        {
            var soru = (from s in db.Sorular
                        where s.SoruID == cevap.SoruID
                        select s).SingleOrDefault();

            Cevap = new CevapListesi()
            {
                CevapID = cevap.CevapID,
                KullaniciID = cevap.KullaniciID,
                SoruID = cevap.SoruID,
                Cevap = cevap.Cevap,
                BegeniSayisi = Convert.ToInt32(cevap.BegeniSayisi),
                Tarih = Convert.ToDateTime(cevap.Tarih),
                Sorular = SoruOlustur(soru)
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
    }
}