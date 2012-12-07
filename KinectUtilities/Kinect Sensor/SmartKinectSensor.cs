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
        #region Private Variables

        private KinectSensor sensor;
        private SkeletonController skeletonController;
        private Skeleton[] skeletonData;
        private SensorMode sensorMode;

        #endregion

        #region Constructors

        public SmartKinectSensor()
        {
            // RBakerFlag -> Need error handling for this.
            this.sensor = KinectSensor.KinectSensors[0];
            this.skeletonData = new Skeleton[this.sensor.SkeletonStream.FrameSkeletonArrayLength];

            this.skeletonController = new SkeletonController(this.sensor);
            this.sensorMode = SensorMode.Disabled;
        }
        public SmartKinectSensor(KinectSensor sensor)
        {
            this.sensor = sensor;
            this.skeletonData = new Skeleton[this.sensor.SkeletonStream.FrameSkeletonArrayLength];

            this.skeletonController = new SkeletonController(this.sensor);
            this.sensorMode = SensorMode.Disabled;
        }

        #endregion

        #region Properties

        public SkeletonController SkeletonController
        {
            get
            {
                return skeletonController;
            }
        }
        public SensorMode SensorMode
        {
            get
            {
                return sensorMode;
            }
            set
            {
                sensorMode = value;
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
            this.sensor.SkeletonStream.Enable();
            this.sensor.SkeletonStream.AppChoosesSkeletons = true;
            this.sensor.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(sensor_SkeletonFrameReadyForSkeletonRendering);
            this.sensorMode = SensorMode.SkeletonRendering;

            if (!this.skeletonController.SkeletonCapturingFunctions.Contains(SkeletonCapturingFunction.SkeletonRendering))
            {
                this.skeletonController.SkeletonCapturingFunctions.Add(SkeletonCapturingFunction.SkeletonRendering);
            }
        }
        private void EnableFullImageSkeletonRendering()
        {
            this.sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            this.sensor.DepthStream.Enable(DepthImageFormat.Resolution320x240Fps30);
            this.sensor.SkeletonStream.Enable();
            this.sensor.SkeletonStream.AppChoosesSkeletons = true;
            this.sensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReadyForFullImageSkeletonRendering);
            this.sensorMode = SensorMode.FullImageSkeletonRendering;

            if (!this.skeletonController.SkeletonCapturingFunctions.Contains(SkeletonCapturingFunction.SkeletonRendering))
            {
                this.skeletonController.SkeletonCapturingFunctions.Add(SkeletonCapturingFunction.SkeletonRendering);
            }
        }

        #endregion

        #region Event Handlers

        private void sensor_AllFramesReadyForFullImageSkeletonRendering(object sender, AllFramesReadyEventArgs e)
        {
            if (sensorMode != SensorMode.FullImageSkeletonRendering) return;
            
            ColorImageFrame imageFrame = e.OpenColorImageFrame();
            SkeletonFrame skeletonFrame = e.OpenSkeletonFrame();
            if (imageFrame != null && skeletonFrame != null)
            {
                skeletonFrame.CopySkeletonDataTo(skeletonData);
                skeletonController.CaptureSkeletonData(skeletonData, imageFrame, DateTimeUtilities.ToDateTime(skeletonFrame.Timestamp));
            }

        }
        private void sensor_SkeletonFrameReadyForSkeletonRendering(object sender, SkeletonFrameReadyEventArgs e)
        {
            if (sensorMode != SensorMode.SkeletonRendering) return;

            SkeletonFrame skeletonFrame = e.OpenSkeletonFrame();
            if (skeletonFrame != null)
            {
                skeletonFrame.CopySkeletonDataTo(skeletonData);
                skeletonController.CaptureSkeletonData(skeletonData, DateTimeUtilities.ToDateTime(skeletonFrame.Timestamp));
            }
        }

        #endregion
    }
}
