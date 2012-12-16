using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Kinect;

using KinectUtilities;

namespace KinectUtilities.Gestures
{
    public class GestureController : ISkeletonCapturingFunction
    {
        #region Events

        public event KinectEventUtilities.GestureCapturedEventHandler GestureCaptured;

        #endregion

        #region Private Variables

        private readonly object thisLock = new object();
        private SkeletonCapturingFunctionPriority priority;
        private bool longOperation;

        private List<MovingGestureTree> movingGestureTrees;
        private GestureBuilder gestureBuilder;

        #endregion

        #region Constructors

        public GestureController()
        {
            this.movingGestureTrees = new List<MovingGestureTree>();
            this.gestureBuilder = new GestureBuilder();
            this.longOperation = true;
            this.priority = SkeletonCapturingFunctionPriority.Normal;
        }

        #endregion

        #region Properties

        public object Lock
        {
            get
            {
                return thisLock;
            }
        }
        public SkeletonCapturingFunctionPriority Priority
        {
            get
            {
                return priority;
            }
        }
        public bool LongOperation
        {
            get
            {
                return longOperation;
            }
        }

        #endregion

        #region Public Methods

        public void Execute(object data)
        {
            Execute((SkeletonCaptureData)data);
        }
        public void Execute(SkeletonCaptureData data)
        {
            foreach (Skeleton skeleton in data.Skeletons)
            {
                ProcessSkeletonForGesture(skeleton, data.TimeStamp);
            }
        }

        public void AddMovingGestureTree(MovingGestureTree movingGestureTree)
        {
            movingGestureTree.CalculateRuntimeParameters();
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

        private void ProcessSkeletonForGesture(Skeleton skeleton, DateTime timeStamp)
        {
            foreach (MovingGestureTree movingGestureTree in movingGestureTrees)
            {
                movingGestureTree.ProcessSkeletonForGesture(skeleton, timeStamp);
            }
        }

        #endregion

        #region Event Handlers

        private void movingGestureTree_GestureCaptured(IGesture gesture, DateTime timeStamp)
        {
            GestureCaptured(gesture, timeStamp);
        }

        #endregion
    }
}
