using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public TextAsset jsonFile; 

	void Start(){
		//Load dialogue based on cutscene # 
		Cutscenes cutscenesObj = JsonUtility.FromJson<Cutscenes>(jsonFile.text); 
		int cutscene = Data.GetCurrentCutsceneIndex();  

		//loop through cutscene dialogue 
		foreach(Dialogue dialogue in cutscenesObj.cutscenes[cutscene].conversation){
			FindObjectOfType<DialogueManager>().AddDialogueToQueue(dialogue); 
		}

		//Start the cutscene 
		TriggerDialogue(); 

		//Run actions
		FindObjectOfType<DialogueManager>().AddActions(cutscenesObj.cutscenes[cutscene].actions); 

		Data.IncrementCutsceneIndex(); 
	}

	public void TriggerDialogue(){
		FindObjectOfType<DialogueManager>().StartDialogue();  
	}
}
