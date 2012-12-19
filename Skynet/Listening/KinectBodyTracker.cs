using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectUtilities;
using KinectUtilities.JointTracking;

namespace Skynet
{
    public class KinectBodyTracker : IBodyTracker
    {
        #region Private Variables

        private SkeletonController skeletonController;

        #endregion

        #region Constructors

        public KinectBodyTracker(SmartKinectSensor smartSensor)
        {
            this.skeletonController = new SkeletonController(smartSensor.Sensor);
            ConnectSkeletonController();
        }

        #endregion

        #region Public Methods

        public void LoadJointProfiles()
        {

        }

        #endregion

        #region Private Methods

        private void ConnectSkeletonController()
        {
            JointController jointController = new JointController();
            skeletonController.AddFunction(jointController);
            jointController.JointTrackingCaptured += jointController_JointTrackingCaptured;
        }

        #endregion

        #region Event Handlers

        private void jointController_JointTrackingCaptured(MovingJoint joint, DateTime timeStamp)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
