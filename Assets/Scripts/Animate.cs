using UnityEngine;
using System.Collections;

public abstract class Animate : MonoBehaviour {

	protected Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	protected virtual void AttemptMove (float xDir, float yDir, float speed){
		rb.velocity = new Vector2 (xDir, yDir) * speed;
	}
}
