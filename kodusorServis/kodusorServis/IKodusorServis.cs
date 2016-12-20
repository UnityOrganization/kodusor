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
        bool ParolaDegistir(int kullaniciID, string parola);

        [OperationContract]
        kullaniciListesi KullaniciBilgileriniGetir(int kullaniciID);

        [OperationContract]
        bool SoruEkle(int kullaniciID, Sorular soru, List<Etiketler> etiketler);

        [OperationContract]
        bool CevapEkle(Cevaplar cevap);

        [OperationContract]
        bool YorumEkle(Yorum yorum);

        [OperationContract]
        bool SoruyuFavoriyeEkle(FavoriSorular favoriSorular);

        [OperationContract]
        bool CevabiFavoriyeEkle(FavoriCevaplar favoriCevaplar);

        [OperationContract]
        bool CevabıOnayla(int soruID, int cevapID);

        [OperationContract]
        SoruListesi SoruGetir(int soruID);

        [OperationContract]
        bool SoruBegen(int soruID);

        [OperationContract]
        bool SoruBegenme(int soruID);

        [OperationContract]
        bool CevapBegen(int cevapID);

        [OperationContract]
        bool CevapBegenme(int cevapID);

        [OperationContract]
        List<SoruListesi> SoruAra(string baslik);
    }

    
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
        private List<CevapListesi> cevaplar;


        [DataMember]
        public List<CevapListesi> Cevaplar
        {
            get { return cevaplar; }
            set { cevaplar = value; }
        }

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
        private string github;
        private string ceptel;
        private string linkedin;


        [DataMember]
        public string Linkedin
        {
            get { return linkedin; }
            set { linkedin = value; }
        }
        [DataMember]
        public string Ceptel
        {
            get { return ceptel; }
            set { ceptel = value; }
        }
        [DataMember]
        public string Github
        {
            get { return github; }
            set { github = value; }
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

        private List<YorumListesi> yorumListesi;
        [DataMember]
        public List<YorumListesi> YorumListesi
        {
            get { return yorumListesi; }
            set { yorumListesi = value; }
        }

        private kullaniciListesi kullanici;
        [DataMember]
        public kullaniciListesi Kullanici
        {
            get { return kullanici; }
            set { kullanici = value; }
        }

    }
    
    [DataContract]
    public class YorumListesi
    {
        private int yorumID;
        [DataMember]
        public int YorumID
        {
            get { return yorumID; }
            set { yorumID = value; }
        }

        private string yorum;
        [DataMember]
        public string Yorum
        {
            get { return yorum; }
            set { yorum = value; }
        }

        private DateTime tarih;
        [DataMember]
        public DateTime Tarih
        {
            get { return tarih; }
            set { tarih = value; }
        }

        private kullaniciListesi kullanici;
        [DataMember]
        public kullaniciListesi Kullanici
        {
            get { return kullanici; }
            set { kullanici = value; }
        }

    }
}
