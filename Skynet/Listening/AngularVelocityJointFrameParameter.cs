using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectUtilities.JointTracking;

namespace Skynet
{
    public class AngularVelocityJointFrameParameter : IJointFrameParameter
    {
        #region Private Methods

        private MovingJoint movingJoint;
        private TimeSpan timeSpan;

        #endregion

        #region Constructors

        public AngularVelocityJointFrameParameter(MovingJoint movingJoint, TimeSpan timeSpan)
        {
            this.movingJoint = movingJoint;
            this.timeSpan = timeSpan;
        }

        #endregion
    }
}
