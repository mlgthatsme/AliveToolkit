using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AliveAPIDotNet
{
    [Browsable(true)]
    [Editor(typeof(UnmanagedObjectEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class AliveObject : UnmanagedObject
    {
        public AliveObject(IntPtr addr) : base(addr)
        {

        }

        public static string GetVTableName(IntPtr vtable)
        {
            switch(vtable.ToInt32())
            {
                case 0x00545EBC:
                    return "Resource Mgr";
                case 0x005463C0:
                    return "Music Mgr";
                case 0x005441E4:
                    return "Screen Mgr";
                case 0x00544518:
                    return "DDCheat";
                case 0x00544B44:
                    return "Cheats Mgr";
            }
            return vtable.ToInt32().ToString("X8");
        }

        public static string GetFriendlyName(int type)
        {
            if (GameConfiguration.Instance.GameType == GameTypes.Oddysee)
            {
                switch (type)
                {
                    case 43:
                        return "Hero";
                    default:
                        return "Unknown";
                }
            }
            else
            {
                switch (type)
                {
                    case 0:
                        return "No ID";
                    case 7:
                        return "Animation";
                    case 13:
                        return "Brew Machine";
                    case 30:
                        return "Grinder";
                    case 31:
                        return "Laughing Gas";
                    case 33:
                        return "Door";
                    case 34:
                        return "Door Lock";
                    case 35:
                        return "Bird";
                    case 39:
                        return "Electrocute";
                    case 48:
                        return "Rock Spawner";
                    case 50:
                        return "Fleech";
                    case 53:
                        return "Item Count";
                    case 54:
                        return "Flying Slig";
                    case 61:
                        return "Locked Soul";
                    case 64:
                        return "Greeter";
                    case 67:
                        return "Gluckon";
                    case 68:
                        return "Help Phone";
                    case 69:
                        return "Hero";
                    case 78:
                        return "Pulley";
                    case 83:
                        return "Anti Chant";
                    case 84:
                        return "Meat";
                    case 85:
                        return "Meat Sack";
                    case 88:
                        return "Mine";
                    case 91:
                        return "Greeter Body";
                    case 95:
                        return "Pause Menu";
                    case 96:
                        return "Paramite";
                    case 103:
                        return "Pull Rope";
                    case 105:
                        return "Rock";
                    case 106:
                        return "Rock Sack";
                    case 110:
                        return "Mudokon";
                    case 111:
                        return "Red Laser";
                    case 112:
                        return "Scrab";
                    case 118:
                        return "Screen Shake";
                    case 122:
                        return "Gate";
                    case 124:
                        return "Snooz Particle";
                    case 125:
                        return "Slig";
                    case 126:
                        return "Slog";
                    case 129:
                        return "Slug";
                    case 134:
                        return "Particle";
                    case 139:
                        return "Lever";
                    case 142:
                        return "Trapdoor";
                    case 143:
                        return "UXB";
                    case 146:
                        return "Web";
                    default:
                        return "Unknown";
                }
            }
        }

        // Lift
        // 0x124 - Pointer to floor collider

        public AliveObject Duplicate()
        {
            int memSize = AllocatedSize;
            IntPtr alloc = Marshal.AllocHGlobal(memSize + 4);
            Marshal.WriteInt32(alloc, memSize);
            byte[] cloneData = new byte[memSize];
            Marshal.Copy(Pointer, cloneData, 0, memSize);
            Marshal.Copy(cloneData, 0, alloc + 4, memSize);
            return new AliveAPIDotNet.AliveObject(alloc + 4);
        }

        public void Destroy()
        {
            ObjectState |= 4;
        }

        public override string ToString()
        {
            return string.Format("{5} [{4}, {2}, {3}]: {0} {1}", PositionX.ToString("0.00"), PositionY.ToString("0.00"), ObjectID, AllocatedSize, GetVTableName(VTable), GetFriendlyName(ObjectID));
        }

        // Variables

        public IntPtr VTable
        {
            get { return SafeReadIntPtr(mAddress); }
            set { SafeWriteIntPtr(mAddress, value); }
        }

        public UnmanagedObjectPointer[] LoadedBans
        {
            get { return new AliveObjectList(mAddress + 0x10, false).Pointers; }
        }

        //public byte ThrowableCount
        //{
        //    get { return SafeReadByte(mAddress + 0x1A2); }
        //    set { SafeWriteByte(mAddress + 0x1A2, value); }
        //}

        public short ObjectID
        {
            get { return SafeReadInt16(mAddress + 0x04); }
            set { SafeWriteInt16(mAddress + 0x04, value); }
        }

        public short AliveState
        {
            get { return SafeReadInt16(mAddress + GameConfiguration.Instance.OffsetObjectAliveState); }
            set { SafeWriteInt16(mAddress + GameConfiguration.Instance.OffsetObjectAliveState, value); }
        }

        public byte ObjectState
        {
            get { return SafeReadByte(mAddress + 0x06); }
            set { SafeWriteByte(mAddress + 0x06, value); }
        }

        public PathLineObject FloorCollider
        {
            get { IntPtr a = SafeReadIntPtr(mAddress + GameConfiguration.Instance.OffsetObjectCurrentFloor); return (a == IntPtr.Zero) ? null : new PathLineObject(a); }
            set { SafeWriteIntPtr(mAddress + GameConfiguration.Instance.OffsetObjectCurrentFloor, (value != null) ? value.Pointer : IntPtr.Zero); }
        }

        public float PositionX
        {
            get { return HalfFloatToFloat(SafeReadInt32(mAddress + GameConfiguration.Instance.OffsetObjectPositionX)); }
            set { SafeWriteInt32(mAddress + GameConfiguration.Instance.OffsetObjectPositionX, FloatToHalfFloat(value)); }
        }

        public float PositionY
        {
            get { return HalfFloatToFloat(SafeReadInt32(mAddress + GameConfiguration.Instance.OffsetObjectPositionY)); }
            set { SafeWriteInt32(mAddress + GameConfiguration.Instance.OffsetObjectPositionY, FloatToHalfFloat(value)); }
        }

        public float VelocityX
        {
            get { return HalfFloatToFloat(SafeReadInt32(mAddress + GameConfiguration.Instance.OffsetObjectVelocityX)); }
            set { SafeWriteInt32(mAddress + GameConfiguration.Instance.OffsetObjectVelocityX, FloatToHalfFloat(value)); }
        }

        public float VelocityY
        {
            get { return HalfFloatToFloat(SafeReadInt32(mAddress + GameConfiguration.Instance.OffsetObjectVelocityY)); }
            set { SafeWriteInt32(mAddress + GameConfiguration.Instance.OffsetObjectVelocityY, FloatToHalfFloat(value)); }
        }

        public float Scale
        {
            get { return HalfFloatToFloat(SafeReadInt32(mAddress + GameConfiguration.Instance.OffsetObjectScale)); }
            set { SafeWriteInt32(mAddress + GameConfiguration.Instance.OffsetObjectScale, FloatToHalfFloat(value)); }
        }
    }
}