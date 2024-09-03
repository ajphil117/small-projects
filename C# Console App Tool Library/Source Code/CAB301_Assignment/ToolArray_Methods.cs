using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class ToolArray_Methods
    {
        public Tool[][][] initialise(Tool[][][] toolArray)
        {
            // Set category size
            toolArray = new Tool[9][][]; // there are 9 categories total

            // Set category and type nesting sizes
            toolArray[0] = new Tool[5][]; // there are 5 types in the 1st category
            toolArray[1] = new Tool[6][]; // there are 6 types in the 2nd category
            toolArray[2] = new Tool[5][]; // there are 5 types in the 3rd category
            toolArray[3] = new Tool[6][]; // there are 6 types in the 4th category
            toolArray[4] = new Tool[6][]; // there are 6 types in the 5th category
            toolArray[5] = new Tool[6][]; // there are 6 types in the 6th category
            toolArray[6] = new Tool[5][]; // there are 5 types in the 7th category
            toolArray[7] = new Tool[5][]; // there are 5 types in the 8th category
            toolArray[8] = new Tool[6][]; // there are 6 types in the 9th category

            // Set initial tool storage size
            for (int category = 0; category < toolArray.Length; category++)
            {
                for (int type = 0; type < toolArray[category].Length; type++)
                {
                    toolArray[category][type] = new Tool[1];
                }
            }

            return toolArray;
        }

        public Tool[][][] resizeArray(Tool[][][] toolArray, int newSize, int category, int type)
        {
            Array.Resize(ref toolArray[category][type], newSize);
            return toolArray;
        }

        public bool compareTools(Tool toolA, Tool toolB)
        {
            if (toolA.Name == toolB.Name && toolA.Quantity == toolB.Quantity
                && toolA.AvailableQuantity == toolB.AvailableQuantity && toolA.NoBorrowings == toolB.NoBorrowings)
            {
                // Tools match
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

// test length
//foreach (Tool[][] tool in toolArray)
//{
//    Console.WriteLine(tool.Length); // number of Types

//    foreach (Tool[] t in tool)
//    {
//        Console.WriteLine(t.Length); // number of tools in types

//        //foreach (Tool obj in t)
//        //{
//        //    Console.WriteLine(obj); // tools in type
//        //}
//    }
//}

//// all tool in specific category and type

//foreach (Tool t in toolArray[0][0])
//{
//    Console.WriteLine(t);
//}

//// number of tools in type

//Console.WriteLine(toolArray[0][0].Length);