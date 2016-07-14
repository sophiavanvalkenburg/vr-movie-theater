using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LightController : MonoBehaviour {

	public int lightOffStartSeekPosition;
	public int lightOffEndSeekPosition;
	public int lightOnStartSeekPosition;
	bool isOn;
	MediaPlayerCtrl backgroundPlayer;
	Toggle lightSwitch;

	// Use this for initialization
	void Start () {
		isOn = true;
		backgroundPlayer = GetComponent<MediaPlayerCtrl> ();
		lightSwitch = GameObject.Find ("LightSwitch").GetComponent<Toggle> ();
		Debug.Log ("init lightSwitch state: " + lightSwitch.isOn);
	}

	// Update is called once per frame
	void Update () {
		lightSwitch.isOn = isOn;
		if (backgroundPlayer.GetCurrentState () != MediaPlayerCtrl.MEDIAPLAYER_STATE.PLAYING)
			return;

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
