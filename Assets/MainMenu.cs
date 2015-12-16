using UnityEngine;
using System.Collections;
using Leap;
using Recognizer;

public class MainMenu : MonoBehaviour {

	Controller controller = new Controller();
	private ThumbsUpRecognizer tsr;

	// Use this for initialization
	void Start () {
		tsr = new ThumbsUpRecognizer(() => { Application.LoadLevel ("CameraScene"); });
	}
	
	// Update is called once per frame
	void Update () {
		Frame frame = controller.Frame ();
		tsr.InvokeIfRecognized (frame);
	}
}
