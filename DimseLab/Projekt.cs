using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DimseLab.Annotations;

namespace DimseLab
{
    class Projekt : INotifyPropertyChanged
    {
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }

        public ObservableCollection<Deltager> _deltagere;
        public ObservableCollection<Deltager> Deltagere
        {
            get { return _deltagere; }
            set
            {
                _deltagere = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Dims> _dimser;
        public ObservableCollection<Dims> Dimser
        {
            get { return _dimser; }
            set
            {
                _dimser = value;
                OnPropertyChanged();
            }
        }

        public Projekt(string navn, string beskrivelse, ObservableCollection<Deltager> listeAfDeltagere, ObservableCollection<Dims> listeAfDimser)
        {
            Navn = navn;
            Beskrivelse = beskrivelse;
            Deltagere = listeAfDeltagere;
            Dimser = listeAfDimser;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
