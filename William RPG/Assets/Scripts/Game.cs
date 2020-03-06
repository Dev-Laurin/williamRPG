using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO; 
using UnityEngine;

public class Game : MonoBehaviour {

	private List<Player> players = new List<Player>(); 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// private Save CreateSaveGameObject(){
	// 	Save save = new Save(); 

	// 	//load the players' data 
	// 	foreach(Player player in players){
	// 		save.player_positions.Add(player.transform.position); 
	// 		save.player_stats.Add(player.stats); 
	// 	}
	// 	return save; 
	// }

	// void SaveGame(){
	// 	Save save = CreateSaveGameObject(); 

	// 	BinaryFormatter bf = new BinaryFormatter(); 
	// 	FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save"); 
	// 	bf.Serialize(file, save); 
	// 	file.Close(); 

	// 	Debug.Log("Game Saved."); 
	// }
}
