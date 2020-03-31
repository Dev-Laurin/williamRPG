using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action {

	public string function; 
	public List<string> parametersJSON; 

	public void DoAction(){
		if(function == "AddToPlayerParty"){
			Debug.Log(parametersJSON[0]); 
			Stats newUnit = new Stats(); 
			JsonUtility.FromJsonOverwrite(parametersJSON[0], newUnit); 
			PlayableUnit pu = new PlayableUnit(newUnit.name, newUnit.hp, 
			newUnit.maxHP, newUnit.maxSP, newUnit.sp, newUnit.level,
			newUnit.defense, newUnit.strength, newUnit.speed); 
			Data.AddToPlayerParty(pu); 
		}
		else if(function == "GoToLastScene"){
			Data.GoToLastScene(); 
		}
	}
}
