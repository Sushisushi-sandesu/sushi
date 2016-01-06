using Leap;
using System;

namespace Recognizer
{
	abstract public class SingleFingerGestureRecognizer : SimpleHandSignRecognizer
	{
		abstract protected Finger.FingerType FingerTypeToExtend();

		public SingleFingerGestureRecognizer(Action e) : base(e)
		{
			// noop
		}

		override public bool IsRecognized(Frame frame)
		{
			foreach (var hand in frame.Hands)
			{
				var fingerToExtend = FindFingerByType(hand, FingerTypeToExtend());
				if (fingerToExtend != null && fingerToExtend.IsExtended &&
					IsOtherFingerClosed(hand, FingerTypeToExtend()))
					return true;
			}
			return false;
		}

		protected Finger FindFingerByType(Hand hand, Finger.FingerType type)
		{
			foreach (var finger in hand.Fingers)
			{
				if (finger.Type() == type)
				return finger;
			}
			return null;
		}

		protected bool IsOtherFingerClosed(Hand hand, Finger.FingerType type)
		{
			foreach (var finger in hand.Fingers)
			{
				if (finger.Type() != type && finger.IsExtended)
				return false;
			}
			return true;
		}
	}

	public class MiddleFingerGestureRecognizer : SingleFingerGestureRecognizer
	{
		override protected Finger.FingerType FingerTypeToExtend ()
		{
			return Finger.FingerType.TYPE_MIDDLE;
		}

		public MiddleFingerGestureRecognizer(Action e) : base(e)
		{
			// noop
		}
	}

	public class PinkyFingerGestureRecognizer : SingleFingerGestureRecognizer
	{
		override protected Finger.FingerType FingerTypeToExtend ()
		{
			return Finger.FingerType.TYPE_PINKY;
		}

		public PinkyFingerGestureRecognizer(Action e) : base(e)
		{
			// noop
		}
	}
}
