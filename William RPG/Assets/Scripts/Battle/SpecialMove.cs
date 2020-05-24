using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMove : MonoBehaviour {

	public string name; 
	public int damage; 
	public List<StatusEffect> statusEffects; 

	public int animationIndex; 

	public SpecialMove(string Name, int Damage, 
		List<StatusEffect> se, int ani){
		name = Name; 
		damage = Damage; 
		statusEffects = se; 
		animationIndex = ani; 
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
