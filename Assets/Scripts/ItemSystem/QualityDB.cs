using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;		//needed for ElementAt

namespace DGCrawl.ItemSystem {
	public class QualityDB : ScriptableObject {

		[SerializeField] List<Quality> database = new List<Quality> ();



		public void Add(Quality item){
			database.Add (item);
			EditorUtility.SetDirty (this);
		}

		public void Insert(int index, Quality item){
			database.Insert (index, item);
			EditorUtility.SetDirty (this);
		}

		public void Remove(Quality item){
			database.Remove (item);
			EditorUtility.SetDirty (this);
		}

		public void Remove(int index){
			database.RemoveAt (index);
			EditorUtility.SetDirty (this);
		}

		public int Count(){
			//diff from toutorial need () for func
			return database.Count;
		}

		public Quality Get(int index){
			return database.ElementAt (index);
		}

		public void Replace(int index, Quality item){
			database [index] = item;
			EditorUtility.SetDirty (this);
		}
	}
}
