using System;
using Leap;
using UnityEngine;

namespace Recognizer
{
	public abstract class SimpleHandSignRecognizer : RecognizerInterface
	{
		const float nextDuration = 3.0f;
		private float totalTime = 3.0f; // To invoke at first
		private Action e;

		public abstract bool IsRecognized (Frame frame);

		public SimpleHandSignRecognizer(Action e) {
			this.e = e;
		}

		public void InvokeIfRecognized(Frame frame)
		{
			AddDeltaTime ();

			if (totalTime > nextDuration && IsRecognized (frame)) {
				ResetTotalTime();
				Invoke (frame);
			}
		}

		private void ResetTotalTime()  {
			totalTime = 0.0f;
		}

		private void AddDeltaTime() {
			totalTime += Time.deltaTime;
		}

		private void Invoke(Frame frame)
		{
			e ();
		}
	}
}
