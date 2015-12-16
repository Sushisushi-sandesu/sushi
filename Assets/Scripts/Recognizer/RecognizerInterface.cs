using Leap;
using System;

namespace Recognizer
{
	interface RecognizerInterface
	{
		void invokeIfRecognized(Frame frame, Func<bool> e);
	}
}
