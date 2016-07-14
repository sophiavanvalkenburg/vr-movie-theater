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
			audioPlayer.FadeOut (0.5f);
			lightController.LightOff ();
			StartCoroutine (Play ());
		}
	}

	public void Stop() {
		if (isPlaying) {
			videoPlayer.Stop ();
			lightController.LightOn ();
			StartCoroutine (CueBackgroundMusic ());
		}
	}

	private IEnumerator Play() {
		yield return new WaitForSeconds (6);
		videoPlayer.Play ();	
		isPlaying = true;
	}

	private IEnumerator CueBackgroundMusic() {
		yield return new WaitForSeconds (6);
		audioPlayer.FadeIn (0.5f);
	}
}
