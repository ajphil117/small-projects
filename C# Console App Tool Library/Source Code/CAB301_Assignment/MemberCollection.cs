using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class MemberCollection : iMemberCollection
    {
        //A collection of registered members can be stored in an object of class MemberCollection
        //This class must use a Binary Search Tree as a class member to store a collection of Memberobjects.
        // An object of this class can be used to store all those registered members or
        //to store a collection of members who are currently renting a particular tool

        public BinarySearchTree memberCollection;

        public MemberCollection()
        {
            // Initialise memberCollection
            if (memberCollection == null)
            {
                memberCollection = new BinarySearchTree();
            }
        }

        // get the number of members in the community library
        public int Number => toArray().Length;       

        //add a new member to this member collection, make sure there are no duplicates in the member collection
        public void add(iMember aMember)
        {
            // Variables
            Member myMember;

            // Create new Member from aMember
            myMember = new Member(aMember.FirstName, aMember.LastName, aMember.ContactNumber, aMember.PIN);

            // Add member - adds check for if member exists to insert method
            memberCollection.Insert(myMember);  
        }

        //delete a given member from this member collection, a member can be deleted only when the member currently is not holding any tool
        public void delete(iMember aMember)
        {
            // Variables
            Member myMember;
            bool borrowing = false;

            // Create new Member from aMember
            myMember = new Member(aMember.FirstName, aMember.LastName, aMember.ContactNumber, aMember.PIN);

            // Check if member is borrowing tools
            foreach (string tool in myMember.Tools)
            {
                if (!string.IsNullOrEmpty(tool))
                {
                    borrowing = true;
                }
            }

            if (!borrowing)
            {
                memberCollection.Delete(myMember);
            }
            else
            {
                Console.WriteLine("== This member is currently borrowing tools, cannot delete them from system. ==\n");
            }
        }

        //search a given member in this member collection. Return true if this memeber is in the member collection; return false otherwise.
        public bool search(iMember aMember)
        {
            // Variables
            Member myMember;
            bool answer;

            // Create new Member from aMember
            myMember = new Member(aMember.FirstName, aMember.LastName, aMember.ContactNumber, aMember.PIN);

            answer = memberCollection.Search(myMember);

            Console.WriteLine("Search worked, does member exist: " + answer);

            return answer;
        }
        
        public iMember[] toArray()
        {
            // Variables
            List<iMember> membersList = new List<iMember>();
            iMember[] members;

            // Use in order tranversal to add members to the list
            membersList = memberCollection.InOrderTraverseList();

            // Convert to array and return
            members = membersList.ToArray();
            return members;
        }
    }
}
