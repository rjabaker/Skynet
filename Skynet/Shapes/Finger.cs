using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skynet
{
    /// <summary>
    /// Defines the methods and parameters of a typical Skynet finger.
    /// </summary>
    public class Finger
    {
        #region Private Variables

        private Joints joints;

        #endregion

        #region Constructors

        public Finger()
        {
            this.joints = new Joints();
        }

        #endregion
    }
}
