using UnityEngine;
using System.Collections;

public class Start_Position_FPC : MonoBehaviour {

	// Use this for initialization
	void Start () {
		double randomDoubleFPC = Random.value;
		if (randomDoubleFPC >= 0 && randomDoubleFPC < 0.16666) {
			transform.Translate(1.16f, 1f, -4.2f);
		}
		
		if (randomDoubleFPC >= 0.1666 && randomDoubleFPC < 0.3332) {
			transform.Translate(1.73f, 1f, 4.2f);
			transform.Rotate (0, 180, 0);
		}
		
		if (randomDoubleFPC >= 0.3332 && randomDoubleFPC < 0.50) {
			transform.Translate(-1f, 1f, -4.2f);
		}
		
		if (randomDoubleFPC >= 0.50 && randomDoubleFPC < 0.6666) {
			transform.Translate(1.9f, 1f, 4.2f);
			transform.Rotate (0, 180, 0);
		}
		
		if (randomDoubleFPC >= 0.6666 && randomDoubleFPC < 0.86328) {
			transform.Translate(3.5f, 1f, -4.2f);
		}
		
		if (randomDoubleFPC >= 0.8632 && randomDoubleFPC < 1) {
			transform.Translate(4.15f, 1f, 4.2f);
			transform.Rotate (0, 180, 0);
		}
	}
}
