using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArduinoUtilities
{
    public static class ArduinoPinUtilities
    {
        #region Delegates

        public delegate void SetPinEventHandler(byte[] commandPackage);

        #endregion

        #region Public Methods

        public static string BinaryString(int number, int minimumLength)
        {
            string binary = Convert.ToString(number, 2).PadLeft(minimumLength, '0');
            return binary;
        }

        public static bool ResponsePackageBelongsToPin(int pinNumber, byte responsePackage)
        {
            bool belongsToPin;

            int responsePackagePin = (int)(responsePackage >> ResponseCommunicationSettings.StateCodeLength);
            belongsToPin = pinNumber == responsePackagePin;

            return belongsToPin;
        }

        public static int ReadPinState(byte responsePackage)
        {
            int state;

            // RBakerFlag -> We make the modulus term 2^x, where x is the number of state code bits.
            // Taking the modulus of the response package to this term, we can elminate all the bits
            // above the modulus, providing the state.
            int modulus = (int)Math.Pow(2, ResponseCommunicationSettings.StateCodeLength);
            state = (int)responsePackage % modulus;

            return state;
        }

        #endregion
    }
}
