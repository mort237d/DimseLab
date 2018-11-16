using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI;
using Windows.UI.Xaml.Media;
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

        private RelayCommand ændreProjektCommand;
        private RelayCommand ændreDeltagerCommand;

        private string _navnProjektTB, _beskrivelseProjektTB, deltagerCollectionProjektTB;
        private string _navnDeltagerTB, _emailDeltagerTB;
        private string _navnDimsTB;

        private Projekt _selectedProjekt;
        private Deltager _selectedDeltager;
        private Dims _selectedDims;
        private Dims _selectedDimsOversigt;

        public ObservableCollection<Projekt> _projekter;
        public ObservableCollection<Dims> _dimser;

        private int btncount = 0;

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
        public ObservableCollection<Dims> Dimser
        {
            get { return _dimser; }
            set
            {
                _dimser = value; 
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

        public Dims SelectedDimsOversigt
        {
            get { return _selectedDimsOversigt; }
            set
            {
                _selectedDimsOversigt = value;
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

        #region Tilføj/slet/ændre command props
        #region Tilføj
        public RelayCommand TilføjProjektCommand
        {
            get { return tilføjProjektCommand; }
            set { tilføjProjektCommand = value; }
        }

        public RelayCommand TilføjDeltagerCommand
        {
            get { return tilføjDeltagerCommand; }
            set { tilføjDeltagerCommand = value; }
        }

        public RelayCommand TilføjDimsCommand
        {
            get { return tilføjDimsCommand; }
            set { tilføjDimsCommand = value; }
        } 
        #endregion

        #region Slet
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

        #region Ændre
        public RelayCommand ÆndreDeltagerCommand
        {
            get { return ændreDeltagerCommand; }
            set { ændreDeltagerCommand = value; }
        }

        public RelayCommand ÆndreProjektCommand
        {
            get { return ændreProjektCommand; }
            set { ændreProjektCommand = value; }
        }
        #endregion
        #endregion

        public ViewModel()
        {
            TilføjRelayCommands();

            LavListeAfProjekter();
            LavListeAfDimser();
        }

        private void LavListeAfDimser()
        {
            Dimser = new ObservableCollection<Dims>()
            {
                new Dims("IR-modtager", new List<string>(), "", "", false, null),
                new Dims("IR-sender", new List<string>(), "", "", false, null),
                new Dims("Lygte", new List<string>(), "", "", false, null),
                new Dims("Skruetrækker", new List<string>(), "", "", false, null),
                new Dims("Badedyr", new List<string>(), "", "", false, null),
                new Dims("Kaffemaskine", new List<string>(), "", "", false, null)
            };
        }

        private void TilføjRelayCommands()
        {
            TilføjProjektCommand = new RelayCommand(tilføjProjekt);
            TilføjDeltagerCommand = new RelayCommand(tilføjDeltager);
            TilføjDimsCommand = new RelayCommand(tilføjDims);

            SletProjektCommand = new RelayCommand(sletProjekt);
            SletDeltagerCommand = new RelayCommand(sletDeltager);
            SletDimsCommand = new RelayCommand(sletDims);

            ÆndreProjektCommand = new RelayCommand(ændreProjekt);
            ÆndreDeltagerCommand = new RelayCommand(ændreDeltager);
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
                            DateTime.Now.AddDays(14).ToString("d"),
                            true, null)
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
                            DateTime.Now.AddDays(14).ToString("d"),
                            true, null)
                    })
            };
        }


        #region KnapFunktioner
        private void tilføjProjekt()
        {
            if (NavnProjektTB != null && BeskrivelseProjektTB != null)
            {
                Projekter.Add(new Projekt(NavnProjektTB, BeskrivelseProjektTB, new ObservableCollection<Deltager>(), new ObservableCollection<Dims>()));
                NavnProjektTB = null;
                BeskrivelseProjektTB = null;
            }
        }
        private void tilføjDeltager()
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
        private void tilføjDims()
        {
            if (SelectedProjekt != null)
            {
                if (SelectedDimsOversigt != null)
                {
                    if (!SelectedProjekt.Dimser.Contains(SelectedDimsOversigt) && !SelectedDimsOversigt.Udlånt)
                    {
                        SelectedProjekt.Dimser.Add(SelectedDimsOversigt);
                        SelectedDimsOversigt.Projekt = SelectedProjekt;
                        SelectedDimsOversigt.Udlånsdato = DateTime.Now.ToString("d");
                        SelectedDimsOversigt.Afleveringsdato = DateTime.Now.AddDays(14).ToString("d");
                        SelectedDimsOversigt.Udlånt = true;
                        SelectedDimsOversigt.TextColor = new SolidColorBrush(Colors.Red);
                    }
                }
            }
        }

        private void sletProjekt()
        {
            if (SelectedProjekt != null)
            {
                Projekter.Remove(SelectedProjekt);
            }
        }
        private void sletDeltager()
        {
            if (SelectedProjekt != null)
            {
                SelectedProjekt.Deltagere.Remove(SelectedDeltager);
            }
        }
        private void sletDims()
        {
            if (SelectedProjekt != null)
            {
                if (SelectedDims != null)
                {
                    Dimser[Dimser.IndexOf(SelectedDims)].Udlånsdato = "";
                    Dimser[Dimser.IndexOf(SelectedDims)].Afleveringsdato = "";
                    Dimser[Dimser.IndexOf(SelectedDims)].Udlånt = false;
                    Dimser[Dimser.IndexOf(SelectedDims)].TextColor = new SolidColorBrush(Colors.Green);
                    SelectedProjekt.Dimser.Remove(SelectedDims);
                }
            }
        }

        private void ændreProjekt()
        {
            if (SelectedProjekt != null)
            {
                if (NavnProjektTB == null)
                {
                    NavnProjektTB = SelectedProjekt.Navn;
                }
                else if (NavnProjektTB != SelectedProjekt.Navn)
                {
                    SelectedProjekt.Navn = NavnProjektTB;
                }

                if (BeskrivelseProjektTB == null)
                {
                    BeskrivelseProjektTB = SelectedProjekt.Beskrivelse;
                }
                else if (BeskrivelseProjektTB != SelectedProjekt.Beskrivelse)
                {
                    SelectedProjekt.Beskrivelse = BeskrivelseProjektTB;
                }

                btncount++;

                if (btncount == 2)
                {
                    NavnProjektTB = null;
                    BeskrivelseProjektTB = null;

                    btncount = 0;
                }
            }
        }
        private void ændreDeltager()
        {
            if (SelectedDeltager != null)
            {
                if (NavnDeltagerTB == null)
                {
                    NavnDeltagerTB = SelectedDeltager.Navn;
                }
                else if (NavnDeltagerTB != SelectedDeltager.Navn)
                {
                    SelectedDeltager.Navn = NavnDeltagerTB;
                }

                if (EmailDeltagerTB == null)
                {
                    EmailDeltagerTB = SelectedDeltager.Email;
                }
                else if (EmailDeltagerTB != SelectedDeltager.Email)
                {
                    SelectedDeltager.Email = EmailDeltagerTB;
                }

                btncount++;

                if (btncount == 2)
                {
                    NavnDeltagerTB = null;
                    EmailDeltagerTB = null;

                    btncount = 0;
                }
            }
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
