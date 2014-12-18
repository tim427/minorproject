using UnityEngine;
using System.Collections;

public class NavMeshPathfinder : MonoBehaviour {



	public float searchAngle;
	public float detectionAngle;
	public float detectionDistance;
	public Transform target;
	public int turnspeed = 1;

	private Vector3 initialRot;
	private NavMeshAgent navMesh;
	private bool isTriggered;
	private bool turningAroundNow;
	private bool moveBackNow;
	private Vector3 initialPos;
	private Vector3 targetRot;
	private int factor = -1;


	// Use this for initialization
	void Start () {


		initialRot = transform.forward;
		initialPos = transform.position;
		isTriggered = false;

	}
	
	// Update is called once per frame
	void Update () {


//		transform.Rotate(Time.deltaTime, 0, 0);

		Vector3 targetDir = target.transform.position - transform.position;
		Vector3 forward = transform.forward;
		float angle = Vector3.Angle(forward, targetDir);
		float distance = Vector3.Distance(forward, targetDir);
		isTriggered = false;

		// checking if guard detects player
		if (angle < detectionAngle && distance < detectionDistance && !isTriggered)
		{
			// when detected set destination
			navMesh = GetComponent<NavMeshAgent>();
			navMesh.SetDestination(target.position);
			isTriggered = true;
		}


		if (!isTriggered) {
			navMesh = GetComponent<NavMeshAgent>();
			navMesh.SetDestination(initialPos);

		}

		bool rotating = true;

	
		if (rotating = true){

			float step = turnspeed * Time.deltaTime;

			print (transform.forward == targetRot);
			print (transform.forward);
			print (targetRot);


			if (transform.forward==initialRot){
				print ("check");
				factor = factor*-1;
				targetRot = new Vector3(Mathf.Sin ( Mathf.Deg2Rad * factor *searchAngle),0,Mathf.Cos(Mathf.Deg2Rad * factor * searchAngle));
			}

			else if (transform.forward == targetRot){
				print ("check1");
				targetRot = initialRot;
			}

			Vector3 direction = Vector3.RotateTowards (transform.forward, targetRot, step, 0.0F);

			transform.rotation = Quaternion.LookRotation (direction);

		}


//		if (navMesh.destination == transform.position)
//
//
//		if (moveBackNow) {	
//			print ("Moving back");
//			navMesh = GetComponent<NavMeshAgent>();
//			navMesh.SetDestination(initialPos);
//			turningAroundNow = true;
//			moveBackNow = false;
//		}
//		if (turningAroundNow) {
//
//			float step = turnspeed * Time.deltaTime;
//			targetDir = initialPos - transform.position;
//			Vector3 direction = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
//			transform.rotation = Quaternion.LookRotation (direction);
//
//		}
	}

}
