using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public AudioSource audioSource;

	private float designatedVolume = .38f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator FadeOut (float fadeTime) {
		float startVolume = audioSource.volume;
		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
			yield return null;
		}
		audioSource.Pause ();
	}

	public IEnumerator FadeIn (float fadeTime) {
		audioSource.Play ();
		while (audioSource.volume < designatedVolume) {
			audioSource.volume += designatedVolume * Time.deltaTime / fadeTime;
			yield return null;
		}
	}
}
