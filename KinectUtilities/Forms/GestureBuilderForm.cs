using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using KinectUtilities.Gestures;

namespace KinectUtilities
{
    public partial class GestureBuilderForm : Form
    {
        #region Delegates

        private delegate void DisplayRenderedImageEventHandler(Bitmap image);
        private delegate void DisplayRenderedImageTimeStampEventHandler(DateTime timeStamp);
        private delegate void UpdateFrameMetadataEventHandler(DateTime timeStamp);

        #endregion

        #region Private Variables

        private RenderCanvas renderCanvas;
        private SmartKinectSensor sensor;

        private TimeSpan timeSpan;
        private bool recording;
        private bool replaying;

        #endregion

        #region Constructors

        public GestureBuilderForm(SmartKinectSensor sensor)
        {
            InitializeComponent();

            this.sensor = sensor;

            this.timeSpan = TimeSpan.FromSeconds(10);
            this.renderCanvas = new RenderCanvas(timeSpan);
            this.sensor.SkeletonController.SkeletonRendered += renderCanvas.SkeletonFrameCaptured;
            this.renderCanvas.ImageRendered += new ImagingUtilities.ImageRenderedEventHandler(renderCanvas_ImageRendered);
            this.renderCanvas.ReplayCanvasComplete += new ImagingUtilities.ImageRenderingCompleteEventHandler(renderCanvas_ReplayCanvasComplete);

            this.recording = true;
            this.replaying = false;

            InitializeFormControls();
        }

        #endregion

        #region Private Methods

        private void InitializeFormControls()
        {
            SetRecordButtonColor();
            
            memoryTimeNumericTextBox.Value = (int)timeSpan.TotalSeconds;
        }

        private void ToggleRenderCanvas(bool record)
        {
            renderCanvas.CanvasMode = record ? RenderCanvas.Mode.ListeningAndFiring : RenderCanvas.Mode.Stopped;
        }
        private void DisplayRenderedImage(Bitmap image)
        {
            if (this.frameCapturePictureBox.InvokeRequired)
            {
                DisplayRenderedImageEventHandler d = new DisplayRenderedImageEventHandler(DisplayRenderedImage);
                this.Invoke(d, new object[] { image });
            }
            else
            {
                frameCapturePictureBox.Image = image;
            }
        }
        private void UpdateTimeStamp(DateTime timeStamp)
        {
            if (this.timeStampLabel.InvokeRequired)
            {
                DisplayRenderedImageTimeStampEventHandler d = new DisplayRenderedImageTimeStampEventHandler(UpdateTimeStamp);
                this.Invoke(d, new object[] { timeStamp });
            }
            else
            {
                timeStampLabel.Text = timeStamp.ToString("mm:ss.fff");
            }
        }
        private void UpdateFrameMetadata(DateTime timeStamp)
        {
            if (replaying) return;
            if (this.timeStampLabel.InvokeRequired)
            {
                UpdateFrameMetadataEventHandler d = new UpdateFrameMetadataEventHandler(UpdateFrameMetadata);
                this.Invoke(d, new object[] { timeStamp });
            }
            else
            {
                UpdateMemoryIntervalLabels();
            }
        }
        private void UpdateMemoryIntervalLabels()
        {
            this.memoryStartTimeLabel.Text = renderCanvas.MemoryStartTime.ToString("mm:ss.fff");
            this.memoryEndTimeLabel.Text = renderCanvas.MemoryEndTime.ToString("mm:ss.fff");
        }
        private void UpdateMemoryIntervalMetadata()
        {
            gestureStartTimeListBox.BeginUpdate();
            gestureStartTimeListBox.DataSource = renderCanvas.FramesTimeStamps;
            gestureStartTimeListBox.FormatString = "mm:ss.fff";
            gestureStartTimeListBox.EndUpdate();
        }
        private void UpdateGestureStartAndEndTimeListBoxes()
        {
            gestureStartTimeListBox.BeginUpdate();
            gestureStartTimeListBox.DataSource = renderCanvas.FramesTimeStamps;
            gestureStartTimeListBox.FormatString = "mm:ss.fff";
            gestureStartTimeListBox.EndUpdate();

            gestureEndTimeListBox.BeginUpdate();
            gestureEndTimeListBox.DataSource = renderCanvas.FramesTimeStamps;
            gestureEndTimeListBox.FormatString = "mm:ss.fff";
            gestureEndTimeListBox.EndUpdate();
        }

        private void SetRecordButtonColor()
        {
            if (recording)
            {
                recordButton.BackColor = Color.Green;
            }
            else
            {
                recordButton.BackColor = Color.Gray;
            }
        }
        private void SetReplayButtonColor()
        {
            if (replaying)
            {
                replayButton.BackColor = Color.Green;
            }
            else
            {
                replayButton.BackColor = Color.Gray;
            }
        }

        #endregion

        #region Event Handlers

        private void renderCanvas_ImageRendered(Bitmap image, DateTime timeStamp)
        {
            DisplayRenderedImage(image);
            UpdateTimeStamp(timeStamp);
            UpdateFrameMetadata(timeStamp);
        }
        private void renderCanvas_ReplayCanvasComplete(DateTime timeStamp)
        {
            replaying = false;
            SetReplayButtonColor();
        }

        private void replayButton_Click(object sender, EventArgs e)
        {
            // Stop recording.
            recording = false;
            ToggleRenderCanvas(recording);
            SetRecordButtonColor();
            UpdateMemoryIntervalMetadata();

            replaying = true;
            renderCanvas.ReplayCanvas();
            SetReplayButtonColor();
        }
        private void recordButton_Click(object sender, EventArgs e)
        {
            recording = !recording;
            ToggleRenderCanvas(recording);
            SetRecordButtonColor();
            UpdateMemoryIntervalMetadata();
        }
        private void gestureStartTimeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime timeStamp = (DateTime)gestureStartTimeListBox.SelectedItem;
            Bitmap frame = renderCanvas.GetImageAtDateTime(timeStamp);
            DisplayRenderedImage(frame);
            UpdateTimeStamp(timeStamp);
        }
        private void gestureEndTimeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime timeStamp = (DateTime)gestureEndTimeListBox.SelectedItem;
            Bitmap frame = renderCanvas.GetImageAtDateTime(timeStamp);
            DisplayRenderedImage(frame);
            UpdateTimeStamp(timeStamp);
        }
        private void GestureBuilderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.sensor.SkeletonController.SkeletonRendered -= renderCanvas.SkeletonFrameCaptured;
            this.renderCanvas.ImageRendered -= new ImagingUtilities.ImageRenderedEventHandler(renderCanvas_ImageRendered);
            this.renderCanvas.ReplayCanvasComplete += new ImagingUtilities.ImageRenderingCompleteEventHandler(renderCanvas_ReplayCanvasComplete);
        }
        private void memoryTimeNumericTextBox_ValueChanged(object sender, EventArgs e)
        {
            timeSpan = TimeSpan.FromSeconds((double)memoryTimeNumericTextBox.Value);
            renderCanvas.RenderDuration = timeSpan;
        }

        #endregion
    }
}
