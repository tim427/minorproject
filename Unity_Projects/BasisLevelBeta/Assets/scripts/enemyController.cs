using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour {
	
	public Vector3 direction;
	public float enemySpeed;
	public float turnspeed;
	private Vector3 targetLoc;
	public bool isTriggered = false;
	private Vector3 initialLoc;
	public bool moveBackNow;
	private Vector3 initialRot;
	public bool turningAroundNow;
	
	// Use this for initialization
	void Start () {
		direction = new Vector3(0,0,0);
		initialLoc = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		initialRot = transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity = direction * 0;
		GameObject target = GameObject.FindGameObjectWithTag ("Player");
		Vector3 targetDir = target.transform.position - transform.position;
		Vector3 forward = transform.forward;
		float angle = Vector3.Angle(forward, targetDir);
		float distance = Vector3.Distance(forward, targetDir);
		
		//Checking whether the guard detects a player
		if (angle < 25.0F && distance < 3.5F && !isTriggered)
		{
			print ("Detected by " + name);
			triggerGuard();
		}
		
		
		if (isTriggered) {
			moveTowardsTarget (targetLoc, 1f);
		}
		if (moveBackNow) {	
			moveBack ();
		}
		if (turningAroundNow) {
			turnAround();
		}
		
		move (direction);
	}
	
	//Function that triggers a guard and initializes the 'walking'
	public void triggerGuard() {
		turningAroundNow = false;
		moveBackNow = false;
		isTriggered = true;
		targetLoc = getTargetPos (GameObject.FindGameObjectWithTag ("Player"));
	}
	
	//Function that makes the guard move
	private void move(Vector3 dir) {
		rigidbody.velocity = dir * enemySpeed ;
	}
	
	//Function that directs the guard towards the target position
	private void moveTowardsTarget(Vector3 target, float distance) {
		if (Vector3.Distance (target, transform.position) < distance) {
			direction = new Vector3 (0, 0, 0);
			isTriggered = false;
			moveBackNow = true;
		} 
		else { 
			
			Vector3 targetDir = target - transform.position;
			float step = turnspeed * Time.deltaTime;
			direction = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
			transform.rotation = Quaternion.LookRotation (direction);
		}
		
	}
	
	//Function that allows the target of a guard to be set externally
	public Vector3 getTargetPos (GameObject gmObj) {
		return new Vector3(gmObj.transform.position.x, transform.position.y, gmObj.transform.position.z);
	}

	//Function that allows the guard to walk back to the initial position
	private void moveBack() {
		if (Vector3.Distance (initialLoc, transform.position) < 0.5f) {
			float step = turnspeed * Time.deltaTime;
			turningAroundNow = true;
			moveBackNow = false;
			
		} 
		else { 
			float step = turnspeed * Time.deltaTime;
			Vector3 targetDir = initialLoc - transform.position;
			direction = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
			transform.rotation = Quaternion.LookRotation (direction);
		}
		
	}

	//Function that allows a guard to turn around to his initial rotation
	private void turnAround() {
		float step = turnspeed * Time.deltaTime;
		if (transform.forward != initialRot) {
			direction = Vector3.RotateTowards (transform.forward, initialRot, step, 0.0F);
			transform.rotation = Quaternion.LookRotation (direction);
		}
		else {
			turningAroundNow = false;
			direction = new Vector3(0,0,0);
		}
	}
	
	
	
}
