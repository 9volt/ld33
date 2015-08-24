using UnityEngine;
using System.Collections;

public class volcano_score : MonoBehaviour {
	GameObject ball;
	public int score;
	public string name;
	public GameObject explosion;
	public GameObject fire;

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
			Instantiate(explosion, other.transform.position, other.transform.rotation);
			Instantiate(fire, other.transform.position, other.transform.rotation);
		}
	}
}
