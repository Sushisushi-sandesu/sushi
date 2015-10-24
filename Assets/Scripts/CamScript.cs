using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CamScript : MonoBehaviour {

	public RawImage image;

	// Use this for initialization
	void Start () {

		WebCamDevice[] devices = WebCamTexture.devices;

//		string backCamName = "";
		for (int i = 0; i < devices.Length; i++) {
			Debug.Log ("Device " + i +  ":" + devices [i].name + "IS FRONT FACING:" + devices [i].isFrontFacing);
//			if (!devices[i].isFrontFacing) {
//				backCamName = devices[i].name;
//			}
		}

		WebCamTexture webCamTexture = new WebCamTexture (devices[0].name, 1280, 720, 30);


		image.texture = webCamTexture;
		image.material.mainTexture = webCamTexture;
		webCamTexture.Play ();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
