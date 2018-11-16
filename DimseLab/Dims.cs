using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DimseLab.Annotations;

namespace DimseLab
{
    class Dims : INotifyPropertyChanged
    {
        private bool _udlånt;

        public string Navn { get; set; }
        public List<string> Keywords { get; set; }
        public string Udlånsdato { get; set; }
        public string Afleveringsdato { get; set; }

        public bool Udlånt
        {
            get { return _udlånt; }
            set
            {
                _udlånt = value; 
                OnPropertyChanged();
            }
        }

        public Dims(string navn, List<string> keywords, string udlånsdato, string afleveringsdato, bool udlånt)
        {
            Navn = navn;
            Keywords = keywords;
            Udlånsdato = udlånsdato;
            Afleveringsdato = afleveringsdato;
            Udlånt = udlånt;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
