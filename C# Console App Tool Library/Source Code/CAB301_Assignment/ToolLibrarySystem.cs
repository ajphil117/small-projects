using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class ToolLibrarySystem : iToolLibrarySystem
    {
        /** Declare Variables **/

        // Full tool library array
        public static ToolCollection toolLibrary = new ToolCollection();

        // Full member library tree
        public static MemberCollection memberLibrary = new MemberCollection();

        // Set instance of ToolArray_Methods class
        ToolArray_Methods methods = new ToolArray_Methods();


        /** Methods **/

        public ToolLibrarySystem()
        {
            // Check if empty
            if (toolLibrary == null)
            {
                // Initialise library
                toolLibrary.toolCollection = methods.initialise(toolLibrary.toolCollection);
            }            
        }

        public void add(iTool aTool)
        {
            toolLibrary.add(aTool);
        }

        public void add(iTool aTool, int quantity)
        {
            // Variables
            int category;
            int type;

            Tool myTool;
            Tool[] myTools;
            List<Tool> toolsList;

            bool exists;
            int toolIndex = -1;
            int newQuantity;
            int newAvalibleQuantity;

            // Get category and type where this tool belongs
            category = CLI_Menus.category.Value;
            type = CLI_Menus.type.Value;

            // Create a new Tool from aTool
            myTool = new Tool(aTool.Name, aTool.Quantity);

            // Get tool array for this category and type
            myTools = toolLibrary.toolCollection[category][type];

            // Check tool exists
            exists = ToolLibrarySystem.toolLibrary.search(myTool);
            if (!exists)
            {
                // No tool to update
                Console.WriteLine("Tool does not exist.");
                return;
            }

            // Get the index of the tool
            foreach (Tool tool in myTools)
            {
                if (tool != null && tool.Name == myTool.Name)
                {
                    toolIndex = Array.IndexOf(myTools, tool);
                }
            }

            //// Check tool exists
            //if (toolIndex == -1)
            //{
            //    // No tool to delete
            //    Console.WriteLine("Tool does not exist.");
            //    return;
            //}

            // Convert the array into a list
            toolsList = new List<Tool>(myTools);

            // Get new quantitys
            newQuantity = myTool.Quantity + quantity;
            newAvalibleQuantity = myTool.AvailableQuantity + quantity;

            // Update tool
            myTool = toolsList[toolIndex];
            myTool.Quantity = newQuantity;
            myTool.AvailableQuantity = newAvalibleQuantity;
            toolsList[toolIndex] = myTool;

            // Convert back to array and update library
            myTools = toolsList.ToArray();
            toolLibrary.toolCollection[category][type] = myTools;
        }

        //add a new member to the system
        public void add(iMember aMember)
        {
            memberLibrary.add(aMember);
        }

        //a member borrows a tool from the tool library
        public void borrowTool(iMember aMember, iTool aTool)
        {
            // Variables
            int numTools = 0;
            
            // Create Member from aMember and Tool from aTool
            Member member = new Member(aMember.FirstName, aMember.LastName, aMember.ContactNumber, aMember.PIN);
            Tool tool = new Tool(aTool.Name, aTool.Quantity);

            // Check member is not already borrowing 3 tools
            foreach (string myTool in aMember.Tools)
            {
                if (myTool != null)
                {
                    numTools += 1;
                }
            }
            if (numTools == 3)
            {
                // Member borrowing 3 tools, cannot borrow another
                Console.WriteLine("\n== Already borrowing 3 tools, cannot borrow any more. ==\n");
                return;
            }

            // Add tool
            member.addTool(aTool);
        }

        public void delete(iTool aTool)
        {
            toolLibrary.delete(aTool);
        }

        public void delete(iTool aTool, int quantity)
        {
            // Variables
            int category;
            int type;

            Tool myTool;
            Tool[] myTools;
            List<Tool> toolsList;

            bool exists;
            int toolIndex = -1;
            int newQuantity;
            int newAvalibleQuantity;

            // Get category and type where this tool belongs
            category = CLI_Menus.category.Value;
            type = CLI_Menus.type.Value;

            // Create a new Tool from aTool
            myTool = new Tool(aTool.Name, aTool.Quantity);

            // Get tool array for this category and type
            myTools = toolLibrary.toolCollection[category][type];

            // Check tool exists
            exists = ToolLibrarySystem.toolLibrary.search(myTool);
            if (!exists)
            {
                // No tool to update
                Console.WriteLine("Tool does not exist.");
                return;
            }

            // Get the index of the tool
            foreach (Tool tool in myTools)
            {
                if (tool != null && tool.Name == myTool.Name)
                {
                    toolIndex = Array.IndexOf(myTools, tool);
                }
            }

            // Check tool exists
            //if (toolIndex == -1)
            //{
            //    // No tool to delete
            //    Console.WriteLine("Tool does not exist.");
            //    return;
            //}

            // Convert the array into a list
            toolsList = new List<Tool>(myTools);

            // Get new quantity
            newQuantity = myTool.Quantity - quantity;
            newAvalibleQuantity = myTool.AvailableQuantity - quantity;

            // Check quantity - if quantity of a tool is <= 0, delete the tool
            if (newQuantity <= 0)
            {
                delete(myTool);
                return;
            }

            // Update tool
            myTool = toolsList[toolIndex];
            myTool.Quantity = newQuantity;
            myTool.AvailableQuantity = newAvalibleQuantity;
            toolsList[toolIndex] = myTool;

            // Convert back to array and update library
            myTools = toolsList.ToArray();
            toolLibrary.toolCollection[category][type] = myTools;


            // Check that tool qunatity got removed -- remove later
            //foreach (Tool t in toolLibrary.toolCollection[category][type])
            //{
            //    if (t != null)
            //    {
            //        Console.WriteLine(t.Name + t.Quantity);
            //    }
            //    else
            //    {
            //        Console.WriteLine("null");
            //    }
            //}
            //Console.WriteLine();
        }

        //delete a memeber from the system
        public void delete(iMember aMember)
        {
            memberLibrary.delete(aMember);           
        }

        //given the contact phone number of a member, display all the tools that the member are currently renting
        public void display(string aMemberContactNo)
        {
            // Constants
            string HEADER_TEXT = "Welcome to the Tool Library";
            string HEADER = "=================Tools Renting=================";
            string FOOTER = "===============================================";

            // Variables
            iMember[] members;
            string[] tools = null;
            bool borrowing = false;

            // Get array of members
            members = memberLibrary.toArray();

            // Find member with the contact number
            foreach (iMember member in members)
            {
                if (member.ContactNumber == aMemberContactNo)
                {
                    // Get tools borrowing
                    //tools = member.Tools;   
                    tools = listTools(member);
                }
            }

            // Display tools
            Console.WriteLine(HEADER_TEXT);
            Console.WriteLine(HEADER);

            // Check member is not already borrowing 3 tools
            foreach (string tool in tools)
            {
                if (!string.IsNullOrEmpty(tool))
                {
                    borrowing =true;
                }
            }

            if (!borrowing || tools == null)
            {
                // Member borrowing 3 tools, cannot borrow another
                Console.WriteLine("=== You are currently not renting any tools ===");
                Console.WriteLine(FOOTER);
            }
            else if (borrowing && tools != null)
            {
                for (int counter = 0; counter < tools.Length; counter++)
                {
                    Console.WriteLine((counter + 1) + ". " + tools[counter]);
                }
                Console.WriteLine(FOOTER);
            }
        }

        //display all the tools of a tool type selected by a member
        public void displayTools(int aToolType)
        {
            // Constants
            string HEADER_TEXT = "\nWelcome to the Tool Library";
            string HEADER = "==============Tools==============\n";
            string FOOTER = "=================================";            

            // Variables
            int category;
            Tool[] tools;
            int toolCount;
            string menuText = "";

            // Instance of CLI_Menus class
            CLI_Menus menus = new CLI_Menus();

            // Get the category
            category = CLI_Menus.category.Value;

            // Get tools to display
            tools = toolLibrary.toolCollection[category][aToolType];

            // Get number of tools to display
            toolCount = tools.Length;

            // Display the tools
            menuText += HEADER;           
            for (int counter = 0; counter < toolCount; counter++)
            {
                if (tools != null)
                {
                   if (tools[counter] != null)
                   {
                        menuText += (counter + 1) + ". " + tools[counter] + "\n";
                   }                    
                }
                else
                {
                    menuText += "===There are no tools for this type===\n";
                }
            } 
            menuText += FOOTER;

            Console.WriteLine(HEADER_TEXT);
            Console.WriteLine(menuText);
        }
    
        //Display top three most frequently borrowed tools by the members in the descending order by the number of times each tool has been borrowed.
        public void displayTopThree()
        {
            // Variables
            iTool topTool = null;
            iTool[] topThree = new iTool[3];

            for (int counter = 0; counter < 3; counter++)
            {                
                // Get the top tool for the current poistion (1st, 2nd, 3rd)               
                topTool = Tool.GetTopTool(counter, topThree);                

                // Add it to the array
                topThree[counter] = topTool;

                // Check if tool was returned
                if (topTool == null)
                {
                    break;
                }
            }

            // Display the array
            for (int counter = 0; counter < topThree.Length; counter++)
            {
                if (topThree[counter] != null)
                {
                    Console.WriteLine((counter + 1) + ". " + topThree[counter].Name + ", " + topThree[counter].NoBorrowings + " borrowings");
                }
                else
                {
                    Console.WriteLine("\n== All (other) tools in the system have 0 borrowings. ==\n");
                    return;
                }
            }
        }

        //get a list of tools that are currently held by a given member
        public string[] listTools(iMember aMember)
        {
            return aMember.Tools;           
        }

        //a member returns a tool to the tool library
        public void returnTool(iMember aMember, iTool aTool)
        {
            // Create Member from aMember
            Member member = new Member(aMember.FirstName, aMember.LastName, aMember.ContactNumber, aMember.PIN);

            // Return tool
            member.deleteTool(aTool);
        }

    }
}
