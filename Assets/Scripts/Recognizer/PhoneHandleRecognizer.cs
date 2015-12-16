using UnityEngine;
using System;
using Leap;

namespace Recognizer
{
	public class PhoneHandleRecognizer : SimpleHandSignRecognizer
	{
		public PhoneHandleRecognizer (Action e) : base(e)
		{
			// noop
		}

		public override bool IsRecognized (Frame frame)
		{
			FingerList fingers = frame.Fingers;
			FingerList ff = fingers.Extended ();

			if (ff.Count == 2) {
				if ((ff[0].Type () == Finger.FingerType.TYPE_PINKY && ff[1].Type () == Finger.FingerType.TYPE_THUMB) ||
				    (ff[1].Type () == Finger.FingerType.TYPE_PINKY && ff[0].Type () == Finger.FingerType.TYPE_THUMB)) {
					Debug.Log("Phone Handle recognized");
					return true;
				}
			}
			return false;
		}
	}
}
