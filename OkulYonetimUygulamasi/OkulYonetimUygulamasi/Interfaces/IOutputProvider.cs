using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulYonetimUygulamasi.Interfaces
{
    public interface IOutputProvider
    {
        void Write(string message);
        void WriteLine(string message);
    }
}
