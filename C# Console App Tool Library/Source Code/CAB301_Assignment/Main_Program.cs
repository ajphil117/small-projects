using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Main_Program
    {
        static void Main(string[] args)
        {
            /** Program Start **/

            // Object reference to menus class
            CLI_Menus start = new CLI_Menus();

            // Begin program
            start.Main_Menu();

            // Keep console from closing
            Console.ReadLine();


            /** Program Testing **/

            /** If used, needs to be moved up to before Program Start **/

            /*
            ToolCollection collection = new ToolCollection();
            ToolLibrarySystem library = new ToolLibrarySystem();
            MemberCollection memberCollection = new MemberCollection();

            // Tools to add
            iTool tool1 = new Tool("Saw", 4);
            iTool tool2 = new Tool("Screwdriver", 1);
            iTool tool3 = new Tool("Hammer", 1);
            iTool tool4 = new Tool("Wrench", 1);
            iTool tool5 = new Tool("Spanner", 1);

            // Set category and type
            CLI_Menus.category = 0;
            CLI_Menus.type = 0;

            // Add tool 1 and 2 to 0 0
            ToolLibrarySystem.toolLibrary.add(tool1);
            ToolLibrarySystem.toolLibrary.add(tool2);

            // Set category and type
            CLI_Menus.category = 1;
            CLI_Menus.type = 1;

            // Add tool 3 and 4 to 1 1
            ToolLibrarySystem.toolLibrary.add(tool3);
            ToolLibrarySystem.toolLibrary.add(tool4);

            // Set category and type
            CLI_Menus.category = 0;
            CLI_Menus.type = 1;

            // Add tool 5 to 0 1
            ToolLibrarySystem.toolLibrary.add(tool5);

            // Object reference for ToolCollection class
            iMember myMember = new Member("Jane", "Phil", "24232525", "1234");
            library.add(myMember);
            iMember myMember2 = new Member("Ash", "Jane", "123456789", "1234");
            library.add(myMember2);
            iMember myMember3 = new Member("Jamie", "Campbell", "32523525", "1234");
            library.add(myMember3);
            */
        }
    }
}
