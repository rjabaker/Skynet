using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Kinect;

using KinectUtilities;

namespace KinectUtilities.Gestures
{
    [XmlRoot("GestureTree")]
    public class GestureTree
    {
        #region Private Variables

        private List<ConnectedJoint> connectedJoints;

        #endregion

        #region Constructors

        public GestureTree()
        {
            // RBakerFlag -> Build this gesture tree by deserializing XML.
            connectedJoints = new List<ConnectedJoint>();
        }

        #endregion

        #region Properties

        [XmlArray("ConnectedJoints"),
        XmlArrayItem("ConnectedJoint", typeof(ConnectedJoint))]
        public List<ConnectedJoint> ConnectedJoints
        {
            get
            {
                return connectedJoints;
            }
        }

        #endregion

        #region Public Methods

        public bool GestureCaptured(List<Joint> skeletonJoints)
        {
            // Returns true if the list of joints matches the gesture tree.
            return connectedJoints.Count != 0 && DoesParentSatisfyRules(connectedJoints.First(), skeletonJoints);
        }
        public void CaptureGesture(Skeleton skeleton)
        {
            // RBakerFlag -> THIS IS TEST CODE!
            ConnectedJoint a = new ConnectedJoint(skeleton.Joints[JointType.ShoulderRight].JointType, 1);
            ConnectedJoint b = new ConnectedJoint(skeleton.Joints[JointType.ElbowRight].JointType, 2);
            ConnectedJoint c = new ConnectedJoint(skeleton.Joints[JointType.WristRight].JointType, 3);
            ConnectedJoint d = new ConnectedJoint(skeleton.Joints[JointType.HandRight].JointType, 4);

            a.NextJoint = b;
            b.NextJoint = c;
            c.NextJoint = d;
            d.NextJoint = null;

            a.AddChildJointAngleRule(skeleton.Joints[JointType.ShoulderRight], skeleton.Joints[JointType.ElbowRight], b, 0.1);
            a.AddChildJointAngleRule(skeleton.Joints[JointType.ShoulderRight], skeleton.Joints[JointType.WristRight], c, 0.1);
            a.AddChildJointAngleRule(skeleton.Joints[JointType.ShoulderRight], skeleton.Joints[JointType.HandRight], d, 0.1);

            b.AddChildJointAngleRule(skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.WristRight], c, 0.1);
            b.AddChildJointAngleRule(skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.HandRight], d, 0.1);

            c.AddChildJointAngleRule(skeleton.Joints[JointType.WristRight], skeleton.Joints[JointType.HandRight], d, 0.1);

            connectedJoints.Add(a);
            connectedJoints.Add(b);
            connectedJoints.Add(c);
            connectedJoints.Add(d);
        }

        #endregion

        #region Private Methods

        private bool DoesParentSatisfyRules(ConnectedJoint connectedJoint, List<Joint> skeletonJoints)
        {
            bool satisfyRules = true;

            Joint thisJoint;
            ConnectedJoint child;

            // Extract this joint. If it's null, the gesture will not be found; return false. Remove
            // this joint from the collection, since it's a parent and will no longer be traversed.
            thisJoint = skeletonJoints.First(joint => joint.JointType == connectedJoint.JointType);
            if (thisJoint == null) return false;
            skeletonJoints.RemoveAll(joint => joint.JointType == connectedJoint.JointType);

            foreach (Joint joint in skeletonJoints)
            {
                child = (ConnectedJoint)connectedJoints.Select(cj => cj.JointType == joint.JointType);
                satisfyRules &= child != null && connectedJoint.DoesChildMeetGestureRules(child.ID, thisJoint, joint);
                if (!satisfyRules) break;
            }

            if (connectedJoint.NextJoint == null)
            {
                return satisfyRules;
            }
            else if(!satisfyRules)
            {
                // If false already, exit the traversal.
                return satisfyRules;
            }
            else
            {
                return satisfyRules && DoesParentSatisfyRules(connectedJoint.NextJoint, skeletonJoints);
            }
        }

        #endregion
    }
}
