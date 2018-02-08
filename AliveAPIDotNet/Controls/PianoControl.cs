using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AliveAPIDotNet
{
    public partial class PianoControl : UserControl
    {
        public PianoControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            MouseMove += PianoControl_MouseMove;
            MouseDown += PianoControl_MouseDown;
            MouseUp += PianoControl_MouseUp;
        }

        public class NoteChangeArgs : EventArgs
        {
            public enum NoteState
            {
                Pressed = 0,
                Released = 1
            }

            public int Note { get; internal set; }
            public NoteState State { get; internal set; }
        }

        public event EventHandler<NoteChangeArgs> OnNoteChanged;

        private void PianoControl_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
            Refresh();
        }

        private void PianoControl_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            Refresh();
        }

        private void PianoControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                Refresh();
            }
        }

        struct PianoKey
        {
            public Rectangle Bounds;
            public int Note;
        }

        List<PianoKey> mKeys = new List<PianoKey>();
        bool IsMouseDown = false;
        int CurrentNoteDown = 0;

        public int KeyStart { get; set; } = 24;
        public int KeyEnd { get; set; } = 108;

        protected override void OnPaint(PaintEventArgs e)
        {
            Point MousePos = PointToClient(MousePosition);

            int prevNoteDown = CurrentNoteDown;

            if (IsMouseDown)
            {
                foreach (var k in mKeys)
                {
                    if (k.Bounds.Contains(MousePos))
                    {
                        CurrentNoteDown = k.Note;
                        break;
                    }
                }
            }
            else
            {
                CurrentNoteDown = -1;
            }

            if (prevNoteDown != CurrentNoteDown)
            {
                if (CurrentNoteDown == -1)
                {
                    OnNoteChanged?.Invoke(this, new NoteChangeArgs() { Note = prevNoteDown, State = NoteChangeArgs.NoteState.Released });
                }
                else
                {
                    OnNoteChanged?.Invoke(this, new NoteChangeArgs() { Note = prevNoteDown, State = NoteChangeArgs.NoteState.Released });
                    OnNoteChanged?.Invoke(this, new NoteChangeArgs() { Note = CurrentNoteDown, State = NoteChangeArgs.NoteState.Pressed });
                }
            }

            mKeys.Clear();

            int keyWidth = 16;
            int bottomGap = 24;
            int keyHeight = Height - bottomGap;
            int blackKeyWidth = 10;

            int xOffset = 0;
            int semitoneIndex = KeyStart % 12;
            int c = KeyStart;
            

            for (int i = KeyStart; i <= KeyEnd; i++)
            {
                if (!new int[] { 1, 3, 6, 8, 10 }.Contains(semitoneIndex))
                {
                    Rectangle dstRect = new Rectangle(xOffset, 0, keyWidth, keyHeight);
                    e.Graphics.FillRectangle((i == CurrentNoteDown) ? Brushes.Red : Brushes.White, dstRect);
                    e.Graphics.DrawLine(Pens.Black, new Point(xOffset, 0), new Point(xOffset, keyHeight));
                    mKeys.Add(new PianoKey() { Bounds = dstRect, Note = i });
                    if (c % 12 == 0)
                    {
                        e.Graphics.DrawString(i.ToString(), Font, Brushes.Black, new Rectangle(xOffset - (keyWidth/2), keyHeight, keyWidth *  2, bottomGap), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    }
                    xOffset += keyWidth;
                }
                
                semitoneIndex++;
                c++;

                if (semitoneIndex > 11)
                    semitoneIndex = 0;
            }

            e.Graphics.DrawLine(Pens.Black, new Point(0, keyHeight), new Point(Width, keyHeight));
            e.Graphics.DrawLine(Pens.Black, new Point(0, 0), new Point(Width, 0));

            xOffset = 0;
            semitoneIndex = KeyStart % 12;
            for (int i = KeyStart; i <= KeyEnd; i++)
            {
                if (new int[] { 1, 3, 6, 8, 10 }.Contains(semitoneIndex))
                {
                    Rectangle dstRect = new Rectangle(xOffset - (blackKeyWidth/2), 0, blackKeyWidth, keyHeight / 2);
                    e.Graphics.FillRectangle((i == CurrentNoteDown) ? Brushes.Blue : Brushes.Black, dstRect);
                    mKeys.Add(new PianoKey() { Bounds = dstRect, Note = i });
                }
                else
                {
                    xOffset += keyWidth;
                }

                semitoneIndex++;

                if (semitoneIndex > 11)
                    semitoneIndex = 0;
            }

            mKeys.Reverse();
        }
    }
}
