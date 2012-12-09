using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace KinectUtilities
{
    [Serializable()]
    public class SkeletonRenderFrames
    {
        #region Private Variables

        private List<DateTime> framesTimeStamps;
        private Dictionary<DateTime, List<SkeletonRenderFrame>> skeletonFrames;
        private bool isReadOnly;

        #endregion

        #region Constructors

        public SkeletonRenderFrames()
        {
            this.framesTimeStamps = new List<DateTime>();
            this.skeletonFrames = new Dictionary<DateTime, List<SkeletonRenderFrame>>();
            this.isReadOnly = false;
        }
        public SkeletonRenderFrames(bool isReadOnly)
        {
            // RBakerFlag -> This constructor will be used for deserialization.
            this.skeletonFrames = new Dictionary<DateTime, List<SkeletonRenderFrame>>();
            this.isReadOnly = isReadOnly;
        }
        public SkeletonRenderFrames(SerializationInfo info, StreamingContext context)
        {
            this.isReadOnly = (bool)info.GetValue("isReadOnly", typeof(bool));
            this.skeletonFrames = (Dictionary<DateTime, List<SkeletonRenderFrame>>)info.GetValue("skeletonFrames", typeof(Dictionary<DateTime, List<SkeletonRenderFrame>>));
            BuildFrameTimeStampsCollection();
        }

        #endregion

        #region Properties

        public DateTime MostRecentFrameTime
        {
            get
            {
                return FramesTimeStamps.Count > 0 ? FramesTimeStamps.Last() : DateTime.MinValue;
            }
        }
        public DateTime OldestFrameTime
        {
            get
            {
                return FramesTimeStamps.Count > 0 ? FramesTimeStamps.First() : DateTime.MinValue;
            }
        }
        public List<DateTime> FramesTimeStamps
        {
            get
            {
                return framesTimeStamps;
            }
        }
        public DateTime this[int index]
        {
            get
            {
                return framesTimeStamps[index];
            }
            set
            {
                framesTimeStamps[index] = value;
            }
        }
        public List<SkeletonRenderFrame> this[DateTime key]
        {
            get
            {
                return skeletonFrames[key];
            }
            set
            {
                skeletonFrames[key] = value;
            }
        }
        public int Count
        {
            get { return framesTimeStamps.Count; }
        }
        public bool IsReadOnly
        {
            get { return isReadOnly; }
        }

        #endregion

        #region Public Methods

        public void Sort()
        {
            framesTimeStamps.Sort();
        }

        public void Add(DateTime key, List<SkeletonRenderFrame> value)
        {
            if (isReadOnly) return;

            if (!skeletonFrames.ContainsKey(key))
            {
                skeletonFrames.Add(key, value);
                framesTimeStamps.Add(key);
            }
            else
            {
                skeletonFrames[key] = value;
            }
        }

        public void Add(DateTime key, List<SkeletonRenderFrame> value, int index)
        {
            if (isReadOnly) return;

            if (!skeletonFrames.ContainsKey(key))
            {
                skeletonFrames.Add(key, value);
                framesTimeStamps.Insert(index, key);
            }
            else
            {
                skeletonFrames[key] = value;
            }
        }

        public bool ContainsKey(DateTime key)
        {
            return skeletonFrames.ContainsKey(key);
        }

        public bool Remove(DateTime key)
        {
            bool removed = false;

            if (!isReadOnly && skeletonFrames.ContainsKey(key))
            {
                removed = skeletonFrames.Remove(key);
                if (framesTimeStamps.Contains(key)) removed = framesTimeStamps.Remove(key);
            }
            else if (!isReadOnly && framesTimeStamps.Contains(key))
            {
                removed = framesTimeStamps.Remove(key);
            }

            return removed;
        }

        public bool TryGetValue(DateTime key, out List<SkeletonRenderFrame> value)
        {
            return skeletonFrames.TryGetValue(key, out value);
        }

        public void Clear()
        {
            if (isReadOnly) return;
            skeletonFrames.Clear();
            framesTimeStamps.Clear();
        }

        public bool Contains(DateTime dateTime)
        {
            return framesTimeStamps.Contains(dateTime) && skeletonFrames.ContainsKey(dateTime);
        }

        public bool Remove(KeyValuePair<DateTime, List<SkeletonRenderFrame>> item)
        {
            bool removed = false;

            if (!isReadOnly && skeletonFrames.ContainsKey(item.Key) && skeletonFrames[item.Key] == item.Value)
            {
                removed = skeletonFrames.Remove(item.Key);
                if (framesTimeStamps.Contains(item.Key)) removed = framesTimeStamps.Remove(item.Key);
            }

            return removed;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("isReadOnly", this.isReadOnly);
            info.AddValue("skeletonFrames", this.skeletonFrames);
        }

        #endregion

        #region Private Methods

        private void BuildFrameTimeStampsCollection()
        {
            if (skeletonFrames.Count > 0)
            {
                framesTimeStamps = skeletonFrames.Keys.ToList();
                framesTimeStamps.Sort();
            }
            else
            {
                framesTimeStamps = new List<DateTime>();
            }
        }

        #endregion
    }
}
