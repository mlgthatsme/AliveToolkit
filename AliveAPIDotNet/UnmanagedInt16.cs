using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AliveAPIDotNet
{
    public class UnmanagedInt16 : UnmanagedObject
    {
        public UnmanagedInt16(IntPtr addr) : base(addr, true, 2)
        {
        }

        public Int16 Value { get { return this; } set { Set(value); } }
        
        public void Set(Int16 v)
        {
            SafeWriteInt16(mAddress, v);
        }

        public static implicit operator Int16(UnmanagedInt16 op)
        {
            return op.SafeReadInt16(op.mAddress);
        }

        public override string ToString()
        {
            return ((int)this).ToString();
        }
    }
}
