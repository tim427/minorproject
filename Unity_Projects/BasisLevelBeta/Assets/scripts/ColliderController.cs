using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColliderController : MonoBehaviour {

	public bool SwitchOn = false;
	public string OnScreenText;
	public Font Font;
	public bool spacePressed = false;

	void FixedUpdate() {
		if (spacePressed == false && Input.GetKeyDown(KeyCode.Space))
		{
			spacePressed = true;
		}
	}

	// Place here all "enter" events like pop-up messages
	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Schakelaar")
		{
			OnScreenText = "This is a Power Switch. To toggle the switch press the spacebar";
		}

		if (collision.gameObject.tag == "Deur")
		{
			OnScreenText = "To open the door press the spacebar";

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
			OnScreenText = "";

		}

		if (collision.gameObject.tag == "Deur")
		{
			OnScreenText = "";
		}
	}

	// Place here all "stay" events, like listeners to specific buttons for further actions
	void OnTriggerStay(Collider collision)
	{

		if (collision.gameObject.tag == "Schakelaar")
		{
			if (spacePressed) {
				spacePressed = false;
				SwitchOn = !SwitchOn;
				// collision.animation.Play ("Switch|SwitchAction");
				OnScreenText = "You just toggled the switch " +  (SwitchOn?"ON":"OFF");
			}
		}

		if (collision.gameObject.tag == "Deur")
		{
			if (spacePressed) {
				spacePressed = false;
				if (SwitchOn) {
					// collision.animation.Play ("Door|DoorAction");
					OnScreenText = "DOOR OPENS";

				} else {
					// collision.animation.Play ("Door|DoorAction");
					// in reverse
					OnScreenText = "FAILED! NO POWER ON DOOR\nTWEEDE REGEL";
				}
			}
		}
	}

	void OnGUI() {
		GUI.skin.font = Font;

		if (OnScreenText.Length > 0)
		{
			string[] lines = OnScreenText.Split('\n');
			int longestLineLength = 0;
			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i].Length > longestLineLength)
				{
					longestLineLength = lines[i].Length;
				}
				i++;
			}
			int width = longestLineLength * 9 + 30;
			int height = 30*lines.Length;
			int x = Screen.width/2 - width/2;
			int y = Screen.height/2;
			GUI.Box(new Rect(x, y, width, height), OnScreenText);
		}

	}
		
}