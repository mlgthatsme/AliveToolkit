using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AliveAPIDotNet;

namespace AliveAPIDotNet.Input
{
    public class InputPad : UnmanagedObject
    {
        public InputPad(IntPtr addr) : base(addr, true, 0x18)
        {
            Pressed = new UnmanagedInt32(addr);
        }

        /// <summary>
        /// Raw pressed input.
        /// </summary>
        public UnmanagedInt32 Pressed { get; private set; }

        //DWORD mPressed;
        //BYTE field_4_dir;
        //BYTE field_5;
        //WORD field_6_padding;
        //DWORD field_8_previous;
        //DWORD field_C_held;
        //DWORD field_10_released;
        //DWORD field_14_padding;
    }
}
