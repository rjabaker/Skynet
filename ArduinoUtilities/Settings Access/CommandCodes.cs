using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoUtilities
{
    public static class CommandCodes
    {
        public static int PinCommand
        {
            get
            {
                return Settings.CommandCodes.Default.PinCommandCode;
            }
        }

        public static int SettingCommand
        {
            get
            {
                return Settings.CommandCodes.Default.SettingCommandCode;
            }
        }
    }
}
