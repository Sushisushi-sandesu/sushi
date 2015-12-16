using Leap;
using System;

namespace Recognizer
{
	public class OkRecognizer : SimpleHandSignRecognizer
	{
		private const float THRESHOLD = 20F;
		public OkRecognizer (Action e) : base(e)
		{
			// noop
		}

		// If distance of thumb finger and another finger is short than THRESHOLD, return true.
		public override bool IsRecognized (Frame frame)
		{
			foreach (var hand in frame.Hands)
			{
				Finger thumb = FindFingerByType (hand.Fingers, Finger.FingerType.TYPE_THUMB);
				if (thumb == null) continue;
				foreach (var finger in hand.Fingers)
				{
					if (finger.Type () == Finger.FingerType.TYPE_THUMB) continue;
					if (IsNear (thumb, finger)) return true;
				}
			}
			return false;
		}

		private bool IsNear (Finger one, Finger another)
		{
			return one.TipPosition.DistanceTo (another.TipPosition) < THRESHOLD;
		}

		private Finger FindFingerByType (FingerList fingers, Finger.FingerType type)
		{
			foreach (var finger in fingers)
			{
				if (finger.Type () == type)
				return finger;
			}
			return null;
		}
	}
}
