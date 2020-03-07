using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMove : MonoBehaviour {

	private string name; 
	private int damage; 
	private List<StatusEffect> statusEffects; 

	private Animation animation; 

	public SpecialMove(string Name, int Damage, 
		List<StatusEffect> se, Animation ani){
		name = Name; 
		damage = Damage; 
		statusEffects = se; 
		animation = ani; 
	}

	public List<StatusEffect> getStatusEffects(){
		return statusEffects; 
	}

	public string getName(){
		return name; 
	}

	public void setName(string Name){
		name = Name; 
	}

	public void setDamage(int d){
		damage = d; 
	}

	public int getDamage(){
		return damage; 
	}
}
