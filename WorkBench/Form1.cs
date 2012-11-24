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

namespace WorkBench
{
    public partial class Form1 : Form
    {
        private bool ledON;

        private ArduinoSerialPort serialPort;
        private delegate void SetDisplayOnEventHandler(int timesOn);

        private PinMapping pinMapping;
        private PinMapping analogPinMapping;

        public Form1()
        {
            InitializeComponent();
            ledON = false;
            toggleLED.BackColor = Color.Red;

            serialPort = new ArduinoSerialPort("COM5", 115200);
            serialPort.Open();

            analogPinMapping = new PinMapping(11);
            pinMapping = new PinMapping(13);
            serialPort.ComponentMappings.Add(analogPinMapping);
            serialPort.ComponentMappings.Add(pinMapping);
            analogPinMapping.SetPinMode(SetPinModeStateCodes.OutputStateCode);
            pinMapping.SetPinMode(SetPinModeStateCodes.OutputStateCode);
            pinMapping.FeedbackEvent += new SkynetUtilities.FeedbackRecievedEventHandler(ResponsePackageRecieved);
            
            this.replyPackageTextBox.Text = "0";
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
            analogPinMapping.SetPin(Convert.ToInt32(analogIntensityTextBox.Text));
        }
    }
}
