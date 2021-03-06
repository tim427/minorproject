﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

	public Light cameraLed;
	private float blinktime = 0.5f;
	private float direction;
	private bool started = false;
	public int maxLeft = 270;
	public int maxRight = 90;
	public float rotationSpeed = 1f;
	public float angleWidth = 60f;
	public float detectionDistance = 8f;
	private RaycastHit hitInfo;
	public static RaycastHit hitInfoLast;
	public float x;
	public float y;
	public float z;
	public AudioClip camera_detect;
	private bool canplaysound = true;
	
	void Update ()
	{
		float degrees = transform.FindChild ("Body").rotation.eulerAngles.z;
		if ((degrees >= maxRight - 1 && degrees <= maxRight + 1) || !started) {
			started = true;
			direction = rotationSpeed;
		}
		if (degrees >= maxLeft - 1 && degrees <= maxLeft + 1) {
			direction = -rotationSpeed;
		}
		transform.FindChild ("Body").Rotate (0, 0, direction);
		
		GameObject target = GameObject.FindGameObjectWithTag ("Player");
		Vector3 targetDir = target.transform.position - transform.FindChild ("Body").position;
		Vector3 forward = transform.FindChild ("Body").up;
		float angle = Vector3.Angle (forward, targetDir);
		float distance = Vector3.Distance (forward, targetDir);
		bool playerInSight = false;
		if (angle < angleWidth && distance < detectionDistance) {
			if (Physics.Raycast (transform.position, targetDir, out hitInfo, detectionDistance) && hitInfo.transform.tag == "Player") {

				playerInSight = true;
				x = hitInfo.transform.position.x;
				y = hitInfo.transform.position.y;
				z = hitInfo.transform.position.z;
				//				enemyController.triggerGuardExternal(GameObject.FindGameObjectWithTag ("Player"));

			}
		}

		UpdateLightColor(playerInSight);
	}

	void UpdateLightColor(bool playerDetected)
	{
		if(playerDetected){
			cameraLed.color = Color.red;
			if (canplaysound == true){
			audio.PlayOneShot (camera_detect,1.0f);
			canplaysound = false;
			}
		} else {
			cameraLed.color = Color.green;
			canplaysound = true;
		}
	}
}
