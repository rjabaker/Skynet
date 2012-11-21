using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skynet
{
    public class PinFeedback
    {
        #region Constructors

        public PinFeedback(int pinNumber, int pinState)
        {
            PinNumber = pinNumber;
            PinState = pinState;
            FeedbackRecievedTime = DateTime.Now;
        }

        #endregion

        #region Properties

        public int PinNumber { get; set; }
        public int PinState { get; set; }
        public DateTime FeedbackRecievedTime { get; set; }

        #endregion
    }
}
