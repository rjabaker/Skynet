﻿namespace WorkBench
{
    partial class KinectTester
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
            this.skeletonPicture = new System.Windows.Forms.PictureBox();
            this.capturedLabel = new System.Windows.Forms.Label();
            this.replayButton = new System.Windows.Forms.Button();
            this.jointTrackingUpdateTextBox = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.skeletonPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // skeletonPicture
            // 
            this.skeletonPicture.Location = new System.Drawing.Point(55, 40);
            this.skeletonPicture.Name = "skeletonPicture";
            this.skeletonPicture.Size = new System.Drawing.Size(451, 433);
            this.skeletonPicture.TabIndex = 0;
            this.skeletonPicture.TabStop = false;
            // 
            // capturedLabel
            // 
            this.capturedLabel.AutoSize = true;
            this.capturedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capturedLabel.ForeColor = System.Drawing.Color.Red;
            this.capturedLabel.Location = new System.Drawing.Point(538, 89);
            this.capturedLabel.Name = "capturedLabel";
            this.capturedLabel.Size = new System.Drawing.Size(166, 39);
            this.capturedLabel.TabIndex = 1;
            this.capturedLabel.Text = "Captured!";
            // 
            // replayButton
            // 
            this.replayButton.Location = new System.Drawing.Point(547, 175);
            this.replayButton.Name = "replayButton";
            this.replayButton.Size = new System.Drawing.Size(170, 66);
            this.replayButton.TabIndex = 2;
            this.replayButton.Text = "Replay";
            this.replayButton.UseVisualStyleBackColor = true;
            this.replayButton.Click += new System.EventHandler(this.replayButton_Click);
            // 
            // jointTrackingUpdateTextBox
            // 
            this.jointTrackingUpdateTextBox.AutoSize = true;
            this.jointTrackingUpdateTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jointTrackingUpdateTextBox.Location = new System.Drawing.Point(546, 268);
            this.jointTrackingUpdateTextBox.Name = "jointTrackingUpdateTextBox";
            this.jointTrackingUpdateTextBox.Size = new System.Drawing.Size(70, 26);
            this.jointTrackingUpdateTextBox.TabIndex = 3;
            this.jointTrackingUpdateTextBox.Text = "label1";
            // 
            // KinectTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 540);
            this.Controls.Add(this.jointTrackingUpdateTextBox);
            this.Controls.Add(this.replayButton);
            this.Controls.Add(this.capturedLabel);
            this.Controls.Add(this.skeletonPicture);
            this.Name = "KinectTester";
            this.Text = "KinectTester";
            ((System.ComponentModel.ISupportInitialize)(this.skeletonPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox skeletonPicture;
        private System.Windows.Forms.Label capturedLabel;
        private System.Windows.Forms.Button replayButton;
        private System.Windows.Forms.Label jointTrackingUpdateTextBox;
    }
}