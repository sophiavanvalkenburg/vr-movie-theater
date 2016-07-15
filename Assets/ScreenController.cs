using UnityEngine;
using System.Collections;

public class ScreenController : MonoBehaviour {

	private MediaPlayerCtrl videoPlayer;
	private AudioController audioPlayer;
	private LightController lightController;
	private bool isPlaying = false;

	// Use this for initialization
	void Start () {
		videoPlayer = GetComponent<MediaPlayerCtrl> ();
		audioPlayer = GameObject.Find ("Music").GetComponent<AudioController> ();
		lightController = GameObject.Find ("BackgroundVideoManager").GetComponent<LightController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Prepare() {
		if (!isPlaying && videoPlayer.GetCurrentState () == MediaPlayerCtrl.MEDIAPLAYER_STATE.READY) {
			StartCoroutine(audioPlayer.FadeOut (2.5f));
			lightController.LightOff ();
			StartCoroutine (WaitAndPlay ());
		}
	}

	public void Stop() {
		if (isPlaying) {
			videoPlayer.Stop ();
			lightController.LightOn ();
			CueBackgroundMusic ();
			isPlaying = false;
		}
	}

	public void Play () {
		if (!isPlaying && videoPlayer.GetCurrentState () == MediaPlayerCtrl.MEDIAPLAYER_STATE.READY) {
			videoPlayer.Play ();	
			isPlaying = true;
		}
	}

	public IEnumerator WaitAndPlay() {
		yield return new WaitForSeconds (6);
		videoPlayer.Play ();	
		isPlaying = true;
	}

	private void CueBackgroundMusic() {
		StartCoroutine(audioPlayer.FadeIn (4.0f));
	}
}
