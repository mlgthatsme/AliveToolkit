using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AliveAPIDotNet
{
    public class PathLineObject : UnmanagedObject
    {
        public PathLineObject(IntPtr addr) : base(addr)
        {
            
        }

        public override string ToString()
        {
            return string.Format("Path Line: {0} {1} {2} {3} {4}", X1, Y1, X2, Y2, Mode);
        }

        public override int AllocatedSize
        {
            get
            {
                return 20;
            }
        }

        public short X1
        {
            get { return SafeReadInt16(mAddress + 0); }
            set { SafeWriteInt16(mAddress + 0, value); }
        }

        public short Y1
        {
            get { return SafeReadInt16(mAddress + 2); }
            set { SafeWriteInt16(mAddress + 2, value); }
        }

        public short X2
        {
            get { return SafeReadInt16(mAddress + 4); }
            set { SafeWriteInt16(mAddress + 4, value); }
        }

        public short Y2
        {
            get { return SafeReadInt16(mAddress + 6); }
            set { SafeWriteInt16(mAddress + 6, value); }
        }

        public short Mode
        {
            get { return SafeReadInt16(mAddress + 8); }
            set { SafeWriteInt16(mAddress + 8, value); }
        }
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

        public PathLineObject[] PathEntries
        {
            get
            {
                List<PathLineObject> objs = new List<PathLineObject>();
                IntPtr objAddr = Marshal.ReadIntPtr(Marshal.ReadIntPtr(mAddress));

                for (int i = 0; i < Count; i++)
                {
                    PathLineObject pathObj = new PathLineObject(objAddr + (20 * i));
                    objs.Add(pathObj);
                }

                return objs.ToArray();
            }
        }
    }
}
