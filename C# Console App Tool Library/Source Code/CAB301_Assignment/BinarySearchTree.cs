using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    /** <summary>
     * 
     *  This class was derived from the lecture code on binary search trees.
     *  https://blackboard.qut.edu.au/bbcswebdav/pid-9209802-dt-content-rid-38020345_1/xid-38020345_1
     *  (the link to the code demo zip file)
     * 
     * </summary>
     */
    class BinarySearchTree
    {
		public BinarySearchTree_Node root;
		public static List<iMember> members = new List<iMember>();

		public BinarySearchTree()
		{
			root = null;
		}

		public bool IsEmpty()
		{
			return root == null;
		}

		public bool Search(Member member)
		{
			return Search(member, root);
		}

		private bool Search(Member member, BinarySearchTree_Node r)
		{
			if (r != null)
			{
				if (member.CompareTo(r.Member) == 0)
                {
					return true;
                }					
				else if (member.CompareTo(r.Member) < 0)
                {
					return Search(member, r.LeftChild);
				}					
				else
                {
					return Search(member, r.RightChild);
				}					
			}
            else
            {
				return false;
            }				
		}

		public void Insert(Member member)
		{
			if (root == null)
            {
				root = new BinarySearchTree_Node(member);
			}				
			else
            {
				Insert(member, root);
			}				
		}

		// pre: ptr != null
		// post: item is inserted to the binary search tree rooted at ptr
		private void Insert(Member member, BinarySearchTree_Node ptr)
		{
			// Check if member already exists
			if (member.CompareTo(ptr.Member) == 0)
            {
				// Member exists, cannot add
				Console.WriteLine("== Cannot register member, information added matches a current members information. ==\n");
				return;
			}
			// Does not exist
			else if (member.CompareTo(ptr.Member) < 0)
			{
				if (ptr.LeftChild == null)
                {
					ptr.LeftChild = new BinarySearchTree_Node(member);
                }					
				else
                {
					Insert(member, ptr.LeftChild);
                }
					
			}
			else
			{
				if (ptr.RightChild == null)
					ptr.RightChild = new BinarySearchTree_Node(member);
				else
					Insert(member, ptr.RightChild);
			}
		}

		// there are three cases to consider:
		// 1. the node to be deleted is a leaf
		// 2. the node to be deleted has only one child 
		// 3. the node to be deleted has both left and right children
		public void Delete(Member member)
		{
			// search for item and its parent
			BinarySearchTree_Node ptr = root; // search reference
			BinarySearchTree_Node parent = null; // parent of ptr
			while ((ptr != null) && (member.CompareTo(ptr.Member) != 0))
			{
				parent = ptr;
				if (member.CompareTo(ptr.Member) < 0) // move to the left child of ptr
                {
					ptr = ptr.LeftChild;
				}					
				else
                {
					ptr = ptr.RightChild;
				}					
			}

			if (ptr != null) // if the search was successful
			{
				// case 3: item has two children
				if ((ptr.LeftChild != null) && (ptr.RightChild != null))
				{
					// find the right-most node in left subtree of ptr
					if (ptr.LeftChild.RightChild == null) // a special case: the right subtree of ptr.LChild is empty
					{
						ptr.Member = ptr.LeftChild.Member;
						ptr.LeftChild = ptr.LeftChild.LeftChild;
					}
					else
					{
						BinarySearchTree_Node p = ptr.LeftChild;
						BinarySearchTree_Node pp = ptr; // parent of p
						while (p.RightChild != null)
						{
							pp = p;
							p = p.RightChild;
						}
						// copy the item at p to ptr
						ptr.Member = p.Member;
						pp.RightChild = p.LeftChild;
					}
				}
				else // cases 1 & 2: item has no or only one child
				{
					BinarySearchTree_Node c;
					if (ptr.LeftChild != null)
                    {
						c = ptr.LeftChild;
					}						
					else
                    {
						c = ptr.RightChild;
					}						

					// remove node ptr
					if (ptr == root) //need to change root
                    {
						root = c;
					}						
					else
					{
						if (ptr == parent.LeftChild)
							parent.LeftChild = c;
						else
							parent.RightChild = c;
					}
				}

			}
		}

		public List<iMember> InOrderTraverseList()
		{
			// Variables
			List<iMember> myMembers = new List<iMember>();
			
			// Reset members array
			members.Clear();

			InOrderTraverseList(root);

			myMembers = members;
			return myMembers;
		}

		public void InOrderTraverseList(BinarySearchTree_Node root)
		{
			// Variables
			int compareResult = -1;
			bool match = true;			
			
			if (root != null)
			{
				InOrderTraverseList(root.LeftChild);

				// Check member hasn't already been added
				if (members.Count != 0)
				{
					foreach (Member member in members)
					{
						compareResult = member.CompareTo(root.Member);
						if (compareResult != 0)
						{
							match = false;
						}
					}

					if (!match)
					{
						members.Add(root.Member);
					}
				}
				else
				{
					// No members in the array to compare, add member
					members.Add(root.Member);
				}
				
				InOrderTraverseList(root.RightChild);
			}
		}


		public void PreOrderTraverse()
		{
			Console.Write("PreOrder: ");
			PreOrderTraverse(root);
			Console.WriteLine();
		}

		private void PreOrderTraverse(BinarySearchTree_Node root)
		{
			if (root != null)
			{
				Console.Write(root.Member);
				PreOrderTraverse(root.LeftChild);
				PreOrderTraverse(root.RightChild);
			}
		}

		public void InOrderTraverse()
		{
			Console.Write("InOrder: ");
			InOrderTraverse(root);
			Console.WriteLine();
		}

		private void InOrderTraverse(BinarySearchTree_Node root)
		{
			if (root != null)
			{
				InOrderTraverse(root.LeftChild);
				Console.Write(root.Member);
				InOrderTraverse(root.RightChild);
			}
		}

		public void PostOrderTraverse()
		{
			Console.Write("PostOrder: ");
			PostOrderTraverse(root);
			Console.WriteLine();
		}

		private void PostOrderTraverse(BinarySearchTree_Node root)
		{
			if (root != null)
			{
				PostOrderTraverse(root.LeftChild);
				PostOrderTraverse(root.RightChild);
				Console.Write(root.Member);
			}
		}

		public void Clear()
		{
			root = null;
		}

	}
}
