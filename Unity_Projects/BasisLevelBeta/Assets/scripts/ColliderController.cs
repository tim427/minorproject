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
	private int guardStateHighest;
	public bool switchOn = false;
	public bool keyUnlocked = false;
	public bool liftUnlocked = false;
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
		
		guardStateHighest = 0;
		foreach (GameObject guard in GameObject.FindGameObjectsWithTag("Guard")) {
			if(guard.GetComponent<NavMeshPathfinder>().state > guardStateHighest)
			{
				guardStateHighest = guard.GetComponent<NavMeshPathfinder>().state;
			}
		}
		print(guardStateHighest);
		
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
		
		if (Collider != null && Collider.gameObject.tag == "DoorSwitch") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (switchOn) {
					Collider.audio.Play ();
					SetOnScreenText ("You have opened the door.");
					doorSubject = Collider;
					
				} else {
					SetOnScreenText ("The door won't open");
				}
			}
		}
		
		if (Collider != null && Collider.gameObject.tag == "KeyLock" && !keyUnlocked) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				bool gottem = false;
				for (int i = 0; i < CollectedGameObjects.Count; i++) {
					if (CollectedGameObjects [i].name == "Keycard") {
						gottem = true;
					}
					
					
				}
				if (gottem) {
					SetOnScreenText ("You have the appropriate keycard.");	
					keyUnlocked = true;
				} else {
					SetOnScreenText ("You lack the appropriate keycard.");
				}
				<<<<<<< HEAD
			}
		}
		
		if (Collider != null && Collider.gameObject.tag == "DoorKey") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (keyUnlocked) {
					Collider.audio.Play ();
					SetOnScreenText ("You have opened the door.");
					doorSubject = Collider;
					
				} else {
					SetOnScreenText ("The door won't open");
				}
			}
		}
		
		if (Collider != null && Collider.gameObject.tag == "LiftLock" && !liftUnlocked) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				bool gottem = false;
				for (int i = 0; i < CollectedGameObjects.Count; i++) {
					if (CollectedGameObjects [i].name == "Liftkey") {
						gottem = true;
						=======
						if (Collider != null && Collider.gameObject.tag == "Cryocell") {
							if (!PlayedGameObjects.Contains (Collider.gameObject)) {
								if (Input.GetKeyDown (KeyCode.Space)) {
									Collider.audio.Play ();
									Collider.animation.Play ();
									PlayedGameObjects.Add (Collider.gameObject);
								}
							}
						}
						
						if (Collider != null && Collider.gameObject.tag == "Elevator") {
							if (Input.GetKeyDown (KeyCode.Space)) {
								Application.LoadLevel("mainmenu");
								>>>>>>> origin/master
							}
							
							
						}
						<<<<<<< HEAD
						if (gottem) {
							SetOnScreenText ("You have the appropriate Lift key.");	
							liftUnlocked = true;
						} else {
							SetOnScreenText ("You lack the appropriate Lift key.");
							=======
							if (Collider != null && (Collider.gameObject.tag == "CollectableConsumable" || Collider.gameObject.tag == "CollectableReusable")) {
								if (Input.GetKeyDown (KeyCode.Space)) {
									SetOnScreenText ("Successfully collected the " + Collider.gameObject.name);
									AudioSource.PlayClipAtPoint (collectSound, transform.position);
									CollectedGameObjects.Add (Collider.gameObject);
									Collider.gameObject.SetActive (false);
									Destroy (Collider.gameObject.collider);
									Collider = null;
								}
								>>>>>>> origin/master
							}
						}
					}
					if (Collider != null && Collider.gameObject.tag == "DoorLift") {
						if (Input.GetKeyDown (KeyCode.Space)) {
							if (liftUnlocked) {
								Collider.audio.Play ();
								SetOnScreenText ("You have opened the door.");
								doorSubject = Collider;
								
							} else {
								// in reverse
								SetOnScreenText ("The door won't open");
							}
						}
					}
					if (Collider != null && Collider.gameObject.tag == "Cryocell") {
						if (!PlayedGameObjects.Contains (Collider.gameObject)) {
							if (Input.GetKeyDown (KeyCode.Space)) {
								Collider.audio.Play ();
								Collider.animation.Play ();
								PlayedGameObjects.Add (Collider.gameObject);
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
						SetOnScreenText ("Press <spacebar> to toggle the power switch.");
					}
					
					if (Collider.gameObject.tag == "Door") {
						SetOnScreenText ("Press <spacebar> to open the door.");
					}
					
					<<<<<<< HEAD
					if (Collider.gameObject.tag == "Cryocell") {
						SetOnScreenText ("Press <spacebar> to open the door.");
						=======
						if (Collider.gameObject.tag == "CollectableConsumable" || Collider.gameObject.tag == "CollectableReusable") {
							SetOnScreenText ("Press <spacebar> to collect " + Collider.gameObject.name + ".");
						}
						
						if (Collider.gameObject.tag == "Elevator") {
							SetOnScreenText ("Press <spacebar> to leave this floor");
						}
						>>>>>>> origin/master
					}
					
					if (Collider.gameObject.tag == "CollectableConsumable" || Collider.gameObject.tag == "CollectableReusable") {
						SetOnScreenText ("Press <spacebar> to collect " + Collider.gameObject.name + ".");
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
					
					GUI.Box (new Rect (10, 80	0, 200, 30), "Guard state: " + guardStateHighest);
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