using UnityEngine;
using System.Collections;

public class NavMeshPathfinder : MonoBehaviour {

	private NavMeshAgent navMesh;
	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		navMesh = GetComponent<NavMeshAgent>();
		navMesh.SetDestination(target.position);
	}
}
