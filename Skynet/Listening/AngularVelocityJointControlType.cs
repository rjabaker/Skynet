using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skynet
{
    public class AngularVelocityJointControlType : IJointControlType
    {
        #region Public Methods

        public byte[] GetJointFrameCommandPackage(IJointFrameParameter iParameter)
        {
            AngularVelocityJointFrameParameter parameter = (AngularVelocityJointFrameParameter)iParameter;
            return null;
        }

        #endregion
    }
}
