using ExcelDataReader;
using System;
using System.Text;

namespace ExcelToPDF
{
    public class Program
    {
        static void Main(string[] args)
        {
            var filepath = @"C:\Users\User\Desktop\pdf\Employes.xlsx";

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
            {
                using (var reader  = ExcelReaderFactory.CreateReader(stream))
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
                    Console.WriteLine(dataSet.Tables.Count);
                    if (dataSet.Tables.Count > 0)
                    {
                        var dataTable = dataSet.Tables[0];

                        for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                        {
                            Console.WriteLine(rowIndex + 2); // index starts from 0 and first row is the header that is not included in configurations
                            //for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                            //{
                                // Console.WriteLine(dataTable.Rows[rowIndex][columnIndex]); // general display cells by cols and rows in both iterations
                                var employeeContractNrAndStartDate = (string)dataTable.Rows[rowIndex][2];
                                // substract two different values ex: 74/07.02.1999 (document number + birth date
                                string[] values = employeeContractNrAndStartDate.Split("/");
                                var documentNumber = values[0];
                                var employeeBirthDate = values[1];
                                var employeeName = dataTable.Rows[rowIndex][3];
                                var employeeAddress = dataTable.Rows[rowIndex][4];
                                var employeeCNP = dataTable.Rows[rowIndex][5];
                            //}
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sheet doesn't exist.");
                    }
                }

            }
        }
    }
}