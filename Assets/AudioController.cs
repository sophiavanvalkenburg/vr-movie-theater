using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public AudioSource audioSource;

	private float designatedVolume;

	// Use this for initialization
	void Start () {
		designatedVolume = audioSource.volume; // pass by value?
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
		audioSource.Stop ();
	}

	public IEnumerator FadeIn (float fadeTime) {
		audioSource.Play ();
		float startVolume = audioSource.volume;
		while (audioSource.volume < designatedVolume) {
			audioSource.volume += startVolume * Time.deltaTime / fadeTime;
			yield return null;
		}
	}
}
