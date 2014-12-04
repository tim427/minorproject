using UnityEngine;
using System.Collections;

public class Deur_Links_movement : MonoBehaviour {
	
	float x_position;
	
	void Start () {
		// Defines the start position of the door.
		x_position = transform.position.x;
	}
	
	void Update () {
		// Moves the door to the right.
		if (transform.position.x < (x_position + 1.9) ) {
			transform.Translate (Time.deltaTime, 0, 0);
		}
	}
} 