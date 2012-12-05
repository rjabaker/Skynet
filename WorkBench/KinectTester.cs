﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

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
            sensor.SkeletonRenderedEventHandler += DisplayRenderedImage;
            sensor.TestCapturedEventHandler += GestureCaptured;
            sensor.NumberOfSkeletonsToRecognize = 1;
            sensor.EnableSkeletonRenderingSensor();

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

        public void DisplayRenderedImage(Bitmap image)
        {
            skeletonPicture.Image = image;
        }

        public void GestureCaptured(bool captured)
        {
            cwAnalogPinMapping.SetPin(captured ? 255 : 0);
            ccwAnalogPinMapping.SetPin(0);
        }
    }
}
