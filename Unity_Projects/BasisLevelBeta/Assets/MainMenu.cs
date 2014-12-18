using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public bool menu;
	public bool options;
	private bool video;
	private bool audiosettings;
	public float fieldOfView = 60f;
	public float sfxVolume = 0.5f;
	public float musicVolume = 0.5f;
	
	public int resX;
	public int resY;
	// Use this for initialization
	void Start () {
		menu = true;
	}
	
	// Update is called once per frame
	
	void OnGUI() {
		if (menu) {
			mainMenu();
		}
		
		if (options) {
			optionsMenu();
		}
		
		if (video) {
			videoOptions();
		}
		
		if (audiosettings) {
			audioOptions ();
		}
	}
	
	//Function that describes the main menu
	private void mainMenu() {
		//Start the game, view/edit Options or quit the game
		if (GUI.Button(new Rect (Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 30), "Start game")) {
			Application.LoadLevel("Hoofdscene");
		}
		
		if (GUI.Button(new Rect (Screen.width / 2 - 75, Screen.height / 2, 150, 30), "Options")) {
			menu = false;
			options = true;
		}
		
		if (GUI.Button(new Rect (Screen.width / 2 - 75, Screen.height / 2 + 50, 150, 30), "Quit game")) {
			Application.Quit();
		}
	}
	
	//Function that describes the options menu
	private void optionsMenu() {
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
	
	//Function that describes the graphics options menu
	private void videoOptions() {		
		int currentQLevel = QualitySettings.GetQualityLevel();
		resX = Screen.width;
		resY = Screen.height;
		int[] antiAliasingOptions = new int[] {0,2,4,8};
		int antiAliasingCurrent = QualitySettings.antiAliasing;
		int aaIndex = 0;
		bool fs = Screen.fullScreen;
		string[] qualities = QualitySettings.names;
		int indexRes = 0;
		Resolution[] resolutions = Screen.resolutions;
		
		if (GUI.Button(new Rect(Screen.width /2 - 225, Screen.height / 2 -120, 150, 30), "Decrease")) {
			if (currentQLevel > 0) {
				QualitySettings.DecreaseLevel();
			}
			
		}
		
		GUI.Box(new Rect (Screen.width / 2 - 225, Screen.height/2 - 85, 150, 30), "Quality: " + qualities[QualitySettings.GetQualityLevel()]);
		
		if (GUI.Button(new Rect(Screen.width /2 - 225, Screen.height / 2 - 50, 150, 30), "Increase")) {
			if (currentQLevel < qualities.Length-1) {
				QualitySettings.IncreaseLevel();
			}
		}
		
		
		//Button to return to the previous menu
		if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 + 90, 150, 30), "Back")) {
			video = false;
			options = true;
		}
		
		//Slider to adjust the field of view
		float tempFoV = GUI.HorizontalSlider(new Rect(Screen.width/2 + 75, Screen.height/2 + 40, 150, 30), fieldOfView, 60f, 110f);
		fieldOfView = (int)tempFoV;
		GUI.Label(new Rect (Screen.width / 2 + 105, Screen.height/2 + 50, 150, 30), "Field of view: " + fieldOfView);
		
		
		if (QualitySettings.vSyncCount == 1) {
			if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 120, 150, 30), "vSync: on")) {
				QualitySettings.vSyncCount = 0;
			}
		}
		else {
			if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 120, 150, 30), "vSync: off")) {
				QualitySettings.vSyncCount = 1;
			}
		}
		
		if (QualitySettings.anisotropicFiltering == AnisotropicFiltering.Enable || QualitySettings.anisotropicFiltering == AnisotropicFiltering.ForceEnable) {
			if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 90, 150, 30), "Anisotropic Filtering: on")) {
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
			}
			
		}
		if (QualitySettings.anisotropicFiltering == AnisotropicFiltering.Disable) {
			if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 90, 150, 30), "Anisotropic Filtering: off")) {
				QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
			}
		}
		
		
		for (int i = 0; i < resolutions.Length; i++) {
			if (resolutions[i].height == resY && resolutions[i].width == resX) {
				indexRes = i;
			}
		}
		
		if (GUI.Button(new Rect(Screen.width /2 + 75, Screen.height / 2 - 120, 150, 30), "Decrease")) {
			if (indexRes > 1) {
				resX = resolutions[indexRes-1].width;
				resY = resolutions[indexRes-1].height;
				Screen.SetResolution(resX,resY,fs);
			}
		}
		
		GUI.Box (new Rect(Screen.width /2 + 75, Screen.height / 2 - 85, 150, 30), "Resolution: " + resX + "x" + resY);
		
		if (GUI.Button(new Rect(Screen.width /2 + 75, Screen.height / 2 - 50, 150, 30), "Increase")) {
			if (indexRes < resolutions.Length-1) {
				resX = resolutions[indexRes+1].width;
				resY = resolutions[indexRes+1].height;
				Screen.SetResolution(resX,resY,fs);
			}
		}
		
		
		if (Screen.fullScreen) {
			if(GUI.Button(new Rect(Screen.width/2 + 75, Screen.height / 2, 150, 30), "Windowed")) {
				Screen.SetResolution(resX,resY, false);
			}
		}
		else {
			if(GUI.Button(new Rect(Screen.width/2 + 75, Screen.height / 2	, 150, 30), "Fullscreen")) {
				Screen.SetResolution(resX,resY, true);
			}
		}
		
		
		for (int i = 0; i < 4; i++) {
			if (antiAliasingCurrent == antiAliasingOptions[i]) {
				aaIndex = i;
			}
		}
		if (GUI.Button(new Rect(Screen.width /2 - 225, Screen.height / 2, 150, 30), "Decrease")) {
			if (antiAliasingCurrent > antiAliasingOptions[0]) {
				QualitySettings.antiAliasing = antiAliasingOptions[aaIndex-1];
			}
		}
		
		GUI.Box (new Rect(Screen.width /2 - 225, Screen.height / 2 + 35, 150, 30), "Anti Aliasing: MSAAx" + antiAliasingCurrent);
		
		if (GUI.Button(new Rect(Screen.width /2 - 225, Screen.height / 2 + 70, 150, 30), "Increase")) {
			if (antiAliasingCurrent < antiAliasingOptions[3]) {
				QualitySettings.antiAliasing = antiAliasingOptions[aaIndex+1];
			}
		}
	}
	
	//Function that describes the audio options menu 
	private void audioOptions() {
		//Slider for the Sound Effects volume
		sfxVolume = GUI.HorizontalSlider(new Rect(Screen.width/2 - 75, Screen.height/2 - 60, 150, 45), sfxVolume, 0.0f, 1.0f); 
		GUI.Label(new Rect(Screen.width/2 - 30, Screen.height/2 - 40, 150, 30), "SFX; " + sfxVolume);
		
		//Slider for the Music volume
		musicVolume = GUI.HorizontalSlider(new Rect(Screen.width/2 - 75, Screen.height/2 - 10, 150, 45), musicVolume, 0.0f, 1.0f); 
		GUI.Label(new Rect(Screen.width/2 - 35, Screen.height/2 + 10, 150, 30), "Music; " + musicVolume);
		
		//Button to return to the previous menu
		if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 + 60, 150, 30), "Back")) {
			audiosettings = false;
			options = true;
		}
	}

}
