using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CalcProject.App
{
    // Class for working with R numbers
    public class RomanNumber
    {
        #region Data
        
        private static readonly Dictionary<char , int> Digits = new ()
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
            this.Value = number;
        }

        #endregion

        #region method Add
        
        public static RomanNumber Add(int num1, int num2)
        {
            return new RomanNumber(num1 + num2);
        }

        public static RomanNumber Add(RomanNumber num1, int num2)
        {
            if (num1 is null)
                throw new ArgumentException("Empty obj");
            return new RomanNumber(num1.Value + num2);
        }

        public static RomanNumber Add(string num1, string num2)
        {
            return new RomanNumber(Parse(num1) + RomanNumber.Parse(num2));
        }

        public static RomanNumber Add(RomanNumber num1, string num2)
        {
            if (num1 is null)
                throw new ArgumentException("Empty obj");
            return new RomanNumber(num1.Value + Parse(num2));
        }

        public static RomanNumber Add(string num1, RomanNumber num2)
        {
            if (num2 is null)
                throw new ArgumentException("Empty obj");
            return new RomanNumber(Parse(num1) + num2.Value);
        }
        public static RomanNumber Add(RomanNumber num1, RomanNumber num2)
        {
            if (num1  is null || num2 is null)
            {
                throw new ArgumentException("Empty obj");
            }
            return new RomanNumber(num1.Value + num2.Value);
        }

        public static RomanNumber Add(int num1, string num2)
        {
            return new RomanNumber(num1 + Parse(num2));
        }
        
        public RomanNumber Add(int num)
        {
            return new RomanNumber(this.Value + num);
        }
    
        public RomanNumber Add(String num)
        {
            return new(this.Value + Parse(num));
        }
        
        public RomanNumber Add(RomanNumber num)
        {
            if (num is null)
                throw new ArgumentException("Empty obj");
            
            return new(this.Value + num.Value);
        }
        
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

            #region var

            var sum = 0;
            bool negNum = false;

            #endregion
            
            if (str == null)
                throw new ArgumentNullException();
        
            if (str == string.Empty)
                throw new ArgumentException("Empty string not allowed");
            
            if (str.StartsWith('-') && str.Length > 1)
            {
                negNum = true;
                str = str[1..];
            }
            
            foreach (var inputSymbol in str)
            {
                if (!Digits.ContainsKey(inputSymbol))
                    throw new ArgumentException($"Invalid input data: {inputSymbol}");
            }
            
            #region Parse to Int

            if (str.Length <= 1)
            {
                return Digits[Char.Parse(str)];
            }
            

            for (int i = 0; i < str.Length - 1; i++)
            {
                var number = Digits[str[i]];
                var nextNumber = Digits[str[i + 1]];

                if (number > nextNumber || number == nextNumber)
                {
                    sum += number;
                }
                else if (number < nextNumber)
                {
                    sum -= number;
                }
            }

            sum += Digits[str[^1]];
            
            #endregion

            return negNum ? -sum : sum;
        }
        #endregion
    }
}
