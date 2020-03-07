using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
	private Player player; 

	public Player GetPlayer(){
		return player; 
	}

	public void SetPlayer(Player p){
		player = p; 
	}

	void Start () {
		DontDestroyOnLoad(this.gameObject); 
		SceneManager.sceneLoaded += OnSceneLoaded; 
		this.gameObject.SetActive(false); 
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		if(scene.name == "Title"){
			SceneManager.sceneLoaded -= OnSceneLoaded; 
			Destroy(this.gameObject); 
		}
		else{ //Battle scene 
			this.gameObject.SetActive(scene.name == "Battle");
		}
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
