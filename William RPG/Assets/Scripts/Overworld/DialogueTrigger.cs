using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	void Start(){
		//Load dialogue based on cutscene # 

		//Example Dialogue
		Dialogue dialogue = new Dialogue();
		dialogue.name = "William"; 
		dialogue.sentences = new string [] {"Hello there mystery person I have never seen before.", 
		"Would you like to join my party? ", 
		"I have to save my favorite TV show from being canceled."}; 
		FindObjectOfType<DialogueManager>().AddDialogueToQueue(dialogue); 

		dialogue = new Dialogue();
		dialogue.name = "Mysterious Person"; 
		dialogue.sentences = new string [] {"Depends.", "Which show?"}; 
		FindObjectOfType<DialogueManager>().AddDialogueToQueue(dialogue);
		TriggerDialogue(); 
	}

	public void TriggerDialogue(){
		FindObjectOfType<DialogueManager>().StartDialogue();  
	}
}
