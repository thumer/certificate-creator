using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CertificateCreator
{
    public static class CertificateService
    {
        public static void GenerateCertificates(CommandLineOptions options)
        {
            Directory.CreateDirectory(options.Output);
            X509Certificate2? rootCert = null;
            RSA? rootRsa = null;

            if (options.CreateRoot)
            {
                rootRsa = RSA.Create(2048);
                var rootRequest = new CertificateRequest($"C={options.Country}, ST={options.State}, L={options.Locality}, O={options.Organization}, CN={options.CommonName}", rootRsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                rootRequest.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, false, 0, true));
                rootRequest.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(rootRequest.PublicKey, false));
                rootCert = rootRequest.CreateSelfSigned(DateTimeOffset.Now, options.ExpirationDate);
                var rootPem = ExportCertificateAndPrivateKeyToPem(rootCert, rootRsa);
                File.WriteAllText(Path.Combine(options.Output, "root.pem"), rootPem);
            }

            using RSA serverRsa = RSA.Create(2048);
            var serverRequest = new CertificateRequest($"C={options.Country}, ST={options.State}, L={options.Locality}, O={options.Organization}, CN={options.CommonName}", serverRsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            X509Certificate2 serverCert;

            if (rootCert != null && rootRsa != null)
            {
                serverCert = serverRequest.Create(rootCert, DateTimeOffset.Now, options.ExpirationDate, new byte[] { 1, 2, 3, 4 });
            }
            else
            {
                serverRequest.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, false));
                serverCert = serverRequest.CreateSelfSigned(DateTimeOffset.Now, options.ExpirationDate);
            }

            var serverPem = ExportCertificateAndPrivateKeyToPem(serverCert, serverRsa);
            File.WriteAllText(Path.Combine(options.Output, $"{options.CommonName}.pem"), serverPem);
        }

        private static string ExportCertificateAndPrivateKeyToPem(X509Certificate2 cert, RSA rsa)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("-----BEGIN CERTIFICATE-----");
            builder.AppendLine(Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
            builder.AppendLine("-----END CERTIFICATE-----");
            var privateKey = rsa.ExportRSAPrivateKey();
            builder.AppendLine("-----BEGIN RSA PRIVATE KEY-----");
            builder.AppendLine(Convert.ToBase64String(privateKey, Base64FormattingOptions.InsertLineBreaks));
            builder.AppendLine("-----END RSA PRIVATE KEY-----");
            return builder.ToString();
        }
    }
}
