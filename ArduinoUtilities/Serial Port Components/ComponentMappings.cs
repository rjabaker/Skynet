using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoUtilities
{
    public class ComponentMappings : IList<IComponentMapping>
    {
        #region Public Events

        public event SerialPortUtilities.ResponsePackageRecievedEventHandler ResponseEvent;

        #endregion

        #region Private Variables

        private List<IComponentMapping> componentMappings;
        private SerialPortUtilities.SetPinEventHandler setPinEventHandler;
        private SerialPortUtilities.ToggleListeningForResponsePackageEventHandler toggleListeningForResponsePackageEventHandler;

        #endregion

        #region Constructors

        public ComponentMappings(SerialPortUtilities.SetPinEventHandler setPinEventHandler, SerialPortUtilities.ToggleListeningForResponsePackageEventHandler toggleListeningForResponsePackageEventHandler)
        {
            this.componentMappings = new List<IComponentMapping>();
            this.setPinEventHandler = setPinEventHandler;
            this.toggleListeningForResponsePackageEventHandler = toggleListeningForResponsePackageEventHandler;
        }

        #endregion

        #region Properties

        public IComponentMapping this[int index]
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

        public SerialPortUtilities.SetPinEventHandler SetPinEventHandler
        {
            get
            {
                return setPinEventHandler;
            }
            set
            {
                setPinEventHandler = value;
                foreach (IComponentMapping mapping in componentMappings)
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
                foreach (IComponentMapping mapping in componentMappings)
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

        public int IndexOf(IComponentMapping item)
        {
            return componentMappings.IndexOf(item);
        }

        public void Insert(int index, IComponentMapping item)
        {
            componentMappings.Insert(index, item);
            item.SetPinEventHandler = setPinEventHandler;
            item.ToggleListeningForResponsePackageEventHandler = toggleListeningForResponsePackageEventHandler;
        }

        public void RemoveAt(int index)
        {
            componentMappings.RemoveAt(index);
        }

        public void Add(IComponentMapping item)
        {
            componentMappings.Add(item);
            item.SetPinEventHandler = setPinEventHandler;
            item.ToggleListeningForResponsePackageEventHandler = toggleListeningForResponsePackageEventHandler;
        }

        public void Clear()
        {
            componentMappings.Clear();
        }

        public bool Contains(IComponentMapping item)
        {
            return componentMappings.Contains(item);
        }

        public void CopyTo(IComponentMapping[] array, int arrayIndex)
        {
            componentMappings.CopyTo(array, arrayIndex);
        }

        public bool Remove(IComponentMapping item)
        {
            return componentMappings.Remove(item);
        }

        public IEnumerator<IComponentMapping> GetEnumerator()
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
