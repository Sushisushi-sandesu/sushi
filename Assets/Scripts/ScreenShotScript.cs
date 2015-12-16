﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Leap;
using Recognizer;

public class ScreenShotScript : MonoBehaviour
{
	Controller controller = new Controller();
	public AudioSource shutterAudio;
	public RawImage screenShot;
	private Texture2D screenShotTexture;
	private string lastScreenShotPath;

	private PieceRecognizer pr; 
	
	// Use this for initialization
	void Start ()
	{
		screenShotTexture = new Texture2D (100, 100);
		screenShot.texture = screenShotTexture;
		pr = new PieceRecognizer(() => {
			TakeScreenShot ();
			return true;
		});
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnGUI ()
	{
		Frame frame = controller.Frame ();
		pr.invokeIfRecognized (frame);
	}

	void LateUpdate ()
	{
		StartCoroutine (DisplayScreenShot ());
	}
	
	private void TakeScreenShot ()
	{
		string current_time = System.DateTime.Now.ToString ().Replace ("/", "_").Replace (":", "_");
		lastScreenShotPath = Application.temporaryCachePath + "/" + current_time + ".png";
		Debug.Log ("Screenshot saved: " + lastScreenShotPath);
		StartCoroutine (CaptureScreen (lastScreenShotPath));
		shutterAudio.Play ();
	}
	
	private IEnumerator CaptureScreen (string path)
	{
		// Wait till the last possible moment before screen rendering to hide the UI
		yield return null;
		
		// Wait for screen rendering to complete
		yield return new WaitForEndOfFrame ();
		
		// Take screenshot
		Application.CaptureScreenshot (path);
	}

	private IEnumerator DisplayScreenShot ()
	{
		yield return null;
		
		if (System.IO.File.Exists (lastScreenShotPath)) {
			Debug.Log ("Last screenshot found!");
			var screenShotBytes = System.IO.File.ReadAllBytes (lastScreenShotPath);
			screenShotTexture.LoadImage (screenShotBytes);
			screenShot.material.mainTexture = screenShotTexture;
		}
	}
}
