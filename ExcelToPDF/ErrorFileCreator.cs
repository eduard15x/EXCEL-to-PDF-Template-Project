namespace ExcelToPDF
{
    public class ErrorFileCreator
    {
        public void CreateErrorFile(string pathToCreateFile, string errorsToWrite)
        {
            try
            {
                // Create or overwrite the text file
                using (StreamWriter writer = new StreamWriter(pathToCreateFile))
                {
                    // Write the text to the file
                    writer.WriteLine(errorsToWrite);
                }

                Console.WriteLine($"Fisierul text care contine toate erorile a fost creat in locatia {pathToCreateFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
