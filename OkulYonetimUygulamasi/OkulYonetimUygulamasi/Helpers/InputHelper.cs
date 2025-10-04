using OkulYonetimUygulamasi.Interfaces;
using OkulYonetimUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OkulYonetimUygulamasi.Helpers
{
    public class InputHelper
    {
        private readonly IInputProvider _input;
        private readonly IOutputProvider _output;
        public InputHelper(IInputProvider input, IOutputProvider output)
        {
            _input = input;
            _output = output;
        }
        public string GetInput(string prompt)
    {
        return _input.GetInput(prompt);
    }
        public SUBE? SubeKontrol(string subeIstek)
        {
            //Sube kontrolde sube boş bırakıldığında hata vermesin istiyorum çünkü
            //yeni kayıt olmuş ve şubesi atanmayan öğrenci olabilir. Empty şubede de öğrenci listesi varsa listelensin.
            while (true)
            {
                var subebilgisi = _input.GetInput(subeIstek).ToUpper();
                
                // Sadece Enter'a basıldıysa (tamamen boşsa): Şubesiz öğrenciler
                if (string.IsNullOrEmpty(subebilgisi))
                    return null;

                if (Enum.TryParse(subebilgisi, out SUBE sube) && Enum.IsDefined(typeof(SUBE), sube))
                {
                    return sube;
                }
                _output.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                continue;
            }

        }
        public int OgrenciNoKontrol()
        {
            while (true)
            {
                var numara = _input.GetInput("Öğrenci numarası: ");
                if (!int.TryParse(numara, out int sayi))
                {
                    _output.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    continue;
                }

                return sayi;
            }
        }
        public CINSIYET? CinsiyetKontrol(string cinsiyetIstek)
        {
            while (true)
            {
                var cinsiyetBilgisi = _input.GetInput(cinsiyetIstek).ToUpper();
               
                if (string.IsNullOrEmpty(cinsiyetBilgisi))
                    return null;

                cinsiyetBilgisi = cinsiyetBilgisi.Trim().ToUpper();
                if (cinsiyetBilgisi == "K")
                    return CINSIYET.Kiz;
                else if (cinsiyetBilgisi == "E")
                    return CINSIYET.Erkek;
                else
                {
                    _output.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    return CinsiyetKontrol(cinsiyetIstek); // Kullanıcıya tekrar sor
                }
            }
        }
        public DateTime? DogumTarihiKontrol(string tarihIstek)
        {
            DateTime dogumTarihBilgisi;
            var girilenTarih = _input.GetInput(tarihIstek);
            
            if (string.IsNullOrEmpty(girilenTarih))
                return null;

            if (DateTime.TryParse(girilenTarih, out dogumTarihBilgisi))
                return dogumTarihBilgisi;

            _output.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
            return DogumTarihiKontrol(tarihIstek);

        }
        public string? OgrenciAdSoyadKontrol(string istek)
        {
            while (true)
            {
                var input = _input.GetInput(istek);


                if (string.IsNullOrEmpty(input))
                    return null;

                // Sadece harfler (Türkçe dahil) ve boşluklara izin veren regex
                if (Regex.IsMatch(input, @"^(?=.*[a-zA-ZçÇğĞıİöÖşŞüÜ])[a-zA-ZçÇğĞıİöÖşŞüÜ\s]+$"))
                    return HelperFunctions.IlkHarfBuyut(input);
                else
                {
                    _output.WriteLine("Hatalı giriş yapıldı. Tekrar deneyin.");
                    OgrenciAdSoyadKontrol(istek);
                }
                return input;
            }
         
        }
    }
}
