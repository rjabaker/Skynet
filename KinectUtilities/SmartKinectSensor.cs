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
    public class SmartKinectSensor
    {
        #region Event Handlers

        private SkeletonRenderer.SkeletonRenderedEventHandler skeletonRenderedEventHandler;
        private SkeletonRenderer.TestCaptured testCapturedEventHandler;

        #endregion

        #region Private Variables

        private KinectSensor sensor;
        private Skeleton[] skeletonData;
        private SkeletonRecognizer skeletonRecognizer;

        private int numberOfSkeletonsToRecognize;
        
        #endregion

        #region Constructors

        public SmartKinectSensor()
        {
            // RBakerFlag -> Need error handling for this.
            this.sensor = KinectSensor.KinectSensors[0];

            this.skeletonData = new Skeleton[this.sensor.SkeletonStream.FrameSkeletonArrayLength];
            this.skeletonRecognizer = new SkeletonRecognizer();

            this.numberOfSkeletonsToRecognize = 0;
        }

        #endregion

        #region Properties

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

        public SkeletonRenderer.SkeletonRenderedEventHandler SkeletonRenderedEventHandler
        {
            get
            {
                return skeletonRenderedEventHandler;
            }
            set
            {
                skeletonRenderedEventHandler = value;
            }
        }
        public SkeletonRenderer.TestCaptured TestCapturedEventHandler
        {
            get
            {
                return testCapturedEventHandler;
            }
            set
            {
                testCapturedEventHandler = value;
            }
        }

        #endregion

        #region Public Methods

        public void EnableSkeletonRenderingSensor()
        {
            Start();
            EnableSkeletonRendering();
        }
        public void EnableFullImageSkeletonRenderingSensor()
        {
            Start();
            EnableFullImageSkeletonRendering();
        }

        #endregion

        #region Private Methods

        private void Start()
        {
            sensor.Start();
        }
        private void Stop()
        {
            sensor.Stop();
        }
        private void EnableSkeletonRendering()
        {
            SkeletonRenderer.Sensor = this.sensor;
            SkeletonRenderer.CreateDefaultBitmap(new Size(sensor.ColorStream.FrameWidth, sensor.ColorStream.FrameHeight), Color.Black);

            sensor.SkeletonStream.Enable();
            sensor.SkeletonStream.AppChoosesSkeletons = true;
            this.sensor.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(sensor_SkeletonFrameReady);
        }
        private void EnableFullImageSkeletonRendering()
        {
            SkeletonRenderer.Sensor = this.sensor;
            SkeletonRenderer.CreateDefaultBitmap(new Size(sensor.ColorStream.FrameWidth, sensor.ColorStream.FrameHeight), Color.Black);

            sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            sensor.DepthStream.Enable(DepthImageFormat.Resolution320x240Fps30);
            sensor.SkeletonStream.Enable();
            sensor.SkeletonStream.AppChoosesSkeletons = true;
            this.sensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReady);
        }

        private void RecognizeSkeleton(int numberToRecognize)
        {
            Bitmap bitmap = null;

            List<Skeleton> closestSkeletons = skeletonRecognizer.Recognize(skeletonData, numberToRecognize);
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
                    if (skeletonRenderedEventHandler != null)
                    {
                        bitmap = bitmap == null ? SkeletonRenderer.RenderSkeleton(skeleton) : SkeletonRenderer.RenderSkeleton(bitmap, skeleton);
                    }

                    //GestureTree tree = new GestureTree();
                    //tree.CaptureGesture(skeleton);
                    //KinectSerializer.SerializeToXml<GestureTree>(tree, "C:\\Users\\Robert\\Documents\\GitHub\\docs\\design\\The Eye\\Phase 1\\Files\\Gesture Bin\\flex.xml");
                    GestureTree tree = KinectSerializer.DeserializeFromXml<GestureTree>("C:\\Users\\Robert\\Documents\\GitHub\\docs\\design\\The Eye\\Phase 1\\Files\\Gesture Bin\\flex.xml");
                    List<Joint> joints = new List<Joint>();
                    joints.Add(skeleton.Joints[JointType.ShoulderRight]);
                    joints.Add(skeleton.Joints[JointType.ElbowRight]);
                    joints.Add(skeleton.Joints[JointType.WristRight]);
                    joints.Add(skeleton.Joints[JointType.HandRight]);
                    bool captured = tree.DoesSkeletonContainGesture(joints);
                    if (captured) SkeletonRenderer.RenderSkeleton(bitmap, skeleton, Pens.SeaGreen);
                    TestCapturedEventHandler(captured);
                }
            }

            if (bitmap != null) skeletonRenderedEventHandler(bitmap);
        }
        private void RecognizeSkeleton(ColorImageFrame imageFrame, int numberToRecognize)
        {
            List<Skeleton> closestSkeletons = skeletonRecognizer.Recognize(skeletonData, numberToRecognize);
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
                    if (skeletonRenderedEventHandler != null)
                    {
                        Bitmap bitmap = SkeletonRenderer.RenderSkeleton(imageFrame, skeleton);
                        skeletonRenderedEventHandler(bitmap);
                    }
                }
            }
        }

        #endregion

        #region Event Handlers

        private void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            ColorImageFrame imageFrame = e.OpenColorImageFrame();
            SkeletonFrame skeletonFrame = e.OpenSkeletonFrame();
            if (imageFrame != null && skeletonFrame != null)
            {
                skeletonFrame.CopySkeletonDataTo(skeletonData);
                RecognizeSkeleton(imageFrame, numberOfSkeletonsToRecognize);
            }

        }

        private void sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            SkeletonFrame skeletonFrame = e.OpenSkeletonFrame();
            if (skeletonFrame != null)
            {
                skeletonFrame.CopySkeletonDataTo(skeletonData);
                RecognizeSkeleton(numberOfSkeletonsToRecognize);
            }
        }

        #endregion
    }
}
