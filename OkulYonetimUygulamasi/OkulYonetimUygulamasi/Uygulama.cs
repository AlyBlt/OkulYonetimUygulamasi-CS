using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OkulYonetimUygulamasi
{
    internal class Uygulama
    {
        public static Okul okul = new Okul();



        public void Menu()
        {
            Console.WriteLine("\n------  Okul Yönetim Uygulaması  ------\n");
            Console.WriteLine("1 - Bütün Öğrencileri Listele");
            Console.WriteLine("2 - Şubeye Göre Öğrencileri Listele");
            Console.WriteLine("3 - Cinsiyetine Göre Öğrencileri Listele");
            Console.WriteLine("4 - Şu Tarihten Sonra Doğan Öğrencileri Listele");
            Console.WriteLine("5 - İllere Göre Sıralayarak Öğrencileri Listele");
            Console.WriteLine("6 - Öğrencinin Tüm Notlarını Listele");
            Console.WriteLine("7 - Öğrencinin Okuduğu Kitapları Listele");
            Console.WriteLine("8 - Okuldaki En Yüksek Notlu 5 Öğrenciyi Listele");
            Console.WriteLine("9 - Okuldaki En Düşük Notlu 3 Öğrenciyi Listele");
            Console.WriteLine("10 - Şubedeki En Yüksek Notlu 5 Öğrenciyi Listele");
            Console.WriteLine("11 - Şubedeki En Düşük Notlu 3 Öğrenciyi Listele");
            Console.WriteLine("12 - Öğrencinin Not Ortalamasını Gör");
            Console.WriteLine("13 - Şubenin Not Ortalamasını Gör");
            Console.WriteLine("14 - Öğrencinin Okuduğu Son Kitabı Gör");
            Console.WriteLine("15 - Öğrenci Ekle");
            Console.WriteLine("16 - Öğrenci Güncelle");
            Console.WriteLine("17 - Öğrenci Sil");
            Console.WriteLine("18 - Öğrencinin Adresini Gir");
            Console.WriteLine("19 - Öğrencinin Okuduğu Kitabı Gir");
            Console.WriteLine("20 - Öğrencinin Notunu Gir");
            Console.WriteLine("\nÇıkış yapmak için \"çıkış\" yazıp \"enter\"a basın.");


        }
        public void Yonetim()
        {
            SahteOgrenciEkle();
            SahteAdresEkle();
            SahteKitapEkle();
            SahteNotEkle();
            Menu();
            int sayac = 1;
            while (true)
            {

                Console.Write("\nYapmak istediğiniz işlemi seçiniz: ");
                string secim = Console.ReadLine().ToUpper();

                switch (secim)
                {
                    case "1":
                        ButunOgrenciListele();
                        break;
                    case "2":
                        SubeyeGoreOgrenciListele();
                        break;
                    case "3":
                        CinsiyeteGoreOgrenciListele();
                        break;
                    case "4":
                        DogumTarihineGoreOgrenciListele();
                        break;
                    case "5":
                        IllereGoreOgrenciListele();
                        break;
                    case "6":
                        OgrencininTumNotListele();
                        break;
                    case "7":
                        OgrencininKitaplariListele();
                        break;
                    case "8":
                        OkuldaEnYuksekNotlu5Listele();
                        break;
                    case "9":
                        OkuldaEnDusukNotlu3Listele();
                        break;
                    case "10":
                        SubedeEnYuksekNotlu5Listele();
                        break;
                    case "11":
                        SubedeEnDusukNotlu3Listele();
                        break;
                    case "12":
                        OgrencininNotOrtalamasiGor();
                        break;
                    case "13":
                        SubeninNotOrtalamasiGor();
                        break;
                    case "14":
                        //OgrencininOkuduguSonKitapGor();
                        break;
                    case "15":
                       // OgrenciEkle();
                        break;
                    case "16":
                        //OgrenciGuncelle();
                        break;
                    case "17":
                        //OgrenciSil();
                        break;
                    case "18":
                        //OgrencininAdresiniGir();
                        break;
                    case "19":
                        //OgrencininOkuduguKitabiGir();
                        break;
                    case "20":
                        OgrenciNotuGir();
                        break;
                    case "ÇIKIŞ":
                    case "CİKİS":
                        Environment.Exit(0);
                        break;
                    case "LİSTE":
                    case "LISTE":
                        Console.Clear();
                        Menu();
                        break;
                    default:
                        Console.WriteLine("\nHatalı işlem gerçekleştirildi. Tekrar deneyin.");
                        Yardimci.ListeCikis();
                        sayac++;
                        break;
                }
                if (sayac > 10)
                {
                    Console.WriteLine("\nÜzgünüm sizi anlayamıyorum. Program sonlandırılıyor.\n");
                    Environment.Exit(0);
                }

            }
        }
       

        public void ButunOgrenciListele()
        {
            if (!Yardimci.OgrenciVarMi(okul))
            {
                Console.WriteLine("\nHenüz sisteme kayıtlı öğrenci yok.");
                return;
            }

            Yardimci.OgrenciListele(okul.Ogrenciler, 1, "Bütün Öğrencileri Listele");
        }

        public void SubeyeGoreOgrenciListele()
        {
            Yardimci.BaslikYazdir(2, "Şubeye Göre Öğrenci Listele");
            Console.Write("Listelemek istediğiniz şubeyi girin (A/B/C): ");
            string subebilgisi = Console.ReadLine().ToUpper();

            if (Enum.TryParse<SUBE>(subebilgisi, out SUBE sube))
            {
                var liste = Yardimci.OgrenciBulSube(okul, sube);

                if (liste.Count == 0)
                {
                    Console.WriteLine("Bu şubede öğrenci bulunamadı.");
                }
                else
                {
                    Console.WriteLine();
                    Yardimci.OgrenciListele(liste,2, "Şubeye Göre Öğrenci Listele", false);
                }
            }
            else
            {
                Console.WriteLine("Hatalı şube girdiniz.");
            }
        }

        public void CinsiyeteGoreOgrenciListele()
        {
            Yardimci.BaslikYazdir(3, "Cinsiyete Göre Öğrenciler");
            Console.Write("Listelemek istediğiniz cinsiyeti girin (K/E): ");
            string cinsiyetBilgisi = Console.ReadLine().ToUpper();
               
            while (cinsiyetBilgisi != "K" && cinsiyetBilgisi != "E")
            
            { 
                    Console.WriteLine("Hatalı giriş yaptınız. Lütfen sadece K veya E girin.");
                    cinsiyetBilgisi=Console.ReadLine().ToUpper();
                
            }

            CINSIYET cinsiyet = (cinsiyetBilgisi == "K") ? CINSIYET.Kiz : CINSIYET.Erkek;

            var liste = okul.Ogrenciler.Where(o => o.Cinsiyet == cinsiyet).ToList();

            if (liste.Count == 0)
            {
                Console.WriteLine($"\nSistemde hiç {(cinsiyet == CINSIYET.Kiz ? "Kız" : "Erkek")} öğrenci bulunmamaktadır.");
                return;
            }

            Yardimci.OgrenciListele(liste, 3, "Cinsiyete Göre Öğrenciler", false);
        }
        public void DogumTarihineGoreOgrenciListele()
        {
            Yardimci.BaslikYazdir(4, "Dogum Tarihine Göre Ögrencileri Listele");
            Console.Write("Hangi tarihten sonraki ögrencileri listelemek istersiniz (örn. 01.01.2000): ");
            string girilenTarih = Console.ReadLine();

            DateTime dogumTarihBilgisi;
            bool basariliMi = DateTime.TryParse(girilenTarih, out dogumTarihBilgisi);

            while (!basariliMi)
            {
                Console.WriteLine("Geçersiz tarih formatı. Lütfen tekrar deneyin (örn. 01.01.2000): ");
                girilenTarih = Console.ReadLine();
                basariliMi = DateTime.TryParse(girilenTarih, out dogumTarihBilgisi);
            }
            // Listeleme:
            var liste = okul.Ogrenciler.Where(o => o.DogumTarihi > dogumTarihBilgisi).ToList();

            if (liste.Count == 0)
            {
                Console.WriteLine($"\n{dogumTarihBilgisi:dd.MM.yyyy} tarihinden sonra doğmuş öğrenci bulunmamaktadır.");
                return;
            }

            Console.WriteLine();
            Yardimci.OgrenciListele(liste, 4, "Dogum Tarihine Göre Ögrencileri Listele", false);
        }

        public void IllereGoreOgrenciListele()
        {
       
            Yardimci.OgrenciListele1(5);
        }
        public void OgrencininTumNotListele()
        {
             Yardimci.OgrenciListele2(6);
        }
        public void OgrencininKitaplariListele()
        {
            Yardimci.OgrenciListele3(7);
        }
        public void OkuldaEnYuksekNotlu5Listele()
        {
            List<Ogrenci>liste=okul.Ogrenciler.OrderByDescending(a=>a.Ortalama).Take(5).ToList();
            Yardimci.OgrenciListele(liste, 8, "Okuldaki en başarılı 5 öğrenciyi listele");
        }
        public void OkuldaEnDusukNotlu3Listele()
        {
            List<Ogrenci> liste = okul.Ogrenciler.OrderBy(a => a.Ortalama).Take(3).ToList();
            Yardimci.OgrenciListele(liste, 9, "Okuldaki en başarısız 3 öğrenciyi listele");
        }
        public void SubedeEnYuksekNotlu5Listele()
        {
            Yardimci.BaslikYazdir(10, "Şubedeki en başarılı 5 ögrenciyi listele");
            Console.Write("Listelemek istediğiniz şubeyi girin (A/B/C): ");
            string subebilgisi = Console.ReadLine().ToUpper();
            if (Enum.TryParse<SUBE>(subebilgisi, out SUBE sube))
            {
                var liste = Yardimci.OgrenciBulSube(okul, sube);
                var subeliste=liste.OrderByDescending(a=>a.Ortalama).Take(5).ToList();

                if (subeliste.Count == 0)
                {
                    Console.WriteLine("Bu şubede öğrenci bulunamadı.");
                }
               
                else
                {
                    Console.WriteLine();
                    Yardimci.OgrenciListele(subeliste, 10, "Şubedeki en başarılı 5 ögrenciyi listele", false);
                }
            }
            else
            {
                Console.WriteLine("Hatalı şube girdiniz.");
            }
        }
        public void SubedeEnDusukNotlu3Listele()
        {
            Yardimci.BaslikYazdir(11, "Şubedeki en başarısız 3 öğrenciyi listele");
            Console.Write("Listelemek istediğiniz şubeyi girin (A/B/C): ");
            string subebilgisi = Console.ReadLine().ToUpper();
            if (Enum.TryParse<SUBE>(subebilgisi, out SUBE sube))
            {
                var liste = Yardimci.OgrenciBulSube(okul, sube);
                var subeliste = liste.OrderBy(a => a.Ortalama).Take(3).ToList();

                if (subeliste.Count == 0)
                {
                    Console.WriteLine("Bu şubede öğrenci bulunamadı.");
                }

                else
                {
                    Console.WriteLine();
                    Yardimci.OgrenciListele(subeliste, 11, "Şubedeki en başarısız 3 öğrenciyi listele", false);
                }
            }
            else
            {
                Console.WriteLine("Hatalı şube girdiniz.");
            }
        }
        public void OgrencininNotOrtalamasiGor()
        {
            Yardimci.BaslikYazdir(12, "Ögrencinin Not Ortalamasını Gör");
            Console.Write("Öğrencinin numarası: ");
            string numara = Console.ReadLine();

            if (int.TryParse(numara, out int sayi))
            {
                var ogrenci = Yardimci.OgrenciBulNo(okul, sayi);
                if (ogrenci == null)
                {
                    Console.WriteLine("\nBu numarada bir öğrenci bulunmamaktadır.");
                    return;
                }

                Console.WriteLine("\nÖğrencinin Adı Soyadı: " + ogrenci.Ad + " " + ogrenci.Soyad);
                Console.WriteLine("Öğrencinin Şubesi: " + ogrenci.Sube.ToString());

                Console.WriteLine("\nÖgrencinin not ortalaması:" + ogrenci.Ortalama);
                Yardimci.ListeCikis();
            }
            else
            {
                Console.WriteLine("Hatali giris yapildi. Tekrar deneyin.");
            }
         
        }
        public void SubeninNotOrtalamasiGor()
        {
            Yardimci.BaslikYazdir(13, "Şubenin Not Ortalamasını Gör");
            Console.Write("Listelemek istediğiniz şubeyi girin (A/B/C): ");
            string subebilgisi = Console.ReadLine().ToUpper();
            if (Enum.TryParse<SUBE>(subebilgisi, out SUBE sube))
            {
                var liste = Yardimci.OgrenciBulSube(okul, sube);
                double ortalama=liste.Average(a => a.Ortalama);

                if (liste.Count == 0)
                {
                    Console.WriteLine("Bu şubede öğrenci bulunamadı.");
                }

                else
                {
                    Console.WriteLine();
                    Console.WriteLine(subebilgisi + " şubesinin not ortalaması: " + ortalama);
                }
            }
            else
            {
                Console.WriteLine("Hatalı şube girdiniz.");
            }
        
        }
        //public void OgrencininOkuduguSonKitapGor()
        //{ }
        //public void OgrenciEkle()
        //{ }
        //public void OgrenciGuncelle()
        //{ }
        //public void OgrenciSil()
        //{ }
        //public void OgrencininAdresiniGir()
        //{ }

        //public void OgrencininOkuduguKitabiGir()
        //{ }


        public void OgrenciNotuGir()
        {

            try
            {
                Yardimci.BaslikYazdir(20,"Not Gir");

                Console.Write("Öğrenci numarası: ");
                int no = int.Parse(Console.ReadLine());

                var ogrenci = Yardimci.OgrenciBulNo(okul, no);

                if (ogrenci == null)
                {
                    Console.WriteLine("Bu numaraya ait bir öğrenci bulunamadı.");
                    return;
                }

                else
                {

                    Console.WriteLine("Öğrencinin Adı Soyadı: " + ogrenci.Ad + " " + ogrenci.Soyad);
                    Console.WriteLine("Öğrencinin Şubesi: " + ogrenci.Sube);
                }
                               
                Console.Write("Not eklemek istediğiniz ders: ");
                string ders = Console.ReadLine();
                Console.Write("Eklemek istediginiz not adedi: ");
                int adet = int.Parse(Console.ReadLine());

                for (int i = 1; i <= adet; i++)
                {
                    int not;
                    while (true)
                    {
                        Console.Write(i + ". notu girin (0-100): ");
                        if (int.TryParse(Console.ReadLine(), out not) && not >= 0 && not <= 100)
                            break;

                        Console.WriteLine("Geçersiz değer! Lütfen 0 ile 100 arasında bir sayı girin.");
                    }
                 

                    okul.NotEkle(no, ders, not);
                }
                Console.WriteLine("\nNot(lar) başarıyla eklendi.");
                Yardimci.ListeCikis();
            }
            catch (Exception e)
            {
                Console.WriteLine("\nHata: " + e.Message);
            }

        }

        public void SahteOgrenciEkle()
        {
            okul.OgrenciEkle(1, "Elif", "Selçuk", new DateTime(2001, 5, 5), CINSIYET.Kiz, SUBE.A);
            okul.OgrenciEkle(2, "Betül", "Yılmaz", new DateTime(2000, 10, 2), CINSIYET.Kiz, SUBE.B);
            okul.OgrenciEkle(3, "Hakan", "Çelik", new DateTime(2001, 8, 12), CINSIYET.Erkek, SUBE.C);
            okul.OgrenciEkle(4, "Kerem", "Akay", new DateTime(2002, 6, 10), CINSIYET.Erkek, SUBE.A);
            okul.OgrenciEkle(5, "Hatice", "Çınar", new DateTime(2000, 6, 5), CINSIYET.Kiz, SUBE.B);
            okul.OgrenciEkle(6, "Selim", "İleri", new DateTime(2004, 7, 20), CINSIYET.Erkek, SUBE.B);
            okul.OgrenciEkle(7, "Selin", "Kamış", new DateTime(2002, 5, 20), CINSIYET.Kiz, SUBE.C);
            okul.OgrenciEkle(8, "Sinan", "Avcı", new DateTime(2003, 2, 15), CINSIYET.Erkek, SUBE.A);
            okul.OgrenciEkle(9, "Deniz", "Çoban", new DateTime(2000, 2, 2), CINSIYET.Erkek, SUBE.C);
            okul.OgrenciEkle(10, "Selda", "Kavak", new DateTime(1999, 9, 20), CINSIYET.Kiz, SUBE.B);

        }

        public void SahteKitapEkle()
        {
            okul.KitapEkle(1, "Ölü Ozanlar Derneği");
            okul.KitapEkle(1, "George Orwell- 1984");
            okul.KitapEkle(2, "Bülbülü Öldürmek");
            okul.KitapEkle(2, "Hayvan Çiftliği");
            okul.KitapEkle(3, "Büyük Umutlar");
            okul.KitapEkle(4, "Harry Potter ve Felsefe Taşı");
            okul.KitapEkle(5, "Uğultulu Tepeler");
            okul.KitapEkle(5, "Harry Potter Azkaban Tutsağı");
            okul.KitapEkle(6, "Bir Ses Böler Geceyi");
            okul.KitapEkle(7, "Masal Masal İçinde");
            okul.KitapEkle(7, "Sis ve Gece");
            okul.KitapEkle(8, "Agatha'nın Anahtarı");
            okul.KitapEkle(9, "Çavdar Tarlasında Çocuklar");
            okul.KitapEkle(10, "Kuşların Şarkısı");
                       

        }

        public void SahteAdresEkle()
        {
            okul.AdresEkle(1, "Ankara", "Çankaya", "Bağlıca");
            okul.AdresEkle(2, "Ankara", "Keçiören", "Osmangazi");
            okul.AdresEkle(3, "Ankara", "Çankaya", "Çukurambar");
            okul.AdresEkle(4, "İzmir", "Karşıyaka", "Alaybey");
            okul.AdresEkle(5, "İzmir", "Gaziemir", "Atıfbey");
            okul.AdresEkle(6, "İzmir", "Gaziemir", "Irmak");
            okul.AdresEkle(7, "İzmir", "Bayraklı", "Adalet");
            okul.AdresEkle(8, "İstanbul", "Arnavutköy", "Anadolu");
            okul.AdresEkle(9, "İstanbul", "Beykoy", "Acarlar");
            okul.AdresEkle(10, "İstanbul", "Ataşehir", "Atatürk");

        }

        public void SahteNotEkle()
        {
            okul.NotEkle(1, "Türkçe", 22); okul.NotEkle(1, "Matematik", 23); okul.NotEkle(1, "Fen", 66); okul.NotEkle(1, "Sosyal", 40);
            okul.NotEkle(2, "Türkçe", 87); okul.NotEkle(2, "Matematik", 14); okul.NotEkle(2, "Fen", 37); okul.NotEkle(2, "Sosyal", 93);
            okul.NotEkle(3, "Türkçe", 67); okul.NotEkle(3, "Matematik", 39); okul.NotEkle(3, "Fen", 11); okul.NotEkle(3, "Sosyal", 47);
            okul.NotEkle(4, "Türkçe", 55); okul.NotEkle(4, "Matematik", 23); okul.NotEkle(4, "Fen", 94); okul.NotEkle(4, "Sosyal", 21);
            okul.NotEkle(5, "Türkçe", 85); okul.NotEkle(5, "Matematik", 87); okul.NotEkle(5, "Fen", 12); okul.NotEkle(5, "Sosyal", 73);
            okul.NotEkle(6, "Türkçe", 67); okul.NotEkle(6, "Matematik", 87); okul.NotEkle(6, "Fen", 81); okul.NotEkle(6, "Sosyal", 22);
            okul.NotEkle(7, "Türkçe", 2);  okul.NotEkle(7, "Matematik", 54); okul.NotEkle(7, "Fen", 69); okul.NotEkle(7, "Sosyal", 84);
            okul.NotEkle(8, "Türkçe", 63); okul.NotEkle(8, "Matematik", 38); okul.NotEkle(8, "Fen", 53); okul.NotEkle(8, "Sosyal", 41);
            okul.NotEkle(9, "Türkçe", 13); okul.NotEkle(9, "Matematik", 72); okul.NotEkle(9, "Fen", 44); okul.NotEkle(9, "Sosyal", 22);
            okul.NotEkle(10, "Türkçe", 5); okul.NotEkle(10, "Matematik",35); okul.NotEkle(10,"Fen", 92); okul.NotEkle(10, "Sosyal", 61);
        }



    }
}
