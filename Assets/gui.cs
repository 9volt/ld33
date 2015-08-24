using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gui : MonoBehaviour {
	private int score;
	private string extra;
	private string cur_ex;
	private int cur_count;
	public int delay = 3;


	// Use this for initialization
	void Start () {
		score = 0;
		cur_count =1;
		cur_ex = "";
		extra = null;
		StartCoroutine(timer(delay));
	}
	
	// Update is called once per frame
	void FixedUpdate() {

	}

	void OnGUI(){
		if (extra != null){
			GUI.Label(new Rect((Screen.width / 9) - 100, (Screen.height /9) + 0, 200, Screen.height), extra);
		}
		GUI.Label(new Rect((Screen.width / 9) - 100, (Screen.height /9) - 40, 200, 60), "Score: " + score);

	}

	public void increaseScore(string name, int value){
		score += value;
		this.setExtra(name);
	}

	public void reset(){
		cur_count =1;
		score = 0;
		extra = null;
	}

	public void setExtra(string value){
		if (value.Equals(cur_ex)){
			cur_count++;
		}else{
			//Debug.Log("is falase" + cur_ex);
			cur_count =1;
		}
		cur_ex = value;

		if (cur_count > 1){
			value += " x" +cur_count;
		}
		extra = value + '\n' + extra;

	}

	IEnumerator timer(float y){
		//Debug.Log("Erasing");
		yield return new WaitForSeconds(y);
		if(extra != null){
			int x = extra.LastIndexOf('\n');
			if (x > 0){
				extra = extra.Substring(0,x);
			}else{
				extra = null;
			}
		}
		StartCoroutine(timer(delay));
	}
}
