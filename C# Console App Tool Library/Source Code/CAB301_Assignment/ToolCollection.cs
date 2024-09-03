using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    /// <summary>
    ///  In this software application, an object of the ToolCollection class is used to store and manipulate
    ///  a collection of Tool objects.
    ///  This class must use an Array or Arrays to store a collection of Tool objects.
    ///  An object of this class can be used to store a collection of tools that are being rented by a member
    ///  or a collection of tools of a tool type.
    ///  
    /// Dude in tute said he would use one multidimensional array fro the tools
    /// </summary>
    class ToolCollection : iToolCollection
    {
        /** Declare Variables **/
        // Tool collection array
        public Tool[][][] toolCollection;

        int number;

        // Set instance of ToolArray_Methods class
        ToolArray_Methods methods = new ToolArray_Methods();


        /** Methods **/

        public ToolCollection()
        {   
            // Check if empty
            if (toolCollection == null)
            {
                // Initialise collection
                toolCollection = methods.initialise(toolCollection);
            }

            // Set variables
            this.number = Number;
        }
        
        // get the number of the types of tools in the community library
        public int Number => number;

        public void add(iTool aTool)
        {
            // Variables
            int category;
            int type;
            int toolCount;
            Tool myTool;
            Tool[] myTools;
            //int quantityDiff;

            // Instance of ToolLibrarySystem class
            //ToolLibrarySystem library = new ToolLibrarySystem();

            // Get category and type where this tool belongs
            category = CLI_Menus.category.Value;
            type = CLI_Menus.type.Value;

            // Create a new Tool from aTool
            myTool = new Tool(aTool.Name, aTool.Quantity);

            // Get tool array for this category and type
            myTools = ToolLibrarySystem.toolLibrary.toolCollection[category][type];

            // Check if tool name already exists
            foreach (Tool tool in myTools)
            {
                if (tool != null && tool.Name == myTool.Name)
                {
                    // Tool already exists
                    Console.WriteLine("\n== The tool you are trying to add already exists in the system. ==");
                    return;

                    ////// Tool already exists, check quantity
                    //if (tool.Quantity < myTool.Quantity)
                    //{
                    //    quantityDiff = myTool.Quantity - tool.Quantity;

                    //    // Add quantity
                    //    library.add(myTool, quantityDiff);
                    //}
                    //else if (tool.Quantity > myTool.Quantity)
                    //{
                    //    quantityDiff = tool.Quantity - myTool.Quantity;

                    //    // Delete quantity
                    //    library.delete(myTool, quantityDiff);
                    //}
                }
            }

            // Get the number of tools in the type
            if (toolCollection == null || toolCollection[category] == null || toolCollection[category][type] == null)
            {
                toolCount = 0;
                
                // Add tool
                toolCollection[category][type][toolCount] = myTool;
            }
            else
            {
                toolCount = toolCollection[category][type].Length;

                // Resize array for new tool
                toolCollection = methods.resizeArray(toolCollection, toolCount + 1, category, type);
                
                // Add tool
                toolCollection[category][type][toolCount-1] = myTool;
            }
        }

        /** <summary>
         *      Some of this code was derived from the below source.
         *      (Converting the array to a list to remove tool then converting back to array.)
         *      Source:
         *      https://stackoverflow.com/questions/496896/how-to-delete-an-element-from-an-array-in-c-sharp 
         * </summary>
         */
        public void delete(iTool aTool)
        {
            // Variables
            int category;
            int type;
            
            Tool myTool;
            Tool[] tools;
            List<Tool> toolsList;

            bool exists;
            int toolIndex = -1;

            // Get category and type where this tool belongs
            category = CLI_Menus.category.Value;
            type = CLI_Menus.type.Value;

            // Create a new Tool from aTool
            myTool = new Tool(aTool.Name, aTool.Quantity);

            // Get tool array for this category and type
            tools = toolCollection[category][type];

            // Check tool exists
            exists = ToolLibrarySystem.toolLibrary.search(myTool);
            if (!exists)
            {
                // No tool to delete
                Console.WriteLine("Tool does not exist.");
                return;
            }

            // Get the index of the tool
            foreach (Tool tool in tools)
            {
                if (tool != null && methods.compareTools(tool, myTool))
                {
                    toolIndex = Array.IndexOf(tools, tool);
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
            toolsList = new List<Tool>(tools);

            // Delete the tool
            toolsList.RemoveAt(toolIndex);

            // Convert back to array and update collection
            tools = toolsList.ToArray();
            toolCollection[category][type] = tools;
        }

        public bool search(iTool aTool)
        {
            // Variables
            Tool myTool;

            // Create a new Tool from aTool
            myTool = new Tool(aTool.Name, aTool.Quantity);

            // Search the collection for myTool
            foreach (Tool[][] types in toolCollection)
            {
                foreach (Tool[] tools in types)
                {
                    foreach (Tool tool in tools)
                    {
                        if (tool != null && methods.compareTools(tool, myTool))
                        {
                            // Tool was found
                            return true;
                        }
                    }
                }
            }

            // No tool was found
            return false;            
        }

        // output the tools in this tool collection to an array of iTool
        public iTool[] toArray()
        {
            // Variables
            iTool[] myTools;
            List<iTool> toolsList = new List<iTool>();

            // Add all tools in the collection to list
            foreach (Tool[][] types in toolCollection)
            {
                foreach (Tool[] tools in types)
                {
                    foreach (Tool tool in tools)
                    {
                        if (tool != null)
                        {
                            toolsList.Add(tool);
                        }                        
                    }
                }
            }

            // Convert list to array
            myTools = toolsList.ToArray();

            return myTools;
        }

    }
}
