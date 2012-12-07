using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Microsoft.Kinect;

namespace KinectUtilities
{
    [Serializable()]
    public class SkeletonRenderFrame : ISerializable
    {
        #region Private Variables

        private Skeleton skeleton;
        private DateTime timeStamp;
        private Bitmap image;

        #endregion

        #region Constructors

        public SkeletonRenderFrame(Skeleton skeleton)
        {
            this.skeleton = skeleton;
            this.timeStamp = DateTime.Now;
            this.image = null;
        }
        public SkeletonRenderFrame(Skeleton skeleton, DateTime timeStamp)
        {
            this.skeleton = skeleton;
            this.timeStamp = timeStamp;
            this.image = null;
        }
        public SkeletonRenderFrame(Skeleton skeleton, DateTime timeStamp, Bitmap image)
        {
            this.skeleton = skeleton;
            this.timeStamp = timeStamp;
            this.image = image;
        }
        public SkeletonRenderFrame(SerializationInfo info, StreamingContext context)
        {
            this.skeleton = (Skeleton)info.GetValue("skeleton", typeof(Skeleton));
            this.timeStamp = (DateTime)info.GetValue("timeStamp", typeof(DateTime));
            this.image = (Bitmap)info.GetValue("image", typeof(Bitmap));
        }

        #endregion

        #region Properties

        public Skeleton Skeleton
        {
            get
            {
                return skeleton;
            }
            set
            {
                skeleton = value;
            }
        }
        public DateTime TimeStamp
        {
            get
            {
                return timeStamp;
            }
            set
            {
                timeStamp = value;
            }
        }
        public Bitmap Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
            }
        }

        #endregion

        #region Public Methods

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("skeleton", this.skeleton);
            info.AddValue("timeStamp", this.timeStamp);
            info.AddValue("image", this.image);
        }

        #endregion
    }
}
