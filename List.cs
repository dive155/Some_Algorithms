using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class List : MonoBehaviour {
	public GUIText textUI;

	public int[] elements;
	public int insertionIndex;
	public int insertionValue;
	public int removalIndex;
	public int searchValue;

	public class Item {//an item for the list
		public int index;
		public int value; 
		public Item next;

		public void setValue(int val) {
			value = val;
		}

		public void setIndex(int val) {
			index = val;
		}
	}

	public class TheList { //the list itself
		private Item head;
		public int length = 0;

		public string PrintList(){ //printing the list
			string OutString;
			OutString = string.Format("Length of the list: {0}.\n", length);  //the header

			Item current = head; //starting from the first element
			while (current != null) {
				OutString += string.Format("List[{0}]: {1}\n", current.index, current.value); //writing the value of the current element
				current = current.next;
			}
			OutString += "\n";
			return OutString;
		}

		public void addEnd(int val) { //adding an element to the end of the list
			Item newElem = new Item (); //creating new element
			newElem.setValue (val); //setting the value
			newElem.next = null; //nothing follows the element
			length++; //incrementing the length of the list

			if (head == null) { //if it's the first element
				newElem.setIndex(0); 
				head = newElem; //saving it as the first element
			} else { //if it's not the first element
				int i = 1;
				Item current = head; 
				while (current.next != null) {//searching for the last element
					current = current.next;
					i++;
				}
				newElem.setIndex (i); //assigning the index
				current.next = newElem; //attaching the new element to the last one
			}
		}

		public void Insert (int ind, int val){
			if (ind > length - 1) //making sure we're not out of range
				return;

			Item newElem = new Item (); //creating new element
			newElem.setValue (val);


			if (ind == 0) { //if inserting to the head of the list
				newElem.next = head; //the next element
				head = newElem; //we have a new head
				FixIndexes ();
				length++;
				return;
			}
				
			Item current = head;
			for (int i = 0; i < ind-1; i++) {
				current = current.next; //moving to the element ind-1
			}

			Item nextElem = current.next; //remembering the next element
			current.next = newElem; //putting the new one in it's place
			newElem.next = nextElem; //attaching the old next element to the newly inserted one
			length++;
			FixIndexes ();
		}

		public void Remove (int ind) {
			if (ind > length - 1) //making sure we're not out of range
				return;

			Item current = head;
			length--;

			if (ind == 0) { //if removing the first element
				head = head.next;
				FixIndexes ();
				return;
			}

			for (int i = 0; i < ind-1; i++) {
				current = current.next; //moving to the element ind-1
			}

			Item nextElem = current.next;
			nextElem = nextElem.next; //finding an element after the one we delete
			current.next = nextElem; //fixing our chain

			FixIndexes ();
		}

		public void FixIndexes() { //making sure indexes are ok
			Item current = head; 
			int j = 0;
			while (current != null) { //just go through all the elements
				current.setIndex (j); //and assign indexes to them
				current = current.next;
				j++;
			}
		}

		public int SearchValue (int val) {  //searching by value
			Item current = head; //starting from the first element
			while (current != null) { //searching through elements
				if (current.value == val) { //got eem!
					return current.index;
				}
				current = current.next;
			}
			return -1; //aint got eem, error code
		}

	}
	// Use this for initialization
	void Start () {
		TheList myList = new TheList ();
		for (int i = 0; i < elements.Length; i++) {
			myList.addEnd (elements [i]); //adding elements from the array into the list
		}

		textUI.text += "The source list:\n";
		textUI.text += myList.PrintList();

		myList.Insert (insertionIndex, insertionValue);
		textUI.text += string.Format("Inserting the value {0} into the spot indexed {1}:\n", insertionValue, insertionIndex); //printing the message
		textUI.text += myList.PrintList();

		myList.Remove (removalIndex);
		textUI.text += string.Format("Removing the item indexed {0}:\n", removalIndex);
		textUI.text += myList.PrintList();

		int searchInd = myList.SearchValue (searchValue);
		if (searchInd == -1) {
			textUI.text += string.Format ("An element with value {0} was not found.\n", searchValue);
		} else {
			textUI.text += string.Format ("An element with value {0} was found at index {1}.\n", searchValue, searchInd);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
