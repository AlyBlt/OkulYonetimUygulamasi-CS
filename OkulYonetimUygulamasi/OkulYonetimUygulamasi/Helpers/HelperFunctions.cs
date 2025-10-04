using OkulYonetimUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulYonetimUygulamasi.Helpers
{
    internal class HelperFunctions
    {
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
        public static bool OgrenciVarMi(Okul okul)
        {
            return okul.Ogrenciler != null && okul.Ogrenciler.Any();
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
