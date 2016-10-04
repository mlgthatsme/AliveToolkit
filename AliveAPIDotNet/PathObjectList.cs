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

        public short Unknown1
        {
            get { return SafeReadInt16(mAddress + 10); }
            set { SafeWriteInt16(mAddress + 10, value); }
        }

        public short Unknown2
        {
            get { return SafeReadInt16(mAddress + 12); }
            set { SafeWriteInt16(mAddress + 12, value); }
        }

        public short Unknown3
        {
            get { return SafeReadInt16(mAddress + 14); }
            set { SafeWriteInt16(mAddress + 14, value); }
        }

        public short Unknown4
        {
            get { return SafeReadInt16(mAddress + 16); }
            set { SafeWriteInt16(mAddress + 16, value); }
        }

        public short Unknown5
        {
            get { return SafeReadInt16(mAddress + 18); }
            set { SafeWriteInt16(mAddress + 18, value); }
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

        public short MaxCount
        {
            get
            {
                return Marshal.ReadInt16(Marshal.ReadIntPtr(mAddress) + 8);
            }
            set
            {
                Marshal.WriteInt16(Marshal.ReadIntPtr(mAddress) + 8, value);
            }
        }

        void IncreaseSize(short elements)
        {
            
            byte[] oldData = new byte[20 * MaxCount];
            Marshal.Copy(Marshal.ReadIntPtr(Marshal.ReadIntPtr(mAddress)), oldData, 0, oldData.Length);

            MaxCount += elements;
            //Marshal.FreeHGlobal(Marshal.ReadIntPtr(Marshal.ReadIntPtr(mAddress)) - 4); FIXME: We get memory leaks for now
            IntPtr newMem = Marshal.AllocHGlobal(MaxCount * 20);
            Marshal.Copy(oldData, 0, newMem, oldData.Length);

            Marshal.WriteIntPtr(Marshal.ReadIntPtr(mAddress), newMem);

            Console.WriteLine("Increasing list to " + (MaxCount * 20).ToString());
        }

        public PathLineObject AddLine(short x1, short y1, short x2, short y2, short mode)
        {
            if (Count <= MaxCount)
                IncreaseSize(100);

            Count++;
            
            PathLineObject po = new PathLineObject(Marshal.ReadIntPtr(Marshal.ReadIntPtr(mAddress)) + (20 * Count));
            po.X1 = x1;
            po.Y1 = y1;
            po.X2 = x2;
            po.Y2 = y2;
            po.Mode = mode;
            po.Unknown1 = -1;
            po.Unknown2 = -1;
            po.Unknown3 = -1;
            po.Unknown4 = -1;
            po.Unknown5 = -1;
            return po;
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
