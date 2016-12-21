using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGCrawl.ItemSystem {
	[System.Serializable]
	public class Quality : QualityInterface {

		[SerializeField]string _name;
		[SerializeField]Sprite _icon;

		public Quality(){
			_name = "";
			_icon = new Sprite ();
		}

		#region QualityInterface implementation

		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}

		public Sprite Icon {
			get {
				return _icon;
			}
			set {
				_icon = value;
			}
		}

		#endregion
	}
}
