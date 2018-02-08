using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AliveAPIDotNet.Unmanaged
{
    public class UnmanagedString : UnmanagedObject
    {
        public UnmanagedString(IntPtr addr) : base(addr)
        {
        }

        public override string ToString()
        {
            return ReadString(mAddress);
        }

        static string ReadString(IntPtr ptr)
        {
            if (ptr != IntPtr.Zero)
            {
                string s = "";

                IntPtr currentOffset = ptr;

                while(true)
                {
                    byte c = Marshal.ReadByte(currentOffset);
                    if (c == 0)
                        return s;

                    s += (char)c;
                    currentOffset = currentOffset + 1;
                }
            }

            return "";
        }

        public static implicit operator string(UnmanagedString str)
        {
            return ReadString(str.mAddress);
        }
    }
}
