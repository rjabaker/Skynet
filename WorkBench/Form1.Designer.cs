namespace WorkBench
{
    partial class Form1
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
            this.toggleLED = new System.Windows.Forms.Button();
            this.replyPackageTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cwAnalogIntensityTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.analogGoButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ccwAnalogIntensityTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // toggleLED
            // 
            this.toggleLED.Location = new System.Drawing.Point(58, 321);
            this.toggleLED.Name = "toggleLED";
            this.toggleLED.Size = new System.Drawing.Size(167, 52);
            this.toggleLED.TabIndex = 0;
            this.toggleLED.Text = "Toggle LED";
            this.toggleLED.UseVisualStyleBackColor = true;
            this.toggleLED.Click += new System.EventHandler(this.toggleLED_Click);
            // 
            // replyPackageTextBox
            // 
            this.replyPackageTextBox.Location = new System.Drawing.Point(72, 395);
            this.replyPackageTextBox.Name = "replyPackageTextBox";
            this.replyPackageTextBox.ReadOnly = true;
            this.replyPackageTextBox.Size = new System.Drawing.Size(134, 20);
            this.replyPackageTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 379);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Reply Package";
            // 
            // cwAnalogIntensityTextBox
            // 
            this.cwAnalogIntensityTextBox.Location = new System.Drawing.Point(103, 39);
            this.cwAnalogIntensityTextBox.Name = "cwAnalogIntensityTextBox";
            this.cwAnalogIntensityTextBox.Size = new System.Drawing.Size(115, 20);
            this.cwAnalogIntensityTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "CW Intensity:";
            // 
            // analogGoButton
            // 
            this.analogGoButton.Location = new System.Drawing.Point(105, 116);
            this.analogGoButton.Name = "analogGoButton";
            this.analogGoButton.Size = new System.Drawing.Size(75, 31);
            this.analogGoButton.TabIndex = 5;
            this.analogGoButton.Text = "AnalogGo";
            this.analogGoButton.UseVisualStyleBackColor = true;
            this.analogGoButton.Click += new System.EventHandler(this.analogGoButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "CCW Intensity:";
            // 
            // ccwAnalogIntensityTextBox
            // 
            this.ccwAnalogIntensityTextBox.Location = new System.Drawing.Point(103, 79);
            this.ccwAnalogIntensityTextBox.Name = "ccwAnalogIntensityTextBox";
            this.ccwAnalogIntensityTextBox.Size = new System.Drawing.Size(115, 20);
            this.ccwAnalogIntensityTextBox.TabIndex = 6;
            this.ccwAnalogIntensityTextBox.TextChanged += new System.EventHandler(this.ccwAnalogIntensityTextBox_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(59, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(167, 52);
            this.button1.TabIndex = 8;
            this.button1.Text = "E-Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 428);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ccwAnalogIntensityTextBox);
            this.Controls.Add(this.analogGoButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cwAnalogIntensityTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.replyPackageTextBox);
            this.Controls.Add(this.toggleLED);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button toggleLED;
        private System.Windows.Forms.TextBox replyPackageTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox cwAnalogIntensityTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button analogGoButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ccwAnalogIntensityTextBox;
        private System.Windows.Forms.Button button1;
    }
}

