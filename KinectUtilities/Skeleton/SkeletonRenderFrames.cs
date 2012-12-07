using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace KinectUtilities
{
    [Serializable()]
    public class SkeletonRenderFrames : IDictionary<DateTime, List<SkeletonRenderFrame>>, ISerializable
    {
        #region Private Variables

        private Dictionary<DateTime, List<SkeletonRenderFrame>> skeletonFrames;
        private bool isReadOnly;

        #endregion

        #region Constructors

        public SkeletonRenderFrames()
        {
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
        }

        #endregion

        #region Properties

        public ICollection<DateTime> Keys
        {
            get { return skeletonFrames.Keys; }
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
            get { return skeletonFrames.Count; }
        }

        public bool IsReadOnly
        {
            get { return isReadOnly; }
        }

        public ICollection<List<SkeletonRenderFrame>> Values
        {
            get { return skeletonFrames.Values; }
        }

        #endregion

        #region Public Methods

        public void Add(DateTime key, List<SkeletonRenderFrame> value)
        {
            if (isReadOnly) return;

            if (!skeletonFrames.ContainsKey(key))
            {
                skeletonFrames.Add(key, value);
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
            }

            return removed;
        }

        public bool TryGetValue(DateTime key, out List<SkeletonRenderFrame> value)
        {
            return skeletonFrames.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<DateTime, List<SkeletonRenderFrame>> item)
        {
            if (isReadOnly) return;
            skeletonFrames.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            if (isReadOnly) return;
            skeletonFrames.Clear();
        }

        public bool Contains(KeyValuePair<DateTime, List<SkeletonRenderFrame>> item)
        {
            return skeletonFrames.ContainsKey(item.Key) && skeletonFrames[item.Key] == item.Value;
        }

        public void CopyTo(KeyValuePair<DateTime, List<SkeletonRenderFrame>>[] array, int arrayIndex)
        {
            KeyValuePair<DateTime, List<SkeletonRenderFrame>>[] frames = skeletonFrames.ToArray<KeyValuePair<DateTime, List<SkeletonRenderFrame>>>();
            frames.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<DateTime, List<SkeletonRenderFrame>> item)
        {
            bool removed = false;

            if (!isReadOnly && skeletonFrames.ContainsKey(item.Key) && skeletonFrames[item.Key] == item.Value)
            {
                removed = skeletonFrames.Remove(item.Key);
            }

            return removed;
        }

        public IEnumerator<KeyValuePair<DateTime, List<SkeletonRenderFrame>>> GetEnumerator()
        {
            return skeletonFrames.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("isReadOnly", this.isReadOnly);
            info.AddValue("skeletonFrames", this.skeletonFrames);
        }

        #endregion
    }
}
