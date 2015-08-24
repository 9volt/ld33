using UnityEngine;
using System.Collections;

public class driver : MonoBehaviour {
	public GameObject[] targets;
	GameObject cur_target;
	NavMeshAgent navmesh;
	public Color[] colors;
	GameObject ball;
	public GameObject explosion;
	public int points = 50;

	// Use this for initialization
	void Start () {
		Renderer rend = GetComponent<Renderer> ();
		rend.material.color = colors[Random.Range(0, colors.Length - 1)];
		targets = GameObject.FindGameObjectsWithTag("Target");
		ball = GameObject.FindGameObjectWithTag("Ball");
		navmesh = GetComponent<NavMeshAgent>();
		ChangeTarget();
	}

	void ChangeTarget(){
		cur_target = targets[Mathf.FloorToInt(Random.Range (0, targets.Length))];
		navmesh.destination = cur_target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (cur_target.transform.position, this.transform.position) < 1.0f) {
			ChangeTarget ();
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject == ball) {
			Camera.main.GetComponent<gui>().increaseScore("Car", points);
			Instantiate(explosion, transform.position, transform.rotation);
			DestroyObject(this.gameObject);
		}
	}
}
