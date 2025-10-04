using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OkulYonetimUygulamasi
{
    internal class Uygulama
    {
        public static Okul okul = new Okul();



        public void Menu()
        {
            Console.WriteLine("\n------  Okul Yönetim Uygulaması  ------\n");
            Console.WriteLine($"Tarih: {DateTime.Now.ToShortDateString()}  Saat: {DateTime.Now.ToLongTimeString()}\n");
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
                        OgrencininOkuduguSonKitapGor();
                        break;
                    case "15":
                       OgrenciEkle();
                        break;
                    case "16":
                        OgrenciGuncelle();
                        break;
                    case "17":
                        OgrenciSil();
                        break;
                    case "18":
                        OgrencininAdresiniGir();
                        break;
                    case "19":
                        OgrencininOkuduguKitabiGir();
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
                        //Console.Clear();
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
                Yardimci.ListeCikis();
                return;
            }

            //asağıdaki listeyi silinen ve yerine eklenen öğrenci olursa numaraları sırasıyla göstersin diye ekledim.
            //mesela 5 silindi, daha sonra eklenen öğrenci 5 numaraya atandı ama 5 en sonda görünmesin listede.
            var siraliOgrenciler = okul.Ogrenciler.OrderBy(o => o.No).ToList();

            Yardimci.OgrenciListele(siraliOgrenciler, 1, "Bütün Öğrencileri Listele");
        }

        public void SubeyeGoreOgrenciListele()
        {
            Yardimci.BaslikYazdir(2, "Şubeye Göre Öğrenci Listele");
            Console.WriteLine("Şubesi atanmamış öğrenciler için \"Enter\"a basabilirsiniz.");
            SUBE? sube=Yardimci.SubeKontrol("Listelemek istediğiniz şubeyi girin (A/B/C): ");
            
            List<Ogrenci> liste;
            if (sube == null)
            {
                // Şubesi atanmamış öğrencileri filtrele
                liste = okul.Ogrenciler.Where(o => !Enum.IsDefined(typeof(SUBE), o.Sube)).ToList();
            }
            else
            {
                // Girilen şubedeki öğrencileri filtrele
                liste = okul.Ogrenciler.Where(o => o.Sube == sube).ToList();
            }

            if (liste == null || liste.Count == 0)
            {
                Console.WriteLine("\nListelenecek ögrenci yok.");
                Yardimci.ListeCikis();
                return;
            }

            Console.WriteLine();
            Yardimci.OgrenciListele(liste, 2, "Şubeye Göre Öğrenci Listele", false);

        }

        public void CinsiyeteGoreOgrenciListele()
        {
            Yardimci.BaslikYazdir(3, "Cinsiyete Göre Öğrenciler");
            Console.WriteLine("Cinsiyet bilgisi girilmemiş öğrenciler için \"Enter\"a basabilirsiniz.");
            CINSIYET? cinsiyet=Yardimci.CinsiyetKontrol("Listelemek istediğiniz cinsiyeti girin (K/E): ");
            List<Ogrenci> liste;
            if (cinsiyet == null)
            {
                // Cinsiyeti belirtilmemiş öğrencileri listele
                liste = okul.Ogrenciler.Where(o => o.Cinsiyet == null || o.Cinsiyet == CINSIYET.Empty).ToList();
            }
            else
            {
                liste = okul.Ogrenciler.Where(o => o.Cinsiyet == cinsiyet).ToList();
            }
            if (liste.Count == 0 || liste==null)
            {
                                          
                Console.WriteLine("\nListelenecek ögrenci yok.");
                Yardimci.ListeCikis();
                return;
            }
            Console.WriteLine();
            Yardimci.OgrenciListele(liste, 3, "Cinsiyete Göre Öğrenciler", false);
        }
        public void DogumTarihineGoreOgrenciListele()
        {
            Yardimci.BaslikYazdir(4, "Dogum Tarihine Göre Ögrencileri Listele");
            DateTime? dogumTarihBilgisi;
            while (true)
            {
                dogumTarihBilgisi = Yardimci.DogumTarihiKontrol("Hangi tarihten sonraki ögrencileri listelemek istersiniz (örn. 01.01.2000): ");
               
                if (dogumTarihBilgisi == null)
                {
                    Console.WriteLine("Veri girişi yapılmadı. Tekrar deneyin");
                    continue;
                }
                break;
            }
            var liste = okul.Ogrenciler.Where(o => o.DogumTarihi > dogumTarihBilgisi).ToList();
            if (liste.Count == 0)
            {
                Console.WriteLine("Listelenecek ögrenci yok.");
                Yardimci.ListeCikis();
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
            Console.WriteLine("Şubesi atanmamış öğrenciler için \"Enter\"a basabilirsiniz.");
            SUBE? sube = Yardimci.SubeKontrol("Listelemek istediğiniz şubeyi girin (A/B/C): ");
            List<Ogrenci> liste;

            if (sube == null)
            {
                // Şubesi atanmamış öğrenciler
                liste = okul.Ogrenciler.Where(o => !Enum.IsDefined(typeof(SUBE), o.Sube)).ToList();
            }
            else
            {
                // Belirli şubedeki öğrenciler
                liste = okul.Ogrenciler.Where(o => o.Sube == sube).ToList();
            }
            var subeliste=liste.OrderByDescending(a=>a.Ortalama).Take(5).ToList();

                if (subeliste.Count == 0)
                {
                Console.WriteLine("\nListelenecek ögrenci yok.");
                Yardimci.ListeCikis();
                return;
                }
               
                    Console.WriteLine();
                    Yardimci.OgrenciListele(subeliste, 10, "Şubedeki en başarılı 5 ögrenciyi listele", false);
               
        }
        public void SubedeEnDusukNotlu3Listele()
        {
            Yardimci.BaslikYazdir(11, "Şubedeki en başarısız 3 öğrenciyi listele");
            Console.WriteLine("Şubesi atanmamış öğrenciler için \"Enter\"a basabilirsiniz.");
            SUBE? sube = Yardimci.SubeKontrol("Listelemek istediğiniz şubeyi girin (A/B/C): ");
            List<Ogrenci> liste;

            if (sube == null)
            {
                // Şubesi atanmamış öğrenciler
                liste = okul.Ogrenciler.Where(o => !Enum.IsDefined(typeof(SUBE), o.Sube)).ToList();
            }
            else
            {
                // Belirli şubedeki öğrenciler
                liste = okul.Ogrenciler.Where(o => o.Sube == sube).ToList();
            }
            var subeliste = liste.OrderBy(a => a.Ortalama).Take(3).ToList();

            if (subeliste.Count == 0)
            {
                Console.WriteLine("\nListelenecek ögrenci yok.");
                Yardimci.ListeCikis();
                return;
            }

            Console.WriteLine();
            Yardimci.OgrenciListele(subeliste, 11, "Şubedeki en başarısız 3 öğrenciyi listele", false);
        }
        public void OgrencininNotOrtalamasiGor()
        {
            Yardimci.BaslikYazdir(12, "Ögrencinin Not Ortalamasını Gör");
            Ogrenci ogrenci = null;
            while (true)
            {
                int numara=Yardimci.OgrenciNoKontrol();
                ogrenci = Yardimci.OgrenciBulNo(okul, numara);
            
                if (ogrenci == null)
                {
                    Console.WriteLine("Bu numarada bir ögrenci yok.Tekrar deneyin.");
                    continue;
                }
                break;
            }

                Yardimci.OgrenciBilgiYazdir(ogrenci);

                Console.WriteLine("\nÖgrencinin not ortalaması:" + ogrenci.Ortalama);
                Yardimci.ListeCikis();
           
         
        }
        public void SubeninNotOrtalamasiGor()
        {
               Yardimci.BaslikYazdir(13, "Şubenin Not Ortalamasını Gör");
            Console.WriteLine("Şubesi atanmamış öğrenciler için \"Enter\"a basabilirsiniz.");
            SUBE? sube= Yardimci.SubeKontrol("Bir şube seçin (A/B/C): ");
            List<Ogrenci> liste;
            if (sube ==null)
            {
                // Şubesi atanmamış öğrenciler
                liste = okul.Ogrenciler.Where(o => o.Sube==SUBE.Empty).ToList();
                sube=SUBE.Empty;
            }
            else
            {
                // Belirli şubedeki öğrenciler
                liste = okul.Ogrenciler.Where(o => o.Sube == sube).ToList();
            }

                if (liste == null || liste.Count == 0)
                {
                    Console.WriteLine("\nBu şubede öğrenci bulunamadı.");
                Yardimci.ListeCikis();
                return;
                }

            double ortalama = liste.Average(o => o.Ortalama);
            Console.WriteLine();
            Console.WriteLine(sube + " şubesinin not ortalaması: " + ortalama);
            Yardimci.ListeCikis();
        }
        public void OgrencininOkuduguSonKitapGor()
        {
            Yardimci.BaslikYazdir(14, "Ögrencinin okudugu son kitabı listele");
            Ogrenci ogrenci;
           
            while (true)
            {
                int numara = Yardimci.OgrenciNoKontrol();
                ogrenci = Yardimci.OgrenciBulNo(okul, numara);

                if (ogrenci == null)
                {
                    Console.WriteLine("Bu numarada bir ögrenci yok. Tekrar deneyin.");
                    continue;
                }
                break;
            }

                Yardimci.OgrenciBilgiYazdir(ogrenci);
                if (ogrenci.Kitaplar == null || ogrenci.Kitaplar.Count == 0)
                {
                    Console.WriteLine("Bu öğrencinin henüz okuduğu bir kitap kaydı yok.");
                }
                else
                {
                    Console.WriteLine("\nÖğrencinin Okuduğu Kitaplar");
                    Console.WriteLine("------------------------------");
                    Console.WriteLine(ogrenci.Kitaplar.Last());
                    Yardimci.ListeCikis();
                }
       
        }
        public void OgrenciEkle()
        {
            Yardimci.BaslikYazdir(15, "Öğrenci Ekle");
            string? ad;
            string? soyad;
            DateTime? dogumTarihi;
            CINSIYET? cinsiyet;
            SUBE? sube;

            //int numara = Yardimci.OgrenciNoKontrol();
            //Ogrenci ogrenci = Yardimci.OgrenciBulNo(okul, numara);

            //Öğrenci numarasını sormasın otomatik atasın istiyorum.Çünkü burada mesela 5 numaralı öğrenci var,
            //numaraya 5 yazınca hata vermiyor ama en sonda otomatik max numara + 1'e atıyor. Kullanıcının kafası karışabilir.
            //Ya baştan uyarı versin ya da hiç sormasın/bence sormasa daha işlevsel.
            //Ayrıca en sona atayınca arada silinenler boş kalıyor. Ben eklenen öğrenciyi boş numara varsa 
            //en küçük boş numaraya atasın istiyorum.

            int numara = Yardimci.BosOgrenciNumarasiGetir(okul.Ogrenciler);

            while (true)
            {
                ad = Yardimci.OgrenciAdSoyadKontrol("Öğrencinin adı: ");
                if (!string.IsNullOrWhiteSpace(ad))
                    break;
                Console.WriteLine("Veri girişi yapılmadı. Tekrar deneyin.");
            }
            while (true)
            {
                soyad = Yardimci.OgrenciAdSoyadKontrol("Öğrencinin soyadı: ");
                if (!string.IsNullOrWhiteSpace(soyad))
                    break;
                Console.WriteLine("Veri girişi yapılmadı. Tekrar deneyin.");
            }
            while (true)
            {
                dogumTarihi = Yardimci.DogumTarihiKontrol("Öğrencinin doğum tarihi: ");
                if (dogumTarihi == null)
                {
                    Console.WriteLine("Veri girişi yapılmadı. Tekrar deneyin.");
                    continue;
                }
                break;

            }
            Console.WriteLine("\nCinsiyeti boş bırakmak için \"Enter\"a basabilirsiniz.");
            cinsiyet = Yardimci.CinsiyetKontrol("Öğrencinin cinsiyeti (K/E): ");
            if (cinsiyet == null) cinsiyet = CINSIYET.Empty;

            Console.WriteLine("\nŞubeyi boş bırakmak için \"Enter\"a basabilirsiniz.");
            sube = Yardimci.SubeKontrol("Öğrencinin şubesi (A/B/C): ");
            if (sube == null) sube = SUBE.Empty;

            
            Console.WriteLine("\nÖğrenci " + numara + " okul numarası ile sisteme basarılı bir sekilde eklenmistir.");
            okul.OgrenciEkle(numara, ad, soyad, dogumTarihi.Value, cinsiyet.Value, sube.Value);
            Yardimci.ListeCikis();
        }
        public void OgrenciGuncelle()
        {
            Yardimci.BaslikYazdir(16, "Öğrenci Güncelle");
            Ogrenci ogrenci;
            int numara;
            string? ad;
            string? soyad;
            DateTime? dogumTarihi;
            CINSIYET? cinsiyet;
            SUBE? sube;

            Console.WriteLine("Değiştirmek istemediğiniz bilgiler için \"Enter\"a basabilirsiniz.");
            Console.WriteLine("Bilgileri güncellenecek öğrencinin numarası zorunludur.");
           
            while (true)
            {
                numara = Yardimci.OgrenciNoKontrol();
                ogrenci = Yardimci.OgrenciBulNo(okul, numara);

                if (ogrenci == null)
                {
                    Console.WriteLine("Bu numarada bir ögrenci yok. Tekrar deneyin.");
                    continue;
                }
                break;
            }
           
            ad = Yardimci.OgrenciAdSoyadKontrol("Öğrencinin adı: ");
            if (ad==null)
                ad = ogrenci.Ad;
            soyad = Yardimci.OgrenciAdSoyadKontrol("Öğrencinin soyadı: ");
            if (soyad==null)
                soyad = ogrenci.Soyad;
            dogumTarihi = Yardimci.DogumTarihiKontrol("Öğrencinin doğum tarihi: ");
            if (dogumTarihi == null)
                dogumTarihi = ogrenci.DogumTarihi;

            cinsiyet = Yardimci.CinsiyetKontrol("Öğrencinin cinsiyeti (K/E): ");
            if (cinsiyet == null) cinsiyet = ogrenci.Cinsiyet;

            sube = Yardimci.SubeKontrol("Öğrencinin şubesi (A/B/C): ");
            if (sube == null)
                sube = ogrenci.Sube;

            okul.OgrenciGuncelle(numara, ad, soyad, dogumTarihi.Value, cinsiyet.Value, sube.Value);
            Console.WriteLine("\nÖğrenci güncellendi.");
            Yardimci.ListeCikis();
        }
        
        public void OgrenciSil()
        {
            Yardimci.BaslikYazdir(17, "Ögrenci sil");
            int numara;
            Ogrenci ogrenci;
            while (true)
            {
                numara = Yardimci.OgrenciNoKontrol();
                ogrenci = Yardimci.OgrenciBulNo(okul, numara);

                if (ogrenci == null)
                {
                    Console.WriteLine("Bu numarada bir ögrenci yok. Tekrar deneyin.");
                    continue;
                }
                break;
            }
            Yardimci.OgrenciBilgiYazdir(ogrenci); 
            Console.WriteLine("\nOnaylamak için \"E\" iptal etmek için \"H\" yazıp \"Enter\"a basın.");
            while (true)
            {
                Console.Write("Ögrenciyi silmek istediginize emin misiniz (E/H): ");
                string? giris = Console.ReadLine();
                if (giris == null)
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin."); continue;
                }
                string cevap = giris.ToUpper();
                if (int.TryParse(cevap, out _))
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    continue;
                }
                if (cevap == "")
                { Console.WriteLine("Veri girişi yapılmadı. Tekrar deneyin."); }

                if (cevap != "E" && cevap != "H" && cevap!="")
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    continue;
                }
                if (cevap=="H")
                {
                    Yardimci.ListeCikis();
                    return;
                }
                if (cevap == "E")
                {
                    Console.WriteLine("Ögrenci basarılı bir sekilde silindi.");
                    Yardimci.ListeCikis();
                    okul.OgrenciSil(numara);
                    return;
                }
            }
         
        }
        public void OgrencininAdresiniGir()
        {
            Yardimci.BaslikYazdir(18, "Ögrencinin Adresini Gir");
            Console.WriteLine("Adres bilgilerini dolduruken boş bırakmak için \"enter\"a basabilirsiniz.");
            int numara;
            string il="";
            string ilce="";
            string mahalle="";
            Ogrenci? ogrenci = null;
            while (true)
            {
                numara = Yardimci.OgrenciNoKontrol();
                ogrenci = Yardimci.OgrenciBulNo(okul, numara);

                if (ogrenci == null)
                {
                    Console.WriteLine("Bu numaraya ait bir öğrenci bulunamadı. Tekrar deneyin.");
                    continue;
                }
                break;
            }
            Yardimci.OgrenciBilgiYazdir(ogrenci);
            Console.WriteLine();
            while (true)
            {
                Console.Write("İl: ");
                string? cevapIl= Console.ReadLine();
                
                if (int.TryParse(cevapIl, out _) || cevapIl == " ")
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    continue;
                }
                if (cevapIl == null)
                { Console.WriteLine("Veri girişi yapılmadı."); break; }

                il=Yardimci.IlkHarfBuyut(cevapIl);
                break;
            }
            while (true)
            {
                Console.Write("İlçe: ");
                string? cevapIlce = Console.ReadLine();
                if (int.TryParse(cevapIlce, out int _) || cevapIlce==" ")
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    continue;
                }
                if (cevapIlce == null)
                { Console.WriteLine("Veri girişi yapılmadı."); break; }
                ilce = Yardimci.IlkHarfBuyut(cevapIlce);
                break;
            }
            while (true)
            {
                Console.Write("Mahalle: ");
                string? cevapMahalle = Console.ReadLine();
                if (int.TryParse(cevapMahalle, out int _) || cevapMahalle==" ")
                {
                    Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    continue;
                }
                if (cevapMahalle == null)
                    { Console.WriteLine("Veri girişi yapılmadı."); break; }
                mahalle = Yardimci.IlkHarfBuyut(cevapMahalle);
                break;
            }

            Console.WriteLine("\nBilgiler sisteme girilmistir.");
            okul.AdresEkle(numara, il, ilce, mahalle);
        }

        public void OgrencininOkuduguKitabiGir()
        {
            Yardimci.BaslikYazdir(19, "Ögrencinin okudugu kitabı gir");
            int numara;
            string kitapAdi;
            Ogrenci ogrenci;
            while (true)
            {
                numara = Yardimci.OgrenciNoKontrol();
                ogrenci = Yardimci.OgrenciBulNo(okul, numara);

                if (ogrenci == null)
                {
                    Console.WriteLine("Bu numaraya ait bir öğrenci bulunamadı. Tekrar deneyin.");
                    continue;
                }
                break;
            }
            Yardimci.OgrenciBilgiYazdir(ogrenci);
            
            while (true)
            {
                Console.Write("\nEklenecek Kitabin Adı: ");
                string? kitap=Console.ReadLine();
                if (string.IsNullOrWhiteSpace(kitap))
                {
                    Console.WriteLine("Kitap adı boş bırakılamaz. Tekrar deneyin.");
                    continue;
                }
                kitapAdi = Yardimci.IlkHarfBuyut(kitap);
                Console.WriteLine("Bilgiler sisteme girilmistir.");
                break;
            }
            okul.KitapEkle(numara, kitapAdi);

        }

        public void OgrenciNotuGir()
        {
            Yardimci.BaslikYazdir(20, "Not Gir");
            int numara;
            Ogrenci ogrenci;
            string? ders;
            int adet;
            try
            {
                while (true)
                {
                    numara = Yardimci.OgrenciNoKontrol();
                    ogrenci = Yardimci.OgrenciBulNo(okul, numara);

                    if (ogrenci == null)
                    {
                        Console.WriteLine("Bu numarada bir ögrenci yok. Tekrar deneyin.");
                        continue;
                    }
                    break;
                }
                Yardimci.OgrenciBilgiYazdir(ogrenci);
                Console.WriteLine();
                while (true)
                {
                    Console.Write("Not eklemek istediğiniz dersi giriniz: ");
                     ders = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(ders) || int.TryParse(ders, out _))
                    {
                        Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                        continue;
                    }
                    break;
                }
                while (true)
                {
                    Console.Write("Eklemek istediginiz not adedi: ");
                    string? giris = Console.ReadLine();
                    if (int.TryParse(giris, out _))
                    {
                        adet = int.Parse(giris);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin."); continue;
                    }
                }

                for (int i = 1; i <= adet; i++)
                {
                    int not;
                    while (true)
                    {
                        Console.Write(i + ". notu girin (0-100): ");
                        if (int.TryParse(Console.ReadLine(), out not) && not >= 0 && not <= 100)
                            break;

                        Console.WriteLine("Hatali giris yapildi. Tekrar deneyin");
                    }
                 

                    okul.NotEkle(numara, Yardimci.IlkHarfBuyut(ders), not);
                }
                Console.WriteLine("\nBilgiler sisteme girilmistir.");
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
