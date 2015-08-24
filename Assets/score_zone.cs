using UnityEngine;
using System.Collections;

public class score_zone : MonoBehaviour {
	GameObject ball;
	public int score;
	public string name;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindGameObjectWithTag("Ball");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject == ball) {
			Camera.main.GetComponent<gui>().increaseScore(name, score);
		}
	}
}
