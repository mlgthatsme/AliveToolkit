using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AliveAPIDotNet
{
    public partial class ObjectHexEdit : Form
    {
        public ObjectHexEdit(AliveObject obj)
        {
            InitializeComponent();
            aliveObj = obj;

            hexBox1.ByteProvider = new ObjectByteProvider(aliveObj);
        }

        AliveObject aliveObj;

        private void timer1_Tick(object sender, EventArgs e)
        {
            hexBox1.Invalidate();
        }

        private void hexBox1_SelectionStartChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = new PropertyGridMemorySupplier(aliveObj, (int)hexBox1.SelectionStart);
        }
    }

    public class PropertyGridMemorySupplier
    {
        public PropertyGridMemorySupplier(AliveObject obj, int offset)
        {
            this.obj = obj;
            this.offset = offset;
        }

        int offset;
        AliveObject obj;

        public int Offset
        {
            get
            {
                return offset;
            }
        }

        public string OffsetHex
        {
            get
            {
                return offset.ToString("X");
            }
        }

        public byte Byte
        {
            get
            {
                return obj.SafeReadByte(new IntPtr(obj.Pointer.ToInt32() + offset));
            }
            set
            {
                obj.SafeWriteByte(new IntPtr(obj.Pointer.ToInt32() + offset), value);
            }
        }

        public short Int16
        {
            get
            {
                return obj.SafeReadInt16(new IntPtr(obj.Pointer.ToInt32() + offset));
            }
            set
            {
                obj.SafeWriteInt16(new IntPtr(obj.Pointer.ToInt32() + offset), value);
            }
        }

        public int Int32
        {
            get
            {
                return obj.SafeReadInt32(new IntPtr(obj.Pointer.ToInt32() + offset));
            }
            set
            {
                obj.SafeWriteInt32(new IntPtr(obj.Pointer.ToInt32() + offset), value);
            }
        }

        public float FixedPoint
        {
            get
            {
                return AliveObject.HalfFloatToFloat(obj.SafeReadInt32(new IntPtr(obj.Pointer.ToInt32() + offset)));
            }
            set
            {
                obj.SafeWriteInt32(new IntPtr(obj.Pointer.ToInt32() + offset), AliveObject.FloatToHalfFloat(value));
            }
        }
    }

    public class ObjectByteProvider : IByteProvider
    {
        public ObjectByteProvider(AliveObject obj)
        {
            AbeObject = obj;
        }

        AliveObject AbeObject;

        public byte ReadByte(long index)
        {
            return Marshal.ReadByte(new IntPtr(AbeObject.Pointer.ToInt32() + index));
        }
        public void WriteByte(long index, byte value)
        {
            Marshal.WriteByte(new IntPtr(AbeObject.Pointer.ToInt32() + index), value);
        }
        public void InsertBytes(long index, byte[] bs)
        {

        }
        public void DeleteBytes(long index, long length)
        {

        }

        /// <summary>
        /// Returns the total length of bytes the byte provider is providing.
        /// </summary>
        public long Length { get { return AbeObject.AllocatedSize; } }
        public event EventHandler LengthChanged;
        public bool HasChanges()
        {
            return true;
        }
        public void ApplyChanges()
        {

        }
        public event EventHandler Changed;
        public bool SupportsWriteByte()
        {
            return true;
        }
        public bool SupportsInsertBytes()
        {
            return false;
        }
        public bool SupportsDeleteBytes()
        {
            return false;
        }
    }
}
