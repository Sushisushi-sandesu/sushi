using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class CameraSceneController : MonoBehaviour {


//	public OVRCameraRig currentCameraRig;
	private Canvas canvas;

	// oculus position and rotation
//	private Vector3 position;
//	private Quaternion rotation;

	private const float DISTANCE_TO_CAMERA = 300.0f;


	private Vector3 offset;

	// Use this for initialization
	void Start () {
//		position = InputTracking.GetLocalPosition (VRNode.CenterEye);
//		rotation = GetInputTrackingLocalRotation();
		canvas = GetComponent<Canvas> ();
		offset = canvas.transform.position - GetInputTrackingLocalRotation();
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log ("local position: " + InputTracking.GetLocalPosition (VRNode.CenterEye));
		Debug.Log ("local rotation: " + InputTracking.GetLocalRotation (VRNode.CenterEye).eulerAngles);
		canvas.transform.position = GetInputTrackingLocalPosition() + offset;
//		canvas.transform.rotation = GetInputTrackingLocalRotation();
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
