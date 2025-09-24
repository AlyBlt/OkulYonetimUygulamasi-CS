using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulYonetimUygulamasi
{
    internal class Okul
    {
        // Okula ait herhnagi bir bir bilginin değiştirilmesi gerektiğinde, ilgili işlemler bu sınıfta yapılmalı.

        public List<Ogrenci> Ogrenciler = new List<Ogrenci>();


        public void OgrenciEkle(int no, string ad, string soyad, DateTime dogumTarihi, CINSIYET cinsiyet, SUBE sube)
        {
            // Ogrenci oluşacak

            Ogrenci o = new Ogrenci();

            o.No = no;
            o.Ad = ad;
            //
            //
            //
            //


            this.Ogrenciler.Add(o);
        }

        public void NotEkle(int no, string ders, int not)
        {
            Ogrenci o = this.Ogrenciler.Where(x => x.No == no).FirstOrDefault();

            if (o != null)
            {
                //DersNotu dn = new DersNotu(ders,not);

                //o.Notlar.Add(dn);


                o.Notlar.Add(  new DersNotu(ders, not)  );
            }
        }
    }
}
