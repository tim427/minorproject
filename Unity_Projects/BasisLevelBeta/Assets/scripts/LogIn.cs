using UnityEngine;
using System.Collections;

public class LogIn : MonoBehaviour {
	
	private string usernameString = string.Empty;
	private string passwordString = string.Empty;
	
	public bool showLogin = false;
	
	private Rect windowRect = new Rect(0, 0, Screen.width, Screen.height);
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI () {
		if(showLogin) {
			GUI.Window(0, windowRect, WindowFunction, "Login");
		}
	}
	
	void WindowFunction (int windowID){
		usernameString = GUI.TextField (new Rect (Screen.width / 3, 2 * Screen.height / 5, Screen.width / 3, Screen.height / 10), usernameString, 10);
		passwordString = GUI.PasswordField (new Rect (Screen.width / 3, 2 * Screen.height / 3, Screen.width / 3, Screen.height / 10), passwordString, "*"[0], 10);
		
		if (GUI.Button (new Rect (Screen.width / 2, 4 * Screen.height / 5, Screen.width / 8, Screen.height / 8), "Login")) {
			WWWForm form = new WWWForm();
			WWW www = new WWW("https://drproject.twi.tudelft.nl/ewi3620tu6/scores.php?username="+usernameString);
			StartCoroutine( WaitForRequest( www ) );			
		}
		
		GUI.Label(new Rect(Screen.width/3, 35*Screen.height/100, Screen.width/5, Screen.height/8), "Username");
		GUI.Label(new Rect(Screen.width/3, 62*Screen.height/100, Screen.width/8, Screen.height/8), "Password");
	}
	
	IEnumerator WaitForRequest(WWW www)
	{
			yield return www;
			// check for errors
			if (www.error == null)
			{
//							print("WWW Ok!: " + www.data);
				if(www.data != "false") {
					Debug.Log ("Hi USER!");
					GameObject.FindGameObjectWithTag("Player").GetComponent<ColliderController>().userName = usernameString;
				}
				else {
					Debug.Log ("Wrong username or password");
				}
			} else {
//							print("WWW Error: "+ www.error);
			}    
	} 
}
