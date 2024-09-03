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
    class BinarySearchTree_Node
    {
		private Member member; // value
		private BinarySearchTree_Node leftChild; // reference to its left child 
		private BinarySearchTree_Node rightChild; // reference to its right child

		public BinarySearchTree_Node(Member member)
		{
			this.member = member;
			leftChild = null;
			rightChild = null;
		}

		public Member Member
		{
			get { return member; }
			set { member = value; }
		}

		public BinarySearchTree_Node LeftChild
		{
			get { return leftChild; }
			set { leftChild = value; }
		}

		public BinarySearchTree_Node RightChild
		{
			get { return rightChild; }
			set { rightChild = value; }
		}


	}
}
