using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AliveAPIDotNet
{
    public class UnmanagedInt32 : UnmanagedObject
    {
        public UnmanagedInt32(IntPtr addr) : base(addr, true, 4)
        {
        }

        public Int32 Value { get { return this; } set { Set(value); } }
        
        public void Set(Int32 v)
        {
            SafeWriteInt32(mAddress, v);
        }

        public static implicit operator Int32(UnmanagedInt32 op)
        {
            return op.SafeReadInt32(op.mAddress);
        }

        public override string ToString()
        {
            return ((int)this).ToString();
        }
    }
}
