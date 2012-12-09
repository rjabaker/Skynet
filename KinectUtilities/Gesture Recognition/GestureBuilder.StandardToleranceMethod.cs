using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Kinect;

namespace KinectUtilities.Gestures
{
    public partial class GestureBuilder
    {
        private class StandardToleranceMethod : IGestureBuilderMethod
        {
            #region Private Variables

            private MovingGestureTree movingGestureTree;
            private GestureBuilderParameters parameters;

            private DateTime gestureStartDateTime;
            private TimeSpan captureTimeTolerance;
            private TimeSpan gestureDuration;
            private SkeletonRenderFrames framesCapture;

            #endregion

            #region Constructors

            public StandardToleranceMethod(GestureBuilderParameters parameters)
            {
                this.movingGestureTree = new MovingGestureTree();
                this.parameters = parameters;

                this.gestureStartDateTime = DateTime.MinValue;
                this.captureTimeTolerance = TimeSpan.Zero;
                this.gestureDuration = TimeSpan.Zero;
                this.framesCapture = new SkeletonRenderFrames();
            }

            #endregion

            #region Properties

            public MovingGestureTree MovingGestureTree
            {
                get
                {
                    return movingGestureTree;
                }
            }

            #endregion

            #region Public Methods

            public void Start()
            {
                SetUpRenderFramesForGestureBuilding();
                CalculateBuildParameters();
            }

            #endregion

            #region Private Variables

            private void SetUpRenderFramesForGestureBuilding()
            {
                if (parameters.SkeletonRenderFrames.FramesTimeStamps.Count > 0)
                {
                    parameters.SkeletonRenderFrames.FramesTimeStamps.Sort();
                }
            }

            private void BuildFrame(SkeletonRenderFrame frame)
            {
                // Converts the frame into a collection of GestureTrees and adds it to the MovingGestureTree.

                TimeSpan frameTime = TimeSpan.FromMilliseconds(DateTimeUtilities.DifferenceInMilliseconds(gestureStartDateTime, frame.TimeStamp));
                TimeSpan minDeltaTime = frameTime < captureTimeTolerance ? TimeSpan.Zero : frameTime.Subtract(captureTimeTolerance);
                TimeSpan maxDeltaTime = frameTime.Add(captureTimeTolerance);

                GestureTree headAndSpineTree = new GestureTree(
                    SkeletonMiningUtilities.GetJointCollection(frame.Skeleton, SkeletonMiningUtilities.SkeletonJointCollection.HeadAndSpine),
                    GestureStandardToleranceParameters.JointAngleTolerance, minDeltaTime, maxDeltaTime);
                movingGestureTree.GestureTrees.Add(headAndSpineTree);

                GestureTree rightArmTree = new GestureTree(
                    SkeletonMiningUtilities.GetJointCollection(frame.Skeleton, SkeletonMiningUtilities.SkeletonJointCollection.RightArm),
                    GestureStandardToleranceParameters.JointAngleTolerance, minDeltaTime, maxDeltaTime);
                movingGestureTree.GestureTrees.Add(rightArmTree);

                GestureTree leftArmTree = new GestureTree(
                    SkeletonMiningUtilities.GetJointCollection(frame.Skeleton, SkeletonMiningUtilities.SkeletonJointCollection.LeftArm),
                    GestureStandardToleranceParameters.JointAngleTolerance, minDeltaTime, maxDeltaTime);
                movingGestureTree.GestureTrees.Add(leftArmTree);

                GestureTree rightLegTree = new GestureTree(
                   SkeletonMiningUtilities.GetJointCollection(frame.Skeleton, SkeletonMiningUtilities.SkeletonJointCollection.RightLeg),
                   GestureStandardToleranceParameters.JointAngleTolerance, minDeltaTime, maxDeltaTime);
                movingGestureTree.GestureTrees.Add(rightLegTree);

                GestureTree leftLegTree = new GestureTree(
                   SkeletonMiningUtilities.GetJointCollection(frame.Skeleton, SkeletonMiningUtilities.SkeletonJointCollection.LeftLeg),
                   GestureStandardToleranceParameters.JointAngleTolerance, minDeltaTime, maxDeltaTime);
                movingGestureTree.GestureTrees.Add(leftLegTree);
            }

            private void CalculateBuildParameters()
            {
                CalculateGestureStartDateTime();
                CalculateGestureDuration();
                CalculateTotalFramesCapture();
                CalculateCaptureTimeTolerance();
            }
            private void CalculateGestureStartDateTime()
            {
                gestureStartDateTime = parameters.SkeletonRenderFrames.FramesTimeStamps.Count > 0 ?
                    parameters.SkeletonRenderFrames.FramesTimeStamps.First() : DateTime.MinValue;
            }
            private void CalculateCaptureTimeTolerance()
            {
                captureTimeTolerance = TimeSpan.FromMilliseconds(gestureDuration.Milliseconds / framesCapture.Count);
            }
            private void CalculateGestureDuration()
            {
                if (parameters.SkeletonRenderFrames.Count > 0)
                {
                    int interval = DateTimeUtilities.DifferenceInMilliseconds(parameters.SkeletonRenderFrames.FramesTimeStamps.First(), parameters.SkeletonRenderFrames.FramesTimeStamps.Last());
                    gestureDuration = TimeSpan.FromMilliseconds(interval);
                }
            }
            private void CalculateTotalFramesCapture()
            {
                // Sample the total frames capture. Build the working frames capture via the framesPerSecondCapture frequency.
                // Create a collection of frames existing in a one second interval (potentially longer). Sample the one
                // second collection and update the working frames capture.

                SkeletonRenderFrames oneSecondIntervalFrames = new SkeletonRenderFrames();

                TimeSpan oneSecond = TimeSpan.FromSeconds(1);
                TimeSpan intervalSpan = TimeSpan.Zero;
                TimeSpan deltaFrameSpan;

                foreach (DateTime timeStamp in parameters.SkeletonRenderFrames.FramesTimeStamps)
                {
                    if (intervalSpan.Equals(TimeSpan.Zero)) oneSecondIntervalFrames = new SkeletonRenderFrames();

                    oneSecondIntervalFrames.Add(timeStamp, parameters.SkeletonRenderFrames[timeStamp]);
                    deltaFrameSpan = TimeSpan.FromMilliseconds(DateTimeUtilities.DifferenceInMilliseconds(gestureStartDateTime, timeStamp));
                    intervalSpan.Add(deltaFrameSpan);

                    if (intervalSpan >= oneSecond)
                    {
                        UpdateFramesCapture(oneSecondIntervalFrames);
                        intervalSpan = TimeSpan.Zero;
                    }
                }
            }
            private void UpdateFramesCapture(SkeletonRenderFrames oneSecondIntervalFrames)
            {
                // Assumes the interval between frames is approximately constant. Adds an evenly spaced sample number of frames
                // to the working frames capture.

                double frameStep = oneSecondIntervalFrames.Count / GestureStandardToleranceParameters.FramesPerSecondCapture;

                if (frameStep <= 1)
                {
                    framesCapture.Add(oneSecondIntervalFrames);
                }
                else
                {
                    int count = 0;
                    foreach (DateTime timeStamp in parameters.SkeletonRenderFrames.FramesTimeStamps)
                    {
                        count += 1;
                        if (count > frameStep)
                        {
                            framesCapture.Add(timeStamp, parameters.SkeletonRenderFrames[timeStamp]);
                            count = 0;
                        }
                    }
                }
            }

            #endregion
        }
    }
}
