using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skynet
{
    public interface IJointControlType
    {
        #region Methods

        byte[] GetJointFrameCommandPackage(IJointFrameParameter parameter);

        #endregion
    }
}
