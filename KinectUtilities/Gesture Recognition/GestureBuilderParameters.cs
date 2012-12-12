using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectUtilities.Gestures
{
    public class GestureBuilderParameters
    {
        #region Private Variables

        private IGesture gesture;
        private SkeletonRenderFrames skeletonRenderFrames;

        private DateTime gestureStartTime;
        private DateTime gestureEndTime;

        private GestureBuilder.BuildStrategy buildStrategy;

        #endregion

        #region Constructors

        public GestureBuilderParameters(IGesture gesture, SkeletonRenderFrames skeletonRenderFrames, DateTime gestureStartTime, DateTime gestureEndTime, GestureBuilder.BuildStrategy buildStrategy)
        {
            this.gesture = gesture;
            this.skeletonRenderFrames = skeletonRenderFrames;

            this.gestureStartTime = gestureStartTime;
            this.gestureEndTime = gestureEndTime;

            this.buildStrategy = buildStrategy;
        }

        #endregion

        #region Properties

        public IGesture Gesture
        {
            get
            {
                return gesture;
            }
        }
        public SkeletonRenderFrames SkeletonRenderFrames
        {
            get
            {
                return skeletonRenderFrames;
            }
        }
        public DateTime GestureStartTime
        {
            get
            {
                return gestureStartTime;
            }
        }
        public DateTime GestureEndTime
        {
            get
            {
                return gestureEndTime;
            }
        }
        public GestureBuilder.BuildStrategy BuildStrategy
        {
            get
            {
                return buildStrategy;
            }
        }

        #endregion
    }
}
