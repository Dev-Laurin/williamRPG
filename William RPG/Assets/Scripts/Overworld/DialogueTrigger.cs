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
		Debug.Log(cutscenesObj.cutscenes.Count);  

		foreach(Dialogue dialogue in cutscenesObj.cutscenes[cutscene].conversation){
			FindObjectOfType<DialogueManager>().AddDialogueToQueue(dialogue); 
		}
		//loop through cutscene dialogue 

	}

	public void TriggerDialogue(){
		FindObjectOfType<DialogueManager>().StartDialogue();  
	}

	public void TriggerCutsceneActions(){
		
	}
}
