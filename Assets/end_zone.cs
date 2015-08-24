using UnityEngine;
using System.Collections;

public class end_zone: MonoBehaviour {
	GameObject ball;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindGameObjectWithTag("Ball");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject == ball) {
			gui g = Camera.main.gameObject.GetComponent<gui>();
			PlayerPrefs.SetInt("last_score", g.score);
			Application.LoadLevel("testing");
		}
	}
}
