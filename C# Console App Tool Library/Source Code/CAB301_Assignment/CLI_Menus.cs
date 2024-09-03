using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class CLI_Menus
    {
        /** Declare Variables **/

        string textInput;
        public static int? category;
        public static int? type;


        /** Methods **/

        public void Make_Menu(string menuText, int numOptions, string previousMenu)
        {
            // Constants:
            string HEADER_TEXT = "Welcome to the Tool Library";
            string SELECTION_TEXT = "\nPlease make a selection (1-{0}, or 0 to ";
            string PREVIOUS_MENU = "return to " + previousMenu + " menu): ";
            string EXIT = "exit): ";

            // Print Menu:
            Console.WriteLine(HEADER_TEXT);
            Console.WriteLine(menuText);
            Console.Write(SELECTION_TEXT, numOptions);

            if (previousMenu == "")
            {
                Console.Write(EXIT);
            }
            else
            {
                Console.Write(PREVIOUS_MENU);
            }
        }

        public void Main_Menu()
        {
            // Constants
            int OPTION_COUNT = 2; // the previous menu option is not included as it will always be present
            string PREVIOUS_MENU = "";

            // Menu Text Constant
            string MAIN_TEXT = "===========Main Menu===========\n" +
                               "1. Staff Login\n" +
                               "2. Member Login\n" +
                               "0. Exit\n" +
                               "===============================";

            // Reset textInput
            textInput = "";

            // Print menu text
            Make_Menu(MAIN_TEXT, OPTION_COUNT, PREVIOUS_MENU);

            // Read keypress and check validity
            textInput = CLI_Inputs.Return_Key_Press(textInput, OPTION_COUNT);

            // Create an array of methods to switch between menus
            Action[] methods = { CLI_Inputs.Exit, Staff_Login, Member_Login };

            // Change menu from input
            CLI_Inputs.Select_Menu(textInput, OPTION_COUNT, methods);
        }

        public void Staff_Login()
        {
            // Constants
            string MENU_BRANCH = "staff";
            string LOGIN_HEADER = "===========Staff Login==========="; 

            // Print header
            Console.WriteLine(LOGIN_HEADER);

            // Run login functionality
            CLI_Inputs.Login(MENU_BRANCH);
        }

        public void Member_Login()
        {
            // Constants
            string MENU_BRANCH = "member";
            string LOGIN_HEADER = "===========Member Login===========";            

            // Print header
            Console.WriteLine(LOGIN_HEADER);

            // Run login functionality
            CLI_Inputs.Login(MENU_BRANCH);           
        }

        public void Staff_Menu()
        {
            // Constants
            int OPTION_COUNT = 6; // the previous menu option is not included as it will always be present
            string PREVIOUS_MENU = "main";

            // Instance of CLI_Staff class
            CLI_Staff staff = new CLI_Staff();

            // Menu Text Constant
            string STAFF_TEXT = "================Staff Menu================\n" +
                                "1. Add a new tool\n" +
                                "2. Add new pieces of an existing tool\n" +
                                "3. Remove some pieces of a tool\n" +
                                "4. Register a new member\n" +
                                "5. Remove a member\n" +
                                "6. Find the contact number of a member\n" +
                                "0. Return to main menu\n" +
                                "==========================================";

            // Reset textInput
            textInput = "";

            // Print menu text
            Make_Menu(STAFF_TEXT, OPTION_COUNT, PREVIOUS_MENU);

            // Read keypress and check validity
            textInput = CLI_Inputs.Return_Key_Press(textInput, OPTION_COUNT);

            // Create an array of methods to switch between menus
            Action[] methods = { Main_Menu, staff.Add_New_Tool, staff.Add_New_Tool_Pieces, staff.Remove_Tool_Pieces,
                                 staff.Register_Member, staff.Remove_Member, staff.Get_Contact_Number};

            // Change menu from input
            CLI_Inputs.Select_Menu(textInput, OPTION_COUNT, methods);
        }

        public void Member_Menu()
        {
            // Constants
            int OPTION_COUNT = 5; // the previous menu option is not included as it will always be present
            string PREVIOUS_MENU = "main";

            // Instance of CLI_Member class
            CLI_Member member = new CLI_Member();

            // Menu Text Constant
            string MEMBER_TEXT = "===============Member Menu===============\n" +
                                 "1. Display all the tools of a tool type\n" +
                                 "2. Borrow a tool\n" +
                                 "3. Return a tool\n" +
                                 "4. List all the tools that I am renting\n" +
                                 "5. Display top three (3) most frequentely rented tools\n" +
                                 "0. Return to main menu\n" +
                                 "=========================================";

            // Reset textInput
            textInput = "";

            // Print menu text
            Make_Menu(MEMBER_TEXT, OPTION_COUNT, PREVIOUS_MENU);

            // Read keypress and check validity
            textInput = CLI_Inputs.Return_Key_Press(textInput, OPTION_COUNT);

            // Create an array of methods to switch between menus
            Action[] methods = { Main_Menu, member.Display_Tools, member.Borrow_Tool, member.Return_Tool, member.List_Borrowed,
                                 member.Display_Top_Three};

            // Change menu from input
            CLI_Inputs.Select_Menu(textInput, OPTION_COUNT, methods);
        }

        public void Tool_Categories(string menuBranch)
        {
            // Constants
            int OPTION_COUNT = 9;

            // Menu Text Constant
            string TOOL_CATEGORIES = "=========Tool Categories=========\n" +
                                     "1. Gardening Tools\n" +
                                     "2. Flooring Tools\n" +
                                     "3. Fencing Tools\n" +
                                     "4. Measuring Tools\n" +
                                     "5. Cleaning Tools\n" +
                                     "6. Painting Tools\n" +
                                     "7. Electronic Tools\n" +
                                     "8. Electricity Tools\n" +
                                     "9. Automotive Tools\n" +
                                     "0. Return to " + menuBranch + " menu\n" +
                                     "=================================";

            string GARDENING_TYPES = "=========Gardening Tools=========\n" +
                                 "1. Line Trimmers\n" +
                                 "2. Lawn Mowers\n" +
                                 "3. Hand Tools\n" +
                                 "4. Wheelbarrows\n" +
                                 "5. Garden Power Tools\n" +
                                 "0. Return to " + menuBranch + " menu\n" +
                                 "=================================";

            string FLOORING_TYPES = "=========Flooring Tools=========\n" +
                                    "1. Scrapers\n" +
                                    "2. Floor Lasers\n" +
                                    "3. Floor Levelling Tools\n" +
                                    "4. Floor Levelling Materials\n" +
                                    "5. Floor Hand Tools\n" +
                                    "6. Tilting Tools\n" +
                                    "0. Return to " + menuBranch + " menu\n" +
                                    "================================";

            string FENCING_TYPES = "=========Fencing Tools=========\n" +
                                   "1. Hand Tools\n" +
                                   "2. Electric Fencing\n" +
                                   "3. Steel Fencing\n" +
                                   "4. Power Tools\n" +
                                   "5. Fencing Accessories\n" +
                                   "0. Return to " + menuBranch + " menu\n" +
                                   "===============================";

            string MEASURING_TYPES = "=========Measuring Tools=========\n" +
                                     "1. Distance Tools\n" +
                                     "2. Laser Measurer\n" +
                                     "3. Measuring Jugs\n" +
                                     "4. Temperature & Humidity Tools\n" +
                                     "5. Levelling Tools\n" +
                                     "6. Markers\n" +
                                     "0. Return to " + menuBranch + " menu\n" +
                                     "==================================";

            string CLEANING_TYPES = "=========Cleaning Tools=========\n" +
                                    "1. Draining\n" +
                                    "2. Car Cleaning\n" +
                                    "3. Vacuum\n" +
                                    "4. Pressure Cleaners\n" +
                                    "5. Pool Cleaning\n" +
                                    "6. Floor Cleaning\n" +
                                    "0. Return to " + menuBranch + " menu\n" +
                                    "================================";

            string PAINTING_TYPES = "=========Painting Tools=========\n" +
                                    "1. Sanding Tools\n" +
                                    "2. Brushes\n" +
                                    "3. Rollers\n" +
                                    "4. Paint Removal Tools\n" +
                                    "5. Paint Scrapers\n" +
                                    "6. Sprayers\n" +
                                    "0. Return to " + menuBranch + " menu\n" +
                                    "================================";

            string ELECTRONIC_TYPES = "=========Electronic Tools=========\n" +
                                      "1. Voltage Tester\n" +
                                      "2. Oscilloscopes\n" +
                                      "3. Thermal Imaging\n" +
                                      "4. Data Test Tool\n" +
                                      "5. Insulation Testers\n" +
                                      "0. Return to " + menuBranch + " menu\n" +
                                      "==================================";

            string ELECTRICITY_TYPES = "=========Electricity Tools=========\n" +
                                       "1. Test Equipment\n" +
                                       "2. Safety Equipment\n" +
                                       "3. Basic Hand Tools\n" +
                                       "4. Circuit Protection\n" +
                                       "5. Cable Tools\n" +
                                       "0. Return to " + menuBranch + " menu\n" +
                                       "===================================";

            string AUTOMOTIVE_TYPES = "=========Automotive Tools=========\n" +
                                      "1. Jacks\n" +
                                      "2. Air Compressors\n" +
                                      "3. Battery Chargers\n" +
                                      "4. Socket Tools\n" +
                                      "5. Braking\n" +
                                      "6. Drivetrain\n" +
                                      "0. Return to " + menuBranch + " menu\n" +
                                      "==================================";

            // Reset textInput, category, and type
            textInput = "";
            category = null ;
            type = null;

            // Display all the tool categories
            Make_Menu(TOOL_CATEGORIES, OPTION_COUNT, menuBranch);

            // Get selection
            textInput = CLI_Inputs.Return_Key_Press(textInput, OPTION_COUNT);

            // Store category chosen
            if (textInput != "0")
            {
                category = (Int32.Parse(textInput)) - 1;
            }
            
            // Create an array of menus to switch between
            string[] menuText = { menuBranch, GARDENING_TYPES, FLOORING_TYPES, FENCING_TYPES, MEASURING_TYPES,
                                 CLEANING_TYPES, PAINTING_TYPES, ELECTRONIC_TYPES, ELECTRICITY_TYPES,
                                 AUTOMOTIVE_TYPES};

            // Change menu from input
            CLI_Inputs.Select_Menu_Category(textInput, OPTION_COUNT, menuText);
        }

        // menubranch is the member or staff
        public void Tool_Selection(string menuBranch, string displayType)
        {
            // Variables
            int optionCount = getTypeCount();  // get no. of tools in the type  

            // Get instance of CLI_Menus and ToolLibrary classes
            CLI_Menus menus = new CLI_Menus();
            ToolLibrarySystem library = new ToolLibrarySystem();

            // Reset textInput
            textInput = "";            

            // Get selection
            textInput = CLI_Inputs.Return_Key_Press(textInput, optionCount);

            // Store type chosen
            if (textInput != "0")
            {
                type = (Int32.Parse(textInput)) - 1;
            }
            else
            {
                if (menuBranch == "staff")
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

            // Change menu from input
            CLI_Inputs.Select_Menu_Type(textInput, menuBranch, category.Value, type.Value, displayType);
        }

        public void Member_Selection(string displayType)
        {
            // Constants
            string HEADER_TEXT = "Welcome to the Tool Library";
            string HEADER = "==============Members==============\n";
            string FOOTER = "===================================";
            
            // Variables
            iMember[] members;
            int memberCount;
            string menuText = "";

            // Instance of CLI_Menus class
            CLI_Menus menus = new CLI_Menus();

            // Get members to display
            members = ToolLibrarySystem.memberLibrary.toArray();

            // Get number of members to display
            memberCount = members.Length;

            // Display the members
            menuText += HEADER;
            for (int counter = 0; counter < memberCount; counter++)
            {
                if (members != null)
                {
                    if (members[counter] != null)
                    {
                        menuText += (counter + 1) + ". " + members[counter] + "\n";
                    }
                }
                else
                {
                    menuText += "===There are currently no registered members===\n";
                }
            }
            menuText += FOOTER;

            Console.WriteLine(HEADER_TEXT);
            Console.WriteLine(menuText);

            // Remove member if required
            if (displayType == "delete")
            {
                CLI_Inputs.Remove_Selected_Member(displayType);
            }
            // Find members contact number if required
            else if (displayType == "find")
            {
                CLI_Inputs.Find_Selected_Member(displayType);
            }
        }

        public int getTypeCount()
        {
            if (category == 0 || category == 2 || category == 6 || category == 7)
            {
                return 5;
            }
            else
            {
                return 6;
            }
        }

    }
}
