# EXCEL-to-PDF-Template-Project
This will be an automatic program that will get data from EXCEL and create PDFs with that based on what you need and how you create a temple.\
You can check this project and template to see how it works.

## NuGet Packages Used
For reading data from Excel files
ExcelDataReader\
ExcelDataReader.DataSet\

For generating PDF from HTML (2 options)
IronPDF (need license if you want to remove watermark from generated PDFs)\
GrapeCity.Documents.Html (need license if you want to remove watermark from generated PDFs)\

To check configuration settings
Microsoft.Extension.Configuration\
Microsoft.Extension.Configuration.Json\

IMPORTANT
-You need to add to or create appsettings.json in your project at the root and add the licensed key
{
  "IronPdf.LicenseKey": "API KEY"
}

You can use this package (IronPDF) but it will add a watermark on the PDFs created.


EXAMPLE\
Red borders shown the dynamic data coming from excel that complete the spaces in the PDF.\
You need to add in HTML template created the variable and the place where you want to modify them.
![image](https://github.com/eduard15x/EXCEL-to-PDF-Template-Project/assets/89576994/8ab76053-1d17-4d14-ad5b-2cc3365cad7c)
![image](https://github.com/eduard15x/EXCEL-to-PDF-Template-Project/assets/89576994/77d7a3a9-5fd2-4355-acdb-880797151d0f)
