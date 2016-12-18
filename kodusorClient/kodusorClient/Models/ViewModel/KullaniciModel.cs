using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kodusorClient.kodusorServis;

namespace kodusorClient.ViewModel
{
    public class KullaniciModel
    {
        public kullaniciListesi Kullanici { get; set; }
        public List<SoruListesi> SoruListesi { get; set; }
        public List<CevapListesi> CevapListesi { get; set; }
        public List<EtiketListesi> EtiketListesi { get; set; }
        public List<SoruListesi> FavoriSorular { get; set; }
        public List<CevapListesi> FavoriCevaplar { get; set; }
        public SoruListesi Soru { get; set; }
    }
}