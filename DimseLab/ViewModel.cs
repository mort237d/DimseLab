using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DimseLab.Annotations;
using GalaSoft.MvvmLight.Command;

namespace DimseLab
{
    class ViewModel : INotifyPropertyChanged
    {
        #region Field
        private RelayCommand tilføjProjektCommand;
        private RelayCommand tilføjDeltagerCommand;
        private RelayCommand tilføjDimsCommand;

        private RelayCommand sletProjektCommand;
        private RelayCommand sletDeltagerCommand;
        private RelayCommand sletDimsCommand;

        private string _navnProjektTB, _beskrivelseProjektTB, deltagerCollectionProjektTB;
        private string _navnDeltagerTB, _emailDeltagerTB;
        private string _navnDimsTB;

        private Projekt _selectedProjekt;
        private Deltager _selectedDeltager;
        private Dims _selectedDims;

        public ObservableCollection<Projekt> _projekter; 
        #endregion

        public ObservableCollection<Projekt> Projekter
        {
            get { return _projekter; }
            set
            {
                _projekter = value;
                OnPropertyChanged();
            }
        }

        #region Selected props
        public Projekt SelectedProjekt
        {
            get { return _selectedProjekt; }
            set
            {
                _selectedProjekt = value;
                OnPropertyChanged();
            }
        }

        public Deltager SelectedDeltager
        {
            get { return _selectedDeltager; }
            set
            {
                _selectedDeltager = value;
                OnPropertyChanged();
            }
        }

        public Dims SelectedDims
        {
            get { return _selectedDims; }
            set
            {
                _selectedDims = value;
                OnPropertyChanged();
            }
        } 
        #endregion

        #region Textbox props
        public string NavnDeltagerTB
        {
            get { return _navnDeltagerTB; }
            set
            {
                _navnDeltagerTB = value;
                OnPropertyChanged();
            }
        }
        public string EmailDeltagerTB
        {
            get { return _emailDeltagerTB; }
            set
            {
                _emailDeltagerTB = value;
                OnPropertyChanged();
            }
        }

        public string NavnProjektTB
        {
            get { return _navnProjektTB; }
            set
            {
                _navnProjektTB = value;
                OnPropertyChanged();
            }
        }

        public string BeskrivelseProjektTB
        {
            get { return _beskrivelseProjektTB; }
            set
            {
                _beskrivelseProjektTB = value;
                OnPropertyChanged();
            }
        }

        public string DeltagerCollectionProjektTB
        {
            get { return deltagerCollectionProjektTB; }
            set
            {
                deltagerCollectionProjektTB = value;
                OnPropertyChanged();
            }
        }

        public string NavnDimsTB
        {
            get { return _navnDimsTB; }
            set
            {
                _navnDimsTB = value;
                OnPropertyChanged();
            }
        } 
        #endregion

        #region Tilføj/slet command props
        public RelayCommand TilføjProjektCommand
        {
            get { return tilføjProjektCommand; }
            set { tilføjProjektCommand = value; }
        }

        public RelayCommand TilføjDeltagerCommand
        {
            get { return tilføjDeltagerCommand; }
            set
            {
                tilføjDeltagerCommand = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand TilføjDimsCommand
        {
            get { return tilføjDimsCommand; }
            set { tilføjDimsCommand = value; }
        }

        public RelayCommand SletProjektCommand
        {
            get { return sletProjektCommand; }
            set { sletProjektCommand = value; }
        }

        public RelayCommand SletDeltagerCommand
        {
            get { return sletDeltagerCommand; }
            set { sletDeltagerCommand = value; }
        }

        public RelayCommand SletDimsCommand
        {
            get { return sletDimsCommand; }
            set { sletDimsCommand = value; }
        } 
        #endregion

        public ViewModel()
        {
            TilføjRelayCommands();

            LavListeAfProjekter();
        }

        private void TilføjRelayCommands()
        {
            TilføjProjektCommand = new RelayCommand(tilføjProjekt);
            TilføjDeltagerCommand = new RelayCommand(tilføjDeltager);
            TilføjDimsCommand = new RelayCommand(tilføjDims);

            SletProjektCommand = new RelayCommand(sletProjekt);
            SletDeltagerCommand = new RelayCommand(sletDeltager);
            SletDimsCommand = new RelayCommand(sletDims);
        }

        private void LavListeAfProjekter()
        {
            _projekter = new ObservableCollection<Projekt>()
            {
                new Projekt("Projekt 1", "Vand", 
                    new ObservableCollection<Deltager>()
                    {
                        new Deltager("Morten", "mail.dk"),
                        new Deltager("Kurt", "mail.com")
                    },
                    new ObservableCollection<Dims>()
                    {
                        new Dims("IR-modtager", 
                            new List<string>() {"IR", "Modtager", "Robot"}, 
                            DateTime.Now.ToString("d"),
                            DateTime.Now.AddDays(14).ToString("d"))
                    }),
                new Projekt("Projekt 2", "Ild", 
                    new ObservableCollection<Deltager>()
                    {
                        new Deltager("Mads", "mail.org"),
                        new Deltager("Karsten", "mail.dk")
                    },
                    new ObservableCollection<Dims>()
                    {
                        new Dims("lys-komponent", 
                            new List<string>() {"Lys", "Robot"}, 
                            DateTime.Now.ToString("d"),
                            DateTime.Now.AddDays(14).ToString("d"))
                    })
            };
        }


        #region KnapFunktioner
        public void tilføjProjekt()
        {
            if (NavnProjektTB != null && BeskrivelseProjektTB != null)
            {
                Projekter.Add(new Projekt(NavnProjektTB, BeskrivelseProjektTB, new ObservableCollection<Deltager>(), new ObservableCollection<Dims>()));
                NavnProjektTB = null;
                BeskrivelseProjektTB = null;
            }
        }
        public void tilføjDeltager()
        {
            if (SelectedProjekt != null)
            {
                if (NavnDeltagerTB != null && EmailDeltagerTB != null)
                {
                    SelectedProjekt.Deltagere.Add(new Deltager(NavnDeltagerTB, EmailDeltagerTB));
                    NavnDeltagerTB = null;
                    EmailDeltagerTB = null;
                }
            }
        }
        public void tilføjDims()
        {
            if (SelectedProjekt != null)
            {
                if (NavnDimsTB != null)
                {
                    SelectedProjekt.Dimser.Add(new Dims(NavnDimsTB, new List<string>() { "1" }, DateTime.Now.ToString("d"), DateTime.Now.AddDays(14).ToString("d")));
                    NavnDimsTB = null;
                }
            }
        }

        public void sletProjekt()
        {
            Projekter.Remove(SelectedProjekt);
        }
        private void sletDeltager()
        {
            SelectedProjekt.Deltagere.Remove(SelectedDeltager);
        }
        private void sletDims()
        {
            SelectedProjekt.Dimser.Remove(SelectedDims);
        } 
        #endregion

        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
