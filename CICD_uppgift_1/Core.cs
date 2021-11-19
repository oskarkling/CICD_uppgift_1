using System;

namespace CICD_uppgift_1
{
    public class Core
    {

        private string errormsg;
        private Input input;

        //Init in constructor
        public Core()
        {
            errormsg = "";
            input = new Input();
        }
        public void Run()
        {

            WelcomeMenu();
        }
        private void WelcomeMenu()
        {
            bool runMenu = true;
            while (runMenu)
            {
                int nrOfMenuChoices = 2;
                Console.WriteLine("1. Login\n0. Exit");
                var stringInput = Console.ReadLine();
                if (input.IsMenuInputValid(stringInput, out int menuChoice, out errormsg, nrOfMenuChoices))
                {
                    switch(menuChoice)
                    {
                        case 1:
                            Login();
                            break;
                        case 0:
                            runMenu = false;
                            Console.Clear();
                            Console.WriteLine("bye bye");
                            Environment.Exit(0);
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(errormsg);
                }   
            }
        }

        private void Login()
        {
            Console.WriteLine("Enter username");
            var inputUsername = Console.ReadLine();
            if(input.IsStringInputValid(inputUsername, out errormsg))
            {
                Console.WriteLine("Enter password");
                var inputPassword = Console.ReadLine();
                if(input.IsStringInputValid(inputPassword, out errormsg))
                {
                    //if username and password is correct:
                        //if user
                            UserMainMenu();
                    // if Admin
                            //AdminMainMenu();
                }
                else
                {
                    Console.WriteLine(errormsg);
                }
            }
            else
            {
                Console.WriteLine(errormsg);
            }
        }

        private void UserMainMenu()
        {
            bool runMenu = true;
            while (runMenu)
            {
                int nrOfMenuChoices = 2;
                Console.WriteLine("");
                var stringInput = Console.ReadLine();
                if (input.IsMenuInputValid(stringInput, out int menuChoice, out errormsg, nrOfMenuChoices))
                {
                    switch(menuChoice)
                    {
                        case 1:
                            
                            break;
                        case 0:
 
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(errormsg);
                }    
            }
        }

    private void AdminMainMenu()
    {
         bool runMenu = true;
            while (runMenu)
            {
                int nrOfMenuChoices = 2;
                Console.WriteLine("");
                var stringInput = Console.ReadLine();
                if (input.IsMenuInputValid(stringInput, out int menuChoice, out errormsg, nrOfMenuChoices))
                {
                    switch(menuChoice)
                    {
                        case 1:

                            break;
                        case 0:

                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(errormsg);
                }
                
            }
        }
    }
}