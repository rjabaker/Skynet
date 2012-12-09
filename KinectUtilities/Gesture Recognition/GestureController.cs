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
        private GestureBuilder gestureBuilder;

        #endregion

        #region Constructors

        public GestureController()
        {
            this.movingGestureTrees = new List<MovingGestureTree>();
            this.gestureBuilder = new GestureBuilder();
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

        public void AddMovingGestureTree(MovingGestureTree movingGestureTree)
        {
            movingGestureTrees.Add(movingGestureTree);
            movingGestureTree.GestureCaptured += new GestureUtilities.GestureCapturedEventHandler(movingGestureTree_GestureCaptured);
        }
        public MovingGestureTree BuildMovingGestureTree(GestureBuilderParameters gestureBuilderParameters, bool addWhenBuilt)
        {
            MovingGestureTree movingGestureTree = gestureBuilder.BuildMovingGestureTree(gestureBuilderParameters);
            if (addWhenBuilt) AddMovingGestureTree(movingGestureTree);

            return movingGestureTree;
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
