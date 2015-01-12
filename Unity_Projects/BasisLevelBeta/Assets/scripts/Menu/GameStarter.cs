using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour {

	// Use this for initialization
	void OnMouseDown () {
		print ("mouse click");
		Application.LoadLevel ("Hoofdscene");
	}

}
