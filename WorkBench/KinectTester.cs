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

using KinectUtilities;

namespace WorkBench
{
    public partial class KinectTester : Form
    {
        private SmartKinectSensor sensor;

        public KinectTester()
        {
            InitializeComponent();

            sensor = new SmartKinectSensor();
            sensor.SkeletonRenderedEventHandler += DisplayRenderedImage;
            sensor.NumberOfSkeletonsToRecognize = 1;
            sensor.EnableSkeletonRenderingSensor();
        }

        public void DisplayRenderedImage(Bitmap image)
        {
            skeletonPicture.Image = image;
        }
    }
}
