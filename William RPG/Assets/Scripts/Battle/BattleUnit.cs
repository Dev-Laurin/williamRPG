﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour {

	public Unit unit; 
	public bool isPlayer; 
	public bool isDodging; 

	PlayerHUD HUD; 


	public int GetHP(){
		return unit.hp; 
	}

	public string GetName(){
		return unit.name; 
	}

	//get the current speed, including status effects 
	public int GetSpeed(){
		return unit.speed; 
	}

	public void SetStats(Unit u, PlayerHUD hud = null, bool isP = false){
		unit = u; 
		HUD = hud; 
		isPlayer = isP; 
		SetHUD(); 
	}

	public string TakeDamage(int damage){
		if(isDodging){
			isDodging = false; 
			return "You dodged the attack.";
		}  
		unit.hp = unit.hp - (damage - unit.defense); 
		if(unit.hp <= 0){
			unit.hp = 0; 
		}
		SetHUD(); 
		return "The attack was successful."; 
	}

	public void Heal(int amount){
		unit.hp += amount; 
		if(unit.hp > unit.maxHP){
			unit.hp = unit.maxHP; 
		}
		SetHUD(); 
	}

	public void GainSP(int amount){
		unit.sp += amount; 
		if(unit.sp > unit.maxSP){
			unit.sp = unit.maxSP; 
		}
		SetHUD(); 
	}

	public void SetHUD(){
		if(HUD != null) HUD.SetHUD(unit); 
	}
}
