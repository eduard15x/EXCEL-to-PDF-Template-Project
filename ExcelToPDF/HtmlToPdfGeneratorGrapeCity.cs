using GrapeCity.Documents.Html;
using GrapeCity.Documents.Pdf;

namespace ExcelToPDF
{
    public class HtmlToPdfGeneratorGrapeCity
    {
        private static void GeneratePdf(string html, string outputPath)
        {
            // Create a GcPdfDocument instance
            var doc = new GcPdfDocument();
            // Add a new page to the document
            var page = doc.Pages.Add();

            // Take the graphics instance of the page
            var graphics = page.Graphics;

            // Define GcHtmlBrowser instance
            var path = new BrowserFetcher().GetDownloadedPath();

            using (var browser = new GcHtmlBrowser(path))
            {
                // Add the HTML file to it, using DrawHtml method which reads the html content from the invoice file
                var ok = graphics.DrawHtml(browser, html, 72, 72, new HtmlToPdfFormat(false) { MaxPageWidth = 6.5f }, out System.Drawing.SizeF size);

                if (!ok)
                {
                    throw new ApplicationException("HTML to PDF conversion failed;");
                }
            }

            doc.Save(outputPath);
        }
    }
}
