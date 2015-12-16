using Leap;
using System;
using UnityEngine;

namespace Recognizer
{
	public class PrayRecognizer : SimpleHandSignRecognizer
	{
		private const float DISTANCE_THRESHOLD = 60F;
		public PrayRecognizer (Action e) : base(e)
		{
			// noop
		}

		public override bool IsRecognized (Frame frame)
		{
			if (frame.Hands.Count < 2) return false;
			var palmDistance =
				frame.Hands[0].StabilizedPalmPosition.DistanceTo(frame.Hands[1].StabilizedPalmPosition);

			var palmNormalDot =
				frame.Hands[0].PalmNormal.Dot(frame.Hands[1].PalmNormal);

			var handDirectionDot =
				frame.Hands[0].Direction.Dot(frame.Hands[1].Direction);

			return palmDistance < DISTANCE_THRESHOLD &&
				palmNormalDot < 0F &&
				handDirectionDot > 0F;
		}
	}
}
