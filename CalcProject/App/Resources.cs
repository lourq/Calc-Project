using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalcProject.App
{
    public class Resources
    {
        public Resources(){}
        public  String Culture { get; set; } = "uk-UA";

        public  String GetEmptyStringMessage(String? culture = null)
        { 
            if (culture == null) culture = Culture;
            switch (culture)
            {
                case "uk-UA": return "Порожній рядок неприпустимий";
                case "en-US": return "Empty string not allowed";
            }
            throw new Exception("Unupported culture");
        }

        public  String GetInvalidCharMessage(char c, String? culture = null)
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => $"Недозволений символ '{c}'",
                "en-US" => $"Invalid char '{c}'",
                _ => throw new Exception("Unupported culture")
            };
        }
        public  String GetInvalidTypeMessage(int objNumber, String type, String? culture = null)
        {
            culture = culture ?? Culture;
            return culture switch
            {
                "uk-UA" => $"obj{objNumber}: тип '{type}' не підтримується",
                "en-US" => $"obj{objNumber}: type '{type}' unsupported",
                _ => throw new Exception("Unupported culture")
            };
        }
        public  String GetMispalcedNMessage(String? culture = null)
        {
            if (culture == null) culture = Culture;
            return culture switch
            {
                "uk-UA" => "'N' не дозволяється у даному контексті",
                "en-US" => "'N' is not allowed in this context",
                _ => throw new Exception("Unupported culture")
            };
        }
        public  String GetEnterNumberMessage(String? culture = null)
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => "Введiть число: ",
                "en-US" => "Enter number: ",
                _ => throw new Exception("Unsupported culture"),
            };
        }
        // Enter operation
        public  String GetEnterOperationMessage(String? culture = null)
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => "Введiть операцiю: ",
                "en-US" => "Enter operation: ",
                _ => throw new Exception("Unsupported culture"),
            };
        }
        // Result
        public  String GetResultMessage(int res, String? culture = null)
        {
            culture ??= Culture;
            return culture switch
            {
                "uk-UA" => $"Результат: {res}",
                "en-US" => $"Result: {res}",
                _ => throw new Exception("Unsupported culture"),
            };
        }
    }
}
