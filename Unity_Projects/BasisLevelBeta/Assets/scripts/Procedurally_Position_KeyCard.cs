using UnityEngine;
using System.Collections;

public class Procedurally_Position_KeyCard : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		double randomDoubleKeyCard = Random.value;
		// Original position x  = 9.602 y = 1.259 z = 1.181
		if (randomDoubleKeyCard >= 0 && randomDoubleKeyCard < 0.125) {
			transform.Translate(0, 0, 0);
			transform.Rotate (0, 0, 180);
		}

		if (randomDoubleKeyCard >= 0.125 && randomDoubleKeyCard < 0.25) {
			transform.Translate(-2.5f, 0, 0);
			transform.Rotate (0, 0, 180);
		}

		if (randomDoubleKeyCard >= 0.25 && randomDoubleKeyCard < 0.375) {
			transform.Translate(-6.191f, -0.235f, 8.623f);
			transform.Rotate (0, 0, 180);
		}

		if (randomDoubleKeyCard >= 0.375 && randomDoubleKeyCard < 0.5) {
			transform.Translate(-11.2f, -0.015f, 20.761f);
			transform.Rotate (0, -270, 180);
		}

		if (randomDoubleKeyCard >= 0.5 && randomDoubleKeyCard < 0.625) {
			transform.Translate(-11.2f, -0.015f, 26.361f);
			transform.Rotate (0, -270, 180);
		}

		if (randomDoubleKeyCard >= 0.625 && randomDoubleKeyCard < 0.75) {
			transform.Translate(-24.5f, -0.15f, 19.361f);
			transform.Rotate (0, 0, 180);
		}

		if (randomDoubleKeyCard >= 0.75 /*&& randomDoubleKeyCard < 0.875*/) {
			transform.Translate(-26.2f, -0.15f, 19.361f);
			transform.Rotate (0, 0, 180);
		}

		if (randomDoubleKeyCard >= 0.875 && randomDoubleKeyCard <= 1.0) {
			transform.Translate(3.909f, -0.242f, 1.606f);
			transform.Rotate (0, 0, 180);
		}
	}
}