using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sort1 : MonoBehaviour {

	public GUIText textUI;

	public int[] ints;
	private int[] ints1;
	private int[] ints2;
	private int[] ints3;
	//private int[] pints = new int[10];

	// Use this for initialization
	void Start () {
		ints1 = ints; //making a copy of the source array to be used in sorting
		ints2 = ints;
		ints3 = ints;

		//InvokeRepeating("Iterate", 1.0f, 1.0f);
		textUI.text += "The source array is: \n";
		PrintArray (ints);

		textUI.text += "Sorted via bubble sort: \n";
		BubbleSort (ints1);
		PrintArray (ints1);

		textUI.text += "Sorted via merging sort: \n";
		MergeSort (ints2);
		PrintArray (ints2);

		textUI.text += "Sorted via quick sort: \n";
		QuickSort (ints3, 0, ints3.Length-1);
		PrintArray (ints3);

		textUI.text += "Fibonacci numbers: \n1 1 ";
		Fibonacci (1, 1);
		textUI.text += "\n";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//one iteration of doing things
	void Iterate() {
		//textUI.text += "dope stuff \n";
		PrintArray (ints);
		BubbleSort (ints);
	}

	//printing an array
	void PrintArray (int[] array)
	{
		for (int i = 0; i < array.Length; i++) {
			textUI.text += array [i];
			textUI.text += " ";
		}
		textUI.text += "\n";
	}

	//bubble sort
	void BubbleSort (int[] array) {
		int length = array.Length;
		int j = length - 1;
		do { 
			for (int i = 0; i < length - 1; i++) {
				if (array [i] > array [i + 1]) { //if the next one is smaller, then swap the two
					Swap (array, i, i+1);
				}
			}
			j--; //no need to check the last one again
		} while (j > 2);
	}

	void Swap (int[] array, int el1, int el2) { //swap two elements
		int k = array [el1];
		array [el1] = array [el2];
		array [el2] = k;
	}
		
	void Merge (int[] L, int[] R, int[] A) { //merge two arrays in increasing order
		int nl = L.Length; 
		int nr = R.Length;
		int i = 0;
		int j = 0;
		int k = 0;

		while (i < nl && j < nr) { //while there are unpicked elements
			if (L [i] <= R [j]) { //comparing smallest unpicked elements
				A [k] = L [i]; //if the element in the left array is smaller, put it into A
				k++; //next element in A
				i++; //smallest unpicked element  in L
			} else {
				A [k] = R [j];
				k++;
				j++;
			}
		}
		while (i < nl) { //if there are no elements left in R
			A [k] = L [i];
			i++;
			k++;
		}
		while (j < nr) { //if there are no elements left in L
			A [k] = R [j];
			j++;
			k++;
		}
	}

	void MergeSort (int[] A) {
		int n = A.Length;
		if (n < 2) //an array of length 1 is always sorted
			return;
		int mid = n / 2; //middle of the array
		int[] left = new int[mid]; //left half
		int[] right = new int[n - mid]; //right half
		for (int i = 0; i <= mid - 1; i++)
			left [i] = A [i]; //saving the halves
		for (int i = mid; i <= n - 1; i++)
			right [i - mid] = A [i];
		MergeSort (left); //sorting each half
		MergeSort (right);
		Merge (left, right, A); //merging them together
	}

	void QuickSort (int[] A, int start, int end) {
		if (start < end) {
			int pIndex = Partition (A, start, end); //dividing into two parts
			QuickSort (A, start, pIndex - 1); //sorting each part
			QuickSort (A, pIndex + 1, end);
		}
	}

	int Partition (int[] A, int start, int end){
		//choose an arbitrary element (pivot) (we pick the last element)
		//then arrange the array so that all the elements < pivot
		//are to the left of the pivot, and vice versa
		int pivot = A [end]; //pick the last element as pivot
		int pIndex = start; //starting comparing from the start
		for (int i = start; i <= end - 1; i++) {
			if (A [i] <= pivot) { //if an element is lesser than the pivot
				Swap (A, i, pIndex); //it should be to the left of it
				pIndex++; //keep organising
			}
		}
		Swap (A, pIndex, end); //putting the last element into the place
		return pIndex;
	}

	void Fibonacci(int a, int b) {
		int c = a + b; //sum of previous two numbers
		textUI.text += c;
		textUI.text += " ";
		if (c > 80) //end condition
			return;
		Fibonacci (b, c); //next number
	}

}
	