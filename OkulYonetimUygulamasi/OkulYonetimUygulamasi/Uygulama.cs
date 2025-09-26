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
                        //SubeyeGoreOgrenciListele();
                        break;
                    case "3":
                        //CinsiyeteGoreOgrenciListele();
                        break;
                    case "4":
                        //DogumTarihineGoreOgrenciListele();
                        break;
                    case "5":
                        //IllereGoreOgrenciListele();
                        break;
                    case "6":
                        //OgrencininTumNotListele();
                        break;
                    case "7":
                        //OgrencininKitaplariListele();
                        break;
                    case "8":
                        //OkuldaEnYuksekNotlu5Listele();
                        break;
                    case "9":
                        //OkuldaEnDusukNotlu3Listele();
                        break;
                    case "10":
                        //SubedeEnYuksekNotlu5Listele();
                        break;
                    case "11":
                        //SubedeEnDusukNotlu3Listele();
                        break;
                    case "12":
                        //OgrencininNotOrtalamasiGor();
                        break;
                    case "13":
                        //SubeninNotOrtalamasiGor();
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
                        //OgrenciNotuGir();
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
                        Console.WriteLine("\nMenüyü tekrar listelemek için \"liste\", çıkış yapmak için \"çıkış\" yazın.");
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
       

        public void BaslikYazdir(string baslik)
        {
            Console.WriteLine("\n1-" + baslik + "---------------------------------------------------------------------------------\n");
        }

        public void ButunOgrenciListele()
        {
            BaslikYazdir("Bütün Öğrencileri Listele");

            Console.WriteLine("Şube".PadRight(15) + "No".PadRight(15) + "Adı".PadRight(5) + "Soyadı".PadRight(20) + "Not Ort.".PadRight(15) + "Okuduğu Kitap Say.");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------");

            foreach (Ogrenci ogrenci in okul.Ogrenciler )
            {
                Console.WriteLine(ogrenci.Sube.ToString().PadRight(15) + ogrenci.No.ToString().PadRight(15) + (ogrenci.Ad + " " + 
                    ogrenci.Soyad).PadRight(25) + ogrenci.Ortalama.ToString("0.00").PadRight(15) + ogrenci.KitapSayisi); 
            }



        }
        //public void SubeyeGoreOgrenciListele()
        //{ }
        //public void CinsiyeteGoreOgrenciListele()
        //{ }
        //public void DogumTarihineGoreOgrenciListele()
        //{ }
        //public void IllereGoreOgrenciListele()
        //{ }
        //public void OgrencininTumNotListele()
        //{ }
        //public void OgrencininKitaplariListele()
        //{ }
        //public void OkuldaEnYuksekNotlu5Listele()
        //{ }
        //public void OkuldaEnDusukNotlu3Listele()
        //{ }
        //public void SubedeEnYuksekNotlu5Listele()
        //{ }
        //public void SubedeEnDusukNotlu3Listele()
        //{ }
        //public void OgrencininNotOrtalamasiGor()
        //{ }
        //public void SubeninNotOrtalamasiGor()
        //{ }
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
                BaslikYazdir("Not Gir");

                Console.Write("Öğrenci numarası : ");
                int no = int.Parse(Console.ReadLine());

                // bu numaralı öğrenci bulunup bilgileri ekrana yazılacak.

                //
                string ders = Console.ReadLine();

                // 
                int adet = int.Parse(Console.ReadLine());

                for (int i = 1; i <= adet; i++)
                {
                    Console.Write(i + ". notu girin: ");
                    int not = int.Parse(Console.ReadLine());
                    //Okul.NotEkle(no, ders, not);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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

        }



    }
}
