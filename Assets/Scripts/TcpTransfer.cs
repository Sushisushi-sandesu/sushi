using UnityEngine;
using System.Collections;

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;

public class TcpTransfer : MonoBehaviour {
	
	public RawImage screenShot;
	private Texture2D screenShotTexture;

	TcpListener list;
	TcpListener list2;
	IPAddress localAddr = IPAddress.Parse("127.0.0.1");
	string remoteAddr = "127.0.0.1";
	int port1 = 5055;
	int port2 = 5050;

	string fileName;
	string receiveFileName;
	
	// Use this for initialization
	void Start () {
		Debug.Log ("starting!!!");
		screenShotTexture = new Texture2D (100, 100);
		screenShot.texture = screenShotTexture;

		string current_time = System.DateTime.Now.ToString ().Replace ("/", "_").Replace (":", "_");
		receiveFileName = Application.temporaryCachePath + "/" + current_time + ".png";
		fileName = Application.temporaryCachePath + "/photo.png";
		
		list = new TcpListener(localAddr,port1);
		list2 = new TcpListener(localAddr,port2);
		list.Start();
		list2.Start();
		Debug.Log ("get file info");
		Thread listener = new Thread(keepListen);
		listener.Start();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.S)) {
			Debug.Log ("sent file info");
			sendFile();
		}
	}
	private void keepListen(){
		while (true) {
			getFile();
		}
	}
	private void getFile() {
		TcpClient client = list.AcceptTcpClient();
		StreamReader sr = new StreamReader(client.GetStream());
		string rd = sr.ReadLine();
		string v = rd.Substring(rd.LastIndexOf('.') + 1);
		
		Debug.Log("reading file. Length: " + v + " bytes.");
		TcpClient client2 = list2.AcceptTcpClient();
		
		Debug.Log("reading from rnetworkStream");
		NetworkStream rnetworkStream = client2.GetStream();
		FileStream fileStream = new FileStream(receiveFileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
		long filezie = int.Parse(v);
		int byteSize = 0;
		
		byte[] downBuffer = new byte[2048];
		while ((byteSize = rnetworkStream.Read(downBuffer, 0, downBuffer.Length)) > 0)
		{
			fileStream.Write(downBuffer, 0, byteSize);
		}
		fileStream.Close();
		rnetworkStream.Close();
		client.Close ();
		client2.Close ();
		Debug.Log("Finish geting file");
	}
	private void sendFile() {
		FileInfo fi = new FileInfo(fileName);
		
		Debug.Log("Trying to connect: " + remoteAddr + ": " + port1);
		TcpClient client = new TcpClient(remoteAddr, port1);
		Debug.Log("File: " + fi.Name + "  Length: " + fi.Length);
		StreamWriter sw = new StreamWriter(client.GetStream());
		
		sw.WriteLine(fi.Name + "." + fi.Length);
		sw.Flush();
		System.Threading.Thread.Sleep (100);
		Debug.Log("Trying to connect: " + remoteAddr + ": " + port2);
		TcpClient client2 = new TcpClient(remoteAddr, port2);
		Debug.Log("File: " + fi.Name + "  Length: " + fi.Length);
		
		NetworkStream wnetworkStream = client2.GetStream();
		byte [] b1 = File.ReadAllBytes(fileName);
		Debug.Log("Writing file. Length: " + b1.Length + " bytes.");
		
		FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
		
		int bytesSize = 0;
		byte[] downBuffer = new byte[2048];
		while ((bytesSize = fileStream.Read(downBuffer, 0, downBuffer.Length)) > 0)
		{
			wnetworkStream.Write(downBuffer, 0, bytesSize);
		}
		fileStream.Close();
		client.Close();
	}
	
	void LateUpdate ()
	{
		StartCoroutine (DisplayScreenShot ());
	}
	
	private void TakeScreenShot (string path)
	{
		StartCoroutine (CaptureScreen (path));
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
		
		if (System.IO.File.Exists (receiveFileName)) {
			//	Debug.Log ("Last screenshot found!");
			var screenShotBytes = System.IO.File.ReadAllBytes (receiveFileName);
			screenShotTexture.LoadImage (screenShotBytes);
			screenShot.material.mainTexture = screenShotTexture;
		}
	}
}
