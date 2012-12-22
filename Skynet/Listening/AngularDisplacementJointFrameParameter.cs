using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectUtilities.JointTracking;
using ToolBox.Functions;

namespace Skynet
{
    public class AngularDisplacementJointFrameParameter : IJointFrameParameter
    {
        #region Private Methods

        private MovingJoint movingJoint;
        private TimeSpan timeSpan;
        private double bendAngle;

        #endregion

        #region Properties

        public AngularDisplacementJointFrameParameter(MovingJoint thisMovingJoint, MovingJoint previousMovingJoint, TimeSpan timeSpan)
        {
            this.movingJoint = thisMovingJoint;
            this.timeSpan = timeSpan;
            this.bendAngle = previousMovingJoint == null ? thisMovingJoint.BendAngle : thisMovingJoint.BendAngle - previousMovingJoint.BendAngle;
        }

        #endregion

        #region Properties

        public double BendAngle
        {
            get
            {
                return bendAngle;
            }
        }
        public TimeSpan TimeSpan
        {
            get
            {
                return timeSpan;
            }
        }

        #endregion
    }
}
