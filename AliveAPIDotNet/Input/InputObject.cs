using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AliveAPIDotNet;

namespace AliveAPIDotNet.Input
{
    public class InputObject : UnmanagedObject
    {
        public InputObject(IntPtr addr) : base(addr, true, 0x44)
        {
            Pads = new InputPad[] { new InputPad(addr), new InputPad(addr + 0x18) };
        }

        /// <summary>
        /// Contains two pads. For player 1 and 2.
        /// </summary>
        public InputPad[] Pads { get; private set; }

        // Todo, rest of input object
    }
}
