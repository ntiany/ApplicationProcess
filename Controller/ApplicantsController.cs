using System.Collections.Generic;
using View;
using Model;


namespace Controller
{
    class ApplicantsController : IController
    {
        private string[] options = { "Find applicant by", "Find an applicant by part of information", "Add a new row", "Update information", "" +
                "Get all applicants", "Delete a row from table" };
        private Communication dataSource = new Communication();
        private string firstName = "first_name";
        private string lastName = "last_name";
        private string tableName = TablesNames.applicants.ToString();
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
                        dataSource.PrintValuesWithFullname(tableName, firstName, lastName);
                        break;
                    case 2:
                        dataSource.PrintValuesLikeWithFullname(tableName, firstName, lastName);
                        break;
                    case 3:
                        dataSource.AddNewRow(tableName);
                        dataSource.PrintAllRecords(tableName, Headers.ToArray());
                        break;
                    case 4:
                        dataSource.UpdateRow(tableName, Headers);
                        break;
                    case 5:
                        dataSource.PrintAllRecords(tableName, Headers.ToArray());
                        break;
                    case 6:
                        dataSource.DeteleRow(tableName);
                        break;
                    case 7:
                        userExit = true;
                        break;
                    default:
                        Display.PrintMessage("There is no such choice");
                        break;
                }
            }
        }
    }
}
