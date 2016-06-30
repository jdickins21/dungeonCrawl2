using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	protected static bool isNear = false;

	//keeps track of everything 
	public GameObject itemsManager;

	//add to item list
	public GameObject toAdd;
	//remove from item list
	public GameObject toRemove;

	//item knows player is near
	protected void OnTriggerEnter(){
		isNear = true;
	}

	//item kows player has left
	protected void OnTriggerExit(){
		isNear = false;
	}

	//function called by player to pickup item
	public void pickUp(){
		if (isNear) {
			ItemManager.addItem (this.gameObject.GetComponent<Item> ());
			Destroy (gameObject);
		}
	}
}
