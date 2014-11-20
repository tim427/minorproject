using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColliderController : MonoBehaviour {

	//GameObject you might collide with 
	public Collider Schakelaar;
	public Collider DeurLinks;
	public Collider DeurRechts;

	//if you'd like to call a function on yourself when you collide with Schakelaar
	public string MessageToSelf;

	//calls a function on the collider object
	public string MessageToCollider;

	//If you want to call a function on yourself when they collide.
	public bool SendToSelf = false;

	public string OnCollisionEnterMessage, OnCollisionExitMessage, OnCollisionStayMessage,
	OnTriggerEnterMessage, OnTriggerExitMessage, OnTriggerStayMessage;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider == Schakelaar) {
			Schakelaar.SendMessage (OnCollisionEnterMessage, SendMessageOptions.DontRequireReceiver);
			if (SendToSelf == true) {
				this.SendMessage (MessageToSelf, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.collider == Schakelaar)
		{
			Schakelaar.SendMessage(OnCollisionExitMessage, SendMessageOptions.DontRequireReceiver);

			if (SendToSelf == true)
			{
				this.SendMessage(MessageToSelf, SendMessageOptions.DontRequireReceiver);
			}

		}
	}

	void OnTriggerEnter(Collider collision)
	{
		if (collision.collider == Schakelaar)
		{
			Schakelaar.SendMessage(OnTriggerEnterMessage, SendMessageOptions.DontRequireReceiver);

			if (SendToSelf == true)
			{
				this.SendMessage(MessageToSelf, SendMessageOptions.DontRequireReceiver);
			}

			print ("It's te god damm Schakelaar :)");

		}

		if (collision.collider == DeurLinks || collision.collider == DeurRechts)
		{
			Schakelaar.SendMessage(OnTriggerEnterMessage, SendMessageOptions.DontRequireReceiver);

			if (SendToSelf == true)
			{
				this.SendMessage(MessageToSelf, SendMessageOptions.DontRequireReceiver);
			}

			print ("It's te god damm Deur :)");

		}
	}

	void OnTriggerExit(Collider collision)
	{
		if (collision.collider == Schakelaar)
		{
			Schakelaar.SendMessage(OnTriggerExitMessage, SendMessageOptions.DontRequireReceiver);

			if (SendToSelf == true)
			{
				this.SendMessage(MessageToSelf, SendMessageOptions.DontRequireReceiver);
			}

		}
	}

	void OnTriggerStay(Collider collision)
	{
		if (collision.collider == Schakelaar)
		{
			Schakelaar.SendMessage(OnTriggerStayMessage, SendMessageOptions.DontRequireReceiver);

			if (SendToSelf == true)
			{
				this.SendMessage(MessageToSelf, SendMessageOptions.DontRequireReceiver);
			}

		}
	}
}