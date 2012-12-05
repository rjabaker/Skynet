using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using KinectUtilities;

namespace KinectUtilities.Gestures
{
    [XmlRoot("AngleGestureRule")]
    public class AngleGestureRule
    {
        #region Private Variables

        double[] angleRange;

        #endregion

        #region Constructors

        public AngleGestureRule()
        {
            this.angleRange = new double[] { double.PositiveInfinity, double.NegativeInfinity };
        }
        public AngleGestureRule(double[] angleRange)
        {
            this.angleRange = FixAngleRangeRule(angleRange);
        }

        #endregion

        #region Properties

        [XmlIgnore()]
        public double LowerAngleRange
        {
            get
            {
                return angleRange[0];
            }
            set
            {
                angleRange[0] = value;
                FixAngleRangeRule(angleRange);
            }
        }

        [XmlIgnore()]
        public double UpperAngleRange
        {
            get
            {
                return angleRange[1];
            }
            set
            {
                angleRange[1] = value;
                FixAngleRangeRule(angleRange);
            }
        }

        [XmlElement("RuleRange")]
        public double[] RuleRange
        {
            get
            {
                return angleRange;
            }
            set
            {
                angleRange = FixAngleRangeRule(value);
            }
        }

        #endregion

        #region Public Methods

        public bool DoesValueMeetRule(double value)
        {
            return value >= LowerAngleRange && value <= UpperAngleRange;
        }

        #endregion

        #region Private Methods

        private double[] FixAngleRangeRule(double[] rule)
        {
            // Ensures that the rule is in the form of two doubles, with the lower value first.
            // If the rule lacks a value, an appropriate unreachable limit will be added instead.
            // Extra rules will be discarded.
            if (rule.Length == 0)
            {
                rule = new double[] { double.NegativeInfinity, double.PositiveInfinity };
            }
            else if (rule.Length == 1)
            {
                rule = new double[] { rule[0], double.PositiveInfinity };
            }
            else if (rule.Length > 2)
            {
                double[] temporary = new double[] { double.PositiveInfinity, double.NegativeInfinity };
                for (int index = 0; index < rule.Length; index++)
                {
                    // Create largest rule range.
                    if (rule[index] < temporary[0]) temporary[0] = rule[index];
                    if (rule[index] > temporary[1]) temporary[1] = rule[index];
                }
                rule = temporary;
            }

            if (rule[0] > rule[1])
            {
                // Make sure the lower value is first in the array.
                double placeholder = rule[0];
                rule[0] = rule[1];
                rule[1] = placeholder;
            }

            return rule;
        }

        #endregion
    }
}
