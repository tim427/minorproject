using UnityEngine;
using System.Collections;


[RequireComponent(typeof(NavMeshAgent))]

public class EnemyControllerNAV : MonoBehaviour
{
	
	public Light stateLight;
	public float searchAngle;
	public float detectionAngle;
	public float detectionDistance;
	public Transform target;
	public float turnspeed = 1;
	public bool linkedWithCamera = true;
	public int state;
	public bool intelligentPatrolling;
	public Vector3[] patrolPositions = new Vector3[5];
	
	private float weightFactor = 1.0005f;
	private float forgettingFactor = 1.00000005f;
	private Vector3 inCameraPos;
	private float timeSinceDetection;
	private bool detection;
	private float startState2;
	private float timeSinceMovingInState4;
	private float timeSinceMovingInState5;
	private float timeSinceMoving;
	private Vector3 initialRot;
	private NavMeshAgent navMesh;
	private Vector3 initialPos;
	private Vector3 targetRot;
	private int factor = -1;
	private float initialAngle;
	private Vector3 targetPos;
	private RaycastHit hitInfo;
	private bool needNewPatrolTarget = true;
	private Vector3 patrolTarget;
	private float[,] patrolPathsWeight;
	private int addNum;
	private int patrolTargetMemory;
	private int patrolPositionNum = 0;
	private bool initialWeightUpdate = true;
	private Animator animatoriation;
	private Vector3 currentPosition;
	private Vector3 previousPosition;
	
	// Use this for initialization
	void Start ()
	{
		// set initial state
		detection = false;
		state = 0;
		// fill WeightsVector with ones
		patrolPathsWeight = new float[patrolPositions.Length,patrolPositions.Length];

		for (int i = 0; i<patrolPositions.Length; i++){
			for (int j = 0; j<patrolPositions.Length; j++){
				patrolPathsWeight[i,j]=1;
			}
		}


		// determine initial data
		initialRot = transform.forward;
		targetRot = initialRot;
		initialAngle = Vector3.Angle (Vector3.forward, initialRot);
		initialPos = transform.position;
		currentPosition = transform.position;
		previousPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{

		
		// Retrieve info of target every frame
		Vector3 targetDir = target.transform.position - transform.position;
		Vector3 forward = transform.forward;
		float angle = Vector3.Angle (forward, targetDir);
		float distance = Vector3.Distance (forward, targetDir);
		
		
		// Update weights when detected
		if (detection){
			UpdateWeights();
			initialWeightUpdate = false;
		} else { 
			// store time when not detected
			timeSinceDetection = Time.time;
			initialWeightUpdate = true;
		}

		Animation ();
		InvokeRepeating("ForgetWeights",0, 1);
		StateDefiner(targetDir, angle, distance);
		GuardAction(state);

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
		case "grey":
			stateLight.color = Color.grey;
			break;
		}

	}
	
	
	public void stopGame() 
	{
		Application.LoadLevel("endgamefailure");
	}
	
	
	public void Patrolling(Vector3[] patrolTargets)
	{
		if (patrolTarget.x - transform.position.x < 0.5F && patrolTarget.x - transform.position.x > -0.5F && transform.position.z - patrolTarget.z < 0.5F && transform.position.z - patrolTarget.z > -0.5F){
			needNewPatrolTarget = true;
		}
		
		if (needNewPatrolTarget){
			patrolTargetMemory = patrolPositionNum;
			patrolPositionNum = TargetChooser();
			patrolTarget = patrolTargets[patrolPositionNum];
			needNewPatrolTarget = false;
		}
		
		MoveTo (patrolTarget);
		LedColour ("green");
	}
	
	
	public void UpdateWeights() 
	{
		if (patrolPositionNum<patrolTargetMemory){

			if (initialWeightUpdate){
				patrolPathsWeight[patrolPositionNum,patrolTargetMemory]=patrolPathsWeight[patrolPositionNum,patrolTargetMemory]*Mathf.Pow (weightFactor,50);
			}

			patrolPathsWeight[patrolPositionNum,patrolTargetMemory]=patrolPathsWeight[patrolPositionNum,patrolTargetMemory]*weightFactor; 
		}
		
		if (patrolTargetMemory<patrolPositionNum){
			if (initialWeightUpdate){
				patrolPathsWeight[patrolTargetMemory,patrolPositionNum]=patrolPathsWeight[patrolTargetMemory,patrolPositionNum]*Mathf.Pow (weightFactor,50);
			}

			patrolPathsWeight[patrolTargetMemory,patrolPositionNum]=patrolPathsWeight[patrolTargetMemory,patrolPositionNum]*weightFactor; 
		}
	}

	public void ForgetWeights()
	{
		for (int i = 0; i<patrolPositions.Length; i++){
			for (int j = i+1; j<patrolPositions.Length; j++){
				patrolPathsWeight[i,j] = Mathf.Pow(patrolPathsWeight[i,j], 1/forgettingFactor);
			}
		}
	}
	
