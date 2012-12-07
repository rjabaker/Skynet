using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Kinect;

using KinectUtilities;

namespace KinectUtilities
{
    public class Gesture : IGesture
    {
        #region Private Variables

        private string gestureName;
        private int gestureID;

        #endregion

        #region Properties

        public string GestureName
        {
            get
            {
                return gestureName;
            }
            set
            {
                gestureName = value;
            }
        }
        public int GestureID
        {
            get
            {
                return gestureID;
            }
            set
            {
                gestureID = value;
            }
        }

        #endregion
    }
}
