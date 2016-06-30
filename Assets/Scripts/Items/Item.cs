using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {

	//list of stats to reference for statToMod
	protected List<string> stats = new List<string> (){"health", "strength", "speed", "defence"}; 

	//string to find stat and float fo how much
	protected string statToMod;
	protected float mod;

	public Player player;

	void Awake(){
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}

	//item function effects playerstats
	public void modStat(string stat, float amount){
		player.addToStat (stat, amount);
	}

	public void onPickup(Player player){
		//to change animation and set base damage for stick, sword, and fists only
		return;
	}

	public string getStattoMod(){
		return statToMod;
	}

	public float getMod(){
		return mod;
	}
}
