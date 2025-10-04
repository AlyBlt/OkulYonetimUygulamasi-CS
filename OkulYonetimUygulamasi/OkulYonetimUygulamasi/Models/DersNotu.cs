using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulYonetimUygulamasi.Models
{
    public class DersNotu
    {
        public DersNotu(string dersAdi, int not)
        {
            DersAdi = dersAdi;
            Not = not;
        }

        public string DersAdi { get; set; }
        public int Not { get; set; }
    }
}
