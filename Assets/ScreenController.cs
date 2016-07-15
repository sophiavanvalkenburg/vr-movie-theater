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
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Load(string filename) {
		videoPlayer.Load (filename);
	}

	public void Prepare() {
		if (!isPlaying) {
			StartCoroutine(audioPlayer.FadeOut (2.5f));
			StartCoroutine (WaitAndPlay ());
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

	public void Stop() {
		if (isPlaying || isPaused) {
			videoPlayer.Stop ();
			StartCoroutine(CueBackgroundMusic ());
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

	public IEnumerator WaitAndPlay() {
		yield return new WaitForSeconds (6);
		videoPlayer.Play ();	
		isPlaying = true;
		isPaused = false;
	}

	private IEnumerator CueBackgroundMusic() {
		yield return new WaitForSeconds (3);
		StartCoroutine(audioPlayer.FadeIn (4.0f));
	}
}
