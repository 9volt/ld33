﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class reset : MonoBehaviour {
	Rigidbody rb;
	Vector3 start;
	Quaternion start_rot;
	bool spinning;
	float f;
	bool launched;
	GUIStyle gs;
	public Texture2D tex;
	public Texture2D bactex;
	public Texture2D full_tex;
	public Font font;
	public AudioSource source;
	public AudioClip launch;
	public GameObject backstop;
	float launch_time;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
		spinning = false;
		rb.useGravity = false;
		start = transform.position;
		start_rot = transform.rotation;
		f = 0.0f;
		launched = false;
		gs = new GUIStyle();
		gs.font = font;
		gs.normal.textColor = Color.yellow;
		backstop.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!launched) {
			if (CrossPlatformInputManager.GetButton ("Jump")) {
				if (spinning) {
					if (f >= 2000f) {
						gs.normal.textColor = Color.red;
					} else {
						f += 25f;
					}
					rb.AddTorque (rb.angularVelocity + Vector3.right * 10f);
					source.pitch = f / 666f;
				} else {
					source.Play ();
					spinning = true;
					rb.useGravity = false;
					rb.velocity = Vector3.zero;
					rb.angularVelocity = Vector3.zero;
					transform.position = start;
					transform.rotation = start_rot;
				}
			} else if (spinning) {
				Launch ();
			}
		} else {
			if(Time.timeSinceLevelLoad - launch_time > 2f){
				backstop.gameObject.SetActive(true);
			}
		}
	}

	void Launch(){
		source.Stop();
		source.pitch = 1f;
		source.PlayOneShot(launch);
		rb.AddForce(Vector3.forward * f);
		rb.AddForce(Vector3.right * 20f);
		rb.useGravity = true;
		spinning = false;
		f = 0.0f;
		launched = true;
		launch_time = Time.timeSinceLevelLoad;
	}
	
	void OnGUI(){
		GUI.skin.box.normal.background = bactex;

		if (spinning) {
			GUI.Label (new Rect ((Screen.width / 2) + 230, (Screen.height / 2) + 52, 200, Screen.height), (f / 20).ToString () + "%", gs);
			GUI.Box (new Rect ((Screen.width / 2) -200, (Screen.height / 2) + 50, 400, 20),"");
			if (f > 0f && f < 2000f){
				GUI.skin.box.normal.background = tex;
			} else if (f == 0f) {
				Debug.Log("grey");
				GUI.skin.box.normal.background = bactex;
			} else {
				GUI.skin.box.normal.background = full_tex;
			}
			GUI.Box (new Rect ((Screen.width / 2)-200, (Screen.height / 2) + 50, f/5, 20),"");
		} else if (!launched) {
			GUI.Box (new Rect ((Screen.width / 2) -200, (Screen.height / 2) + 50, 400, 20),"");
			GUI.Label (new Rect ((Screen.width / 2) - 100,  (Screen.height / 2) + 50, 200, Screen.height), "Hold [SPACE] to charge", gs);
		}
	}
}