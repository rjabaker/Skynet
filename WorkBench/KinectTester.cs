using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using Microsoft.Kinect;

using KinectUtilities;
using ArduinoUtilities;
using Skynet;

namespace WorkBench
{
    public partial class KinectTester : Form
    {
        private SmartKinectSensor sensor;

        private ArduinoSerialPort serialPort;
        private PinMapping cwAnalogPinMapping;
        private PinMapping ccwAnalogPinMapping;
        private PinMapping eStop;

        public KinectTester()
        {
            InitializeComponent();

            sensor = new SmartKinectSensor();
            sensor.SkeletonController.SkeletonRenderedEventHandler += DisplayRenderedImage;
            sensor.SkeletonController.GestureCapturedEventHandler += GestureCaptured;
            sensor.EnableSkeletonRenderingSensor();

            capturedLabel.Visible = false;
            sensor.SkeletonController.SkeletonCapturingFunctions.Add(SkeletonCapturingFunction.GestureCapturing);

            // InitializeArduino();
        }

        public void InitializeArduino()
        {
            serialPort = new ArduinoSerialPort("COM5", 115200);
            serialPort.Open();

            cwAnalogPinMapping = new PinMapping(11);
            ccwAnalogPinMapping = new PinMapping(10);
            eStop = new PinMapping(8);
            serialPort.ComponentMappings.Add(cwAnalogPinMapping);
            serialPort.ComponentMappings.Add(ccwAnalogPinMapping);
            serialPort.ComponentMappings.Add(eStop);
            cwAnalogPinMapping.SetPinMode(SetPinModeStateCodes.OutputStateCode);
            cwAnalogPinMapping.SetPinMode(SetPinModeStateCodes.OutputStateCode);
            eStop.SetPinMode(SetPinModeStateCodes.OutputStateCode);
            eStop.SetPin(true);
        }

        public void DisplayRenderedImage(List<Skeleton> skeletons, Bitmap image, DateTime timeStamp)
        {
            skeletonPicture.Image = image;
        }

        public void GestureCaptured(IGesture gesture)
        {
            capturedLabel.Visible = !capturedLabel.Visible;
        }

        public void GestureCaptured(bool captured)
        {
            cwAnalogPinMapping.SetPin(captured ? 255 : 0);
            ccwAnalogPinMapping.SetPin(0);
        }
    }
}
