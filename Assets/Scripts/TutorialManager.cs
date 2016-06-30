using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public Text voice;
	public GameObject arrow;
	public GameObject player;
	public TextAsset textFileOne;
	public TextAsset textFileTwo;
	public TextAsset textFileThree;

	//to keep track of the items

	public string[] lines;
	private string[] linesAlt;
	private string[] fistLines;
	private bool speakLocked = false;

	// Use this for initialization
	void Start () {
		if (textFileOne != null) {
			lines = textFileOne.text.Split ("\n"[0]);
		}
		if (textFileTwo != null) {
			linesAlt = textFileTwo.text.Split ("\n"[0]);
		}
		if (textFileThree != null) {
			fistLines = textFileThree.text.Split ("\n"[0]);
		}
		voice.canvasRenderer.SetAlpha( 0.0f );
		speak (0);
		speak (1);
		speak (2);
	}

	// Update is called once per frame
	void Update () {

	}

	private IEnumerator talk(int num){
		while (speakLocked) {
			yield return null;
		}
		voice.text = lines[num];
		if (voice.text == null) {
			StopCoroutine(talk(num));
		}
		speakLocked = true;
		yield return new WaitForSeconds(1.0f);
		voice.CrossFadeAlpha (1.0f, 2.0f, false);
		yield return new WaitForSeconds(3.0f);
		voice.CrossFadeAlpha (0.0f, 2.0f, false); 
		yield return new WaitForSeconds(2.0f);
		speakLocked = false;
	}

	private IEnumerator altTalk(int num){
		while (speakLocked) {
			yield return null;
		}
		voice.text = linesAlt[num];
		if (voice.text == null) {
			StopCoroutine(altTalk(num));
		}
		speakLocked = true;
		yield return new WaitForSeconds(1.0f);
		voice.CrossFadeAlpha (1.0f, 2.0f, false);
		yield return new WaitForSeconds(3.0f);
		voice.CrossFadeAlpha (0.0f, 2.0f, false); 
		yield return new WaitForSeconds(2.0f);
		speakLocked = false;
	}

	private IEnumerator fistTalk(int num){
		while (speakLocked) {
			yield return null;
		}
		voice.text = fistLines[num];
		if (voice.text == null) {
			StopCoroutine(fistTalk(num));
		}
		speakLocked = true;
		yield return new WaitForSeconds(1.0f);
		voice.CrossFadeAlpha (1.0f, 2.0f, false);
		yield return new WaitForSeconds(3.0f);
		voice.CrossFadeAlpha (0.0f, 2.0f, false); 
		yield return new WaitForSeconds(2.0f);
		speakLocked = false;
	}

	public void speak(int num){
		StartCoroutine (talk (num));
	}

	public void altSpeak(int num){
		StartCoroutine (altTalk (num));
	}

	public void fistSpeak(int num){
		StartCoroutine (fistTalk (num));
	}

}
