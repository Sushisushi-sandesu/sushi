using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Globalization; 

public class ClockScript : MonoBehaviour {
	public Image i1;
	public Image i2;
	public Image i3;
	public Image i4;
	private string tm;
	DateTime dt;
	// Use this for initialization
	void Start () {	
		dt = DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
		dt = DateTime.Now;
		if (tm != dt.ToString ()) {
			string strDate = dt.ToString("T", DateTimeFormatInfo.InvariantInfo);		
			i1.sprite = Resources.Load <Sprite>(strDate[3].ToString ());
			i2.sprite = Resources.Load <Sprite>(strDate[4].ToString ());
			i3.sprite = Resources.Load <Sprite>(strDate[6].ToString ());
			i4.sprite = Resources.Load <Sprite>(strDate[7].ToString ());
			tm = dt.ToString ();
		}
	}
}
