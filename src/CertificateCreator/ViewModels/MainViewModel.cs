using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace CertificateCreator.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _country = "AT";
        private string _state = "Oberösterreich";
        private string _locality = string.Empty;
        private string _organization = string.Empty;
        private string _commonName = string.Empty;
        private DateTime _expiration = DateTime.Today.AddYears(5);
        private bool _createRoot;

        public string Country { get => _country; set { _country = value; OnPropertyChanged(); } }
        public string State { get => _state; set { _state = value; OnPropertyChanged(); } }
        public string Locality { get => _locality; set { _locality = value; OnPropertyChanged(); } }
        public string Organization { get => _organization; set { _organization = value; OnPropertyChanged(); } }
        public string CommonName { get => _commonName; set { _commonName = value; OnPropertyChanged(); } }
        public DateTime Expiration { get => _expiration; set { _expiration = value; OnPropertyChanged(); } }
        public bool CreateRoot { get => _createRoot; set { _createRoot = value; OnPropertyChanged(); } }

        public RelayCommand CreateCommand { get; }

        public MainViewModel()
        {
            CreateCommand = new RelayCommand(Create);
        }

        private void Create()
        {
            using var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var opts = new CommandLineOptions
                {
                    Country = Country,
                    State = State,
                    Locality = Locality,
                    Organization = Organization,
                    CommonName = CommonName,
                    ExpirationDate = Expiration,
                    CreateRoot = CreateRoot,
                    Output = dialog.SelectedPath
                };
                CertificateService.GenerateCertificates(opts);
                System.Windows.MessageBox.Show("Zertifikate erstellt", "Fertig");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
