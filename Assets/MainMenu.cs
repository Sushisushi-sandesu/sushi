using UnityEngine;
using System;
using System.Globalization; 
using System.Collections;
using Recognizer;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public UnityEngine.UI.Image pictureR;
	public UnityEngine.UI.Image pictureL;

	TcpListener list;
	IPAddress localAddr = IPAddress.Parse("127.0.0.1");
	string remoteAddr = "127.0.0.1";
	int port2 = 5000;
	int port1 = 5005;

	bool spaceEnable = true;
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

		list = new TcpListener(localAddr,port1);
		list.Start();
		Thread listener = new Thread(receiveMessage);
		listener.Start();
	}
	void disableSpace(){
		Thread.Sleep (200);
		spaceEnable = true;
	}
	void showImages () {
		if(messages.Count > 0){
			message last = messages.Peek();
			int gazou = last.gazou;
			pictureL.sprite = Resources.Load <Sprite>(selectGazou(gazou));
			pictureR.sprite = Resources.Load <Sprite>(selectGazou(gazou));
		}
		pictureL.enabled = linemode;
		pictureR.enabled = linemode;
	}
	
	// Update is called once per frame
	void Update () {
		Leap.Frame frame = controller.Frame ();
		tsr.InvokeIfRecognized (frame);
	
		if (Input.GetKey(KeyCode.D)) {
			if (spaceEnable) {
				linemode = !linemode;
				spaceEnable = false;
				Thread exeOnce = new Thread (disableSpace);
				exeOnce.Start ();
				Debug.Log (linemode);
			}
			Debug.Log ("press D!!");
		} else if (Input.GetKey(KeyCode.A)){
			message t = new message();
			t.gazou = UnityEngine.Random.Range (0, 4);
			t.you   = true;
			messages.Push(t);
		} else if (Input.GetKeyDown(KeyCode.S)) {
			if(messages.Count > 0){
				message last = messages.Peek();
				sendMessage(last);
			}
		}
		showImages();
	}

	private void receiveMessage(){
		string rd;
		while (true) {
			TcpClient client = list.AcceptTcpClient();
			StreamReader sr = new StreamReader(client.GetStream());
			rd = sr.ReadLine();
			Debug.Log(rd);
			linemode = true;
			message t = new message();
			t.gazou = int.Parse(rd);
			t.you   = false;
			messages.Push(t);
			client.Close ();
		}
	}
	private void sendMessage(message msg) {
		TcpClient client = new TcpClient(remoteAddr, port2);
		StreamWriter sw = new StreamWriter(client.GetStream());
		sw.WriteLine (msg.gazou);
		sw.Flush();
		client.Close();
	}
}
