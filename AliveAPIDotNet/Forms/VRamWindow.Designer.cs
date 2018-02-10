namespace AliveAPIDotNet.Forms
{
    partial class VRamWindow
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
            this.pictureBoxVRam = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVRam)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxVRam
            // 
            this.pictureBoxVRam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxVRam.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxVRam.Name = "pictureBoxVRam";
            this.pictureBoxVRam.Size = new System.Drawing.Size(1024, 512);
            this.pictureBoxVRam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxVRam.TabIndex = 0;
            this.pictureBoxVRam.TabStop = false;
            this.pictureBoxVRam.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // VRamWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 512);
            this.Controls.Add(this.pictureBoxVRam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "VRamWindow";
            this.Text = "VRamWindow";
            this.Load += new System.EventHandler(this.VRamWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVRam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxVRam;
    }
}