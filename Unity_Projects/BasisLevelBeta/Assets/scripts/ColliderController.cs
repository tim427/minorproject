using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColliderController : MonoBehaviour {

	public bool SwitchOn = false;
	public string OnScreenText = "";

	// Place here all "enter" events like pop-up messages
	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Schakelaar")
		{
			print ("This is a Power Switch. To toggle the switch press the spacebar");
		}

		if (collision.gameObject.tag == "Deur")
		{

			print ("To open the door press the spacebar");

		}

		if (collision.gameObject.tag == "Koelkast") 
		{
			collision.animation.Play ("Cylinder|CylinderAction");
		}

	}

	// Place here al "exit" events like removing the pop-up messages
	void OnTriggerExit(Collider collision)
	{
		if (collision.gameObject.tag == "Schakelaar")
		{
			print ("SWITCH Helper text remove");

		}

		if (collision.gameObject.tag == "Deur")
		{

			print ("DOOR Helper text remove");

		}
	}

	// Place here all "stay" events, like listeners to specific buttons for further actions
	void OnTriggerStay(Collider collision)
	{

		if (collision.gameObject.tag == "Schakelaar")
		{
			if (Input.GetKeyDown (KeyCode.Space)) {
				SwitchOn = !SwitchOn;
				// collision.animation.Play ("Switch|SwitchAction");
				print ("You just toggled the switch " +  (SwitchOn?"ON":"OFF"));
			}
		}

		if (collision.gameObject.tag == "Deur")
		{
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (SwitchOn) {
					// collision.animation.Play ("Door|DoorAction");
					print ("DOOR OPENS");

				} else {
					// collision.animation.Play ("Door|DoorAction");
					// in reverse
					print ("FAILED! NO POWER ON DOOR");
					OnScreenText = "FAILED! NO POWER ON DOOR";
				}
			}
		}
	}

	void OnGUI() {

		GUI.Button (new Rect (10,10,150,20), OnScreenText);

	}
		
}