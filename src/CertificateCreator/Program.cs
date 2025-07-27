using System;
using System.Windows;

namespace CertificateCreator
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                var options = CommandLineOptions.Parse(args);
                CertificateService.GenerateCertificates(options);
                return;
            }
            var app = new App();
            app.Run(new MainWindow());
        }
    }
}
