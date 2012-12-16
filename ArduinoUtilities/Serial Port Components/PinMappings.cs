using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoUtilities
{
    public class PinMappings : IList<IPinMapping>
    {
        #region Public Events

        public event SerialPortUtilities.ResponsePackageRecievedEventHandler ResponseEvent;

        #endregion

        #region Private Variables

        private List<IPinMapping> componentMappings;
        private ArduinoPinUtilities.SetPinEventHandler setPinEventHandler;
        private SerialPortUtilities.ToggleListeningForResponsePackageEventHandler toggleListeningForResponsePackageEventHandler;

        #endregion

        #region Constructors

        public PinMappings(ArduinoPinUtilities.SetPinEventHandler setPinEventHandler, SerialPortUtilities.ToggleListeningForResponsePackageEventHandler toggleListeningForResponsePackageEventHandler)
        {
            this.componentMappings = new List<IPinMapping>();
            this.setPinEventHandler = setPinEventHandler;
            this.toggleListeningForResponsePackageEventHandler = toggleListeningForResponsePackageEventHandler;
        }

        #endregion

        #region Properties

        public IPinMapping this[int index]
        {
            get
            {
                return componentMappings[index];
            }
            set
            {
                componentMappings[index] = value;
            }
        }

        public ArduinoPinUtilities.SetPinEventHandler SetPinEventHandler
        {
            get
            {
                return setPinEventHandler;
            }
            set
            {
                setPinEventHandler = value;
                foreach (IPinMapping mapping in componentMappings)
                {
                    mapping.SetPinEventHandler = setPinEventHandler;
                }
            }
        }

        public SerialPortUtilities.ToggleListeningForResponsePackageEventHandler ToggleListeningForResponsePackageEventHandler
        {
            get
            {
                return toggleListeningForResponsePackageEventHandler;
            }
            set
            {
                toggleListeningForResponsePackageEventHandler = value;
                foreach (IPinMapping mapping in componentMappings)
                {
                    mapping.ToggleListeningForResponsePackageEventHandler = toggleListeningForResponsePackageEventHandler;
                }
            }
        }

        public int Count
        {
            get
            {
                return componentMappings.Count;
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

        public void FireRepsonseEvent(byte responsePackage)
        {
            if (ResponseEvent != null) ResponseEvent(responsePackage);
        }

        public int IndexOf(IPinMapping item)
        {
            return componentMappings.IndexOf(item);
        }

        public void Insert(int index, IPinMapping item)
        {
            componentMappings.Insert(index, item);
            item.SetPinEventHandler = setPinEventHandler;
            item.ToggleListeningForResponsePackageEventHandler = toggleListeningForResponsePackageEventHandler;
        }

        public void RemoveAt(int index)
        {
            componentMappings.RemoveAt(index);
        }

        public void Add(IPinMapping item)
        {
            componentMappings.Add(item);
            item.SetPinEventHandler = setPinEventHandler;
            item.ToggleListeningForResponsePackageEventHandler = toggleListeningForResponsePackageEventHandler;
        }

        public void Clear()
        {
            componentMappings.Clear();
        }

        public bool Contains(IPinMapping item)
        {
            return componentMappings.Contains(item);
        }

        public void CopyTo(IPinMapping[] array, int arrayIndex)
        {
            componentMappings.CopyTo(array, arrayIndex);
        }

        public bool Remove(IPinMapping item)
        {
            return componentMappings.Remove(item);
        }

        public IEnumerator<IPinMapping> GetEnumerator()
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
