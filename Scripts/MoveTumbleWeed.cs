using UnityEngine;
using System.Collections;

public class MoveTumbleWeed : MonoBehaviour {
	
	public float speed;
	public float rotationFactor;
	private float currentRotation;
	private float currentPosition;


	void Start ()
	{
		currentPosition = transform.position.x;
		currentRotation = 0.0f;
	}

	void Update () {
		currentPosition += speed;
		transform.position = new Vector3 (currentPosition, -4.0f, -1.5f);
		currentRotation -= rotationFactor;
		transform.rotation = Quaternion.Euler (0.0f, 0.0f, currentRotation);

		CheckOutOfBounds ();
	}

	void CheckOutOfBounds () {
		if (transform.position.x > 11.0f) {
			GameObject.Destroy (this.gameObject);
		}
	}
}
