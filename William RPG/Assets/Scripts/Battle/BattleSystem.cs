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
		int i = currentUnitIndex;  
		foreach (Transform child in turnCarousel.transform) {
			if(i >= u.Count){
				child.gameObject.GetComponent<Image>().sprite = defaultTurnImage;
				child.gameObject.SetActive(false); 
			} 
			else{
				child.gameObject.GetComponent<Image>().sprite = u[i].unit.battleHUDSprite; 
				child.gameObject.SetActive(true);  
			}
			i++; 
    	}
	}

	IEnumerator SetupBattle(){

		//Setup Player Party  
		for(int i=0; i<Party.Count; i++){
			//set the first 4 players on the battlefield
			GameObject playerObj = Instantiate(player, playerPositions[i]);
			playerObj.transform.position = playerPositions[i].position;  
			BattleUnit playerUnit = playerObj.GetComponent<BattleUnit>(); 
			playerUnit.SetStats(Party[i], playerHUDs[i], true); 
			players.Add(playerUnit); 

		}

		//Setup Enemy Party 
		for(int i=0; i<EnemyParty.Count; i++){
			GameObject enemyObj = Instantiate(enemy, enemyPositions[i]); 
			enemyObj.transform.position = enemyPositions[i].position; 
			BattleUnit enemyUnit = enemyObj.GetComponent<BattleUnit>(); 
			enemyUnit.SetStats(EnemyParty[i], playerHUDs[3]); //change the HUD
			enemies.Add(enemyUnit); 
			//set HUDS 
			
		}

		//hide any HUDS that are not being used 
		for(int i=Party.Count; i<playerHUDs.Count; i++){
			playerHUDs[i].SetActive(false); 
		}

		units = getTurnOrder();
		currentUnitIndex = 0;
		//update the turn list 
		SetTurnCarousel(units);

		//don't show the target until the player's turn
		targetIdentifier.SetActive(false); 
		targetIdentifier.transform.position = enemyPositions[targetPos].transform.position;

		//wait for user to press enter to go to next text 
		yield return waitForAnyKeyPress(); 
 
		SetupNextTurn(); 
	}

	//returns list of who goes next based on character's speed
	List<BattleUnit> getTurnOrder(){
		//combine lists to compare speed 
		var newList = players.Concat(enemies); 
		//sort by speed 
		return newList.OrderBy(s => s.GetSpeed()).ToList();  
	}

	void SetupNextTurn(){
		//if all the enemies are dead or if all the players are KO'd = end battle
		int dead = 0; 
		for(int i=0; i<players.Count; i++){
			if(players[i].GetHP() <= 0){
				//remove player from turn list 
				players.RemoveAt(i);
				i--;  
				dead++;	
			}   
		}
		if(dead == players.Count){
			//Game Over 
			state = BattleState.LOST; 
			StartCoroutine(EndBattle()); 
			return; 
		}
		//if all the enemies are dead 
		dead = 0; 
		for(int i=0; i<enemies.Count; i++){
			if(enemies[i].GetHP() <= 0){
				enemies.RemoveAt(i); 
				i--; 
				dead++; 
			} 
		}
		if(dead == enemies.Count){
			state = BattleState.WON; 
			StartCoroutine(EndBattle());
			return; 
		}

		//if we already went through everyone's turn 
		if(currentUnitIndex >= units.Count){
			units = getTurnOrder(); 
			currentUnitIndex = 0; 
			SetTurnCarousel(units); 
		}
		
		if(units[currentUnitIndex].isPlayer){
			state = BattleState.PLAYERTURN;  
			optionsMenu.SetActive(true); 
			PlayerTurn();
		} 
		else{
			state = BattleState.ENEMYTURN; 
			StartCoroutine(EnemyTurn()); 
		}
	}

	bool IsMiddleTarget(){
		//is our targetPos a middle target? 
		return (targetPos - 2) % 3 == 0; 
	}

	//vDir = 0 -- not diagonal
	//move the target to specific battlestations to select an enemy
	void moveTargetIdentifier(float hDir, float vDir){ //-1 for left 1 for right
		//loop through 
		if(hDir > 0){
			//go right
			targetPos++; 
		} 
		else{
			targetPos--; 
		}
		//make sure we can't go out of range 
		if(targetPos < 0) targetPos = 0; 
		if(targetPos >= enemyPositions.Count) targetPos = enemyPositions.Count - 1;
		
		//make sure we can't choose empty positions
		if(targetPos >= enemies.Count) targetPos = enemies.Count - 1; 
		targetIdentifier.transform.position = enemyPositions[targetPos].transform.position;
	}

	void PlayerTurn(){
		dialogueText.text = "Choose an action.";
	}

	void Update(){
		if(chooseTarget){
			if(Input.GetKeyUp(KeyCode.RightArrow)){
				moveTargetIdentifier(1.0f, 0.0f); 
			}
			else if(Input.GetKeyUp(KeyCode.LeftArrow)){
				moveTargetIdentifier(-1.0f, 0.0f); 
			}
			else if(Input.GetKey(KeyCode.Return)){
				//set the target 
				//deactivate the target object -- the player is done choosing
				targetIdentifier.SetActive(false); 
				chooseTarget = false;
			}
		}
	}

	IEnumerator PlayerAttack(){
		//set target over enemy battlestation 1
		dialogueText.text = "Choose a target.";
		targetIdentifier.transform.position = Vector3.MoveTowards(targetIdentifier.transform.position, enemyPositions[0].transform.position, 0.5f);
		targetIdentifier.SetActive(true); 
		optionsMenu.SetActive(false);
		chooseTarget = true;
		//StartCoroutine(chooseATarget()); 
		//wait for target to be chosen via Update func
		yield return new WaitUntil(() => chooseTarget == false); 
		//attack target 
		dialogueText.text = enemies[targetPos].TakeDamage(units[currentUnitIndex].unit.strength); 
		yield return waitForAnyKeyPress(); 
		EndPlayerTurn();
	}

	IEnumerator PlayerHeal(){
		optionsMenu.SetActive(false);
		var p = units[currentUnitIndex]; 
		p.Heal(5); 
		dialogueText.text = "You feel revived."; 
		yield return waitForAnyKeyPress();
		EndPlayerTurn();  
	}

	void EndPlayerTurn(){
		currentUnitIndex++; 
		//update the carousel 
		SetTurnCarousel(units);
		SetupNextTurn();
	}

	IEnumerator PlayerDodge(){
		optionsMenu.SetActive(false);
		var p = units[currentUnitIndex]; 
		p.isDodging = true;  
		dialogueText.text = "You loosen up and focus on dodging the next attack."; 
		yield return waitForAnyKeyPress(); 
		EndPlayerTurn(); 
	}

	IEnumerator EnemyTurn(){
		dialogueText.text = units[currentUnitIndex].GetName() + " attacks."; 
		yield return new WaitForSeconds(1);
		//yield return waitForAnyKeyPress();
		//choose a target 
		dialogueText.text = players[Random.Range(0, players.Count - 1)].TakeDamage(units[currentUnitIndex].unit.strength);   
		yield return new WaitForSeconds(1);
		currentUnitIndex++; 
		//update the carousel 
		SetTurnCarousel(units);
		SetupNextTurn();
	}

	IEnumerator EndBattle(){
		if(state == BattleState.WON){
			dialogueText.text = "You won!"; 
			yield return waitForAnyKeyPress(); 
			Data.RemoveEnemyParty(); 
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
