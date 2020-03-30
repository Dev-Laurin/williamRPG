using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

	//Who's speaking 
	public string name; 
	[TextArea(3, 10)]
	public string[] sentences; 
	public Animation animation; 

}
