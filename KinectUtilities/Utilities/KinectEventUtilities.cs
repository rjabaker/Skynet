using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

using Microsoft.Kinect;

namespace KinectUtilities
{
    public static class KinectEventUtilities
    {
        #region Delegates

        public delegate void SkeletonRenderedEventHandler(Bitmap image);
        public delegate void GestureCapturedEventHandler(IGesture gesture);

        #endregion
    }
}
