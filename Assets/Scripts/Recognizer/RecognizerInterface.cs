using Leap;
using System;

namespace Recognizer
{
	public interface RecognizerInterface
	{
		void InvokeIfRecognized(Frame frame);
	}
}
