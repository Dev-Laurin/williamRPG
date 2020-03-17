using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data {

	private static List<Unit> playerParty = new List<Unit>(); 
	private static List<Unit> enemyParty = new List<Unit>(); 

	public static void AddToPlayerParty(Unit unit){ 
		playerParty.Add(unit); 
		SortPlayerParty(); 
	}

	public static void RemoveFromPlayerParty(Unit unit){
		Unit unitToRemove = playerParty.Find(u => u.name == unit.name); 
		playerParty.Remove(unitToRemove); 
	}

	public static void SortPlayerParty(){
		playerParty.Sort(delegate(Unit x, Unit y){
			if(x.partyPos == y.partyPos) return 0; 
			else if(x.partyPos > y.partyPos) return 1; 
			else if(x.partyPos < y.partyPos) return -1; 
			else return x.partyPos.CompareTo(y.partyPos); 
		}); 

	}

	public static void UpdatePlayerUnit(Unit unit){
		int index = playerParty.FindIndex(u => u.name == unit.name); 
		playerParty[index] = unit; 
	}

	public static void UpdatePlayerPartyStats(List<Unit> party){
		playerParty = party; 
	}

	public static void UpdatePlayerPartyPos(Unit unit, int pos){
		unit.partyPos = pos; 
		SortPlayerParty(); 
	}

	public static List<Unit> GetPlayerParty(){
		return playerParty; 
	}

	public static void StoreCollidedEnemy(Unit enemy){
		enemyParty.Add(enemy); 
	}

	public static List<Unit> GetEnemyParty(){
		return enemyParty; 
	}


}
