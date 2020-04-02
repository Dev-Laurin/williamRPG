using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Player : PlayableUnit {

	public GameObject partyMemberPrefab; 
	public Queue<Vector3> positions = new Queue<Vector3>();
	
	public Player(string NAME, int HP, int MAXHP, 
		int MAXSP, int SP, int LEVEL, int DEFENSE, 
		int STRENGTH, int SPEED) : base(NAME, HP, MAXHP, 
		MAXSP, SP, LEVEL, DEFENSE, 
		STRENGTH, SPEED){}

	// Use this for initialization
	public override void Start () {
		base.Start(); 

		//player cannot follow themself 
		followPlayer = false; 

		//create party members
		List<PlayableUnit> party = Data.GetPlayerParty();
		Debug.Log(party[0]);
		Debug.Log(party.Count);   
		for(int i=1; i<party.Count; i++){
			PlayableUnit player = party[i]; 
			Transform pos = gameObject.transform; 
			pos.position += new Vector3(-2 * player.partyPos, 0, 0); 
			//who to follow, reference this
			partyMemberPrefab.GetComponent<PlayableUnit>().player = gameObject; 
			GameObject obj = Instantiate(partyMemberPrefab, pos);
			obj.transform.position = pos.position; 
			Debug.Log(obj); 
		}
		 
	}

	void OnTriggerEnter2D(Collider2D other){
		EnemyUnit collidedUnit = other.gameObject.GetComponent<EnemyUnit>();
		if(other.gameObject.tag == "Enemy"){
			//transfer data to global game object 
			Data.StoreCollidedEnemy(collidedUnit); 
			Data.UpdatePlayerUnit(this); 
			//Go to next scene 
			SceneManager.LoadScene("Battle"); 
		}
	}

	public override void Update(){
		//Data.UpdatePlayerPartyTransforms(); 
		positions.Enqueue(gameObject.transform.position); 
	}

	public override void FixedUpdate(){
		base.FixedUpdate(); 
		//check if user has pressed some input keys 
		if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
			//convert user input into world movement 
			float horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed;
			float verticalMovement = Input.GetAxisRaw("Vertical") * moveSpeed; 

			//assign world movements to a Vector 
			Vector3 directionOfMovement = new Vector3(horizontalMovement, verticalMovement, 0); 

			//apply movement to player's transform 
			transform.Translate(directionOfMovement * Time.deltaTime, Space.World); 
		}
	}
}
