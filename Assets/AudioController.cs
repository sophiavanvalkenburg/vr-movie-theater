using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(StartVideo ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private IEnumerator StartVideo()
	{
		yield return new WaitForSeconds (25);
		AudioSource audioSource = GetComponent<AudioSource> ();
		float startVolume = audioSource.volume;
		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.deltaTime / 0.5f;
			yield return null;
		}
		audioSource.Stop ();
		audioSource.volume = startVolume;
//		Play ();
	}
}
