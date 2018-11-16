using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using DimseLab.Annotations;

namespace DimseLab
{
    class Dims : INotifyPropertyChanged
    {
        private bool _udlånt;
        private Brush _textColor;
        private Projekt _projekt;

        public string Navn { get; set; }
        public List<string> Keywords { get; set; }
        public string Udlånsdato { get; set; }
        public string _afleveringsdato;

        public bool Udlånt
        {
            get { return _udlånt; }
            set
            {
                _udlånt = value; 
                OnPropertyChanged();
            }
        }

        public Brush TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;
                OnPropertyChanged();
            }
        }

        public string Afleveringsdato
        {
            get { return _afleveringsdato; }
            set
            {
                _afleveringsdato = value;
                OnPropertyChanged();
            }
        }

        public Projekt Projekt
        {
            get { return _projekt; }
            set
            {
                _projekt = value; 
                OnPropertyChanged();
            }
        }

        public Dims(string navn, List<string> keywords, string udlånsdato, string afleveringsdato, bool udlånt, Projekt projekt)
        {
            Navn = navn;
            Keywords = keywords;
            Udlånsdato = udlånsdato;
            Afleveringsdato = afleveringsdato;
            Udlånt = udlånt;

            Projekt = projekt;

            TextColor = new SolidColorBrush(Colors.Green);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
