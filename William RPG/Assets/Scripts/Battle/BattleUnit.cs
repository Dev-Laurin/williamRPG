using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour {

	public Unit unit; 

	// Use this for initialization
	void Start () {
		//give the unit a special move 

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetStats(Unit u){
		unit = u; 
	}

	public bool TakeDamage(int damage){
		unit.hp = unit.hp - (damage - unit.defense); 
		if(unit.hp <= 0){
			return true; 
		}
		return false; 
	}

	public void Heal(int amount){
		unit.hp += amount; 
		if(unit.hp > unit.maxHP){
			unit.hp = unit.maxHP; 
		}
	}

	public void GainSP(int amount){
		unit.sp += amount; 
		if(unit.sp > unit.maxSP){
			unit.sp = unit.maxSP; 
		}
	}
}
