using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectUtilities;

namespace KinectUtilities.Gestures
{
    public class GestureUtilities
    {
        #region Delegates

        public delegate void GestureCapturedEventHandler(IGesture gesture, DateTime timeStamp);

        #endregion
    }
}
