using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoUtilities
{
    public static class CommandCommunicationSettings
    {
        public static int BaudRate
        {
            get
            {
                return Settings.CommandCommunicationSettings.Default.BaudRate;
            }
        }
    }
}
