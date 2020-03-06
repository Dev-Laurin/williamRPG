using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public enum BattleState {START, PLAYER1TURN, PLAYER2TURN, 
	PLAYER3TURN, PLAYER4TURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour {

	public GameObject player1; 
	public GameObject enemy; 

	BattleUnit player1Unit; 
	BattleUnit enemyUnit; 

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
		player1Unit = player1Obj.GetComponent<BattleUnit>(); 
		GameObject enemyObj = Instantiate(enemy, enemyPos1); 
		enemyUnit = enemyObj.GetComponent<BattleUnit>(); 

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

	IEnumerator PlayerAttack(){
		bool isDead = enemyUnit.TakeDamage(player1Unit.strength); 
		dialogueText.text = "The attack was successful."; 
		yield return waitForAnyKeyPress(); 
		if(isDead){
			state = BattleState.WON; 
			EndBattle(); 
		}
		else{
			state = BattleState.ENEMYTURN; 
			StartCoroutine(EnemyTurn(false)); 
		}
	}

	IEnumerator PlayerHeal(){
		player1Unit.Heal(5); 
		player1HUD.SetHUD(player1Unit); 
		dialogueText.text = "You feel revived."; 
		yield return waitForAnyKeyPress(); 
		state = BattleState.ENEMYTURN; 
		StartCoroutine(EnemyTurn(false)); 
	}

	IEnumerator PlayerDodge(){
		dialogueText.text = "You loosen up and focus on dodging the next attack."; 
		yield return waitForAnyKeyPress(); 
		state = BattleState.ENEMYTURN; 
		StartCoroutine(EnemyTurn(true)); 
	}

	IEnumerator EnemyTurn(bool dodging){

		dialogueText.text = enemyUnit.name + " attacks."; 
		yield return waitForAnyKeyPress();  

		bool isDead = false; 
		if(dodging){
			//player doesn't take damage 
			dialogueText.text = "You moved out of the way.";  
		}
		else{
			isDead = player1Unit.TakeDamage(enemyUnit.strength); 
			player1HUD.SetHUD(player1Unit); 

			dialogueText.text = "You were hit."; 
		}

		yield return waitForAnyKeyPress(); 
		if(isDead){
			state = BattleState.LOST; 
			EndBattle(); 
		}
		else{
			state = BattleState.PLAYER1TURN; 
			PlayerTurn(); 
		}

	}

	void EndBattle(){
		if(state == BattleState.WON){
			dialogueText.text = "You won!"; 
		} else if(state == BattleState.LOST){
			dialogueText.text = "You were defeated."; 
		}
	}

	public void OnAttackButton(){
		
		if(state != BattleState.PLAYER1TURN){
			return; 
		}

		StartCoroutine(PlayerAttack()); 
	}
	
	public void OnHealButton(){
		
		if(state != BattleState.PLAYER1TURN){
			return; 
		}

		StartCoroutine(PlayerHeal()); 
	}
	
	public void OnDodgeButton(){
		
		if(state != BattleState.PLAYER1TURN){
			return; 
		}

		StartCoroutine(PlayerDodge()); 
	}
}
