using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 

public static class Save {
	
	public static void SaveGame(List<GameObject>playableUnits, 
	List<GameObject>enemies, List<NPC> npcs){
		//New file 
		string path = Application.persistentDataPath + "/superWilliamRPGSave.json"; 
		FileStream file = new FileStream(path, FileMode.Create); 
		//Serialize the data into json string
		string json = ""; 
		for(int i=0; i<playableUnits.Count; i++){
			SavePlayer sp = new SavePlayer(); 
			sp.Save(playableUnits[i]); 
			json += JsonUtility.ToJson(sp); 
		}
		Debug.Log(json); 
		//Write json to file 
		// AddText(file, json); 
		file.Close(); 
	}

}
