using UnityEngine;
using System.Collections;

public class koelkastAnimator : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Invoke ("openKoelkast", 2);
		
	}
	
	// Update is called once per frame
	void openKoelkast () 
	{
		animation.Play ("Cylinder|CylinderAction");
	}
}
