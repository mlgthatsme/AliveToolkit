using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AliveAPIDotNet
{
    public class AliveObjectList
    {
        public AliveObjectList(IntPtr addr)
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

        public void Add(AliveObject obj)
        {
            IntPtr objAddr = Marshal.ReadIntPtr(Marshal.ReadIntPtr(mAddress));
            Marshal.WriteIntPtr(objAddr + (4 * Count), obj.Pointer);
            Count++;
        }

        public AliveObject[] Objects
        {
            get
            {
                List<AliveObject> objs = new List<AliveObject>();
                IntPtr objAddr = Marshal.ReadIntPtr(Marshal.ReadIntPtr(mAddress));

                for (int i = 0; i < Count; i++)
                {
                    objs.Add(new AliveObject(Marshal.ReadIntPtr(objAddr + (4 * i))));
                }

                return objs.ToArray();
            }
        }
    }
}
