using System;
using View;

namespace Controller
{
    public class MainController : IController
    {
        private IController mentorsController = new MentorsController();
        private IController applicantsController = new ApplicantsController();

        public void StartMenu()
        {
            bool userExit = false;

            while (!userExit)
            {
                Display.PrintControllerMenu();
                int choice = UserInput.GetNumber();

                switch (choice)
                {
                    case 1:
                        mentorsController.StartMenu();
                        break;
                    case 2:
                        applicantsController.StartMenu();
                        break;
                    case 3:
                        Display.PrintMessage("Thank you for using Application Process Program!");
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
