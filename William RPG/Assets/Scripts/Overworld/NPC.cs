using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class NPC : Unit {

	//interaction indicator 
	public GameObject indicator = new GameObject(); 
	public bool isEnabled; 
	public int triggersDialogueIndex;
	public int disappearsAfterDialogueIndex;  

	public NPC(string NAME, int HP, int MAXHP, 
		int MAXSP, int SP, int LEVEL, int DEFENSE, 
		int STRENGTH, int SPEED) : base(NAME, HP, MAXHP, 
		MAXSP, SP, LEVEL, DEFENSE, 
		STRENGTH, SPEED){}

	// Use this for initialization
	void Start () {
		//deactivate talking animation at first 
		indicator.SetActive(false);

		//Is this NPC here anymore?
		if(Data.GetCurrentCutsceneIndex() > disappearsAfterDialogueIndex && disappearsAfterDialogueIndex >= 0){
			isEnabled = false; 	
		} 
		gameObject.SetActive(isEnabled); 
	}

	void OnTriggerEnter2D(Collider2D other){
		//For talking indicator 
		try{
			if(other.gameObject.GetComponent<BattleUnit>().isPlayer){
				//show talking animation 
				indicator.SetActive(true);
			}
		}
		catch(System.Exception e){
			Debug.Log(e); 
		}
	}

	void OnTriggerExit2D(Collider2D other){
		indicator.SetActive(false);
	}
	
	void FixedUpdate(){
		if(Input.GetKeyUp("space")){
			if(indicator.activeSelf){
				//pass this object to Data in case cutscene needs it
				isEnabled = false; 
				SceneManager.LoadScene("Cutscene"); 
			}
		}
	}
}
