using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skynet
{
    /// <summary>
    /// Defines the methods and parameters of a typical Skynet Hand.
    /// </summary>
    public class Hand : IHand
    {
        #region Private Variables

        private Wrist wrist;
        private Palm palm;
        private Fingers fingers;

        #endregion

        #region Constructors

        public Hand()
        {

        }

        #endregion

        #region Properties

        public Wrist Wrist
        {
            get
            {
                return wrist;
            }
            set
            {
                wrist = value;
            }
        }

        public Palm Palm
        {
            get
            {
                return palm;
            }
            set
            {
                palm = value;
            }
        }

        public Fingers Fingers
        {
            get
            {
                return fingers;
            }
            set
            {
                fingers = value;
            }
        }

        #endregion
    }
}
