using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectUtilities
{
    public interface ISkeletonCapturingFunction
    {
        #region Properties

        object Lock { get; }
        bool LongOperation { get; }
        SkeletonCapturingFunctionPriority Priority { get; }

        #endregion

        #region Methods

        void Execute(object data);

        #endregion
    }
}
