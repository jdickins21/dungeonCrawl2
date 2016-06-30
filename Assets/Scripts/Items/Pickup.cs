using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	protected bool isNear = false;

	//keeps track of everything 
	public GameObject itemsManager;

	//item knows player is near
	protected void OnTriggerEnter(){
		print ("welcome");
		isNear = true;
	}

	//item kows player has left
	protected void OnTriggerExit(){
		print ("bye");
		isNear = false;
	}

	//function called by player to pickup item
	public virtual void pickUp(){
		if (isNear) {
			ItemManager.addItem (this.gameObject.GetComponent<Item> ());
			Destroy (gameObject);
		}
	}
}
