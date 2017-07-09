using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aTree : MonoBehaviour {

	public GUIText textUI;
	public GUIText textUI2;
	public int[] inputs;
	public int searchVal;
	public int deleteVal;

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

		public void SearchTree(Node current, int val) { //search for a value
			if (current.value == val) { //if the value is found
				textOut.text += "The element was found\n";
			} else { //if it's not found
				if (val < current.value ) { //if it's less than current, search to the left
					if (current.left != null) { //if there are more nodes
						SearchTree (current.left, val); //search them
					} else { //if there is no nodes, then aint got eem
						textOut.text += "The element was not found\n";
					}
				} else { //else to the right
					if (current.right != null) {
						SearchTree (current.right, val);
					} else {
						textOut.text += "The element was not found\n";
					}
				}
			}
		}

		public Node FindMin(Node current) {
			if (current.left != null) { //find the min below the current node
				return FindMin (current.left);
			} else {
				//textOut.text += string.Format ("The min is {0} \n", current.value);
				return current;
			}
		}

		public void Delete (Node current, int val) {
			if (val < current.value)
				Delete (current.left, val); //if val is smaller, search left
			else if (val > current.value)
				Delete (current.right, val); //if val is greater, search right
			else { //got eem
				if (current.left == null && current.right == null) {
					textOut.text += string.Format ("{0} has no children, removing\n", current.value);
					current = null; //if current has no children, just delete it
				} else if (current.left == null) { //if it has one child on the right
					textOut.text += string.Format ("{0} has a child on the right\n", current.value);
					current = current.right; //just attach the right subtree instead of this node
				} else if (current.right == null) { //if it has one child on the left
					textOut.text += string.Format ("{0} has a child on the left\n", current.value);
					current = current.left; //ditto
				} else {
					Node temp = FindMin (current.right);
					current.setValue (temp.value);
					Delete (current.right, temp.value);
				}
			}

		}
	}
	// Use this for initialization
	void Start () {

		TheTree myTree = new TheTree ();
		myTree.textOut = textUI;

		for (int i = 0; i < inputs.Length; i++) {
			myTree.Insert (myTree.root, inputs [i]); //adding elements from the array into the list
		}

		myTree.PrintTree (myTree.root);
		myTree.SearchTree (myTree.root, searchVal);
		myTree.FindMin (myTree.root.right.right);

		myTree.textOut = textUI2;
		myTree.Delete (myTree.root, deleteVal);
		myTree.PrintTree (myTree.root);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
