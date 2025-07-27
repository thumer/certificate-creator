using System;

namespace CertificateCreator
{
    public class CommandLineOptions
    {
        public string Country { get; set; } = "AT";
        public string State { get; set; } = "Oberösterreich";
        public string Locality { get; set; } = string.Empty;
        public string Organization { get; set; } = string.Empty;
        public string CommonName { get; set; } = string.Empty;
        public bool CreateRoot { get; set; }
        public DateTime ExpirationDate { get; set; } = DateTime.Today.AddYears(5);
        public string Output { get; set; } = Environment.CurrentDirectory;

        public static CommandLineOptions Parse(string[] args)
        {
            var options = new CommandLineOptions();
            foreach (var arg in args)
            {
                if (arg.StartsWith("--country=")) options.Country = arg.Substring(10);
                else if (arg.StartsWith("--state=")) options.State = arg.Substring(8);
                else if (arg.StartsWith("--locality=")) options.Locality = arg.Substring(11);
                else if (arg.StartsWith("--org=")) options.Organization = arg.Substring(6);
                else if (arg.StartsWith("--cn=")) options.CommonName = arg.Substring(5);
                else if (arg.StartsWith("--output=")) options.Output = arg.Substring(9);
                else if (arg.StartsWith("--years="))
                {
                    if (int.TryParse(arg.Substring(8), out var years))
                        options.ExpirationDate = DateTime.Today.AddYears(years);
                }
                else if (arg == "--with-root") options.CreateRoot = true;
            }
            return options;
        }
    }
}
