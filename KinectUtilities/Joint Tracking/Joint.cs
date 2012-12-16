using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ToolBox.Math;

namespace KinectUtilities.JointTracking
{
    public class Joint
    {
        #region Private Variables

        private JointType jointType;
        private Vertex3 position;

        #endregion

        #region Constructors

        public Joint(JointType jointType, Vertex3 position)
        {
            this.jointType = jointType;
            this.position = position;
        }

        #endregion

        #region Properties

        public JointType JointType
        {
            get
            {
                return jointType;
            }
        }
        public Vertex3 Position
        {
            get
            {
                return position;
            }
        }

        #endregion
    }
}
