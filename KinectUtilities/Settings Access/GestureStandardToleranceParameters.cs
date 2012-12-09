using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectUtilities
{
    public static class GestureStandardToleranceParameters
    {
        public static double JointAngleTolerance
        {
            get
            {
                return Settings.GestureStandardToleranceParameters.Default.JointAngleTolerance;
            }
        }

        public static double FramesPerSecondCapture
        {
            get
            {
                return Settings.GestureStandardToleranceParameters.Default.FramesPerSecondCapture;
            }
        }
    }
}
