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
	public UnityEngine.UI.Image[] pictures;


	TcpListener list;
	IPAddress localAddr = IPAddress.Parse("127.0.0.1");
	string remoteAddr = "127.0.0.1";
	int port2 = 5000;
	int port1 = 5005;
	
	struct message {
		public int gazou;
		public bool you;
	};
		
	Leap.Controller controller = new Leap.Controller();
	private ThumbsUpRecognizer tsr;

	private System.Collections.ArrayList messages;
	private bool linemode = false;

	bool spaceEnable = true;
	bool aEnable = true;
	bool receive = false;
	Vector3[] positions = { new Vector3(16.5f,0f,0f), new Vector3(8.5f,0f,0f), new Vector3(0.5f,0f,0f), new Vector3(-7.5f,0f,0f)};

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
		messages = new System.Collections.ArrayList(){};

		list = new TcpListener(localAddr,port1);
		list.Start();
		Thread listener = new Thread(receiveMessage);
		listener.Start();
		for (int i=0; i<4; i++)
			pictures [i].enabled = false;
	}
	void disableSpace(){
		Thread.Sleep (200);
		spaceEnable = true;
	}
	void disableA(){
		Thread.Sleep (400);
		aEnable = true;
	}
 
	void showImages () {
		if(messages.Count > 0 && messages.Count <= 4){
			for(int i=0;i<messages.Count;i++){
				pictures[i].sprite = Resources.Load <Sprite>(selectGazou(((message)messages[i]).gazou));
				pictures[i].enabled = linemode;}
		}
	}
	void moveImages(){
		if (pictures[0].transform.localPosition.x < 16) {
			for(int i=0;i<4;i++)
				pictures[i].transform.localPosition = Vector3.Lerp(pictures[i].transform.localPosition, positions[i], 0.1f);
			return;
		}
		messages.RemoveAt(0);
		UnityEngine.UI.Image first = pictures[0];
		Array.Copy(pictures, 1, pictures, 0, pictures.Length - 1);
		pictures[pictures.Length - 1] = first;
		pictures[3].transform.localPosition = new Vector3(-16f,0f,0f);
		pictures[3].sprite = Resources.Load <Sprite>(selectGazou(((message)messages[3]).gazou));

	}

	void FixedUpdate (){
		if (receive) {
			receive = false;
			showImages();
		}
		if(messages.Count >= 4)
			moveImages();
	}
	// Update is called once per frame
	void Update () {
		Leap.Frame frame = controller.Frame ();
		tsr.InvokeIfRecognized (frame);

		if (Input.GetKey(KeyCode.Space)) {
			if (spaceEnable) {
				Debug.Log("press key Space");
				linemode = !linemode;
				spaceEnable = false;
				Thread exeOnce = new Thread (disableSpace);
				exeOnce.Start ();
				showImages();
			}
		} else if (Input.GetKey(KeyCode.A)){
			if (aEnable) {
				Debug.Log("press key A");
				message t = new message();
				t.gazou = UnityEngine.Random.Range (0, 4);
				t.you   = true;
				messages.Add(t);
				aEnable = false;
				Thread exeOnce = new Thread (disableA);
				exeOnce.Start ();
				showImages();
			}
		} else if (Input.GetKeyDown(KeyCode.S)) {
			if(messages.Count > 0){
				message last = (message)messages[0];
				sendMessage(last);
			}
		}
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
			messages.Add(t);
			client.Close ();
			receive = true;
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
