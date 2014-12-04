using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour {

	private Vector3 direction;
	public float enemySpeed;
	public float turnspeed;
	private Vector3 newDir;
	public Transform target;
	Transform cloned;
	private bool isTriggered = false;
	private bool isCloned = false;
	// Use this for initialization
	void Start () {
		direction = new Vector3(0,0,0);
		isTriggered = true;
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.velocity = direction * 0;
		if (isTriggered) {
			setTarget (GameObject.Find("Cube"));
			turnTowards (target);
		}
		move (direction);
	}


	private void move(Vector3 dir) {
		rigidbody.velocity = dir * enemySpeed;
	}


	//Function that determines the direction the enemy will move in
	private void turnTowards (Transform targ) {
		cloneTrigger(targ);
		moveTowardsTarget (cloned);

	}

	private void cloneTrigger (Transform targ) {
		if (!isCloned) {
			cloned = (Transform)Instantiate (targ.transform, targ.transform.position, targ.transform.rotation);
			cloned.renderer.enabled = false;
			cloned.collider.enabled = false;
			isCloned = true;
			print ("trying to clone");
		}
	}

	private void moveTowardsTarget(Transform cloned) {
		if (cloned != null) {
			if (Vector3.Distance (cloned.transform.position, transform.position) > 2) {
				Vector3 targetDir = cloned.position - transform.position;
				float step = turnspeed * Time.deltaTime;
				direction = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
				transform.rotation = Quaternion.LookRotation (direction);
				print ("trying to move");
			} 
			else { 
				print ("trying to destroy");
				direction = new Vector3 (0, 0, 0);
				isTriggered = false;
			}
		} 
		else {	
			print ("cloned = null");
		}
	}

	//Function that allows the target of a guard to be set externally
	public void setTarget (GameObject gmObj) {
		this.target = gmObj.transform;
	}

}
