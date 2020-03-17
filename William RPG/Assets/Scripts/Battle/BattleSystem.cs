using System.Collections;
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public enum BattleState {START, PLAYERTURN, ENEMYTURN, 
	WON, LOST}

public class BattleSystem : MonoBehaviour {

	public GameObject player; 
	public GameObject enemy; 

	List<BattleUnit> players = new List<BattleUnit>(); 
	
	//Battle positions
	public List<Transform> playerPositions;
	
	public Sprite defaultTurnImage; 
	public List<PlayerHUD> playerHUDs;

	public List<Transform> enemyPositions;
	//enemies = organized by battlestation position
	List<BattleUnit> enemies = new List<BattleUnit>(); 

	public BattleState state; 

	public Text dialogueText; 

	public GameObject turnCarousel; 

	List<Unit>Party = Data.GetPlayerParty();
	List<Unit>EnemyParty = Data.GetEnemyParty(); 

	//the current unit's turn 
	int currentUnitIndex; 
	List<BattleUnit> units; 

	public GameObject targetIdentifier; 
	bool chooseTarget; //is player choosing a target?
	int targetPos; //position in battlestation array

	public GameObject optionsMenu; 

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
			if(i >= u.Count){
				child.gameObject.GetComponent<Image>().sprite = defaultTurnImage; 	
			} 
			else{
				child.gameObject.GetComponent<Image>().sprite = u[i].unit.battleHUDSprite;  
			}
			i++; 
    	}
	}

	IEnumerator SetupBattle(){

		//Setup Player Party  
		for(int i=0; i<Party.Count; i++){
			//set the first 4 players on the battlefield
			GameObject playerObj = Instantiate(player, playerPositions[i]); 
			BattleUnit playerUnit = playerObj.GetComponent<BattleUnit>(); 
			playerUnit.SetStats(Party[i], playerHUDs[i], true); 
			players.Add(playerUnit); 

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
		currentUnitIndex = 0;
		StartCoroutine(SetupNextTurn()); 
	}

	//returns list of who goes next based on character's speed
	List<BattleUnit> getTurnOrder(){
		//combine lists to compare speed 
		var newList = players.Concat(enemies); 
		//sort by speed 
		return newList.OrderBy(s => s.GetSpeed()).ToList();  
	}

	IEnumerator SetupNextTurn(){
		Debug.Log("Setup turn"); 
		Debug.Log("current unit index: " + currentUnitIndex); 

		//if all the enemies are dead or if all the players are KO'd = end battle
		int dead = 0; 
		for(int i=0; i<players.Count; i++){
			if(players[i].isDead) dead++;  
		}
		if(dead == players.Count){
			//Game Over 
			state = BattleState.LOST; 
			yield return EndBattle(); 
			yield break; 
		}
		//if all the enemies are dead 
		dead = 0; 
		for(int i=0; i<enemies.Count; i++){
			if(enemies[i].isDead) dead++; 
		}
		if(dead == enemies.Count){
			state = BattleState.WON; 
			yield return EndBattle();
			yield break; 
		}

		//if we already went through everyone's turn 
		if(currentUnitIndex >= units.Count){
			units = getTurnOrder(); 
			currentUnitIndex = 0; 
		}
		
		if(units[currentUnitIndex].isPlayer){
			state = BattleState.PLAYERTURN;  
			Debug.Log("Player turn"); 
			optionsMenu.SetActive(true); 
			PlayerTurn(); //need to yield / setup turns
			yield; 
			currentUnitIndex++; 
		} 
		else{
			state = BattleState.ENEMYTURN; 
			Debug.Log("Enemy turn."); 
			yield return EnemyTurn(); 
			currentUnitIndex++; 
		}
		//update the carousel 
		SetTurnCarousel(units);
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
		targetIdentifier.transform.position = Vector3.MoveTowards(targetIdentifier.transform.position, enemyPositions[targetPos].transform.position, 0.5f); 
	}

	void FixedUpdate(){
		//cycle through the enemy targets with left 
		//and right arrows
		if(state == BattleState.PLAYERTURN && chooseTarget){
			moveTargetIdentifier((int)Input.GetAxis("Horizontal"), (int)Input.GetAxis("Vertical")); 
			if(Input.GetKey(KeyCode.Return)){
				//set the target 
				//deactivate the target object -- the player is done choosing
				targetIdentifier.SetActive(false); 
				chooseTarget = false; 
			}
		}
		
	}

	void PlayerTurn(){
		dialogueText.text = "Choose an action.";
		//set target over enemy battlestation 1
		targetIdentifier.transform.position = Vector3.MoveTowards(targetIdentifier.transform.position, enemyPositions[0].transform.position, 0.5f);
		targetIdentifier.SetActive(true); 
	}


	IEnumerator PlayerAttack(){
		chooseTarget = true;
		//wait for target to be chosen via Update func
		yield return new WaitUntil(() => chooseTarget == false); 
		//attack target 
		Debug.Log("target pos: " + targetPos); 
		var target = enemies[targetPos]; 
		Debug.Log("current unit index: " + currentUnitIndex); 
		target.TakeDamage(units[currentUnitIndex].unit.strength); 
		dialogueText.text = "The attack was successful."; 
		yield return waitForAnyKeyPress(); 
		optionsMenu.SetActive(false);
		StartCoroutine(SetupNextTurn());
	}

	IEnumerator PlayerHeal(){
		var p = units[currentUnitIndex]; 
		p.Heal(5); 
		p.SetHUD(); 
		dialogueText.text = "You feel revived."; 
		yield return waitForAnyKeyPress();
		optionsMenu.SetActive(false);
		StartCoroutine(SetupNextTurn());
	}

	IEnumerator PlayerDodge(){
		dialogueText.text = "You loosen up and focus on dodging the next attack."; 
		yield return waitForAnyKeyPress(); 
		optionsMenu.SetActive(false);
		StartCoroutine(SetupNextTurn());
	}

	IEnumerator EnemyTurn(){
		dialogueText.text = units[currentUnitIndex].GetName() + " attacks."; 
		yield return waitForAnyKeyPress();
		//choose a target 
		BattleUnit target = players[Random.Range(0, players.Count - 1)]; 
		target.TakeDamage(units[currentUnitIndex].unit.strength);  
		dialogueText.text = "You were hit."; 
		yield return waitForAnyKeyPress();
		StartCoroutine(SetupNextTurn());
	}

	IEnumerator EndBattle(){
		if(state == BattleState.WON){
			dialogueText.text = "You won!"; 
			yield return waitForAnyKeyPress(); 
			SceneManager.LoadScene("Overworld"); 
		} else if(state == BattleState.LOST){
			dialogueText.text = "You were defeated."; 
			yield return waitForAnyKeyPress();
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
