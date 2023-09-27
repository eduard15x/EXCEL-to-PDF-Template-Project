using System.Text.RegularExpressions;

namespace ExcelToPDF
{
    public class TableInfoRegexPattern
    {
        private const string DocumentNumberPattern = @"^\d{1,3}$";
        private const string ContractNumberPattern = @"^\d+$";
        private const string EmployeeNamePattern = @"^[A-Za-z\s\-]+$";
        private const string EmployeeIdPattern = @"^\d{13}$";
        // private const string EmployeeAddressPattern = @"^STR\.\s[A-Za-z\s\.,0-9-]+,\s(?:[Nn][Rr]\.\s[A-Za-z0-9\s\.-]+,)?\s[A-Za-z\s\.,]+,\s[A-Za-z\s]+$";

        public bool CheckDocumentNumber(string documentNumber)
        {
            if (string.IsNullOrWhiteSpace(documentNumber) || string.IsNullOrEmpty(documentNumber))
            {
                return false;
            }

            return Regex.IsMatch(documentNumber, DocumentNumberPattern);
        }

        public bool CheckContractNumber(string contractNumber)
        {
            if (string.IsNullOrWhiteSpace(contractNumber) || string.IsNullOrEmpty(contractNumber))
            {
                return false;
            }

            return Regex.IsMatch(contractNumber, ContractNumberPattern);
        }

        public bool CheckEmployeeName(string employeeName)
        {
            if (string.IsNullOrWhiteSpace(employeeName) || string.IsNullOrEmpty(employeeName))
            {
                return false;
            }

            return Regex.IsMatch(employeeName, EmployeeNamePattern);
        }

        public bool EmployeeId(string employeeId)
        {
            if (string.IsNullOrWhiteSpace(employeeId) || string.IsNullOrEmpty(employeeId))
            {
                return false;
            }

            return Regex.IsMatch(employeeId, EmployeeIdPattern);
        }

        //public bool CheckEmployeeAddress(string employeeAddress)
        //{
        //    if (string.IsNullOrWhiteSpace(employeeAddress) || string.IsNullOrEmpty(employeeAddress))
        //    {
        //        return false;
        //    }

        //    return Regex.IsMatch(employeeAddress, EmployeeAddressPattern);
        //}
    }
}
