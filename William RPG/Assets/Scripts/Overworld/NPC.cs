using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class NPC : Unit {

	//interaction indicator 
	public GameObject indicator = new GameObject(); 

	public NPC(string NAME, int HP, int MAXHP, 
		int MAXSP, int SP, int LEVEL, int DEFENSE, 
		int STRENGTH) : base(NAME, HP, MAXHP, 
		MAXSP, SP, LEVEL, DEFENSE, 
		STRENGTH){}

	// Use this for initialization
	void Start () {
		//deactivate talking animation at first 
		indicator.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other){
		//For talking indicator 
		try{
			Player collidedUnit = other.gameObject.GetComponent<Player>();
			//show talking animation 
			indicator.SetActive(true); 
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
				SceneManager.LoadScene("Cutscene"); 
			}
		}
	}
}
