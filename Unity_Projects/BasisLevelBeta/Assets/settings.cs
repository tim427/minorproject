using UnityEngine;
using System.Collections;

public class settings : MonoBehaviour {
	private bool menu = true;
	private bool options = false;
	private bool video = false;
	private bool audio = false;
	
	public float fieldOfView = 60f;
	public float sfxVolume = 0.5f;
	public float musicVolume = 0.5f;
	
	void OnGUI() {
		if (menu) {
			//Start the game, view/edit Options or quit the game
			if (GUI.Button(new Rect (Screen.width / 2, Screen.height / 2 - 50, 100, 30), "Start game")) {
				//Start the game
			}
			
			if (GUI.Button(new Rect (Screen.width / 2, Screen.height / 2, 100, 30), "Options")) {
				menu = false;
				options = true;
			}
			
			if (GUI.Button(new Rect (Screen.width / 2, Screen.height / 2 + 50, 100, 30), "Quit")) {
				Application.Quit ();
			}
		}
		
		if (options) {
			//View/edit video settings, view/edit audio settings or go back to the menu
			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 - 50, 100, 30), "Video Settings")) {
				options = false;
				video = true;
			}
			
			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 - 0, 100, 30), "Audio Settings")) {
				options = false;
				audio = true;
			}
			
			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 + 50, 100, 30), "Back")) {
				menu = true;
				options = false;
			}
		}
		
		if (video) {
			//Edit video settings (MSAA slider (0/2/4/8), Resolution(1080p/720p/480p), graphics quality,
			//field of view (60-110), Anisotropic filtering (on/off), vSync (on/off)
			string[] qualities = QualitySettings.names;

			//for loop to create Quality buttons
			for (int i=0; i < qualities.Length; i++) {
				if(GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 120 + i * 30, 100, 30), qualities[i])){
					QualitySettings.SetQualityLevel (i, true);
				}
			}
			GUI.Label(new Rect (Screen.width / 2 - 75, Screen.height/2 - 150, 100, 30), "Quality:");

			//Button to return to the previous menu
			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 + 90, 100, 30), "Back")) {
				//Go back to the menu
				video = false;
				options = true;
			}

			//Slider to adjust the field of view
			float tempFoV = GUI.HorizontalSlider(new Rect(Screen.width/2, Screen.height/2, 100, 30), fieldOfView, 60f, 110f);
			fieldOfView = (int)tempFoV;
			GUI.Label(new Rect (Screen.width / 2, Screen.height/2 - 15, 100, 30), "Field of view: " + fieldOfView);


			if (QualitySettings.vSyncCount == 1) {
				if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 - 120, 100, 30), "vSync: on")) {
					QualitySettings.vSyncCount = 0;
				}
			}
			else {
				if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 - 120, 100, 30), "vSync: off")) {
					QualitySettings.vSyncCount = 1;
				}
			}
			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 - 90, 100, 30), "Turn Anisotropic Filtering on")) {
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
			}
			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 - 60, 100, 30), "Turn Anisotropic Filtering off")) {
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
			}

		

		}
		if (audio) {
			//Slider for the Sound Effects volume
			sfxVolume = GUI.HorizontalSlider(new Rect(Screen.width/2, Screen.height/2 - 60, 100, 45), sfxVolume, 0.0f, 1.0f); 
			GUI.Label(new Rect(Screen.width/2, Screen.height/2 - 40, 150, 30), "SFX; " + sfxVolume);

			//Slider for the Music volume
			musicVolume = GUI.HorizontalSlider(new Rect(Screen.width/2, Screen.height/2 - 10, 100, 45), musicVolume, 0.0f, 1.0f); 
			GUI.Label(new Rect(Screen.width/2, Screen.height/2 + 10, 150, 30), "Music; " + musicVolume);

			//Button to return to the previous menu
			if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 + 60, 100, 30), "Back")) {
			//Go back to the menu
			audio = false;
			options = true;
			}



		}
	}
}
