using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class reset : MonoBehaviour {
	Rigidbody rb;
	Vector3 start;
	Quaternion start_rot;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
		rb.useGravity = false;
		start = transform.position;
		start_rot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButton ("Jump")) {
			//this.transform.position = new Vector3 (0f, 3f, 0f);
			rb.useGravity = false;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
			transform.position = start;
			transform.rotation = start_rot;
			StartCoroutine(shoot(rb));
		}
	}

	IEnumerator shoot(Rigidbody rb){
		yield return new WaitForSeconds(.5f);
		rb.AddRelativeTorque(Vector3.forward * 100f);
		yield return new WaitForSeconds(.5f);
		rb.AddForce(Vector3.forward * 200f);
		rb.AddForce(Vector3.right * 20f);
		rb.useGravity = true;
	}
}