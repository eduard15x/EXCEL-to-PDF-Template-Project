# EXCEL-to-PDF-Template-Project
This will be an automatic program that will get data from EXCEL and create PDFs with that based on what you need and how you create a temple.
You can check this project and template to see how it works.

NuGet Packages Used
For reading data from Excel files
-ExcelDataReader
-ExcelDataReader.DataSet

For generating PDF from HTML (2 options)
-IronPDF (need license if you want to remove watermark from generated PDFs)
-GrapeCity.Documents.Html (need license if you want to remove watermark from generated PDFs)

To check configuration settings
-Microsoft.Extension.Configuration
-Microsoft.Extension.Configuration.Json

IMPORTANT
-You need to add to or create appsettings.json in your project at the root and add the licensed key
{
  "IronPdf.LicenseKey": "API KEY"
}

You can use this package (IronPDF) but it will add a watermark on the PDFs created.
