using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public static class Data {

	private static List<BattleUnit> playerParty = new List<BattleUnit>(); 
	private static List<BattleUnit> enemyParty = new List<BattleUnit>(); 

	//Placeholder for file saving 
	static string SceneName = "Overworld"; 

	static int cutsceneIndex = 0; 

	//on construction
	static Data(){
		//Add will to the party -- REMOVE THIS LATER
		AddToPlayerParty(GameObject.Find("Will").GetComponent<BattleUnit>()); 
	}

	public static void AddToPlayerParty(BattleUnit unit){ 
		BattleUnit newUnit = unit; 
		playerParty.Add(newUnit); 
		SortPlayerParty(); 
	}

	public static void RemoveFromPlayerParty(BattleUnit unit){
		BattleUnit unitToRemove = playerParty.Find(u => u.stats.name == unit.stats.name); 
		playerParty.Remove(unitToRemove); 
	}

	public static void SortPlayerParty(){
		playerParty.Sort(delegate(BattleUnit x, BattleUnit y){
			if(x.partyPosition == y.partyPosition) return 0; 
			else if(x.partyPosition > y.partyPosition) return 1; 
			else if(x.partyPosition < y.partyPosition) return -1; 
			else return x.partyPosition; 
		}); 
	}

	public static void UpdatePlayerUnit(BattleUnit character){
		int index = playerParty.FindIndex(u => u.stats.name == character.stats.name); 
		playerParty[index] = character; 
	}

	public static void UpdatePlayerPartyStats(List<BattleUnit> party){
		playerParty = party; 
	}

	public static void UpdatePlayerPartyPos(BattleUnit unit, int pos){
		unit.partyPosition = pos; 
		SortPlayerParty(); 
	}

	public static List<BattleUnit> GetPlayerParty(){
		return playerParty; 
	}

	public static void EmptyPlayerParty(){
		playerParty = new List<BattleUnit>(); 
	}

	public static void StoreCollidedEnemy(BattleUnit enemy){
		enemyParty.Add(enemy); 
	}

	public static List<BattleUnit> GetEnemyParty(){
		return enemyParty; 
	}

	public static void RemoveEnemyParty(){
		enemyParty = new List<BattleUnit>(); 
	}

	public static void SaveGame(){
		//what scene name 
		SceneName =  SceneManager.GetActiveScene().name; 

		//players' positions

		//enemies' positions

		//npcs' positions 

		//camera position 

		//items 

		//player party - stats, positions

		//save to json file 

	}

	public static void LoadGame(){
		SceneManager.LoadScene("Overworld"); 

		//load player's positions 
		for(int i=0; i<playerParty.Count; i++){

		}
	}

	//coming back from a cutscene 
	public static void GoToLastScene(){
		//load the game state of the last scene 
		LoadGame(); 
	}

	public static void IncrementCutsceneIndex(){
		cutsceneIndex++; 
	}

	public static int GetCurrentCutsceneIndex(){
		return cutsceneIndex; 
	}

}
