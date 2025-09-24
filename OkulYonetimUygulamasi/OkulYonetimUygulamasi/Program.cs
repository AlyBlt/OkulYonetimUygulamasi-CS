namespace OkulYonetimUygulamasi
{
    internal class Program
    {
        // kullanıcı ile etkileşime geçilecek kodlar burada yazılacak.

        // Okul sınıfı metotlarına ve/veya özelliklerine erişebilmek için Okul nesnesi oluşturuduk
        static Okul Okul = new Okul();

        static void Main(string[] args)
        {
            Uygulama();
        }
        static void Uygulama()
        {
            // Menu();
            // SecimAl
            // switch-case
        }
        static void NotGir()
        {
            
            try
            {
                Console.WriteLine("20-Not Gir ----------------------------------------------------------");

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
                    Okul.NotEkle(no, ders, not);
                }

            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }


        }

        static void SahteVeriGir()
        {
            Okul.OgrenciEkle(1, "Ali" , "Yılmaz", new DateTime(2000,5,3), CINSIYET.Erkek, SUBE.A);
            // başka öğrenciler de eklenir


            // adres
            // ders notu
            // kitap
            // gibi veriler için de sahte veriler eklenir.


        }
    }
}
