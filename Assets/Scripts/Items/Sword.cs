﻿using UnityEngine;
using System.Collections;

public class Sword : Item {

	// Use this for initialization
	void Start () {
		statToMod = stats[2];
		mod = 0;
	}

	public void onPickup(){
		//to change animation and set base damage for stick, sword, and fists only
		Player.stats ["stregth"] = 1.0f;
	}
}