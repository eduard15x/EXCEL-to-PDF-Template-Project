using Microsoft.Extensions.Configuration;

namespace ExcelToPDF
{
    public class HtmlToPdfGeneratorIronPdf
    {
        public void Initialize()
        {
            // Build the configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            // Get the value from appsettings.json
            string apiKey = configuration["IronPdf.LicenseKey"];

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new Exception("License doesn't exist!");
            }

            bool result = License.IsValidLicense(apiKey);
            bool isLic = License.IsLicensed;

            if (!result || !isLic)
            {
                throw new Exception("License is expired!");
            }
        }

        public void GeneratePDF(int pdfFileNumber, string pdfFileName, string htmlTemplate)
        {
            ChromePdfRenderer renderer = new ChromePdfRenderer();
            PdfDocument pdf = renderer.RenderHtmlAsPdf(htmlTemplate);
            pdf.SaveAs($"{pdfFileNumber}_{pdfFileName}.pdf");
        }
    }
}
