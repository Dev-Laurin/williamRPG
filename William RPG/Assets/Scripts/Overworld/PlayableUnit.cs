using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayableUnit : Unit {

	//Position in the party. Drawing/following/battle place
	public int partyPos; 

	Animation battleIdle; 

	public PlayableUnit(string NAME, int HP, int MAXHP, 
		int MAXSP, int SP, int LEVEL, int DEFENSE, 
		int STRENGTH, int SPEED) : base(NAME, HP, MAXHP, 
		MAXSP, SP, LEVEL, DEFENSE, 
		STRENGTH, SPEED){}
	
}
