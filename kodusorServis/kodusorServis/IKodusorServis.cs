﻿using System;
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
        List<SoruListesi> SolulariListele();

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
        private int kullaniciID;


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
        public int KullaniciID
        {
            get { return kullaniciID; }
            set { kullaniciID = value; }
        }
    }



    [DataContract]
    public class kullaniciListesi
    {
        private int kullaniciID = 0;
        private string adi = "";
        private string soyadi = "";
        private string dogumTarihi = "";
        private string mail = "";
        private string parola = "";
        private string profilFoto = "";
        private string hakkimda = "";
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
        public string DogumTarihi
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
}
