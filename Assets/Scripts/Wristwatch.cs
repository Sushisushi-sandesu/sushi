using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
using Leap;

public class Wristwatch : MonoBehaviour
{
	Controller controller = new Controller();
	public UnityEngine.UI.Image i1;
	public UnityEngine.UI.Image i2;
	public UnityEngine.UI.Image i3;
	public UnityEngine.UI.Image i4;
	private string tm;
	DateTime dt;
	// Use this for initialization
	void Start () {
		dt = DateTime.Now;
	}

	// Update is called once per frame
	void Update()
	{
		Vector3? wristPosition = GetWristPosition(controller.Frame());
		if (wristPosition.HasValue)
		{
			UnityEngine.Debug.Log(wristPosition.Value.ToString());
			ShowWatch();
			UpdateWatch();
			transform.localPosition = wristPosition.Value;
		}
		else
		{
			HideWatch();
		}
	}

	private void ShowWatch()
	{
		i1.enabled = true;
		i2.enabled = true;
		i3.enabled = true;
		i4.enabled = true;
	}

	private void HideWatch()
	{
		i1.enabled = false;
		i2.enabled = false;
		i3.enabled = false;
		i4.enabled = false;
	}

	private void UpdateWatch ()
	{
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

	private Vector3? GetWristPosition (Frame frame)
    {
		InteractionBox interactionBox = frame.InteractionBox;
		Hand hand = frame.Hands.Leftmost;
		if (!hand.IsValid) return null;
		Vector wristPosition = interactionBox.NormalizePoint(hand.Arm.WristPosition);
		return new Vector3(wristPosition.x, wristPosition.y, wristPosition.z);
	}
}
