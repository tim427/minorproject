﻿using UnityEngine;
using System.Collections;

public class GameQuitter : MonoBehaviour {
	
	// Use this for initialization
	void OnMouseOver () {
		print ("mouse click");
		Application.Quit();
	}
	
}