using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour {
	Animator animator; 
	public Animator HUDAnimator; 
	Stats stats; 

	bool isDodging; 

	void Start(){
		//Set animator
		animator = GetComponent<Animator>(); 
		stats = GetComponent<Stats>(); 
	}

	public int GetHP(){
		return stats.hp; 
	}

	public string GetName(){
		return stats.name; 
	}

	//get the current speed, including status effects 
	public int GetSpeed(){
		return stats.speed; 
	}

	public void SetStats(){
		SetHUD(); 
	}

	public string TakeDamage(int damage){
		if(isDodging){
			isDodging = false; 
			return "You dodged the attack.";
		}  
		stats.hp = stats.hp - (damage - stats.defense); 
		if(stats.hp <= 0){
			stats.hp = 0; 
		}
		SetHUD(); 
		return "The attack was successful."; 
	}

	public void Heal(int amount){
		stats.hp += amount; 
		if(stats.hp > stats.maxHP){
			stats.hp = stats.maxHP; 
		}
		SetHUD(); 
	}

	public void GainSP(int amount){
		stats.sp += amount; 
		if(stats.sp > stats.maxSP){
			stats.sp = stats.maxSP; 
		}
		SetHUD(); 
	}

	public void SetHUD(){
		//if(HUD != null) HUD.SetHUD(stats); 
	}
}
