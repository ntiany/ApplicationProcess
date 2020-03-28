using System.Collections.Generic;
using System.Text;
using Model;
using View;

namespace Controller
{
    public class Communication
    {
        private IDaoPattern dataSource = new DaoDatabase();

        public List<string> GetHeaders (string tableName)
        {
            return dataSource.GetHeaders(tableName);
        }

        public void PrintAllRecords(string tableName, params string[] columns)
        {
            Display.ClearScreen();
            List<List<string>> data = dataSource.GetColumnValues(tableName, columns);
            Display.PrintTable(data, columns);
        }

        public void PrintFilteredValues(string tableName, List<string> headers)
        {
            Display.ClearScreen();
            Display.PrintMessage("Which column do you want to review?");
            string columnToReview = UserInput.GetInputFromOptions(headers);
            Display.PrintMessage("On which column do you want to put a filter?");
            string conditionColumn = UserInput.GetInputFromOptions(headers);
            Display.PrintMessage("Add a filter value");
            string condition = UserInput.GetInputForSQL();
            List<List<string>> filteredData = dataSource.GetFilteredValues(tableName, condition, conditionColumn, columnToReview);
            Display.PrintMessage("\nYour results: \n");
            Display.PrintTable(filteredData, columnToReview);
        }

        public void PrintColumnsNames(List<string> headers)
        {
            Display.ClearScreen();
            Display.PrintMessage("The columns are:");
            Display.PrintList(headers);
        }

        public void PrintValuesWithFullname(string tableName, string firstName, string lastName)
        {
            Display.ClearScreen();
            List<string> headers = GetHeaders(tableName);
            Display.PrintMessage("On which column do you want to put a filter?");
            string conditionColumn = UserInput.GetInputFromOptions(headers);
            Display.PrintMessage("Add a filter value");
            string condition = UserInput.GetInputForSQL();
            List<List<string>> data = dataSource.GetValuesWithFullname(tableName, firstName, lastName, condition, conditionColumn);
            string[] newHeaders = data[0].ToArray();
            data.RemoveAt(0);
            Display.PrintTable(data, newHeaders);
        }

        public void PrintValuesLikeWithFullname(string tableName, string firstName, string lastName)
        {
            Display.ClearScreen();
            List<string> headers = GetHeaders(tableName);
            Display.PrintMessage("On which column do you want to put a filter?");
            string conditionColumn = UserInput.GetInputFromOptions(headers);
            Display.PrintMessage("Add a filter value");
            string condition = UserInput.GetInputForSQLLike();
            Display.PrintMessage("Which column do you want to view?");
            string column = UserInput.GetInputFromOptions(headers);
            List<List<string>> data = dataSource.GetValuesLikeWithFullname(tableName, firstName, lastName, condition, conditionColumn, column);
            string[] newHeaders = data[0].ToArray();
            data.RemoveAt(0);
            Display.PrintTable(data, newHeaders);
        }

        public void UpdateRow(string tableName, List<string> headers)
        {
            Display.PrintMessage("Which record do you want to update? Provide the id");
            int id = 0;
            while (!dataSource.IsDuplicated(tableName, "id", id))
            {
                id = UserInput.GetNumber();
            }
            Display.PrintMessage("Which column do you want to update?");
            string column = UserInput.GetInputFromOptions(headers);
            Display.PrintMessage("Please provide a new value");
            string value = UserInput.GetInputForSQL();
            dataSource.UpdateRow(tableName, id, column, value);
        }

        public void DeteleRow(string tableName)
        {
            Display.PrintMessage("Which record do you want to delete? Provide the id");
            int id = 0;
            while (!dataSource.IsDuplicated(tableName, "id", id))
            {
                id = UserInput.GetNumber();
            }
            dataSource.DeleteRow(tableName, id);
            Display.PrintMessage("Row deleted");
        }

        public void AddNewRow(string tableName)
        {
            List<string> headers = GetHeaders(tableName);
            List<string> types = dataSource.GetColumnsType(tableName);
            List<string> data = new List <string>{ (dataSource.GetLastID(tableName) + 1).ToString() };

            for (int i = 1; i < headers.Count; i++)
            {
                string userInput;
                Display.PrintMessage($"Add value for {headers[i]}");

                if (types[i] == "int")
                {
                    bool isDuplicated = true;
                    int userInt = 0;
                    while (isDuplicated)
                    {
                        Display.PrintMessage("The number must be unique!");
                        userInt = UserInput.GetNumber();
                        isDuplicated = dataSource.IsDuplicated(tableName, headers[i], userInt);
                    }
                    userInput = $"{userInt}";
                }
                else
                {
                   userInput = UserInput.GetInputForSQL();
                }

                data.Add(userInput);
            }

            dataSource.InsertRecord(tableName, data.ToArray());
            Display.PrintMessage("The record has been added!");
        }

    }
}
