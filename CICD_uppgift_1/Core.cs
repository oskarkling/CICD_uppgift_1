using System;
using System.Collections.Generic;

namespace CICD_uppgift_1
{
    public class Core
    {
        public List<IUser> userList;
        private string errormsg;
        private Input input;
        private UserManager userManager;

        //Init in constructor
        public Core()
        {
            errormsg = "";
            input = new Input();
            userList = new List<IUser>();
            userManager = new UserManager();
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
                Console.Clear();
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
                    foreach(var user in userList)
                    {                    
                        if(user is User)
                        {
                            if(((User)user).Username == inputUsername)
                            {
                                if(((User)user).Password == inputPassword)
                                {
                                    Console.Clear();
                                    userManager.SetUser(user);
                                    UserMainMenu();
                                }
                                else
                                {
                                    System.Console.WriteLine("wrong password");
                                }
                            }                           
                        }
                        else if(user is Admin)
                        {
                            if(((Admin)user).Username == inputUsername)
                            {
                                if(((Admin)user).Password == inputPassword)
                                {
                                    Console.Clear();
                                    userManager.SetUser(user);
                                    AdminMainMenu();
                                }
                                else
                                {
                                    System.Console.WriteLine("wrong password");
                                }
                            }
                        }
                    }
                    if(userManager.GetUser() == null)
                    {
                        System.Console.WriteLine("user does not exist");
                    }
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
                int nrOfMenuChoices = 3;
                Console.WriteLine("1. Show your salary\n2. Show your role in company\n3. Remove your account\n0. Logout");
                var stringInput = Console.ReadLine();
                if (input.IsMenuInputValid(stringInput, out int menuChoice, out errormsg, nrOfMenuChoices))
                {
                    switch(menuChoice)
                    {
                        case 1:
                            ShowSalary();
                            break;
                        case 2:
                            ShowRole();
                            break;
                        case 0:
                            runMenu = false;
                            userManager = new UserManager();
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

        private void ShowRole()
        {
            Roles role;
            if(userManager.UserIsAdmin)
            {
                role = ((Admin)userManager.GetUser()).Role;
            }
            else
            {
                role = ((User)userManager.GetUser()).Role;
            }
            Console.Clear();
            System.Console.WriteLine("Your role in the company is: " + role);
        }

        private void AdminMainMenu()
    {
         bool runMenu = true;
            while (runMenu)
            {
                int nrOfMenuChoices = 3;
                System.Console.WriteLine("Admin Menu");
                Console.WriteLine("1. Show your salary\n2. Show your role in company\n3. Remove your account\n0. Logout");
                var stringInput = Console.ReadLine();
                if (input.IsMenuInputValid(stringInput, out int menuChoice, out errormsg, nrOfMenuChoices))
                {
                    switch(menuChoice)
                    {
                        case 1:
                            ShowSalary();
                            break;
                        case 2:
                            ShowRole();
                            break;
                        case 0:
                            runMenu = false;
                            userManager = new UserManager();
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

        private void ShowSalary()
        {
            int salary;
            if(userManager.UserIsAdmin)
            {
                salary = ((Admin)userManager.GetUser()).Salary;
            }
            else
            {
                salary = ((User)userManager.GetUser()).Salary;
            }
            Console.Clear();
            System.Console.WriteLine("Your salary is: " + salary);

        }
    }
}