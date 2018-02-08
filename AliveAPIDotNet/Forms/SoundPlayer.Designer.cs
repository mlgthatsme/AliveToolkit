namespace AliveAPIDotNet.Forms
{
    partial class SoundPlayer
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
            this.pianoControl1 = new AliveAPIDotNet.PianoControl();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // pianoControl1
            // 
            this.pianoControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pianoControl1.KeyEnd = 108;
            this.pianoControl1.KeyStart = 24;
            this.pianoControl1.Location = new System.Drawing.Point(12, 39);
            this.pianoControl1.Name = "pianoControl1";
            this.pianoControl1.Size = new System.Drawing.Size(803, 113);
            this.pianoControl1.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(159, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Text = "Programs";
            // 
            // SoundPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 155);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pianoControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SoundPlayer";
            this.Text = "SoundPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SoundPlayer_FormClosing);
            this.Load += new System.EventHandler(this.SoundPlayer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PianoControl pianoControl1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}