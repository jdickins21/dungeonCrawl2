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

	//to keep track of direction input
	private Vector2 MoveVec;


	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
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
		MoveVec = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if(MoveVec != Vector2.zero && !ignoreInput){
			AttemptMove (MoveVec, stats["speed"]);
		}
	}

	void FixedUpdate () {
	}

	public void addToStat(string stat, float mod){
		if (stats.ContainsKey(stat)) {
			stats [stat] += mod;
		}
	}
}
