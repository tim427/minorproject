using UnityEngine;
using System.Collections;

public class Textfield : MonoBehaviour {
	public string userfieldvalue;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//OtherScript otherScript = GetComponent<ColliderController> ();
		//otherScript.userName = userfieldvalue;
	}

	void OnGUI(){
		userfieldvalue = GUI.TextField;
		print (userfieldvalue);


	}
}
