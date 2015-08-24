using UnityEngine;
using System.Collections;

public class CarSpawner : MonoBehaviour {
	public GameObject car;
	public int car_count;
	GameObject[] targets;

	// Use this for initialization
	void Start () {
		targets = GameObject.FindGameObjectsWithTag("Target");
	}

	Transform RandomPos(){
		return targets[Random.Range (0, targets.Length - 1)].transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectsWithTag ("Car").Length < car_count) {
			Transform pos = RandomPos();
			Instantiate(car, pos.position, pos.rotation);
		}
	}
}
