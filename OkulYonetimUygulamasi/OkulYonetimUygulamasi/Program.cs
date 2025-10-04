using OkulYonetimUygulamasi.Interfaces;
using OkulYonetimUygulamasi.Models;
using OkulYonetimUygulamasi.Providers;
namespace OkulYonetimUygulamasi
   
{
    internal class Program
    {
     
        static void Main(string[] args)
        {
            IInputProvider inputProvider = new ConsoleInputProvider();
            IOutputProvider outputProvider = new ConsoleOutputProvider();

            var uygulama = new Uygulama(inputProvider, outputProvider);
             uygulama.Yonetim();

        }
             

    }
}
