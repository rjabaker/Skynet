using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArduinoUtilities;

namespace Skynet
{
    public class PinMapping : IComponentMapping
    {
        #region Events

        public event SkynetUtilities.FeedbackRecievedEventHandler FeedbackEvent;

        #endregion

        #region Private Variables

        private int pinNumber;
        private string description;

        private bool listeningForResponsePackage;

        private SerialPortUtilities.SetPinEventHandler setPinEventHandler;
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

        public SerialPortUtilities.SetPinEventHandler SetPinEventHandler
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
            // Build a command package. Listen for a response package. Set the pin.
            byte[] commandPackage = CommandPackageCode(turnOn);
            toggleListeningForResponePackageEventHandler((IComponentMapping)this, true);
            SetPinEventHandler(commandPackage);
        }

        public byte[] CommandPackageCode(bool turnOn)
        {
            byte commandID = (byte)CommandCodes.PinCommand;
            byte pinID = (byte)pinNumber;
            byte stateID = (byte)(turnOn ? 1 : 0);

            byte[] commandPackage = new byte[] { commandID, pinID, stateID };
            return commandPackage;
        }

        #endregion

        #region Event Handlers

        public void ResponsePackageRecieved(byte responsePackage)
        {
            bool belongsToPin = SerialPortUtilities.ResponsePackageBelongsToPin(pinNumber, responsePackage);

            if (belongsToPin)
            {
                // Stop listening for a response package. Read the pin state and send it out to the listeners.
                toggleListeningForResponePackageEventHandler((IComponentMapping)this, false);
                int state = SerialPortUtilities.ReadPinState(responsePackage);
                PinFeedback feedback = new PinFeedback(pinNumber, state);
                if (FeedbackEvent != null) FeedbackEvent(feedback);
            }
        }

        #endregion
    }
}
