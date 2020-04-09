﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Stats {

	//For collision / movement 
	public float moveSpeed = 5f;  
	public Rigidbody2D rb; 

	//are we following the player? 
	public bool followPlayer;  
	public GameObject player; 
	public float FollowSpeed; 
	float AllowedDistance = 70.0f; 
	float TargetDistance = 20; 

	//Sprite Animations 
	Animation walkLeft; 
	Animation walkRight; 
	Animation walkUp; 
	Animation walkDown; 

	//Sprites for HUDs 
	public Sprite HUDSprite; 
	public Sprite battleHUDSprite; 

	public Unit(string NAME, int HP, int MAXHP, 
		int MAXSP, int SP, int LEVEL, int DEFENSE, 
		int STRENGTH, int SPEED){
		name = NAME; 
		hp = HP; 
		maxHP = MAXHP; 
		maxSP = MAXSP; 
		sp = SP; 
		level = LEVEL; 
		defense = DEFENSE; 
		strength = STRENGTH;
		speed = SPEED;  
	}

	// Use this for initialization
	public virtual void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>(); 
		if(rb == null){
			Debug.LogError("Player::Start cant find Rigidbody2D </sadface>"); 
		}
		AllowedDistance = 1.0f; 
		TargetDistance = 2; 
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if(followPlayer){
			
			TargetDistance = Vector3.Distance(transform.position, player.transform.position); 
			Debug.Log(TargetDistance); 
			Debug.Log("Allowed distance " + AllowedDistance); 
			if(TargetDistance >= AllowedDistance){
				FollowSpeed = 0.05f; 
				transform.position = Vector3.MoveTowards(transform.position, player.transform.position, FollowSpeed); 
				
			}
			else{
				FollowSpeed = 0; 
			}
		}
	}

	public virtual void FixedUpdate(){
		//for inheritance 
	}
}
