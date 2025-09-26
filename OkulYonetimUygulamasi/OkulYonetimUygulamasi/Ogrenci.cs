using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulYonetimUygulamasi
{
    internal class Ogrenci
    {
        public int No { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime DogumTarihi { get; set; }
        public SUBE Sube { get; set; }
        public CINSIYET Cinsiyet { get; set; }
        public Adres Adresi { get; set; }

        public List<DersNotu> Notlar = new List<DersNotu>();
        public List<string> Kitaplar = new List<string>();

        public float Ortalama
        {
            get
            {
                if (Notlar == null || Notlar.Count == 0) return 0;
                float ort = Notlar.Sum(x => x.Not);
                return ort / this.Notlar.Count;

            }
        }

        public int KitapSayisi
        {
            get
            {
                return this.Kitaplar.Count;
            }

        }


        public void KitapEkle(string kitapAdi)
        {
            // Önce boşsa listeyi oluştur (opsiyonel)
            if (Kitaplar == null)
                Kitaplar = new List<string>();

            // Kitap listesinde büyük-küçük harfe duyarsız kontrol yaparak varsa ekleme
            if (!Kitaplar.Any(k => string.Equals(k, kitapAdi.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                Kitaplar.Add(kitapAdi.Trim());
            }
        }

        public void AdresEkle(string il, string ilce, string mahalle)
        {
            if (string.IsNullOrWhiteSpace(il) || string.IsNullOrWhiteSpace(ilce) || string.IsNullOrWhiteSpace(mahalle))
            {
                Console.WriteLine("Adres bilgileri eksik. İl ve ilçe boş olamaz.");
                return;
            }

            this.Adresi = new Adres
            {
                Il = il.Trim(),
                Ilce = ilce.Trim(),
                Mahalle = mahalle.Trim()
            };
        }

              
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
