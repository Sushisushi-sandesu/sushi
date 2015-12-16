using UnityEngine;
using System;
using Leap;

namespace Recognizer
{
	public abstract class SwipeRecognizer : RecognizerInterface
	{
		private Action e;

		public SwipeRecognizer (Action e)
		{
			this.e = e;
		}

		public void InvokeIfRecognized (Frame frame)
		{
			if (IsRecognized (frame))
				Invoke (frame);
		}

		private bool IsRecognized (Frame frame)
		{
			GestureList gestures = frame.Gestures ();
			foreach (Gesture gesture in gestures) {
				if (gesture.Type == Gesture.GestureType.TYPESWIPE) {
					SwipeGesture swipeGesture = new SwipeGesture (gesture);
					return IsAcceptableDirection (swipeGesture.Direction);
				}
			}
			return false;
		}

		private void Invoke (Frame frame)
		{
			e ();
		}

		public abstract bool IsAcceptableDirection (Vector direction);
	}

	public class HorizontalSwipeRecognizer : SwipeRecognizer
	{
		public HorizontalSwipeRecognizer (Action e) : base(e)
		{
			// noop
		}

		public override bool IsAcceptableDirection (Vector direction)
		{
			if (Math.Abs (direction.x) > Math.Abs (direction.z)) {
				Debug.Log ("horizontal"); 
				return true;
			}
			return false;
		}
	}

	public class VerticalSwipeRecognizer : SwipeRecognizer
	{
		public VerticalSwipeRecognizer (Action e) : base(e)
		{
			// noop
		}

		public override bool IsAcceptableDirection (Vector direction)
		{
			if (Math.Abs (direction.x) < Math.Abs (direction.z)) {
				Debug.Log ("vertical"); 
				return true;
			}
			return false;
		}
	}
}
