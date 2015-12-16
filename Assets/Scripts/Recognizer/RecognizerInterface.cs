using Leap;
using System;

namespace Recognizer
{
	public interface RecognizerInterface
	{
		void invokeIfRecognized(Frame frame);
	}
}
