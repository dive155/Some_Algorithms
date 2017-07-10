using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aTree : MonoBehaviour {

	public GUIText textUI;
	public GUIText textUI2;
	public GUIText textUI3;
	public int[] inputs;
	public int searchVal;
	public int deleteVal;
	public int[] inputs2;

	public class Node {//a node of the tree
		public int index;
		public int value; 
		public Node left;
		public Node right;

		public void setValue(int val) {
			value = val;
		}
			
	}

	public class TheTree { //the tree
		public Node root;
		public GUIText textOut;

		public void AddRoot(int val) {
			Node newNode = new Node ();
			newNode.setValue(val);
			root = newNode;
		}

		public void Insert(Node current, int val) {
			if (current == null) { //if the tree is empty
				Node newNode = new Node (); //creating a new node
				newNode.setValue(val);
				root = newNode; //it's now the root
				return;
			}
				
			if (val <= current.value) { //if the val is less or equal than the current nodes value
				if (current.left != null) { //if there is a node already
					Insert (current.left, val); //go on to check it then
				} else { //if there is no node, attach the new one
					Node newNode = new Node (); //creating a new node
					newNode.setValue(val);
					current.left = newNode;
				}
			} else {
				if (current.right != null) {
					Insert (current.right, val);
				} else {
					Node newNode = new Node (); //creating a new node
					newNode.setValue(val);
					current.right = newNode;
				}
			}
		}

		public void PrintTree(Node current) {
			textOut.text += string.Format ("{0} \n", current.value);
			if (current.left != null) {
				textOut.text += " go left ";
				PrintTree (current.left);
			}
			if (current.right != null) {
				textOut.text += " go right ";
				PrintTree (current.right);
			}
			textOut.text += " go up \n";
		}

		public Node SearchTree(Node current, int val) { //search for a value
			if (current.value == val) { //if the value is found
				textOut.text += "The node \nwas found\n";
				return current;
			} else { //if it's not found
				if (val < current.value ) { //if it's less than current, search to the left
					if (current.left != null) { //if there are more nodes
						SearchTree (current.left, val); //search them
					} else { //if there are no nodes, then aint got eem
						textOut.text += "The node \nwas not found\n";
					}
				} else { //else to the right
					if (current.right != null) {//if there are more nodes
						SearchTree (current.right, val); //search them
					} else {//if there are no nodes, then aint got eem
						textOut.text += "The node \nwas not found\n";
					}
				}
			}
			return null;
		}

		public Node FindMin(Node current) {
			if (current.left != null) { //find the min below the current node
				return FindMin (current.left);
			} else {
				//textOut.text += string.Format ("The min is {0} \n", current.value);
				return current;
			}
		}

		public void Delete (Node current, Node parent, int val) { //removing a node
			if (val < current.value)
				Delete (current.left, current, val); //if val is smaller, search left
			else if (val > current.value)
				Delete (current.right, current, val); //if val is greater, search right
			else { //got eem
				if (current.left == null && current.right == null) { //if current has no children, just delete it
					//textOut.text += string.Format ("{0} has no children, removing\n", current.value);
					if (parent.left == current) //deleting the node by removing references to it
						parent.left = null; //i wish i could just use "delete", i miss c++ :(
					else
						parent.right = null;
				} else if (current.left == null) { //if it has one child on the right
					//textOut.text += string.Format ("{0} has a child on the right\n", current.value);
					current.value = current.right.value; //just attach the right subtree instead of this node
					current.left = current.right.left; //i tried current=current.right but it does not work
					current.right = current.right.right; //so i had to resort to doing it manually
				} else if (current.right == null) { //if it has one child on the left
					//textOut.text += string.Format ("{0} has a child on the left\n", current.value);
					current.value = current.left.value; //ditto
					current.right = current.left.right;
					current.left = current.left.left;
				} else { //if the node has two children
					Node temp = FindMin (current.right); //searching for minimum on the right subtree
					current.setValue (temp.value); //placing the min instead of the current node
					Delete (current.right, current, temp.value); //removing the min from where it used to be
				}
			}
		}

		/* Rotations:
          10       left--->        12
		 /  \                     /  \
		8   12                   10  13
		   /  \                 /  \
          11  13   <---right   8   11
        */

		public void RotateLeft (Node current) {
			if (current == null) //a little check
				return;
			if (current.right == null)
				return;

			Node temp = new Node (); //a temporary node
			int tempVal;

			tempVal = current.value; //switching 10 and 12
			current.value = current.right.value;
			current.right.value = tempVal;

			temp = current.right.left; //saving 11
			current.right.left = current.left; //attach 8 to 12
			current.left = current.right; //moving left subtree to the right
			current.right = current.left.right; //attaching 13 to 12
			current.left.right = temp; //bringing back 11
		}

		public void RotateRight (Node current) {
			if (current == null) //a little check
				return;
			if (current.left == null)
				return;

			Node temp = new Node (); //a temporary node
			int tempVal;

			tempVal = current.value; //switching 10 and 12
			current.value = current.left.value;
			current.left.value = tempVal;

			temp = current.left.right; //saving 11
			current.left.right = current.right; //attach 13 to 10
			current.right = current.left; // moving left subree to the right
			current.left = current.right.left; //attaching 8 to 12
			current.right.left = temp; //bringing back 11;

		}

		public void MakeBackbone() {
			Node current = root;
			while (current != null) {
				while (current.left != null) {
					RotateRight (current);
				}
				current = current.right;
			}
		}
			
	}
	// Use this for initialization
	void Start () {

		TheTree myTree = new TheTree ();
		myTree.textOut = textUI; //first tree

		for (int i = 0; i < inputs.Length; i++) {
			myTree.Insert (myTree.root, inputs [i]); //adding elements from the array into the list
		}

		myTree.PrintTree (myTree.root); //printing the tree
		myTree.SearchTree (myTree.root, searchVal); //searching

		myTree.textOut = textUI2; //more space to print the tree

		textUI2.text += string.Format ("Deleting {0} \n", deleteVal);
		myTree.Delete (myTree.root, null, deleteVal);
		myTree.PrintTree (myTree.root);

		TheTree newTree = new TheTree ();
		newTree.textOut = textUI3; //second tree

		for (int j = 0; j < inputs2.Length; j++) {
			newTree.Insert (newTree.root, inputs2 [j]); //adding elements from the array into the list
		}

		newTree.PrintTree (newTree.root);

		textUI3.text += "Backbone:\n";
		//newTree.RotateRight (newTree.root);
		newTree.MakeBackbone();
		newTree.PrintTree (newTree.root);
		//Day-Stout-Warren algorithm
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
