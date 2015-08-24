using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class paddle : MonoBehaviour {
	public HingeJoint hj;
	public string key;
	bool up = false;
	AudioSource clip;
	// Use this for initialization
	void Start () {
		clip = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		JointSpring js = hj.spring;
		if(CrossPlatformInputManager.GetButton(key)) {
			if(!up){
				clip.Play();
			}
			up = true;
			js.targetPosition = 45f;
		} else {
			up = false;
			js.targetPosition = -10f;
		}
		hj.spring = js;
	}
}
