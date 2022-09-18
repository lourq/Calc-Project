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
    public record class RomanNumber
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
        
        public static Resources Resources { get; set; } = null!;
        
        const string ZERO_DIGIT = "N";

        #endregion

        #region Сonstructors

        public RomanNumber(int number = 0)
        {
            this.Value = number;
        }

        #endregion

        #region method Add
        
        public static RomanNumber Add(object obj1, object obj2)
        {
            var rns = new RomanNumber[] { null!, null! };
            var pars = new object[] { obj1, obj2 };

            for (int i = 0; i < 2; i++)
            {
                if (pars[i] is null) throw new ArgumentException(Resources.GetInvalidTypeMessage(i+1,pars[i].GetType().Name));

                if (pars[i] is int val) rns[i] = new RomanNumber(val);
                else if (pars[i] is String str) rns[i] = new RomanNumber(Parse(str));
                else if (pars[i] is RomanNumber rn) rns[i] = rn;
                else throw new ArgumentException(Resources.GetInvalidTypeMessage(i+1,pars[i].GetType().Name));
            }            

            return rns[0].Add(rns[1]);
        }
        
        public static RomanNumber Add(int num1, int num2)
        {
            return new RomanNumber(num1 + num2);
        }

        public static RomanNumber Add(RomanNumber num1, int num2)
        {
            if (num1 is null)
                throw new ArgumentNullException();
            return new RomanNumber(num1.Value + num2);
        }

        public static RomanNumber Add(string num1, string num2)
        {
            return new RomanNumber(Parse(num1) + RomanNumber.Parse(num2));
        }

        public static RomanNumber Add(RomanNumber num1, string num2)
        {
            if (num1 is null)
                throw new ArgumentNullException();
            return new RomanNumber(num1.Value + Parse(num2));
        }

        public static RomanNumber Add(string num1, RomanNumber num2)
        {
            if (num2 is null)
                throw new ArgumentNullException();
            return new RomanNumber(Parse(num1) + num2.Value);
        }
        public static RomanNumber Add(RomanNumber num1, RomanNumber num2)
        {
            if (num1  is null || num2 is null)
            {
                throw new ArgumentNullException();
            }
            return new RomanNumber(num1.Value + num2.Value);
        }

        public static RomanNumber Add(int num1, string num2)
        {
            return new RomanNumber(num1 + Parse(num2));
        }
        
        public RomanNumber Add(int num)
        {
            return this with { Value = this.Value + num };
        }
    
        public RomanNumber Add(String num)
        {
            return new(this.Value + RomanNumber.Parse(num));
        }
        
        public RomanNumber Add(RomanNumber num)
        {
            if (num is null)
                throw new ArgumentNullException();
            
            return new(this.Value + num.Value);
        }
        
        #endregion
        
        #region method toString

        public override string ToString()
        {
            if (Value == 0) 
                return ZERO_DIGIT;


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

        #region Parse String to Int method
        public static int Parse(String str) 
        {

            #region var

            int sum = 0;
            bool negNum = false;

            #endregion
            
            if (str == ZERO_DIGIT)
                return 0;
            
            if (str == null)
                throw new ArgumentNullException();

            if (str == string.Empty)
                throw new ArgumentException(Resources.GetEmptyStringMessage());
            
            if (str.StartsWith('-') && str.Length > 1)
            {
                negNum = true;
                str = str[1..];
            }
            
            foreach (var inputSymbol in str)
            {
                if (!Digits.ContainsKey(inputSymbol))
                    throw new ArgumentException(Resources.GetInvalidCharMessage(inputSymbol));
            }
            
            #region Parse to Int

            if (str.Length == 1)
            {
                return negNum ? -Digits[Char.Parse(str)] : Digits[Char.Parse(str)];
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
