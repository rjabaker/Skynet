using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skynet
{
    /// <summary>
    /// Most shapes require two pins to control a motor, implementing clockwise and counter-clockwise rotation.
    /// This class contains two pin mappings to ease rotation control.
    /// </summary>
    public class SmartPinMapping
    {
        #region Private Variables

        private PinMapping cwPinMapping;
        private PinMapping ccwPinMapping;

        #endregion

        #region Constructors

        public SmartPinMapping(PinMapping cwPinMapping, PinMapping ccwPinMapping, SkynetUtilities.FeedbackRecievedEventHandler pinFeedbackEventHandler)
        {
            this.cwPinMapping = cwPinMapping;
            this.ccwPinMapping = ccwPinMapping;

            this.cwPinMapping.FeedbackEvent += pinFeedbackEventHandler;
            this.ccwPinMapping.FeedbackEvent += pinFeedbackEventHandler;
        }

        #endregion

        #region Properties

        public PinMapping CwPinMapping
        {
            get
            {
                return cwPinMapping;
            }
            set
            {
                cwPinMapping = value;
            }
        }

        public PinMapping CCwPinMapping
        {
            get
            {
                return ccwPinMapping;
            }
            set
            {
                ccwPinMapping = value;
            }
        }

        #endregion
    }
}
