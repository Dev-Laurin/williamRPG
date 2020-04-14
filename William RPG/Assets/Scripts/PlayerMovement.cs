﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.InputSystem; 

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
}
