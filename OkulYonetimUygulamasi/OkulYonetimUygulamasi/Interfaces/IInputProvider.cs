using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulYonetimUygulamasi.Interfaces
{
    public interface IInputProvider
    {
        string GetInput(string prompt);
    }
}
