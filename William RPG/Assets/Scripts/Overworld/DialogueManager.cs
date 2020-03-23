﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DialogueManager : MonoBehaviour {

	public Text nameText; 
	public Text dialogueText; 
	private Queue<string> sentences; 

	void Start(){
		sentences = new Queue<string>(); 
	}

	public void StartDialogue(Dialogue dialogue){
		nameText.text = dialogue.name;  
		//clear from previous conversation 
		sentences.Clear(); 

		foreach(string sentence in dialogue.sentences){
			sentences.Enqueue(sentence); 
		}

		DisplayNextSentence(); 
	}

	public void DisplayNextSentence(){
		if(sentences.Count == 0){
			EndDialogue(); 
			return; 
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines(); 
		StartCoroutine(TypeSentence(sentence)); 
	}

	IEnumerator TypeSentence(string sentence){
		dialogueText.text = ""; 
		foreach(char letter in sentence.ToCharArray()){
			dialogueText.text += letter;
			yield return null; //wait one frame  
		}
	}

	private void EndDialogue(){
		Debug.Log("This conversation is over."); 
	}
}
