using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliveAPIDotNet
{
    public enum GameTypes
    {
        Oddysee = 1,
        Exoddus = 2,
    }

    public class SpawnEntry
    {
        public SpawnEntry(int pathId, string name, byte[] paramsData, params string[] resources)
        {
            PathID = pathId;
            Name = name;
            Resources = resources;
            ParamsData = paramsData;
        }

        public SpawnEntry(int pathId, string name, byte[] paramsData, int width, int height, params string[] resources)
        {
            PathID = pathId;
            Name = name;
            Resources = resources;
            ParamsData = paramsData;
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return Name;
        }

        public byte[] ParamsData;
        public int PathID;
        public string Name;
        public string[] Resources;

        public int Width = 25;
        public int Height = 25;
    }

    public class GameConfiguration
    {
        public static GameConfiguration Instance;

        // Main Game Config
        public GameTypes GameType = GameTypes.Exoddus;

        public int AddressSwitches = 0x005C1A28;
        public int AddressPlayerPointer = 0x5C1B8C;
        public int AddressScreenInfo = 0x5BB5F4;
        public int AddressPathData = 0x005C1128;
        public int AddressObjectList = 0x00BB47C4;
        public int AddressObjectListActive = 0x005C1124;
        public int AddressLevelConfigs = 0x00559660;

        // Offsets
        public int OffsetObjectPositionX = 0xB8;
        public int OffsetObjectPositionY = 0xBC;
        public int OffsetObjectVelocityX = 0xC4;
        public int OffsetObjectVelocityY = 0xC8;
        public int OffsetObjectScale = 0xCC;
        public int OffsetObjectCurrentFloor = 0x100;
        public int OffsetObjectAliveState = 0x106;

        public int HeroTypeID = 69;

        // Spawning

        static byte[] CreateMudParams()
        {
            byte[] p = new byte[128];
            for (int i = 0; i < p.Length; i++)
            {
                p[i] = 0;
            }


            return p;
        }

        static byte[] CreateFlySligParams()
        {
            byte[] p = new byte[32];
            for (int i = 0; i < p.Length; i++)
            {
                p[i] = 0;
            }

            p[5] = 60;
            p[7] = 30;
            p[9] = 60;
            p[13] = 45;
            p[15] = 120;
            p[17] = 30;
            p[19] = 1;
            p[21] = 90;
            p[23] = 20;
            p[25] = 12;
            p[29] = 1;

            return p;
        }

        static byte[] CreateCustomParams(params int[] offsets)
        {
            byte[] b = new byte[100];
            foreach (int i in offsets)
            {
                b[i] = 1;
            }
            return b;
        }

        public SpawnEntry[] SpawnEntries = {
            new SpawnEntry(10, "Rock Sack",null, "RTHROW.BND"),
            new SpawnEntry(15, "Slig",null, "SLGLEVER.BAN","SLGLIFT.BAN","SLGSLEEP.BAN","SLGEDGE.BAN","SLGSMASH.BAN","SLGBEAT.BAN","SLGKNFD.BAN","SLIGZ.BND","SLIG.BND", "SLGBLOW.BAN"),
            new SpawnEntry(16, "Slog",null, "SLOG.BND", "DOGKNFD.BAN"),
            new SpawnEntry(17, "Switch",null, 75, 25, "SWITCH1.BAN", "ABEPULL.BAN"),
            new SpawnEntry(19, "Anti Chant Orb", null, "MAIMORB.BAN","SPLINE.BAN", "SPARKS.BAN", "METAL.BAN", "EXPLO2.BAN"),
            new SpawnEntry(24, "Mine", null, "MINE.BND","EXPLODE.BND"),
            new SpawnEntry(25, "UXB", null, "UXB.BND","EXPLODE.BND"),
            new SpawnEntry(26, "Paramite", null, "PARAMITE.BND"),
            new SpawnEntry(27, "Stone", null, "ABESTONE.BAN"),
            new SpawnEntry(28, "Bird Portal", null, "PORTAL.BND", "SHRYPORT.BND"),
            new SpawnEntry(38, "Electric Wall", CreateCustomParams(4), "ELECWALL.BAN"),
            new SpawnEntry(40, "Meat Sack",null, "MTHROW.BND"),
            new SpawnEntry(41, "Scrab",null, "SCRAB.BND"),
            new SpawnEntry(49, "Mudokon", CreateMudParams(), "ABEBSIC1.BAN","ABEKNFD.BAN","ABEKNBK.BAN","ABEEDGE.BAN","MUDIDLE.BAN","MUDPAL.BND", "MUDSCRUB.BAN","MUDCHSL.BAN","MUDWORK.BND"),
            new SpawnEntry(82, "Flying Slig", CreateFlySligParams(), "FLYSLIG.BND", "SLGBLOW.BAN", "GRENADE.BAN", "SMEXP.BAN","METAL.BAN", "BIGFLASH.BAN", "VAPOR.BAN"),
            new SpawnEntry(83, "Fleech",null, "FLEECH.BAN", "FLEEBLOW.BAN"),
            new SpawnEntry(84, "Slurg",null, "SLURG.BAN"),
            new SpawnEntry(93, "Mine Car",null, "BAYROLL.BAN", "ABECAR.BAN", "METAL.BAN", "EXPLO2.BAN"),
            new SpawnEntry(94, "Bonebag",null, "BONEBAG.BAN", "BTHROW.BND"),
            new SpawnEntry(98, "Ghost Trap",null, "GHOSTTRP.BAN"),
            new SpawnEntry(101, "Fart Machine",null, "ABESTONE.BAN", "LCDFONT.FNT", "BREWBTN.BAN", "EVILFART.BAN", "EXPLO2.BAN"),
            new SpawnEntry(103, "Naked Slig",null, "CRAWLSLG.BND", "GRENADE.BAN", "SLGBLOW.BAN", "SMEXP.BAN", "METAL.BAN", "BIGFLASH.BAN", "VAPOR.BAN"),
            new SpawnEntry(106, "Greeter",null, "MFLARE.BAN", "MOTION.BAN", "GREETER.BAN", "SPLINE.BAN", "EXPLO2.BAN"),
        };
    }
}
