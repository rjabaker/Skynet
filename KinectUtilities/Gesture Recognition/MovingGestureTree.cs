using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Kinect;

using KinectUtilities;
using ToolBox.Functions;

namespace KinectUtilities.Gestures
{
    [XmlRoot("MovingGestureTree")]
    public class MovingGestureTree
    {
        #region Events

        public event GestureUtilities.GestureCapturedEventHandler GestureCaptured;

        #endregion

        #region Private Variables

        private Gesture gesture;

        private List<GestureTree> gestureTrees;
        private List<GestureTree> activeGestureTrees;

        private TimeSpan currentExecutionTime;
        private TimeSpan minimumGestureDuration;
        private TimeSpan maximumGestureDuration;
        private DateTime currentExecutionDateTime;

        #endregion

        #region Constructors

        public MovingGestureTree()
        {
            this.gesture = new Gesture();

            this.gestureTrees = new List<GestureTree>();
            this.activeGestureTrees = new List<GestureTree>();

            this.currentExecutionTime = TimeSpan.Zero;
            this.minimumGestureDuration = TimeSpan.Zero;
            this.maximumGestureDuration = TimeSpan.Zero;
            this.currentExecutionDateTime = DateTime.MinValue;
        }

        #endregion

        #region Properties

        [XmlElement("Gesture")]
        public Gesture Gesture
        {
            get
            {
                return gesture;
            }
            set
            {
                gesture = value;
            }
        }

        [XmlArray("GestureTrees"),
        XmlArrayItem("GestureTree", typeof(GestureTree))]
        public List<GestureTree> GestureTrees
        {
            get
            {
                return gestureTrees;
            }
            set
            {
                gestureTrees = value;
            }
        }

        #endregion

        #region Public Methods

        public void CalculateRuntimeParameters()
        {
            TimeSpan maxMinimumDuration = TimeSpan.MinValue;
            TimeSpan maxMaximumDuration = TimeSpan.MinValue;
            foreach (GestureTree tree in gestureTrees)
            {
                if (tree.MinDeltaTime > maxMinimumDuration) maxMinimumDuration = tree.MinDeltaTime;
                if (tree.MaxDeltaTime > maxMaximumDuration) maxMaximumDuration = tree.MaxDeltaTime;
            }

            minimumGestureDuration = maxMinimumDuration;
            maximumGestureDuration = maxMaximumDuration;
        }

        public void ProcessSkeletonForGesture(Skeleton skeleton, DateTime timeStamp)
        {
            UpdateTiming(timeStamp);
            CheckActiveGesturesForExecution(skeleton);
            EvaluateExecution();
        }

        #endregion

        #region Private Methods

        private void UpdateTiming(DateTime timeStamp)
        {
            currentExecutionTime = currentExecutionDateTime == DateTime.MinValue ? TimeSpan.Zero :
                TimeSpan.FromMilliseconds(DateTimeUtilities.DifferenceInMilliseconds(currentExecutionDateTime, timeStamp));
            if (currentExecutionTime > maximumGestureDuration)
            {
                ResetExecutionParameters();
            }

            currentExecutionDateTime = timeStamp;
        }
        private void CheckActiveGesturesForExecution(Skeleton skeleton)
        {
            foreach (GestureTree gestureTree in activeGestureTrees)
            {
                gestureTree.SearchForGestureExecution(skeleton, currentExecutionTime);
            }
        }

        private void EvaluateExecution()
        {
            if (activeGestureTrees.Count > 0)
            {
                if (FailedExecution())
                {
                    ResetExecutionParameters();
                }
                else if (PassedAllExecutions())
                {
                    GestureCaptured(gesture, currentExecutionDateTime);
                    ResetExecutionParameters();
                }
                else
                {
                    SetActiveGestureTrees();
                }
            }
            else
            {
                ResetExecutionParameters();
            }
        }
        private void ResetExecutionParameters()
        {
            currentExecutionTime = TimeSpan.Zero;
            ResetGestureTrees();
            SetActiveGestureTrees();
        }
        private void ResetGestureTrees()
        {
            // Set all gesture trees to unexecuted.
            foreach (GestureTree gestureTree in gestureTrees)
            {
                gestureTree.Executed = false;
            }
        }
        private bool FailedExecution()
        {
            // Returns true if even one tree in the activeGestureTrees failed execution. Returns false
            // if there are no active gesture trees, since no active trees failed.

            foreach (GestureTree gestureTree in activeGestureTrees)
            {
                if (gestureTree.FailedExecution(currentExecutionTime))
                {
                    return true;
                }
            }

            return false;
        }
        private bool PassedAllExecutions()
        {
            // Assumes this gestureTrees collection is sorted. Assumes the activeGestureTrees didn't fail execution.
            // If the current activeGesture tree is completely executed and it contains the gesture tree with the 
            // latest delta time (meaning it must be exeucted last), there are no other unexecuted gestures in the 
            // moving gesture tree (since the latest gesture has been picked up).

            bool activeGestureTreesAllExecuted = true;
            bool latestGestureTreeInActiveGestureTrees = false;
            foreach (GestureTree gestureTree in activeGestureTrees)
            {
                activeGestureTreesAllExecuted &= gestureTree.Executed;
                latestGestureTreeInActiveGestureTrees = gestureTree.MinDeltaTime.CompareTo(minimumGestureDuration) == 0;
            }

            return activeGestureTreesAllExecuted && latestGestureTreeInActiveGestureTrees;
        }
        private void SetActiveGestureTrees()
        {
            // Assumes this gestureTrees collection is sorted.
            // RBakerFlag -> Should sort active gestures so that gesture trees are ORDERED. That is, frame A should occur 
            // before frame B, and its delta time span should be relative to that.

            activeGestureTrees = new List<GestureTree>();
            foreach (GestureTree gestureTree in gestureTrees)
            {
                if (gestureTree.StillActive(currentExecutionTime))
                {
                    activeGestureTrees.Add(gestureTree);
                }
                else if (gestureTree.NotActiveYet(currentExecutionTime))
                {
                    // Since tree is sorted, if this gestureTree item isn't active yet, none
                    // after it will be either, so we can break here.
                    break;
                }
            }
        }

        #endregion
    }
}
