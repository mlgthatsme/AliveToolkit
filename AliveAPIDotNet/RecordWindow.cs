using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AliveAPIDotNet.Demos;
using System.IO;

namespace AliveAPIDotNet
{
    public partial class RecordWindow : Form
    {
        public RecordWindow()
        {
            InitializeComponent();
            
        }
        

        DemoRecorder mRecorder = new DemoRecorder();
        DemoPlayer mPlayer = new DemoPlayer();

        bool recording = false;
        bool playing = false;
        

        private void button1_Click(object sender, EventArgs e)
        {
            recording = !recording;

            if (recording)
            {
                button2.Enabled = false;
                button1.Text = "Stop Recording";
                mRecorder = new DemoRecorder();
                mRecorder.Start();
            }
            else
            {
                button2.Enabled = true;
                button1.Text = "Start Recording";
                var demoFile = mRecorder.Stop();
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.AddExtension = true;
                dialog.DefaultExt = ".demo2";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(dialog.FileName, demoFile);
                }
                mRecorder.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            playing = !playing;

            if (playing)
            {
                mPlayer = new DemoPlayer();
                button2.Text = "Stop Playing";
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    mPlayer.Open(File.Open(dialog.FileName, FileMode.Open));
                }
            }
            else
            {
                button2.Text = "Start Playing";
                mPlayer.Dispose();
            }
        }
    }
}
