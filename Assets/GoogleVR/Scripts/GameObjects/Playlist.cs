using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Playlist : MonoBehaviour {

	// hardcoded list of movies for now
	private string[] movieFilenames = new string[3] {
		"39851_1_sciencetake-humanoid_wg_480p.mp4",
		"EasyMovieTexture.mp4",
		"ed_1024_512kb.mp4"
	};

	// the game objects
	//public List<GameObject> thumbnailObjects = new List<GameObject> ();

	// thumbnail plane
	public GameObject thumbnailPrefab;
	//private const float THUMBNAIL_SCALE_X = 0.16f;
	//private const float THUMBNAIL_SCALE_Z = 0.09f;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < movieFilenames.Length; i++) {
			// initialize the thumbnail gameobject from the prefab
			GameObject thumbnailObj = (GameObject) Instantiate(thumbnailPrefab);
			// set the grid as the parent of the thumbnail
			thumbnailObj.transform.SetParent(gameObject.transform);
			// calculate coordinates of each thumbnail (3 columns)
			int x = i%3 - 2;
			int z = 2 * (i / 3) - 2;
			// set the coordinates relative to parent
			thumbnailObj.transform.localPosition = new Vector3(x, 5, z);
			thumbnailObj.transform.localRotation = Quaternion.identity;
			// add thumbnail data
			Thumbnail thumbnail = thumbnailObj.GetComponent<Thumbnail> ();
			thumbnail.movieFileName = movieFilenames [i];
			//thumbnailObjects.Add (thumbnailObj);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
