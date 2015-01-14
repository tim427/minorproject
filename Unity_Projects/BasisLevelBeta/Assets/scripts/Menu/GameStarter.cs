using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour {

	// Use this for initialization
	void OnMouseDown () {
		print ("mouse click");
		Application.LoadLevel ("Hoofdscene");
	}

	void OnMouseOver(){
		print ("Hovering");
		transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		}

	void OnMouseExit(){
		transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
		}
}
