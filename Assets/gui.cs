using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gui : MonoBehaviour {
	public int score;
	private string extra;
	private string cur_ex;
	private int cur_count;
	public int delay = 3;
	public AudioClip special;
	AudioSource source;
	public bool tut = true;
	public Texture a1;
	public Texture a2;
	public Font font;
	GUIStyle gs;

	// Use this for initialization
	void Start () {
		source = gameObject.GetComponent<AudioSource>();
		gs = new GUIStyle();
		gs.font = font;
		gs.normal.textColor = Color.yellow;
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
		int highscore = 0;
		if (PlayerPrefs.HasKey ("high_score")) {
			if(PlayerPrefs.GetInt("high_score") < score){
				PlayerPrefs.SetInt("high_score", score);
			}
		} else {
			PlayerPrefs.SetInt ("high_score", score);
		}
		highscore = PlayerPrefs.GetInt("high_score");
		if (extra != null){
			GUI.Label(new Rect((Screen.width / 9) - 100, (Screen.height /9) + 0, 200, Screen.height), extra, gs);
		}
		GUI.Label(new Rect((Screen.width / 9) - 100, (Screen.height /9) - 60, 200, 60), "High Score: " + highscore, gs);
		GUI.Label(new Rect((Screen.width / 9) - 100, (Screen.height /9) - 40, 200, 60), "Score: " + score, gs);
		if (tut & (System.DateTime.Now.Second % 2 != 0)){
			GUI.Label(new Rect((Screen.width / 2 -200), (Screen.height - 55),50, 38), a1);
			GUI.Label(new Rect((Screen.width / 2 +170), (Screen.height - 55),50, 38), a2);
		}
	}

	public void increaseScore(string name, int value){
		score += value;
		this.setExtra(name);
		if(name == "Through the Tower") {
			source.PlayOneShot (special);
		} else if(name == "Out of the Frying Pan") {
			source.PlayOneShot(special);
		} else if(name == "Up the Road") {
			source.PlayOneShot(special);
		}
	}

	public void reset(){
		tut = true;
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
		if (tut) {
			StartCoroutine(tutorial());
		}
	}

	IEnumerator tutorial(){
		yield return new WaitForSeconds (5.0f);
		tut = false;
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
