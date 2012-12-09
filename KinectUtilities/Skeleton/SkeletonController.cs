using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using Microsoft.Kinect;

using KinectUtilities.Gestures;

namespace KinectUtilities
{
    public class SkeletonController
    {
        #region Event Handlers

        public event KinectEventUtilities.SkeletonRenderedEventHandler SkeletonRendered;
        public event KinectEventUtilities.GestureCapturedEventHandler GestureCaptured;

        #endregion

        #region Private Variables

        private KinectSensor sensor;
        private SkeletonRenderer skeletonRenderer;
        private SkeletonRecognizer skeletonRecognizer;

        private List<SkeletonCapturingFunction> capturingFunctions;
        private int numberOfSkeletonsToRecognize;

        private Bitmap defaultBitmap;

        #endregion

        #region Constructors

        public SkeletonController(KinectSensor sensor)
        {
            this.sensor = sensor;
            this.skeletonRenderer = new SkeletonRenderer(this.sensor);
            this.skeletonRecognizer = new SkeletonRecognizer();

            this.capturingFunctions = new List<SkeletonCapturingFunction>();
            this.numberOfSkeletonsToRecognize = 1;

            this.defaultBitmap = ImagingUtilities.CreateDefaultBitmap(new Size(this.sensor.ColorStream.FrameWidth, this.sensor.ColorStream.FrameHeight), Color.Black);
        }

        #endregion

        #region Properties

        public Bitmap DefaultBitmap
        {
            get
            {
                return defaultBitmap;
            }
            set
            {
                defaultBitmap = value;
            }
        }
        public int NumberOfSkeletonsToRecognize
        {
            get
            {
                return numberOfSkeletonsToRecognize;
            }
            set
            {
                numberOfSkeletonsToRecognize = value;
            }
        }
        public List<SkeletonCapturingFunction> SkeletonCapturingFunctions
        {
            get
            {
                return this.capturingFunctions;
            }
            set
            {
                this.capturingFunctions = value;
            }
        }

        #endregion

        #region Public Methods

        public void CaptureSkeletonData(Skeleton[] skeletonData, DateTime timeStamp)
        {
            List<Skeleton> skeletons = RecognizeSkeletons(skeletonData);

            if (capturingFunctions.Contains(SkeletonCapturingFunction.SkeletonRendering)) RenderSkeletons(skeletons, timeStamp);
            if (capturingFunctions.Contains(SkeletonCapturingFunction.GestureCapturing)) CaptureGestures(skeletons);
        }
        public void CaptureSkeletonData(Skeleton[] skeletonData, ColorImageFrame imageFrame, DateTime timeStamp)
        {
            List<Skeleton> skeletons = RecognizeSkeletons(skeletonData);

            if (capturingFunctions.Contains(SkeletonCapturingFunction.SkeletonRendering)) RenderSkeletons(skeletons, imageFrame, timeStamp);
            if (capturingFunctions.Contains(SkeletonCapturingFunction.GestureCapturing)) CaptureGestures(skeletons);
        }

        #endregion

        #region Private Methods

        private void RenderSkeletons(List<Skeleton> skeletons, DateTime timeStamp)
        {
            Bitmap bitmap = (Bitmap)defaultBitmap.Clone();

            foreach (Skeleton skeleton in skeletons)
            {
                bitmap = skeletonRenderer.RenderSkeleton(bitmap, skeleton);
            }

            SkeletonRendered(skeletons, bitmap, timeStamp);
        }
        private void RenderSkeletons(List<Skeleton> skeletons, ColorImageFrame imageFrame, DateTime timeStamp)
        {
            Bitmap bitmap = null;

            foreach (Skeleton skeleton in skeletons)
            {
                if (bitmap == null)
                {
                    bitmap = skeletonRenderer.RenderSkeleton(bitmap, skeleton);
                }
                else
                {
                    bitmap = skeletonRenderer.RenderSkeleton(imageFrame, skeleton);
                }
            }

            if (bitmap != null) SkeletonRendered(skeletons, bitmap, timeStamp);
        }

        private void CaptureGestures(List<Skeleton> skeletons)
        {
            foreach (Skeleton skeleton in skeletons)
            {
                CaptureSkeletonGestures(skeleton);
            }
        }
        private void CaptureSkeletonGestures(Skeleton skeleton)
        {
            //List<Joint> list = new List<Joint>();
            //list.Add(skeleton.Joints[JointType.ShoulderRight]);
            //list.Add(skeleton.Joints[JointType.ElbowRight]);
            //list.Add(skeleton.Joints[JointType.WristRight]);
            //list.Add(skeleton.Joints[JointType.HandRight]);

            //GestureTree tree = KinectSerializer.DeserializeFromXml<GestureTree>("C:\\Users\\Robert\\Documents\\GitHub\\docs\\design\\The Eye\\Phase 1\\Files\\Gesture Bin\\flex.xml");
            //bool gestureCaptured = tree.DoesSkeletonContainGesture(list);

            //if(gestureCaptured) gestureCapturedEventHandler(null);
        }

        private List<Skeleton> RecognizeSkeletons(Skeleton[] skeletonData)
        {
            List<Skeleton> recognizedSkeletons = new List<Skeleton>();

            List<Skeleton> closestSkeletons = skeletonRecognizer.Recognize(skeletonData, numberOfSkeletonsToRecognize);
            foreach (Skeleton skeleton in closestSkeletons)
            {
                if (skeleton.TrackingState != SkeletonTrackingState.Tracked)
                {
                    sensor.SkeletonStream.ChooseSkeletons(skeleton.TrackingId);
                    if (skeletonRecognizer.TrackedSkeletons.ContainsKey(skeleton.TrackingId))
                    {
                        skeletonRecognizer.TrackedSkeletons[skeleton.TrackingId] = skeleton;
                    }
                    else
                    {
                        skeletonRecognizer.TrackedSkeletons.Add(skeleton.TrackingId, skeleton);
                    }
                }
                else
                {
                    skeletonRecognizer.TrackedSkeletons[skeleton.TrackingId] = skeleton;
                    recognizedSkeletons.Add(skeleton);
                }
            }

            return recognizedSkeletons;
        }

        #endregion
    }
}
