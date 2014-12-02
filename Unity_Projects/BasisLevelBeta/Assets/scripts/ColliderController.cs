﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ColliderController : MonoBehaviour {
	
	private Collider Collider = null;
	private List<GameObject> CollectedGameObjects = new List<GameObject>();
	private List<GameObject> PlayedGameObjects = new List<GameObject>();
	public bool switchOn = false;
	public bool showCollectables = false;
	public string onScreenText;
	public Font Font;

	public Transform Guard;
	
	void Update() {
		Vector3 targetDir = transform.position - Guard.position;
		Vector3 forward = Guard.forward;
		float angle = Vector3.Angle(targetDir, forward);
		if (angle < 25.0F)
				onScreenText = "MONGOOL! Je bent gesnapt!";
		else
				onScreenText = "";

		/////
		
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			showCollectables = !showCollectables;
			print(CollectedGameObjects.Count);
			for (int i = 0; i < CollectedGameObjects.Count; i++)
			{
				print(CollectedGameObjects[0].name);
			}
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
		
		if (Collider != null && Collider.gameObject.tag == "Collectable")
		{
			if (Input.GetKeyDown(KeyCode.Space)) {
				onScreenText = "Successfully collected the " + Collider.gameObject.name;
				
				CollectedGameObjects.Add(Collider.gameObject);
				Collider.gameObject.SetActive(false);
			}
		}
	}
	
	// Place here all "enter" events like pop-up messages
	void OnTriggerEnter(Collider collision)
	{
		Collider = collision;
		if (Collider.gameObject.tag == "Switch")
		{
			onScreenText = "This is a Power Switch. To toggle the switch press the spacebar";
		}
		
		if (Collider.gameObject.tag == "Door")
		{
			onScreenText = "To open the door press the spacebar";
		}
		
		if (Collider.gameObject.tag == "Refrigerator") 
		{
			if (!PlayedGameObjects.Contains(Collider.gameObject))
			{
				Collider.animation.Play ();
				PlayedGameObjects.Add(Collider.gameObject);
			}
		}
		
		if (Collider.gameObject.tag == "Collectable") 
		{
			onScreenText = "To collect the " + Collider.gameObject.name + " press the spacebar!";
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