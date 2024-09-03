using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class CLI_Staff
    {
        /** Declare Variables **/

        // Instance of CLI_Menus class for Make_Menu method
        CLI_Menus menus = new CLI_Menus();

        
        /** Methods **/

        public void Add_New_Tool()
        {
            // Constants
            string ADD = "\n========Add New Tool========";
            string BLANK_FIELDS = "(Leave all fields blank if you wish to return to the previous menu.)";
            string FOOTER = "============================";
            string NAME = "Name: ";
            string QUANTITY = "Quantity: ";
            string VALIDITY_CATCH = "\n== I'm sorry, your input was not recognised. Please enter a valid input. ==\n";
            string RETURN = "Press any key to return to previous menu...";

            // Variables
            string name = "";
            string toolName = "";
            string quantityString;
            int quantity = -1;
            bool valid = false;
            Tool newTool;

            // Instance of ToolLibrarySystem
            ToolLibrarySystem library = new ToolLibrarySystem();
            
            // Previous menu
            string previousMenu = "staff";
            string displayType = "display";
            
            // Display tool categories and tool types
            menus.Tool_Categories(previousMenu);

            // Display all the tools of the selected tool type
            menus.Tool_Selection(previousMenu, displayType);

            while (!valid)
            {
                // Get new tool info
                Console.WriteLine(ADD);
                Console.WriteLine(BLANK_FIELDS);
                Console.Write(NAME);
                name = Console.ReadLine();
                Console.Write(QUANTITY);
                quantityString = Console.ReadLine();
                Console.WriteLine(FOOTER);

                // Check for blank fields
                if (name == "" && quantityString == "")
                {
                    // Return to previous menu
                    Console.WriteLine();
                    menus.Staff_Menu();
                    return;
                }

                // Check validity
                try
                {
                    quantity = Int32.Parse(quantityString);

                    // Edit name so first letter is uppercase
                    toolName = name.Substring(0, 1).ToUpper() + name.Substring(1, name.Length - 1);

                    // Worked, therefore inputs valid
                    valid = true;
                }
                catch
                {
                    Console.Write(VALIDITY_CATCH);
                }
            }

            // Create new tool
            newTool = new Tool(toolName, quantity);

            // Add new tool to the library
            library.add(newTool);

            // Display all the tools in the selected tool type again
            library.displayTools(CLI_Menus.type.Value);
            Console.Write(RETURN);
            Console.ReadKey(true);
            Console.WriteLine("\n");
            menus.Staff_Menu();
        }

        public void Add_New_Tool_Pieces()
        {
            // Instance of ToolLibrarySystem
            ToolLibrarySystem library = new ToolLibrarySystem();

            // Previous menu
            string previousMenu = "staff";
            string displayType = "add";

            // Display tool categories and tool types
            menus.Tool_Categories(previousMenu);

            // Display all the tools of the selected tool type
            menus.Tool_Selection(previousMenu, displayType);            
        }

        public void Remove_Tool_Pieces()
        {
            // Instance of ToolLibrarySystem
            ToolLibrarySystem library = new ToolLibrarySystem();

            // Previous menu and display type variables
            string previousMenu = "staff";
            string displayType = "delete";

            // Display tool categories and tool types
            menus.Tool_Categories(previousMenu);

            // Select tool and remove quantity
            menus.Tool_Selection(previousMenu, displayType);

            // Display all the tools of the selected tool type
            // Select a tool from the tool list
            // Input the number of pieces of the tool to be removed
            // If the number of pieces of the tool is not more than the number of
            // pieces that are currently in the library, reduce the total quantity
            // and the available quantity of the tool
        }

        public void Register_Member()
        {
            // Constants
            string REGISTER = "\n========Register New Member========";
            string BLANK_FIELDS = "(Leave all fields blank if you wish to return to the previous menu.)";
            string FOOTER = "===================================\n";
            string FIRST_NAME = "First Name: ";
            string LAST_NAME = "Last Name: ";
            string CONTACT_NUMBER = "Contact Number: ";
            string PASSWORD = "PIN: ";
            string VALIDITY_CATCH = "== I'm sorry, your input was not recognised. Please enter a valid input. ==\n";
            string RETURN = "Press any key to return to previous menu...";

            // Variables
            string firstName = "";
            string lastName = "";
            string firstNameFormatted = "";
            string lastNameFormatted = "";
            string contactNo = "";
            long contactNoInt;
            string password = "";
            bool valid = false;
            Member newMember;

            // Instance of ToolLibrarySystem
            ToolLibrarySystem library = new ToolLibrarySystem();

            // Previous menu
            string previousMenu = "staff";
            string displayType = "display";

            // Display members
            menus.Member_Selection(displayType);

            while (!valid)
            {
                // Get new member info
                Console.WriteLine(REGISTER);
                Console.WriteLine(BLANK_FIELDS);
                Console.Write(FIRST_NAME);
                firstName = Console.ReadLine();
                Console.Write(LAST_NAME);
                lastName = Console.ReadLine();
                Console.Write(CONTACT_NUMBER);
                contactNo = Console.ReadLine();
                Console.Write(PASSWORD);
                password = Console.ReadLine();
                Console.WriteLine(FOOTER);

                // Check for blank fields
                if (firstName == "" && lastName == "" && contactNo == "" && password == "")
                {
                    // Return to previous menu
                    menus.Staff_Menu();
                    return;
                }

                // Check validity
                if (contactNo.Length > 10)
                {
                    Console.Write("== The contact number entered is invalid as it is too long. ==\n");
                }
                else
                {
                    try
                    {
                        contactNoInt = Int64.Parse(contactNo);

                        // Worked, therefore inputs valid
                        valid = true;
                    }
                    catch
                    {
                        Console.Write(VALIDITY_CATCH);
                    }
                }
                
            }

            // Edit names so first letter is uppercase
            firstNameFormatted = firstName.Substring(0, 1).ToUpper() + firstName.Substring(1, firstName.Length - 1);
            lastNameFormatted = lastName.Substring(0, 1).ToUpper() + lastName.Substring(1, lastName.Length - 1);

            // Create new member
            newMember = new Member(firstNameFormatted, lastNameFormatted, contactNo, password);

            // Add new member to the library
            library.add(newMember);

            // Display all the members again
            menus.Member_Selection(displayType);
            Console.Write(RETURN);
            Console.ReadKey(true);
            Console.WriteLine("\n");
            menus.Staff_Menu();
        }

        public void Remove_Member()
        {
            // Instance of ToolLibrarySystem
            ToolLibrarySystem library = new ToolLibrarySystem();

            // Dislay type variable
            string displayType = "delete";

            // Display all the members and remove selected
            menus.Member_Selection(displayType);
        }

        public void Get_Contact_Number()
        {
            // Instance of ToolLibrarySystem
            ToolLibrarySystem library = new ToolLibrarySystem();

            // Dislay type variable
            string displayType = "find";

            // Display all the members and get contact number of member selected
            menus.Member_Selection(displayType);
        }

    }
}
