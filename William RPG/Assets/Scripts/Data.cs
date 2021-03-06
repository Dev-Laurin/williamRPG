﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data {

	private static List<PlayableUnit> playerParty = new List<PlayableUnit>(); 
	private static List<EnemyUnit> enemyParty = new List<EnemyUnit>(); 

	public static void AddToPlayerParty(PlayableUnit unit){ 
		playerParty.Add(unit); 
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


}
