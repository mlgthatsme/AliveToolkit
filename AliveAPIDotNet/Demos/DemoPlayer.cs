using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliveAPIDotNet.Demos
{
    public class DemoPlayer : IDisposable
    {
        QuikSave mSave;
        private bool mReady = false;
        private bool mPlaying = false;
        private int mRandomSeed = 0;
        private int mCurrentState = 0;
        private List<DemoInputState> mInputStates = new List<DemoInputState>();

        public void Dispose()
        {
            AliveAPI.OnInput -= AliveAPI_OnInput;
        }

        public void Open(Stream file)
        {
            BinaryReader reader = new BinaryReader(file);

            byte[] saveData = reader.ReadBytes(8192);
            mSave = new QuikSave(saveData);

            mRandomSeed = reader.ReadInt32();
            var stateCount = reader.ReadInt32();

            for(int i = 0; i < stateCount;i++)
            {
                mInputStates.Add(new DemoInputState() { gnFrame = reader.ReadInt32(), Pressed = reader.ReadUInt32() });
            }

            AliveAPI.OnInput += AliveAPI_OnInput;

            if (mInputStates.Count > 0)
                Console.WriteLine($"First Replay State at frame {mInputStates[0].gnFrame}");

            lock(mInputStates)
            {
                mReady = true;
            }
            file.Close();
        }

        private void AliveAPI_OnInput(object sender, InputEventArgs e)
        {
            if (e.Index != 0)
                return;

            lock (mInputStates)
            {
                if (mReady && !mPlaying)
                {
                    AliveAPI.QuikLoad(mSave);
                    AliveAPI.RandomSeed = mRandomSeed;
                    mPlaying = true;
                }
                if (mPlaying)
                {
                    if (mInputStates.Count > 0)
                    {
                        if (mInputStates[0].gnFrame == AliveAPI.gnFrame)
                        {
                            mCurrentState = (int)mInputStates[0].Pressed;
                            Console.WriteLine($"State Change: {mCurrentState.ToString("X")}");
                            mInputStates.RemoveAt(0);
                        }

                        e.Pad.Pressed.Value = mCurrentState;
                    }
                    else
                    {
                        mPlaying = false;
                        mReady = false;
                    }
                }

            }        }
    }
}
