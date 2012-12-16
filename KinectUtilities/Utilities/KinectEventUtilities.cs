using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

using Microsoft.Kinect;

using KinectUtilities.Gestures;
using KinectUtilities.JointTracking;

namespace KinectUtilities
{
    public static class KinectEventUtilities
    {
        #region Delegates

        public delegate void SkeletonRenderedEventHandler(List<Skeleton> skeletons, Bitmap image, DateTime timeStamp);
        public delegate void GestureCapturedEventHandler(IGesture gesture, DateTime timeStamp);
        public delegate void JointTrackingCapturedEventHandler(MovingJoint joint, DateTime timeStamp);

        #endregion
    }
}
