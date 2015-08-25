using UnityEngine;
using System.Collections;

public class ball_noise : MonoBehaviour {
	public AudioSource source;
	public AudioSource roller;
	Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		roller.pitch = rb.velocity.magnitude * .1f;
		roller.volume = rb.velocity.magnitude * .5f;

	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Wall") {
			source.Play ();
		}
		if (collision.gameObject.tag == "Floor") {
			roller.Play();
		}
	}

	void OnCollisionExit(Collision collision){
		if (collision.gameObject.tag == "Floor") {
			roller.Stop();
		}
	}
}
