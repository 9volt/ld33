using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class reset : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButton ("Jump")) {
			this.transform.position = new Vector3 (0f, 3f, 0f);
		}
	}
}
