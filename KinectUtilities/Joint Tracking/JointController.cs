using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectUtilities;
using ToolBox.Math;

namespace KinectUtilities.JointTracking
{
    public class JointController : ISkeletonCapturingFunction
    {
        #region Events

        public event KinectEventUtilities.JointTrackingCapturedEventHandler JointTrackingCaptured;

        #endregion

        #region Private Variables

        private readonly object thisLock = new object();
        private SkeletonCapturingFunctionPriority priority;
        private bool longOperation;

        #endregion

        #region Constructors

        public JointController()
        {
            this.longOperation = false;
            this.priority = SkeletonCapturingFunctionPriority.Normal;
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
        public bool LongOperation
        {
            get
            {
                return longOperation;
            }
        }
        public SkeletonCapturingFunctionPriority Priority
        {
            get
            {
                return priority;
            }
        }

        #endregion

        #region Public Methods

        public void Execute(object data)
        {
            if (data is SkeletonCaptureData)
            {
                Execute((SkeletonCaptureData)data);
            }
        }
        public void Execute(SkeletonCaptureData data)
        {
            // Only supports single skeleton capture.
            if (data.Skeletons.Count == 0) return;
            TrackSkeletonJoints(data.Skeletons[0], data.TimeStamp);
        }

        #endregion

        #region Private Methods

        private void TrackSkeletonJoints(Microsoft.Kinect.Skeleton skeleton, DateTime timeStamp)
        {
            // Tracks the MovingJoints in the specified skeleton.
            foreach (JointType jointType in JointUtilities.MovingJoints)
            {
                // RBakerFlag -> Needs to be refactored. Need to fire event.
                JointType[] connectedJoints = JointUtilities.GetConnectedJoints(jointType);
                if (!ShouldTrackJoint(skeleton, jointType, connectedJoints)) continue;

                Vertex3 jointPosition = GetJointPosition(skeleton, jointType);
                Vertex3[] connectedJointPositions = GetJointPositions(skeleton, connectedJoints);

                Vector3 boneA = new Vector3(connectedJointPositions[0], jointPosition);
                Vector3 boneB = new Vector3(connectedJointPositions[1], jointPosition);
                double bendAngle = Vector3Functions.Angle(boneA, boneB);

                MovingJoint movingJoint = new MovingJoint(jointType, jointPosition, bendAngle);
                OnJointTrackingCaptured(movingJoint, timeStamp);
            }
        }

        private bool ShouldTrackJoint(Microsoft.Kinect.Skeleton skeleton, JointType joint, JointType[] connectedJoints)
        {
            // All the joints must betracked. There must be two connected joints.
            return connectedJoints.Length == 2
                && JointTrackingState(skeleton, joint) == Microsoft.Kinect.JointTrackingState.Tracked
                && JointTrackingState(skeleton, connectedJoints[0]) == Microsoft.Kinect.JointTrackingState.Tracked
                && JointTrackingState(skeleton, connectedJoints[1]) == Microsoft.Kinect.JointTrackingState.Tracked;
        }
        private Microsoft.Kinect.JointTrackingState JointTrackingState(Microsoft.Kinect.Skeleton skeleton, JointType jointType)
        {
            Microsoft.Kinect.JointType kinectJointType = JointUtilities.TranslateJointType(jointType);
            return skeleton.Joints[kinectJointType].TrackingState;
        }

        private Vertex3[] GetJointPositions(Microsoft.Kinect.Skeleton skeleton, JointType[] jointTypes)
        {
            Vertex3[] jointPositions = new Vertex3[jointTypes.Length];
            for (int index = 0; index < jointTypes.Length; index++)
            {
                jointPositions[index] = GetJointPosition(skeleton, jointTypes[index]);
            }

            return jointPositions;
        }
        private Vertex3 GetJointPosition(Microsoft.Kinect.Skeleton skeleton, JointType jointType)
        {
            Microsoft.Kinect.JointType kinectJointType = JointUtilities.TranslateJointType(jointType);
            Microsoft.Kinect.SkeletonPoint kinectPoint = skeleton.Joints[kinectJointType].Position;
            Vertex3 position = GetPosition(kinectPoint);

            return position;
        }
        private Vertex3 GetPosition(Microsoft.Kinect.SkeletonPoint kinectPoint)
        {
            return new Vertex3(kinectPoint.X, kinectPoint.Y, kinectPoint.Z);
        }

        private void OnJointTrackingCaptured(MovingJoint joint, DateTime timeStamp)
        {
            if (JointTrackingCaptured != null)
            {
                JointTrackingCaptured(joint, timeStamp);
            }
        }

        #endregion
    }
}
