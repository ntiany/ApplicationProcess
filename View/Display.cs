using System;
using Model;
using System.Collections.Generic;

namespace View
{
    public class Display
    {
        public static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void ClearScreen()
        {
            Console.Clear();
        }

        public static void PrintTable(List<List<string>> data, params string[] headers)
        {
            PrintMessage(string.Join(' ', headers));
            foreach (var element in data)
            {
                PrintMessage(string.Join(' ', element.ToArray()));
            }
        }

        public static void PrintList(List<string> list)
        {
            PrintMessage(string.Join(", ", list.ToArray()));
        }

        public static void PrintControllerMenu()
        {
            int tablesNumber = Enum.GetNames(typeof(TablesNames)).Length;

            PrintMessage($"\nPlease choose which table you want to navigate (1 - {tablesNumber + 1}):");

            for (int i = 1; i < tablesNumber+1; i++)
            {
                PrintMessage($"{i}) {(TablesNames)i - 1} table");
                AddExitToMenu(i, tablesNumber);
            }
        }

        public static void PrintControllerMenu(string[] options)
        {
            int optionsNumber = options.Length;

            PrintMessage($"\nPlease choose command (1 - {optionsNumber + 1}):");

            for (int i = 1; i < optionsNumber + 1; i++)
            {
                PrintMessage($"{i}) {options[i-1]}");
                AddExitToMenu(i, optionsNumber);
            }

        }

        private static void AddExitToMenu(int currentIndex, int optionsNumber)
        {
            if (currentIndex == optionsNumber) PrintMessage($"{currentIndex + 1}) Exit");
        }
    }
}
