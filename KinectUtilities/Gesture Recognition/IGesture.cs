using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectUtilities
{
    public interface IGesture
    {
        #region Properties

        string GestureName { get; set; }
        int GestureID { get; set; }

        #endregion
    }
}
