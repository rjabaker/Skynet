using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Kinect;

using KinectUtilities;

namespace KinectUtilities.Gestures
{
    [XmlRoot("ConnectedJoint")]
    public class ConnectedJoint
    {
        #region Private Variables

        private int jointID;
        private JointType jointType;
        private ConnectedJoint nextJoint;
        private List<ChildGestureRules> childGestureRulesCollection;

        #endregion

        #region Constructors

        public ConnectedJoint()
        {
            // Used for serialization.
            this.jointID = -1;
            this.nextJoint = null;
            this.childGestureRulesCollection = new List<ChildGestureRules>();
        }
        public ConnectedJoint(JointType jointType, int jointID)
        {
            this.jointType = jointType;
            this.jointID = jointID;
            this.nextJoint = null;
            this.childGestureRulesCollection = new List<ChildGestureRules>();
        }

        #endregion

        #region Properties

        [XmlElement("ID")]
        public int ID
        {
            get
            {
                return jointID;
            }
            set
            {
                jointID = value;
            }
        }

        [XmlElement("JointType")]
        public JointType JointType
        {
            get
            {
                return jointType;
            }
            set
            {
                jointType = value;
            }
        }

        [XmlElement("NextJoint")]
        public ConnectedJoint NextJoint
        {
            get
            {
                return nextJoint;
            }
            set
            {
                nextJoint = value;
            }
        }

        [XmlArray("ChildGestureRulesCollection"),
        XmlArrayItem("ChildGestureRules", typeof(ChildGestureRules))]
        public List<ChildGestureRules> ChildGestureRulesCollection
        {
            get
            {
                return childGestureRulesCollection;
            }
            set
            {
                childGestureRulesCollection = value;
            }
        }

        #endregion

        #region Public Methods

        public bool DoesChildMeetGestureRules(int childID, Joint thisJoint, Joint childJoint)
        {
            // Returns true if the child meets the specified rule. If there is no rule for the child in this ConnectedJoint,
            // then the child still meets the rule; return true;
            bool childMeetsRule = true;

            ChildGestureRules childGestureRules = childGestureRulesCollection.Find(c => c.ChildID == childID);
            if (childGestureRules == null || childGestureRules.GestureRules == null || childGestureRules.GestureRules.Count == 0) return childMeetsRule;

            double jointPairAngle = GetJointPairAngle(thisJoint, childJoint);
            foreach (AngleGestureRule gestureRule in childGestureRules.GestureRules)
            {
                childMeetsRule = gestureRule.DoesValueMeetRule(jointPairAngle);
            }

            return childMeetsRule;
        }

        public void AddChildJointRule(AngleGestureRule gestureRule, ConnectedJoint connectedJoint)
        {
            ChildGestureRules childGestureRules = childGestureRulesCollection.Find(gr => gr.ChildID == connectedJoint.ID);

            if (childGestureRules != null)
            {
                childGestureRules.AddGestureRule(gestureRule);
            }
            else
            {
                childGestureRules = new ChildGestureRules(connectedJoint.ID, gestureRule);
                childGestureRulesCollection.Add(childGestureRules);
            }
        }
        public void AddChildJointAngleRule(Joint thisJoint, Joint otherJoint, ConnectedJoint otherConnectedJoint, double tolerance)
        {
            double angle = GetJointPairAngle(thisJoint, otherJoint);
            double[] rule = new double[] { angle - tolerance, angle + tolerance };
            AngleGestureRule gestureRule = new AngleGestureRule(rule);

            AddChildJointRule(gestureRule, otherConnectedJoint);
        }

        #endregion

        #region Private Methods

        private double GetJointPairAngle(Joint jointA, Joint jointB)
        {
            // RBakerFlag -> For now, uses all axis to calculate angle. Use the scalar dot product to find the angle.
            double dotProduct = (jointA.Position.X * jointB.Position.X) + (jointA.Position.Y * jointB.Position.Y)
                + (jointA.Position.Y * jointB.Position.Y);
            double magnitudeA = Math.Sqrt((jointA.Position.X * jointA.Position.X) + (jointA.Position.Y * jointA.Position.Y)
                + (jointA.Position.Z * jointA.Position.Z));
            double magnitudeB = Math.Sqrt((jointB.Position.X * jointB.Position.X) + (jointB.Position.Y * jointB.Position.Y)
                + (jointB.Position.Z * jointB.Position.Z));

            return Math.Acos(dotProduct / (magnitudeA * magnitudeB));
        }

        #endregion
    }
}
