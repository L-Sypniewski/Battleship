using System;

namespace Core.Utils
{
    public static class ColumnNames
    {
        // Taken from https://stackoverflow.com/a/182924/8297552
        public static string ToColumnName(this int number)
        {
            var dividend = number;
            string columnName = string.Empty;

            while (dividend > 0)
            {
                var modulo = ( dividend - 1 ) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = ( dividend - modulo ) / 26;
            }

            return columnName;
        }


        // Taken from https://stackoverflow.com/a/848184/8297552
        public static int ToColumnIndex(this string name)
        {
            var number = 0;
            var pow = 1;
            for (var i = name.Length - 1; i >= 0; i--)
            {
                number += ( name.ToUpper()[i] - 'A' + 1 ) * pow;
                pow *= 26;
            }

            return number;
        }
    }
}