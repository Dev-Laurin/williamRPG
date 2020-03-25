using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit {

	Animation battleIdle; 

	public EnemyUnit(string NAME, int HP, int MAXHP, 
		int MAXSP, int SP, int LEVEL, int DEFENSE, 
		int STRENGTH) : base(NAME, HP, MAXHP, 
		MAXSP, SP, LEVEL, DEFENSE, 
		STRENGTH){}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
