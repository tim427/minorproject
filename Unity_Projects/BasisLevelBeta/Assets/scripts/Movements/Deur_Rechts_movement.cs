using UnityEngine;
using System.Collections;

public class Deur_Rechts_movement : MonoBehaviour {

	float x_position;
		
	void Start () {
		// Defines the start position of the door.
		x_position = this.transform.localPosition.x;
		print (x_position);
	}
		
	void Update () {
		// Moves the door to the left.
		if (this.transform.localPosition.x > (- 0.033f)) {
			transform.Translate (-Time.deltaTime, 0, 0);
		}
	}
}