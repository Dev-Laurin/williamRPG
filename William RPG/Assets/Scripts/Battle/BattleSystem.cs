using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public enum BattleState {START, PLAYER1TURN, PLAYER2TURN, 
	PLAYER3TURN, PLAYER4TURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour {

	public GameObject player1; 
	public GameObject enemy; 

	Unit player1Unit; 
	Unit enemyUnit; 

	public BattleState state; 

	//Battle positions
	public Transform playerPos1; 
	public Transform playerPos2; 
	public Transform playerPos3; 
	public Transform playerPos4; 
	public Transform enemyPos1;

	public Text dialogueText; 

	public PlayerHUD player1HUD;  

	// Use this for initialization
	void Start () {
		state = BattleState.START;
		StartCoroutine(SetupBattle());  
	}

	private IEnumerator waitForKeyPress(KeyCode key){
		bool notPressed = true; 
		while(notPressed){
			if(Input.GetKeyDown(key)){
				notPressed = false; 
			}
			yield return null; //wait until next frame
		}
	}

	private IEnumerator waitForAnyKeyPress(){
		bool notPressed = true; 
		while(notPressed){
			if(Input.anyKey){
				notPressed = false; 
			}
			yield return null; //wait until next frame
		}
	}

	IEnumerator SetupBattle(){
		//put the players and enemies in their spots 
		GameObject player1Obj = Instantiate(player1, playerPos1); 
		player1Unit = player1Obj.GetComponent<Unit>(); 
		GameObject enemyObj = Instantiate(enemy, enemyPos1); 
		enemyUnit = enemyObj.GetComponent<Unit>(); 

		//dialogue beginning text 
		dialogueText.text = "A wild " + enemyUnit.name + " approaches..."; 

		//set references to HUD (status bars with hp, etc)
		player1HUD.SetHUD(player1Unit); 

		//wait for user to press enter to go to next text 
		yield return waitForAnyKeyPress(); 

		//change the state 
		state = BattleState.PLAYER1TURN; 
		PlayerTurn(); 
	}

	void PlayerTurn(){
		dialogueText.text = "Choose an action."; 
	}
	
}
