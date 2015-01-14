using UnityEngine;
using System.Collections;


public class gotoOptions : MonoBehaviour {
	public bool menu;
	public bool options;
	private bool video;
	private bool audiosettings;
	public float fieldOfView = 60f;
	public float sfxVolume = 0.5f;
	public float musicVolume = 0.5f;
	
	public int resX;
	public int resY;

	void OnMouseClick () {
		print ("clicked on options");
		GameObject obj = GameObject.Find ("First Person Controller");
		MainMenu menu = obj.GetComponent<MainMenu> ();
		menu.optionsMenu ();
		}

	public void optionsMenu() {
				if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 30), "Video Settings")) {
						options = false;
						video = true;
				}
		
				if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 0, 150, 30), "Audio Settings")) {
						options = false;
						audiosettings = true;
				}
		
				if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 + 50, 150, 30), "Back")) {
						menu = true;
						options = false;
				}
		}
}
