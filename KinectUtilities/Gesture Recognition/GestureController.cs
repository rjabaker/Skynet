using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Kinect;

using KinectUtilities;

namespace KinectUtilities.Gestures
{
    public class GestureController
    {
        #region Events

        public event KinectEventUtilities.GestureCapturedEventHandler GestureCaptured;

        #endregion

        #region Private Variables

        private List<MovingGestureTree> movingGestureTrees;

        #endregion

        #region Constructors

        public GestureController()
        {
            this.movingGestureTrees = new List<MovingGestureTree>();
        }

        #endregion

        #region Public Methods

        public void ProcessSkeletonForGesture(Skeleton skeleton, DateTime timeStamp)
        {
            foreach (MovingGestureTree movingGestureTree in movingGestureTrees)
            {
                movingGestureTree.ProcessSkeletonForGesture(skeleton, timeStamp);
            }
        }

        #endregion

        #region Private Methods

        private void movingGestureTree_GestureCaptured(IGesture gesture, DateTime timeStamp)
        {
            GestureCaptured(gesture, timeStamp);
        }

        #endregion
    }
}
