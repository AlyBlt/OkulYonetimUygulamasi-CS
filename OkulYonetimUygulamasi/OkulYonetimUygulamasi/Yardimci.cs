using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulYonetimUygulamasi
{
    internal class Yardimci
    {
       
        public static void BaslikYazdir(int baslikno, string baslik)
        {
            Console.WriteLine("\n" + baslikno + "-" + baslik + "-----------------------------------------------------------------------\n");
        }

        public static Ogrenci OgrenciBulNo(Okul okul, int no)
        {
            return okul.Ogrenciler.FirstOrDefault(o => o.No == no);
        }
        
        public static void ListeCikis()
        {
            Console.WriteLine("\nMenüyü tekrar listelemek için \"liste\", çıkış yapmak için \"çıkış\" yazın.");
        }

        public static void OgrenciListele(List<Ogrenci> liste,int no, string baslik, bool baslikYazdir = true)
        {
            //BaslikYazdir(no, baslik);
            if (baslikYazdir)
                BaslikYazdir(no,baslik);
            Console.WriteLine("Şube".PadRight(15) + "No".PadRight(15) + "Adı".PadRight(5) + "Soyadı".PadRight(20) + "Not Ort.".PadRight(15) + "Okuduğu Kitap Say.");
            Console.WriteLine("----------------------------------------------------------------------------------------------");

            foreach (Ogrenci ogrenci in liste)
            {
                Console.WriteLine(ogrenci.Sube.ToString().PadRight(15) + ogrenci.No.ToString().PadRight(15) + (ogrenci.Ad + " " +
                    ogrenci.Soyad).PadRight(25) + ogrenci.Ortalama.ToString("0.00").PadRight(15) + ogrenci.KitapSayisi);
            }

            ListeCikis();
        }

        public static void OgrenciListele1(int no)
        {
            BaslikYazdir(5, "Illere Göre Ögrencileri Listele ");
            Console.WriteLine("Şube".PadRight(15) + "No".PadRight(15) + "Adı".PadRight(5) + "Soyadı".PadRight(20) + "Şehir".PadRight(15) + "Semt");
            Console.WriteLine("------------------------------------------------------------------------------------------------------");
            var liste = Uygulama.okul.Ogrenciler.Where(o => o.Adresi != null && !string.IsNullOrWhiteSpace(o.Adresi.Il))
            .OrderBy(o => o.Adresi.Il).ThenBy(o => o.No).ToList();
            foreach (Ogrenci ogrenci in liste)
            {
                Console.WriteLine(ogrenci.Sube.ToString().PadRight(15) + ogrenci.No.ToString().PadRight(15) + (ogrenci.Ad + " " +
                    ogrenci.Soyad).PadRight(25) + ogrenci.Adresi.Il.ToString().PadRight(15) + ogrenci.Adresi.Ilce);
            }

            ListeCikis();
        }

        public static void OgrenciListele2(int no)
        {
            BaslikYazdir(6, "Ögrencinin notlarını görüntüle");
            Console.Write("Öğrencinin numarası: ");
            string numara = Console.ReadLine();

            if (int.TryParse(numara, out int sayi))
            {
                var ogrenci = OgrenciBulNo(Uygulama.okul, sayi);
                if (ogrenci == null)
                {
                    Console.WriteLine("\nBu numarada bir öğrenci bulunmamaktadır.");
                    return;
                }

                Console.WriteLine("\nÖğrencinin Adı Soyadı: " + ogrenci.Ad + " " + ogrenci.Soyad);
                Console.WriteLine("Öğrencinin Şubesi: " + ogrenci.Sube.ToString());

                if (ogrenci.Notlar.Count == 0)
                {
                    Console.WriteLine(ogrenci.Ad + " " + ogrenci.Soyad + " öğrencisinin henüz girilmiş bir notu yoktur.");
                    return;
                }


                Console.WriteLine("\nDersin Adı".PadRight(15) + "Notu");
                Console.WriteLine("-------------------");

                var liste = Uygulama.okul.Ogrenciler.Where(o => o.No == no).ToList();
                foreach (var dersNotu in ogrenci.Notlar)
                {
                    Console.WriteLine(dersNotu.DersAdi.PadRight(15) + dersNotu.Not);
                }
            }
            else { Console.WriteLine("Hatali giris yapildi. Tekrar deneyin."); }
            
            ListeCikis();
        }

        public static void OgrenciListele3(int no)
        {
            BaslikYazdir(7, "Ögrencinin okudugu kitapları listele");
            Console.Write("Öğrencinin numarası: ");
            string numara = Console.ReadLine();

            if (int.TryParse(numara, out int sayi))
            {
                var ogrenci = OgrenciBulNo(Uygulama.okul, sayi);
                if (ogrenci == null)
                {
                    Console.WriteLine("\nBu numarada bir öğrenci bulunmamaktadır.");
                    return;
                }

                Console.WriteLine("\nÖğrencinin Adı Soyadı: " + ogrenci.Ad + " " + ogrenci.Soyad);
                Console.WriteLine("Öğrencinin Şubesi: " + ogrenci.Sube.ToString());

                if (ogrenci.Kitaplar.Count == 0)
                {
                    Console.WriteLine(ogrenci.Ad + " " + ogrenci.Soyad + " öğrencisinin okuduğu kayıtlı bir kitap yoktur.");
                    return;
                }

                Console.WriteLine("\nOkuduğu Kitaplar");
                Console.WriteLine("-------------------");

                var liste = Uygulama.okul.Ogrenciler.Where(o => o.No == no).ToList();

                foreach (var kitap in ogrenci.Kitaplar)
                {
                    Console.WriteLine(kitap);
                }
            }

            else { Console.WriteLine("Hatali giris yapildi. Tekrar deneyin."); }

                ListeCikis();

        }

        public static bool OgrenciVarMi(Okul okul)
        {
            return okul.Ogrenciler != null && okul.Ogrenciler.Any();
        }

        public static SUBE? SubeKontrol(string subeIstek)
        {
            //Sube kontrolde sube boş bırakıldığında hata vermesin istiyorum çünkü
            //yeni kayıt olmuş ve şubesi atanmayan öğrenci olabilir. Empty şubede de öğrenci listesi varsa listelensin.
                Console.Write(subeIstek);
                string subebilgisi = Console.ReadLine().ToUpper();
                Enum.TryParse<SUBE>(subebilgisi, out SUBE sube);

            // Sadece Enter'a basıldıysa (tamamen boşsa): Şubesiz öğrenciler
            if (subebilgisi == "")
                return null;

            // Sadece boşluk veya saçma karakterler girildiyse: hatalı giriş
                if (!Enum.TryParse<SUBE>(subebilgisi, out sube) || int.TryParse(subebilgisi, out _) || string.IsNullOrWhiteSpace(subebilgisi))
                {
                    Console.WriteLine("Hatali giris yapildi. Tekrar deneyin.");
                return SubeKontrol(subeIstek);
                }
                return sube;
            
        }

        public static int OgrenciNoKontrol()
        {
            while (true)
            {
                Console.Write("Öğrencinin numarası: ");
                string numara = Console.ReadLine();
                if (!int.TryParse(numara, out int sayi))
                {
                    Console.WriteLine("Hatali giris yapildi. Tekrar deneyin.");
                    continue;
                }
                
                return sayi;
            }
            
        }

        public static CINSIYET? CinsiyetKontrol(string cinsiyetIstek)
        {
            while (true)
            {
          
                Console.Write(cinsiyetIstek);
                string cinsiyetBilgisi = Console.ReadLine().ToUpper();
                if (cinsiyetBilgisi == "")
                    return null;

                cinsiyetBilgisi = cinsiyetBilgisi.Trim().ToUpper();
                if (cinsiyetBilgisi == "K")
                    return CINSIYET.Kiz;
                else if (cinsiyetBilgisi == "E")
                    return CINSIYET.Erkek;
                else
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    return CinsiyetKontrol(cinsiyetIstek); // Kullanıcıya tekrar sor
                }
           
            }
        }

        public static DateTime? DogumTarihiKontrol(string tarihIstek)
        {
            DateTime dogumTarihBilgisi;
          
                Console.Write(tarihIstek);
                string girilenTarih = Console.ReadLine();

            if (girilenTarih=="")
                return null;

            if (DateTime.TryParse(girilenTarih, out dogumTarihBilgisi))
                return dogumTarihBilgisi;

            Console.WriteLine("Geçersiz tarih formatı. Lütfen tekrar deneyin.");
            return DogumTarihiKontrol(tarihIstek);

        }

        public static string? OgrenciAdSoyadKontrol(string istek)
        {
            
                Console.Write(istek);
                string name= Console.ReadLine();
            if (name == "")
                return null;
            

            if (int.TryParse(name, out _) || string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                 return OgrenciAdSoyadKontrol(istek);
            }

              
            return name;
        }

        public static void OgrenciBilgiYazdir(Ogrenci ogrenci)
        {
            
            Console.WriteLine("\nÖğrencinin Adı Soyadı: " + ogrenci.Ad + " " + ogrenci.Soyad);
            Console.WriteLine("Öğrencinin Şubesi: " + ogrenci.Sube.ToString());
        }


    }
}
