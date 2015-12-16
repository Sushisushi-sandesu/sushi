using UnityEngine;
using System;
using Leap;

namespace Recognizer
{
	public class PieceRecognizer : RecognizerInterface
	{
		const float next_duration = 3.0f;
		private float totalTime = 3.0f; // To invoke at first
					
		public void invokeIfRecognized(Frame frame, Func<bool> e) 
		{
			addDeltaTime ();

			if (totalTime > next_duration && isRecognized (frame)) {
				resetTotalTime();
				invoke (frame, e);
			}
		}
		
		private bool isRecognized(Frame frame) 
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

		private void resetTotalTime()  {
			totalTime = 0.0f;	
		}

		private void addDeltaTime() {
			totalTime += Time.deltaTime;
		}

		private void invoke(Frame frame, Func<bool> e) 
		{
			e ();
		}
	}
}

