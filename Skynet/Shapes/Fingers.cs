using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skynet
{
    /// <summary>
    /// A collection of Skynet Fingers.
    /// </summary>
    public class Fingers : IList<Finger>
    {
        #region Private Variables

        private List<Finger> fingers;
        private List<Finger> basicFingers;
        private List<SmartFinger> smartFingers;

        #endregion

        #region Constructors

        public Fingers()
        {
            this.fingers = new List<Finger>();
            this.basicFingers = new List<Finger>();
            this.smartFingers = new List<SmartFinger>();
        }

        #endregion

        #region Properties

        public Finger this[int index]
        {
            get
            {
                return fingers[index];
            }
            set
            {
                fingers[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return fingers.Count;
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

        public int IndexOf(Finger finger)
        {
            return fingers.IndexOf(finger);
        }

        public void Add(Finger finger)
        {
            fingers.Add(finger);

            if (finger.GetType() == typeof(SmartFinger))
            {
                smartFingers.Add((SmartFinger)finger);
            }
            else
            {
                basicFingers.Add(finger);
            }
        }

        public void Insert(int index, Finger finger)
        {
            fingers.Insert(index, finger);

            if (finger.GetType() == typeof(SmartFinger))
            {
                smartFingers.Add((SmartFinger)finger);
            }
            else
            {
                basicFingers.Add(finger);
            }
        }

        public void RemoveAt(int index)
        {
            Finger finger = this[index];

            if (finger.GetType() == typeof(SmartFinger))
            {
                smartFingers.Remove((SmartFinger)finger);
            }
            else
            {
                basicFingers.Remove(finger);
            }
        }

        public void Clear()
        {
            fingers.Clear();
            basicFingers.Clear();
            smartFingers.Clear();
        }

        public bool Contains(Finger item)
        {
            return fingers.Contains(item);
        }

        public void CopyTo(Finger[] array, int arrayIndex)
        {
            fingers.CopyTo(array, arrayIndex);
        }

        public bool Remove(Finger item)
        {
            return fingers.Remove(item);
        }

        public IEnumerator<Finger> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
