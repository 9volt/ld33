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
	bool rampage;
	float last_rampage = 0f;
	float rampage_delay = 15f;
	string current_ramage;
	string rampage_target;
	int rampage_goal;
	int rampage_total;
	int rampage_score;
	float rampage_countdown;

	// Use this for initialization
	void Start () {
		last_rampage = Time.realtimeSinceStartup;
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
	
	void Update() {
		if (rampage) {
			rampage_countdown -= Time.deltaTime;
			if(rampage_countdown < 0f){
				rampage = false;
				last_rampage = Time.realtimeSinceStartup;
			}
		} else {
			if(Time.realtimeSinceStartup - rampage_delay > last_rampage){
				rampage = true;
				int rand = Random.Range(0, 4);
				if(rand == 0){
					current_ramage = "Road Rage";
					rampage_target = "Car";
					rampage_goal = 5;
					rampage_total = 0;
					rampage_countdown = 30f;
					rampage_score = 5000;
				} else if(rand == 1){
					current_ramage = "Housing Market Crash";
					rampage_target = "House";
					rampage_goal = 5;
					rampage_total = 0;
					rampage_countdown = 30f;
					rampage_score = 5000;
				} else if(rand == 2){
					current_ramage = "Project Mayham";
					rampage_target = "Office";
					rampage_goal = 5;
					rampage_total = 0;
					rampage_countdown = 30f;
					rampage_score = 5000;
				} else if(rand == 3){
					current_ramage = "Road Trip";
					rampage_target = "Up the Road";
					rampage_goal = 1;
					rampage_total = 0;
					rampage_countdown = 30f;
					rampage_score = 5000;
				}
			}
		}
	}

	void Rampage(string t){
		if(rampage && t == rampage_target) {
			rampage_total++;
			if(rampage_total == rampage_goal){
				rampage = false;
				last_rampage = Time.realtimeSinceStartup;
				increaseScore(current_ramage, rampage_score);
				source.PlayOneShot(special);
			}
		}
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
		if (rampage){
			GUI.Label(new Rect((Screen.width / 2) - 100, 0, 200, Screen.height), current_ramage + " (" + rampage_score + "pts)", gs);
			GUI.Label(new Rect((Screen.width / 2) - 100, 20, 200, Screen.height), rampage_total + " / " + rampage_goal + " : " + rampage_countdown.ToString("0.##") + "s", gs);
		}
		GUI.Label(new Rect((Screen.width / 9) - 100, (Screen.height /9) - 60, 200, 60), "High Score: " + highscore, gs);
		GUI.Label(new Rect((Screen.width / 9) - 100, (Screen.height /9) - 40, 200, 60), "Score: " + score, gs);
		if (tut & (System.DateTime.Now.Second % 2 != 0)){
			GUI.Label(new Rect((Screen.width / 2 -200), (Screen.height - 55),50, 38), a1);
			GUI.Label(new Rect((Screen.width / 2 +170), (Screen.height - 55),50, 38), a2);
		}
	}

	public void increaseScore(string name, int value){
		Rampage(name);
		if(name == "Through the Tower") {
			source.PlayOneShot (special);
		} else if(name == "Out of the Frying Pan") {
			source.PlayOneShot(special);
		} else if(name == "Up the Road") {
			source.PlayOneShot(special);
		}
		if (name.Equals(cur_ex)){
			cur_count++;
		}else{
			//Debug.Log("is falase" + cur_ex);
			cur_count =1;
		}
		cur_ex = name;
		
		if (cur_count > 1){
			name += " x" +cur_count;
		}
		name = name + " (" + (value * cur_count) + ")";
		extra = name + '\n' + extra;

		score += value * cur_count;

		if (tut) {
			StartCoroutine(tutorial());
		}
	}

	public void reset(){
		tut = true;
		cur_count =1;
		score = 0;
		extra = null;
	}
	

	IEnumerator tutorial(){
		yield return new WaitForSeconds (5.0f);
		tut = false;
	}

	IEnumerator timer(float y){

		//Debug.Log("Erasing");
		yield return new WaitForSeconds(y);
		if(extra != null){
			if(extra.Length >45){
				extra = extra.Substring(0, 45);
			}
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
