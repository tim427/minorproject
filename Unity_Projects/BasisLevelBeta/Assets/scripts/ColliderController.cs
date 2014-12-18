using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ColliderController : MonoBehaviour
{
	
		private Collider Collider = null;
		private List<GameObject> CollectedGameObjects = new List<GameObject> ();
		private List<GameObject> PlayedGameObjects = new List<GameObject> ();
		private int SwitchCounter = 0;
		private Collider doorSubject;
		public bool switchOn = false;
		public bool showCollectables = false;
		public string onScreenText;
		public Font Font;
		public AudioClip collectSound;
		public int position_switch;


		void Start() {
			Time.timeScale = 1;			
		}
		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Tab)) {
						showCollectables = !showCollectables;
						Screen.lockCursor = !Screen.lockCursor;
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().enabled = !GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().enabled;
			GetComponent<MouseLook>().enabled = !GetComponent<MouseLook>().enabled;
				}

				if (Collider != null && Collider.gameObject.tag == "Switch") {
						if (Input.GetKeyDown (KeyCode.Space)) {
								switchOn = !switchOn;
								SwitchCounter ++;
								// collision.animation.Play ("Switch|SwitchAction");
								Collider.audio.Play ();
								if (SwitchCounter < 20) {
										SetOnScreenText ("You just toggled the switch " + (switchOn ? "ON" : "OFF"));
										foreach (GameObject ceilingLight in GameObject.FindGameObjectsWithTag("CeilingLights")) {
												ceilingLight.light.color = (switchOn ? new Color (0.64F, 0.82F, 1F, 1F) : new Color (1F, 0F, 0F, 1F));
										}
										GameObject.FindGameObjectWithTag ("MainCamera").camera.backgroundColor = (switchOn ? new Color (0.64F, 0.82F, 1F, 1F) : new Color (0.95F, 0.25F, 0.25F, 1F));
								} else {
										SetOnScreenText ("The lights are malfunctioning due to excessive usage!");
										GameObject.FindGameObjectWithTag ("MainCamera").camera.backgroundColor = new Color (0F, 0F, 0F, 0F);
										foreach (GameObject ceilingLight in GameObject.FindGameObjectsWithTag("CeilingLights")) {
												ceilingLight.light.enabled = false;
										}
										GameObject.FindGameObjectWithTag ("DirectionalLight").light.enabled = false;
								}
				
								position_switch = 10000;
				
								// Switch the switch down
								if (switchOn == false) {
										Collider.transform.FindChild ("Switch").Rotate (0, -position_switch, 0);
								}
								if (switchOn == true) {
										Collider.transform.FindChild ("Switch").Rotate (0, position_switch, 0);
								}
						}
				}
		
				if (Collider != null && Collider.gameObject.tag == "Door") {
						if (Input.GetKeyDown (KeyCode.Space)) {
								if (switchOn) {
										// collision.animation.Play ("Door|DoorAction");
										Collider.audio.Play ();
										SetOnScreenText ("DOOR OPENS");
										doorSubject = Collider;

								} else {
										// collision.animation.Play ("Door|DoorAction");
										// in reverse
										SetOnScreenText ("FAILED! NO POWER ON DOOR");
								}
						}
				}
				
				if (Collider != null && (Collider.gameObject.tag == "CollectableConsumable" || Collider.gameObject.tag == "CollectableReusable")) {
						if (Input.GetKeyDown (KeyCode.Space)) {
								SetOnScreenText ("Successfully collected the " + Collider.gameObject.name);
								AudioSource.PlayClipAtPoint (collectSound, transform.position);
								CollectedGameObjects.Add (Collider.gameObject);
								Collider.gameObject.SetActive (false);
								Destroy (Collider.gameObject.collider);
								Collider = null;
						}
				}
				
				if (doorSubject != null) {
						bool doorLeftFinshed = false;
						bool doorRightFinshed = false;
						if (doorSubject.transform.FindChild ("Door_Left").localPosition.x < 1.49) {
								doorSubject.transform.FindChild ("Door_Left").Translate (Time.deltaTime, 0, 0);
						} else {
								doorLeftFinshed = true;
						}
						if (doorSubject.transform.FindChild ("Door_Right").localPosition.x > -1.69) {
								doorSubject.transform.FindChild ("Door_Right").Translate (-Time.deltaTime, 0, 0);	
						} else {
								doorRightFinshed = true;
						}
						if (doorLeftFinshed && doorRightFinshed) {
								Destroy (doorSubject.gameObject.collider);
								doorSubject = null;
						}
				}
		}
	
		void SetOnScreenText (string text)
		{
				onScreenText = text;
				StartCoroutine (AutoRemoveOnScreenText (onScreenText));
		}
	
		IEnumerator AutoRemoveOnScreenText (string CurrentOnScreenText)
		{
				if (onScreenText != "" && CurrentOnScreenText == onScreenText) {
						yield return new WaitForSeconds (5);
						onScreenText = "";
				}
		}
	
		// Place here all "enter" events like pop-up messages
		void OnTriggerEnter (Collider collision)
		{
				Collider = collision;
				if (Collider.gameObject.tag == "Switch") {
						SetOnScreenText ("This is a Power Switch. To toggle the switch press the spacebar");
				}
		
				if (Collider.gameObject.tag == "Door") {
						SetOnScreenText ("To open the door press the spacebar");
				}
		
				if (Collider.gameObject.tag == "Refrigerator") {
						if (!PlayedGameObjects.Contains (Collider.gameObject)) {
								Collider.audio.Play ();
								Collider.animation.Play ();
								PlayedGameObjects.Add (Collider.gameObject);
						}
				}
		
				if (Collider.gameObject.tag == "CollectableConsumable" || Collider.gameObject.tag == "CollectableReusable") {
						SetOnScreenText ("To collect the " + Collider.gameObject.name + " press the spacebar!");
				}
		}
	
		// Place here al "exit" events like removing the pop-up messages
		void OnTriggerExit (Collider collision)
		{
				SetOnScreenText ("");
				Collider = null;
		}
	
		void OnGUI ()
		{
				GUI.skin.font = Font;
		
				if (onScreenText.Length > 0) {
						string[] lines = onScreenText.Split ('\n');
						int longestLineLength = 0;
						for (int i = 0; i < lines.Length; i++) {
								if (lines [i].Length > longestLineLength) {
										longestLineLength = lines [i].Length;
								}
								i++;
						}
						int width = longestLineLength * 9 + 30;
						int height = lines.Length * 16 + 20;
						int x = Screen.width / 2 - width / 2;
						int y = Screen.height / 2 - height / 2;
						GUI.Box (new Rect (x, y, width, height), onScreenText);
				}
		
				if (showCollectables) {
						GUI.Box (new Rect (10, 10, 250, CollectedGameObjects.Count * 30 + 30), "Collected items: " + CollectedGameObjects.Count);
						for (int i = 0; i < CollectedGameObjects.Count; i++) {
								if (GUI.Button (new Rect (20, i * 30 + 40, 80, 20), CollectedGameObjects [i].name)) {
										if (CollectedGameObjects [i].tag == "CollectableReusable") {
												Reuse (CollectedGameObjects [i]);
										}
										if (CollectedGameObjects [i].tag == "CollectableConsumable") {
												Consume (CollectedGameObjects [i]);
										}
								}
						}
				}
		}
	
		void Reuse (GameObject gameobject)
		{
				if (!gameobject.activeSelf) {
						gameobject.SetActive (true);
						gameobject.transform.position = transform.position;
						gameobject.transform.Translate (0.4f, 0.15f, 0.75f);
						gameobject.transform.rotation = transform.rotation;
						gameobject.transform.Rotate (0, 90, 0);
						gameobject.transform.parent = transform;
				} else {
						gameobject.SetActive (false);
				}
		}
	
		void Consume (GameObject gameobject)
		{
				CollectedGameObjects.Remove (gameobject);
				Destroy (gameobject);
				SetOnScreenText ("Wat een held ben je ook! Je hebt zojuist " + gameobject.name + " in je ...... gestoken");
		}
}