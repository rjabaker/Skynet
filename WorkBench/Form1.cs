using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

using ArduinoUtilities;
using Skynet;
using KinectUtilities;

namespace WorkBench
{
    public partial class Form1 : Form
    {
        #region Arduino Variables

        private bool ledON;
        private bool stopped;

        private ArduinoSerialPort serialPort;
        private delegate void SetDisplayOnEventHandler(int timesOn);

        private PinMapping pinMapping;
        private PinMapping cwAnalogPinMapping;
        private PinMapping ccwAnalogPinMapping;
        private PinMapping eStop;

        #endregion

        #region Constructors

        public Form1()
        {
            InitializeComponent();
            ArduinoSetup();
        }

        #endregion

        #region Arduino Methods

        private void ArduinoSetup()
        {
            ledON = false;
            toggleLED.BackColor = Color.Red;

            serialPort = new ArduinoSerialPort("COM5", 115200);
            serialPort.Open();

            cwAnalogPinMapping = new PinMapping(11);
            ccwAnalogPinMapping = new PinMapping(10);
            pinMapping = new PinMapping(13);
            eStop = new PinMapping(8);
            serialPort.ComponentMappings.Add(cwAnalogPinMapping);
            serialPort.ComponentMappings.Add(ccwAnalogPinMapping);
            serialPort.ComponentMappings.Add(pinMapping);
            serialPort.ComponentMappings.Add(eStop);
            cwAnalogPinMapping.SetPinMode(SetPinModeStateCodes.OutputStateCode);
            cwAnalogPinMapping.SetPinMode(SetPinModeStateCodes.OutputStateCode);
            pinMapping.SetPinMode(SetPinModeStateCodes.OutputStateCode);
            eStop.SetPinMode(SetPinModeStateCodes.OutputStateCode);
            pinMapping.FeedbackEvent += new SkynetUtilities.FeedbackRecievedEventHandler(ResponsePackageRecieved);

            this.replyPackageTextBox.Text = "0";

            eStop.SetPin(true);
            stopped = false;
        }

        private void ToggleLED(bool on)
        {
            pinMapping.SetPin(on);
        }

        private void SetDisplay(int timesOn)
        {
            if (this.replyPackageTextBox.InvokeRequired)
            {
                SetDisplayOnEventHandler d = new SetDisplayOnEventHandler(SetDisplay);
                this.Invoke(d, new object[] { timesOn });
            }

            replyPackageTextBox.Text = ((byte)timesOn).ToString();
        }

        private void ResponsePackageRecieved(PinFeedback feedback)
        {
            SetDisplay(feedback.PinState);
        }

        private void toggleLED_Click(object sender, EventArgs e)
        {
            ledON = !ledON;

            if (ledON)
            {
                toggleLED.BackColor = Color.Green;
            }
            else
            {
                toggleLED.BackColor = Color.Red;
            }

            ToggleLED(ledON);
        }

        private void analogGoButton_Click(object sender, EventArgs e)
        {
            cwAnalogPinMapping.SetPin(Convert.ToInt32(cwAnalogIntensityTextBox.Text));
            ccwAnalogPinMapping.SetPin(Convert.ToInt32(ccwAnalogIntensityTextBox.Text));
        }

        private void ccwAnalogIntensityTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            stopped = !stopped;
            eStop.SetPin(!stopped);

            if (stopped)
            {
                button1.BackColor = Color.Red;
            }
            else
            {
                button1.BackColor = Color.Green;
            }
        }

        #endregion
    }
}
