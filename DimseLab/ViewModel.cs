using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Popups;
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

        private string _udlånsDage;
        public string UdlånsDage
        {
            get { return _udlånsDage; }
            set
            {
                _udlånsDage = value;
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

            LavListeAfDimser();
            LavListeAfProjekter();
        }

        private void LavListeAfDimser()
        {
            Dimser = new ObservableCollection<Dims>()
            {
                new Dims("IR-modtager"),
                new Dims("IR-sender"),
                new Dims("Lygte"),
                new Dims("Skruetrækker"),
                new Dims("Badedyr"),
                new Dims("Kaffemaskine"),
                new Dims("Lodde-Kolbe"),
                new Dims("Tin"),
                new Dims("DimseDut"),
                new Dims("Tingest"),
                new Dims("Makrel"),
                new Dims("Te-Kande"),
                new Dims("Madpakke"),
                new Dims("Sild"),
                new Dims("Tv"),
                new Dims("Radio"),
                new Dims("Haj-Tand"),
                new Dims("Tandpasta")
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
                        new Deltager("Kurt", "mail.com"),
                        new Deltager("Kurt", "mail.com"),
                        new Deltager("Kurt", "mail.com"),
                        new Deltager("Kurt", "mail.com")
                    },
                    new ObservableCollection<Dims>()),
                new Projekt("Projekt 2", "Ild", 
                    new ObservableCollection<Deltager>()
                    {
                        new Deltager("Mads", "mail.org"),
                        new Deltager("Karsten", "mail.dk"),
                        new Deltager("Karsten", "mail.dk"),
                        new Deltager("Karsten", "mail.dk"),
                        new Deltager("Karsten", "mail.dk")
                    },
                    new ObservableCollection<Dims>())
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
                    if (!SelectedProjekt.Dimser.Contains(SelectedDimsOversigt) && SelectedDimsOversigt.Udlånt != "Udlånt")
                    {
                        if (double.TryParse(UdlånsDage, out double n))
                        {
                            SelectedProjekt.Dimser.Add(SelectedDimsOversigt);
                            SelectedDimsOversigt.Projekt = SelectedProjekt;
                            SelectedDimsOversigt.Udlånsdato = DateTime.Now.ToString("d");
                            SelectedDimsOversigt.Afleveringsdato = DateTime.Now.AddDays(n).ToString("d");
                            UdlånsDage = null;
                            SelectedDimsOversigt.UdlånsInfo = " af " + SelectedDimsOversigt.Projekt.Navn + " til og med " + SelectedDimsOversigt.Afleveringsdato;
                            SelectedDimsOversigt.Udlånt = "Udlånt";
                            SelectedDimsOversigt.TextColor = new SolidColorBrush(Colors.Red);
                        }
                    }
                }
            }
        }

        private async void sletProjekt()
        {
            if (SelectedProjekt != null)
            {
                await Besked("Slet projekt",
                    "Du er ved at slette et projekt.\r\nEr du sikker på at du vil slette " + SelectedProjekt.Navn + "?");
            }
        }
        private async void sletDeltager()
        {
            if (SelectedProjekt != null)
            {
                await Besked("Slet deltager", 
                    "Du er ved at slette en deltager.\r\nEr du sikker på at du vil slette " + SelectedDeltager.Navn + " fra " + SelectedProjekt.Navn + "?");
                
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
                    SelectedDimsOversigt.UdlånsInfo = "";
                    Dimser[Dimser.IndexOf(SelectedDims)].Udlånt = "Ikke udlånt";
                    Dimser[Dimser.IndexOf(SelectedDims)].TextColor = new SolidColorBrush(Colors.Green);
                    SelectedProjekt.Dimser.Remove(SelectedDims);
                }
            }
        }

        private async void ændreProjekt()
        {
            if (SelectedProjekt != null)
            {
                if (NavnProjektTB == null || BeskrivelseProjektTB == null)
                {
                    if (NavnProjektTB == null)
                    {
                        NavnProjektTB = SelectedProjekt.Navn;
                    }
                    if (BeskrivelseProjektTB == null)
                    {
                        BeskrivelseProjektTB = SelectedProjekt.Beskrivelse;
                    }
                }
                else if (NavnProjektTB != SelectedProjekt.Navn || BeskrivelseProjektTB != SelectedProjekt.Beskrivelse)
                {
                    await Besked("Ændre projekt",
                        "Du er ved at ændre et projekt." +
                        "\r\nEr du sikker på at du vil ændre navn fra \"" + SelectedProjekt.Navn + "\" til \"" + NavnProjektTB + "\"?" +
                        "\r\nSamt \"" + SelectedProjekt.Beskrivelse + "\" til \"" + BeskrivelseProjektTB + "\"");
                }
            }
        }
        private async void ændreDeltager()
        {
            if (SelectedDeltager != null)
            {
                if (NavnDeltagerTB == null || EmailDeltagerTB == null)
                {
                    if (NavnDeltagerTB == null)
                    {
                        NavnDeltagerTB = SelectedDeltager.Navn;
                    }
                    if (EmailDeltagerTB == null)
                    {
                        EmailDeltagerTB = SelectedDeltager.Email;
                    }
                }
                else if (NavnDeltagerTB != SelectedDeltager.Navn || EmailDeltagerTB != SelectedDeltager.Email)
                {
                    await Besked("Ændre deltager",
                        "Du er ved at ændre en deltager." +
                        "\r\nEr du sikker på at du vil ændre navn fra \"" + SelectedDeltager.Navn + "\" til \"" + NavnDeltagerTB + "\"?" +
                        "\r\nSamt \"" + SelectedDeltager.Email + "\" til \"" + EmailDeltagerTB + "\"");
                }
            }
        }

        private async Task Besked(string title, string content)
        {
            var yesCommand = new UICommand("Ja", cmd => { });
            var noCommand = new UICommand("Nej", cmd => { });

            var dialog = new MessageDialog(content, title);
            dialog.Options = MessageDialogOptions.None;
            dialog.Commands.Add(yesCommand);

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 0;

            if (noCommand != null)
            {
                dialog.Commands.Add(noCommand);
                dialog.CancelCommandIndex = (uint) dialog.Commands.Count - 1;
            }

            var command = await dialog.ShowAsync();

            if (command == yesCommand)
            {
                if (title == "Slet deltager") SelectedProjekt.Deltagere.Remove(SelectedDeltager);
                else if (title == "Slet projekt") Projekter.Remove(SelectedProjekt);
                else if (title == "Ændre projekt")
                {
                    SelectedProjekt.Navn = NavnProjektTB;
                    SelectedProjekt.Beskrivelse = BeskrivelseProjektTB;
                    NavnProjektTB = null;
                    BeskrivelseProjektTB = null;
                }
                else if (title == "Ændre deltager")
                {
                    SelectedDeltager.Navn = NavnDeltagerTB;
                    SelectedDeltager.Email = EmailDeltagerTB;
                    NavnDeltagerTB = null;
                    EmailDeltagerTB = null;
                }
            }
            else if (command == noCommand)
            {
                if (title == "Ændre projekt")
                {
                    NavnProjektTB = null;
                    BeskrivelseProjektTB = null;
                }

                if (title == "Ændre deltager")
                {
                    NavnDeltagerTB = null;
                    EmailDeltagerTB = null;
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
