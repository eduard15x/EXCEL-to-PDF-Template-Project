using ExcelDataReader;
using System.Text;

namespace ExcelToPDF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // initialize IronPDF package and check license
            var pdfGenerator = new HtmlToPdfGeneratorIronPdf();
            pdfGenerator.Initialize();

            // initialize HtmlTemplate
            var htmlTemplate = new HTMLTemplate();

            var filepath = @"C:\Users\User\Desktop\pdf\Employes.xlsx";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var configuration = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true, // doesn't take the columns titles in the iteration
                        }
                    };
                    var dataSet = reader.AsDataSet(configuration);

                    // checking if the file contains a table, and select the first one for reading data
                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];

                        for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                        {
                            //indexing starts from 0 but it starts with index 1 from excel, because first row is considered the header and is set to ignore the header row
                            //for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                            //{
                                // Console.WriteLine(dataTable.Rows[rowIndex][columnIndex]); // general display cells by cols and rows for both iterations
                                var employeeContractNrAndStartDate = dataTable.Rows[rowIndex][2].ToString();
                                // substract two different values ex: 74/07.02.1999 (document number + join date
                                string[] values = employeeContractNrAndStartDate.Split("/");
                                var documentNumber = values[0];
                                var employeeJoinDate = values[1];
                                var employeeName = dataTable.Rows[rowIndex][3].ToString();
                                var employeeAddress = dataTable.Rows[rowIndex][4].ToString();
                                var employeeCNP = dataTable.Rows[rowIndex][5].ToString();

                                // Generatate HTML with completed data
                                var html = htmlTemplate.BuildTemplate(documentNumber, employeeJoinDate, employeeName, employeeAddress, employeeCNP);
                                // Generate PDF file converted from HTML
                                pdfGenerator.GeneratePDF(rowIndex + 1, employeeName, html);
                                Console.WriteLine(rowIndex + 1);
                            //}
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sheet doesn't exist.");
                    }
                }
            };
        }
    }
}