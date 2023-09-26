using ExcelDataReader;
using System.Text;

namespace ExcelToPDF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pdfGenerator = new HtmlToPdfGeneratorIronPdf();
            var dateFormatChecker = new DateFormatChecker();
            var htmlTemplate = new HTMLTemplate();
            var patternFormat = new TableInfoRegexPattern();
            var fileCreator = new ErrorFileCreator();
            pdfGenerator.Initialize();


            var filepath = @"C:\Users\User\Desktop\pdf\Employes.xlsx";
            var filepathErrors = @"C:\Users\User\Desktop\pdf\Errors.txt";
            var errorMessages = "";

            // Dates details about document
            var startDateAtNewOffice = "";
            var documentCreatedDate = "";

            // excel table info
            var documentNumber = "";
            var employeeJoinDate = "";
            var contractNumber = "";
            var employeeName = "";
            var employeeAddress = "";
            var employeeCNP = "";




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


                    dateFormatChecker.CheckDateFormatForUserInput(
                        ref startDateAtNewOffice!,
                        "Introduceti data de incepere la noul sediu ITP."
                    );

                    dateFormatChecker.CheckDateFormatForUserInput(
                        ref documentCreatedDate!,
                        "Introduceti data la care angajatul va primi acest formulat pentru consimtamant/semnat."
                    );

                    Console.WriteLine("Datele au fost adaugate cu succes fisierelor PDF.");
                    Console.WriteLine("Va rugam asteptati...");

                    // checking if the file contains a table, and select the first one for reading data
                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];

                        for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                        {
                            documentNumber = dataTable.Rows[rowIndex][0].ToString();
                            employeeJoinDate = dataTable.Rows[rowIndex][1].ToString();
                            contractNumber = dataTable.Rows[rowIndex][2].ToString();
                            employeeName = dataTable.Rows[rowIndex][3].ToString();
                            employeeAddress = dataTable.Rows[rowIndex][4].ToString();
                            employeeCNP = dataTable.Rows[rowIndex][5].ToString();


                            if (!patternFormat.CheckDocumentNumber(documentNumber!))
                            {
                                var error = $"" +
                                    $"Linia {rowIndex + 2} are formatul gresit pentru coloana A." +
                                    "\nACT ADITIONAL NR - Aceasta celula este goala sau nu respecta formatul." +
                                    "\nnTrebuie sa contina doar cifre, fara alte caractere.\n\n";

                                errorMessages += error;
                                documentNumber = ".......";
                            }

                            if (!dateFormatChecker.CheckDateFormatFromExcelTable(employeeJoinDate!))
                            {
                                var error = $"" +
                                    $"Linia {rowIndex + 2} are formatul gresit pentru coloana B." +
                                    "\nDIN DATA - Aceasta celula este goala sau nu respecta formatul." +
                                    "\nnTrebuie sa fie sub forma de data in urmatorul format: dd.MM.yyy / zi.luna.an  .\n\n";

                                errorMessages += error;
                                documentNumber = ".......";
                            }

                            if (!patternFormat.CheckContractNumber(contractNumber!))
                            {
                                var error = $"" +
                                    $"Linia {rowIndex + 2} are formatul gresit pentru coloana C." +
                                    "\nNR CONTRACT - Aceasta celula este goala sau nu respecta formatul." +
                                    "\nTrebuie sa contina doar cifre, fara litele/simboluri/spatii goale.\n\n";

                                errorMessages += error;
                                contractNumber = "........................";
                            }

                            if (!patternFormat.CheckEmployeeName(employeeName!))
                            {
                                var error = $"" +
                                    $"Linia {rowIndex + 2} are formatul gresit pentru coloana D." +
                                    "\nNUME - Aceasta celula este goala sau nu respecta formatul." +
                                    "\nTrebuie sa contina doar litere / - / .  , fara alte simboluri sau numere." +
                                    "\nEX: PRECUP EDUARD - IONUT\n\n";

                                errorMessages += error;
                                employeeName = "...........................................";
                            }

                            if (!patternFormat.CheckEmployeeAddress(employeeAddress!))
                            {
                                var error = "" +
                                    $"Linia {rowIndex + 2} are formatul gresit pentru coloana E." +
                                    "\nADRESA - Aceasta celula este goala sau nu respecta formatul." +
                                    "\nTrebuie sa contina doar litere / numere / - / . / ,  , fara alte simboluri." +
                                    "\nEX: STR. DOAMNA STANCA, NR. 9, BL. 2, ET. 2, AP. 11, SELIMBAR, SIBIU\n\n";

                                errorMessages += error;
                                employeeAddress = "...........................................................";
                            }

                            if (!patternFormat.EmployeeId(employeeCNP!))
                            {
                                var error = "" +
                                    $"Linia {rowIndex + 2} are formatul gresit pentru coloana F." +
                                    "\nCNP - Aceasta celula este goala sau nu respecta formatul." +
                                    "\nTrebuie sa contina un numar format din 13 cifre, fara litere/simboluri/spatii goale.\n\n";

                                errorMessages += error;
                                employeeCNP = "................................";
                            }

                            // Generatate HTML with completed data
                            var html = htmlTemplate.BuildTemplate(
                                documentNumber!, 
                                employeeJoinDate!, 
                                contractNumber!, 
                                employeeName!.ToUpper(), 
                                employeeAddress!, 
                                employeeCNP!,
                                startDateAtNewOffice,
                                documentCreatedDate
                            );
                            // Generate PDF file converted from HTML template
                            pdfGenerator.GeneratePDF(rowIndex + 1, employeeName!.ToUpper(), html);
                        }

                        fileCreator.CreateErrorFile(filepathErrors, errorMessages);
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