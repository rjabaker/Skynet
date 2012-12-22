using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectUtilities.JointTracking;
using ArduinoUtilities;

namespace Skynet
{
    /// <summary>
    /// Contains the methods and parameters that define a rotating joint.
    /// </summary>
    public class Joint
    {
        #region Private Variables

        // A joint can rotate clockwise and counter-clockwise, so it will have two pin mappings.
        private SmartPinMapping smartPinMapping;
        private JointType jointType;

        #endregion

        #region Constructors

        public Joint(PinMapping cwPinMapping, PinMapping ccwPinMapping, JointType jointType)
        {
            this.smartPinMapping = new SmartPinMapping(cwPinMapping, ccwPinMapping, new SkynetUtilities.FeedbackRecievedEventHandler(PinFeedbackRecievedEventHandler));
            this.jointType = jointType;
        }

        #endregion

        #region Properties

        public SmartPinMapping SmartPinMapping
        {
            get
            {
                return smartPinMapping;
            }
            set
            {
                smartPinMapping = value;
            }
        }
        public JointType JointType
        {
            get
            {
                return jointType;
            }
        }

        #endregion

        #region Public Methods

        public void JointTrackingFrameCaptured(IJointFrameParameter parameter)
        {
            if (parameter is AngularVelocityJointFrameParameter)
            {
                double angle = Math.Abs(parameter.BendAngle);
                if (angle > Math.PI / 2)
                {
                    smartPinMapping.CCwPinMapping.SetPin(0);
                    smartPinMapping.CwPinMapping.SetPin(255);
                }
                else
                {
                    smartPinMapping.CwPinMapping.SetPin(0);
                    smartPinMapping.CCwPinMapping.SetPin(255);
                }
            }
            else if (parameter is AngularDisplacementJointFrameParameter)
            {
                // Assume 200 steps per rotation.
                // RBakerFlag -> TESTCODE!
                if (parameter.TimeSpan.Equals(TimeSpan.Zero)) return;
                int steps = Convert.ToInt32(200* (double)parameter.BendAngle / (2 * Math.PI));
                int rpm = Convert.ToInt32(((double)steps / (double)200) / ((double)parameter.TimeSpan.Milliseconds / (1000 * 60)));
                if (rpm > 60) rpm = 60;

                byte[] commandPackage = new byte[] { (byte)CommandCodes.SetStepper1CommandCode, (byte)rpm, (byte)steps };

                smartPinMapping.CwPinMapping.SetPinEventHandler(commandPackage);
            }
        }

        #endregion

        #region EventHandlers

        private void PinFeedbackRecievedEventHandler(PinFeedback feedback)
        {
            // RBakerFlag -> Do something.
        }

        #endregion
    }
}
