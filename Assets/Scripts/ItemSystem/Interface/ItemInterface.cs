using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGCrawl.ItemSystem {
	public interface ItemInterface {
		//name
		string ISName {get; set;}
		//value
		int ISValue {get; set;} 
		//icon
		Sprite ISIcon {get; set;}
		//burden
		int ISBurden {get; set;}
		//qualitylvl
		Quality Quality	{get; set;}

		//!!other item interfaces!!
		//equip
		//quast flag
		//prefab
	}
}