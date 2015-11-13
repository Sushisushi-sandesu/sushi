using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR;
using System.Collections;

public class DropDownController : MonoBehaviour {
		
	private Dropdown dropDown;
	private bool isShown;


	// Use this for initialization
	void Start () {
		dropDown = GetComponent<Dropdown> ();
		isShown = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		Event e = Event.current;
		if (e.keyCode == KeyCode.D) {
			if (isShown) {
				dropDown.Hide ();
			} else {
				dropDown.Show ();
			}
			isShown = !isShown;

		}
	}

}
