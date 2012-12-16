using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ToolBox.Math;

namespace KinectUtilities.JointTracking
{
    /// <summary>
    /// Moving joints bend, causing stationary joints to translate their position. For example, a wrist joint
    /// bends and causes the hand joint to translate its position. Moving joints have on degree of rotation/bending.
    /// </summary>
    public class MovingJoint : Joint
    {
        #region Private Variables

        private double bendAngle;

        #endregion

        #region Constructors

        public MovingJoint(JointType jointType, Vertex3 position, double bendAngle)
            : base(jointType, position)
        {
            this.bendAngle = bendAngle;
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

        #endregion
    }
}
