using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using AliveAPIDotNet.Unmanaged;

namespace AliveAPIDotNet
{
    public class LevelEntryList : UnmanagedObject
    {
        public LevelEntryList(IntPtr addr) : base(addr, true, 1020)
        {
            Levels = new UnmanagedArray<LevelEntry>(mAddress, 60, 17);
        }

        public class LevelPathEntry : UnmanagedObject
        {
            public LevelPathEntry(IntPtr addr) : base(addr, true, 16)
            {
                PathFileName = new UnmanagedString(SafeReadIntPtr(addr));
                PathConstructorTable = new UnmanagedInt32(addr + 4);
                Unknown1 = new UnmanagedInt32(addr + 8);
                Unknown2 = new UnmanagedInt32(addr + 12);
            }

            public UnmanagedString PathFileName { get; private set; }
            public UnmanagedInt32 PathConstructorTable { get; private set; }
            public UnmanagedInt32 Unknown1 { get; private set; }
            public UnmanagedInt32 Unknown2 { get; private set; }

            public override string ToString()
            {
                return PathFileName;
            }
        }

        public class LevelEntry : UnmanagedObject
        {
            public LevelEntry(IntPtr addr) : base(addr, true, 60)
            {
                MovieList = SafeReadIntPtr(addr + 4);
                VabList = SafeReadIntPtr(addr + 8);
                SeqFile = new UnmanagedString(SafeReadIntPtr(addr + 12));
                Unknown1 = new UnmanagedInt32(addr + 16);
                Name = new UnmanagedString(SafeReadIntPtr(addr + 20));
                PathCount = new UnmanagedInt16(addr + 24);
                Unknown2 = new UnmanagedInt16(addr + 26); 
                Unknown3 = new UnmanagedInt32(addr + 28);
                LvlPath = new UnmanagedString(SafeReadIntPtr(addr + 32)); 
                Unknown4 = new UnmanagedInt32(addr + 36); 
                OvlPath = new UnmanagedString(SafeReadIntPtr(addr + 40)); 
                Unknown5 = new UnmanagedInt32(addr + 44); 
                MovPath = new UnmanagedString(SafeReadIntPtr(addr + 48));
                IdxFile = new UnmanagedString(SafeReadIntPtr(addr + 52));
                PathFile = new UnmanagedString(SafeReadIntPtr(addr + 56));

                PathList = new UnmanagedArray<LevelPathEntry>(SafeReadIntPtr(addr) + 16, 16, PathCount);
            }

            public LevelPathEntry[] PathList { get; private set; }
            public IntPtr MovieList { get; private set; }
            public IntPtr VabList { get; private set; }
            public UnmanagedString SeqFile { get; private set; }
            public UnmanagedInt32 Unknown1 { get; private set; }
            public UnmanagedString Name { get; private set; }
            public UnmanagedInt16 PathCount { get; private set; }
            public UnmanagedInt16 Unknown2 { get; private set; }
            public UnmanagedInt32 Unknown3 { get; private set; }
            public UnmanagedString LvlPath { get; private set; }
            public UnmanagedInt32 Unknown4 { get; private set; }
            public UnmanagedString OvlPath { get; private set; }
            public UnmanagedInt32 Unknown5 { get; private set; }
            public UnmanagedString MovPath { get; private set; }
            public UnmanagedString IdxFile { get; private set; }
            public UnmanagedString PathFile { get; private set; }

            public override string ToString()
            {
                return Name;
            }

            public const int Size = 60;
        }

        public LevelEntry[] Levels { get; protected set; }
    }
}
