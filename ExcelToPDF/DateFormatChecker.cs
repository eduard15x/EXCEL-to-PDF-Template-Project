namespace ExcelToPDF
{
    public class DateFormatChecker
    {
        private const string DateFormat = "dd.MM.yyyy";
        private const string DateFormatMessage = "Va rugam respectati urmatorul format pentru introducerea datei: dd.MM.yyy / zi.luna.an / 20.11.2023";

        public void CheckDateFormatForUserInput(ref string userDateInput, string dateInfo)
        {
            Console.WriteLine(dateInfo);
            Console.WriteLine(DateFormatMessage);

            bool dateFormatIsValid = false;
            userDateInput = Console.ReadLine()!;

            while (!dateFormatIsValid)
            {
                if (string.IsNullOrEmpty(userDateInput) || string.IsNullOrWhiteSpace(userDateInput))
                {
                    Console.WriteLine("Date can not be empty/null/a whitespace");
                    userDateInput = Console.ReadLine()!;
                    continue;
                }

                if (DateTime.TryParseExact(userDateInput, DateFormat, null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    dateFormatIsValid = true;
                }
                else
                {
                    Console.WriteLine("Formatul de date pe care l-ati introdus nu este corect, va rog introduceti data inca o data.");
                    userDateInput = Console.ReadLine()!;
                    continue;
                }
            }
        }

        public bool CheckDateFormatFromExcelTable(string userDateInput)
        {
            if (DateTime.TryParseExact(userDateInput, DateFormat, null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
