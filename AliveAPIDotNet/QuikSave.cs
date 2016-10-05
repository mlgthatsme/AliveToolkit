using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliveAPIDotNet
{
    public class QuikSave
    {
        public QuikSave(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; private set; }
    }
}
