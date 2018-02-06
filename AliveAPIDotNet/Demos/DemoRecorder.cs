using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliveAPIDotNet.Demos
{
    public class DemoRecorder : IDisposable
    {
        public DemoRecorder()
        {

        }

        private bool _hasStarted = false;
        private QuikSave mSaveState;
        private uint mPrevInput = 0;
        private int mRandSeed = 0;
        private List<DemoInputState> mInputStates = new List<DemoInputState>();

        public void Start()
        {
            _hasStarted = true;
            AliveAPI.OnInput += AliveAPI_OnInput;
            mSaveState = AliveAPI.QuikSave();
            mRandSeed = AliveAPI.RandomSeed;
        }

        private void AliveAPI_OnInput(object sender, InputEventArgs e)
        {
            if (_hasStarted)
            {
                if (e.Index == 0 && mPrevInput != (uint)e.Pad.Pressed.Value)
                {
                    mInputStates.Add(new DemoInputState() { gnFrame = AliveAPI.gnFrame, Pressed = (uint)(Int32)e.Pad.Pressed });
                    Console.WriteLine($"Writing State: {((uint)(Int32)e.Pad.Pressed).ToString("X")}");
                    mPrevInput = (uint)e.Pad.Pressed.Value;
                }
            }
        }

        public byte[] Stop()
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write(mSaveState.Data);
            writer.Write(mRandSeed);
            writer.Write(mInputStates.Count);
            for(int i = 0; i < mInputStates.Count;i++)
            {
                writer.Write(mInputStates[i].gnFrame);
                writer.Write(mInputStates[i].Pressed);
            }

            return stream.ToArray();
        }

        public void Dispose()
        {
            AliveAPI.OnInput -= AliveAPI_OnInput;
        }
    }
}
