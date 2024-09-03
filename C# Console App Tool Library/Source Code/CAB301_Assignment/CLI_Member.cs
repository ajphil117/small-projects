using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class CLI_Member
    {
        /** Declare Variables **/
        public static Member currentUser;
        public static int quantBorrowInt;

        // Instance of CLI_Menus class for Make_Menu method
        CLI_Menus menus = new CLI_Menus();


        /** Methods **/

        public void Display_Tools()
        {
            // Constants
            string RETURN = "Press any key to return to previous menu...";

            // Previous menu and display type variables
            string previousMenu = "member";
            string displayType = "display";

            // Display tool categories and tool types
            menus.Tool_Categories(previousMenu);

            // Display all the tools of the selected tool type
            menus.Tool_Selection(previousMenu, displayType);

            // Go back to member menu
            Console.Write(RETURN);
            Console.ReadKey(true);
            Console.WriteLine("\n");
            menus.Member_Menu();
        }

        // Borrow a tool from the tool library, given the name of the tool,
        // if the tool is available in the tool library
        public void Borrow_Tool()
        {
            // Constants            
            string HEADER_TEXT = "Welcome to the Tool Library";
            string HEADER = "==============Borrow Tool==============";
            string BLANK_FIELDS = "(Leave all fields blank if you wish to return to the previous menu.)";
            string FOOTER = "=======================================";
            string TOOL_NAME = "Tool Name: ";
            string QUANTITY = "Quantity to Borrow: ";
            string VALIDITY_CATCH = "\n== I'm sorry, your input was not recognised. Please enter a valid input. ==\n";
            string RETURN = "Press any key to return to previous menu...";

            // Variables
            string toolName;
            string quantBorrow;
            bool valid = false;
            bool inputValid = false;
            iTool[] tools;
            iTool myTool = null;
            bool exists = false;            

            // Instance of ToolLibrarySystem
            ToolLibrarySystem library = new ToolLibrarySystem();

            // Reset quantBorrowInt
            quantBorrowInt = -1;

            // Print headers
            Console.WriteLine(HEADER_TEXT);
            Console.WriteLine(HEADER);
            Console.WriteLine(BLANK_FIELDS);

            while (!valid)
            {
                // Reset inputValid
                inputValid = false;
                
                // Ask for tool info
                Console.Write(TOOL_NAME);
                toolName = Console.ReadLine();
                Console.Write(QUANTITY);
                quantBorrow = Console.ReadLine();

                // Check input validity
                while (!inputValid)
                {
                    // Check for blank fields
                    if (toolName == "" && quantBorrow == "")
                    {
                        // Return to previous menu
                        Console.WriteLine();
                        menus.Member_Menu();
                        return;
                    }

                    try
                    {
                        quantBorrowInt = Int32.Parse(quantBorrow);
                    }
                    catch
                    {
                        // Input not valid
                        Console.WriteLine(VALIDITY_CATCH);

                        // Get inputs again
                        Console.Write(TOOL_NAME);
                        toolName = Console.ReadLine();
                        Console.Write(QUANTITY);
                        quantBorrow = Console.ReadLine();
                    }

                    if (quantBorrowInt != -1)
                    {
                        if (quantBorrowInt > 0 && quantBorrowInt <= 3)
                        {
                            // Quantity input is valid
                            inputValid = true;
                        }
                        else if (quantBorrowInt > 3)
                        {
                            // Input not valid
                            Console.WriteLine("\n== You cannot borrow more than three (3) tools at a time. ==\n");

                            // Get inputs again
                            Console.Write(TOOL_NAME);
                            toolName = Console.ReadLine();
                            Console.Write(QUANTITY);
                            quantBorrow = Console.ReadLine();
                        }
                        else
                        {
                            // Input not valid
                            Console.WriteLine(VALIDITY_CATCH);

                            // Get inputs again
                            Console.Write(TOOL_NAME);
                            toolName = Console.ReadLine();
                            Console.Write(QUANTITY);
                            quantBorrow = Console.ReadLine();
                        }
                    }                    
                }                

                // Check tool exists
                tools = ToolLibrarySystem.toolLibrary.toArray();
                foreach (iTool tool in tools)
                {
                    if (tool.Name.ToUpper() == toolName.ToUpper())
                    {
                        // Tool exists
                        exists = true;

                        // Save tool info
                        myTool = tool;
                    }
                }

                if (exists)
                {
                    // Check availablity
                    if (myTool.AvailableQuantity > 0 && quantBorrowInt <= myTool.AvailableQuantity)
                    {
                        // Tool avaliable, borrow tool
                        library.borrowTool(currentUser, myTool);                       
                    }
                    else if (myTool.AvailableQuantity > 0 && quantBorrowInt > myTool.AvailableQuantity)
                    {
                        // Not enough tools avaliable
                        Console.WriteLine("\n== The quantity requested exceeds what is currently avaliable for this tool. ==\n");
                    }
                    else
                    {
                        // Tool unavaliable
                        Console.WriteLine("== This tool is not currently avaliable to borrow. ==");
                    }

                    Console.WriteLine(FOOTER);

                    // Tool was valid
                    valid = true;
                }
                else
                {
                    // Tool does not exist in the library
                    Console.WriteLine("\n== Tool does not exist in library. ==\n");
                }
            }

            // Go back to member menu
            Console.Write(RETURN);
            Console.ReadKey(true);
            Console.WriteLine("\n");
            menus.Member_Menu();
        }

        public void Return_Tool()
        {
            // Constants       
            string SELECTION_TEXT = "\nPlease make a selection (1-{0}, or 0 to return to member menu): ";
            string HEADER_TEXT = "Welcome to the Tool Library";
            string HEADER = "================Return Borrowed Tool================";
            string FOOTER = "====================================================\n";
            string VALIDITY_CATCH = "\n== I'm sorry, your input was not recognised. Please enter a valid input. ==\n";
            string CHECK = "Are you sure you want to return this tool?(Y/N): ";
            string RETURN = "Press any key to return to previous menu...";

            // Variables
            int toolNum = 0;
            iTool[] tools;
            iTool myTool = null;
            string textInput = "";

            string toolInfo = "";
            string toolName = "";

            bool valid = false;
            bool exists = false;
            string answer;
            bool answerBool = false;

            // Instance of ToolLibrarySystem
            ToolLibrarySystem library = new ToolLibrarySystem();
            CLI_Menus menus = new CLI_Menus();

            // Reset quantBorrowInt
            quantBorrowInt = 0;

            // Display tools            
            library.display(currentUser.ContactNumber);

            // Get the number of tools displayed
            foreach (string tool in CLI_Member.currentUser.Tools)
            {
                if (!string.IsNullOrEmpty(tool))
                {
                    toolNum += 1;
                }
            }
            if (toolNum == 0)
            {
                // There are no tools to delete
                Console.WriteLine("\n== You are not currently renting any tools, nothing to delete. ==\n");

                // Go back to member menu
                Console.Write(RETURN);
                Console.ReadKey(true);
                Console.WriteLine("\n");
                menus.Member_Menu();

                return;
            }

            // Make tool selection
            Console.Write(SELECTION_TEXT, toolNum);
            textInput = CLI_Inputs.Return_Key_Press(textInput, toolNum);

            // Choose tool from input
            for (int counter = 0; counter <= toolNum; counter++)
            {
                if (textInput == "0")
                {
                    // Go back to member menu
                    Console.WriteLine();
                    menus.Member_Menu();
                    return;
                }
                else if (textInput != "0" && textInput == counter.ToString())
                {
                    Console.WriteLine();
                    toolInfo = CLI_Member.currentUser.Tools[counter - 1];

                    // Isolate the tools name from the string
                    toolName = toolInfo.Split(',')[0].Trim();
                }
            }

            // Check tool exists
            tools = ToolLibrarySystem.toolLibrary.toArray();

            foreach (string toolString in currentUser.Tools)
            {
                if (!string.IsNullOrEmpty(toolString))
                {
                    // Compare the two names
                    if (toolString.ToUpper() == toolName.ToUpper())
                    {
                        // Tool exists
                        exists = true;

                        // Get the full tool information
                        foreach (iTool tool in tools)
                        {
                            if (tool.Name.ToUpper() == toolString.ToUpper())
                            {
                                // Save tool info
                                myTool = tool;
                            }
                        }
                    }
                }
            } 

            if (exists)
            {
                // Display tool info and run check
                Console.WriteLine(HEADER);
                Console.WriteLine("Tool Selected: " + toolInfo + "\n");
                Console.Write(CHECK);
                answer = CLI_Inputs.Read_Key_Press();

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
                        answer = CLI_Inputs.Read_Key_Press();
                    }
                }

                // Does user want to return tool
                if (answerBool)
                {
                    Console.WriteLine();
                    Console.WriteLine(FOOTER);
                    library.returnTool(currentUser, myTool);

                    // Update avaliable quantity
                    myTool.AvailableQuantity += 1;
                }
                else
                {
                    Console.WriteLine();
                    Return_Tool();
                    return;
                }                

                // Check return quantity
                //if (borrowedQuantityInt > 0 && quantBorrowInt <= borrowedQuantityInt)
                //{

                //    Console.WriteLine("Can remove tool");
                //    // Tool avaliable, borrow tool

                //    library.returnTool(currentUser, myTool);
                //    myTool.AvailableQuantity = myTool.AvailableQuantity + quantBorrowInt;
                //}
                //else if (myTool.AvailableQuantity > 0 && quantBorrowInt > myTool.AvailableQuantity)
                //{
                //    // Return quantity exceeds what user is borrowing
                //    Console.WriteLine("== The quantity requested exceeds what you are currently renting of this tool. ==");
                //}
                //else
                //{
                //    // Tool not renting
                //    Console.WriteLine("== This tool is not currently being rented by you, cannot return. ==");
                //}
            }
            else
            {
                Console.WriteLine("== This tool is not currently being rented by you, cannot return. ==");
            }

            // Display rented tools again
            library.display(currentUser.ContactNumber);

            // Go back to member menu
            Console.Write(RETURN);
            Console.ReadKey(true);
            Console.WriteLine("\n");
            menus.Member_Menu();
        }

        // List all the tools that are currently holding by the member
        public void List_Borrowed()
        {
            // Constants
            string HEADER_TEXT = "Welcome to the Tool Library";
            string HEADER = "==============Enter Contact Number==============";
            string BLANK_FIELDS = "(Leave field blank if you wish to return to the previous menu.)";
            string FOOTER = "================================================";
            string CONTACT = "Please enter your contact number: ";
            string VALIDITY_CATCH = "\n== I'm sorry, your input was not recognised. Please enter a valid input. ==\n";
            string RETURN = "Press any key to return to previous menu...";

            // Variables
            string contactNumber = "";
            long contactNumberInt;
            bool valid = false;
            iMember[] members;
            bool exists = false;

            // Instance of ToolLibrarySystem
            ToolLibrarySystem library = new ToolLibrarySystem();

            // Print headers
            Console.WriteLine(HEADER_TEXT);
            Console.WriteLine(HEADER);
            Console.WriteLine(BLANK_FIELDS);

            while (!valid)
            {
                // Ask for contact number
                Console.Write(CONTACT);
                contactNumber = Console.ReadLine();

                // Check for blank fields
                if (contactNumber == "")
                {
                    // Return to previous menu
                    Console.WriteLine(FOOTER);
                    Console.WriteLine();
                    menus.Member_Menu();
                    return;
                }

                // Check input was valid
                try
                {
                    contactNumberInt = Int64.Parse(contactNumber);

                    // Check contact matched current users contact
                    if (currentUser.ContactNumber != contactNumber)
                    {
                        // Contacts do not match
                        Console.WriteLine("\n== Incorrect contact number. ==\n");
                    }
                    else
                    {
                        // Contact matched, inputs valid
                        valid = true;
                    }                    
                }
                catch
                {
                    Console.WriteLine(VALIDITY_CATCH);
                }
            }
            Console.WriteLine(FOOTER);
            Console.WriteLine();

            // Display rented tools
            library.display(contactNumber);

            // Go back to member menu
            Console.Write(RETURN);
            Console.ReadKey(true);
            Console.WriteLine("\n");
            menus.Member_Menu();
        }

        // Display top three most frequently borrowed tools by all
        // the members in the descending order by the number of times the
        // tool has been borrowed. The display should include the name
        // of the tool and the number of times that the tool has been
        // borrowed by now
        public void Display_Top_Three()
        {
            // Constants            
            string HEADER_TEXT = "Welcome to the Tool Library";
            string HEADER = "==============Top Three Tools==============";
            string FOOTER = "===========================================";
            string RETURN = "Press any key to return to previous menu...";

            // Variables
            iTool[] tools;
            int numTools = 0;
            iTool myTool = null;

            // Instance of ToolLibrarySystem
            ToolLibrarySystem library = new ToolLibrarySystem();

            // Print headers
            Console.WriteLine(HEADER_TEXT);
            Console.WriteLine(HEADER);

            // Get tool array
            tools = ToolLibrarySystem.toolLibrary.toArray();

            // Get the number of tools in the array
            foreach (iTool tool in tools)
            {
                if (tool != null)
                {
                    numTools += 1;
                }
            }

            // Check if there are (more than) 3 tools that currently exist
            if (numTools >= 3)
            {
                // Display top three
                library.displayTopThree();
            }
            else
            {
                if (numTools == 0)
                {
                    // Tell user there are no tools
                    Console.WriteLine("\n== There are currently no existing tools in the system. ==\n");
                }
                else
                {
                    foreach (iTool tool in tools)
                    {
                        if (numTools == 1)
                        {
                            // Print the only tool
                            Console.WriteLine("1. " + tool.Name + ", " + tool.NoBorrowings + " borrowings");
                            Console.WriteLine("\n== There is currently only one existing tool in the system. ==\n");
                        }
                        else
                        {
                            if (tool != null && myTool == null)
                            {
                                // Save data
                                myTool = tool;
                            }
                            else if (tool != null)
                            {
                                // Match data
                                if (tool.NoBorrowings > myTool.NoBorrowings)
                                {
                                    // Print the two tools depending on noBorrowings
                                    Console.WriteLine("1. " + tool.Name + ", " + tool.NoBorrowings + " borrowings");
                                    Console.WriteLine("2. " + myTool.Name + ", " + myTool.NoBorrowings + " borrowings");
                                    Console.WriteLine("\n== There are currently only two existing tools in the system. ==\n");
                                }
                                else
                                {
                                    // Print the two tools depending on noBorrowings
                                    Console.WriteLine("1. " + myTool.Name + ", " + myTool.NoBorrowings + " borrowings");
                                    Console.WriteLine("2. " + tool.Name + ", " + tool.NoBorrowings + " borrowings");
                                    Console.WriteLine("\n== There are currently only two existing tools in the system. ==\n");
                                }
                            }                            
                        }
                    }
                }
            }
            
            // Print footer and return to member menu
            Console.WriteLine(FOOTER);
            Console.Write(RETURN);
            Console.ReadKey(true);
            Console.WriteLine("\n");
            menus.Member_Menu();
        }

    }
}
