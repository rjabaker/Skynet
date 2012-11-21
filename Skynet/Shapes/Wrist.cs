using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skynet
{
    /// <summary>
    /// Contains the methods and parameters of a Skynet wrist.
    /// </summary>
    public class Wrist
    {
        #region Private Variables

        private SmartPinMapping smartPinMapping;

        #endregion

        #region Constructors

        public Wrist(PinMapping cwPinMapping, PinMapping ccwPinMapping)
        {
            this.smartPinMapping = new SmartPinMapping(cwPinMapping, ccwPinMapping, new SkynetUtilities.FeedbackRecievedEventHandler(PinFeedbackRecievedEventHandler));
        }

        #endregion

        #region Properties

        public SmartPinMapping SmartPinMapping
        {
            get
            {
                return smartPinMapping;
            }
            set
            {
                smartPinMapping = value;
            }
        }

        #endregion

        #region EventHandlers

        private void PinFeedbackRecievedEventHandler(PinFeedback feedback)
        {
            // RBakerFlag -> Do something.
        }

        #endregion
    }
}
