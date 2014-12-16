using UnityEngine;
using System.Collections;

public class NavMeshPathfinder : MonoBehaviour {


	public Transform target;
	private NavMeshAgent navMesh;
	private bool isTriggered;
	private bool turningAroundNow;
	private bool moveBackNow;
	private Vector3 initialPos;


	// Use this for initialization
	void Start () {

		initialPos = transform.position;
		isTriggered = false;

	}
	
	// Update is called once per frame
	void Update () {


		Vector3 targetDir = target.transform.position - transform.position;
		Vector3 forward = transform.forward;
		float angle = Vector3.Angle(forward, targetDir);
		float distance = Vector3.Distance(forward, targetDir);
		moveBackNow = true;

		// checking if guard detects player
		if (angle < 25.0F && distance < 3.5F && !isTriggered)
		{
			// when detected set destination
			print ("Detected by " + name);
			turningAroundNow = false;
			moveBackNow = false;
			isTriggered = true;
		}


		if (isTriggered) {
			navMesh = GetComponent<NavMeshAgent>();
			navMesh.SetDestination(target.position);
			isTriggered = false;

		}
		if (moveBackNow) {	
			print ("Moving back");
			navMesh = GetComponent<NavMeshAgent>();
			navMesh.SetDestination(initialPos);
			turningAroundNow = true;
			moveBackNow = false;
		}
		if (turningAroundNow) {
		}


	}

	public void triggerGuard() {

	}
}
