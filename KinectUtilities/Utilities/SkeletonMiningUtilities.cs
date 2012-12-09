using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Kinect;

namespace KinectUtilities
{
    public static class SkeletonMiningUtilities
    {
        #region Public Static Methods

        public static List<Joint> GetJointCollection(Skeleton skeleton, SkeletonJointCollection jointCollectionType)
        {
            switch (jointCollectionType)
            {
                case SkeletonJointCollection.HeadAndSpine:
                    return GetHeadAndSpineJointCollection(skeleton);
                case SkeletonJointCollection.RightArm:
                    return GetRightArmJointCollection(skeleton);
                case SkeletonJointCollection.LeftArm:
                    return GetLeftArmJointCollection(skeleton);
                case SkeletonJointCollection.RightLeg:
                    return GetRightLegJointCollection(skeleton);
                case SkeletonJointCollection.LeftLeg:
                    return GetLeftLegJointCollection(skeleton);
                default:
                    return new List<Joint>();
            }
        }

        #endregion

        #region Private Static Methods

        private List<Joint> GetHeadAndSpineJointCollection(Skeleton skeleton)
        {
            List<Joint> joints = new List<Joint>();
            joints.Add(skeleton.Joints[JointType.Head]);
            joints.Add(skeleton.Joints[JointType.ShoulderCenter]);
            joints.Add(skeleton.Joints[JointType.Spine]);
            joints.Add(skeleton.Joints[JointType.HipCenter]);

            return joints;
        }
        private List<Joint> GetRightArmJointCollection(Skeleton skeleton)
        {
            List<Joint> joints = new List<Joint>();
            joints.Add(skeleton.Joints[JointType.ShoulderCenter]);
            joints.Add(skeleton.Joints[JointType.ShoulderRight]);
            joints.Add(skeleton.Joints[JointType.ElbowRight]);
            joints.Add(skeleton.Joints[JointType.WristRight]);
            joints.Add(skeleton.Joints[JointType.HandRight]);

            return joints;
        }
        private List<Joint> GetLeftArmJointCollection(Skeleton skeleton)
        {
            List<Joint> joints = new List<Joint>();
            joints.Add(skeleton.Joints[JointType.ShoulderCenter]);
            joints.Add(skeleton.Joints[JointType.ShoulderLeft]);
            joints.Add(skeleton.Joints[JointType.ElbowLeft]);
            joints.Add(skeleton.Joints[JointType.WristLeft]);
            joints.Add(skeleton.Joints[JointType.HandLeft]);

            return joints;
        }
        private List<Joint> GetRightLegJointCollection(Skeleton skeleton)
        {
            List<Joint> joints = new List<Joint>();
            joints.Add(skeleton.Joints[JointType.HipCenter]);
            joints.Add(skeleton.Joints[JointType.HipRight]);
            joints.Add(skeleton.Joints[JointType.KneeRight]);
            joints.Add(skeleton.Joints[JointType.AnkleRight]);
            joints.Add(skeleton.Joints[JointType.FootRight]);

            return joints;
        }
        private List<Joint> GetLeftLegJointCollection(Skeleton skeleton)
        {
            List<Joint> joints = new List<Joint>();
            joints.Add(skeleton.Joints[JointType.HipCenter]);
            joints.Add(skeleton.Joints[JointType.HipLeft]);
            joints.Add(skeleton.Joints[JointType.KneeLeft]);
            joints.Add(skeleton.Joints[JointType.AnkleLeft]);
            joints.Add(skeleton.Joints[JointType.FootLeft]);

            return joints;
        }

        #endregion

        #region Enumerated Types

        public enum SkeletonJointCollection
        {
            RightArm = 1,
            LeftArm = 2,
            RightLeg = 3,
            LeftLeg = 4,
            HeadAndSpine = 5
        }

        #endregion
    }
}
