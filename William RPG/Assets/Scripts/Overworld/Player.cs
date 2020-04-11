using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Player {}

	// public GameObject partyMemberPrefab; 
	// public Queue<Vector3> positions = new Queue<Vector3>();

	// // Use this for initialization
	// public override void Start () {
	// 	base.Start(); 

	// 	//player cannot follow themself 
	// 	followPlayer = false; 

	// 	//create party members
	// 	List<PlayableUnit> party = Data.GetPlayerParty();   
	// 	for(int i=1; i<party.Count; i++){
	// 		PlayableUnit player = party[i]; 
	// 		Transform pos = gameObject.transform; 
	// 		pos.position += new Vector3(-20 * player.partyPos, 0, 0); 
	// 		//who to follow, reference this
	// 		partyMemberPrefab.GetComponent<PlayableUnit>().player = gameObject; 
	// 		GameObject obj = Instantiate(partyMemberPrefab, new Vector3(-20 * player.partyPos, 0, 0), Quaternion.identity);
	// 		obj.transform.position = pos.position; 
	// 		obj.GetComponent<PlayableUnit>().followPlayer = true; 
	// 	}
		 
	// }

	// void OnTriggerEnter2D(Collider2D other){
	// 	EnemyUnit collidedUnit = other.gameObject.GetComponent<EnemyUnit>();
	// 	if(other.gameObject.tag == "Enemy"){
	// 		//transfer data to global game object 
	// 		Data.StoreCollidedEnemy(collidedUnit); 
	// 		Data.UpdatePlayerUnit(this); 
	// 		//Go to next scene 
	// 		SceneManager.LoadScene("Battle"); 
	// 	}
	// }
