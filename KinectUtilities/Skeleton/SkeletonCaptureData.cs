using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;

using Microsoft.Kinect;

namespace KinectUtilities
{
    public class SkeletonCaptureData
    {
        #region Private Variables

        private List<Skeleton> skeletons;
        private ColorImageFrame imageFrame;
        private DateTime timeStamp;

        #endregion

        #region Constructors

        public SkeletonCaptureData(List<Skeleton> skeletons, DateTime timeStamp)
        {
            this.skeletons = skeletons;
            this.imageFrame = null;
            this.timeStamp = timeStamp;
        }
        public SkeletonCaptureData(List<Skeleton> skeletons, ColorImageFrame imageFrame, DateTime timeStamp)
        {
            this.skeletons = skeletons;
            this.imageFrame = imageFrame;
            this.timeStamp = timeStamp;
        }

        #endregion

        #region Properties

        public List<Skeleton> Skeletons
        {
            get
            {
                return skeletons;
            }
        }
        public ColorImageFrame ImageFrame
        {
            get
            {
                return imageFrame;
            }
        }
        public DateTime TimeStamp
        {
            get
            {
                return timeStamp;
            }
        }

        #endregion
    }
}
