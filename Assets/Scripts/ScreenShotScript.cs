using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

public class ScreenShotScript : MonoBehaviour {

	public AudioSource audio;
	public RawImage screenShot;
	
	private Texture2D screenShotTexture;
	private string lastScreenShotPath;
	
	// Use this for initialization
	void Start () {
		screenShotTexture = new Texture2D(100, 100);
		screenShot.texture = screenShotTexture;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		Event e = Event.current;
		
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.F) {
			string current_time = System.DateTime.Now.ToString().Replace("/", "_").Replace(":", "_");
			lastScreenShotPath = FileUtil.GetUniqueTempPathInProject() + current_time + ".png";
			Debug.Log("saved: " + lastScreenShotPath);
			TakeScreenShot (lastScreenShotPath);
			
			audio.Play ();
			Debug.Log ("Screenshot");
			
		}
	}

	void LateUpdate () {
		StartCoroutine (DisplayScreenShot ());
	}
	
	private void TakeScreenShot (string path) {
		StartCoroutine (CaptureScreen (path));
	}
	
	private IEnumerator CaptureScreen (string path) {
		// Wait till the last possible moment before screen rendering to hide the UI
		yield return null;
		
		// Wait for screen rendering to complete
		yield return new WaitForEndOfFrame();
		
		// Take screenshot
		Application.CaptureScreenshot(path);
	}

	private IEnumerator DisplayScreenShot () {
		yield return null;
		
		if (System.IO.File.Exists(lastScreenShotPath)) {
			Debug.Log("File found!");
			var screenShotBytes = System.IO.File.ReadAllBytes(lastScreenShotPath);
			screenShotTexture.LoadImage(screenShotBytes);
			screenShot.material.mainTexture = screenShotTexture;
		}
	}
}