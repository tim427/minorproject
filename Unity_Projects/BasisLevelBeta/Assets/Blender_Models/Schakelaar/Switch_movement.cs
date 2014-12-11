using UnityEngine;
using System.Collections;

public class Switch_movement : MonoBehaviour {

	int total_rotation;

	void Start() {
		total_rotation = 0;
		}

	void Update () {
		if (total_rotation < (200)) {
			transform.Rotate(0, 1, 0);
			total_rotation = total_rotation + 2;
		}
	}
}