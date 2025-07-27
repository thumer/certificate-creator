using System.Windows;
using CertificateCreator.ViewModels;

namespace CertificateCreator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
