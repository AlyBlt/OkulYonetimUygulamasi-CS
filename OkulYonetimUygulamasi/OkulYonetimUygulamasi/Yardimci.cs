using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public static int BosOgrenciNumarasiGetir(List<Ogrenci> ogrenciler)
        {
            var mevcutNumaralar = ogrenciler.Select(o => o.No).OrderBy(n => n).ToList();
            for (int i = 1; i <= mevcutNumaralar.Count; i++)
            {
                if (mevcutNumaralar[i - 1] != i)
                    return i;
            }
            return mevcutNumaralar.Count + 1;
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
            while (true)
            {
                int sayi=OgrenciNoKontrol();
               
                    var ogrenci = OgrenciBulNo(Uygulama.okul, sayi);
                    if (ogrenci == null)
                    {
                        Console.WriteLine("Bu numarada bir ögrenci yok.Tekrar deneyin.");
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
        public static void OgrenciListele3(int no)
        {
            BaslikYazdir(7, "Ögrencinin okudugu kitapları listele");
            while (true)
            {
                int sayi = OgrenciNoKontrol();

                    var ogrenci = OgrenciBulNo(Uygulama.okul, sayi);
                    if (ogrenci == null)
                    {
                        Console.WriteLine("Bu numarada bir ögrenci yok.Tekrar deneyin.");
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
        public static bool OgrenciVarMi(Okul okul)
        {
            return okul.Ogrenciler != null && okul.Ogrenciler.Any();
        }
        public static SUBE? SubeKontrol(string subeIstek)
        {
            //Sube kontrolde sube boş bırakıldığında hata vermesin istiyorum çünkü
            //yeni kayıt olmuş ve şubesi atanmayan öğrenci olabilir. Empty şubede de öğrenci listesi varsa listelensin.
            while (true)
            {
                Console.Write(subeIstek);
                string subebilgisi = Console.ReadLine()?.ToUpper();

                // Sadece Enter'a basıldıysa (tamamen boşsa): Şubesiz öğrenciler
                if (string.IsNullOrEmpty(subebilgisi))
                    return null;

                if (int.TryParse(subebilgisi, out _) || string.IsNullOrWhiteSpace(subebilgisi))
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    continue;
                }

                // Sadece boşluk veya saçma karakterler girildiyse: hatalı giriş
                if (Enum.TryParse(subebilgisi, out SUBE sube) && Enum.IsDefined(typeof(SUBE), sube))
                {
                    return sube;
                }
                    Console.WriteLine("Hatali giris yapildi. Tekrar deneyin.");
                continue;
            }
            
        }
        public static int OgrenciNoKontrol()
        {
            while (true)
            {
                Console.Write("Öğrencinin numarası: ");
                string? numara = Console.ReadLine();
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
                if (string.IsNullOrEmpty(cinsiyetBilgisi))
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

            if (string.IsNullOrEmpty(girilenTarih))
                return null;

            if (DateTime.TryParse(girilenTarih, out dogumTarihBilgisi))
                return dogumTarihBilgisi;

            Console.WriteLine("Hatali giris yapildi. Tekrar deneyin.");
            return DogumTarihiKontrol(tarihIstek);

        }
        public static string? OgrenciAdSoyadKontrol(string istek)
        {
                Console.Write(istek);
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    return null; 

            // Sadece harfler (Türkçe dahil) ve boşluklara izin veren regex
            if (Regex.IsMatch(input, @"^(?=.*[a-zA-ZçÇğĞıİöÖşŞüÜ])[a-zA-ZçÇğĞıİöÖşŞüÜ\s]+$"))
                return IlkHarfBuyut(input);
            else
            {
                Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                OgrenciAdSoyadKontrol(istek);
            }
            return input;
        }

        public static void OgrenciBilgiYazdir(Ogrenci ogrenci)
        {
            
            Console.WriteLine("\nÖğrencinin Adı Soyadı: " + ogrenci.Ad + " " + ogrenci.Soyad);
            Console.WriteLine("Öğrencinin Şubesi: " + ogrenci.Sube.ToString());
        }

        public static string IlkHarfBuyut(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var kelimeler = input.Trim().ToLower().Split(' ');
            for (int i = 0; i < kelimeler.Length; i++)
            {
                if (kelimeler[i].Length > 0)
                {
                    kelimeler[i] = char.ToUpper(kelimeler[i][0]) + kelimeler[i].Substring(1);
                }
            }

            return string.Join(" ", kelimeler);
        }

    }
}
