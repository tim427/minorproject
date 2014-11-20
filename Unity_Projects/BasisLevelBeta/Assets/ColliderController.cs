using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColliderController : MonoBehaviour {

	//GameObject you might collide with 
	public Collider Schakelaar;
	public Collider Deur;


	// Place here all "enter" events like pop-up messages
	void OnTriggerEnter(Collider collision)
	{
		if (collision.collider == Schakelaar)
		{

			print ("This is a Power Switch. Do you want to toggle the switch? Y/N");
		}

		if (collision.collider == Deur)
		{

			print ("Enter Deur :)");

		}
	}

	// Place here al "exit" events like removing the pop-up messages
	void OnTriggerExit(Collider collision)
	{
		if (collision.collider == Schakelaar)
		{
			print ("Exit Schakelaar :)");

		}

		if (collision.collider == Deur)
		{

			print ("Exit Deur :)");

		}
	}

	// Place here all "stay" events, like listeners to specific buttons for further actions
	void OnTriggerStay(Collider collision)
	{
		if (collision.collider == Schakelaar)
		{
			if (Input.GetKeyDown(KeyCode.Space))
				print("You just toggled the switch");
		}
	}
}