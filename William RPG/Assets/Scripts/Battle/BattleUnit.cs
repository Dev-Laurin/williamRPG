using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour {
	public Sprite HUDImage; 
	public bool isPlayer; 
	public int partyPosition; 
	public Stats stats; 
	Animator animator; 
	bool isDodging; 
	PlayerHUD HUD; 

	void Start(){
		//Set animator
		animator = GetComponent<Animator>(); 
	}

	public void SetPartyPosition(int pos){
		partyPosition = pos; 
	}

	public int GetPartyPosition(){
		return partyPosition; 
	}

	public Sprite GetHUDSprite(){
		return HUDImage; 
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

	public int GetStrength(){
		return stats.strength; 
	}

	public void SetDodging(){
		isDodging = true; 
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
		UpdateHUD();
		return "The attack was successful."; 
	}

	public void Heal(int amount){
		stats.hp += amount; 
		if(stats.hp > stats.maxHP){
			stats.hp = stats.maxHP; 
		}
		UpdateHUD();
	}

	public void GainSP(int amount){
		stats.sp += amount; 
		if(stats.sp > stats.maxSP){
			stats.sp = stats.maxSP; 
		}
		UpdateHUD(); 
	}

	public void SetHUD(PlayerHUD hud){
		HUD = hud; 
		HUD.SetHUD(stats, HUDImage); 
		isPlayer = true; 
	}

	public void UpdateHUD(){
		if(HUD != null) HUD.UpdateHUD(stats); 
	}
}
