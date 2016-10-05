using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AliveAPIDotNet
{
    public partial class RecordWindow : Form
    {
        public RecordWindow()
        {
            InitializeComponent();

            AliveAPI.GameTick += AliveAPI_GameTick;
        }

        class Replay
        {
            public List<QuikSave> SaveStates = new List<QuikSave>();
            public int PlaybackFrame = 0;
        }

        Replay replay = new Replay();
        bool recording = false;
        bool playing = false;

        private void AliveAPI_GameTick(object sender, EventArgs e)
        {
            if (recording)
            {
                replay.SaveStates.Add(AliveAPI.QuikSave());
            }
            else if (playing)
            {
                AliveAPI.QuikLoad(replay.SaveStates[replay.PlaybackFrame]);
                replay.PlaybackFrame++;

                if (replay.PlaybackFrame >= replay.SaveStates.Count)
                    replay.PlaybackFrame = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            recording = !recording;

            if (recording)
            {
                button2.Enabled = false;
                button1.Text = "Stop Recording";
                replay.PlaybackFrame = 0;
                replay.SaveStates.Clear();
            }
            else
            {
                button2.Enabled = true;
                button1.Text = "Start Recording";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            playing = !playing;

            if (playing)
            {
                button2.Text = "Stop Playing";
            }
            else
            {
                button2.Text = "Start Playing";
            }
        }
    }
}
