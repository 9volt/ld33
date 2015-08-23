using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class paddle : MonoBehaviour {
	public HingeJoint hj;
	public string key;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		JointSpring js = hj.spring;
		if(CrossPlatformInputManager.GetButton(key)) {
			js.targetPosition = 40f;
		} else {
			js.targetPosition = -10f;
		}
		hj.spring = js;
	}
}
