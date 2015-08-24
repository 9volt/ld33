﻿using UnityEngine;
using System.Collections;

public class building_exploder : MonoBehaviour {
	public GameObject explosion;
	public GameObject fire;
	public int health;
	public int points;
	public GameObject ball;
	public BoxCollider col;
	public Renderer rend;
	public Texture burned;
	public string name;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static Vector3 GetRandomPointInBoxCollider(BoxCollider box) { 
		Vector3 bLocalScale = box.transform.localScale; 
		Vector3 boxPosition = box.transform.position; 
		boxPosition += new Vector3 (bLocalScale.x * box.center.x, bLocalScale.y * box.center.y, bLocalScale.z * box.center.z); 
		
		Vector3 dimensions = new Vector3(bLocalScale.x * box.size.x, 
		                                 bLocalScale.y * box.size.y, 
		                                 bLocalScale.z * box.size.z); 
		
		Vector3 newPos = new Vector3 (UnityEngine.Random.Range (boxPosition.x - (dimensions.x / 2), boxPosition.x + (dimensions.x / 2)), 
		                              UnityEngine.Random.Range (boxPosition.y - (dimensions.y / 2), boxPosition.y + (dimensions.y / 2)), 
		                              UnityEngine.Random.Range (boxPosition.z - (dimensions.z / 2), boxPosition.z + (dimensions.z / 2))); 
		return newPos; 
	}

	void OnCollisionEnter(Collision collision){
		if (collision.collider.gameObject == ball) {
			Debug.Log ("BOOM");
			Camera.main.GetComponent<gui>().increaseScore(name, points);
			GameObject go = (GameObject)Instantiate(explosion, GetRandomPointInBoxCollider(col), collision.transform.rotation);
			health--;
			if(health == 0){
				Instantiate(fire, GetRandomPointInBoxCollider(col), collision.transform.rotation);
				rend.material.mainTexture = burned;
				rend.material.SetTexture("_EmissionMap", null);
				rend.material.SetColor("_EmissionColor", Color.black);
			}
		}
	}
}
