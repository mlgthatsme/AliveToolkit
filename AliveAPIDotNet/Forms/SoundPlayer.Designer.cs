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
            this.pianoControl = new AliveAPIDotNet.PianoControl();
            this.comboBoxProgram = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // pianoControl
            // 
            this.pianoControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pianoControl.KeyEnd = 108;
            this.pianoControl.KeyStart = 24;
            this.pianoControl.Location = new System.Drawing.Point(12, 39);
            this.pianoControl.Name = "pianoControl";
            this.pianoControl.Size = new System.Drawing.Size(803, 113);
            this.pianoControl.TabIndex = 0;
            // 
            // comboBoxProgram
            // 
            this.comboBoxProgram.FormattingEnabled = true;
            this.comboBoxProgram.Location = new System.Drawing.Point(12, 12);
            this.comboBoxProgram.Name = "comboBoxProgram";
            this.comboBoxProgram.Size = new System.Drawing.Size(159, 21);
            this.comboBoxProgram.TabIndex = 1;
            this.comboBoxProgram.Text = "Programs";
            // 
            // SoundPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 155);
            this.Controls.Add(this.comboBoxProgram);
            this.Controls.Add(this.pianoControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SoundPlayer";
            this.Text = "SoundPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SoundPlayer_FormClosing);
            this.Load += new System.EventHandler(this.SoundPlayer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PianoControl pianoControl;
        private System.Windows.Forms.ComboBox comboBoxProgram;
    }
}