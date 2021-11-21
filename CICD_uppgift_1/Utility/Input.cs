using System;
using System.Collections.Generic;
using System.Linq;

namespace CICD_uppgift_1
{
    //Class is for handling input from user and checking if it is correct
    public class Input
    {
        //Checks if the string input is a number and between the menu range
        public bool IsMenuInputValid(string input, out int validNumber, out string errormsg, int nrOfMenuChoices)
        {
            errormsg = "no error";
            validNumber = 0;

            if (IsNumberInputValid(input, out validNumber, out errormsg, true))
            {
                if(validNumber <= nrOfMenuChoices)
                {
                    return true;
                }
                else
                {
                    errormsg = $"Menu choice is out for range for menu choices. Use 0 - {nrOfMenuChoices - 1}\n";
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        //Checking if the input string is empty
        public bool IsStringInputValid(string input, out string errormsg)
        {
            errormsg ="no error";

            if(IsInputEmpty(input))
            {
                errormsg = "Input was empty\n";
                return false;
            }
            else
            {
                return true;
            }
        }

        //Checks if the string input is a valid number and if it can be zero or not
        public bool IsNumberInputValid(string input, out int validNumber, out string errormsg, bool canBeZero)
        {
            errormsg = "no error";
            validNumber = 0;

            if(IsInputEmpty(input)) 
            {
                errormsg = "Input was empty\n";
                return false;
            }
            else
            {
                if (IsInputANumber(input, out validNumber))
                {
                    if(!IsNumberNegative(validNumber))
                    {
                        if(canBeZero)
                        {
                            return true;
                        }
                        else
                        {
                            if(validNumber != 0)
                            {
                                return true;
                            }
                            else
                            {
                                errormsg = "You can not input 0.\nIs 0 a Natural Number? No, 0 is NOT a natural number because natural numbers are counting numbers. For counting any number of objects, we start counting from 1 and not from 0.\n";
                                return false;
                            }
                        }
                    }
                    else
                    {
                        errormsg = "No negative numbers here!\n";
                        return false;
                    }
                }
                else
                {
                    errormsg = "Input was not a valid number\n";
                    return false;
                }        
            }
        }

        // Checks if string input is a number
        // Then sends an int with that number
        private bool IsInputANumber(string input, out int number)
        {
            return int.TryParse(input, out number);
        }

        // Checks if string is empty
        private bool IsInputEmpty(string input)
        {
            return input == string.Empty;
        }

        // Checks if the number is negative
        private bool IsNumberNegative(int number)
        {
            return number < 0;
        }
    }
}