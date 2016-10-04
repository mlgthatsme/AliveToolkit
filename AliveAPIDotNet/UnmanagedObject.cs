using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AliveAPIDotNet
{
    public class UnmanagedObject
    {
        public UnmanagedObject(IntPtr addr)
        {
            mAddress = addr;
        }

        internal IntPtr mAddress;

        public virtual int AllocatedSize // This is a malloc hack and can go very wrong.
        {
            get { return Marshal.ReadInt32(mAddress - 0x04); }
        }

        public override bool Equals(object obj)
        {
            UnmanagedObject aliveObj = obj as UnmanagedObject;
            return (aliveObj != null && mAddress == aliveObj.mAddress);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public IntPtr Pointer { get { return mAddress; } }

        public override string ToString()
        {
            return Pointer.ToInt32().ToString("X8");
        }

        public static float HalfFloatToFloat(int halfFloat)
        {
            return halfFloat / (float)0x10000;
        }

        public static int FloatToHalfFloat(float v)
        {
            return (int)(v * 0x10000);
        }

        bool IsPtrInRange(IntPtr ptr)
        {
            return ((ptr.ToInt32() < mAddress.ToInt32() + AllocatedSize) && MemoryHelper.IsMemoryRegionValid(ptr));
        }

        public void SafeWriteInt32(IntPtr addr, int value)
        {
            if (IsPtrInRange(addr))
                Marshal.WriteInt32(addr, value);
        }

        public void SafeWriteInt16(IntPtr addr, short value)
        {
            if (IsPtrInRange(addr))
                Marshal.WriteInt16(addr, value);
        }

        public void SafeWriteByte(IntPtr addr, byte value)
        {
            if (IsPtrInRange(addr))
                Marshal.WriteByte(addr, value);
        }

        public void SafeWriteIntPtr(IntPtr addr, IntPtr value)
        {
            if (IsPtrInRange(addr))
                Marshal.WriteIntPtr(addr, value);
        }

        public int SafeReadInt32(IntPtr addr)
        {
            if (IsPtrInRange(addr))
                return Marshal.ReadInt32(addr);

            return 0;
        }

        public short SafeReadInt16(IntPtr addr)
        {
            if (IsPtrInRange(addr))
                return Marshal.ReadInt16(addr);

            return 0;
        }

        public byte SafeReadByte(IntPtr addr)
        {
            if (IsPtrInRange(addr))
                return Marshal.ReadByte(addr);

            return 0;
        }

        public IntPtr SafeReadIntPtr(IntPtr addr)
        {
            if (IsPtrInRange(addr))
                return Marshal.ReadIntPtr(addr);

            return IntPtr.Zero;
        }
    }
}
