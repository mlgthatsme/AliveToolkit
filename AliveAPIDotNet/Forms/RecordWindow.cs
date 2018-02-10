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

namespace AliveAPIDotNet.Forms
{
    public partial class RecordWindow : Form
    {
        public RecordWindow()
        {
            InitializeComponent();
            FormClosing += RecordWindow_FormClosing;
        }

        private void RecordWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public static DemoRecorder mRecorder = new DemoRecorder();
        public static DemoPlayer mPlayer = new DemoPlayer();

        bool recording = false;
        bool playing = false;
        

        private void button1_Click(object sender, EventArgs e)
        {
            recording = !recording;

            if (recording)
            {
                buttonPlay.Enabled = false;
                buttonStart.Text = "Stop Recording";
                mRecorder = new DemoRecorder();
                mRecorder.Start();
            }
            else
            {
                buttonPlay.Enabled = true;
                buttonStart.Text = "Start Recording";
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
                buttonPlay.Text = "Stop Playing";
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    mPlayer.Open(File.Open(dialog.FileName, FileMode.Open));
                }
            }
            else
            {
                buttonPlay.Text = "Start Playing";
                mPlayer.Dispose();
            }
        }
    }
}
