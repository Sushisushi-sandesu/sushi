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

		public abstract bool isRecognized (Frame frame);

		public SimpleHandSignRecognizer(Action e) {
			this.e = e;
		}

		public void invokeIfRecognized(Frame frame)
		{
			addDeltaTime ();

			if (totalTime > nextDuration && isRecognized (frame)) {
				resetTotalTime();
				invoke (frame);
			}
		}

		private void resetTotalTime()  {
			totalTime = 0.0f;
		}

		private void addDeltaTime() {
			totalTime += Time.deltaTime;
		}

		private void invoke(Frame frame)
		{
			e ();
		}
	}
}
