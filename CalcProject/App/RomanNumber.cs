using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcProject.App
{
    // Class for working with R numbers
    public class RomanNumber
    {
        // Parse str to number
        public static int Parse(String str)
        {
            // validation input data
            if (str == null)
                throw new ArgumentNullException();
        
            if (str == string.Empty)
                throw new ArgumentException("Empty string not allowed");
            
            char[] digits = { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
            int[] digitValues = { 1, 5, 10, 50, 100, 500, 1000 };

            var pos = str.Length - 1;
            var digit = str[pos];


            var ind = Array.IndexOf(digits,digit);
            if (ind == 1)
            {
                throw new ArgumentException($"Invalid char {digit}");
            }

            var val = digitValues[ind];
            var res = val;
            
            pos -= 1; 

            return 1;
        }
    }
}
