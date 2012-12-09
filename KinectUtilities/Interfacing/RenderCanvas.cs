using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Timers;
using System.Threading;

using Microsoft.Kinect;

namespace KinectUtilities
{
    /// <summary>
    /// The RenderCanvas does not drawn the skeleton, but it does retain a copy of the rendered images 
    /// (if any exist) and the SkeletonFrames. It's true  purpose is to catalog the movements of the last render duration.
    /// </summary>
    public partial class RenderCanvas
    {
        #region Events

        public event ImagingUtilities.ImageRenderedEventHandler ImageRendered;

        #endregion

        #region Private Variables

        private static string canvasPlayerLock = "canvasPlayerLock";

        private SkeletonRenderFrames skeletonFrames;
        private TimeSpan renderDuration;
        private CanvasMode canvasMode;
        private CanvasMode previousCanvasMode;

        #endregion

        #region Constructors

        public RenderCanvas(TimeSpan renderDuration)
        {
            this.skeletonFrames = new SkeletonRenderFrames();
            this.renderDuration = renderDuration;
            this.canvasMode = CanvasMode.ListeningAndFiring;
            this.previousCanvasMode = CanvasMode.ListeningAndFiring;
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
            if (canvasMode != CanvasMode.Listening || canvasMode != CanvasMode.ListeningAndFiring) return;

            List<SkeletonRenderFrame> capturedFrames = new List<SkeletonRenderFrame>();

            SkeletonRenderFrame skeletonFrame;
            foreach (Skeleton skeleton in skeletons)
            {
                skeletonFrame = new SkeletonRenderFrame(skeleton, timeStamp);
                capturedFrames.Add(skeletonFrame);
            }

            UpdateSkeletonFrames(capturedFrames, timeStamp);
        }
        public void SkeletonFrameCaptured(List<Skeleton> skeletons, Bitmap bitmap, DateTime timeStamp)
        {
            if (canvasMode != CanvasMode.Listening && canvasMode != CanvasMode.ListeningAndFiring) return;

            if (bitmap == null)
            {
                SkeletonFrameCaptured(skeletons, timeStamp);
                return;
            }

            List<SkeletonRenderFrame> capturedFrames = new List<SkeletonRenderFrame>();

            SkeletonRenderFrame skeletonFrame;
            foreach (Skeleton skeleton in skeletons)
            {
                skeletonFrame = new SkeletonRenderFrame(skeleton, timeStamp, bitmap);
                capturedFrames.Add(skeletonFrame);
            }

            UpdateSkeletonFrames(capturedFrames, timeStamp);
            if (canvasMode == CanvasMode.Firing || canvasMode == CanvasMode.ListeningAndFiring) ImageRendered(bitmap, timeStamp);
        }

        public void ReplayCanvas()
        {
            SetCanvasMode(CanvasMode.Firing);

            lock (canvasPlayerLock)
            {
                CanvasPlayer canvasPlayer = new CanvasPlayer(this.skeletonFrames);
                canvasPlayer.ImageRendered += new ImagingUtilities.ImageRenderedEventHandler(canvasPlayer_ImageRendered);
                canvasPlayer.PlayerFinished += new CanvasPlayer.PlayerFinishedEventHandler(canvasPlayer_PlayerFinished);
                Thread thread = new Thread(canvasPlayer.Start);
                thread.Start();
            }
        }

        public Bitmap GetLatestImage()
        {
            DateTime mostRecent = skeletonFrames.MostRecentFrameTime;
            return GetImageAtDateTime(mostRecent);
        }
        public Bitmap GetImageAtDateTime(DateTime specifiedDateTime)
        {
            Bitmap image = null;

            List<SkeletonRenderFrame> skeletonRenderFrames;
            bool framesExist = skeletonFrames.TryGetValue(specifiedDateTime, out skeletonRenderFrames);

            if (framesExist && skeletonRenderFrames.Count > 0)
            {
                // Image may be null.
                image = skeletonRenderFrames[0].Image;
            }

            return image;
        }

        public void SaveCanvasFrames(string filename)
        {
            SetCanvasMode(CanvasMode.Stopped);
            KinectSerializer.SerializeObject<SkeletonRenderFrames>(filename, skeletonFrames);
            RevertCanvasMode();
        }
        public void LoadCanvasFrames(string filename)
        {
            SetCanvasMode(CanvasMode.Stopped);
            skeletonFrames = KinectSerializer.DeserializeObject<SkeletonRenderFrames>(filename);
            RevertCanvasMode();
        }

        #endregion

        #region Private Methods

        private void UpdateSkeletonFrames(List<SkeletonRenderFrame> capturedFrames, DateTime timeStamp)
        {
            DateTime oldest = skeletonFrames.OldestFrameTime;
            if (oldest != DateTime.MinValue && DateTimeUtilities.DifferenceInMilliseconds(oldest, timeStamp) > renderDuration.TotalSeconds * 1000)
            {
                skeletonFrames.Remove(oldest);
                UpdateSkeletonFrames(capturedFrames, timeStamp);
            }
            else
            {
                skeletonFrames.Add(timeStamp, capturedFrames);
            }

            if (skeletonFrames.Count == 290)
            {
                // RBakerFlag -> TESTCODE! REMOVE ASAP!
                Gestures.GestureBuilder b = new Gestures.GestureBuilder();
                Gestures.GestureBuilderParameters p = new Gestures.GestureBuilderParameters(null, skeletonFrames, skeletonFrames.FramesTimeStamps.First(), TimeSpan.FromSeconds(7), Gestures.GestureBuilder.BuildStrategy.StandardTolerance);
                b.BuildMovingGestureTree(p);
            }
        }

        private void SetCanvasMode(CanvasMode mode)
        {
            previousCanvasMode = canvasMode;
            canvasMode = mode;
        }
        private void RevertCanvasMode()
        {
            SetCanvasMode(previousCanvasMode);
        }

        #endregion

        #region Event Handlers

        private void canvasPlayer_ImageRendered(Bitmap image, DateTime timeStamp)
        {
            ImageRendered(image, timeStamp);
        }
        private void canvasPlayer_PlayerFinished()
        {
            RevertCanvasMode();
        }

        #endregion
    }
}
