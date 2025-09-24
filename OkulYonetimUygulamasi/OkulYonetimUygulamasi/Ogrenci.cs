using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulYonetimUygulamasi
{
    internal class Ogrenci
    {
        public int No { get; set; }
        public string Ad {  get; set; }
        public string Soyad {  get; set; }
        public DateTime DogumTarihi { get; set; }
        public float Ortalama {  get; set; }
        public SUBE Sube {  get; set; }
        public CINSIYET Cinsiyet { get; set; }
        public Adres Adresi { get; set; }

        public List<DersNotu> Notlar = new List<DersNotu>();
        public List<string> Kitaplar = new List<string>();

    }
    public enum SUBE
    {
        Empty, A, B, C
    }
    public enum CINSIYET
    {
        Empty, Kiz, Erkek
    }
}
