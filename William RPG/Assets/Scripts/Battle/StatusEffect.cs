using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour {

	public string name; 
	public int damageOverTime; 
	public int durationInTurns; 

	public delegate void func(Unit u); 
	func function; 

	public void SetFunc(func f){
		function = f; 
	}
	
	public void GiveDamage(Unit unit){
		function(unit); 
	}

	public bool IsOver(int turn, int startingTurn){
		if(turn - startingTurn == durationInTurns){
			return true; 
		}
		return false; 
	}
}
