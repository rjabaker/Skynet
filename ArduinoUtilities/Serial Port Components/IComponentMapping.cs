﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoUtilities
{
    public interface IComponentMapping
    {
        #region Properties

        int PinNumber { get; set; }
        string Description { get; set; }
        bool ListeningForResponsePackage { get; set; }

        SerialPortUtilities.SetPinEventHandler SetPinEventHandler { get; set; }
        SerialPortUtilities.ResponsePackageRecievedEventHandler ResponsePackageRecievedEventHandler { get; set; }
        SerialPortUtilities.ToggleListeningForResponsePackageEventHandler ToggleListeningForResponsePackageEventHandler { get; set; }

        #endregion

        #region Methods

        byte[] DigitalWriteCommandPackageCode(bool turnOn);
        byte[] AnalogWriteCommandPackageCode(int intensity);

        #endregion
    }
}
