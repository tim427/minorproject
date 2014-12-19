using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class NavMeshPathfinder : MonoBehaviour
{
	
	
	public Light stateLight;
	public float searchAngle;
	public float detectionAngle;
	public float detectionDistance;
	public Transform target;
	public float turnspeed = 1;
	public bool linkedWithCamera = true;
	
	private Vector3 inCameraPos;
	private float timeSinceDetection;
	private bool detection;
	private float startState2;
	private float timeSinceMovingInState4;
	private float timeSinceMovingInState5;
	private int state;
	private Vector3 initialRot;
	private NavMeshAgent navMesh;
	private Vector3 initialPos;
	private Vector3 targetRot;
	private int factor = -1;
	private float initialAngle;
	private Vector3 targetPos;
	
	
	// Use this for initialization
	void Start ()
	{
		
		detection = false;
		state = 0;
		
		initialRot = transform.forward;
		targetRot = initialRot;
		initialAngle = Vector3.Angle (Vector3.forward, initialRot);
		initialPos = transform.position;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		print ("state" + state);
		
		Vector3 targetDir = target.transform.position - transform.position;
		Vector3 forward = transform.forward;
		float angle = Vector3.Angle (forward, targetDir);
		float distance = Vector3.Distance (forward, targetDir);
		
		if (!detection) {
			timeSinceDetection = Time.time;
		}
		detection = false;
		
		// checking if guard detects player
		if (angle < detectionAngle && distance < detectionDistance) {
			if (Time.time - timeSinceDetection > 2 || state == 2 || state == 4 || state == 3) {
				
				state = 5;
				
			} else  if (state < 5) {
				state = 1;
			}
			
			detection = true;
			
			targetPos = target.position;
		}
		
		else if (state != 5 && CameraController.x != 0) {
			state = 3;
			CameraController.x = 0;
			inCameraPos = target.position;
			
		} else if (state == 4 && Time.time - timeSinceMovingInState4 > 10 || state == 3 && Time.time - timeSinceMovingInState5 > 5) {
			state = 2;
			startState2 = Time.time;
		} else if (state == 5) {
			state = 4;
			
		} else if (state == 2 && Time.time - startState2 > 10 || state == 1) {
			state = 0;
		} 
		
		
		
		
		switch (state) {
		case 0:
			
			MoveTo (initialPos);
			
			TurnTo (initialRot);
			
			LedColour ("green");
			
			break;
			
		case 1:
			
			TurnTo (targetPos - transform.position);
			
			LedColour ("yellow");
			
			break;
			
		case 2:
			MoveTo (initialPos);
			
			LookAround (searchAngle);
			
			LedColour ("yellow");
			
			break;
			
		case 3: 
			
			LedColour ("yellow");
			
			
			if (inCameraPos.x-transform.position.x<0.5F && inCameraPos.x-transform.position.x> -0.5F && inCameraPos.z-transform.position.z<0.5F && inCameraPos.z-transform.position.z>-0.5F ) {
				
				
				print ("targetPos bereiekt");
				
				LookAround (90);
				
			} else {
				
				timeSinceMovingInState5 = Time.time;
				MoveTo (inCameraPos);
				
			}
			
			
			break;
			
			
			
		case 4:
			
			LedColour ("yellow");
			
			
			if (targetPos.x - transform.position.x < 0.5F && targetPos.x - transform.position.x > -0.5F && transform.position.z - targetPos.z < 0.5F && transform.position.z - targetPos.z > -0.5F) {
				
				print ("targetPos bereiekt");
				
				LookAround (90);
				
			} else {
				
				timeSinceMovingInState4 = Time.time;
				MoveTo (targetPos);
			}
			
			break;
			
		case 5: 
			
			MoveTo (targetPos);
			
			LedColour ("red");

			if (target.position.x - transform.position.x < 1F && target.position.x - transform.position.x > -1F && transform.position.z - target.position.z < 1F && transform.position.z - target.position.z > -1F) {

				stopGame ();
			}
			
			break;
			
		}
		
		
		
	}
	
	// method to make enemy look arnoud with variable searchangle
	public void LookAround (float angle)
	{
		float step = turnspeed * Time.deltaTime;
		
		if (Vector3.Distance (transform.forward, targetRot) < 0.01F) {
			transform.forward = targetRot;
		}
		
		
		if (transform.forward == initialRot) {
			factor = factor * -1;
			targetRot = new Vector3 (Mathf.Sin (Mathf.Deg2Rad * factor * (angle + initialAngle * factor)), 0, Mathf.Cos (Mathf.Deg2Rad * factor * (angle + initialAngle * factor)));
		} else if (transform.forward == targetRot) {
			targetRot = initialRot;
		}
		
		Vector3 direction = Vector3.RotateTowards (transform.forward, targetRot, step, 0.0F);
		
		transform.rotation = Quaternion.LookRotation (direction);
		
	}
	
	public void MoveTo (Vector3 position)
	{
		
		navMesh = GetComponent<NavMeshAgent> ();
		navMesh.SetDestination (position);
		
		if (Vector3.Distance (transform.position, position) < 0.01f) {
			transform.position = position;
		}
	}
	
	public void TurnTo (Vector3 orientation)
	{
		
		float step = turnspeed * Time.deltaTime;
		Vector3 direction = Vector3.RotateTowards (transform.forward, orientation, step, 0.0F);
		transform.rotation = Quaternion.LookRotation (direction);
	}
	
	public void LedColour (string colour)
	{
		
		switch (colour) {
			
		case "red":
			stateLight.color = Color.red;
			break;
			
		case "green":
			stateLight.color = Color.green;
			break;
			
		case "yellow":
			stateLight.color = Color.yellow;
			break;
		}
		
	}

	public void stopGame() {

		print ("je bent af");

		Time.timeScale = 0;

	}
	
}
