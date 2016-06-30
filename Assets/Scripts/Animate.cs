using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Animate : MonoBehaviour {

	protected Rigidbody2D rb2d;

	//dictionary of stats
	public static Dictionary<string, float> stats = new Dictionary<string, float>(){
		{"health", 0.0f},
		{"strength", 0.0f},
		{"speed", 0.0f},
		{"defence", 0.0f}
	};

	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
	}

	protected virtual void AttemptMove (float xDir, float yDir, float speed){
		rb2d.velocity = new Vector2(xDir,yDir) * speed;
	}
}
