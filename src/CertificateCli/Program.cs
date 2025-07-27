using CertificateCreator;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: certificatecli [options]\n" +
                "Run with --help to view available options.");
            return;
        }

        var options = CommandLineOptions.Parse(args);
        CertificateService.GenerateCertificates(options);
        Console.WriteLine($"Certificates written to {options.Output}");
    }
}
