using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DialogueManager : MonoBehaviour {

	public Text nameText; 
	public Text dialogueText; 
	private Queue<string> sentences = new Queue<string>();
	//be able to do multiple speaker's dialogues, or switching back and forth 
	private Queue<Dialogue> dialogues = new Queue<Dialogue>(); 

	public void AddDialogueToQueue(Dialogue dialogue){
		//add to the queue
		dialogues.Enqueue(dialogue); 
	}

	//Pre: Need to load up dialogue queue before calling this 
	public void StartDialogue(){
		Dialogue dialogue = dialogues.Dequeue(); 
		Debug.Log(dialogue.name); 

		nameText.text = dialogue.name;  
		//clear from previous conversation 
		sentences.Clear(); 

		foreach(string sentence in dialogue.sentences){
			sentences.Enqueue(sentence); 
		}

		DisplayNextSentence(); 
	}

	public void DisplayNextSentence(){
		if(sentences.Count == 0 && dialogues.Count == 0){
			EndDialogue(); 
			return; 
		}
		else if(sentences.Count == 0){
			//go to next dialogue sequence 
			StartDialogue();  
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
		//Ending Action 
		PlayableUnit dell = new PlayableUnit("Dell", 20,
		20, 5, 5, 1, 1, 7, 3); 
		Data.AddToPlayerParty(dell);
		Data.GoToLastScene(); 
	}
}