	public void GuardAction(int state) 
	{
		switch (state) {
			// state 0: initial state, means enemy is at rest or enemy is patrolling. This depends on the boolean intelligentPatrolling
		case 0:

			if (intelligentPatrolling){
				Patrolling(patrolPositions);
			} else {
				MoveTo (initialPos);
				TurnTo (initialRot);
				LedColour ("green");
			}
			break;
			// state 1: enemy wakes up but doesn't take any action yet, except for looking at the player
		case 1:
			TurnTo (targetPos - transform.position);
			LedColour ("yellow");
			break;
			// state 2: enemy is returning back to initial state, but remains attentive
		case 2:
			MoveTo (initialPos);
			LookAround (searchAngle);
			LedColour ("yellow");
			break;
			// state 3: enemy has reached last known position of his target, he looks around in this place. Enemy is attentive
		case 3: 
			LedColour ("yellow");

			if (inCameraPos.x-transform.position.x<0.5F && inCameraPos.x-transform.position.x> -0.5F && inCameraPos.z-transform.position.z<0.5F && inCameraPos.z-transform.position.z>-0.5F ) {
				LookAround (90);
			} else {
				timeSinceMovingInState5 = Time.time;
				MoveTo (inCameraPos);
			}

			break;
			// state 4: enemy is moving to last known place of its target. Enemy is attentive
		case 4:
			LedColour ("yellow");

			if (targetPos.x - transform.position.x < 0.5F && targetPos.x - transform.position.x > -0.5F && transform.position.z - targetPos.z < 0.5F && transform.position.z - targetPos.z > -0.5F) {
				LookAround (90);
			} else {
				timeSinceMovingInState4 = Time.time;
				MoveTo (targetPos);
			}

			break;
			// state 5: enemy is alerted and follows his target
		case 5:
			MoveTo (targetPos);
			LedColour ("red");

			if (target.position.x - transform.position.x < 2F && target.position.x - transform.position.x > -2F && transform.position.z - target.position.z < 2F && transform.position.z - target.position.z > -2F) {	
				stopGame ();
			}

			break;
		}
	}
	
	public void StateDefiner(Vector3 targetDir, float angle, float distance)
	{
		detection = false;
		
		if (angle < detectionAngle && distance < detectionDistance && Physics.Raycast (transform.position, targetDir, out hitInfo, detectionDistance) && hitInfo.transform.tag == "Player") {


	
			if ((Time.time - timeSinceDetection)/(distance*0.15) > 2 || state == 2 || state == 4 || state == 3 || state == 0 && intelligentPatrolling) {
				state = 5;	
			} else  if (state < 5) {
				state = 1;
			}

			detection = true;
			targetPos = target.position;
		} else if (state != 5 && CameraController.x != 0) {
			state = 3;
			CameraController.x = 0;
			inCameraPos = target.position;
		} else if (state == 4 && Time.time - timeSinceMovingInState4 > 10 || state == 3 && Time.time - timeSinceMovingInState5 > 5) {
			
			if ( intelligentPatrolling ){
				state = 0;
			} else {
				state = 2;
				startState2 = Time.time;
			}

		} else if (state == 5) {
			state = 4;
		} else if (state == 2 && Time.time - startState2 > 10 || state == 1 || isNotMoving()) {
			state = 0;
		} 
	}

	public int TargetChooser()
	{
		int res = 0;
		float sumWeights = 0.0f;

		for (int i = 0; i<patrolPositionNum; i++){
			sumWeights = sumWeights + patrolPathsWeight[i,patrolPositionNum];
		}

		for (int i = patrolPositionNum+1; i<patrolPositions.Length; i++){
			sumWeights = sumWeights + patrolPathsWeight[patrolPositionNum,i];
		}

		float treshold = Random.Range(0.0f,sumWeights);
		bool stopPickingTarget = false;

		for (int i = 0; i<patrolPositionNum; i++){
			treshold = treshold - patrolPathsWeight[i,patrolPositionNum];
			if (treshold<0.0f){
				res = i;
				stopPickingTarget = true;
				break;
			}
		}
		if(!stopPickingTarget){
			for ( int i = patrolPositionNum+1; i<patrolPositions.Length; i++){
				treshold = treshold - patrolPathsWeight[patrolPositionNum,i];
				if(treshold<0){
					res = i;
					break;
				}
			}
		}

		return res;
	}

	bool IsMoving()
	{
		bool res = true;

		if(!intelligentPatrolling && state == 0 || state == 1){
			res = false;
		}
		return res;
	}

	void Animation(){

		if (GetComponent<Animator>() != null){
			if(!isNotMoving()){
				GetComponent<Animator>().enabled = true;
			} else {
				GetComponent<Animator>().enabled = false;
	        }
		}
        
    }

	bool isNotMoving(){
		bool res = false;
		previousPosition = currentPosition;
		currentPosition = transform.position;

		if (Vector3.Distance(currentPosition, previousPosition) < 0.01f){
			if (Time.time - timeSinceMoving > 3) {
				res = true;
			}
		} else {
			timeSinceMoving = Time.time;
		}

		return res;
	}
}