using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour {

	public string name; 
	public int hp; 
	public int maxHP; 
	public int maxSP; 
	public int sp; 
	public int level; 

	public int defense; 
	public int strength;

	// Use this for initialization
	void Start () {
		//give the unit a special move 

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool TakeDamage(int damage){
		hp = hp - (damage - defense); 
		if(hp <= 0){
			return true; 
		}
		return false; 
	}

	public void Heal(int amount){
		hp += amount; 
		if(hp > maxHP){
			hp = maxHP; 
		}
	}

	public void GainSP(int amount){
		sp += amount; 
		if(sp > maxSP){
			sp = maxSP; 
		}
	}
}
