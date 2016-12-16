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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IKodusorServis
    {

        [OperationContract]
        string GetData(int value);


        // TODO: Add your service operations here
        [OperationContract]
        string KayitOl(Kullanicilar kullanici);

        [OperationContract]
        List<SoruListesi> SorulariListele(int id);

        [OperationContract]
        int GirisYap(string mail, string parola);

        [OperationContract]
        List<CevapListesi> KullaniciCevapları(int kullaniciID);

        [OperationContract]
        List<SoruListesi> FavoriSorular(int kullaniciID);

        [OperationContract]
        List<CevapListesi> FavoriCevaplar(int kullaniciID);

        [OperationContract]
        List<EtiketListesi> KullanicininEtiketleri(int kullaniciID);

        [OperationContract]
        bool KullaniciBilgileriGuncelle(Kullanicilar kullanici, IletisimBilgileri iletisimBilgileri);

        [OperationContract]
        bool ParolaDegistir(int kullaniciID, string parola);

        [OperationContract]
        kullaniciListesi KullaniciBilgileriniGetir(int kullaniciID);

        [OperationContract]
        bool SoruEkle(int kullaniciID, Sorular soru, List<Etiketler> etiketler);

        [OperationContract]
        bool SoruSil(int kullaniciID, int soruID);

        [OperationContract]
        bool CevapEkle(Cevaplar cevap);

        [OperationContract]
        bool YorumEkle(Yorum yourum);

        [OperationContract]
        bool SoruyuFavoriyeEkle(FavoriSorular favoriSorular);

        [OperationContract]
        bool CevabiFavoriyeEkle(FavoriCevaplar favoriCevaplar);


        [OperationContract]
        List<kullaniciListesi> KullanicilariListele();
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class SoruListesi
    {
        private int soruID;
        private string baslik;
        private string icerik;
        private DateTime tarih;
        private int begeniSayisi;
        private int onayCevapID;
        private kullaniciListesi kullanici;
        private List<EtiketListesi> etiketler;
        private int cevapSayisi;


        [DataMember]
        public int SoruID
        {
            get { return soruID; }
            set { soruID = value; }
        }

        [DataMember]
        public string Baslik
        {
            get { return baslik; }
            set { baslik = value; }
        }

        [DataMember]
        public string Icerik
        {
            get { return icerik; }
            set { icerik = value; }
        }

        [DataMember]
        public DateTime Tarih
        {
            get { return tarih; }
            set { tarih = value; }
        }

        [DataMember]
        public int BegeniSayisi
        {
            get { return begeniSayisi; }
            set { begeniSayisi = value; }
        }

        [DataMember]
        public int OnayCevapID
        {
            get { return onayCevapID; }
            set { onayCevapID = value; }
        }

        [DataMember]
        public kullaniciListesi Kullanici
        {
            get { return kullanici; }
            set { kullanici = value; }
        }

        [DataMember]
        public List<EtiketListesi> Etiketler
        {
            get { return etiketler; }
            set { etiketler = value; }
        }

        [DataMember]
        public int CevapSayisi
        {
            get { return cevapSayisi; }
            set { cevapSayisi = value; }
        }
    }

    [DataContract]
    public class kullaniciListesi
    {
        private int kullaniciID = 0;
        private string adi = "";
        private string soyadi = "";
        private DateTime dogumTarihi = DateTime.Now;
        private string mail = "";
        private string parola = "";
        private string profilFoto = "";
        private string hakkimda = "";
        private int iletisimBilgileriID = 0;
        private IletisimBilgileriListesi iletisimBilgileri = null;
        
        [DataMember]
        public IletisimBilgileriListesi IletisimBilgileri
        {
            get { return iletisimBilgileri; }
            set { iletisimBilgileri = value; }
        }
        [DataMember]
        public int IletisimBilgileriID
        {
            get { return iletisimBilgileriID; }
            set { iletisimBilgileriID = value; }
        }
        [DataMember]
        public int KullaniciID
        {
            get { return kullaniciID; }
            set { kullaniciID = value; }
        }
        [DataMember]
        public string Adi
        {
            get { return adi; }
            set { adi = value; }
        }
        [DataMember]
        public string Soyadi
        {
            get { return soyadi; }
            set { soyadi = value; }
        }
        [DataMember]
        public DateTime DogumTarihi
        {
            get { return dogumTarihi; }
            set { dogumTarihi = value; }
        }
        [DataMember]
        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }
        [DataMember]
        public string Parola
        {
            get { return parola; }
            set { parola = value; }
        }
        [DataMember]
        public string ProfilFoto
        {
            get { return profilFoto; }
            set { profilFoto = value; }
        }
        [DataMember]
        public string Hakkimda
        {
            get { return hakkimda; }
            set { hakkimda = value; }
        }
    }

    [DataContract]
    public class EtiketListesi
    {
        private int etiketID;
        private string etiketAdi;

        [DataMember]
        public int EtiketID
        {
            get { return etiketID; }
            set { etiketID = value; }
        }

        [DataMember]
        public string EtiketAdi
        {
            get { return etiketAdi; }
            set { etiketAdi = value; }
        }
    }

    [DataContract]
    public class CevapListesi
    {
        private int cevapID;
        [DataMember]
        public int CevapID
        {
            get { return cevapID; }
            set { cevapID = value; }
        }

        private int soruID;
        [DataMember]
        public int SoruID
        {
            get { return soruID; }
            set { soruID = value; }
        }

        private int kullaniciID;
        [DataMember]
        public int KullaniciID
        {
            get { return kullaniciID; }
            set { kullaniciID = value; }
        }

        private string cevap;
        [DataMember]
        public string Cevap
        {
            get { return cevap; }
            set { cevap = value; }
        }

        private DateTime tarih;
        [DataMember]
        public DateTime Tarih
        {
            get { return tarih; }
            set { tarih = value; }
        }

        private int begeniSayisi;
        [DataMember]
        public int BegeniSayisi
        {
            get { return begeniSayisi; }
            set { begeniSayisi = value; }
        }

        private SoruListesi sorular;
        [DataMember]
        public SoruListesi Sorular
        {
            get { return sorular; }
            set { sorular = value; }
        }

    }

    [DataContract]
    public class IletisimBilgileriListesi
    {
        private int iletisimBilgileriID;
        [DataMember]
        public int IletisimBilgileriID
        {
            get { return iletisimBilgileriID; }
            set { iletisimBilgileriID = value; }
        }

        private string website;
        [DataMember]
        public string Website
        {
            get { return website; }
            set { website = value; }
        }
        
        private string twitter;
        [DataMember]
        public string Twitter
        {
            get { return twitter; }
            set { twitter = value; }
        }
        
        private string github;
        [DataMember]
        public string Github
        {
            get { return github; }
            set { github = value; }
        }
        
        private string linkedin;
        [DataMember]
        public string Linkedin
        {
            get { return linkedin; }
            set { linkedin = value; }
        }
        
        private string ceptel;
        [DataMember]
        public string Ceptel
        {
            get { return ceptel; }
            set { ceptel = value; }
        }

    }
}
