using UnityEngine;
using System.Collections;

public class ScreenController : MonoBehaviour {

	private MediaPlayerCtrl videoPlayer;
	private AudioController audioPlayer;
	private LightController lightController;
	private bool isPlaying = false;
	private bool isPaused = false;

	// Use this for initialization
	void Start () {
		videoPlayer = GetComponent<MediaPlayerCtrl> ();
		audioPlayer = GameObject.Find ("Music").GetComponent<AudioController> ();
		lightController = GameObject.Find ("BackgroundVideoManager").GetComponent<LightController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Load(string filename) {
		videoPlayer.Load (filename);
	}

	public void Prepare() {
		if (!isPlaying) {
			audioPlayer.FadeOut (0.5f);
			lightController.LightOff ();
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
			lightController.LightOn ();
			StartCoroutine (CueBackgroundMusic ());
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
		yield return new WaitForSeconds (6);
		audioPlayer.FadeIn (0.5f);
	}
}
