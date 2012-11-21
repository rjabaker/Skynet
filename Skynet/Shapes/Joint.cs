using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skynet
{
    /// <summary>
    /// Contains the methods and parameters that define a rotating joint.
    /// </summary>
    public class Joint
    {
        #region Private Variables

        // A joint can rotate clockwise and counter-clockwise, so it will have two pin mappings.
        private SmartPinMapping smartPinMapping;

        #endregion

        #region Constructors

        public Joint(PinMapping cwPinMapping, PinMapping ccwPinMapping)
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
