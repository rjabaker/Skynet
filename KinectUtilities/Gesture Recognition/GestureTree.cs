using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Kinect;

using KinectUtilities;

namespace KinectUtilities.Gestures
{
    [XmlRoot("GestureTree")]
    public class GestureTree
    {
        #region Private Variables

        private List<ConnectedJoint> connectedJoints;
        private TimeSpan maxDeltaTime;
        private TimeSpan minDeltaTime;
        private bool executed;

        #endregion

        #region Constructors

        public GestureTree()
        {
            this.connectedJoints = new List<ConnectedJoint>();
            this.minDeltaTime = TimeSpan.Zero;
            this.maxDeltaTime = TimeSpan.Zero;
            this.executed = false;
        }
        public GestureTree(TimeSpan minDeltaTime, TimeSpan maxDeltaTime)
        {
            this.connectedJoints = new List<ConnectedJoint>();
            this.minDeltaTime = minDeltaTime;
            this.maxDeltaTime = maxDeltaTime;
            this.executed = false;
        }
        public GestureTree(List<Joint> joints, double jointAngleTolerance, TimeSpan minDeltaTime, TimeSpan maxDeltaTime)
        {
            this.connectedJoints = BuildConnectedJoints(joints, jointAngleTolerance);
            this.minDeltaTime = minDeltaTime;
            this.maxDeltaTime = maxDeltaTime;
            this.executed = false;
        }

        #endregion

        #region Properties

        [XmlArray("ConnectedJoints"),
        XmlArrayItem("ConnectedJoint", typeof(ConnectedJoint))]
        public List<ConnectedJoint> ConnectedJoints
        {
            get
            {
                return connectedJoints;
            }
        }

        [XmlElement("MaxDeltaTime")]
        public TimeSpan MaxDeltaTime
        {
            get
            {
                return maxDeltaTime;
            }
            set
            {
                maxDeltaTime = value;
            }
        }

        [XmlElement("MinDeltaTime")]
        public TimeSpan MinDeltaTime
        {
            get
            {
                return minDeltaTime;
            }
            set
            {
                minDeltaTime = value;
            }
        }

        [XmlIgnore()]
        public bool Executed
        {
            get
            {
                return executed;
            }
            set
            {
                executed = value;
            }
        }

        #endregion

        #region Public Methods

        public void SearchForGestureExecution(Skeleton skeleton, TimeSpan currentExecutionTime)
        {
            bool containsGesture = DoesSkeletonContainGesture(skeleton, currentExecutionTime);
            if (containsGesture) executed = true;
        }
        public bool DoesSkeletonContainGesture(Skeleton skeleton, TimeSpan currentExecutionTime)
        {
            if (!StillActive(currentExecutionTime)) return false;

            List<Joint> requiredJoints = MineRequiredJoints(skeleton);
            return DoesSkeletonContainGesture(requiredJoints);
        }

        public bool StillActive(TimeSpan currentExecutionTime)
        {
            return executed && currentExecutionTime <= maxDeltaTime && currentExecutionTime >= minDeltaTime;
        }
        public bool FailedExecution(TimeSpan currentExecutionTime)
        {
            return !executed && currentExecutionTime >= maxDeltaTime;
        }
        public bool NotActiveYet(TimeSpan currentExecutionTime)
        {
            return currentExecutionTime < minDeltaTime;
        }

        #endregion

        #region Private Methods

        private List<ConnectedJoint> BuildConnectedJoints(List<Joint> joints, double jointAngleTolerance)
        {
            List<ConnectedJoint> connectedJoints = new List<ConnectedJoint>();

            for (int index = 0; index < joints.Count; index++)
            {
                connectedJoints.Add(new ConnectedJoint(joints[index].JointType, index));
            }

            for (int index = 0; index < connectedJoints.Count; index++)
            {
                for (int innerIndex = index + 1; innerIndex < connectedJoints.Count - 1; innerIndex++)
                {
                    if (innerIndex == index + 1) connectedJoints[index].NextJoint = connectedJoints[innerIndex];
                    connectedJoints[index].AddChildJointAngleRule(joints[index], joints[innerIndex], connectedJoints[innerIndex], jointAngleTolerance);
                }
            }

            return connectedJoints;
        }

        private List<Joint> MineRequiredJoints(Skeleton skeleton)
        {
            // Assumes the connectedJoints are sorted.
            List<Joint> requiredJoints = new List<Joint>();
            foreach (ConnectedJoint connectedJoint in connectedJoints)
            {
                requiredJoints.Add(skeleton.Joints[connectedJoint.JointType]);
            }

            return requiredJoints;
        }

        private bool DoesSkeletonContainGesture(List<Joint> skeletonJoints)
        {
            // Returns true if the list of joints matches the gesture tree.
            return connectedJoints.Count != 0 && DoesParentSatisfyRules(connectedJoints.First(), skeletonJoints);
        }
        private bool DoesParentSatisfyRules(ConnectedJoint connectedJoint, List<Joint> skeletonJoints)
        {
            bool satisfyRules = true;

            Joint thisJoint;
            ConnectedJoint child;

            // Extract this joint. If it's null, the gesture will not be found; return false. Remove
            // this joint from the collection, since it's a parent and will no longer be traversed.
            thisJoint = skeletonJoints.Find(joint => joint.JointType == connectedJoint.JointType);
            if (thisJoint == null) return false;
            skeletonJoints.RemoveAll(joint => joint.JointType == connectedJoint.JointType);

            foreach (Joint joint in skeletonJoints)
            {
                child = connectedJoints.Find(cj => cj.JointType == joint.JointType);
                satisfyRules &= child != null && connectedJoint.DoesChildMeetGestureRules(child.ID, thisJoint, joint);
                if (!satisfyRules) break;
            }

            if (connectedJoint.NextJoint == null)
            {
                return satisfyRules;
            }
            else if(!satisfyRules)
            {
                // If false already, exit the traversal.
                return satisfyRules;
            }
            else
            {
                return satisfyRules && DoesParentSatisfyRules(connectedJoint.NextJoint, skeletonJoints);
            }
        }

        #endregion
    }
}
