using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DGCrawl.ItemSystem.Editor {
	public partial class QualityDBEditor : EditorWindow {

		QualityDB qualityDb;
		Quality selectedItem;
		Texture2D selectedTexture;

		const int SPRITE_BUTTON_SIZE = 92;

		const string DB_FILE_NAME = @"DGCQualityDB";
		const string DB_FOLDER_NAME = @"DB";
		const string DB_FULL_PATH = @"Assets/" + DB_FOLDER_NAME + "/" + DB_FILE_NAME;



		[MenuItem("DGC/DB/Quality Editor %#i")]
		public static void Init(){
			QualityDBEditor window = EditorWindow.GetWindow<QualityDBEditor>();
			window.minSize = new Vector2 (400, 300);
			window.titleContent = new GUIContent ("Quality");
			window.Show();
		}


		void OnEnable(){
			qualityDb = AssetDatabase.LoadAssetAtPath (DB_FULL_PATH, typeof(QualityDB)) as QualityDB;
			if (qualityDb == null) {
				if (!AssetDatabase.IsValidFolder ("")) {
					AssetDatabase.CreateFolder ("Assets", DB_FOLDER_NAME);
				}
				qualityDb = ScriptableObject.CreateInstance<QualityDB> ();
				AssetDatabase.CreateAsset (qualityDb, DB_FULL_PATH);
				AssetDatabase.SaveAssets ();
				AssetDatabase.Refresh ();
			}
			selectedItem = new Quality ();
		}


		void OnGUI(){

			AddQualityToDB();
		}

		private void AddQualityToDB(){
			//name
			selectedItem.Name = EditorGUILayout.TextField("Name: ", selectedItem.Name);
			//sprite
			if (selectedItem.Icon) {
				selectedTexture = selectedItem.Icon.texture;
			} else {
				selectedTexture = null;
			}
			if (GUILayout.Button (selectedTexture, GUILayout.Width (SPRITE_BUTTON_SIZE), GUILayout.Height (SPRITE_BUTTON_SIZE))) {
				int controllerID = EditorGUIUtility.GetControlID (FocusType.Passive);
				EditorGUIUtility.ShowObjectPicker<Sprite> (null, true, null, controllerID);
			}
			string commandName = Event.current.commandName;
			if (commandName == "ObjectSelectorUpdated") {
				selectedItem.Icon = (Sprite)EditorGUIUtility.GetObjectPickerObject ();
				Repaint ();
			}

			if (GUILayout.Button ("Save")) {
				if (selectedItem == null || selectedItem.Name == "") {
					return;
				}

				qualityDb.Add (selectedItem);

				selectedItem = new Quality();
			}
		}
	}
}
