﻿// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;

namespace ScanningAndDetecting3DObjects
{
	// A custom rotation gesture recognizer that fires only when a threshold is passed
	internal partial class ThresholdRotationGestureRecognizer : UIRotationGestureRecognizer
	{
		// The threshold after which this gesture is detected. 
		const double threshold = Math.PI / 15; // (12°)

		// Indicates whether the currently active gesture has exceed the threshold
		private bool thresholdExceeded = false;

		private double previousRotation = 0;
		internal double RotationDelta { get; private set; }

		internal ThresholdRotationGestureRecognizer(IntPtr handle) : base(handle)
		{
		}

		// Observe when the gesture's state changes to reset the threshold
		public override UIGestureRecognizerState State
		{
			get => base.State;
			set
			{
				base.State = value;

				switch(value)
				{
					case UIGestureRecognizerState.Began :
					case UIGestureRecognizerState.Changed :
						break;
					default :
						// Reset threshold check
						thresholdExceeded = false;
						previousRotation = 0;
						RotationDelta = 0;
						break;
				}
			}
		}

		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);

			if (thresholdExceeded)
			{
				RotationDelta = Rotation - previousRotation;
				previousRotation = Rotation;
			}

			if (! thresholdExceeded && Math.Abs(Rotation) > threshold)
			{
				thresholdExceeded = true;
				previousRotation = Rotation;
			}
		}
	}
}
