using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LightController : MonoBehaviour {

	public int lightOffStartSeekPosition;
	public int lightOffEndSeekPosition;
	public int lightOnStartSeekPosition;
	public bool isOn = false;

	private bool isRendered = false;

	MediaPlayerCtrl backgroundPlayer;
	LightSwitch lightSwitch;

	// Use this for initialization
	void Start () {
		isOn = true;
		backgroundPlayer = GetComponent<MediaPlayerCtrl> ();
		lightSwitch = GameObject.Find ("Lever").GetComponent<LightSwitch> ();
		Debug.Log ("init lightSwitch state: " + lightSwitch.isOn);
		GameObject.Find ("Theater").transform.localScale = new Vector3(0, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		lightSwitch.isOn = isOn;
		if (backgroundPlayer.GetCurrentState () != MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING)
			return;

		if (!isRendered && backgroundPlayer.GetSeekPosition () > 1000) {
			GameObject.Find ("Theater").transform.localScale = new Vector3(30, 30, 30);
			isRendered = true;
			backgroundPlayer.Pause ();
			backgroundPlayer.SeekTo (0);
			GameObject.Find ("Music").GetComponent<AudioSource> ().Play ();
		}

		if (isOn) {
			int currentPosition = backgroundPlayer.GetSeekPosition ();
			Debug.Log ("lightSwitch position: " + currentPosition);
				if (currentPosition + 100 >= backgroundPlayer.GetDuration ()) backgroundPlayer.Pause ();
		} else {
			int currentPosition = backgroundPlayer.GetSeekPosition ();
			Debug.Log ("lightSwitch position: " + currentPosition);
			if (currentPosition >= lightOffEndSeekPosition) 
				backgroundPlayer.Pause ();
		}
	}

	public void LightOff() {
		if (!isOn)
			return;

		Debug.Log ("LightController: Light off!");
		isOn = false;
		Debug.Log ("video state: " + backgroundPlayer.GetCurrentState ());
		Debug.Log ("lightSwitch position: " + backgroundPlayer.GetSeekPosition ());
		if (backgroundPlayer.GetSeekPosition () > 0) {
//			backgroundPlayer.Load (backgroundPlayer.m_strFileName);
			backgroundPlayer.Stop();
		} else {
			backgroundPlayer.SeekTo (lightOffStartSeekPosition);
		}
		Debug.Log ("lightSwitch position: " + backgroundPlayer.GetSeekPosition ());
		backgroundPlayer.Play ();
	}

	public void LightOn() {
		if (isOn)
			return;

		Debug.Log ("LightController: Light on!");
		isOn = true;
		backgroundPlayer.SeekTo (lightOnStartSeekPosition);
		backgroundPlayer.Play ();
	}
}
