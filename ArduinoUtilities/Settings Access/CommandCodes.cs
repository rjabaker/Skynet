using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoUtilities
{
    public static class CommandCodes
    {
        public static int DigitalPinWriteCommandCode
        {
            get
            {
                return Settings.CommandCodes.Default.DigitalPinWriteCommandCode;
            }
        }

        public static int SettingCommandCode
        {
            get
            {
                return Settings.CommandCodes.Default.SettingCommandCode;
            }
        }

        public static int AnalogPinWriteCommandCode
        {
            get
            {
                return Settings.CommandCodes.Default.AnalogPinWriteCommandCode;
            }
        }

        public static int SetPinModeCommandCode
        {
            get
            {
                return Settings.CommandCodes.Default.SetPinModeCommandCode;
            }
        }
    }
}
