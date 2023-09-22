using ExcelDataReader;
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
                            for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
                            {
                                Console.WriteLine(dataTable.Rows[rowIndex][columnIndex] + "\t");
                            }
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