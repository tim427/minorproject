using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour {

	private Vector3 direction;
	public float enemySpeed;
	public float turnspeed;
	private Vector3 targetLoc;
//	public Transform target;
	private Transform cloned;
	private bool isTriggered = false;
	// Use this for initialization
	void Start () {
		direction = new Vector3(0,0,0);
	}
	
	// Update is called once per frame 
	void Update () {

		GameObject target = GameObject.FindGameObjectWithTag ("Player");
		Vector3 targetDir = target.transform.position - transform.position;
		Vector3 forward = transform.forward;
		float angle = Vector3.Angle(forward, targetDir);
		float distance = Vector3.Distance(forward, targetDir);
		if (angle < 25.0F && distance < 3.5F && !isTriggered)
		{
			print ("Detected by " + name);
			triggerGuard();
		}


		rigidbody.velocity = direction * 0;
		if (isTriggered) {
				moveTowardsTarget (targetLoc);
		}
		move (direction);
	}

	public void triggerGuard() {
		isTriggered = true;
		targetLoc = getTargetPos (GameObject.FindGameObjectWithTag ("Player"));
	}


	private void move(Vector3 dir) {
		rigidbody.velocity = dir * enemySpeed;
	}
	

	private void moveTowardsTarget(Vector3 target) {
			if (Vector3.Distance (target, transform.position) > 2) {
				Vector3 targetDir = target - transform.position;
				float step = turnspeed * Time.deltaTime;
				direction = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
				transform.rotation = Quaternion.LookRotation (direction);
			} 
			else { 
				direction = new Vector3 (0, 0, 0);
				isTriggered = false;
			}

	}

	//Function that allows the target of a guard to be set externally
	public Vector3 getTargetPos (GameObject gmObj) {
		return new Vector3(gmObj.transform.position.x, transform.position.y, gmObj.transform.position.z);
	}

}
