using UnityEngine;
using System.Collections;

public class Procedural_SecondElevatorKey : MonoBehaviour {

	// Use this for initialization
	void Start () {
		double randomSecondElevatorKey  = Random.value;

		if (randomSecondElevatorKey >= 0 && randomSecondElevatorKey < 0.5) {
			transform.Translate(-42.8f, 1.26f, 12.31f);
			transform.Rotate (0, 0, 180);
		}
	
		if (randomSecondElevatorKey >= 0.5 && randomSecondElevatorKey <= 1) {
			transform.Translate(-40.8f, 0.67f, 17.8f);
			transform.Rotate (0, 0, 180);
		}
	}
}
