using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public static class Data {

	private static List<PlayableUnit> playerParty = new List<PlayableUnit>(); 
	private static List<EnemyUnit> enemyParty = new List<EnemyUnit>(); 

	//Placeholder for file saving 
	static string SceneName = "Overworld"; 

	static int cutsceneIndex = 0; 

	//on construction
	static Data(){
		AddToPlayerParty(GameObject.Find("Player").GetComponent<Player>()); 
	}

	public static void AddToPlayerParty(PlayableUnit unit){ 
		PlayableUnit u = new PlayableUnit(unit.name, unit.hp, 
		unit.maxHP, unit.maxSP, unit.sp, unit.level, unit.defense,
		unit.strength, unit.speed); 
		playerParty.Add(u); 
		SortPlayerParty(); 
	}

	public static void RemoveFromPlayerParty(PlayableUnit unit){
		PlayableUnit unitToRemove = playerParty.Find(u => u.name == unit.name); 
		playerParty.Remove(unitToRemove); 
	}

	public static void SortPlayerParty(){
		playerParty.Sort(delegate(PlayableUnit x, PlayableUnit y){
			if(x.partyPos == y.partyPos) return 0; 
			else if(x.partyPos > y.partyPos) return 1; 
			else if(x.partyPos < y.partyPos) return -1; 
			else return x.partyPos.CompareTo(y.partyPos); 
		}); 

	}

	public static void UpdatePlayerUnit(PlayableUnit unit){
		int index = playerParty.FindIndex(u => u.name == unit.name); 
		playerParty[index] = unit; 
	}

	public static void UpdatePlayerPartyStats(List<PlayableUnit> party){
		playerParty = party; 
	}

	public static void UpdatePlayerPartyPos(PlayableUnit unit, int pos){
		unit.partyPos = pos; 
		SortPlayerParty(); 
	}

	public static List<PlayableUnit> GetPlayerParty(){
		return playerParty; 
	}

	public static void EmptyPlayerParty(){
		playerParty = new List<PlayableUnit>(); 
	}

	public static void StoreCollidedEnemy(EnemyUnit enemy){
		enemyParty.Add(enemy); 
	}

	public static List<EnemyUnit> GetEnemyParty(){
		return enemyParty; 
	}

	public static void RemoveEnemyParty(){
		enemyParty = new List<EnemyUnit>(); 
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
