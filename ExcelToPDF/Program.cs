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

            // Dates details about document
            var startDateAtNewOffice = string.Empty;
            var documentCreatedDate = string.Empty;
            // excel table info
            var documentNumber = string.Empty;
            var employeeJoinDate = string.Empty;
            var contractNumber = string.Empty;
            var employeeName = string.Empty;
            var employeeAddress = string.Empty;
            var employeeCNP = string.Empty;
            // user filepath and folder locations
            var errorMessages = string.Empty;
            var userInputExcelFilepath = string.Empty;
            var userInputOutputDirectoryForPDF = string.Empty;
            var userInputErrorsTextFile = string.Empty;

            // Starting Program
            // Ask user for excel filepath with data, where to render PDFs and where to create error file txt\
            Console.WriteLine("Va rugam introduceti filepath-ul pentru fisierul excel. (locatia)");
            Console.WriteLine(@"EX:  C:\Users\User\Desktop\pdf\Employes.xlsx");

            while (!File.Exists(userInputExcelFilepath) || string.IsNullOrEmpty(userInputExcelFilepath))
            {
                userInputExcelFilepath = Console.ReadLine();
                if (string.IsNullOrEmpty(userInputExcelFilepath))
                {
                    Console.WriteLine("Introduceti o valoare.");
                    continue;
                }

                if (!File.Exists(userInputExcelFilepath))
                    Console.WriteLine($"Fisierul '{userInputExcelFilepath}' nu exista, va rugam reincercati.");
            }

            Console.WriteLine("\nVa rugam introduceti directory-ul/folderul unde doriti sa fie salvate fisierele PDF care se vor crea.");
            Console.WriteLine(@"EX: \t C:\Users\User\Desktop\Fisiere");
            while (!Directory.Exists(userInputOutputDirectoryForPDF) || string.IsNullOrEmpty(userInputOutputDirectoryForPDF))
            {
                userInputOutputDirectoryForPDF = Console.ReadLine();
                if (string.IsNullOrEmpty(userInputOutputDirectoryForPDF))
                {
                    Console.WriteLine("Introduceti o valoare.");
                    continue;
                }

                if (!Directory.Exists(userInputOutputDirectoryForPDF))
                Console.WriteLine($"Folderul '{userInputOutputDirectoryForPDF}' nu exista, va rugam reincercati.");
            }

            Console.WriteLine("\nVa rugam introduceti directory-ul/folderul unde doriti sa salvati fisierul care va afisa erorile.");
            Console.WriteLine(@"EX: \t C:\Users\User\Desktop\Fisiere");
            while (!Directory.Exists(userInputErrorsTextFile) || string.IsNullOrEmpty(userInputErrorsTextFile))
            {
                userInputErrorsTextFile = Console.ReadLine();
                if (string.IsNullOrEmpty(userInputErrorsTextFile))
                {
                    Console.WriteLine("Introduceti o valoare.");
                    continue;
                }

                if (!Directory.Exists(userInputErrorsTextFile))
                {
                    Console.WriteLine($"Folderul '{userInputErrorsTextFile}' nu exista, va rugam reincercati.");
                    continue;
                }
            }

            // Add file to the directory
            userInputErrorsTextFile += @"\Errors.txt";

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            using (var stream = File.Open(userInputExcelFilepath, FileMode.Open, FileAccess.Read))
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
                        "\nIntroduceti data de incepere la noul sediu ITP."
                    );

                    dateFormatChecker.CheckDateFormatForUserInput(
                        ref documentCreatedDate!,
                        "\nIntroduceti data la care angajatul va primi acest formulat pentru consimtamant/semnat."
                    );

                    Console.WriteLine("Datele au fost adaugate cu succes fisierelor PDF.");
                    Console.WriteLine("Va rugam asteptati...\n\n");

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

                            if (string.IsNullOrEmpty(employeeAddress) || string.IsNullOrWhiteSpace(employeeAddress))
                            {
                                var error = $"Linia {rowIndex + 2} are celula goala/empty pentru coloana E.\n\n";

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
                            pdfGenerator.GeneratePDF(rowIndex + 1, employeeName!.ToUpper(), html, userInputOutputDirectoryForPDF);
                        }

                        Console.WriteLine($"Locatia fisierului excel din care au fost luate datele este:\t {userInputExcelFilepath}\n");
                        Console.WriteLine($"Folderul in care s-au creat documentele PDF se gaseste in {userInputOutputDirectoryForPDF}\n");
                        Console.WriteLine($"S-au creat in total {dataTable.Rows.Count} fisiere PDF.\n");
                        fileCreator.CreateErrorFile(userInputErrorsTextFile, errorMessages);
                    }
                }
            };
        }
    }
}