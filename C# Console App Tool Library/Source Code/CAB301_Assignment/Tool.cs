using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    /// <summary>
    /// 
    /// In this software application, each tool is represented by an object of class Tool.  
    /// 
    /// </summary> 
    class Tool : iTool
    {
        /** Declare Variables **/
        string name;
        int quantity;
        int availableQuantity;
        int noBorrowings;
        //iMemberCollection borrowers;
        MemberCollection borrowers;
        Tool tool;


        /** Methods **/       
        
        public Tool (string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
            AvailableQuantity = quantity;
            //NoBorrowings = noBorrowings;
            borrowers = new MemberCollection();
        }

        public string Name
        {
            get => name;
            set => name = value;        
        }

        public int Quantity
        {
            get => quantity;
            set => quantity = value;
        }

        public int AvailableQuantity
        {
            get => availableQuantity;
            set => availableQuantity = value;
        }

        public int NoBorrowings
        { 
            get => noBorrowings;
            set => noBorrowings = value;
        }

        public iMemberCollection GetBorrowers => borrowers;

        //add a member to the borrower list
        public void addBorrower(iMember aMember)
        {
            // Add borrower to borrower list
            GetBorrowers.add(aMember);
            borrowers.memberCollection.InOrderTraverse();
        }

        //delete a member from the borrower list
        public void deleteBorrower(iMember aMember)
        {
            // Delete borrower from borrower list
            GetBorrowers.delete(aMember);
        }
        
        //returns the top tool for the poistion given - e.g. 0 = top, 1 = second top, 2 = third top
        public static iTool GetTopTool(int position, iTool[] topTools)
        {
            // Variables
            iTool[] tools;
            int maxTop = -1;
            iTool topTool = null;            

            // Get the tool array
            tools = ToolLibrarySystem.toolLibrary.toArray();

            // Go through array to get top position borrower
            foreach (Tool tool in tools)
            {
                // Compare maxTop to no. borrowings
                if (position == 0 && tool.NoBorrowings != 0 && maxTop <= tool.NoBorrowings)
                {
                    // Get new highest borrowings
                    maxTop = tool.NoBorrowings;

                    // Save this tools info
                    topTool = tool;                    
                }
                else if (tool.NoBorrowings != 0 && maxTop <= tool.NoBorrowings && tool.NoBorrowings <= topTools[position - 1].NoBorrowings && tool.Name != topTools[position - 1].Name)
                {
                    // Get the maximum no. borrowing 
                    maxTop = tool.NoBorrowings;

                    // Save this tools info
                    topTool = tool;
                }
                
            }

            return topTool;
        } 

        //return a string containing the name and the available quantity of this tool
        public override string ToString()
        {            
            return name + ",  " + availableQuantity;
        }

        //return a string containing all info on a tool
        public string ToolInfo()
        {
            return "Name: " + name + ", Quantity: " + quantity + ", Avalible Quantity: " + availableQuantity + ", No. Borrowings: " + noBorrowings;
        }

    }
}
