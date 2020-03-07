using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	//For collision / movement 
	public float moveSpeed = 5f;  
	public Rigidbody2D rb; 
	public Vector3 position;

	//Stats 
	public string name; 
	public int hp; 
	public int maxHP; 
	public int maxSP; 
	public int sp; 
	public int level; 
	public int defense; 
	public int strength;

	//Position in the party. Drawing/following/battle place
	public int partyPos; 

	//Sprite Animations 
	Animation walkLeft; 
	Animation walkRight; 
	Animation walkUp; 
	Animation walkDown; 

	Animation battleIdle; 

	public Unit(string NAME, int HP, int MAXHP, 
		int MAXSP, int SP, int LEVEL, int DEFENSE, 
		int STRENGTH){
		name = NAME; 
		hp = HP; 
		maxHP = MAXHP; 
		maxSP = MAXSP; 
		sp = SP; 
		level = LEVEL; 
		defense = DEFENSE; 
		strength = STRENGTH; 
	}

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>(); 
		if(rb == null){
			Debug.LogError("Player::Start cant find Rigidbody2D </sadface>"); 
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
