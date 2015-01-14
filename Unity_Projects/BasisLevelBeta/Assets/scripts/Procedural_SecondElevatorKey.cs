using UnityEngine;
using System.Collections;

public class Procedural_SecondElevatorKey : MonoBehaviour {

	// Use this for initialization
	void Start () {
		double randomSecondElevatorKey  = Random.value;

		if (randomSecondElevatorKey >= 0 && randomSecondElevatorKey < 0.5) {
			transform.Translate(-12.31f, 1.26f, -42.8f );
			transform.Rotate (0, 0, 180);
		}
	
		if (randomSecondElevatorKey >= 0.5 && randomSecondElevatorKey <= 1) {
			transform.Translate(-17.8f, 0.67f, -40.8f);
			transform.Rotate (0, 0, 180);
		}
	}
}
