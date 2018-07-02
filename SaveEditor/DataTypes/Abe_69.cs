using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SaveEditor.DataTypes
{
    [TypeConverter(typeof(ValueTypeTypeConverter<Abe_69>))]
    public struct Abe_69
    {
        public short field_0_id { get; set; }
        public int field_4_xpos;
        public int field_8_ypos;
        public int field_c_velx;
        public int field_10_vely;
        public short field_14_path_number;
        public short field_16_lvl_number;
        public int field_18_sprite_scale;
        public short field_1C_scale;
        public short field_1e_r;
        public short field_20_g;
        public short field_22_b;
        public short word24;
        public short word26;
        public short word28;
        public short word2A;
        public byte byte2C;
        public byte byte2D;
        public byte byte2E;
        public int field_30_health;
        public short field_34_animation_num;
        public short word36;
        public short word38;
        public short field_3a_collision_line_id;
        public int dword3C;
        public short word40;
        public short word42;
        public byte field_44_is_abe_controlled;
        public int dword48;
        public int dword4C;
        public int dword50;
        public int dword54;
        public int dword58;
        public int dword5C;
        public short word60;
        public short word62;
        public int dword64;
        public int dword68;
        public byte field_6c_rock_bone_count;
        public byte byte6D;
        public byte byte6E;
        public byte byte6F;
        public short word70;
        public short word72;
        public short word74;
        public int dword78;
        public int dword7C;
        public int dword80;
        public int dword84;
        public int dword88;
        public int dword8C;
        public int dword90;
        public int dword94;
        public int dword98;
        public int dword9C;
        public short wordA0;
        public short wordA2;
        public byte byteA4;
        public int dwordA8;
        public short wordAC;
        public short wordAE;
        public short wordB0;
        public short wordB2;
        public short wordB4;
        public short wordB6;
        public short wordB8;
        public short wordBA;
        public short wordBC;
        public short wordBE;
        public short wordC0;
        public short wordC2;
        public short wordC4;
        public short wordC6;
        public short wordC8;
        public byte field_ca_throw_direction;
        public short wordCC;
        public int dwordD0;
        public short wordD4;
        public short wordD6;
    };

    public class ValueTypeTypeConverter<T> : ExpandableObjectConverter where T : struct
    {
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
                throw new ArgumentNullException("propertyValues");

            T ret = default(T);
            object boxed = ret;
            foreach (DictionaryEntry entry in propertyValues)
            {
                PropertyInfo pi = ret.GetType().GetProperty(entry.Key.ToString());
                if (pi != null && pi.CanWrite)
                {
                    pi.SetValue(boxed, Convert.ChangeType(entry.Value, pi.PropertyType), null);
                }
            }
            return (T)boxed;
        }
    }
}