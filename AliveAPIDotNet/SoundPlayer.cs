using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AliveAPIDotNet
{
    public partial class SoundPlayer : Form
    {
        public SoundPlayer()
        {
            InitializeComponent();

            pianoControl1.OnNoteChanged += PianoControl1_OnNoteChanged;
        }

        private void PianoControl1_OnNoteChanged(object sender, PianoControl.NoteChangeArgs e)
        {
            if (e.State == PianoControl.NoteChangeArgs.NoteState.Pressed)
            {
                AliveAPI.PlayMidiNote(comboBox1.SelectedIndex, e.Note, 0.0f, 0.5f);
            }
        }

        private void SoundPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void SoundPlayer_Load(object sender, EventArgs e)
        {
            for(int i = 0; i < 64;i++)
            {
                comboBox1.Items.Add($"Program {i}");
            }

            comboBox1.SelectedIndex = 0;
        }
    }
}
