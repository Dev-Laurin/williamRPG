using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;  

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
	public Rigidbody2D rb;
	Animator animator; 

	PlayerControls controls; 
	Vector2 move; 

	void Awake(){
		//Input system
		controls = new PlayerControls(); 
		controls.Movement.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
		controls.Movement.Move.canceled += ctx => move = Vector2.zero;   

		//Animation 
		animator = GetComponent<Animator>(); 
	}

	void OnEnable(){
		controls.Movement.Enable(); 
	}

	void OnDisable(){
		controls.Movement.Disable(); 
	}

	void FixedUpdate(){
		//Get the last movement coords for transition purposes
		animator.SetFloat("LastDirectionHorizontal", animator.GetFloat("Horizontal"));
		animator.SetFloat("LastDirectionVertical", animator.GetFloat("Vertical"));

		//Set the current movement
		animator.SetFloat("Speed", move.sqrMagnitude);
		animator.SetFloat("Horizontal", move.x); 
		animator.SetFloat("Vertical", move.y); 

		//Apply movement to the rigidbody 
		rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime); 
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Enemy"){
			//transfer data to global game object 
			Data.StoreCollidedEnemy(other.gameObject.GetComponent<BattleUnit>()); 
			Data.UpdatePlayerUnit(gameObject.GetComponent<BattleUnit>()); 
			//Go to next scene 
			SceneManager.LoadScene("Battle"); 
		}
		else if(other.gameObject.tag == "Special Item"){
			Debug.Log("Special Item!!"); 
			//add special move to character 
			SpecialMove spMove = other.gameObject.GetComponent<SpecialMove>(); 
			Debug.Log("Learned Special Move -- " + spMove.name); 
			gameObject.GetComponent<BattleUnit>().AddSpecialMove(spMove); 
			//destroy object 
			Destroy(other.gameObject); 
		}
	}
}
