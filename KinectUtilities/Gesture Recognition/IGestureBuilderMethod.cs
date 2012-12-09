using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectUtilities.Gestures
{
    public interface IGestureBuilderMethod
    {
        #region Properties

        MovingGestureTree MovingGestureTree { get; }

        #endregion

        #region Methods

        void Start();

        #endregion
    }
}
