using UnityEngine;
using System.Collections;

public class DeviceScript : MonoBehaviour {
	private Collider Collider = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
				if (Collider != null && Collider.gameObject.tag == "Device") {
						if (Input.GetKeyDown (KeyCode.Space)) {
							foreach (GameObject DroneFixed in GameObject.FindGameObjectsWithTag("Guard")){
								gameObject.SetActive(false);
							}
						}
				}
		}
}
