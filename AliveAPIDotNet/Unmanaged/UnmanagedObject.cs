﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using AliveAPIDotNet.Forms;

namespace AliveAPIDotNet.Unmanaged
{
    internal class UnmanagedObjectEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            ObjectEditor editor = new ObjectEditor((UnmanagedObject)value);
            editor.Show();
            return base.EditValue(context, provider, value);
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }

    [Browsable(true)]
    [Editor(typeof(UnmanagedObjectEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class UnmanagedObject
    {
        public UnmanagedObject(IntPtr addr, bool staticMemory = false, int predeterminedSize = 0)
        {
            mAddress = addr;
            mIsStaticMemory = staticMemory;
            mPredeterminedSize = predeterminedSize;
        }

        internal IntPtr mAddress;
        internal bool mIsStaticMemory;
        private int mPredeterminedSize;

        public virtual int AllocatedSize // This is a malloc hack and can go very wrong.
        {
            get { return (mPredeterminedSize == 0) ? Marshal.ReadInt32(mAddress - 0x04) : mPredeterminedSize; }
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

        public virtual IntPtr Pointer { get { return mAddress; }set { mAddress = value; } }

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
            if (mIsStaticMemory)
                return true;

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
