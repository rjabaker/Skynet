using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoUtilities
{
    public static class SetPinModeStateCodes
    {
        public static int OutputStateCode
        {
            get
            {
                return Settings.SetPinModeStateCodes.Default.OutputStateCode;
            }
        }

        public static int InputStateCode
        {
            get
            {
                return Settings.SetPinModeStateCodes.Default.InputStateCode;
            }
        }

        public static int InputPullupStateCode
        {
            get
            {
                return Settings.SetPinModeStateCodes.Default.InputPullupStateCode;
            }
        }
    }
}
