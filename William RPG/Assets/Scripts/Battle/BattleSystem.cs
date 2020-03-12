using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
using UnityEngine.UI; 

public enum BattleState {START, PLAYERTURN, ENEMYTURN, 
	WON, LOST}

public class BattleSystem : MonoBehaviour {

	public GameObject player; 
	public GameObject enemy; 

	List<BattleUnit> players = new List<BattleUnit>(); 
	List<Transform> playerPositions = new List<Transform>();
	List<PlayerHUD> playerHUDs = new List<PlayerHUD>();

	List<Transform> enemyPositions = new List<Transform>(); 
	List<BattleUnit> enemies = new List<BattleUnit>(); 

	public BattleState state; 

	//Battle positions
	public Transform playerPos1; 
	public Transform playerPos2; 
	public Transform playerPos3; 
	public Transform playerPos4; 
	public Transform enemyPos1;
	public Transform enemyPos2; 
	public Transform enemyPos3;
	public Transform enemyPos4;
	public Transform enemyPos5;

	public Text dialogueText; 

	public PlayerHUD player1HUD; 
	public PlayerHUD player2HUD;
	public PlayerHUD player3HUD;
	public PlayerHUD player4HUD; 

	public GameObject turnCarousel; 

	List<Unit>Party = Data.GetPlayerParty();
	List<Unit>EnemyParty = Data.GetEnemyParty(); 

	//the current unit's turn 
	int currentUnitIndex; 
	List<BattleUnit> currentTurns; //the list of who's going this round
	List<BattleUnit> units; 

