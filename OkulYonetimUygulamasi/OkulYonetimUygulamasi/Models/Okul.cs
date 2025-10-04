using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulYonetimUygulamasi.Models
{
    public class Okul
    {
       
        public List<Ogrenci> Ogrenciler = new List<Ogrenci>();


        public void OgrenciEkle(int no, string ad, string soyad, DateTime dogumTarihi, CINSIYET cinsiyet, SUBE sube)
        {
            // Ogrenci oluşacak

            Ogrenci ogrenci = new Ogrenci();

            ogrenci.No = no;
            ogrenci.Ad = ad;
            ogrenci.Soyad = soyad;
            ogrenci.DogumTarihi = dogumTarihi;
            ogrenci.Cinsiyet = cinsiyet;
            ogrenci.Sube = sube;
            
            Ogrenciler.Add(ogrenci);
        }

        public void OgrenciSil(int no)
        {
            Ogrenci ogrenci = Ogrenciler.Where(x => x.No == no).FirstOrDefault();

            if (ogrenci != null)
            {
                Ogrenciler.Remove(ogrenci);
            }
        }

        public void OgrenciGuncelle(int no, string ad, string soyAd, DateTime dogumTarihi, CINSIYET cinsiyet, SUBE sube)
        {
            Ogrenci ogrenci = Ogrenciler.Where(x => x.No == no).FirstOrDefault();

            if (ogrenci != null)
            {
                ogrenci.No = no;
                ogrenci.Ad = ad;
                ogrenci.Soyad = soyAd;
                ogrenci.DogumTarihi = dogumTarihi;
                ogrenci.Cinsiyet = cinsiyet;
                ogrenci.Sube = sube;
            }
        }
        public void NotEkle(int no, string ders, int not)
        {
            Ogrenci ogrenci = Ogrenciler.Where(x => x.No == no).FirstOrDefault();

            if (ogrenci != null)
            {
               ogrenci.Notlar.Add(  new DersNotu(ders, not)  );
            }
        }

        public void KitapEkle(int no, string kitapAdi)
        {
            Ogrenci ogrenci = Ogrenciler.Where(x => x.No == no).FirstOrDefault();
            if (ogrenci != null)
            {
                ogrenci.KitapEkle(kitapAdi);
            }
        }

        public void AdresEkle(int no, string il, string ilce, string mahalle)
        {
            Ogrenci ogrenci = Ogrenciler.Where(a => a.No == no).FirstOrDefault();
            if (ogrenci != null)
            {
                ogrenci.AdresEkle(il, ilce, mahalle);
            }
        }
    }
}
