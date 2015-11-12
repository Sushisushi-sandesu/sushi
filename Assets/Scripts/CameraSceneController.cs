using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class CameraSceneController : MonoBehaviour {


//	public OVRCameraRig currentCameraRig;
	private Canvas canvas;

	private const float DISTANCE_TO_CAMERA = 300.0f;


	private Vector3 offset;

	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas> ();
		offset = canvas.transform.position - GetInputTrackingLocalRotation();
	}
	
	// Update is called once per frame
	void Update () {
		canvas.transform.position = GetInputTrackingLocalPosition() + offset;
	}

	Vector3 GetInputTrackingLocalRotation()
	{
		return InputTracking.GetLocalRotation (VRNode.CenterEye).eulerAngles;
	}
	Vector3 GetInputTrackingLocalPosition()
	{
		return InputTracking.GetLocalPosition (VRNode.CenterEye);
	}
}
