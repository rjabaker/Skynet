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
        private TimeSpan gestureDuration;

        private GestureBuilder.BuildStrategy buildStrategy;

        #endregion

        #region Constructors

        public GestureBuilderParameters(IGesture gesture, SkeletonRenderFrames skeletonRenderFrames, DateTime gestureStartTime, TimeSpan gestureDuration, GestureBuilder.BuildStrategy buildStrategy)
        {
            this.gesture = gesture;
            this.skeletonRenderFrames = skeletonRenderFrames;

            this.gestureStartTime = gestureStartTime;
            this.gestureDuration = gestureDuration;

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
        public TimeSpan GestureDuration
        {
            get
            {
                return gestureDuration;
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
