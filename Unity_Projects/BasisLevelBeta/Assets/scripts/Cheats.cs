using UnityEngine;
using System.Collections;

public class Cheats : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F2)) {
			transform.position = new Vector3(0, 1, 100);
			transform.Rotate(0, 270, 0);
		}
	}
}
