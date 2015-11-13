using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CamScript : MonoBehaviour {

	public RawImage image;
	public AudioSource audio;
	public RawImage screenShot;

	private Texture2D screenShotTexture;
	private string lastScreenShotPath;

	// Use this for initialization
	void Start () {

		WebCamDevice[] devices = WebCamTexture.devices;

		string backCamName = "";
		for (int i = 0; i < devices.Length; i++) {
			Debug.Log ("Device " + i +  ":" + devices [i].name + "IS FRONT FACING:" + devices [i].isFrontFacing);
			if (devices[i].name.StartsWith("BUFFALO")) {
				backCamName = devices[i].name;
				Debug.Log("set camera to: " + backCamName);
			}
		}

		WebCamTexture webCamTexture = new WebCamTexture (backCamName, 1280, 720, 30);


		image.texture = webCamTexture;
		image.material.mainTexture = webCamTexture;
		webCamTexture.Play ();

		screenShotTexture = new Texture2D(400, 300);
		screenShot.texture = screenShotTexture;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void TakeScreenShot (string path) {
		StartCoroutine (CaptureScreen (path));
	}

	IEnumerator CaptureScreen (string path) {
		// Wait till the last possible moment before screen rendering to hide the UI
		yield return null;
		GameObject.Find("UICanvas").GetComponent<Canvas>().enabled = false;
		
		// Wait for screen rendering to complete
		yield return new WaitForEndOfFrame();
		
		// Take screenshot
		Application.CaptureScreenshot(path);

		// Show UI after we're done
		GameObject.Find("UICanvas").GetComponent<Canvas>().enabled = true;
	}

	void OnGUI () {
		Event e = Event.current;

		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.F) {
			string current_time = System.DateTime.Now.ToString().Replace("/", "_").Replace(":", "_");
			lastScreenShotPath = "/Users/nate/" + current_time + ".png";
			Debug.Log("saved: " + lastScreenShotPath);
			TakeScreenShot (lastScreenShotPath);
		
			audio.Play ();
			Debug.Log ("Screenshot");

		}
	}

	void LateUpdate () {
		StartCoroutine (DisplayScreenShot ());
	}

	IEnumerator DisplayScreenShot () {
		yield return null;

		if (System.IO.File.Exists(lastScreenShotPath)) {
			Debug.Log("File found!");
			var screenShotBytes = System.IO.File.ReadAllBytes(lastScreenShotPath);
			screenShotTexture.LoadImage(screenShotBytes);
			screenShot.material.mainTexture = screenShotTexture;
		}
	}
}
