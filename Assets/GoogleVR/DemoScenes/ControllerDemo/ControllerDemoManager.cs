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

	public ScreenController screenController;
	public LightController lightController;

	private Renderer controllerCursorRenderer;

	// Currently selected GameObject.
	private GameObject selectedObject;


	void Awake ()
	{
	}

	void Start () {
		lightController = GameObject.Find ("BackgroundVideoManager").GetComponent<LightController> ();
		screenController = GameObject.Find ("ScreenVideoManager").GetComponent<ScreenController> ();
	}

	void Update ()
	{
		UpdatePointer ();
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
			case "Screen":
				SelectScreenAction ();
				break;
			}
		
		}
    
	}

	private void SelectScreenAction()
	{
		if (GvrController.TouchDown) {
			Debug.Log ("Clicking the screen");
			TogglePauseVideo ();
			PlayVideo ();
		}
	}

	private void selectLightSwitchAction()
	{
		Toggle lightSwitch = GameObject.Find ("LightSwitch").GetComponent<Toggle> ();

		if (GvrController.TouchDown) {
			if (lightSwitch.isOn) {
				PlayVideo ();
			} else {
				StopVideo ();
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
			SetObjTouchDownAppearance (selectedObject);
			if (thumbnailObject != null && thumbnailObject.movieFileName != null) {
				StopVideo ();
				LoadVideo (thumbnailObject.movieFileName);
				PlayVideo ();
			}
		} else {
			SetObjHoverAppearance (selectedObject);
		}
	}

	private void selectObjectIfHit()
	{
		RaycastHit hitInfo;
		Vector3 rayDirection = GvrController.Orientation * Vector3.forward;
		if (Physics.Raycast (Vector3.zero, rayDirection, out hitInfo)) {
			if (hitInfo.collider && hitInfo.collider.gameObject) {
				SetSelectedObject (hitInfo.collider.gameObject);
			}
		} else {
			SetSelectedObject (null); 
		}
	}

	private void SetObjTouchDownAppearance(GameObject obj){
		obj.GetComponent<MeshRenderer> ().material.color = Color.black;
	}

	private void SetObjHoverAppearance(GameObject obj){
		obj.GetComponent<MeshRenderer> ().material.color = Color.cyan;
	}

	private void SetObjInactiveAppearance(GameObject obj){
		obj.GetComponent<MeshRenderer> ().material.color = Color.white;
	}

	private void SetSelectedObject (GameObject obj)
	{
		if (null != selectedObject) {
			SetObjInactiveAppearance (selectedObject);
		}
		if (null != obj) {
			SetObjHoverAppearance (obj);
		}
		selectedObject = obj;
	}

	public void TogglePauseVideo()
	{
		screenController.TogglePause();
	}

	public void LoadVideo(string filename)
	{
		Debug.Log ("DemoManager: loading " + filename);
		screenController.Load (filename);
	}

	public void PlayVideo ()
	{
		Debug.Log ("DemoManager: Preparing video");
		lightController.LightOff ();
		screenController.Prepare ();
	}

	public void StopVideo ()
	{
		Debug.Log ("DemoManager: stopping video");
		screenController.Stop ();
		lightController.LightOn ();
	}
}
