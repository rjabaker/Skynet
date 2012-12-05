using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace KinectUtilities.Gestures
{
    [XmlRoot("ChildGestureRules")]
    public class ChildGestureRules
    {
        #region Private Variables

        private int childID;
        private List<AngleGestureRule> gestureRules;

        #endregion

        #region Constructors

        public ChildGestureRules()
        {
            childID = -1;
            this.gestureRules = new List<AngleGestureRule>();
        }

        public ChildGestureRules(int childID)
        {
            this.childID = childID;
            this.gestureRules = new List<AngleGestureRule>();
        }

        public ChildGestureRules(int childID, AngleGestureRule gestureRule)
        {
            this.childID = childID;
            this.gestureRules = new List<AngleGestureRule>();
            this.gestureRules.Add(gestureRule);
        }

        #endregion

        #region Properties

        [XmlElement("ChildID")]
        public int ChildID
        {
            get
            {
                return childID;
            }
            set
            {
                childID = value;
            }
        }

        [XmlArray("GestureRules"),
        XmlArrayItem("GestureRule", typeof(AngleGestureRule))]
        public List<AngleGestureRule> GestureRules
        {
            get
            {
                return gestureRules;
            }
            set
            {
                gestureRules = value;
            }
        }

        #endregion

        #region Public Methods

        public void AddGestureRule(AngleGestureRule gestureRule)
        {
            if (!gestureRules.Contains(gestureRule)) gestureRules.Add(gestureRule);
        }
        public bool RemoveGestureRule(AngleGestureRule gestureRule)
        {
            return gestureRules.Remove(gestureRule);
        }

        #endregion
    }
}
