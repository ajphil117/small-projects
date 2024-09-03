using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class CLI_Inputs
    {
        /** Constants **/
        static int[] CATEGORIES = { 0, 1, 2, 3, 4, 5, 6, 7, 8};

        /** Variables **/
        public static Member currentUser;


        /** Methods **/
                
        public static void Exit()
        {
            // Close console
            Environment.Exit(0);            
        }
        
        public static void Login(string menuBranch)
        {
            // Constants
            // Staff
            string USERNAME = "Username (staff): ";

            // Member
            string FIRST_NAME = "First name: ";
            string LAST_NAME = "Last name: ";

            string PASSWORD_STAFF = "Password (today123): ";
            string PASSWORD = "Password: ";
            string LOGIN_FOOTER = "==================================";

            // Variables
            string username;
            string firstName;
            string lastName;
            string password;
            bool retry;

            // Reset currentUser
            CLI_Member.currentUser = null;

            ToolLibrarySystem members = new ToolLibrarySystem();
            iMember[] memberArray;

            // Instance of CLI_Menus class
            CLI_Menus menus = new CLI_Menus();

            // Check type of login required
            if (menuBranch == "staff")
            {
                // Get login details
                Console.Write(USERNAME);
                username = Console.ReadLine();
                Console.Write(PASSWORD_STAFF);
                password = Console.ReadLine();

                // Check vailidity
                if (username == "staff" && password == "today123")
                {
                    // Login valid, proceed
                    Console.WriteLine(LOGIN_FOOTER);
                    Console.WriteLine();
                    menus.Staff_Menu();
                    return;
                }
                else
                {
                    Console.WriteLine("\n====LOGIN INVALID====");
                    retry = Retry_Login();

                    if (retry)
                    {
                        // Retry login
                        Console.WriteLine();
                        menus.Staff_Login();
                        return;
                    }
                    else
                    {
                        // Do not retry, go back to main menu
                        Console.WriteLine();
                        menus.Main_Menu();
                        return;
                    }
                }

            }
            else if (menuBranch == "member")
            {
                // Get login details
                Console.Write(FIRST_NAME);
                firstName = Console.ReadLine();
                Console.Write(LAST_NAME);
                lastName = Console.ReadLine();
                Console.Write(PASSWORD);
                password = Console.ReadLine();

                // Check if member exists
                memberArray = ToolLibrarySystem.memberLibrary.toArray();
                foreach (Member member in memberArray)
                {
                    if (member.FirstName.ToUpper() == firstName.ToUpper() && member.LastName.ToUpper() == lastName.ToUpper()
                        && member.PIN == password)
                    {
                        // Save login info
                        CLI_Member.currentUser = member;

                        // Login valid, proceed
                        Console.WriteLine(LOGIN_FOOTER);
                        Console.WriteLine();
                        menus.Member_Menu();
                        return;
                    }
                }

                // Member was not found, login invalid
                Console.WriteLine("\n====LOGIN INVALID====");
                retry = Retry_Login();

                if (retry)
                {
                    // Retry login
                    Console.WriteLine();
                    menus.Member_Login();
                    return;
                }
                else
                {
                    // Do not retry, go back to main menu
                    Console.WriteLine();
                    menus.Main_Menu();
                    return;
                }
            }
        }
        
        public static bool Retry_Login()
        {
            // Variables
            string retry;
            bool retryBool = false;
            bool valid = false;            

            // Check if want to retry
            Console.Write("Would you like to retry? (Y/N): ");
            retry = Read_Key_Press();

            while (!valid)
            {
                if (retry.ToUpper() == "Y")
                {
                    retryBool = true;
                    valid = true;                    
                }
                else if (retry.ToUpper() == "N")
                {
                    retryBool = false;
                    valid = true;
                }
                else
                {
                    Console.Write("Please enter a valid input. (Y/N): ");
                    retry = Read_Key_Press();
                }
            }

            return retryBool;            
        }

        public static string Return_Key_Press(string textInput, int optionCount)
        {
            // Constants
            string SELECTION_TEXT = "Please make a selection (1-{0}, or 0 to return to main menu): ";
            string VALIDITY_CATCH = "== I'm sorry, your input was not recognised. Please enter a valid input. ==";

            // Variables
            bool inputValid = false;

            while (!inputValid)
            {
                // Read users keypress
                textInput = CLI_Inputs.Read_Key_Press();

                // Check key press validity
                inputValid = CLI_Inputs.Check_Key_Press(textInput, optionCount);

                if (!inputValid)
                {
                    // Reset textInput
                    textInput = "";

                    // Ask to enter valid option
                    Console.WriteLine();
                    Console.WriteLine(VALIDITY_CATCH);
                    Console.WriteLine();
                    Console.Write(SELECTION_TEXT, optionCount);
                }
            }

            return textInput;
        }

        public static string Read_Key_Press()
        {
            // Variables
            string textInput;
            ConsoleKeyInfo readKeyResult = Console.ReadKey(true);

            // Read input into textInput
            textInput = (readKeyResult.KeyChar).ToString();

            // Print key pressed to console
            Console.WriteLine(textInput.ToUpper());

            return textInput;
        }

        public static bool Check_Key_Press(string textInput, int numOptions)
        {
            // Variables
            bool valid = false;
            
            // Check validity
            for (int counter = 0; counter <= numOptions; counter++)
            {
                if (textInput == counter.ToString())
                {
                    valid = true;
                }
            }

            return valid;
        }


        // https://stackoverflow.com/questions/7712137/array-containing-methods
        public static void Select_Menu(string textInput, int numOptions, Action[] methodArray)
        {
            // Variables
            Action method;
            
            for (int counter = 0; counter <= numOptions; counter++)
            {
                if (textInput == counter.ToString())
                {
                    Console.WriteLine();
                    method = methodArray[counter];
                    method.Invoke();
                }
            }
        }

        public static void Select_Menu_Category(string textInput, int numOptions, string[] menuText)
        {
            // Variables
            string previousMenu = menuText[0];
            int optionCountType;

            // Instance of CLI_Menus class for staff/member menus
            CLI_Menus menus = new CLI_Menus();
            
            for (int counter = 1; counter <= numOptions; counter++)
            {
                if (textInput == "0")
                {
                    if (previousMenu == "staff")
                    {
                        // Return to staff menu
                        Console.WriteLine();
                        menus.Staff_Menu();
                        return;
                    }
                    else
                    {
                        // Return to member menu
                        Console.WriteLine();
                        menus.Member_Menu();
                        return;
                    }                    
                }
                else if (textInput == counter.ToString())
                {
                    // Get optionCount
                    if (counter - 1 == 0 || counter - 1 == 2 || counter - 1 == 6 || counter - 1 == 7)
                    {
                        optionCountType = 5;
                    }
                    else
                    {
                        optionCountType = 6;
                    }

                    // Display tool type menu selected
                    Console.WriteLine();
                    menus.Make_Menu(menuText[counter], optionCountType, previousMenu);                    
                }
            }
        }

        public static void Select_Menu_Type(string textInput, string previousMenu, int category, int type, string displayType)
        {
            // Constants
            string SELECTION_TEXT = "\nPlease make a selection (1-{0}, or 0 to return to " + previousMenu + " menu): ";
            string HEADER = "===============================Update Tool Pieces===============================";
            string FOOTER = "=============================================================================";
            string QUANTITY = "Quantity: ";
            string VALIDITY_CATCH = "\n== I'm sorry, your input was not recognised. Please enter a valid input. ==\n";
            string RETURN = "Press any key to return to previous menu...";

            // Variables
            Tool[] myTools;
            Tool toolSelected = null;
            int toolNum;
            string myTextInput = "";
            bool valid = false;
            string quantityString;
            int quantity = -1;

            // Instances of classes
            CLI_Menus menus = new CLI_Menus();
            ToolLibrarySystem library = new ToolLibrarySystem();

            // Get the number of tools displayed
            myTools = ToolLibrarySystem.toolLibrary.toolCollection[category][type];
            toolNum = myTools.Length;

            // Display tools
            Console.WriteLine();
            library.displayTools(CLI_Menus.type.Value);

            // If this menu is only being used to display, end early
            if (displayType == "display")
            {
                return;
            }

            // Make tool selection
            Console.Write(SELECTION_TEXT, toolNum - 1);
            myTextInput = CLI_Inputs.Return_Key_Press(myTextInput, toolNum - 1);

            // Choose tool from input
            for (int counter = 0; counter < toolNum; counter++)
            {
                if (myTextInput == "0")
                {
                    if (previousMenu == "staff")
                    {
                        // Go back to staff menu
                        Console.WriteLine();
                        menus.Staff_Menu();
                        return;
                    }
                    else
                    {
                        // Go back to member menu
                        Console.WriteLine();
                        menus.Member_Menu();
                        return;
                    }
                }
                else if (myTextInput != "0" && myTextInput == counter.ToString())
                {
                    Console.WriteLine();
                    toolSelected = myTools[counter - 1];
                }
            }

            // Display tool info and get new quantity
            Console.WriteLine(HEADER);
            Console.WriteLine(toolSelected.ToolInfo() + "\n");

            if (displayType == "add")
            {
                while (!valid)
                {
                    Console.Write("Add" + QUANTITY);
                    quantityString = Console.ReadLine();

                    // Check validity
                    try
                    {
                        quantity = Int32.Parse(quantityString);
                        if (quantity <= 0)
                        {
                            Console.WriteLine(VALIDITY_CATCH);
                        }
                        else
                        {
                            // Worked, therefore inputs valid                            
                            valid = true;
                        }                       
                    }
                    catch
                    {
                        Console.WriteLine(VALIDITY_CATCH);
                    }
                }

                // Add quantity
                library.add(toolSelected, quantity);
            }
            else if (displayType == "delete")
            {
                while (!valid)
                {
                    Console.Write("Delete" + QUANTITY);
                    quantityString = Console.ReadLine();

                    // Check validity
                    try
                    {
                        quantity = Int32.Parse(quantityString);
                        if (quantity < 1)
                        {
                            Console.WriteLine(VALIDITY_CATCH);
                        }
                        else
                        {
                            // Worked, therefore inputs valid
                            valid = true;
                        }
                    }
                    catch
                    {
                        Console.WriteLine(VALIDITY_CATCH);
                    }
                }

                // Delete quantity
                if (quantity > toolSelected.Quantity)
                {
                    Console.WriteLine("\n== The entered quantity exceeds the current quantity of the tool in the library. ==");
                }
                else
                {
                    library.delete(toolSelected, quantity);
                }
                
            }

            // Display tool info again
            Console.WriteLine("\n" + toolSelected.ToolInfo());
            Console.WriteLine(FOOTER);

            // Go back to staff menu
            Console.Write(RETURN);
            Console.ReadKey(true);
            Console.WriteLine("\n");
            menus.Staff_Menu();
        }

        public static void Remove_Selected_Member(string displayType)
        {
            // Constants
            string SELECTION_TEXT = "\nPlease make a selection (1-{0}, or 0 to return to staff menu): ";
            string HEADER = "===============================Remove Registered Member===============================";
            //string FOOTER = "=============================================================================";
            string CHECK = "Are you sure you want to delete this member?(Y/N): ";
            //string VALIDITY_CATCH = "== I'm sorry, your input was not recognised. Please enter a valid input. ==\n";
            string RETURN = "Press any key to return to previous menu...";

            // Variables
            iMember[] myMembers;
            iMember memberSelected = null;
            int memberNum;
            string textInput = "";
            string answer;
            bool answerBool = false;
            bool valid = false;

            // Instances of classes
            CLI_Menus menus = new CLI_Menus();
            ToolLibrarySystem library = new ToolLibrarySystem();

            // Get the number of members displayed
            myMembers = ToolLibrarySystem.memberLibrary.toArray();
            memberNum = myMembers.Length;

            // Make member selection
            Console.Write(SELECTION_TEXT, memberNum);
            textInput = CLI_Inputs.Return_Key_Press(textInput, memberNum);

            // Choose member from input
            for (int counter = 0; counter <= memberNum; counter++)
            {
                if (textInput == "0")
                {
                    // Go back to member menu
                    Console.WriteLine();
                    menus.Staff_Menu();
                    return;
                }
                else if (textInput != "0" && textInput == counter.ToString())
                {
                    Console.WriteLine();
                    memberSelected = myMembers[counter - 1];
                }
            }

            // Display member info and run check
            Console.WriteLine(HEADER);
            Console.WriteLine("Member Selected: " + memberSelected.ToString() + "\n");
            Console.Write(CHECK);
            answer = Read_Key_Press();

            while (!valid)
            {    
                // Check validity
                if (answer.ToUpper() == "Y")
                {
                    answerBool = true;
                    valid = true;
                }
                else if (answer.ToUpper() == "N")
                {
                    answerBool = false;
                    valid = true;
                }
                else
                {
                    Console.Write("Please enter a valid input. (Y/N): ");
                    answer = Read_Key_Press();
                }
            }

            // Delete member or return to member selection
            if (answerBool)
            {
                Console.WriteLine();
                library.delete(memberSelected);
            }
            else
            {
                Console.WriteLine();
                menus.Member_Selection(displayType);
            }

            // Display members again
            menus.Member_Selection("display");

            //Console.WriteLine(FOOTER);

            // Go back to staff menu
            Console.Write(RETURN);
            Console.ReadKey(true);
            Console.WriteLine("\n");
            menus.Staff_Menu();
        }

        public static void Find_Selected_Member(string displayType)
        {
            // Constants
            string SELECTION_TEXT = "\nPlease make a selection (1-{0}, or 0 to return to staff menu): ";
            string HEADER = "================Get Contact Number================";            
            string FOOTER = "==================================================";
            string RETURN = "Press any key to return to previous menu...";

            // Variables
            iMember[] myMembers;
            iMember memberSelected = null;
            int memberNum;
            string textInput = "";
            string contactNumber; ;
            bool answerBool = false;
            bool valid = false;            

            // Instances of classes
            CLI_Menus menus = new CLI_Menus();
            ToolLibrarySystem library = new ToolLibrarySystem();

            // Get the number of members displayed
            myMembers = ToolLibrarySystem.memberLibrary.toArray();
            memberNum = myMembers.Length;

            // Make member selection
            Console.Write(SELECTION_TEXT, memberNum);
            textInput = CLI_Inputs.Return_Key_Press(textInput, memberNum);

            // Choose member from input
            for (int counter = 0; counter <= memberNum; counter++)
            {
                if (textInput == "0")
                {
                    // Go back to member menu
                    Console.WriteLine();
                    menus.Staff_Menu();
                    return;
                }
                else if (textInput != "0" && textInput == counter.ToString())
                {
                    Console.WriteLine();
                    memberSelected = myMembers[counter - 1];
                }
            }

            // Display member contact number
            string CONTACT_NUMBER = "Contact Number of " + memberSelected.FirstName + " " + memberSelected.LastName + ": ";
            Console.WriteLine(HEADER);
            Console.WriteLine(CONTACT_NUMBER + memberSelected.ContactNumber);
            Console.WriteLine(FOOTER);

            // Go back to staff menu
            Console.Write(RETURN);
            Console.ReadKey(true);
            Console.WriteLine("\n");
            menus.Staff_Menu();
        }

    }
}
