namespace WorkBench
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
            // KinectTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 540);
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
    }
}