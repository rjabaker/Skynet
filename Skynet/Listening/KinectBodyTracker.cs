using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectUtilities;
using KinectUtilities.JointTracking;
using ArduinoUtilities;
using ToolBox.Functions;

namespace Skynet
{
    public class KinectBodyTracker : IBodyTracker
    {
        #region Constants

        private const int framesFrequencyFilter = 15;

        #endregion

        #region Private Variables

        private SkeletonController skeletonController;
        private ArduinoSerialPort arduinoSerialPort;
        private Joints joints;
        private PinMapping emergencyStop;

        private MovingJoint previousMovingJoint;
        private DateTime previousTimeStamp;
        private int frameCount;

        #endregion

        #region Constructors

        public KinectBodyTracker(SmartKinectSensor smartSensor, ArduinoSerialPort arduinoSerialPort)
        {
            this.skeletonController = smartSensor.SkeletonController;
            this.arduinoSerialPort = arduinoSerialPort;
            this.joints = new Joints();
            this.previousTimeStamp = DateTime.MinValue;
            this.frameCount = 0;

            CreateEmergencyStop();
            LoadJointProfiles();
            ConnectSkeletonController();
        }

        #endregion

        #region Public Methods

        public void LoadJointProfiles()
        {
            // RBakerFlag -> TEST CODE!
            PinMapping cw = new PinMapping(9, "clockwise");
            PinMapping ccw = new PinMapping(10, "anti-clockwise");
            arduinoSerialPort.ComponentMappings.Add(cw);
            arduinoSerialPort.ComponentMappings.Add(ccw);

            cw.SetPinMode(SetPinModeStateCodes.OutputStateCode);
            ccw.SetPinMode(SetPinModeStateCodes.OutputStateCode);

            Joint joint = new Joint(cw, ccw, JointType.ElbowRight);
            joints.Add(joint);
        }
        public void CreateEmergencyStop()
        {
            // RBakerFlag -> This should be in a "reserved pins" settings file.
            // Or even a pin profile.
            emergencyStop = new PinMapping(8);
            arduinoSerialPort.ComponentMappings.Add(emergencyStop);
            emergencyStop.SetPinMode(SetPinModeStateCodes.OutputStateCode);
            emergencyStop.SetPin(true);
        }

        #endregion

        #region Private Methods

        private void ConnectSkeletonController()
        {
            JointController jointController = new JointController();
            skeletonController.AddFunction(jointController);
            jointController.JointTrackingCaptured += jointController_JointTrackingCaptured;
        }

        private void AlertJoints(MovingJoint joint, DateTime timeStamp)
        {
            IJointFrameParameter parameter = GetJointFrameParameter(joint, timeStamp);
            previousMovingJoint = joint;
            previousTimeStamp = timeStamp;
            
            Joints jointsToAlert = joints.GetJointsOfType(joint.JointType);
            foreach (Joint jointToAlert in jointsToAlert)
            {
                jointToAlert.JointTrackingFrameCaptured(parameter);
            }
        }

        private IJointFrameParameter GetJointFrameParameter(MovingJoint joint, DateTime timeStamp)
        {
            // RBakerFlag -> For now, default to this.
            TimeSpan timeSpan = GetTimeSpanSinceLastCapture(timeStamp);
            // AngularVelocityJointFrameParameter parameter = new AngularVelocityJointFrameParameter(joint, timeSpan);
            AngularDisplacementJointFrameParameter parameter = new AngularDisplacementJointFrameParameter(joint, previousMovingJoint, timeSpan);
            return parameter;
        }
        private TimeSpan GetTimeSpanSinceLastCapture(DateTime timeStamp)
        {
            TimeSpan timeSpan;

            if (previousTimeStamp.Equals(DateTime.MinValue))
            {
                timeSpan = TimeSpan.Zero;
            }
            else
            {
                timeSpan = TimeSpan.FromMilliseconds(DateTimeUtilities.DifferenceInMilliseconds(previousTimeStamp, timeStamp));
            }

            return timeSpan;
        }

        #endregion

        #region Event Handlers

        private void jointController_JointTrackingCaptured(MovingJoint joint, DateTime timeStamp)
        {
            if (frameCount % framesFrequencyFilter == 0)
            {
                frameCount = 1;
                AlertJoints(joint, timeStamp);
            }
            else
            {
                frameCount += 1;
            }
        }

        #endregion
    }
}
