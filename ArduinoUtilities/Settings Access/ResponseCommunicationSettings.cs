using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoUtilities
{
    public static class ResponseCommunicationSettings
    {
        public static int StateCodeLength
        {
            get
            {
                return Settings.ResponseCommunicationSettings.Default.StateCodeLength;
            }
        }
    }
}
