using UnityEngine;
using System.Collections;

public class Procedural_ArmoryKey : MonoBehaviour {
	// Use this for initialization
	void Start () {
		double randomDoubleArmoryKey = Random.value;
		if (randomDoubleArmoryKey >= 0 && randomDoubleArmoryKey < 0.2) {
			transform.Translate(-70f, 1.108f, 16.5f);
			transform.Rotate (0, 0, 180);
		}
		
		if (randomDoubleArmoryKey >= 0.2 && randomDoubleArmoryKey < 0.4) {
			transform.Translate(-63f, 1.11f, 20.25f);
			transform.Rotate (0, 0, 180);
		}
		
		if (randomDoubleArmoryKey >= 0.4 && randomDoubleArmoryKey < 0.6) {
			transform.Translate(-69.3f, 1.11f, 31.6f);
			transform.Rotate (0, 0, 180);
		}
		
		if (randomDoubleArmoryKey >= 0.6 && randomDoubleArmoryKey < 0.8) {
			transform.Translate(-66f, 0.01f, 40f);
			transform.Rotate (0, 0, 180);
		}
		
		if (randomDoubleArmoryKey >= 0.8 && randomDoubleArmoryKey <= 1) {
			transform.Translate(-71.8f, 0.975f, -14.05f);
			transform.Rotate (0, 0, 180);
		}
	}
}
