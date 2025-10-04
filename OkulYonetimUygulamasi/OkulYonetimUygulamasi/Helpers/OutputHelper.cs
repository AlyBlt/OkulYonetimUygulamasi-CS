using OkulYonetimUygulamasi.Interfaces;
using OkulYonetimUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulYonetimUygulamasi.Helpers
{
    public class OutputHelper
    {
        
        private readonly IOutputProvider _output;
        private readonly IInputProvider _input;
        private readonly InputHelper _inputHelper;
        public OutputHelper(IOutputProvider output, IInputProvider input, InputHelper inputHelper)
        {
            _output = output;
            _input = input;
            _inputHelper = inputHelper;
        }
        public void WriteLine(string message)
        {
            _output.WriteLine(message);
        }
        public void Write(string message)
        {
            _output.Write(message);
        }

        public void BaslikYazdir(int baslikno, string baslik)
        {
            _output.WriteLine("\n" + baslikno + "-" + baslik + "-----------------------------------------------------------------------\n");
        }
        public void ListeCikis()
        {
            _output.WriteLine("\nMenüyü tekrar listelemek için \"liste\", çıkış yapmak için \"çıkış\" yazın.");
        }
        public void OgrenciListele(List<Ogrenci> liste, int no, string baslik, bool baslikYazdir = true)
        {
            //BaslikYazdir(no, baslik);
            if (baslikYazdir)
                BaslikYazdir(no, baslik);
            _output.WriteLine("Şube".PadRight(15) + "No".PadRight(15) + "Adı".PadRight(5) + "Soyadı".PadRight(20) + "Not Ort.".PadRight(15) + "Okuduğu Kitap Say.");
            _output.WriteLine("----------------------------------------------------------------------------------------------");

            foreach (Ogrenci ogrenci in liste)
            {
                _output.WriteLine(ogrenci.Sube.ToString().PadRight(15) + ogrenci.No.ToString().PadRight(15) + (ogrenci.Ad + " " +
                    ogrenci.Soyad).PadRight(25) + ogrenci.Ortalama.ToString("0.00").PadRight(15) + ogrenci.KitapSayisi);
            }

            ListeCikis();
        }
        public void OgrenciListele1(int no)
        {
            BaslikYazdir(5, "İllere Göre Ögrencileri Listele ");
            _output.WriteLine("Şube".PadRight(15) + "No".PadRight(15) + "Adı".PadRight(5) + "Soyadı".PadRight(20) + "Şehir".PadRight(15) + "Semt");
            _output.WriteLine("------------------------------------------------------------------------------------------------------");
            var liste = Uygulama.okul.Ogrenciler.Where(o => o.Adresi != null && !string.IsNullOrWhiteSpace(o.Adresi.Il))
            .OrderBy(o => o.Adresi.Il).ThenBy(o => o.No).ToList();
            foreach (Ogrenci ogrenci in liste)
            {
                _output.WriteLine(ogrenci.Sube.ToString().PadRight(15) + ogrenci.No.ToString().PadRight(15) + (ogrenci.Ad + " " +
                    ogrenci.Soyad).PadRight(25) + ogrenci.Adresi.Il.ToString().PadRight(15) + ogrenci.Adresi.Ilce);
            }

            ListeCikis();
        }
        public void OgrenciListele2(int no)
        {
            BaslikYazdir(6, "Öğrencinin notlarını görüntüle");
            while (true)
            {
                int sayi = _inputHelper.OgrenciNoKontrol();

                var ogrenci = HelperFunctions.OgrenciBulNo(Uygulama.okul, sayi);
                if (ogrenci == null)
                {
                    Console.WriteLine("Bu numarada bir öğrenci yok. Tekrar deneyin.");
                    continue;
                }

                OgrenciBilgiYazdir(ogrenci);

                if (ogrenci.Notlar.Count == 0)
                {
                    Console.WriteLine("\n" + ogrenci.Ad + " " + ogrenci.Soyad + " öğrencisine ait bir not bulunmamaktadır");
                    ListeCikis();
                    return;
                }


                Console.WriteLine("\nDersin Adı".PadRight(15) + "Notu");
                Console.WriteLine("-------------------");

                foreach (var dersNotu in ogrenci.Notlar)
                {
                    Console.WriteLine(dersNotu.DersAdi.PadRight(15) + dersNotu.Not);
                }

                break;
            }

            ListeCikis();
        }
        public void OgrenciListele3(int no)
        {
            BaslikYazdir(7, "Öğrencinin okuduğu kitapları listele");
            while (true)
            {
                int sayi = _inputHelper.OgrenciNoKontrol();

                var ogrenci = HelperFunctions.OgrenciBulNo(Uygulama.okul, sayi);
                if (ogrenci == null)
                {
                    Console.WriteLine("Bu numarada bir öğrenci yok. Tekrar deneyin.");
                    continue;
                }

                OgrenciBilgiYazdir(ogrenci);

                if (ogrenci.Kitaplar.Count == 0)
                {
                    Console.WriteLine("\nÖğrencinin okuduğu herhangi bir kitap bulunmamaktadır.");
                    ListeCikis();
                    return;
                }

                Console.WriteLine("\nOkuduğu Kitaplar");
                Console.WriteLine("-------------------");

                foreach (var kitap in ogrenci.Kitaplar)
                {
                    Console.WriteLine(kitap);
                }
                break;

            }
            ListeCikis();
        }
        public void OgrenciBilgiYazdir(Ogrenci ogrenci)
        {

            _output.Write("\nÖğrencinin Adı Soyadı: " + ogrenci.Ad + " " + ogrenci.Soyad);
            _output.WriteLine("\nÖğrencinin Şubesi: " + ogrenci.Sube.ToString());
        }
    }
}
