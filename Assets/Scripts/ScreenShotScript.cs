using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Leap;
using Recognizer;

public class ScreenShotScript : MonoBehaviour
{
	Controller controller = new Controller();
	public AudioSource shutterAudio;

	private RawImage screenShotPreviewImage;
	private Texture2D screenShotTexture;
	private string lastScreenShotPath;
	private PeaceRecognizer pr;
	private PhoneHandleRecognizer phr;
	private OkRecognizer ok;

	private HorizontalSwipeRecognizer hsr;
	private VerticalSwipeRecognizer vsr;
	private bool hasNewScreenShot;

	// Use this for initialization
	void Start ()
	{
		screenShotTexture = new Texture2D(800, 600);
		screenShotPreviewImage = GameObject.FindGameObjectWithTag("ScreenShotPreviewImage").GetComponent<RawImage> ();
		screenShotPreviewImage.enabled = false;
		hasNewScreenShot = false;

		pr = new PeaceRecognizer (TakeScreenShot);
		ok = new OkRecognizer (() => { UnityEngine.Debug.Log("recognized ok"); TakeScreenShot(); });
		//phr = new PhoneHandleRecognizer (TakeScreenShot);

		controller.EnableGesture (Gesture.GestureType.TYPE_SWIPE);
		hsr = new HorizontalSwipeRecognizer (() => {});
		vsr = new VerticalSwipeRecognizer (() => {});
	}

	// Update is called once per frame
	void Update ()
	{
	}

	void OnGUI ()
	{
		// gesture
		Frame frame = controller.Frame ();
		pr.InvokeIfRecognized (frame);
		ok.InvokeIfRecognized (frame);
		//phr.InvokeIfRecognized (frame);
		hsr.InvokeIfRecognized (frame);
		vsr.InvokeIfRecognized (frame);
	}

	void LateUpdate ()
	{
		if (hasNewScreenShot) {
			hasNewScreenShot = false;
			StartCoroutine (DisplayScreenShot ());
		}
	}

	private void TakeScreenShot ()
	{
		lastScreenShotPath = Application.persistentDataPath + "/" + "screenshot.png";
		UnityEngine.Debug.Log ("Screenshot saved: " + lastScreenShotPath);	
		shutterAudio.Play ();
        StartCoroutine (CaptureScreen (lastScreenShotPath));
	}

	private IEnumerator CaptureScreen (string path)
	{
		// Wait till the last possible moment before screen rendering to hide the UI
		yield return null;

		GameObject.Find("ScreenShotCanvas").GetComponent<Canvas>().enabled = false;

		path = "\"" + path + "\"";

		ProcessStartInfo startInfo = new ProcessStartInfo()
		{
			FileName = "screencapture",
			Arguments = path,
		};
		Process proc = new Process()
		{
			StartInfo = startInfo,
		};

		// Wait for screen rendering to complete
		yield return new WaitForEndOfFrame ();

		proc.Start();

		// wait for the process to end
		yield return new WaitForEndOfFrame ();
		yield return new WaitForEndOfFrame ();

		hasNewScreenShot = true;

		GameObject.Find("ScreenShotCanvas").GetComponent<Canvas>().enabled = true;
	}

	private IEnumerator DisplayScreenShot ()
	{
		yield return null;

		Thread.Sleep(1000);

		if (System.IO.File.Exists (lastScreenShotPath)) {
			UnityEngine.Debug.Log ("Last screenshot found!");
			var screenShotBytes = System.IO.File.ReadAllBytes (lastScreenShotPath);
			screenShotPreviewImage.enabled = true;
			screenShotTexture.LoadImage (screenShotBytes);
			screenShotPreviewImage.texture = screenShotTexture;

			Animator anim = GameObject.FindGameObjectWithTag("ScreenShotPreviewImage").GetComponent<Animator> ();
			anim.SetTrigger("PopUp");
		} else {
			UnityEngine.Debug.Log ("File not found");
		}
	}
}
