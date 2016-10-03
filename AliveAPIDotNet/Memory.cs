using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliveAPIDotNet
{
    public struct MemoryAllocation
    {
        public IntPtr Address;
        public int Size;
        public DateTime Time;
        public IntPtr Caller;

        public override string ToString()
        {
            return string.Format("[{0}] {1} bytes at {2}", Address.ToString("X8"), Size, Time.ToString());
        }
    }
}
