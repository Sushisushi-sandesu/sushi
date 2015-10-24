using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
using System.Collections;

public class DropDownController : MonoBehaviour {
		
	private Dropdown dropDown;
//	private Vector3 originalRotation;
	private bool isShown;

//	private const int THRESHOLD = 3;


	// Use this for initialization
	void Start () {
		dropDown = GetComponent<Dropdown> ();
//		originalRotation = InputTracking.GetLocalRotation (VRNode.CenterEye).eulerAngles;
		isShown = true;
	}
	
	// Update is called once per frame
	void Update () {
//		Vector3 newRotation = InputTracking.GetLocalRotation (VRNode.CenterEye).eulerAngles;
//		if (Mathf.Abs ((newRotation - originalRotation).y) > THRESHOLD) {
//			if (isShown) {
//				dropDown.Hide ();
//			} else {
//				dropDown.Show ();
//			}
//			isShown = !isShown;
//		}
//		originalRotation = newRotation;




	}

	void OnGUI() {
		Event e = Event.current;
		if (e.type == EventType.KeyDown) {
			if (isShown) {
				dropDown.Hide ();
			} else {
				dropDown.Show ();
			}
			isShown = !isShown;
		}
	}
}
