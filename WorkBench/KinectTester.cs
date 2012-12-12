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
        private delegate void DisplayRenderedImageEventHandler(Bitmap image, DateTime timeStamp);

        private SmartKinectSensor sensor;
        private RenderCanvas renderCanvas;

        private ArduinoSerialPort serialPort;
        private PinMapping cwAnalogPinMapping;
        private PinMapping ccwAnalogPinMapping;
        private PinMapping eStop;

        public KinectTester()
        {
            InitializeComponent();

            sensor = new SmartKinectSensor();
            renderCanvas = new RenderCanvas(TimeSpan.FromSeconds(10));
            sensor.SkeletonController.SkeletonRendered += renderCanvas.SkeletonFrameCaptured;
            sensor.SkeletonController.GestureCaptured += GestureCaptured;
            sensor.EnableSkeletonRenderingSensor();

            renderCanvas.ImageRendered += DisplayRenderedImage;

            capturedLabel.Visible = false;
            sensor.SkeletonController.SkeletonCapturingFunctions.Add(SkeletonCapturingFunction.GestureCapturing);

            GestureBuilderForm test = new GestureBuilderForm(sensor);
            test.Show();

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

        public void DisplayRenderedImage(Bitmap image, DateTime timeStamp)
        {
            if (this.skeletonPicture.InvokeRequired)
            {
                DisplayRenderedImageEventHandler d = new DisplayRenderedImageEventHandler(DisplayRenderedImage);
                this.Invoke(d, new object[] { image, timeStamp });
            }
            else
            {
                skeletonPicture.Image = image;
            }
        }

        public void GestureCaptured(IGesture gesture, DateTime timeStamp)
        {
            capturedLabel.Visible = !capturedLabel.Visible;
        }

        public void GestureCaptured(bool captured)
        {
            cwAnalogPinMapping.SetPin(captured ? 255 : 0);
            ccwAnalogPinMapping.SetPin(0);
        }

        private void replayButton_Click(object sender, EventArgs e)
        {
            renderCanvas.ReplayCanvas();
        }
    }
}
