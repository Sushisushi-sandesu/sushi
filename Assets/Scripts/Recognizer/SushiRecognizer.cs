using Leap;
using System;

namespace Recognizer
{
	public class SushiRecognizer : SimpleHandSignRecognizer
	{
		public SushiRecognizer (Action e) : base(e)
		{
			// noop
		}

		public override bool IsRecognized (Frame frame)
		{
			foreach (var hand in frame.Hands)
			{
				Finger thumb = FindFingerByType(hand.Fingers, Finger.FingerType.TYPE_THUMB);
				Finger index = FindFingerByType(hand.Fingers, Finger.FingerType.TYPE_INDEX);
				Finger middle = FindFingerByType(hand.Fingers, Finger.FingerType.TYPE_MIDDLE);

				if (
					IsTipNear(index, middle) &&
					IsNearFromThumb(index, thumb) &&
					IsProperFingersFolded(hand.Fingers)
				)
					return true;
			}
			return false;
		}

		// Return true if the two fingers' tips are near.
		private bool IsTipNear (Finger one, Finger another)
		{
			return one.TipPosition.DistanceTo (another.TipPosition) < 20F;
		}

		// Return true if the finger and the thumb's tip are near.
		private bool IsNearFromThumb (Finger finger, Finger thumb)
		{
			Vector fingerBase = finger.Bone(Bone.BoneType.TYPE_METACARPAL).Center;
			Vector baseToFingerTip = finger.TipPosition - fingerBase;
			Vector baseToThumbTip = thumb.TipPosition - fingerBase;

			float distance =
				baseToFingerTip.Cross(baseToThumbTip).Magnitude / baseToFingerTip.Magnitude;
			return distance < 40F;
		}

		// Return true if the ring finger and pinky finger are folded.
		// This code checks it by calculating their angle from the index finger instead of checking
		// IsExtended property of the fingers.
		//
		// This is because the indexFinger's IsExtended property often becomes false
		// when you make this sushi gesture.
		// The IsExtended property bacomes false if you bend your finger only slightly.
		private bool IsProperFingersFolded (FingerList fingers)
		{
			Vector indexDirection =
				FindFingerByType(fingers, Finger.FingerType.TYPE_INDEX).Direction;
			Vector ringDirection =
				FindFingerByType(fingers, Finger.FingerType.TYPE_RING).Direction;
			Vector pinkyDirection =
				FindFingerByType(fingers, Finger.FingerType.TYPE_PINKY).Direction;

			float threshold = 0.8F;

			return indexDirection.Dot(ringDirection) < threshold &&
				   indexDirection.Dot(pinkyDirection) < threshold &&
				   indexDirection.Cross(ringDirection).Magnitude > 0 &&
				   indexDirection.Cross(pinkyDirection).Magnitude > 0;

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
