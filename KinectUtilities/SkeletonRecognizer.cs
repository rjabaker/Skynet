using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Kinect;

namespace KinectUtilities
{
    public class SkeletonRecognizer
    {
        #region Private Variables

        private Dictionary<int, Skeleton> trackedSkeletons;

        #endregion

        #region Constructors

        public SkeletonRecognizer()
        {
            trackedSkeletons = new Dictionary<int, Skeleton>();
        }

        #endregion

        #region Public Properties

        public Dictionary<int, Skeleton> TrackedSkeletons
        {
            get
            {
                return trackedSkeletons;
            }
            set
            {
                trackedSkeletons = value;
            }
        }

        #endregion

        #region Public Methods

        public List<Skeleton> Recognize(Skeleton[] skeletonData, int numberToRecognize)
        {
            if (skeletonData == null || skeletonData.Length == 0) return new List<Skeleton>();
            List<Skeleton> closestSkeletons = GetClosestSkeletons(skeletonData, numberToRecognize);

            return closestSkeletons;
        }

        public static List<Skeleton> GetClosestSkeletons(Skeleton[] skeletonData, int numberToRecognize)
        {
            List<Skeleton> closestSkeletons = new List<Skeleton>();

            Skeleton[] skeletonDataCopy = skeletonData;
            skeletonDataCopy.OrderBy(skeleton => skeleton.Position.Z);
            foreach (Skeleton skeleton in skeletonDataCopy)
            {
                if (skeleton.TrackingState != SkeletonTrackingState.NotTracked && closestSkeletons.Count < numberToRecognize)
                {
                    closestSkeletons.Add(skeleton);
                }
            }

            return closestSkeletons;
        }

        #endregion
    }
}
