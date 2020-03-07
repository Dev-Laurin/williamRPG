using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Player : MonoBehaviour {

	public Unit unit; 

	// Use this for initialization
	void Start () {
		unit.rb = gameObject.GetComponent<Rigidbody2D>(); 
		if(unit.rb == null){
			Debug.LogError("Player::Start cant find Rigidbody2D </sadface>"); 
		}
		Data.AddToPlayerParty(unit); 
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("Collision -- Battle"); 
		Unit collidedUnit = other.gameObject.GetComponent<Unit>(); 
		if(collidedUnit.isEnemy){
			//transfer data to global game object 
			Data.StoreCollidedEnemy(collidedUnit); 
			Data.UpdatePlayerUnit(unit); 
			//Go to next scene 
			SceneManager.LoadScene("Battle"); 
		}
		else if(collidedUnit.isPlayable){
			//add them to the party 
			Data.AddToPlayerParty(collidedUnit); 
			Debug.Log("Added: " + collidedUnit.name + " to the party!"); 
			collidedUnit.followPlayer = true; 
		}
		
	}

	void FixedUpdate(){
		//check if user has pressed some input keys 
		if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
			//convert user input into world movement 
			float horizontalMovement = Input.GetAxisRaw("Horizontal") * unit.moveSpeed;
			float verticalMovement = Input.GetAxisRaw("Vertical") * unit.moveSpeed; 

			//assign world movements to a Vector 
			Vector3 directionOfMovement = new Vector3(horizontalMovement, verticalMovement, 0); 

			//apply movement to player's transform 
			unit.transform.Translate(directionOfMovement * Time.deltaTime, Space.World); 
		}
	}
}
