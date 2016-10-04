using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AliveAPIDotNet
{
    class MemoryHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            internal PROCESSOR_INFO_UNION p;
            public uint dwPageSize;
            public UIntPtr lpMinimumApplicationAddress;
            public UIntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        };

        [StructLayout(LayoutKind.Explicit)]
        public struct PROCESSOR_INFO_UNION
        {
            [FieldOffset(0)]
            internal uint dwOemId;
            [FieldOffset(0)]
            internal ushort wProcessorArchitecture;
            [FieldOffset(2)]
            internal ushort wReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public UIntPtr BaseAddress;
            public UIntPtr AllocationBase;
            public uint AllocationProtect;
            public UIntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        [DllImport("kernel32.dll")]
        private static extern UIntPtr VirtualQuery(
            UIntPtr lpAddress,
            out MEMORY_BASIC_INFORMATION lpBuffer,
            UIntPtr dwLength
        );

        public static bool IsMemoryRegionValid(IntPtr ptr)
        {
            MEMORY_BASIC_INFORMATION mbi = new MEMORY_BASIC_INFORMATION();

            if (VirtualQuery(new UIntPtr((uint)ptr.ToInt32()),
                                 out mbi,
                                 (UIntPtr)System.Runtime.InteropServices.Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))) != UIntPtr.Zero)
            {
                return (mbi.State != 0x2000); // TODO: Check and make sure this is correct
            }
            else
            {
                return false;
            }
        }
    }
}
