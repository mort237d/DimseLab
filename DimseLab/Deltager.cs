using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimseLab
{
    class Deltager
    {
        public string Navn { get; set; }
        public string Email { get; set; }

        public Deltager(string navn, string email)
        {
            Navn = navn;
            Email = email;
        }
    }
}
