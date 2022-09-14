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
        #region Data
        
        private static readonly Dictionary<char , int> digits = new ()
        {
            {'I',1},                                                                                                                           
            {'V',5},                                                                                                                           
            {'X',10},                                                                                                                          
            {'L',50},                                                                                                                          
            {'C',100},                                                                                                                         
            {'D',500},                                                                                                                         
            {'M',1000}  
        };

        public int Value { get; set; }

        #endregion

        #region Сonstructors

        public RomanNumber(int number = 0)
        {
            Value = number;
        }

        #endregion

        #region method Add


        #endregion
        
        #region method toString

        public override string ToString()
        {
            if (Value == 0)
            {
                return "N";
            }

            int n = this.Value < 0 ? -this.Value : this.Value;
            String res = this.Value < 0 ? "-" : "";
            String[] parts = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] values = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };


            for (int j = 0; j <= parts.Length - 1; j++)
            {
                while (n >= values[j])
                {
                    n -= values[j];
                    res += parts[j];

                }
            }

            return res;
        }
        
        #endregion

        #region Parse String to int method
        public static int Parse(String str)
        {
            if (str == null)
                throw new ArgumentNullException();
        
            if (str == string.Empty)
                throw new ArgumentException("Empty string not allowed");
            
            foreach (var inputSymbol in str)
            {
                if (!digits.ContainsKey(inputSymbol))
                    throw new ArgumentException($"Invalid input data: {inputSymbol}");
            }

            
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
        #endregion
    }
}
