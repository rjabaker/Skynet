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
            this.analogIntensityTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.analogGoButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // toggleLED
            // 
            this.toggleLED.Location = new System.Drawing.Point(53, 105);
            this.toggleLED.Name = "toggleLED";
            this.toggleLED.Size = new System.Drawing.Size(167, 52);
            this.toggleLED.TabIndex = 0;
            this.toggleLED.Text = "Toggle LED";
            this.toggleLED.UseVisualStyleBackColor = true;
            this.toggleLED.Click += new System.EventHandler(this.toggleLED_Click);
            // 
            // replyPackageTextBox
            // 
            this.replyPackageTextBox.Location = new System.Drawing.Point(67, 179);
            this.replyPackageTextBox.Name = "replyPackageTextBox";
            this.replyPackageTextBox.ReadOnly = true;
            this.replyPackageTextBox.Size = new System.Drawing.Size(134, 20);
            this.replyPackageTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Reply Package";
            // 
            // analogIntensityTextBox
            // 
            this.analogIntensityTextBox.Location = new System.Drawing.Point(67, 39);
            this.analogIntensityTextBox.Name = "analogIntensityTextBox";
            this.analogIntensityTextBox.Size = new System.Drawing.Size(115, 20);
            this.analogIntensityTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Intensity:";
            // 
            // analogGoButton
            // 
            this.analogGoButton.Location = new System.Drawing.Point(197, 33);
            this.analogGoButton.Name = "analogGoButton";
            this.analogGoButton.Size = new System.Drawing.Size(75, 31);
            this.analogGoButton.TabIndex = 5;
            this.analogGoButton.Text = "AnalogGo";
            this.analogGoButton.UseVisualStyleBackColor = true;
            this.analogGoButton.Click += new System.EventHandler(this.analogGoButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.analogGoButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.analogIntensityTextBox);
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
        private System.Windows.Forms.TextBox analogIntensityTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button analogGoButton;
    }
}

