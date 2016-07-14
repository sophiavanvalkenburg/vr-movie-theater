using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {
	public bool isOn;
	GameObject handler;
	GameObject lever;

	// Use this for initialization
	void Start () {
		handler = GameObject.Find ("Lever/Handler");
		lever = GameObject.Find ("Lever");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 levelRotation = lever.transform.rotation.eulerAngles;

		if (isOn) {
			handler.transform.rotation = Quaternion.Euler(levelRotation.x - 50f, 0, 0);
		} else {
			handler.transform.rotation = Quaternion.Euler(levelRotation.x - 110f, 0 , 0);
		}
	
	}
}
