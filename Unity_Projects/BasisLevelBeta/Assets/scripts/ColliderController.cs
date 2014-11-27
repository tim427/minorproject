using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColliderController : MonoBehaviour {

	private Collider Collider = null;
	public bool switchOn = false;
    public bool hasCollectableTorch = false;
    public bool showCollectables = false;
	public string onScreenText;
	public Font Font;

	void Update() {

		if (Input.GetKeyDown(KeyCode.Tab))
        {
            showCollectables = !showCollectables;
        }

		if (Collider != null && Collider.gameObject.tag == "Switch")
		{
			if (Input.GetKeyDown(KeyCode.Space)) {
				switchOn = !switchOn;
				// collision.animation.Play ("Switch|SwitchAction");
				onScreenText = "You just toggled the switch " +  (switchOn?"ON":"OFF");
				foreach (GameObject ceilingLight in GameObject.FindGameObjectsWithTag("CeilingLights")) {
					ceilingLight.light.color = (switchOn?new Color(0.64F, 0.82F, 1F, 1F):new Color(1F, 0F, 0F, 1F));
				}
				GameObject.FindGameObjectWithTag("MainCamera").camera.backgroundColor = (switchOn?new Color(0.64F, 0.82F, 1F, 1F):new Color(0.95F, 0.25F, 0.25F, 1F));
			}
		}
		
		if (Collider != null && Collider.gameObject.tag == "Door")
		{
			if (Input.GetKeyDown(KeyCode.Space)) {
				if (switchOn) {
					// collision.animation.Play ("Door|DoorAction");
					onScreenText = "DOOR OPENS";
					
				} else {
					// collision.animation.Play ("Door|DoorAction");
					// in reverse
					onScreenText = "FAILED! NO POWER ON DOOR";
				}
			}
		}
		
		if (Collider != null && Collider.gameObject.tag == "Torch")
		{
			if (Input.GetKeyDown(KeyCode.Space)) {
				onScreenText = "Successfully collected the Torch";
				hasCollectableTorch = true;
				Destroy(Collider.gameObject);
			}
		}
	}

	// Place here all "enter" events like pop-up messages
	void OnTriggerEnter(Collider collision)
	{
		Collider = collision;
		if (collision.gameObject.tag == "Switch")
		{
			onScreenText = "This is a Power Switch. To toggle the switch press the spacebar";
		}

		if (collision.gameObject.tag == "Door")
		{
			onScreenText = "To open the door press the spacebar";
		}

        if (collision.gameObject.tag == "Refrigerator") 
		{
			collision.animation.Play ("Cylinder|CylinderAction");
		}

        if (collision.gameObject.tag == "Torch") 
        {
			onScreenText = "To collect the Torch press the spacebar!";
        }
	}

	// Place here al "exit" events like removing the pop-up messages
	void OnTriggerExit(Collider collision)
	{
		Collider = null;
		onScreenText = "";
	}

	void OnGUI() {
		GUI.skin.font = Font;

		if (onScreenText.Length > 0)
		{
			string[] lines = onScreenText.Split('\n');
			int longestLineLength = 0;
			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i].Length > longestLineLength)
				{
					longestLineLength = lines[i].Length;
				}
				i++;
			}
			int width = longestLineLength*9 + 30;
			int height = lines.Length*16 + 20;
			int x = Screen.width/2 - width/2;
            int y = Screen.height/2 - height/2;
			GUI.Box(new Rect(x, y, width, height), onScreenText);
		}

	}
}