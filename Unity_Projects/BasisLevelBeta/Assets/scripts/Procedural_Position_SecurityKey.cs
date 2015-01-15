using UnityEngine;
using System.Collections;

public class Procedural_Position_SecurityKey : MonoBehaviour {

	// Use this for initialization
	void Start () {
		double randomDoubleSecurityKey = Random.value;
		if (randomDoubleSecurityKey >= 0 && randomDoubleSecurityKey < 0.16666) {
			transform.Translate(2.25f, 0.91f, 20.5f);
			transform.Rotate (0, 0, 180);
		}
		
		if (randomDoubleSecurityKey >= 0.1666 && randomDoubleSecurityKey < 0.3332) {
			transform.Translate(2.26f, 0.93f, 31f);
			transform.Rotate (0, 0, 180);
		}
		
		if (randomDoubleSecurityKey >= 0.3332 && randomDoubleSecurityKey < 0.50) {
			transform.Translate(-6.1f, 0.93f, 26.5f);
			transform.Rotate (0, 0, 180);
		}
		
		if (randomDoubleSecurityKey >= 0.50 && randomDoubleSecurityKey < 0.6666) {
			transform.Translate(-15.2f, 1.25f, 19.8f);
			transform.Rotate (0, 0, 180);
		}
		
		if (randomDoubleSecurityKey >= 0.6666 && randomDoubleSecurityKey < 0.8632) {
			transform.Translate(-22.5f, 1.02f, 22.2f);
			transform.Rotate (0, 0, 180);
		}
		
		if (randomDoubleSecurityKey >= 0.8632 && randomDoubleSecurityKey < 1) {
			transform.Translate(0f, 1.05f, 48f);
			transform.Rotate (0, 0, 180);
		}
	}
}