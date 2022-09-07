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
        private static Dictionary<char , int> digits = new ()
        {
            {'I',1},                                                                                                                           
            {'V',5},                                                                                                                           
            {'X',10},                                                                                                                          
            {'L',50},                                                                                                                          
            {'C',100},                                                                                                                         
            {'D',500},                                                                                                                         
            {'M',1000}  
        };

        // Parse str to number
        public static int Parse(String str)
        {
            // validation input data
            if (str == null)
                throw new ArgumentNullException();
        
            if (str == string.Empty)
                throw new ArgumentException("Empty string not allowed");
            
            #region Parse to Int

            if (str.Length <= 1)
            {
                return digits[Char.Parse(str)];
            }
            
            var sum = 0;

            for (int i = 0; i < str.Length - 1; i++)
            {
                var number = digits[str[i]];
                var nextNumber = digits[str[i + 1]];

                if (number > nextNumber || number == nextNumber)
                {
                    sum += number;
                }
                else if (number < nextNumber)
                {
                    sum -= number;
                }
            }

            sum += digits[str[str.Length - 1]];
            
            #endregion
            
            return sum;
        }
    }
}
