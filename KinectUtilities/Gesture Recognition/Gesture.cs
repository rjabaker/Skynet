using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Kinect;

using KinectUtilities;

namespace KinectUtilities
{
    [XmlRoot("Gesture")]
    public class Gesture : IGesture
    {
        #region Private Variables

        private string gestureName;
        private int gestureID;

        #endregion

        #region Constructors

        public Gesture()
        {
            this.gestureName = string.Empty;
            this.gestureID = -1;
        }
        public Gesture(string gestureName, int gestureID)
        {
            this.gestureName = gestureName;
            this.gestureID = gestureID;
        }

        #endregion

        #region Properties

        [XmlElement("GestureName")]
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

        [XmlElement("GestureID")]
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
