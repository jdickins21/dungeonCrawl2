using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {

	//array of items collected
	private static List<Item> Inventory;

	//reference to player to modify stats
	public static GameObject player;

	//put item in array
	public static void addItem(Item item){
		Inventory.Add (item);
		item.modStat (item.getStattoMod(), item.getMod());
	}

	//remove item in array
	public static void removeItem(Item item){
		Inventory.Remove (item);
	}

	//itterate through each item and call item function over each itemCard
	public static void showInventory(int xCord, int yCord){

	}
}
