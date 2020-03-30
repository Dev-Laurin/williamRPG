using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection; 

[System.Serializable]
public class Action {

	public string function; 
	public string [] parametersJSON; 

	public void DoAction(){
		Debug.Log(parametersJSON); 
		//get the parameter types from the method 
		var method = typeof(Data).GetMethod(function, BindingFlags.Public | BindingFlags.Static); 
		if(method == null) throw new System.MissingMethodException($"Could not resolve function name '{function}'.");
		ParameterInfo[] pars = method.GetParameters(); 

		//Create objects out of the parameter values in json
		Object [] parameters = new Object[parametersJSON.Length]; 
		for(int i=0; i<pars.Length; i++){
			Debug.Log(pars[i].ParameterType); 
			//deserialize the json to get the arguments to create the object with
			var obj = JsonUtility.FromJson<pars[i].ParameterType>(parametersJSON[i]); 
			parameters[i] = obj; 
		}

		//Call the function with the parameters 
		method.Invoke(null, parameters); 
	}
}
