using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ClockScript : MonoBehaviour {
	public Text txt;
	DateTime dt;
	// Use this for initialization
	void Start () {
		dt = DateTime.Now;
		txt.text = dt.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		dt = DateTime.Now;
		txt.text = dt.ToString ();
	}
}