	public GameObject targetIdentifier; 
	bool chooseTarget; //is player choosing a target?
	int targetPos; //position in battlestation array

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
			if(Input.GetKeyDown("space")){
				notPressed = false; 
			}
			yield return null; //wait until next frame
		}
	}

	void SetTurnCarousel(List<BattleUnit> u){
		//loop through turnCarouselImages
		int i = 0;  
		// foreach(Image item in turnCarousel){
		// 	item.sprite = u[i].battleHUDSprite;  
		// 	i++; 
		// }
		foreach (Transform child in turnCarousel.transform) {
			child.gameObject.GetComponent<Unit>().battleHUDSprite = u[i].unit.battleHUDSprite;  
			i++; 
    	}

	}

	IEnumerator SetupBattle(){
		
		playerPositions.Add(playerPos1); 
		playerPositions.Add(playerPos2); 
		playerPositions.Add(playerPos3);
		playerPositions.Add(playerPos4); 
 
		playerHUDs.Add(player1HUD); 
		playerHUDs.Add(player2HUD); 
		playerHUDs.Add(player3HUD); 
		playerHUDs.Add(player4HUD); 

		//Setup Player Party  
		for(int i=0; i<Party.Capacity; i++){
			//set the first 4 players on the battlefield
			GameObject playerObj = Instantiate(player, playerPositions[i]); 
			BattleUnit playerUnit = playerObj.GetComponent<BattleUnit>(); 
			playerUnit.SetStats(Party[i], true); 
			players.Add(playerUnit); 
			//set references to HUD (status bars with hp, etc) 
			playerHUDs[i].SetHUD(playerUnit); 
		}

		//Setup Enemy Party 
		for(int i=0; i<EnemyParty.Capacity; i++){
			GameObject enemyObj = Instantiate(enemy, enemyPositions[i]); 
			BattleUnit enemyUnit = enemyObj.GetComponent<BattleUnit>(); 
			enemyUnit.SetStats(EnemyParty[i]); 
			enemies.Add(enemyUnit); 
			//set HUDS 

		}

		//wait for user to press enter to go to next text 
		yield return waitForAnyKeyPress(); 

		//change the state
		units = getTurnOrder(); 
		if(units[0].isPlayer){
			state = BattleState.PLAYERTURN;  
			PlayerTurn(); 
		} 
		else{
			state = BattleState.ENEMYTURN; 
			EnemyTurn(); 
		}
		currentUnitIndex = 0;
	}

	//returns list of who goes next based on character's speed
	List<BattleUnit> getTurnOrder(){
		//combine lists to compare speed 
		var newList = players.Concat(enemies); 
		//sort by speed 
		return newList.OrderBy(s => s.GetSpeed()).ToList();  
	}

	void SetupNextTurn(){
		currentUnitIndex++; 
		//if we already went through everyone's turn 
		if(currentUnitIndex > units.Capacity){
			currentTurns = getTurnOrder(); 
		}
		else if(units[currentUnitIndex].isPlayer){
			state = BattleState.PLAYERTURN;  
			PlayerTurn(); 
		} 
		else{
			state = BattleState.ENEMYTURN; 
			EnemyTurn(); 
		}
		//update the carousel 
		SetTurnCarousel(currentTurns); 
	}

	bool IsMiddleTarget(){
		//is our targetPos a middle target? 
		return (targetPos - 2) % 3 == 0; 
	}

	//vDir = 0 -- not diagonal
	//move the target to specific battlestations to select an enemy
	void moveTargetIdentifier(int hDir, int vDir){ //-1 for left 1 for right
		if(IsMiddleTarget()){
			//go to next object based on diagonal
			if(vDir == -1 && hDir == 1 || vDir == 1 && hDir == -1){
				//down right or up left 
				targetPos += hDir * 2; 
			}
			else{
				targetPos += hDir; 
			}
		}
		else{
			//check if we are top or bottom 
			if(targetPos % 3 == 0){
				//we are the top 
				targetPos += hDir * 2; //skip
			}
			else{
				//we are the bottom 
				targetPos += hDir; //next 
			}
		}
		targetIdentifier.transform.position = Vector3.MoveTowards(targetIdentifier.transform.position, Battlestations[targetPos].transform.position, 0.5f); 
	}

	void Update(){
		//cycle through the enemy targets with left 
		//and right arrows
		if(state == BattleState.PLAYERTURN && chooseTarget){
			moveTargetIdentifier(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); 
			if(Input.GetKey(KeyCode.enter)){
				//set the target 
				Debug.Log("Chose " + targetPos); 
				//deactivate the target object -- the player is done choosing
				targetIdentifier.setActive(false); 
			}
		}
		
	}

	void PlayerTurn(){
		dialogueText.text = "Choose a target.";
		//set target over enemy battlestation 1
		targetIdentifier.transform.position = Vector3.MoveTowards(targetIdentifier.transform.position, enemyPos1, 0.5f);
		targetIdentifier.setActive(true); 
	}


	IEnumerator PlayerAttack(){
		bool isDead = target.TakeDamage(units[currentUnitIndex].unit.strength); 
		dialogueText.text = "The attack was successful."; 
		yield return waitForAnyKeyPress(); 
		if(isDead){
			state = BattleState.WON; 
			EndBattle(); 
		}
		else{
			SetupNextTurn(); 
		}
		
	}

	IEnumerator PlayerHeal(){
		player1Unit.Heal(5); 
		player1HUD.SetHUD(player1Unit); 
		dialogueText.text = "You feel revived."; 
		yield return waitForAnyKeyPress();

		SetupNextTurn(); 
		SetupNextTurn();  
	}

	IEnumerator PlayerDodge(){
		dialogueText.text = "You loosen up and focus on dodging the next attack."; 
		yield return waitForAnyKeyPress(); 
		SetupNextTurn(); 
	}

	IEnumerator EnemyTurn(){
		dialogueText.text = enemyUnit.name + " attacks."; 
		yield return waitForAnyKeyPress();  

		bool isDead = false; 
		if(dodging){
			//player doesn't take damage 
			dialogueText.text = "You moved out of the way.";  
		}
		else{
			isDead = player1Unit.TakeDamage(enemyUnit.unit.strength); 
			player1HUD.SetHUD(player1Unit); 

			dialogueText.text = "You were hit."; 
		}

		yield return waitForAnyKeyPress(); 
		if(isDead){
			state = BattleState.LOST; 
			EndBattle(); 
		}
		else{
			SetupNextTurn(); 
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
		
		if(state != BattleState.PLAYERTURN){
			return; 
		}

		StartCoroutine(PlayerAttack()); 
	}
	
	public void OnHealButton(){
		
		if(state != BattleState.PLAYERTURN){
			return; 
		}

		StartCoroutine(PlayerHeal()); 
	}
	
	public void OnDodgeButton(){
		
		if(state != BattleState.PLAYERTURN){
			return; 
		}

		StartCoroutine(PlayerDodge()); 
	}
}
