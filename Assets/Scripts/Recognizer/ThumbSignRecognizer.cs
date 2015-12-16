using System;
using Leap;

namespace Recognizer
{
	public abstract class ThumbSignRecognizer : SimpleHandSignRecognizer
	{
		public ThumbSignRecognizer (Action e) : base(e)
		{
			// noop
		}

		override public bool IsRecognized (Frame frame)
		{
			foreach (var hand in frame.Hands)
			{
				if (IsOnlyThumbExtended (hand))
				{
					if (IsProperDirection(GetThumbFinger(hand.Fingers).Direction))
						return true;
				}
			}
			return false;
		}

		abstract protected bool IsProperDirection(Vector direction);

		protected bool IsOnlyThumbExtended(Hand hand)
		{
			FingerList extendedFingers = hand.Fingers.Extended();
			return extendedFingers.Count == 1 &&
				   extendedFingers[0].Type() == Finger.FingerType.TYPE_THUMB;
		}

		protected Finger GetThumbFinger (FingerList fingers)
		{
			foreach (var finger in fingers)
			{
				if (finger.Type() == Finger.FingerType.TYPE_THUMB) return finger;
			}
			return null;
		}
	}

	public class ThumbsUpRecognizer : ThumbSignRecognizer
	{
		public ThumbsUpRecognizer (Action e) : base(e)
		{
			// noop
		}

		override protected bool IsProperDirection (Vector direction)
		{
			return direction.y > 0.5;
		}
	}

	public class ThumbsDownRecognizer : ThumbSignRecognizer
	{
		public ThumbsDownRecognizer (Action e) : base(e)
		{
			// noop
		}

		override protected bool IsProperDirection (Vector direction)
		{
			return direction.y < -0.5;
		}
	}
}
