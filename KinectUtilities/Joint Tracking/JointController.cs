using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Kinect;

using KinectUtilities;

namespace KinectUtilities.JointTracking
{
    public class JointController : ISkeletonCapturingFunction
    {
        #region Private Variables

        private readonly object thisLock = new object();

        #endregion

        #region Public Methods

        public void Execute(SkeletonCaptureData data)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Properties

        public object Lock
        {
            get
            {
                return thisLock;
            }
        }

        #endregion

        #region Public Methods

        public void Execute(object data)
        {
            // Execute((SkeletonCaptureData)data);
        }

        #endregion
    }
}
