using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Animate {

	//weapon for in game
	public Weapon weapon;

	//for all instances of player
	private bool ignoreInput = false;

	//dictionary of stats
	public static Dictionary<string, float> stats = new Dictionary<string, float>(){
		{"health", 10.0f},
		{"strength", 0.0f},
		{"speed", 5.0f},
		{"defence", 0.0f}
	};

	//singleton of Player
	public static Player playerInstance;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void Awake(){
		playerInstance = this;
	}

	void Update(){
	}

	void FxedUpdate(){

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
