using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectUtilities;

namespace KinectUtilities.Gestures
{
    public interface IGesture
    {
        #region Properties

        string GestureName { get; set; }
        int GestureID { get; set; }

        #endregion
    }
}
