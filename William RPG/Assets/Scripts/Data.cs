using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public static class Data {

	private static List<GameObject> playerParty = new List<GameObject>(); 
	private static List<GameObject> enemyParty = new List<GameObject>(); 

	//Placeholder for file saving 
	static string SceneName = "Overworld"; 

	static int cutsceneIndex = 0; 

	//on construction
	static Data(){
		//Add will to the party -- REMOVE THIS LATER
		AddToPlayerParty(GameObject.Find("Will")); 
	}

	public static void AddToPlayerParty(GameObject unit){ 
		playerParty.Add(unit); 
		SortPlayerParty(); 
	}

	public static void RemoveFromPlayerParty(GameObject unit){
		GameObject unitToRemove = playerParty.Find(u => u.GetComponent<Stats>().name == unit.GetComponent<Stats>().name); 
		playerParty.Remove(unitToRemove); 
	}

	public static void SortPlayerParty(){
		playerParty.Sort(delegate(GameObject x, GameObject y){
			if(x.GetComponent<BattleUnit>().partyPosition == y.GetComponent<BattleUnit>().partyPosition) return 0; 
			else if(x.GetComponent<BattleUnit>().partyPosition > y.GetComponent<BattleUnit>().partyPosition) return 1; 
			else if(x.GetComponent<BattleUnit>().partyPosition < y.GetComponent<BattleUnit>().partyPosition) return -1; 
			else return x.GetComponent<BattleUnit>().partyPosition.CompareTo(y.GetComponent<BattleUnit>().partyPosition); 
		}); 
	}

	public static void UpdatePlayerUnit(GameObject character){
		int index = playerParty.FindIndex(u => u.GetComponent<Stats>().name == character.GetComponent<Stats>().name); 
		playerParty[index] = character; 
	}

	public static void UpdatePlayerPartyStats(List<GameObject> party){
		playerParty = party; 
	}

	public static void UpdatePlayerPartyPos(GameObject unit, int pos){
		unit.GetComponent<BattleUnit>().partyPosition = pos; 
		SortPlayerParty(); 
	}

	public static List<GameObject> GetPlayerParty(){
		return playerParty; 
	}

	public static void EmptyPlayerParty(){
		playerParty = new List<GameObject>(); 
	}

	public static void StoreCollidedEnemy(GameObject enemy){
		enemyParty.Add(enemy); 
	}

	public static List<GameObject> GetEnemyParty(){
		return enemyParty; 
	}

	public static void RemoveEnemyParty(){
		enemyParty = new List<GameObject>(); 
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
