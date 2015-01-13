using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour {

	// Use this for initialization
	void OnMouseOver () {
		print ("mouse click");
		Application.LoadLevel ("Hoofdscene");
	}

}
