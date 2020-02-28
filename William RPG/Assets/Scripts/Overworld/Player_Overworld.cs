using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float moveSpeed = 5f;  
	private Rigidbody2D rb; 

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

	void FixedUpdate(){
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
