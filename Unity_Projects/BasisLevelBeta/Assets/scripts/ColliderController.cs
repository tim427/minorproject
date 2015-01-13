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
	private Collider tempCollider;
	private Collider tempColliderSwitch;
	private int guardStateHighest;
	private string updateUrl = "https://drproject.twi.tudelft.nl/ewi3620tu6/update.php";
	public bool switchOn = false;
	public bool keyUnlocked = false;
	public bool liftUnlocked = false;
	public bool showCollectables = false;
	public string onScreenText;
	public Font Font;
	public AudioClip collectSound;
	public int position_switch;
	public bool switchMove;
	public string userName = "tim";
	public int HighScore;
	
	void Start() {
		Time.timeScale = 1;	
		switchMove = false;
		position_switch = 0;
		UpdateCollectables();
		HighScore = 0;
	}
	void Update ()
	{
		guardStateHighest = 0;
		foreach (GameObject guard in GameObject.FindGameObjectsWithTag("Guard")) {
			if(guard.GetComponent<EnemyControllerNAV>().state > guardStateHighest)
			{
				guardStateHighest = guard.GetComponent<EnemyControllerNAV>().state;
			}
		}
		
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
				switchMove = true;
				tempColliderSwitch = Collider;
				// collision.animation.Play ("Switch|SwitchAction");
				Collider.audio.Play ();
				if (SwitchCounter < 20) {
					SetOnScreenText ("You just toggled the switch " + (switchOn ? "ON" : "OFF"));
					// change the red emergency lights to normal lamps
					foreach (GameObject ceilingLight in GameObject.FindGameObjectsWithTag("CeilingLights")) {
						ceilingLight.light.color = (switchOn ? new Color (0.64F, 0.82F, 1F, 1F) : new Color (1F, 0F, 0F, 1F));
						ceilingLight.light.intensity = (switchOn ? 0.3F : 0.4F );
						ceilingLight.light.range = (switchOn ? 20.0F : 25.0F );
						RenderSettings.ambientLight = (switchOn ? Color.black : Color.black); //werkt niet >> Color(0.2f,0.2f,0.2f,1f);
					}
					foreach (GameObject DirLights in GameObject.FindGameObjectsWithTag("VerticalLights")){
						DirLights.light.color = (switchOn ? new Color (0.64F, 0.82F, 1F, 1F) : new Color (1F, 0F, 0F, 1F));
						DirLights.light.intensity = (switchOn ? 0.1F : 0.2F );
					}
					GameObject.FindGameObjectWithTag ("MainCamera").camera.backgroundColor = (switchOn ? new Color (0.64F, 0.82F, 1F, 1F) : new Color (0.95F, 0.25F, 0.25F, 1F));
				} else {
					SetOnScreenText ("The lights are malfunctioning due to excessive usage!");
					GameObject.FindGameObjectWithTag ("MainCamera").camera.backgroundColor = new Color (0F, 0F, 0F, 0F);
					foreach (GameObject ceilingLight in GameObject.FindGameObjectsWithTag("CeilingLights")) {
						ceilingLight.light.enabled = false;
					}
				}
			}
		}

		if (Collider != null && Collider.gameObject.tag == "DoorSwitch") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (switchOn) {
					HighScore = HighScore + 5;
					Collider.audio.Play ();
					SetOnScreenText ("You have opened the door.");
					tempCollider = Collider;
					
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
					HighScore = HighScore + 5;
				} else {
					SetOnScreenText ("You lack the appropriate keycard.");
				}
			}
		}
		
		if (Collider != null && Collider.gameObject.tag == "DoorKey") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (keyUnlocked) {
					Collider.audio.Play ();
					SetOnScreenText ("You have opened the door.");
					tempCollider = Collider;
					HighScore = HighScore + 5;
					
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
					}
					
					
				}
				if (gottem) {
					SetOnScreenText ("You have the appropriate Lift key.");	
					liftUnlocked = true;
					HighScore = HighScore + 5;
				} else {
					SetOnScreenText ("You lack the appropriate Lift key.");
				}
			}
		}
		if (Collider != null && Collider.gameObject.tag == "DoorLift") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (liftUnlocked) {
					Collider.audio.Play ();
					SetOnScreenText ("You have opened the door.");
					tempCollider = Collider;
					
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
		
		if (Collider != null && Collider.gameObject.tag == "Elevator") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				Vector3 newPos = new Vector3(2.5f,1f,100f);
				transform.position = newPos;

				transform.eulerAngles = new Vector3(0f,270f,0f);
			}
		}
		if (Collider != null && (Collider.gameObject.tag == "CollectableConsumable" || Collider.gameObject.tag == "CollectableReusable")) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				SetOnScreenText ("Successfully collected the " + Collider.gameObject.name);
				AudioSource.PlayClipAtPoint (collectSound, transform.position);
				CollectedGameObjects.Add (Collider.gameObject);
				UpdateCollectables();
				Collider.gameObject.SetActive (false);
				Destroy (Collider.gameObject.collider);
				Collider = null;
			}
		}
		
		if (tempCollider != null) {
			bool doorLeftFinshed = false;
			bool doorRightFinshed = false;
			if (tempCollider.transform.FindChild ("Door_Left").localPosition.x < 1.49) {
				tempCollider.transform.FindChild ("Door_Left").Translate (Time.deltaTime, 0, 0);
			} else {
				doorLeftFinshed = true;
			}
			if (tempCollider.transform.FindChild ("Door_Right").localPosition.x > -1.69) {
				tempCollider.transform.FindChild ("Door_Right").Translate (-Time.deltaTime, 0, 0);	
			} else {
				doorRightFinshed = true;
			}
			if (doorLeftFinshed && doorRightFinshed) {
				Destroy (tempCollider.gameObject.collider);
				tempCollider = null;
			}
		}

		if (switchMove == true && tempColliderSwitch != null) {
			
			// Switch the switch upwards
			if (switchOn == false && position_switch > 0f) {
				tempColliderSwitch.transform.FindChild ("Switch").Rotate (0, 3, 0);
				position_switch--;
			}
			//Switch the switch downwards
			if (switchOn == true && position_switch < 30f) {
				tempColliderSwitch.transform.FindChild ("Switch").Rotate (0, -3, 0);
				position_switch++;
			}
			if (position_switch > 30f || position_switch < 0f){
				switchMove = false;
				tempColliderSwitch = null;
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
		if (onScreenText != "" && CurrentOnScreenText == onScreenText && Collider == null) {
			yield return new WaitForSeconds (0);
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
		
		if (Collider.gameObject.tag == "Cryocell") {
			SetOnScreenText ("Press <spacebar> to open the door.");
		}
		
		if (Collider.gameObject.tag == "CollectableConsumable" || Collider.gameObject.tag == "CollectableReusable") {
			SetOnScreenText ("Press <spacebar> to collect " + Collider.gameObject.name + ".");
		}
		
		if (Collider.gameObject.tag == "Elevator") {
			SetOnScreenText ("Press <spacebar> to leave this floor");
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

		// GUI Box which shows the guard state
		GUI.Box (new Rect (10, 800, 200, 30), "Guard state: " + guardStateHighest);

		// GUI Box which shows the score of the player
		GUI.Box (new Rect (10, 770, 200, 30), "Score: " + HighScore);
	}
	
	void UpdateCollectables() {
		string postData = "{";
		for (int i = 0; i < CollectedGameObjects.Count; i++) {
			postData = postData + "\"" + CollectedGameObjects[i].name + "\": \""+CollectedGameObjects[i].tag+"\"";
			if(i != CollectedGameObjects.Count - 1){
				postData = postData + ", ";
			}
		}
		postData = postData + "}";
		sendData(postData, "collectables");
	}
	
	void sendData (string postData, string type) {
		
		WWWForm form = new WWWForm();
		form.AddField("data", postData );
		
		Hashtable header = new Hashtable();
		
		header.Add("Content-Type", "application/x-www-form-urlencoded");
		
		WWW www = new WWW(updateUrl+"?username="+userName+"&type="+type, form.data, header);
		
		StartCoroutine( WaitForRequest( www ) );
	}
	
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null)
		{
			print("WWW Ok!: " + www.data);
		} else {
			print("WWW Error: "+ www.error);
		}    
	}  
	
	void Reuse (GameObject gameobject)
	{
		if (!gameobject.activeSelf) {
			gameobject.SetActive (true);
			gameobject.transform.position = transform.position;
			gameobject.transform.Translate (0.3f, 0.2f, 0.2f);
			gameobject.transform.rotation = transform.rotation;
			gameobject.transform.Rotate (0, 270, 0);
			gameobject.transform.parent = transform;
		} else {
			gameobject.SetActive (false);
		}
	}
	
	void Consume (GameObject gameobject)
	{
		CollectedGameObjects.Remove (gameobject);
		UpdateCollectables();
		Destroy (gameobject);
		SetOnScreenText ("Wat een held ben je ook! Je hebt zojuist " + gameobject.name + " in je ...... gestoken");
	}
}