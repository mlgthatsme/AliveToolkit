using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Be.Windows.Forms;
using System.Runtime.InteropServices;

namespace AliveAPIDotNet
{
    public partial class ObjectEditor : Form
    {
        public ObjectEditor(AliveObject obj)
        {
            InitializeComponent();
            aliveObj = obj;
        }

        AliveObject aliveObj;

        private void ObjectEditor_Load(object sender, EventArgs e)
        {
            Text = string.Format("Editing {1} at {0}", aliveObj.Pointer.ToString("X"), AliveObject.GetFriendlyName(aliveObj.ObjectID));

            propertyGrid1.SelectedObject = aliveObj;
            //hexBox1.ByteProvider = new ObjectByteProvider(aliveObj);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //hexBox1.Invalidate();
        }
    }

    class ObjectByteProvider : IByteProvider
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
