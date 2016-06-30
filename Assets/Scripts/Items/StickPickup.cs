using UnityEngine;
using System.Collections;

public class StickPickup : Pickup {

	// for tutorial text
	public TutorialManager tManager;

	//to set lpayer weapon
	public Player player;

	//weapon reference
	public Weapon stick;

	//function called by player to pickup item
	public override void pickUp(){
		if (isNear) {
			print ("near");
			ItemManager.addItem (this.gameObject.GetComponent<Item> ());
			//player.weapon = stick;
			Destroy (gameObject);
			tManager.speak (3);
			tManager.speak (4);
			tManager.speak (5);
		} else {
			print ("stick far");
		}
	}
}
