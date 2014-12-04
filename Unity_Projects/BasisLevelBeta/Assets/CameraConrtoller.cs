using UnityEngine;
using System.Collections;

public class CameraConrtoller : MonoBehaviour {

	private float direction;
	public int maxLeft = 270;
	public int maxRight = 90;
	public float rotationSpeed = 1f;
	public float angleWidth = 30f;
	public float detectionDistance = 3f;

	void Update () {
		float degrees = transform.FindChild("Body").rotation.eulerAngles.z;
		if (degrees >= maxRight-1 && degrees <= maxRight+1) {
			direction = rotationSpeed;
		}
		if (degrees >= maxLeft-1 && degrees <= maxLeft+1) {
			direction = -rotationSpeed;
		}
		transform.FindChild("Body").Rotate(0, 0, direction);

		GameObject target = GameObject.FindGameObjectWithTag ("Player");
		Vector3 targetDir = target.transform.position - transform.FindChild("Body").position;
		Vector3 forward = transform.FindChild("Body").up;
		float angle = Vector3.Angle(forward, targetDir);
		float distance = Vector3.Distance(forward, targetDir);
		if (angle < angleWidth && distance < detectionDistance)
		{
			print ("CAMERA!!!");
		}
	}
}
