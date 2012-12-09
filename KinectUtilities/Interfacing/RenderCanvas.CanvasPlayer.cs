using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace KinectUtilities
{
    public partial class RenderCanvas
    {
        private class CanvasPlayer
        {
            #region Delegates

            public delegate void PlayerFinishedEventHandler();

            #endregion

            #region Events

            public event ImagingUtilities.ImageRenderedEventHandler ImageRendered;
            public event PlayerFinishedEventHandler PlayerFinished;

            #endregion

            #region Private Variables

            private SkeletonRenderFrames renderFrames;

            #endregion

            #region Constructors

            public CanvasPlayer(SkeletonRenderFrames renderFrames)
            {
                this.renderFrames = renderFrames;
            }

            #endregion

            #region Public Methods

            public void Start()
            {
                renderFrames.Sort();
                PlayRenderFrames();
                PlayerFinished();
            }

            #endregion

            #region Private Methods

            private void PlayRenderFrames()
            {
                for (int index = 0; index < renderFrames.Count; index++)
                {
                    DateTime now = renderFrames[index];
                    DateTime next = index + 1 == renderFrames.Count ? now : renderFrames[index + 1];

                    RenderTimeStampedFrame(now, next);
                }
            }
            private void RenderTimeStampedFrame(DateTime now, DateTime next)
            {
                if (renderFrames[now].Count > 0 && renderFrames[now][0].Image != null)
                {
                    ImageRendered(renderFrames[now][0].Image, now);
                }

                // Assumes the intervals differ by minutes at most.
                int interval = DateTimeUtilities.DifferenceInMilliseconds(now, next);
                Thread.Sleep(interval);
            }

            #endregion
        }
    }
}
