using UnityEngine;
using System.Collections;

public class Player : Animate {

	//weapon for in game
	public Weapon weapon;

	//list of items in a room that an be pickedup
	public GameObject[] grabables;

	//for all instances of player
	private bool ignoreInput = false;

	//singleton of Player
	public static Player playerInstance;



	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		stats ["health"] = 10.0f;
		stats ["speed"] = 5.0f;
		grabables = GameObject.FindGameObjectsWithTag ("Pickup");
	}

	void Awake(){
		playerInstance = this;
	}

	void OnLevelWasLoaded(int level){
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.E)) {
			foreach (GameObject pickup in grabables){
				if (pickup.GetComponentInChildren<Pickup> () != null) {
					pickup.GetComponentInChildren<Pickup> ().pickUp ();
				}
			}
		}
	}
	protected override void AttemptMove(float xDir, float yDir, float speed){
		base.AttemptMove (xDir, yDir, speed);
	}

	void FixedUpdate () {
		float horizontal = 0.0f; 
		float vertical = 0.0f; 

		horizontal = (Input.GetAxis ("Horizontal"));
		vertical = (Input.GetAxis ("Vertical"));
		if(horizontal != 0){
			vertical = 0;
		}
		if(horizontal != 0 || vertical != 0 && !ignoreInput){
			AttemptMove (horizontal, vertical, stats["speed"]);
		}
	}

	public void addToStat(string stat, float mod){
		if (stats.ContainsKey(stat)) {
			stats [stat] += mod;
		}
	}
}
