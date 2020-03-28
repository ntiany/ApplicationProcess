using System;
using System.Linq;
using System.Collections.Generic;

namespace View
{
    static public class UserInput
    {
        public static int GetNumber()
        {
            int? number = null;
            while (number == null)
            {
                string userInput = Console.ReadLine();
                try
                {
                    number = int.Parse(userInput);
                }
                catch 
                {
                    Display.PrintMessage("Incorrect input type");
                }
            }

            return number.GetValueOrDefault();
        }

        public static string GetInputFromOptions(List<string> options)
        {
            string choice = "";

            while (!options.Any(p => p == choice))
            {
                Display.PrintMessage("Choose from options below:");
                Display.PrintList(options);
                choice = GetStringFromUser();
            }

            return choice;
        }

        public static string GetStringFromUser()
        {
            return Console.ReadLine();
        }

        public static string GetInputForSQL()
        {
            return $"'{GetStringFromUser()}'";
        }

        public static string GetInputForSQLLike()
        {
            return $"'%{GetStringFromUser()}%'";
        }


    }
}
