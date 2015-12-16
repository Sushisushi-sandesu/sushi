using UnityEngine;
using System;
using Leap;

namespace Recognizer
{
	public class PeaceRecognizer : SimpleHandSignRecognizer
	{
		public PeaceRecognizer (Action e) : base(e)
		{
			// noop
		}

		public override bool IsRecognized (Frame frame)
		{
			FingerList fingers = frame.Fingers;
			FingerList ff = fingers.Extended ();

			if (ff.Count == 2) {
				if ((ff[0].Type () == Finger.FingerType.TYPE_INDEX && ff[1].Type () == Finger.FingerType.TYPE_MIDDLE) ||
				    (ff[1].Type () == Finger.FingerType.TYPE_INDEX && ff[0].Type () == Finger.FingerType.TYPE_MIDDLE)) {
					Debug.Log("Piece recognized");
					return true;
				}
			}
			return false;
		}
	}
}
