using UnityEngine;
using System.Collections;

public class ParticleSorter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		particleSystem.renderer.sortingOrder = -10;
	}

}
