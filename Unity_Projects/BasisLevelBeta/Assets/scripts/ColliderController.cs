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
	private string url = "https://drproject.twi.tudelft.nl/ewi3620tu6/";
	private bool reusableLocked = false;
	private bool startTimer;
	private bool openCryoCell;
	private int moveCryoCell;
	private Collider tempColliderCryoCell;
	private static Texture2D _staticRectTexture;
	private static GUIStyle _staticRectStyle;
	private Collider tempColliderSimpleDoor;
	private int tempIntSimpleDoor;
	public float timeLeft;
	public bool switchOn = false;
	public bool keyUnlocked = false;
	public bool liftUnlocked = false;
	public bool showCollectables = false;
	public bool securityUnlocked = false;
	public bool armoryUnlocked = false;
	public bool officeUnlocked = false;
	public bool secondLiftUnlocked = false;
	public string onScreenText;
	public Font Font;
	public AudioClip collectSound;
	public int position_switch;
	public bool switchMove;
	public string userName = "tim";
	public int HighScore;
	public AudioClip explosionSound;

	
	void Start() {
		Time.timeScale = 1;	
		switchMove = false;
		position_switch = 0;
		UpdateCollectables();
		HighScore = 0;
		timeLeft = 15f;
		moveCryoCell = 0;
		pushText("You just started the game. Good luck!");
		foreach (GameObject Ceiling_Lamp in GameObject.FindGameObjectsWithTag("ceilLamp")) {
			Ceiling_Lamp.light.enabled = false;
				}
	}
	
	void FixedUpdate ()
	{
		getSecondScreenPressed();
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
		foreach (GameObject guard in GameObject.FindGameObjectsWithTag("Drone")) {
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
										// change the red directional lights to normal lamps
										foreach (GameObject ceilingLight in GameObject.FindGameObjectsWithTag("CeilingLights")) {
												ceilingLight.light.color = (switchOn ? new Color (0.64F, 0.82F, 1F, 1F) : new Color (1F, 0F, 0F, 1F));
												ceilingLight.light.intensity = (switchOn ? 0.3F : 0.4F);
												ceilingLight.light.range = (switchOn ? 30.0F : 40.0F); // old: 20.0F : 25.0F
												RenderSettings.ambientLight = (switchOn ? Color.black : Color.black);
										} //werkt niet >> Color(0.2f,0.2f,0.2f,1f);
										// Lamp objects
										foreach (GameObject Ceiling_Lamp in GameObject.FindGameObjectsWithTag("ceilLamp")) {
												Ceiling_Lamp.light.color = (switchOn ? new Color (0.64F, 0.82F, 1F, 1F) : new Color (1F, 0F, 0F, 1F));
												Ceiling_Lamp.light.intensity = (switchOn ? 0.5F : 0.1F);
						
										}
										// Directional lights
										foreach (GameObject DirLights in GameObject.FindGameObjectsWithTag("VerticalLights")) {
												DirLights.light.color = (switchOn ? new Color (0.64F, 0.82F, 1F, 1F) : new Color (1F, 0F, 0F, 1F));
												DirLights.light.intensity = (switchOn ? 0.1F : 0.1F);
										}
										GameObject.FindGameObjectWithTag ("MainCamera").camera.backgroundColor = (switchOn ? new Color (0.64F, 0.82F, 1F, 1F) : new Color (0.95F, 0.25F, 0.25F, 1F));
								} else { // turn off all lights
										SetOnScreenText ("The lights are malfunctioning due to excessive usage!");
										GameObject.FindGameObjectWithTag ("MainCamera").camera.backgroundColor = new Color (0F, 0F, 0F, 0F);
										foreach (GameObject ceilingLight in GameObject.FindGameObjectsWithTag("CeilingLights")) {
												ceilingLight.light.enabled = false; 
										}
										foreach (GameObject Ceiling_Lamp in GameObject.FindGameObjectsWithTag("ceilLamp")) {
												Ceiling_Lamp.light.color = (switchOn ? new Color (0F, 0F, 0F, 1F) : new Color (0F, 0F, 0F, 1F));
												Ceiling_Lamp.light.intensity = 0F;
										}
										foreach (GameObject DirLights in GameObject.FindGameObjectsWithTag("VerticalLights")) {
												DirLights.light.color = (switchOn ? new Color (0F, 0F, 0F, 1F) : new Color (0F, 0F, 0F, 1F));
												DirLights.light.intensity = 0F;
										print("directional light off");
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

		if (Collider != null && Collider.gameObject.tag == "SecurityLock" && !securityUnlocked) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				bool gottem = false;
				for (int i = 0; i < CollectedGameObjects.Count; i++) {
					if (CollectedGameObjects [i].name == "SecurityKey") {
						gottem = true;
					}
					
					
				}
				if (gottem) {
					SetOnScreenText ("You have the appropriate keycard.");	
					securityUnlocked = true;
					HighScore = HighScore + 5;
				} else {
					SetOnScreenText ("You lack the appropriate keycard.");
				}
			}
		}
		
		if (Collider != null && Collider.gameObject.tag == "SecurityDoor") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (securityUnlocked) {
					Collider.audio.Play ();
					SetOnScreenText ("You have opened the door.");
					tempCollider = Collider;
					HighScore = HighScore + 5;
					
				} else {
					SetOnScreenText ("The door won't open");
				}
			}
		}

		if (Collider != null && Collider.gameObject.tag == "ArmoryLock" && !armoryUnlocked) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				bool gottem = false;
				for (int i = 0; i < CollectedGameObjects.Count; i++) {
					if (CollectedGameObjects [i].name == "ArmoryKey") {
						gottem = true;
					}
					
					
				}
				if (gottem) {
					SetOnScreenText ("You have the appropriate keycard.");	
					armoryUnlocked = true;
					HighScore = HighScore + 5;
				} else {
					SetOnScreenText ("You lack the appropriate keycard.");
				}
			}
		}
		
		if (Collider != null && Collider.gameObject.tag == "ArmoryDoor") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (armoryUnlocked) {
					Collider.audio.Play ();
					SetOnScreenText ("You have opened the door.");
					tempCollider = Collider;
					HighScore = HighScore + 5;
					
				} else {
					SetOnScreenText ("The door won't open");
				}
			}
		}

		if (Collider != null && Collider.gameObject.tag == "OfficeLock" && !officeUnlocked) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				bool gottem = false;
				for (int i = 0; i < CollectedGameObjects.Count; i++) {
					if (CollectedGameObjects [i].name == "OfficeKey") {
						gottem = true;
					}
					
					
				}
				if (gottem) {
					SetOnScreenText ("You have the appropriate keycard.");	
					officeUnlocked = true;
					HighScore = HighScore + 5;
				} else {
					SetOnScreenText ("You lack the appropriate keycard.");
				}
			}
		}
		
		if (Collider != null && Collider.gameObject.tag == "OfficeDoor") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (officeUnlocked) {
					Collider.audio.Play ();
					SetOnScreenText ("You have opened the door.");
					tempCollider = Collider;
					HighScore = HighScore + 5;
					
				} else {
					SetOnScreenText ("The door won't open");
				}
			}
		}

		if (Collider != null && Collider.gameObject.tag == "SecondElevatorLock" && !secondLiftUnlocked) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				bool gottem = false;
				for (int i = 0; i < CollectedGameObjects.Count; i++) {
					if (CollectedGameObjects [i].name == "SecondElevatorKey") {
						gottem = true;
					}
					
					
				}
				if (gottem) {
					SetOnScreenText ("You have the appropriate keycard.");	
					secondLiftUnlocked = true;
					HighScore = HighScore + 5;
				} else {
					SetOnScreenText ("You lack the appropriate keycard.");
				}
			}
		}
		
		if (Collider != null && Collider.gameObject.tag == "SecondElevatorDoor") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (secondLiftUnlocked) {
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
					SetOnScreenText ("You have the appropriate Lift key. 15 seconds are left, to leave this floor.");	
					liftUnlocked = true;
					startTimer = true;
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

		if (Collider != null && Collider.gameObject.tag == "Cryocell" && openCryoCell != true) {
			//if (!PlayedGameObjects.Contains (Collider.gameObject)) {
				if (Input.GetKeyDown (KeyCode.Space)) {
					Collider.audio.Play ();
					//Collider.animation.Play ();
					//PlayedGameObjects.Add (Collider.gameObject);
					openCryoCell = true;
					tempColliderCryoCell = Collider;
				}
			//}
		}

		if (openCryoCell == true) {
			moveCryoCell += 1;
			print (moveCryoCell);
			if (moveCryoCell < 12 ) {
				tempColliderCryoCell.transform.FindChild("Cylinder").Translate(0, -0.006f, 0);
			}
			if (moveCryoCell >= 12 && moveCryoCell < 120 ) {
				tempColliderCryoCell.transform.FindChild("Cylinder").Translate( 0.0105f, 0, 0);
				tempColliderCryoCell.transform.FindChild ("Cylinder").Rotate (0, 0, 1.2f);
			}
			if (moveCryoCell >= 120 ) {
				openCryoCell = false;
				moveCryoCell = 0;
				Destroy (tempColliderCryoCell.gameObject.collider);
				tempColliderCryoCell = null;
			}
		}

		if (Collider != null && Collider.gameObject.tag == "simple_door" && Input.GetKeyDown (KeyCode.Space)) {
			tempColliderSimpleDoor = Collider;
		}
		
		if (tempColliderSimpleDoor != null) {
			tempColliderSimpleDoor.transform.Rotate (0, -2f, 0);
			tempColliderSimpleDoor.transform.Translate (-0.033f, 0, -0.001f);
			tempIntSimpleDoor += 1;
			if (tempIntSimpleDoor > 70){
				Destroy (tempColliderSimpleDoor.gameObject.collider);
				tempColliderSimpleDoor = null;
				tempIntSimpleDoor = 0;
			}
			
		}
		
		if (Collider != null && Collider.gameObject.tag == "Elevator") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				Vector3 newPos = new Vector3(2.5f,1f,100f);
				transform.position = newPos;
				startTimer = false;
				transform.eulerAngles = new Vector3(0f,270f,0f);
			}
		}


		if (Collider != null && Collider.gameObject.tag == "SecondElevator") {
			if (Input.GetKeyDown (KeyCode.Space)) {
				Application.LoadLevel("endgamesuccess");
			}
		}


		if (Collider != null && (Collider.gameObject.tag == "CollectableConsumable" || Collider.gameObject.tag == "CollectableReusable")) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				SetOnScreenText ("Successfully collected the " + Collider.gameObject.name);
				pushText ("You picked up a " + Collider.gameObject.name);
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

		// Race against clock
		if (startTimer == true) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0f){
				Application.LoadLevel("endgamefailure");
				pushText("Unfortunately, you have not achieved within the time!");
			}
		}
}
	
	void SetOnScreenText (string text)
	{
		onScreenText = text;
		StartCoroutine (AutoRemoveOnScreenText (onScreenText));
	}
	
	void pushText (string msg) {
		WWWForm form = new WWWForm();
		string pushMessage = WWW.EscapeURL(msg);
		WWW www = new WWW(url+"gcm.php?username="+userName+"&msg="+pushMessage);
		StartCoroutine( WaitForRequest( www ) );
	}
	
	IEnumerator AutoRemoveOnScreenText (string CurrentOnScreenText)
	{
		if (onScreenText != "" && CurrentOnScreenText == onScreenText) {
			yield return new WaitForSeconds (2);
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
		if (Collider.gameObject.tag == "SecondElevator") {
			SetOnScreenText ("Press <spacebar> to finish the game");
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
		int colorBoxHeight = 10;
		if (guardStateHighest == 0){
			CreateColorBox(new Rect (0, 0, Screen.width, colorBoxHeight), Color.green);
		} else if (guardStateHighest == 5){
			CreateColorBox(new Rect (0, 0, Screen.width, colorBoxHeight), Color.red);
		}else {
			CreateColorBox(new Rect (0, 0, Screen.width, colorBoxHeight), Color.yellow);
		}

		// GUI Box which shows the score of the player
		GUI.Box (new Rect (10, 770, 200, 30), "Score: " + HighScore);

		// GUI Box which shows the time of the player
		if (startTimer == true){
			GUI.Box (new Rect (10, 740, 200, 30), timeLeft + " seconds");
		}
	}
	
	void UpdateCollectables() {
		string postData = "{";
		for (int i = 0; i < CollectedGameObjects.Count; i++) {
			postData = postData + "\"" + CollectedGameObjects[i].name + "\": {\"type\": \""+CollectedGameObjects[i].tag+"\", \"identifier\": \""+CollectedGameObjects[i].GetInstanceID()+"\"}";
			if(i != CollectedGameObjects.Count - 1){
				postData = postData + ", ";
			}
		}
		postData = postData + "}";
//		print (postData);
		sendData(postData, "collectables");
	}
	
	void sendData (string postData, string type) {
		
		WWWForm form = new WWWForm();
		form.AddField("data", postData );
		
		Hashtable header = new Hashtable();
		
		header.Add("Content-Type", "application/x-www-form-urlencoded");
		
		WWW www = new WWW(url+"update.php?username="+userName+"&type="+type, form.data, header);
		
		StartCoroutine( WaitForRequest( www ) );
	}
	
	void getSecondScreenPressed () {
		WWWForm form = new WWWForm();
		WWW www = new WWW(url+"collectables_use.php?username="+userName);
		StartCoroutine( WaitForRequest( www ) );
	}
	
	void removeSecondScreenPressed (string id) {
		WWWForm form = new WWWForm();
		WWW www = new WWW(url+"collectables_use_remove.php?id="+id);
		StartCoroutine( WaitForRequest( www ) );
	}
	
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null)
		{
//			print("WWW Ok!: " + www.data);
			if(www.url.Contains("collectables_use.php")){
					for (int i = 0; i < CollectedGameObjects.Count; i++) {
						if (CollectedGameObjects [i].GetInstanceID().ToString() == www.data) {
							if(!reusableLocked){
								reusableLocked = true;
								removeSecondScreenPressed(www.data);
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
			if(www.url.Contains("collectables_use_remove.php")){
				print ("Removed!");
				reusableLocked = false;
			}
			if(www.url.Contains("gcm.php")){
				print ("Pushed message!");
			}
		} else {
//			print("WWW Error: "+ www.error);
		}    
	}  
	
	void Reuse (GameObject gameobject)
	{
		if(gameobject.name == "Flashlight"){
			if (!gameobject.activeSelf) {
				gameobject.SetActive (true);
				gameobject.transform.position = transform.position;
				gameobject.transform.Translate (0.3f, 0.4f, 0.2f);
				gameobject.transform.rotation = transform.rotation;
				gameobject.transform.Rotate (0, 270, 0);
				gameobject.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
			} else {
				gameobject.SetActive (false);
			}
			UpdateCollectables();
		}
	}
	
	void Consume (GameObject gameobject) {	
		if (gameobject.name == "EMPDevice") {
			CollectedGameObjects.Remove (gameobject);
			UpdateCollectables();
			Destroy (gameobject);	
			GameObject[] dronesList = GameObject.FindGameObjectsWithTag("Drone");
			AudioSource.PlayClipAtPoint(explosionSound, transform.position);
			SetOnScreenText("The EMP explosion causes all drones to go offline");
			pushText ("You successfully used a " + gameobject.name);
			for (int i = 0;i<dronesList.Length;i++) {
				if (Vector3.Distance(dronesList[i].transform.position,transform.position)<15) {
					dronesList[i].GetComponent<EnemyControllerNAV>().LedColour("grey");
					dronesList[i].GetComponent<EnemyControllerNAV>().enabled = false;
				}
			}
		}
		else {
			CollectedGameObjects.Remove (gameobject);
			UpdateCollectables();
			Destroy (gameobject);
			SetOnScreenText ("Wat een held ben je ook! Je hebt zojuist " + gameobject.name + " in je ...... gestoken");
		}
	}

	static void CreateColorBox(Rect position, Color color) 
	{
		if(_staticRectTexture == null){
			_staticRectTexture = new Texture2D(1,1);
		}
		
		if (_staticRectStyle == null) {
			_staticRectStyle = new GUIStyle();
		}
		
		_staticRectTexture.SetPixel(0,0,color);
		_staticRectTexture.Apply();
		
		_staticRectStyle.normal.background = _staticRectTexture;
		
		GUI.Box (position,GUIContent.none, _staticRectStyle);
		
	}

}