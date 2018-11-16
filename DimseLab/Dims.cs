using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimseLab
{
    class Dims
    {
        public string Navn { get; set; }
        public List<string> Keywords { get; set; }
        public DateTime Udlånsdato { get; set; }
        public DateTime Afleveringsdato { get; set; }

        public Dims(string navn, List<string> keywords, DateTime udlånsdato, DateTime afleveringsdato)
        {
            Navn = navn;
            Keywords = keywords;
            Udlånsdato = udlånsdato;
            Afleveringsdato = afleveringsdato;
        }

    }
}
