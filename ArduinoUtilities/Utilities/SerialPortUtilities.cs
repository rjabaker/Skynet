using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace ArduinoUtilities
{
    public static class SerialPortUtilities
    {
        #region Delegates

        public delegate void ToggleListeningForResponsePackageEventHandler(IPinMapping componentMapping, bool listen);
        public delegate void ResponsePackageRecievedEventHandler(byte responsePackage);

        #endregion
    }
}
