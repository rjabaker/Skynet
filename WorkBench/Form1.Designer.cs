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
            this.SuspendLayout();
            // 
            // toggleLED
            // 
            this.toggleLED.Location = new System.Drawing.Point(56, 93);
            this.toggleLED.Name = "toggleLED";
            this.toggleLED.Size = new System.Drawing.Size(167, 52);
            this.toggleLED.TabIndex = 0;
            this.toggleLED.Text = "Toggle LED";
            this.toggleLED.UseVisualStyleBackColor = true;
            this.toggleLED.Click += new System.EventHandler(this.toggleLED_Click);
            // 
            // timesOnTextBox
            // 
            this.replyPackageTextBox.Location = new System.Drawing.Point(67, 179);
            this.replyPackageTextBox.Name = "timesOnTextBox";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
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
    }
}

