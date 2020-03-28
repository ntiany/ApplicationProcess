using System.Collections.Generic;
using Model;
using View;

namespace Controller
{
    class MentorsController : IController
    {
        private Communication dataSource = new Communication();
        private string[] options = { "Show all mentors", "Get filtered mentors", "Get table headers", "Add new column" };
        private string tableName = TablesNames.mentors.ToString();
        private string firstName = "first_name";
        private string lastName = "last_name";
        private List<string> headers = null;

        private List<string> Headers
        {
            get
            {
                if (headers == null) headers = dataSource.GetHeaders(tableName);
                return headers;
            }
        }

        public void StartMenu()
        {
            bool userExit = false;

            while (!userExit)
            {
                Display.PrintControllerMenu(options);
                int userChoice = UserInput.GetNumber();

                switch (userChoice)
                {
                    case 1:
                        dataSource.PrintAllRecords(tableName, firstName, lastName);
                        break;
                    case 2:
                        dataSource.PrintFilteredValues(tableName, Headers);
                        break;
                    case 3:
                        dataSource.PrintColumnsNames(Headers);
                        break;
                    case 4:
                        break;
                    default:
                        Display.PrintMessage("There is no such choice!");
                        break;
                }
            }
        }
    }
}
