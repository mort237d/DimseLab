using System.ComponentModel;
using System.Runtime.CompilerServices;
using DimseLab.Annotations;

namespace DimseLab
{
    class Deltager : INotifyPropertyChanged
    {
        public string _navn;
        public string _email;

        public Deltager(string navn, string email)
        {
            Navn = navn;
            Email = email;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Navn
        {
            get { return _navn; }
            set
            {
                _navn = value; 
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
