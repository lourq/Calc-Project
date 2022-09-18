using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcProject.App
{
    public class Calc
    {
        private readonly Resources Resources;

        public Calc(Resources resources)
        {
            Resources = resources;
        }
        public RomanNumber EvalExpression(String expression)
        {
            var Operations = new String[] { "+", "-" };

            String[] parts = expression.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid expression");
            }
            if (Array.IndexOf(Operations, parts[1]) == -1)
            {
                throw new ArgumentException("Invalid operation");
            }
            RomanNumber rn1 = new(RomanNumber.Parse(parts[0]));
            RomanNumber rn2 = new(RomanNumber.Parse(parts[2]));
            RomanNumber res =
                parts[1] == Operations[0]
                    ? rn1.Add(rn2)
                    : rn1.Sub(rn2);

            return res;                   
        }
        public void Run()
        {
            String? userInput ;
            String? userOper;
            RomanNumber res = null! ;
            Resources resources = new();
            do
            {
                Console.Write("1)uk-UA\n2)en-US\n");
                userInput = Console.ReadLine() ?? "";
                
                resources.MakeSelect(userInput);                

                Console.WriteLine("\n" + resources.GetEnterOperationMessage() + "\n+ -");
                userOper = Console.ReadLine() ?? "";
                
                Console.Write(resources.GetEnterNumberMessage());
                userInput = Console.ReadLine() ?? "";
                
                try
                {
                    res = EvalExpression($"{userInput} {userOper} {Console.ReadLine() ?? ""}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (res is null);

            Console.WriteLine(resources.GetResultMessage(res.Value));
        }

        public void RunOld()
        {

            String? inp;
            RomanNumber? rn1 = null;
            do
            {
                Console.WriteLine(Resources.GetEnterNumberMessage());
                inp = Console.ReadLine();
                try
                {
                    rn1 = new RomanNumber(RomanNumber.Parse(inp!));
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("System error. Program terminated");
                    return;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (rn1 == null);
            Console.WriteLine(rn1);
        }
    }
}