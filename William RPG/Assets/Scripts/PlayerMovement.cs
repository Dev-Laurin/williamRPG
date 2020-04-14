using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
	public Rigidbody2D rb;
	public Animator animator; 

	PlayerControls controls; 
	Vector2 move; 

	void Awake(){
		controls = new PlayerControls(); 
		controls.Movement.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
		controls.Movement.Move.canceled += ctx => move = Vector2.zero;   
	}

	void OnEnable(){
		controls.Movement.Enable(); 
	}

	void OnDisable(){
		controls.Movement.Disable(); 
	}

	void FixedUpdate(){
		animator.SetFloat("Speed", move.sqrMagnitude);
		animator.SetFloat("Horizontal", move.x); 
		animator.SetFloat("Vertical", move.y);  
		rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime); 
	}
}
