using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AliveAPIDotNet
{
    public class UnmanagedObjectPointer : UnmanagedObject
    {
        public UnmanagedObjectPointer(IntPtr addr) : base(addr)
        {

        }

        public override int AllocatedSize
        {
            get
            {
                return 4;
            }
        }

        public int PointsTo
        {
            get
            {
                return Marshal.ReadInt32(mAddress);
            }
            set
            {
                Marshal.WriteInt32(mAddress, value);
            }
        }
    }
    public class AliveObjectList : UnmanagedObject
    {
        public AliveObjectList(IntPtr addr, bool isPointer = false) : base(addr)
        {
            pointerMode = isPointer;
        }

        public AliveObjectList(IntPtr addr) : base(addr)
        {
            pointerMode = true;
        }

        public override IntPtr Pointer
        {
            get
            {
                if (pointerMode)
                    return Marshal.ReadIntPtr(mAddress);
                else
                    return mAddress;
            }
        }

        bool pointerMode = false;

        public short Count
        {
            get
            {
                return Marshal.ReadInt16(Pointer + 4);
            }
            set
            {
                Marshal.WriteInt16(Pointer + 4, value);
            }
        }

        public void Add(UnmanagedObject objPointer)
        {
            IntPtr objAddr = Marshal.ReadIntPtr(Pointer);
            Marshal.WriteIntPtr(objAddr + (4 * Count), objPointer.Pointer);
            Count++;
        }

        public UnmanagedObject[] Objects
        {
            get
            {
                List<UnmanagedObject> objs = new List<UnmanagedObject>();
                IntPtr objAddr = Marshal.ReadIntPtr(Pointer);

                for (int i = 0; i < Count; i++)
                {
                    objs.Add(new UnmanagedObject(Marshal.ReadIntPtr(objAddr + (4 * i))));
                }

                return objs.ToArray();
            }
        }

        public UnmanagedObjectPointer[] Pointers
        {
            get
            {
                List<UnmanagedObjectPointer> objs = new List<UnmanagedObjectPointer>();
                IntPtr objAddr = Marshal.ReadIntPtr(Pointer);

                for (int i = 0; i < Count; i++)
                {
                    objs.Add(new UnmanagedObjectPointer(objAddr + (4 * i)));
                }

                return objs.ToArray();
            }
        }

        public T[] AsType<T>() where T : UnmanagedObject
        {
            List<T> list = new List<T>();
            foreach(var p in Pointers)
            {
                list.Add((T)Activator.CreateInstance(typeof(T), new object[] { p.mAddress }));
            }
            return list.ToArray();
        }

        public AliveObject[] AsAliveObjects
        {
            get
            {
                List<AliveObject> objs = new List<AliveObject>();
                IntPtr objAddr = Marshal.ReadIntPtr(Pointer);

                for (int i = 0; i < Count; i++)
                {
                    objs.Add(new AliveObject(Marshal.ReadIntPtr(objAddr + (4 * i))));
                }

                return objs.ToArray();
            }
        }
    }
}
