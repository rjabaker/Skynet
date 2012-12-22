using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KinectUtilities.JointTracking;

namespace Skynet
{
    public class Joints : IList<Joint>
    {
        #region Private Variables

        private List<Joint> joints;

        #endregion

        #region Constructors

        public Joints()
        {
            this.joints = new List<Joint>();
        }

        #endregion

        #region Properties

        public Joint this[int index]
        {
            get
            {
                return joints[index];
            }
            set
            {
                joints[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return joints.Count;
            }

        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region Public Methods

        public Joints GetJointsOfType(JointType jointType)
        {
            Joints joints = new Joints();
            foreach (Joint joint in this)
            {
                if (joint.JointType == jointType)
                {
                    joints.Add(joint);
                }
            }

            return joints;
        }

        public int IndexOf(Joint joint)
        {
            return joints.IndexOf(joint);
        }

        public void Add(Joint joint)
        {
            joints.Add(joint);
        }

        public void Insert(int index, Joint joint)
        {
            joints.Insert(index, joint);
        }

        public void RemoveAt(int index)
        {
            joints.RemoveAt(index);
        }

        public void Clear()
        {
            joints.Clear();
        }

        public bool Contains(Joint item)
        {
            return joints.Contains(item);
        }

        public void CopyTo(Joint[] array, int arrayIndex)
        {
            joints.CopyTo(array, arrayIndex);
        }

        public bool Remove(Joint item)
        {
            return joints.Remove(item);
        }

        public IEnumerator<Joint> GetEnumerator()
        {
            return this.joints.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
