using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulYonetimUygulamasi.Models
{
    public class Adres
    {
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string Mahalle { get; set; }

        public override string ToString()
        {
            return $"{Mahalle}, {Ilce}, {Il}";
        }
    }
}
