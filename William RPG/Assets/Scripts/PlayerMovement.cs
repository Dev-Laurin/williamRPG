using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate(){
        float moveSpeed = 5.0f;
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
