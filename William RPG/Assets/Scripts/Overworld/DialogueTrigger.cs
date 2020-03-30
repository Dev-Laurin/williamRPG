using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public TextAsset jsonFile; 

	void Start(){
		LoadCutsceneDialogue(); 
		TriggerDialogue(); 
		TriggerCutsceneActions(); 
	}

	public void LoadCutsceneDialogue(){
		//Load dialogue based on cutscene # 
		Cutscenes cutscenesObj = JsonUtility.FromJson<Cutscenes>(jsonFile.text); 
		int cutscene = Data.GetCurrentCutsceneIndex(); 

		//loop through cutscene dialogue 
		for(int i=0; i<cutscenesObj.cutscenes[cutscene].conversation.Count; i++){
			Dialogue dialogue = cutscenesObj.cutscenes[cutscene].conversation[i]; 
			FindObjectOfType<DialogueManager>().AddDialogueToQueue(dialogue); 
		}
	}

	public void TriggerDialogue(){
		FindObjectOfType<DialogueManager>().StartDialogue();  
	}

	public void TriggerCutsceneActions(){
		
	}
}
