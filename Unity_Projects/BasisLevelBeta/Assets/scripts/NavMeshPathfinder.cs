using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class NavMeshPathfinder : MonoBehaviour {


	public Light stateLight;
	public float searchAngle;
	public float detectionAngle;
	public float detectionDistance;
	public Transform target;
	public float turnspeed = 1;

	private float timeSinceDetection;
	private bool detection;
	private float startState1;
	private int state;
	private Vector3 initialRot;
	private NavMeshAgent navMesh;
	private Vector3 initialPos;
	private Vector3 targetRot;
	private int factor = -1;
	private float initialAngle;


	// Use this for initialization
	void Start () {

		detection = false;
		state = 0;

		initialRot = transform.forward;
		targetRot = initialRot;
		initialAngle = Vector3.Angle( Vector3.forward, initialRot);
		initialPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		print ("state" + state);

		Vector3 targetDir = target.transform.position - transform.position;
		Vector3 forward = transform.forward;
		float angle = Vector3.Angle(forward, targetDir);
		float distance = Vector3.Distance(forward, targetDir);

		if (!detection){
			timeSinceDetection = Time.time;
		}
		detection = false;

		// checking if guard detects player
		if (angle < detectionAngle && distance < detectionDistance)
		{
			if ( Time.time-timeSinceDetection>2){

				state = 3;
			}

			else if (state == 2){

				state = 3;

			}

			else  if ( state < 3){
				state = 1;
			}

			detection = true;
		}

		else if ( state == 3){
			state = 2;
			startState1 = Time.time;
		}

		else if (state == 2 && Time.time-startState1 > 10){
			state = 0;
		}

		else if (state == 1 ){
			state = 0;
		}

		else {
			detection = false;
		}




		switch (state){
		case 0 :

			MoveTo(initialPos);

			TurnTo (initialRot);

			LedColour("green");

			break;

		case 1 :

			TurnTo (target.position-transform.position);

			LedColour("yellow");

			break;

		case 2 :
			MoveTo (initialPos);

			LookAround(searchAngle);

			LedColour("yellow");

			break;

		case 3 : 

			MoveTo (target.position);

			LedColour("red");

			break;

		}

	}

	// method to make enemy look arnoud with variable searchangle
	public void LookAround(float angle) {
		float step = turnspeed * Time.deltaTime;

		print ("LOOKARNOuDM");
		print ("targetRot" + targetRot);
		if (Vector3.Distance(transform.forward, targetRot)<0.01F){
			transform.forward = targetRot;
		}
		
		
		if (transform.forward == initialRot){
			factor = factor*-1;
			targetRot = new Vector3(Mathf.Sin ( Mathf.Deg2Rad * factor *(angle+initialAngle*factor)),0,Mathf.Cos(Mathf.Deg2Rad * factor * (angle+initialAngle*factor)));
		}
		
		else if (transform.forward == targetRot){
			targetRot = initialRot;
		}
		
		Vector3 direction = Vector3.RotateTowards (transform.forward, targetRot, step, 0.0F);
		
		transform.rotation = Quaternion.LookRotation (direction);

	}

	public void MoveTo(Vector3 position){

		navMesh = GetComponent<NavMeshAgent>();
		navMesh.SetDestination(position);

		if (Vector3.Distance(transform.position, position) < 0.01f) {
			transform.position = position;
		}
	}

	public void TurnTo(Vector3 orientation){

		float step = turnspeed * Time.deltaTime;
		Vector3 direction = Vector3.RotateTowards (transform.forward, orientation, step, 0.0F);
		transform.rotation = Quaternion.LookRotation (direction);
	}

	public void LedColour(string colour){

		switch(colour){

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

}
