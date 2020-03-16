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
	
	//Battle positions
	static public Transform playerPos1; 
	static public Transform playerPos2; 
	static public Transform playerPos3; 
	static public Transform playerPos4; 
	public List<Transform> playerPositions = new List<Transform>{playerPos1, playerPos2, playerPos3, playerPos4};
	
	static public PlayerHUD player1HUD; 
	static public PlayerHUD player2HUD;
	static public PlayerHUD player3HUD;
	static public PlayerHUD player4HUD; 
	public List<PlayerHUD> playerHUDs = new List<PlayerHUD>{player1HUD, player2HUD, player3HUD, player4HUD};

	static public Transform enemyPos1;
	static public Transform enemyPos2; 
	static public Transform enemyPos3;
	static public Transform enemyPos4;
	static public Transform enemyPos5;
	List<Transform> enemyPositions = new List<Transform>{enemyPos1, enemyPos2, enemyPos3, enemyPos4, enemyPos5}; 
	//enemies = organized by battlestation position
	List<BattleUnit> enemies = new List<BattleUnit>(); 

	public BattleState state; 

	public Text dialogueText; 

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
		//hide target identifier
		targetIdentifier.SetActive(false); 
		Party = Data.GetPlayerParty();
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

		//Setup Player Party  
		for(int i=0; i<Party.Count; i++){
			//set the first 4 players on the battlefield
			GameObject playerObj = Instantiate(player, playerPositions[i]); 
			BattleUnit playerUnit = playerObj.GetComponent<BattleUnit>(); 
			Debug.Log(Party.Count); 
			playerUnit.SetStats(Party[i], true); 
			players.Add(playerUnit); 
			//set references to HUD (status bars with hp, etc) 
			playerHUDs[i].SetHUD(playerUnit); 
		}

		//Setup Enemy Party 
		for(int i=0; i<EnemyParty.Count; i++){
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
		if(currentUnitIndex > units.Count){
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
		if(targetPos < 0) targetPos = 0; 
		if(targetPos >= enemyPositions.Count) targetPos = enemyPositions.Count - 1;
		Debug.Log("Target Position Index: " + targetPos); 
		Debug.Log("Our pos: " + targetIdentifier.transform.position); 
		Debug.Log("Target pos: " + enemyPositions[targetPos].transform.position);
		targetIdentifier.transform.position = Vector3.MoveTowards(targetIdentifier.transform.position, enemyPositions[targetPos].transform.position, 0.5f); 
	}

	void FixedUpdate(){
		//cycle through the enemy targets with left 
		//and right arrows
		if(state == BattleState.PLAYERTURN && chooseTarget){
			moveTargetIdentifier((int)Input.GetAxis("Horizontal"), (int)Input.GetAxis("Vertical")); 
			if(Input.GetKey(KeyCode.Return)){
				//set the target 
				Debug.Log("Chose " + targetPos); 
				//deactivate the target object -- the player is done choosing
				targetIdentifier.SetActive(false); 
				chooseTarget = false; 
			}
		}
		
	}

	void PlayerTurn(){
		dialogueText.text = "Choose an action.";
		//set target over enemy battlestation 1
		targetIdentifier.transform.position = Vector3.MoveTowards(targetIdentifier.transform.position, enemyPos1.transform.position, 0.5f);
		targetIdentifier.SetActive(true); 
	}


	IEnumerator PlayerAttack(){
		chooseTarget = true;
		//wait for target to be chosen via Update func
		yield return new WaitUntil(() => chooseTarget == false); 
		//attack target 
		var target = enemies[targetPos]; 
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
		var p = units[currentUnitIndex]; 
		p.Heal(5); 
		p.SetHUD(); 
		dialogueText.text = "You feel revived."; 
		yield return waitForAnyKeyPress();
		SetupNextTurn(); 
	}

	IEnumerator PlayerDodge(){
		dialogueText.text = "You loosen up and focus on dodging the next attack."; 
		yield return waitForAnyKeyPress(); 
		SetupNextTurn(); 
	}

	IEnumerator EnemyTurn(){
		dialogueText.text = units[currentUnitIndex].GetName() + " attacks."; 
		yield return waitForAnyKeyPress();  

		//choose a target 
		BattleUnit target = players[Random.Range(0, players.Count - 1)]; 
		bool isDead = target.TakeDamage(units[currentUnitIndex].unit.strength);  
		dialogueText.text = "You were hit."; 

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
