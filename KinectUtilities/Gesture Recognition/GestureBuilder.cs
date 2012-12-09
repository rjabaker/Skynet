using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectUtilities.Gestures
{
    public partial class GestureBuilder
    {
        #region Constructors

        public GestureBuilder()
        {

        }

        #endregion

        #region Public Methods

        public MovingGestureTree BuildMovingGestureTree(GestureBuilderParameters parameters)
        {
            IGestureBuilderMethod gestureBuilderMethod = GetGestureBuilderMethod(parameters);
            gestureBuilderMethod.Start();
            
            return gestureBuilderMethod.MovingGestureTree;
        }

        #endregion

        #region Private Variables

        private IGestureBuilderMethod GetGestureBuilderMethod(GestureBuilderParameters parameters)
        {
            IGestureBuilderMethod gestureBuilderMethod;

            switch (parameters.BuildStrategy)
            {
                case BuildStrategy.StandardTolerance:
                    gestureBuilderMethod = new StandardToleranceMethod(parameters);
                    break;
                default:
                    gestureBuilderMethod = new StandardToleranceMethod(parameters);
                    break;
            }

            return gestureBuilderMethod;
        }

        #endregion
    }
}
