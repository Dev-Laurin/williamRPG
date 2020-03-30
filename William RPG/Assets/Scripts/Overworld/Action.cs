using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection; 

[System.Serializable]
public class Action {

	public string function; 
	public Object [] parameters; 

	public void DoAction(){
		var method = typeof(Data).GetMethod(function, BindingFlags.Public | BindingFlags.Static); 
		if(method == null) throw new System.MissingMethodException($"Could not resolve function name '{function}'."); 
		method.Invoke(null, parameters); 
	}
}
