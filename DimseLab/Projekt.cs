﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DimseLab.Annotations;

namespace DimseLab
{
    class Projekt : INotifyPropertyChanged
    {
        public string _navn;
        public string _beskrivelse;
        private string _startDato;
        private string _slutDato;

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

        public string Navn
        {
            get { return _navn; }
            set
            {
                _navn = value;
                OnPropertyChanged();
            }
        }
        public string Beskrivelse
        {
            get { return _beskrivelse; }
            set
            {
                _beskrivelse = value;
                OnPropertyChanged();
            }
        }
        public string StartDato
        {
            get { return _startDato; }
            set
            {
                _startDato = value;
                OnPropertyChanged();
            }
        }
        public string SlutDato
        {
            get { return _slutDato; }
            set
            {
                _slutDato = value;
                OnPropertyChanged();
            }
        }

        public Projekt(string navn, string beskrivelse, string startDato, string slutDato,  ObservableCollection<Deltager> listeAfDeltagere, ObservableCollection<Dims> listeAfDimser)
        {
            Navn = navn;
            Beskrivelse = beskrivelse;
            StartDato = startDato;
            SlutDato = slutDato;
            Deltagere = listeAfDeltagere;
            Dimser = listeAfDimser;
        }

        #region INotify

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
