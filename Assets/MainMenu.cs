using UnityEngine;
using System;
using System.Globalization; 
using System.Collections;
using Recognizer;

public class MainMenu : MonoBehaviour {
	public UnityEngine.UI.Image pictureR;
	public UnityEngine.UI.Image pictureL;

	struct message {
		public int gazou;
		public bool you;
	};
		
	Leap.Controller controller = new Leap.Controller();
	private ThumbsUpRecognizer tsr;

	private System.Collections.Generic.Stack <message> messages;
	private bool linemode = false;

	string selectGazou (int tmp) {
		switch (tmp) {
		case 0:
			return "salmon";
		case 1:
			return "toro";
		case 2:
			return "tamago";
		default:
			return "kappa";
		}
	}

	// Use this for initialization
	void Start () {
		tsr = new ThumbsUpRecognizer(() => { Application.LoadLevel ("CameraScene"); });
		messages = new System.Collections.Generic.Stack<message>(){};

	}

	void showImages () {
		if(messages.Count > 0){
			message last = messages.Peek();
			int gazou = last.gazou;
			pictureL.sprite = Resources.Load <Sprite>(selectGazou(gazou));
			pictureR.sprite = Resources.Load <Sprite>(selectGazou(gazou));
			linemode = !linemode;
			pictureL.enabled = linemode;
			pictureR.enabled = linemode;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Leap.Frame frame = controller.Frame ();
		tsr.InvokeIfRecognized (frame);

		if (Input.GetKey(KeyCode.Space)) {
			showImages();

		} else if (Input.GetKey(KeyCode.A)){
			message t = new message();
			t.gazou = UnityEngine.Random.Range (0, 4);
			t.you   = true;
			messages.Push(t);
		}
	}
}
