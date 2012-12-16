using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectUtilities;
using KinectUtilities.JointTracking;

namespace KinectUtilities
{
    public static class JointUtilities
    {
        #region Variables

        // RBakerFlag -> Does not support Shoulder Center and Hip Center moving joints, since they are actually
        // defined as multiple moving joints. (A moving joint as one degree of rotation.)
        public static readonly JointType[] MovingJoints = new JointType[]
        {
            JointType.WristLeft, JointType.WristRight, JointType.ElbowLeft, JointType.ElbowRight,
            JointType.ShoulderLeft, JointType.ShoulderRight, JointType.HipLeft, JointType.HipRight,
            JointType.KneeLeft, JointType.KneeRight, JointType.AnkleLeft, JointType.AnkleRight
        };

        #endregion

        #region Methods

        public static JointType[] GetConnectedJoints(JointType jointType)
        {
            // Returns an array of joints connected to the specified joint.
            switch (jointType)
            {
                case JointType.AnkleLeft:
                    return new JointType[] { JointType.FootLeft, JointType.KneeLeft };
                case JointType.AnkleRight:
                    return new JointType[] { JointType.FootRight, JointType.KneeRight };
                case JointType.ElbowLeft:
                    return new JointType[] { JointType.ShoulderLeft, JointType.WristLeft };
                case JointType.ElbowRight:
                    return new JointType[] { JointType.ShoulderRight, JointType.WristRight };
                case JointType.FootLeft:
                    return new JointType[] { JointType.KneeLeft };
                case JointType.FootRight:
                    return new JointType[] { JointType.KneeRight };
                case JointType.HandLeft:
                    return new JointType[] { JointType.WristLeft };
                case JointType.HandRight:
                    return new JointType[] { JointType.WristRight };
                case JointType.Head:
                    return new JointType[] { JointType.ShoulderCenterHead };
                case JointType.HipCenterSpine:
                    return new JointType[] { JointType.Spine };
                case JointType.HipCenterLeft:
                    return new JointType[] { JointType.Spine, JointType.HipLeft };
                case JointType.HipCenterRight:
                    return new JointType[] { JointType.Spine, JointType.HipRight };
                case JointType.HipLeft:
                    return new JointType[] { JointType.KneeLeft, JointType.HipCenterLeft };
                case JointType.HipRight:
                    return new JointType[] { JointType.KneeRight, JointType.HipCenterRight };
                case JointType.KneeLeft:
                    return new JointType[] { JointType.AnkleLeft, JointType.HipLeft };
                case JointType.KneeRight:
                    return new JointType[] { JointType.AnkleRight, JointType.HipRight };
                case JointType.ShoulderCenterHead:
                    return new JointType[] { JointType.Head, JointType.Spine };
                case JointType.ShoulderCenterLeft:
                    return new JointType[] { JointType.ShoulderLeft, JointType.ShoulderRight };
                case JointType.ShoulderCenterRight:
                    return new JointType[] { JointType.ShoulderRight, JointType.ShoulderLeft };
                case JointType.ShoulderCenterSpine:
                    return new JointType[] { JointType.Spine, JointType.Head };
                case JointType.ShoulderLeft:
                    return new JointType[] { JointType.ShoulderCenterLeft, JointType.WristLeft };
                case JointType.ShoulderRight:
                    return new JointType[] { JointType.ShoulderCenterRight, JointType.WristRight };
                case JointType.Spine:
                    return new JointType[] { JointType.ShoulderCenterSpine, JointType.HipCenterSpine };
                case JointType.WristLeft:
                    return new JointType[] { JointType.ElbowLeft, JointType.HandLeft };
                case JointType.WristRight:
                    return new JointType[] { JointType.ElbowRight, JointType.HandRight };
                default:
                    throw new Exception("JointUtilities failed: Unknown joint detected!");
            }
        }
        public static Microsoft.Kinect.JointType TranslateJointType(KinectUtilities.JointTracking.JointType jointType)
        {
            switch (jointType)
            {
                case JointTracking.JointType.AnkleLeft:
                    return Microsoft.Kinect.JointType.AnkleLeft;
                case JointTracking.JointType.AnkleRight:
                    return Microsoft.Kinect.JointType.AnkleRight;
                case JointTracking.JointType.ElbowLeft:
                    return Microsoft.Kinect.JointType.ElbowLeft;
                case JointTracking.JointType.ElbowRight:
                    return Microsoft.Kinect.JointType.ElbowRight;
                case JointTracking.JointType.FootLeft:
                    return Microsoft.Kinect.JointType.FootLeft;
                case JointTracking.JointType.FootRight:
                    return Microsoft.Kinect.JointType.FootRight;
                case JointTracking.JointType.HandLeft:
                    return Microsoft.Kinect.JointType.HandLeft;
                case JointTracking.JointType.HandRight:
                    return Microsoft.Kinect.JointType.HandRight;
                case JointTracking.JointType.Head:
                    return Microsoft.Kinect.JointType.Head;
                case JointTracking.JointType.HipCenterSpine:
                    return Microsoft.Kinect.JointType.HipCenter;
                case JointTracking.JointType.HipCenterLeft:
                    return Microsoft.Kinect.JointType.HipCenter;
                case JointTracking.JointType.HipCenterRight:
                    return Microsoft.Kinect.JointType.HipCenter;
                case JointTracking.JointType.HipLeft:
                    return Microsoft.Kinect.JointType.HipLeft;
                case JointTracking.JointType.HipRight:
                    return Microsoft.Kinect.JointType.HipRight;
                case JointTracking.JointType.KneeLeft:
                    return Microsoft.Kinect.JointType.KneeLeft;
                case JointTracking.JointType.KneeRight:
                    return Microsoft.Kinect.JointType.KneeRight;
                case JointTracking.JointType.ShoulderCenterHead:
                    return Microsoft.Kinect.JointType.ShoulderCenter;
                case JointTracking.JointType.ShoulderCenterLeft:
                    return Microsoft.Kinect.JointType.ShoulderCenter;
                case JointTracking.JointType.ShoulderCenterRight:
                    return Microsoft.Kinect.JointType.ShoulderCenter;
                case JointTracking.JointType.ShoulderCenterSpine:
                    return Microsoft.Kinect.JointType.ShoulderCenter;
                case JointTracking.JointType.ShoulderLeft:
                    return Microsoft.Kinect.JointType.ShoulderLeft;
                case JointTracking.JointType.ShoulderRight:
                    return Microsoft.Kinect.JointType.ShoulderRight;
                case JointTracking.JointType.Spine:
                    return Microsoft.Kinect.JointType.Spine;
                case JointTracking.JointType.WristLeft:
                    return Microsoft.Kinect.JointType.WristLeft;
                case JointTracking.JointType.WristRight:
                    return Microsoft.Kinect.JointType.WristRight;
                default:
                    throw new Exception("JointUtilities failed: Unknown joint detected!");
            }
        }

        #endregion
    }
}
