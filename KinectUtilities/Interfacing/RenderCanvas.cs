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
    /// <summary>
    /// The RenderCanvas does not drawn the skeleton, but it does retain a copy of the rendered images 
    /// (if any exist) and the SkeletonFrames. It's true  purpose is to catalog the movements of the last render duration.
    /// </summary>
    public class RenderCanvas
    {
        #region Private Variables

        private SkeletonRenderFrames skeletonFrames;
        private TimeSpan renderDuration;

        #endregion

        #region Constructors

        public RenderCanvas(TimeSpan renderDuration)
        {
            this.skeletonFrames = new SkeletonRenderFrames();
            this.renderDuration = renderDuration;
        }

        #endregion

        #region Properties

        public TimeSpan RenderDuration
        {
            get
            {
                return renderDuration;
            }
        }

        #endregion

        #region Public Methods

        public void SkeletonFrameCaptured(List<Skeleton> skeletons, DateTime timeStamp)
        {
            List<SkeletonRenderFrame> capturedFrames = new List<SkeletonRenderFrame>();

            SkeletonRenderFrame skeletonFrame;
            foreach (Skeleton skeleton in skeletons)
            {
                skeletonFrame = new SkeletonRenderFrame(skeleton, timeStamp);
                capturedFrames.Add(skeletonFrame);
            }

            UpdateSkeletonFrames(capturedFrames, timeStamp);
        }
        public void SkeletonFrameCaptured(List<Skeleton> skeletons, DateTime timeStamp, Bitmap bitmap)
        {
            List<SkeletonRenderFrame> capturedFrames = new List<SkeletonRenderFrame>();

            SkeletonRenderFrame skeletonFrame;
            foreach (Skeleton skeleton in skeletons)
            {
                skeletonFrame = new SkeletonRenderFrame(skeleton, timeStamp, bitmap);
                capturedFrames.Add(skeletonFrame);
            }

            UpdateSkeletonFrames(capturedFrames, timeStamp);
        }

        public void SaveCanvasFrames(string filename)
        {
            KinectSerializer.SerializeObject<SkeletonRenderFrames>(filename, skeletonFrames);
        }
        public void LoadCanvasFrames(string filename)
        {
            skeletonFrames = KinectSerializer.DeserializeObject<SkeletonRenderFrames>(filename);
        }

        #endregion

        #region Private Methods

        private void UpdateSkeletonFrames(List<SkeletonRenderFrame> capturedFrames, DateTime timeStamp)
        {
            DateTime oldest = skeletonFrames.Count != 0 ? skeletonFrames.Keys.Min() : DateTime.MinValue;
            if (oldest != DateTime.MinValue && timeStamp - oldest > renderDuration)
            {
                skeletonFrames.Remove(oldest);
                UpdateSkeletonFrames(capturedFrames, timeStamp);
            }
            else
            {
                skeletonFrames.Add(timeStamp, capturedFrames);
            }
        }

        #endregion
    }
}
