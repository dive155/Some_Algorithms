using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aTree : MonoBehaviour {

	public GUIText textUI;

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

		public void PrintTree(Node current, string outString) {
			outString += string.Format ("{0} ", current.value);
			textOut.text += string.Format ("{0} ", current.value);
			if (current.left != null)
				PrintTree (current.left, outString);
			if (current.right != null)
				PrintTree (current.right, outString);
		}

	}
	// Use this for initialization
	void Start () {
		textUI.text += "soop";

		TheTree myTree = new TheTree ();
		myTree.textOut = textUI;
		myTree.Insert (myTree.root, 10);
		myTree.Insert (myTree.root, 8);
		myTree.Insert (myTree.root, 12);
		myTree.Insert (myTree.root, 7);
		myTree.Insert (myTree.root, 9);

		textUI.text += "woop";

		string outString = "";
		myTree.PrintTree (myTree.root, outString);
		textUI.text += outString;

		textUI.text += "doop";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
