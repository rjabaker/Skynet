using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArduinoUtilities;

namespace Skynet
{
    public class PinMapping : IPinMapping
    {
        #region Events

        public event SkynetUtilities.FeedbackRecievedEventHandler FeedbackEvent;

        #endregion

        #region Private Variables

        private int pinNumber;
        private string description;

        private bool listeningForResponsePackage;

        private ArduinoPinUtilities.SetPinEventHandler setPinEventHandler;
        private SerialPortUtilities.ResponsePackageRecievedEventHandler responsePackageRecievedEventHandler;
        private SerialPortUtilities.ToggleListeningForResponsePackageEventHandler toggleListeningForResponePackageEventHandler;

        #endregion

        #region Constructors

        public PinMapping(int pinNumber)
        {
            this.pinNumber = pinNumber;
            this.description = string.Empty;

            this.responsePackageRecievedEventHandler = new SerialPortUtilities.ResponsePackageRecievedEventHandler(ResponsePackageRecieved);
            this.listeningForResponsePackage = false;
        }

        public PinMapping(int pinNumber, string pinDescription)
        {
            this.pinNumber = pinNumber;
            this.description = pinDescription;
        }

        #endregion

        #region Properties

        public int PinNumber
        {
            get
            {
                return pinNumber;
            }
            set
            {
                pinNumber = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public bool ListeningForResponsePackage
        {
            get
            {
                return listeningForResponsePackage;
            }
            set
            {
                listeningForResponsePackage = value;
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
            }
        }

        public SerialPortUtilities.ResponsePackageRecievedEventHandler ResponsePackageRecievedEventHandler
        {
            get
            {
                return responsePackageRecievedEventHandler;
            }
            set
            {
                responsePackageRecievedEventHandler = value;
            }
        }

        public SerialPortUtilities.ToggleListeningForResponsePackageEventHandler ToggleListeningForResponsePackageEventHandler
        {
            get
            {
                return toggleListeningForResponePackageEventHandler;
            }
            set
            {
                toggleListeningForResponePackageEventHandler = value;
            }
        }

        #endregion

        #region Public Methods

        public void SetPin(bool turnOn)
        {
            // Build a digital command package. Listen for a response package. Set the pin.
            byte[] commandPackage = DigitalWriteCommandPackageCode(turnOn);
            toggleListeningForResponePackageEventHandler((IPinMapping)this, true);
            SetPinEventHandler(commandPackage);
        }

        public void SetPin(int intensity)
        {
            // Build an analog command package. Listen for a response package. Set the pin.
            byte[] commandPackage = AnalogWriteCommandPackageCode(intensity);
            toggleListeningForResponePackageEventHandler((IPinMapping)this, true);
            SetPinEventHandler(commandPackage);
        }

        public void SetPinMode(int pinMode)
        {
            byte[] commandPackage = SetPinModeCommandPackageCode(pinMode);
            toggleListeningForResponePackageEventHandler((IPinMapping)this, true);
            SetPinEventHandler(commandPackage);
        }

        public byte[] AnalogWriteCommandPackageCode(int intensity)
        {
            byte commandID = (byte)CommandCodes.AnalogPinWriteCommandCode;
            byte pinID = (byte)pinNumber;
            byte stateID = (byte)(intensity);

            byte[] commandPackage = new byte[] { commandID, pinID, stateID };
            return commandPackage;
        }

        public byte[] DigitalWriteCommandPackageCode(bool turnOn)
        {
            byte commandID = (byte)CommandCodes.DigitalPinWriteCommandCode;
            byte pinID = (byte)pinNumber;
            byte stateID = (byte)(turnOn ? 1 : 0);

            byte[] commandPackage = new byte[] { commandID, pinID, stateID };
            return commandPackage;
        }

        public byte[] SetPinModeCommandPackageCode(int pinMode)
        {
            byte commandID = (byte)CommandCodes.SetPinModeCommandCode;
            byte pinID = (byte)pinNumber;
            byte stateID = (byte)pinMode;

            byte[] commandPackage = new byte[] { commandID, pinID, stateID };
            return commandPackage;
        }

        #endregion

        #region Event Handlers

        public void ResponsePackageRecieved(byte responsePackage)
        {
            bool belongsToPin = ArduinoPinUtilities.ResponsePackageBelongsToPin(pinNumber, responsePackage);

            if (belongsToPin)
            {
                // Stop listening for a response package. Read the pin state and send it out to the listeners.
                toggleListeningForResponePackageEventHandler((IPinMapping)this, false);
                int state = ArduinoPinUtilities.ReadPinState(responsePackage);
                PinFeedback feedback = new PinFeedback(pinNumber, state);
                if (FeedbackEvent != null) FeedbackEvent(feedback);
            }
        }

        #endregion
    }
}
