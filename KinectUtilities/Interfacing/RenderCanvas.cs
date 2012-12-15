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

using ToolBox.FileUtilities;

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
        public event ImagingUtilities.ImageRenderingCompleteEventHandler ReplayCanvasComplete;

        #endregion

        #region Private Variables

        private static string canvasPlayerLock = "canvasPlayerLock";

        private SkeletonRenderFrames skeletonFrames;
        private TimeSpan renderDuration;
        private Mode canvasMode;
        private Mode previousCanvasMode;

        #endregion

        #region Constructors

        public RenderCanvas(TimeSpan renderDuration)
        {
            this.skeletonFrames = new SkeletonRenderFrames();
            this.renderDuration = renderDuration;
            this.canvasMode = Mode.ListeningAndFiring;
            this.previousCanvasMode = Mode.ListeningAndFiring;
        }

        #endregion

        #region Properties

        public TimeSpan RenderDuration
        {
            get
            {
                return renderDuration;
            }
            set
            {
                renderDuration = value;
            }
        }
        public Mode CanvasMode
        {
            get
            {
                return canvasMode;
            }
            set
            {
                SetCanvasMode(value);
            }
        }
        public DateTime MemoryStartTime
        {
            get
            {
                return skeletonFrames.FramesTimeStamps.First();
            }
        }
        public DateTime MemoryEndTime
        {
            get
            {
                return skeletonFrames.FramesTimeStamps.Last();
            }
        }
        public List<DateTime> FramesTimeStamps
        {
            get
            {
                return skeletonFrames.FramesTimeStamps;
            }
        }
        public SkeletonRenderFrames SkeletonRenderFrames
        {
            get
            {
                return skeletonFrames;
            }
        }

        #endregion

        #region Public Methods

        public void SkeletonFrameCaptured(List<Skeleton> skeletons, DateTime timeStamp)
        {
            if (canvasMode != Mode.Listening || canvasMode != Mode.ListeningAndFiring) return;

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
            if (canvasMode != Mode.Listening && canvasMode != Mode.ListeningAndFiring) return;

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
            if (canvasMode == Mode.Firing || canvasMode == Mode.ListeningAndFiring) ImageRendered(bitmap, timeStamp);
        }

        public void ReplayCanvas()
        {
            SetCanvasMode(Mode.Firing);

            lock (canvasPlayerLock)
            {
                CanvasPlayer canvasPlayer = new CanvasPlayer(this.skeletonFrames);
                canvasPlayer.ImageRendered += new ImagingUtilities.ImageRenderedEventHandler(canvasPlayer_ImageRendered);
                canvasPlayer.PlayerFinished += new CanvasPlayer.PlayerFinishedEventHandler(canvasPlayer_PlayerFinished);
                Thread thread = new Thread(canvasPlayer.Start);
                thread.Start();
            }
        }
        public void ReplayCanvas(DateTime startTime, DateTime endTime)
        {
            SetCanvasMode(Mode.Firing);
            SkeletonRenderFrames frames = GetFramesWithinInterval(startTime, endTime);

            lock (canvasPlayerLock)
            {
                CanvasPlayer canvasPlayer = new CanvasPlayer(frames);
                canvasPlayer.ImageRendered += new ImagingUtilities.ImageRenderedEventHandler(canvasPlayer_ImageRendered);
                canvasPlayer.PlayerFinished += new CanvasPlayer.PlayerFinishedEventHandler(canvasPlayer_PlayerFinished);
                Thread thread = new Thread(canvasPlayer.Start);
                thread.Start();
            }
        }

        public SkeletonRenderFrames GetFramesWithinInterval(DateTime startTime, DateTime endTime)
        {
            SkeletonRenderFrames frames = new SkeletonRenderFrames();
            foreach (DateTime timeStamp in skeletonFrames.FramesTimeStamps)
            {
                if (timeStamp.CompareTo(startTime) >= 0 && timeStamp.CompareTo(endTime) <= 0)
                {
                    frames.Add(timeStamp, skeletonFrames[timeStamp]);
                }
            }

            return frames;
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
            SetCanvasMode(Mode.Stopped);
            Serializer.SerializeObject<SkeletonRenderFrames>(filename, skeletonFrames);
            RevertCanvasMode();
        }
        public void LoadCanvasFrames(string filename)
        {
            SetCanvasMode(Mode.Stopped);
            skeletonFrames = Serializer.DeserializeObject<SkeletonRenderFrames>(filename);
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
        }

        private void SetCanvasMode(Mode mode)
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
            long ticks = System.Diagnostics.Stopwatch.GetTimestamp();
            DateTime timeStamp = DateTimeUtilities.ToDateTime(ticks);

            RevertCanvasMode();
            ReplayCanvasComplete(timeStamp);
        }

        #endregion
    }
}
