namespace KinectUtilities
{
    partial class GestureBuilderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gestureParametersGroupBox = new System.Windows.Forms.GroupBox();
            this.frameCaptureControlGroupBox = new System.Windows.Forms.GroupBox();
            this.frameCapturePictureBox = new System.Windows.Forms.PictureBox();
            this.timeStampLabel = new System.Windows.Forms.Label();
            this.recordButton = new System.Windows.Forms.Button();
            this.memoryTimeLabel = new System.Windows.Forms.Label();
            this.memoryTimeNumericTextBox = new System.Windows.Forms.NumericUpDown();
            this.replayButton = new System.Windows.Forms.Button();
            this.memoryStartTimeLabel = new System.Windows.Forms.Label();
            this.memoryEndTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gestureStartTimeListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gestureEndTimeListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.replayIntervalButton = new System.Windows.Forms.Button();
            this.gestureParametersGroupBox.SuspendLayout();
            this.frameCaptureControlGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameCapturePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoryTimeNumericTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // gestureParametersGroupBox
            // 
            this.gestureParametersGroupBox.Controls.Add(this.label3);
            this.gestureParametersGroupBox.Controls.Add(this.gestureEndTimeListBox);
            this.gestureParametersGroupBox.Controls.Add(this.label2);
            this.gestureParametersGroupBox.Controls.Add(this.gestureStartTimeListBox);
            this.gestureParametersGroupBox.Location = new System.Drawing.Point(490, 12);
            this.gestureParametersGroupBox.Name = "gestureParametersGroupBox";
            this.gestureParametersGroupBox.Size = new System.Drawing.Size(424, 504);
            this.gestureParametersGroupBox.TabIndex = 0;
            this.gestureParametersGroupBox.TabStop = false;
            this.gestureParametersGroupBox.Text = "Gesture Parameters";
            // 
            // frameCaptureControlGroupBox
            // 
            this.frameCaptureControlGroupBox.Controls.Add(this.label1);
            this.frameCaptureControlGroupBox.Controls.Add(this.memoryEndTimeLabel);
            this.frameCaptureControlGroupBox.Controls.Add(this.memoryStartTimeLabel);
            this.frameCaptureControlGroupBox.Controls.Add(this.memoryTimeNumericTextBox);
            this.frameCaptureControlGroupBox.Controls.Add(this.memoryTimeLabel);
            this.frameCaptureControlGroupBox.Controls.Add(this.timeStampLabel);
            this.frameCaptureControlGroupBox.Controls.Add(this.frameCapturePictureBox);
            this.frameCaptureControlGroupBox.Location = new System.Drawing.Point(12, 12);
            this.frameCaptureControlGroupBox.Name = "frameCaptureControlGroupBox";
            this.frameCaptureControlGroupBox.Size = new System.Drawing.Size(472, 689);
            this.frameCaptureControlGroupBox.TabIndex = 1;
            this.frameCaptureControlGroupBox.TabStop = false;
            this.frameCaptureControlGroupBox.Text = "Frame Capture Control";
            // 
            // frameCapturePictureBox
            // 
            this.frameCapturePictureBox.Location = new System.Drawing.Point(6, 19);
            this.frameCapturePictureBox.Name = "frameCapturePictureBox";
            this.frameCapturePictureBox.Size = new System.Drawing.Size(460, 485);
            this.frameCapturePictureBox.TabIndex = 0;
            this.frameCapturePictureBox.TabStop = false;
            // 
            // timeStampLabel
            // 
            this.timeStampLabel.AutoSize = true;
            this.timeStampLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeStampLabel.Location = new System.Drawing.Point(6, 507);
            this.timeStampLabel.Name = "timeStampLabel";
            this.timeStampLabel.Size = new System.Drawing.Size(99, 20);
            this.timeStampLabel.TabIndex = 1;
            this.timeStampLabel.Text = "Time Stamp";
            // 
            // recordButton
            // 
            this.recordButton.Location = new System.Drawing.Point(626, 670);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(92, 31);
            this.recordButton.TabIndex = 2;
            this.recordButton.Text = "Recording";
            this.recordButton.UseVisualStyleBackColor = true;
            this.recordButton.Click += new System.EventHandler(this.recordButton_Click);
            // 
            // memoryTimeLabel
            // 
            this.memoryTimeLabel.AutoSize = true;
            this.memoryTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memoryTimeLabel.Location = new System.Drawing.Point(6, 539);
            this.memoryTimeLabel.Name = "memoryTimeLabel";
            this.memoryTimeLabel.Size = new System.Drawing.Size(196, 20);
            this.memoryTimeLabel.TabIndex = 3;
            this.memoryTimeLabel.Text = "Memory Time (seconds):";
            // 
            // memoryTimeNumericTextBox
            // 
            this.memoryTimeNumericTextBox.Location = new System.Drawing.Point(208, 542);
            this.memoryTimeNumericTextBox.Name = "memoryTimeNumericTextBox";
            this.memoryTimeNumericTextBox.Size = new System.Drawing.Size(41, 20);
            this.memoryTimeNumericTextBox.TabIndex = 4;
            this.memoryTimeNumericTextBox.ValueChanged += new System.EventHandler(this.memoryTimeNumericTextBox_ValueChanged);
            // 
            // replayButton
            // 
            this.replayButton.Location = new System.Drawing.Point(724, 670);
            this.replayButton.Name = "replayButton";
            this.replayButton.Size = new System.Drawing.Size(92, 31);
            this.replayButton.TabIndex = 5;
            this.replayButton.Text = "Replay";
            this.replayButton.UseVisualStyleBackColor = true;
            this.replayButton.Click += new System.EventHandler(this.replayButton_Click);
            // 
            // memoryStartTimeLabel
            // 
            this.memoryStartTimeLabel.AutoSize = true;
            this.memoryStartTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memoryStartTimeLabel.Location = new System.Drawing.Point(27, 606);
            this.memoryStartTimeLabel.Name = "memoryStartTimeLabel";
            this.memoryStartTimeLabel.Size = new System.Drawing.Size(152, 20);
            this.memoryStartTimeLabel.TabIndex = 5;
            this.memoryStartTimeLabel.Text = "Memory Start Time";
            // 
            // memoryEndTimeLabel
            // 
            this.memoryEndTimeLabel.AutoSize = true;
            this.memoryEndTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memoryEndTimeLabel.Location = new System.Drawing.Point(27, 639);
            this.memoryEndTimeLabel.Name = "memoryEndTimeLabel";
            this.memoryEndTimeLabel.Size = new System.Drawing.Size(145, 20);
            this.memoryEndTimeLabel.TabIndex = 6;
            this.memoryEndTimeLabel.Text = "Memory End Time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 571);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Memory Start To End:";
            // 
            // gestureStartTimeListBox
            // 
            this.gestureStartTimeListBox.FormattingEnabled = true;
            this.gestureStartTimeListBox.Location = new System.Drawing.Point(113, 23);
            this.gestureStartTimeListBox.Name = "gestureStartTimeListBox";
            this.gestureStartTimeListBox.ScrollAlwaysVisible = true;
            this.gestureStartTimeListBox.Size = new System.Drawing.Size(193, 134);
            this.gestureStartTimeListBox.TabIndex = 0;
            this.gestureStartTimeListBox.SelectedIndexChanged += new System.EventHandler(this.gestureStartTimeListBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Gesture Start Time: ";
            // 
            // gestureEndTimeListBox
            // 
            this.gestureEndTimeListBox.FormattingEnabled = true;
            this.gestureEndTimeListBox.Location = new System.Drawing.Point(116, 185);
            this.gestureEndTimeListBox.Name = "gestureEndTimeListBox";
            this.gestureEndTimeListBox.ScrollAlwaysVisible = true;
            this.gestureEndTimeListBox.Size = new System.Drawing.Size(193, 134);
            this.gestureEndTimeListBox.TabIndex = 2;
            this.gestureEndTimeListBox.SelectedIndexChanged += new System.EventHandler(this.gestureEndTimeListBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Gesture End Time: ";
            // 
            // replayIntervalButton
            // 
            this.replayIntervalButton.Location = new System.Drawing.Point(822, 670);
            this.replayIntervalButton.Name = "replayIntervalButton";
            this.replayIntervalButton.Size = new System.Drawing.Size(92, 31);
            this.replayIntervalButton.TabIndex = 6;
            this.replayIntervalButton.Text = "Replay Interval";
            this.replayIntervalButton.UseVisualStyleBackColor = true;
            this.replayIntervalButton.Click += new System.EventHandler(this.replayIntervalButton_Click);
            // 
            // GestureBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 713);
            this.Controls.Add(this.replayIntervalButton);
            this.Controls.Add(this.replayButton);
            this.Controls.Add(this.frameCaptureControlGroupBox);
            this.Controls.Add(this.gestureParametersGroupBox);
            this.Controls.Add(this.recordButton);
            this.Name = "GestureBuilderForm";
            this.Text = "GestureBuilderForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GestureBuilderForm_FormClosing);
            this.gestureParametersGroupBox.ResumeLayout(false);
            this.gestureParametersGroupBox.PerformLayout();
            this.frameCaptureControlGroupBox.ResumeLayout(false);
            this.frameCaptureControlGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frameCapturePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoryTimeNumericTextBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gestureParametersGroupBox;
        private System.Windows.Forms.GroupBox frameCaptureControlGroupBox;
        private System.Windows.Forms.PictureBox frameCapturePictureBox;
        private System.Windows.Forms.Label timeStampLabel;
        private System.Windows.Forms.Button recordButton;
        private System.Windows.Forms.NumericUpDown memoryTimeNumericTextBox;
        private System.Windows.Forms.Label memoryTimeLabel;
        private System.Windows.Forms.Button replayButton;
        private System.Windows.Forms.Label memoryEndTimeLabel;
        private System.Windows.Forms.Label memoryStartTimeLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox gestureStartTimeListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox gestureEndTimeListBox;
        private System.Windows.Forms.Button replayIntervalButton;
    }
}