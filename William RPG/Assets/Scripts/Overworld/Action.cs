using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection; 

public class Action {

	public string functionName; 
	public Object [] parameters; 

	public void DoAction(){
		var method = typeof(Data).GetMethod(functionName, BindingFlags.Public | BindingFlags.Static); 
		if(method == null) throw new System.MissingMethodException($"Could not resolve function name '{functionName}'."); 
		method.Invoke(null, parameters); 
	}
}
