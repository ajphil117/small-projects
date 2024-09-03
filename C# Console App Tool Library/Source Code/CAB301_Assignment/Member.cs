using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Member : iMember
    {
        /** Declare Variables **/
        string firstName;
        string lastName;
        string contactNumber;
        string pin;
        string[] tools;

        Member member;


        /** Methods **/

        public Member (string firstName, string lastName, string contactNumber, string pin)
        {
            FirstName = firstName;
            LastName = lastName;
            ContactNumber = contactNumber;
            PIN = pin;
            this.tools = new string[3];
        }

        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }

        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        public string ContactNumber
        { 
            get => contactNumber;
            set => contactNumber = value;
        
        }

        public string PIN
        {
            get => pin;
            set => pin = value;
        }

        // A member cannot borrow more than three (3) tools at the same time
        public string[] Tools => tools;        

        //add a given tool to the list of tools that this member is currently holding
        public void addTool(iTool aTool)
        {
            // Variables
            Tool myTool;
            int numTools = 0;
            bool alreadyBorrowing = false;

            // Check member is not already borrowing 3 tools
            foreach (string tool in CLI_Member.currentUser.tools)
            {
                if (!string.IsNullOrEmpty(tool))
                {
                    numTools += 1;
                }
            }
            if (numTools == 3)
            {
                // Member borrowing 3 tools, cannot borrow another
                Console.WriteLine("Member borrowing 3 tools, cannot borrow another.");
                return;
            }

            // Check if entered quantity will give the member more than three tools
            if (numTools + CLI_Member.quantBorrowInt > 3)
            {
                // Member wiil be borrowing 3 tools, cannot borrow this quantity
                Console.WriteLine("\n== The entered quantity will cause you to borrow more than three (3) tools, this quantity is too high. ==\n");
                return;
            }

            // Increase no. of borrowings and the overall borrowings for the tool
            aTool.NoBorrowings += CLI_Member.quantBorrowInt;

            // Decrease avaliable quantity
            aTool.AvailableQuantity -= CLI_Member.quantBorrowInt;

            // Create Tool from aTool
            myTool = new Tool(aTool.Name, aTool.Quantity);

            // Add tool to the array
            for (int counter = 0; counter < CLI_Member.quantBorrowInt; counter++)
            {
                CLI_Member.currentUser.tools[numTools + counter] = myTool.Name;
            }

            // Check user is not already borrowing this tool
            foreach (string toolString in CLI_Member.currentUser.tools)
            {
                if (!string.IsNullOrEmpty(toolString))
                {
                    // Get the tool name, split off the borrowed quantity after the comma
                    //thisToolName = toolString.Split(',')[0].Trim();

                    // Compare names
                    if (string.Compare(myTool.Name.ToUpper(), toolString.ToUpper()) == 0)
                    {
                        alreadyBorrowing = true;
                    }
                }
            }

            if (!alreadyBorrowing)
            {
                // Remove borrower from the tool's borrower collection
                aTool.addBorrower(CLI_Member.currentUser);
            }

            Console.WriteLine("\n== Tool was successfully borrowed! ==\n");
        }

        //delete a given tool from the list of tools that this member is currently holding
        public void deleteTool(iTool aTool)
        {
            // Variables
            string myToolName;
            string thisToolName;
            int compare = -1;
            int index = 0;
            int numTools = 0;
            bool stillBorrowing = false;

            // Count the number of tools user is borrowing
            foreach (string tool in CLI_Member.currentUser.tools)
            {
                if (!string.IsNullOrEmpty(tool))
                {
                    numTools += 1;
                }
            }
            if (numTools == 0)
            {
                // Member not borrowing tools
                Console.WriteLine("\n== No tools to return, you are not renting any tools. ==\n");
                return;
            }

            // Get name of the tool to return
            myToolName = aTool.Name;

            // Find the tool in the current user's tool array
            foreach (string toolString in CLI_Member.currentUser.tools)
            {
                if (!string.IsNullOrEmpty(toolString))
                {
                    // Get the tool name, split off the borrowed quantity after the comma
                    thisToolName = toolString.Split(',')[0].Trim();

                    // Check if this tool is the tool to return, compare the names
                    compare = string.Compare(myToolName.ToUpper(), thisToolName.ToUpper());

                    if (compare == 0)
                    {
                        // Remove the tool from its position in the array and remove empty postition
                        while (index < numTools)
                        {
                            if (index == (numTools - 1))
                            {
                                // Remove tool
                                CLI_Member.currentUser.tools[index] = null;                                
                            }
                            else
                            {
                                // Move tools into empty position
                                CLI_Member.currentUser.tools[index] = CLI_Member.currentUser.tools[index + 1];
                            }

                            // Increase index
                            index++;
                        }
                    }
                }                

                // Tool was not found, increase index
                index++;
            }

            // Check user is not borrowing any more of this tool
            foreach (string toolString in CLI_Member.currentUser.tools)
            {
                if (!string.IsNullOrEmpty(toolString))
                {
                    // Get the tool name, split off the borrowed quantity after the comma
                    thisToolName = toolString.Split(',')[0].Trim();

                    // Compare names
                    if (string.Compare(myToolName.ToUpper(), thisToolName.ToUpper()) == 0)
                    {
                        stillBorrowing = true;
                    }
                }                    
            }

            if (!stillBorrowing)
            {
                // Remove borrower from the tool's borrower collection
                aTool.deleteBorrower(CLI_Member.currentUser);
            }            
        }

        //return a string containing the first name, lastname, and contact phone number of this member
        public override string ToString()
        {
            return firstName + ", " + lastName + ", " + contactNumber;
        }

        /** <summary>
         *      The source this method was derived from:
         *      https://docs.microsoft.com/en-us/dotnet/api/system.icomparable.compareto?view=net-5.0 
         * </summary>
         */
        public int CompareTo(Member obj)
        {
            // Create member from variables in this class
            this.member = new Member(firstName, lastName, contactNumber, pin);

            // Check if passed object is null
            if (obj == null)
            {
                return 1;
            }

            // Not null, compare the objects as a member to the current member
            Member otherMember = obj;
            if (otherMember != null)
            {
                // Compare members attributes
                if ((member.FirstName.CompareTo(otherMember.FirstName) == 0 && member.lastName.CompareTo(otherMember.lastName) == 0)
                     || member.contactNumber.CompareTo(otherMember.contactNumber) == 0)
                {
                    // Members match
                    return 0;
                }

                return 1;
                //return this.member.CompareTo(otherMember.member);
            }
            else
            {
                throw new ArgumentException("Object is not a Member");
            }
        }
    }
}
