using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableUnit : Unit {

	//Position in the party. Drawing/following/battle place
	public int partyPos; 

	Animation battleIdle; 

	public PlayableUnit(string NAME, int HP, int MAXHP, 
		int MAXSP, int SP, int LEVEL, int DEFENSE, 
		int STRENGTH) : base(NAME, HP, MAXHP, 
		MAXSP, SP, LEVEL, DEFENSE, 
		STRENGTH){}
	
}
