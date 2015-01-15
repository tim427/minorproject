using UnityEngine;
using System.Collections;

public class Procedural_OfficeKey : MonoBehaviour {		
	// Use this for initialization
	void Start () {
		double randomDoubleOfficeKey = Random.value;
		// Original position x  = -12.25 y = 4.32 z = 25.29
		if (randomDoubleOfficeKey >= 0 && randomDoubleOfficeKey < 0.2) {
			transform.Translate(-7f, 1.05f, -12f);
			transform.Rotate (0, 0, 180);
		}
			
		if (randomDoubleOfficeKey >= 0.2 && randomDoubleOfficeKey < 0.4) {
			transform.Translate(0.22f, 1.05f, -13f);
			transform.Rotate (0, 0, 180);
		}
			
		if (randomDoubleOfficeKey >= 0.4 && randomDoubleOfficeKey < 0.6) {
			transform.Translate(-2.341f, 1.05f, -31f);
			transform.Rotate (0, 0, 180);
		}
			
		if (randomDoubleOfficeKey >= 0.6 && randomDoubleOfficeKey < 0.8) {
			transform.Translate(-22.7f, 1.075f, -21.5f);
			transform.Rotate (0, 0, 180);
		}
			
		if (randomDoubleOfficeKey >= 0.8 && randomDoubleOfficeKey <= 1) {
			transform.Translate(-22.3f, 1.25f, 6.1f);
			transform.Rotate (0, 0, 180);
		}
	}
}