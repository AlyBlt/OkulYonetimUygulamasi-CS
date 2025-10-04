using OkulYonetimUygulamasi.Helpers;
using OkulYonetimUygulamasi.Interfaces;
using OkulYonetimUygulamasi.Providers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OkulYonetimUygulamasi.Models
{
    public class Uygulama
    {
        public static Okul okul = new Okul();

        private readonly InputHelper _inputHelper;
        private readonly OutputHelper _outputHelper;

        public Uygulama(IInputProvider inputProvider, IOutputProvider outputProvider)
        {
            _inputHelper = new InputHelper(inputProvider, outputProvider);
            _outputHelper = new OutputHelper(outputProvider, inputProvider, _inputHelper);
        }

        public void Menu()
        {
            _outputHelper.WriteLine("\n------  Okul Yönetim Uygulaması  ------\n");
            _outputHelper.WriteLine($"Tarih: {DateTime.Now.ToShortDateString()}  Saat: {DateTime.Now.ToLongTimeString()}\n");
            _outputHelper.WriteLine("1 - Bütün Öğrencileri Listele");
            _outputHelper.WriteLine("2 - Şubeye Göre Öğrencileri Listele");
            _outputHelper.WriteLine("3 - Cinsiyetine Göre Öğrencileri Listele");
            _outputHelper.WriteLine("4 - Şu Tarihten Sonra Doğan Öğrencileri Listele");
            _outputHelper.WriteLine("5 - İllere Göre Sıralayarak Öğrencileri Listele");
            _outputHelper.WriteLine("6 - Öğrencinin Tüm Notlarını Listele");
            _outputHelper.WriteLine("7 - Öğrencinin Okuduğu Kitapları Listele");
            _outputHelper.WriteLine("8 - Okuldaki En Yüksek Notlu 5 Öğrenciyi Listele");
            _outputHelper.WriteLine("9 - Okuldaki En Düşük Notlu 3 Öğrenciyi Listele");
            _outputHelper.WriteLine("10 - Şubedeki En Yüksek Notlu 5 Öğrenciyi Listele");
            _outputHelper.WriteLine("11 - Şubedeki En Düşük Notlu 3 Öğrenciyi Listele");
            _outputHelper.WriteLine("12 - Öğrencinin Not Ortalamasını Gör");
            _outputHelper.WriteLine("13 - Şubenin Not Ortalamasını Gör");
            _outputHelper.WriteLine("14 - Öğrencinin Okuduğu Son Kitabı Gör");
            _outputHelper.WriteLine("15 - Öğrenci Ekle");
            _outputHelper.WriteLine("16 - Öğrenci Güncelle");
            _outputHelper.WriteLine("17 - Öğrenci Sil");
            _outputHelper.WriteLine("18 - Öğrencinin Adresini Gir");
            _outputHelper.WriteLine("19 - Öğrencinin Okuduğu Kitabı Gir");
            _outputHelper.WriteLine("20 - Öğrencinin Notunu Gir");
            _outputHelper.WriteLine("\nÇıkış yapmak için \"çıkış\" yazıp \"enter\"a basın.");
            
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
                //var inputProvider = new ConsoleInputProvider();
                //string secim = inputProvider.GetInput("\nYapmak istediğiniz işlemi seçiniz: ").ToUpper();
                string secim = _inputHelper.GetInput("\nYapmak istediğiniz işlemi seçiniz: ").ToUpper();

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
                        _outputHelper.WriteLine("\nHatalı işlem gerçekleştirildi. Tekrar deneyin.");
                        _outputHelper.ListeCikis();
                        sayac++;
                        break;
                }
                if (sayac > 10)
                {
                    _outputHelper.WriteLine("\nÜzgünüm sizi anlayamıyorum. Program sonlandırılıyor.\n");
                    Environment.Exit(0);
                }

            }
        }
       

        public void ButunOgrenciListele()
        {
            if (!HelperFunctions.OgrenciVarMi(okul))
            {
                _outputHelper.WriteLine("\nHenüz sisteme kayıtlı öğrenci yok.");
                _outputHelper.ListeCikis();
                return;
            }

            //asağıdaki listeyi silinen ve yerine eklenen öğrenci olursa numaraları sırasıyla göstersin diye ekledim.
            //mesela 5 silindi, daha sonra eklenen öğrenci 5 numaraya atandı ama 5 en sonda görünmesin listede.
            var siraliOgrenciler = okul.Ogrenciler.OrderBy(o => o.No).ToList();

            _outputHelper.OgrenciListele(siraliOgrenciler, 1, "Bütün Öğrencileri Listele");
           
        }

        public void SubeyeGoreOgrenciListele()
        {
            _outputHelper.BaslikYazdir(2, "Şubeye Göre Öğrenci Listele");
            _outputHelper.WriteLine("Şubesi atanmamış öğrenciler için \"Enter\"a basabilirsiniz.");
            SUBE? sube=_inputHelper.SubeKontrol("Listelemek istediğiniz şubeyi girin (A/B/C): ");
            
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
                _outputHelper.WriteLine("\nListelenecek ögrenci yok.");
                _outputHelper.ListeCikis();
                return;
            }

            Console.WriteLine();
            _outputHelper.OgrenciListele(liste, 2, "Şubeye Göre Öğrenci Listele", false);

        }

        public void CinsiyeteGoreOgrenciListele()
        {
            _outputHelper.BaslikYazdir(3, "Cinsiyete Göre Öğrenciler");
            _outputHelper.WriteLine("Cinsiyet bilgisi girilmemiş öğrenciler için \"Enter\"a basabilirsiniz.");
            CINSIYET? cinsiyet=_inputHelper.CinsiyetKontrol("Listelemek istediğiniz cinsiyeti girin (K/E): ");
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
                                          
                _outputHelper.WriteLine("\nListelenecek ögrenci yok.");
                _outputHelper.ListeCikis();
                return;
            }
            Console.WriteLine();
            _outputHelper.OgrenciListele(liste, 3, "Cinsiyete Göre Öğrenciler", false);
        }
        public void DogumTarihineGoreOgrenciListele()
        {
            _outputHelper.BaslikYazdir(4, "Dogum Tarihine Göre Ögrencileri Listele");
            DateTime? dogumTarihBilgisi;
            while (true)
            {
                dogumTarihBilgisi = _inputHelper.DogumTarihiKontrol("Hangi tarihten sonraki ögrencileri listelemek istersiniz (örn. 01.01.2000): ");
               
                if (dogumTarihBilgisi == null)
                {
                    _outputHelper.WriteLine("Veri girişi yapılmadı. Tekrar deneyin");
                    continue;
                }
                break;
            }
            var liste = okul.Ogrenciler.Where(o => o.DogumTarihi > dogumTarihBilgisi).ToList();
            if (liste.Count == 0)
            {
                _outputHelper.WriteLine("Listelenecek ögrenci yok.");
                _outputHelper.ListeCikis();
                return;

            }

            Console.WriteLine();
            _outputHelper.OgrenciListele(liste, 4, "Dogum Tarihine Göre Ögrencileri Listele", false);
        }

        public void IllereGoreOgrenciListele()
        {
       
            _outputHelper.OgrenciListele1(5);
        }
        public void OgrencininTumNotListele()
        {
            _outputHelper.OgrenciListele2(6);
        }
        public void OgrencininKitaplariListele()
        {
            _outputHelper.OgrenciListele3(7);
        }
        public void OkuldaEnYuksekNotlu5Listele()
        {
            List<Ogrenci>liste=okul.Ogrenciler.OrderByDescending(a=>a.Ortalama).Take(5).ToList();
            _outputHelper.OgrenciListele(liste, 8, "Okuldaki en başarılı 5 öğrenciyi listele");
        }
        public void OkuldaEnDusukNotlu3Listele()
        {
            List<Ogrenci> liste = okul.Ogrenciler.OrderBy(a => a.Ortalama).Take(3).ToList();
            _outputHelper.OgrenciListele(liste, 9, "Okuldaki en başarısız 3 öğrenciyi listele");
        }
        public void SubedeEnYuksekNotlu5Listele()
        {
            _outputHelper.BaslikYazdir(10, "Şubedeki en başarılı 5 ögrenciyi listele");
            _outputHelper.WriteLine("Şubesi atanmamış öğrenciler için \"Enter\"a basabilirsiniz.");
            SUBE? sube = _inputHelper.SubeKontrol("Listelemek istediğiniz şubeyi girin (A/B/C): ");
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
                _outputHelper.WriteLine("\nListelenecek ögrenci yok.");
                _outputHelper.ListeCikis();
                return;
                }
               
                    Console.WriteLine();
            _outputHelper.OgrenciListele(subeliste, 10, "Şubedeki en başarılı 5 ögrenciyi listele", false);
               
        }
        public void SubedeEnDusukNotlu3Listele()
        {
            _outputHelper.BaslikYazdir(11, "Şubedeki en başarısız 3 öğrenciyi listele");
            _outputHelper.WriteLine("Şubesi atanmamış öğrenciler için \"Enter\"a basabilirsiniz.");
            SUBE? sube = _inputHelper.SubeKontrol("Listelemek istediğiniz şubeyi girin (A/B/C): ");
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
                _outputHelper.WriteLine("\nListelenecek ögrenci yok.");
                _outputHelper.ListeCikis();
                return;
            }

            Console.WriteLine();
            _outputHelper.OgrenciListele(subeliste, 11, "Şubedeki en başarısız 3 öğrenciyi listele", false);
        }
        public void OgrencininNotOrtalamasiGor()
        {
            _outputHelper.BaslikYazdir(12, "Ögrencinin Not Ortalamasını Gör");
            Ogrenci ogrenci = null;
            while (true)
            {
                int numara=_inputHelper.OgrenciNoKontrol();
                ogrenci = HelperFunctions.OgrenciBulNo(okul, numara);
            
                if (ogrenci == null)
                {
                    _outputHelper.WriteLine("Bu numarada bir ögrenci yok.Tekrar deneyin.");
                    continue;
                }
                break;
            }

            _outputHelper.OgrenciBilgiYazdir(ogrenci);

                _outputHelper.WriteLine("\nÖgrencinin not ortalaması:" + ogrenci.Ortalama);
            _outputHelper.ListeCikis();
           
         
        }
        public void SubeninNotOrtalamasiGor()
        {
            _outputHelper.BaslikYazdir(13, "Şubenin Not Ortalamasını Gör");
            _outputHelper.WriteLine("Şubesi atanmamış öğrenciler için \"Enter\"a basabilirsiniz.");
            SUBE? sube= _inputHelper.SubeKontrol("Bir şube seçin (A/B/C): ");
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
                    _outputHelper.WriteLine("\nBu şubede öğrenci bulunamadı.");
                _outputHelper.ListeCikis();
                return;
                }

            double ortalama = liste.Average(o => o.Ortalama);
            Console.WriteLine();
            _outputHelper.WriteLine(sube + " şubesinin not ortalaması: " + ortalama);
            _outputHelper.ListeCikis();
        }
        public void OgrencininOkuduguSonKitapGor()
        {
            _outputHelper.BaslikYazdir(14, "Ögrencinin okuduğu son kitabı listele");
            Ogrenci ogrenci;
           
            while (true)
            {
                int numara = _inputHelper.OgrenciNoKontrol();
                ogrenci = HelperFunctions.OgrenciBulNo(okul, numara);

                if (ogrenci == null)
                {
                    _outputHelper.WriteLine("Bu numarada bir öğrenci yok. Tekrar deneyin.");
                    continue;
                }
                break;
            }

            _outputHelper.OgrenciBilgiYazdir(ogrenci);
                if (ogrenci.Kitaplar == null || ogrenci.Kitaplar.Count == 0)
                {
                    _outputHelper.WriteLine("Bu öğrencinin henüz okuduğu bir kitap kaydı yok.");
                }
                else
                {
                    _outputHelper.WriteLine("\nÖğrencinin Okuduğu Kitaplar");
                    _outputHelper.WriteLine("------------------------------");
                    _outputHelper.WriteLine(ogrenci.Kitaplar.Last());
                _outputHelper.ListeCikis();
                }
       
        }
        public void OgrenciEkle()
        {
            _outputHelper.BaslikYazdir(15, "Öğrenci Ekle");
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

            int numara = HelperFunctions.BosOgrenciNumarasiGetir(okul.Ogrenciler);

            while (true)
            {
                ad = _inputHelper.OgrenciAdSoyadKontrol("Öğrencinin adı: ");
                if (!string.IsNullOrWhiteSpace(ad))
                    break;
                _outputHelper.WriteLine("Veri girişi yapılmadı. Tekrar deneyin.");
            }
            while (true)
            {
                soyad = _inputHelper.OgrenciAdSoyadKontrol("Öğrencinin soyadı: ");
                if (!string.IsNullOrWhiteSpace(soyad))
                    break;
                _outputHelper.WriteLine("Veri girişi yapılmadı. Tekrar deneyin.");
            }
            while (true)
            {
                dogumTarihi = _inputHelper.DogumTarihiKontrol("Öğrencinin doğum tarihi: ");
                if (dogumTarihi == null)
                {
                    _outputHelper.WriteLine("Veri girişi yapılmadı. Tekrar deneyin.");
                    continue;
                }
                break;

            }
            _outputHelper.WriteLine("\nCinsiyeti boş bırakmak için \"Enter\"a basabilirsiniz.");
            cinsiyet = _inputHelper.CinsiyetKontrol("Öğrencinin cinsiyeti (K/E): ");
            if (cinsiyet == null) cinsiyet = CINSIYET.Empty;

            _outputHelper.WriteLine("\nŞubeyi boş bırakmak için \"Enter\"a basabilirsiniz.");
            sube = _inputHelper.SubeKontrol("Öğrencinin şubesi (A/B/C): ");
            if (sube == null) sube = SUBE.Empty;

            
            _outputHelper.WriteLine("\nÖğrenci " + numara + " okul numarası ile sisteme başarılı bir şekilde eklenmiştir.");
            okul.OgrenciEkle(numara, ad, soyad, dogumTarihi.Value, cinsiyet.Value, sube.Value);
            _outputHelper.ListeCikis();
        }
        public void OgrenciGuncelle()
        {
            _outputHelper.BaslikYazdir(16, "Öğrenci Güncelle");
            Ogrenci ogrenci;
            int numara;
            string? ad;
            string? soyad;
            DateTime? dogumTarihi;
            CINSIYET? cinsiyet;
            SUBE? sube;

            _outputHelper.WriteLine("Değiştirmek istemediğiniz bilgiler için \"Enter\"a basabilirsiniz.");
            _outputHelper.WriteLine("Bilgileri güncellenecek öğrencinin numarası zorunludur.");
           
            while (true)
            {
                numara = _inputHelper.OgrenciNoKontrol();
                ogrenci = HelperFunctions.OgrenciBulNo(okul, numara);

                if (ogrenci == null)
                {
                    _outputHelper.WriteLine("Bu numarada bir öğrenci yok. Tekrar deneyin.");
                    continue;
                }
                break;
            }
           
            ad = _inputHelper.OgrenciAdSoyadKontrol("Öğrencinin adı: ");
            if (ad==null)
                ad = ogrenci.Ad;
            soyad = _inputHelper.OgrenciAdSoyadKontrol("Öğrencinin soyadı: ");
            if (soyad==null)
                soyad = ogrenci.Soyad;
            dogumTarihi = _inputHelper.DogumTarihiKontrol("Öğrencinin doğum tarihi: ");
            if (dogumTarihi == null)
                dogumTarihi = ogrenci.DogumTarihi;

            cinsiyet = _inputHelper.CinsiyetKontrol("Öğrencinin cinsiyeti (K/E): ");
            if (cinsiyet == null) cinsiyet = ogrenci.Cinsiyet;

            sube = _inputHelper.SubeKontrol("Öğrencinin şubesi (A/B/C): ");
            if (sube == null)
                sube = ogrenci.Sube;

            okul.OgrenciGuncelle(numara, ad, soyad, dogumTarihi.Value, cinsiyet.Value, sube.Value);
            _outputHelper.WriteLine("\nÖğrenci güncellendi.");
            _outputHelper.ListeCikis();
        }
        
        public void OgrenciSil()
        {
            _outputHelper.BaslikYazdir(17, "Ögrenci sil");
            int numara;
            Ogrenci ogrenci;
            while (true)
            {
                numara = _inputHelper.OgrenciNoKontrol();
                ogrenci = HelperFunctions.OgrenciBulNo(okul, numara);

                if (ogrenci == null)
                {
                    _outputHelper.WriteLine("Bu numarada bir öğrenci yok. Tekrar deneyin.");
                    continue;
                }
                break;
            }
            _outputHelper.OgrenciBilgiYazdir(ogrenci); 
            _outputHelper.WriteLine("\nOnaylamak için \"E\" iptal etmek için \"H\" yazıp \"Enter\"a basın.");
            while (true)
            {
                var giris= _inputHelper.GetInput("Öğrenciyi silmek istediğinize emin misiniz (E/H): ").ToUpper();
               
                if (giris == null)
                {
                    _outputHelper.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin."); continue;
                }
                string cevap = giris.ToUpper();
                if (int.TryParse(cevap, out _))
                {
                    _outputHelper.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    continue;
                }
                if (cevap == "")
                { _outputHelper.WriteLine("Veri girişi yapılmadı. Tekrar deneyin."); }

                if (cevap != "E" && cevap != "H" && cevap!="")
                {
                    _outputHelper.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    continue;
                }
                if (cevap=="H")
                {
                    _outputHelper.ListeCikis();
                    return;
                }
                if (cevap == "E")
                {
                    _outputHelper.WriteLine("Öğrenci başarılı bir şekilde silindi.");
                    _outputHelper.ListeCikis();
                    okul.OgrenciSil(numara);
                    return;
                }
            }
         
        }
        public void OgrencininAdresiniGir()
        {
            _outputHelper.BaslikYazdir(18, "Öğrencinin Adresini Gir");
            _outputHelper.WriteLine("Adres bilgilerini dolduruken boş bırakmak istediğiniz bilgiler için \"enter\"a basabilirsiniz.");
            int numara;
            string il="";
            string ilce="";
            string mahalle="";
            Ogrenci? ogrenci = null;
            while (true)
            {
                numara = _inputHelper.OgrenciNoKontrol();
                ogrenci = HelperFunctions.OgrenciBulNo(okul, numara);

                if (ogrenci == null)
                {
                    _outputHelper.WriteLine("Bu numaraya ait bir öğrenci bulunamadı. Tekrar deneyin.");
                    continue;
                }
                break;
            }
            _outputHelper.OgrenciBilgiYazdir(ogrenci);
            Console.WriteLine();
            while (true)
            {
                var cevapIl = _inputHelper.GetInput("İl: ");
                               
                if (int.TryParse(cevapIl, out _) || cevapIl == " ")
                {
                    _outputHelper.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    continue;
                }
                if (cevapIl == null)
                { _outputHelper.WriteLine("Veri girişi yapılmadı."); break; }

                il=HelperFunctions.IlkHarfBuyut(cevapIl);
                break;
            }
            while (true)
            {
                var cevapIlce = _inputHelper.GetInput("İlçe: ");
                if (int.TryParse(cevapIlce, out int _) || cevapIlce==" ")
                {
                    _outputHelper.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    continue;
                }
                if (cevapIlce == null)
                { _outputHelper.WriteLine("Veri girişi yapılmadı."); break; }
                ilce = HelperFunctions.IlkHarfBuyut(cevapIlce);
                break;
            }
            while (true)
            {
                var cevapMahalle = _inputHelper.GetInput("Mahalle: ");
                if (int.TryParse(cevapMahalle, out int _) || cevapMahalle==" ")
                {
                    _outputHelper.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    continue;
                }
                if (cevapMahalle == null)
                    { _outputHelper.WriteLine("Veri girişi yapılmadı."); break; }
                mahalle = HelperFunctions.IlkHarfBuyut(cevapMahalle);
                break;
            }

            _outputHelper.WriteLine("\nBilgiler sisteme girilmiştir.");
            okul.AdresEkle(numara, il, ilce, mahalle);
        }

        public void OgrencininOkuduguKitabiGir()
        {
            _outputHelper.BaslikYazdir(19, "Öğrencinin okuduğu kitabı gir");
            int numara;
            string kitapAdi;
            Ogrenci ogrenci;
            while (true)
            {
                numara = _inputHelper.OgrenciNoKontrol();
                ogrenci = HelperFunctions.OgrenciBulNo(okul, numara);

                if (ogrenci == null)
                {
                    _outputHelper.WriteLine("Bu numaraya ait bir öğrenci bulunamadı. Tekrar deneyin.");
                    continue;
                }
                break;
            }
            _outputHelper.OgrenciBilgiYazdir(ogrenci);
            Console.WriteLine();
            
            while (true)
            {
                var kitap = _inputHelper.GetInput("Eklenecek Kitabın Adı: ");
                
                if (string.IsNullOrWhiteSpace(kitap))
                {
                    _outputHelper.WriteLine("Kitap adı boş bırakılamaz. Tekrar deneyin.");
                    continue;
                }
                kitapAdi = HelperFunctions.IlkHarfBuyut(kitap);
                _outputHelper.WriteLine("\nBilgiler sisteme girilmiştir.");
                break;
            }
            okul.KitapEkle(numara, kitapAdi);

        }

        public void OgrenciNotuGir()
        {
            _outputHelper.BaslikYazdir(20, "Not Gir");
            int numara;
            Ogrenci ogrenci;
            string? ders;
            int adet;
            try
            {
                while (true)
                {
                    numara = _inputHelper.OgrenciNoKontrol();
                    ogrenci = HelperFunctions.OgrenciBulNo(okul, numara);

                    if (ogrenci == null)
                    {
                        _outputHelper.WriteLine("Bu numarada bir öğrenci yok. Tekrar deneyin.");
                        continue;
                    }
                    break;
                }
                _outputHelper.OgrenciBilgiYazdir(ogrenci);
                Console.WriteLine();
                while (true)
                {
                    ders = _inputHelper.GetInput("Not eklemek istediğiniz dersi giriniz: ");
                    
                    if (string.IsNullOrWhiteSpace(ders) || int.TryParse(ders, out _))
                    {
                        _outputHelper.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                        continue;
                    }
                    break;
                }
                while (true)
                {
                    var giris = _inputHelper.GetInput("Eklemek istediğiniz not adedi: ");
                    
                    if (int.TryParse(giris, out _))
                    {
                        adet = int.Parse(giris);
                        break;
                    }
                    else
                    {
                        _outputHelper.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin."); continue;
                    }
                }

                for (int i = 1; i <= adet; i++)
                {
                    int not;
                    while (true)
                    {
                        var notGiris = _inputHelper.GetInput(i + ". notu girin (0-100): ");
                       
                        if (int.TryParse(notGiris, out not) && not >= 0 && not <= 100)
                            break;

                        _outputHelper.WriteLine("Hatalı giriş yapildi. Tekrar deneyin.");
                    }
                 

                    okul.NotEkle(numara, HelperFunctions.IlkHarfBuyut(ders), not);
                }
                _outputHelper.WriteLine("\nBilgiler sisteme girilmiştir.");
                _outputHelper.ListeCikis();
            }
            catch (Exception e)
            {
                _outputHelper.WriteLine("\nHata: " + e.Message);
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
