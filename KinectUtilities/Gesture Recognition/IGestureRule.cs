using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Kinect;

using KinectUtilities;

namespace KinectUtilities.Gestures
{
    [XmlRoot("IGestureRule")]
    public interface IGestureRule
    {
        [XmlElement("RuleRange")]
        double[] RuleRange { get; set; }

        bool DoesValueMeetRule(double value);
    }
}
