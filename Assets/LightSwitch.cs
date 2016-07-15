using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {
	public bool isOn;
	GameObject handler;
	GameObject lever;
	Behaviour halo;

	// Use this for initialization
	void Start () {
		handler = GameObject.Find ("Lever/Handler");
		lever = GameObject.Find ("Lever");
		halo = (Behaviour)GameObject.Find("Lever/Handler/StickBall/Ball").GetComponent("Halo");
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

	public void Highlight() {
		halo.enabled = true;
	}

	public void RemoveHighlight() {
		halo.enabled = false;
	}

}
