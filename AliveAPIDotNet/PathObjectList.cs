using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AliveAPIDotNet
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PathEntry
    {
        public short X1;
        public short Y1;
        public short X2;
        public short Y2;

        public short Mode;
        public short Unknown2;
        public short Unknown3;
        public short Unknown4;
        public short Unknown5;
        public short Unknown6;
    }

    public class PathObjectList
    {
        public PathObjectList(IntPtr addr)
        {
            mAddress = addr;
        }

        private IntPtr mAddress;

        public short Count
        {
            get
            {
                return Marshal.ReadInt16(Marshal.ReadIntPtr(mAddress) + 4);
            }
            set
            {
                Marshal.WriteInt16(Marshal.ReadIntPtr(mAddress) + 4, value);
            }
        }

        public PathEntry[] PathEntries
        {
            get
            {
                List<PathEntry> objs = new List<PathEntry>();
                IntPtr objAddr = Marshal.ReadIntPtr(Marshal.ReadIntPtr(mAddress));
                for (int i = 0; i < Count; i++)
                {
                    PathEntry structObject = (PathEntry)Marshal.PtrToStructure(objAddr + (20 * i), typeof(PathEntry));
                    objs.Add(structObject);
                }

                return objs.ToArray();
            }
        }
    }
}
