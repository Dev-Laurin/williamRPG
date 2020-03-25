using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Player : PlayableUnit {
	
	public Player(string NAME, int HP, int MAXHP, 
		int MAXSP, int SP, int LEVEL, int DEFENSE, 
		int STRENGTH, int SPEED) : base(NAME, HP, MAXHP, 
		MAXSP, SP, LEVEL, DEFENSE, 
		STRENGTH, SPEED){}

	// Use this for initialization
	public override void Start () {
		base.Start(); 
		Data.EmptyPlayerParty(); 
		Data.AddToPlayerParty(this);

		//player cannot follow themself 
		followPlayer = false; 
	}

	void OnTriggerEnter2D(Collider2D other){
		EnemyUnit collidedUnit = other.gameObject.GetComponent<EnemyUnit>();
		if(other.gameObject.tag == "Enemy"){
			//transfer data to global game object 
			Data.StoreCollidedEnemy(collidedUnit); 
			Data.UpdatePlayerUnit(this); 
			//Go to next scene 
			SceneManager.LoadScene("Battle"); 
		}
	}

	public override void FixedUpdate(){
		base.FixedUpdate(); 
		//check if user has pressed some input keys 
		if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
			//convert user input into world movement 
			float horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed;
			float verticalMovement = Input.GetAxisRaw("Vertical") * moveSpeed; 

			//assign world movements to a Vector 
			Vector3 directionOfMovement = new Vector3(horizontalMovement, verticalMovement, 0); 

			//apply movement to player's transform 
			transform.Translate(directionOfMovement * Time.deltaTime, Space.World); 
		}
	}
}
