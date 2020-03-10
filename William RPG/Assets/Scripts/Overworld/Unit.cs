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
	public int speed; 

	//Position in the party. Drawing/following/battle place
	public int partyPos; 
	public bool isEnemy; 
	public bool isPlayable;

	public bool followPlayer;  
	public GameObject player; 
	public float FollowSpeed; 
	public RaycastHit Shot; 
	public float AllowedDistance = 1; 
	public float TargetDistance; 

	//Sprite Animations 
	Animation walkLeft; 
	Animation walkRight; 
	Animation walkUp; 
	Animation walkDown; 

	Animation battleIdle; 

	//Sprites for HUDs 
	Sprite mugshot; 
	Sprite battleHUDSprite; 

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
		if(followPlayer){
			TargetDistance = Vector3.Distance(transform.position, player.transform.position); 
			if(TargetDistance >= AllowedDistance){
				FollowSpeed = 0.1f; 
				transform.position = Vector3.MoveTowards(transform.position, player.transform.position, FollowSpeed); 
				
			}
			else{
				FollowSpeed = 0; 
			}
		}
	}
}
