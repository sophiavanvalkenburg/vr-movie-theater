using UnityEngine;
using System.Collections;

public class ScreenController : MonoBehaviour {

	private MediaPlayerCtrl videoPlayer;
	private AudioController audioPlayer;
	public bool isPlaying = false;
	public bool isPaused = false;

	// Use this for initialization
	void Start () {
		videoPlayer = GetComponent<MediaPlayerCtrl> ();
		audioPlayer = GameObject.Find ("Music").GetComponent<AudioController> ();
		transform.localScale = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Load(string filename) {
		videoPlayer.Load (filename);
	}

	public void Prepare(int seconds=6) {
		if (!isPlaying) {
			StartCoroutine(audioPlayer.FadeOut (2.5f));
			StartCoroutine (WaitAndPlay (seconds));
		}
	}

	public void Pause(){
		if (isPlaying) {
			videoPlayer.Pause ();
			isPlaying = false;
			isPaused = true;
		}
	}

	public void TogglePause()
	{
		if (isPlaying) {
			Pause ();
		} else {
			Play ();
		}
	}

	public void Stop(bool startMusic=true) {
		if (isPlaying || isPaused) {
			videoPlayer.Stop ();
			if (startMusic) {
				transform.localScale = new Vector3(0, 0, 0);
				StartCoroutine (CueBackgroundMusic ());
			}
			isPlaying = false;
			isPaused = false;
		}
	}

	public void Play () {
		if (!isPlaying) {
			videoPlayer.Play ();	
			isPlaying = true;
			isPaused = false;
		}
	}

	public IEnumerator WaitAndPlay(int seconds=6) {
		yield return new WaitForSeconds (seconds);
		transform.localScale = new Vector3(18, 11.28f, 20);
		videoPlayer.Play ();	
		isPlaying = true;
		isPaused = false;
	}

	private IEnumerator CueBackgroundMusic() {
		yield return new WaitForSeconds (3);
		StartCoroutine(audioPlayer.FadeIn (4.0f));
	}
}
