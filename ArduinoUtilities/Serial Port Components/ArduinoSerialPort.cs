using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace ArduinoUtilities
{
    public class ArduinoSerialPort
    {
        #region Private Variables

        private SerialPort serialPort;
        private string portName;
        private int baudRate;

        private ComponentMappings componentMappings;

        #endregion

        #region Constructors

        public ArduinoSerialPort(string portName, int baudRate)
        {
            this.portName = portName;
            this.baudRate = baudRate;
            this.serialPort = new SerialPort(this.portName, this.baudRate);
            this.serialPort.DataReceived += new SerialDataReceivedEventHandler(DataRecievedEventHandler);
            // RBakerFlag -> Set up a handshake?

            this.componentMappings = new ComponentMappings(SetPinEventHandler, ToggleListeningForResponsePackageEventHandler);
        }

        #endregion

        #region Properties

        public ComponentMappings ComponentMappings
        {
            get
            {
                return componentMappings;
            }
        }

        #endregion

        #region Public Methods

        public void Write(int toWrite)
        {
            byte[] bytesToWrite = { (byte)toWrite };
            serialPort.Write(bytesToWrite, 0, 1);
        }

        public void Open()
        {
            serialPort.Open();
        }

        #endregion

        #region Event Handlers

        private void SetPinEventHandler(byte[] commandPackage)
        {
            // RBakerFlag -> Need to log the error if the serial port is closed.
            if (serialPort.IsOpen) serialPort.Write(commandPackage, 0, commandPackage.Length);
        }

        private void ToggleListeningForResponsePackageEventHandler(IComponentMapping componentMapping, bool listen)
        {
            // Toggle listening for a response package. Only allow each component mapping to have one listener.

            if (listen && !componentMapping.ListeningForResponsePackage)
            {
                componentMappings.ResponseEvent += componentMapping.ResponsePackageRecievedEventHandler;
                componentMapping.ListeningForResponsePackage = true;
            }
            else if (!listen && componentMapping.ListeningForResponsePackage)
            {
                componentMappings.ResponseEvent -= componentMapping.ResponsePackageRecievedEventHandler;
                componentMapping.ListeningForResponsePackage = false;
            }
        }

        private void DataRecievedEventHandler(object sender, SerialDataReceivedEventArgs e)
        {
            // Read the response package. Fire a response package recieved event.
            byte responsePackage = (byte)serialPort.ReadByte();
            componentMappings.FireRepsonseEvent(responsePackage);
        }

        #endregion
    }
}
