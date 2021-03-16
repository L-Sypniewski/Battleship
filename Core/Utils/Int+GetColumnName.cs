using System;

namespace Core.Utils
{
    public static class GetColumnName
    {
        // Taken from https://stackoverflow.com/a/182924/8297552
        public static string ToColumnName(this int number)
        {
            var dividend = number;
            string columnName = string.Empty;

            while (dividend > 0)
            {
                var modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            } 

            return columnName;
        }
    }
}