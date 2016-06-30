using UnityEngine;
using System.Collections;

public class SwordPickup : Pickup {

	// for tutorial text
	public TutorialManager tManager;

	//to set lpayer weapon
	public Player player;

	//weapon reference
	public Weapon sword;

	//function called by player to pickup item
	public override void pickUp(){
		if (isNear) {
			ItemManager.addItem (this.gameObject.GetComponent<Item> ());
			player.weapon = sword;
			Destroy (gameObject);
			tManager.speak (7);
		}
	}
}
