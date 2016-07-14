// Copyright 2016 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissioßns and
// limitations under the License.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControllerDemoManager : MonoBehaviour
{
	public GameObject controllerPivot;
	public GameObject messageCanvas;
	public Text messageText;

	public MediaPlayerCtrl mediaPlayer;

	private Renderer controllerCursorRenderer;

	// Currently selected GameObject.
	private GameObject selectedObject;


	void Awake ()
	{
	}

	void Update ()
	{
		UpdatePointer ();
		UpdateStatusMessage ();
	}

	private void UpdatePointer ()
	{
		if (GvrController.State != GvrConnectionState.Connected) {
			controllerPivot.SetActive (false);
		}
		controllerPivot.SetActive (true);
		controllerPivot.transform.rotation = GvrController.Orientation;
   
		// raycast and get a selected object if it's hit
		selectObjectIfHit();

		// some debug messages
		if (GvrController.TouchDown) {
			Debug.Log ("Touching Down");
			if (selectedObject != null) {
				Debug.Log ("selectedobj is not null");
			} else {
				Debug.Log ("selectedobj is null");
			}
		}

		// click a thumbnail or the light switch
		if (selectedObject != null) {
			switch (selectedObject.tag) {
			case "Thumbnail": 
				selectThumbnailAction ();
				break;
			case "Light":
				selectLightSwitchAction ();
				break;
			}
		
		}
    
	}

	private void selectLightSwitchAction()
	{
		Debug.Log ("Clicking a light");
		Slider lightSwitch = GameObject.Find ("LightSwitch").GetComponent<Slider> ();

		if (GvrController.TouchDown) {
			if (lightSwitch.value > 0) {
				LightOff ();
				lightSwitch.value = 0;
			} else {
				LightOn ();
				lightSwitch.value = 1f;
			}
		} else {
			//hovering
			lightSwitch.Select ();
		}
	}

	private void selectThumbnailAction ()
	{
		if (GvrController.TouchDown) {
			Debug.Log ("Clicking a thumbnail");
			Thumbnail thumbnailObject = selectedObject.GetComponent<Thumbnail> ();
			selectedObject.GetComponent<MeshRenderer> ().material.color = Color.black;
			if (thumbnailObject != null && thumbnailObject.movieFileName != null) {
				Debug.Log ("thumbnail has movie " + thumbnailObject.movieFileName);
				mediaPlayer.Stop ();
				mediaPlayer.Load (thumbnailObject.movieFileName);
				mediaPlayer.Play ();
			}
		}
	}

	private void selectObjectIfHit()
	{
		RaycastHit hitInfo;
		Vector3 rayDirection = GvrController.Orientation * Vector3.forward;
		if (Physics.Raycast (Vector3.zero, rayDirection, out hitInfo)) {
			Debug.Log ("Raycast is true");
			Debug.Log (hitInfo.collider.ToString ());
			if (hitInfo.collider && hitInfo.collider.gameObject && hitInfo.collider.gameObject.tag == "Thumbnail") {
				Debug.Log ("Hitting something");
				SetSelectedObject (hitInfo.collider.gameObject);
			}
		} else {
			SetSelectedObject (null); 
		}
	}

	private void SetSelectedObject (GameObject obj)
	{
		if (null != selectedObject) {
			selectedObject.GetComponent<MeshRenderer> ().material.color = Color.white;
		}
		if (null != obj) {
			obj.GetComponent<MeshRenderer> ().material.color = Color.grey;
		}
		selectedObject = obj;
	}

	/*
  private void StartDragging() {
    dragging = true;
    selectedObject.GetComponent<Renderer>().material = cubeActiveMaterial;

    // Reparent the active cube so it's part of the ControllerPivot object. That will
    // make it move with the controller.
    selectedObject.transform.SetParent(controllerPivot.transform, true);
  }

  private void EndDragging() {
    dragging = false;
    selectedObject.GetComponent<Renderer>().material = cubeHoverMaterial;

    // Stop dragging the cube along.
    selectedObject.transform.SetParent(null, true);
  }
  */

	private void UpdateStatusMessage ()
	{
		// This is an example of how to process the controller's state to display a status message.
		switch (GvrController.State) {
		case GvrConnectionState.Connected:
        //messageCanvas.SetActive(false);
			break;
		case GvrConnectionState.Disconnected:
			messageText.text = "Controller disconnected.";
			messageText.color = Color.white;
			messageCanvas.SetActive (true);
			break;
		case GvrConnectionState.Scanning:
			messageText.text = "Controller scanning...";
			messageText.color = Color.cyan;
			messageCanvas.SetActive (true);
			break;
		case GvrConnectionState.Connecting:
			messageText.text = "Controller connecting...";
			messageText.color = Color.yellow;
			messageCanvas.SetActive (true);
			break;
		case GvrConnectionState.Error:
			messageText.text = "ERROR: " + GvrController.ErrorDetails;
			messageText.color = Color.red;
			messageCanvas.SetActive (true);
			break;
		default:
        // Shouldn't happen.
			Debug.LogError ("Invalid controller state: " + GvrController.State);
			break;
		}
	}

	public void LightOff ()
	{
		float exposure = 0.3f;
		RenderSettings.skybox.SetFloat ("_Exposure", exposure);
	}

	public void LightOn ()
	{
		float exposure = 1f;
		RenderSettings.skybox.SetFloat ("_Exposure", exposure);
	}

	public void SetLight (float on)
	{
		Debug.Log ("Light toggled!" + on);
		if (on > 0) {
			LightOn ();
		} else {
			LightOff (); 
		}
	}

	public void ToggleLight (bool on)
	{
		Debug.Log ("Light toggled!" + on);
		if (on) {
			LightOn ();
		} else {
			LightOff (); 
		}
	}
}
