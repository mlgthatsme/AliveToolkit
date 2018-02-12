using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaveEditor
{
    public class QuickSave
    {
        static QuickSave()        {
            ObjectSizes.Add(2, 16);
            ObjectSizes.Add(9, 12);
            ObjectSizes.Add(11, 60);
            ObjectSizes.Add(25, 8);
            ObjectSizes.Add(26, 128);
            ObjectSizes.Add(30, 20);
            ObjectSizes.Add(45, 60);
            ObjectSizes.Add(50, 180);
            ObjectSizes.Add(54, 172);
            ObjectSizes.Add(55, 16);
            ObjectSizes.Add(57, 16);
            ObjectSizes.Add(60, 4);
            ObjectSizes.Add(61, 24);
            ObjectSizes.Add(64, 80);
            ObjectSizes.Add(65, 60);
            ObjectSizes.Add(67, 144);
            ObjectSizes.Add(69, 216);
            ObjectSizes.Add(78, 28);
            ObjectSizes.Add(81, 136);
            ObjectSizes.Add(84, 60);
            ObjectSizes.Add(89, 104);
            ObjectSizes.Add(96, 120);
            ObjectSizes.Add(99, 8);
            ObjectSizes.Add(102, 4);
            ObjectSizes.Add(104, 40);
            ObjectSizes.Add(105, 56);
            ObjectSizes.Add(112, 160);
            ObjectSizes.Add(113, 16);
            ObjectSizes.Add(122, 8);
            ObjectSizes.Add(125, 164);
            ObjectSizes.Add(126, 120);
            ObjectSizes.Add(129, 44);
            ObjectSizes.Add(136, 16);
            ObjectSizes.Add(142, 12);
            ObjectSizes.Add(143, 24);
            ObjectSizes.Add(148, 16);
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class WorldInfo
        {
            public int gnFrame { get; set; }
            public short CurrentLevel { get; set; }
            public short CurrentPath { get; set; }
            public short CurrentCam { get; set; }
            public short Unknown1 { get; set; }
            public short ControlledX { get; set; }
            public short ControlledY { get; set; }
            public short ControlledScale { get; set; }
            public short SavedMudokons { get; set; }
            public short KilledMudokons { get; set; }
            public short MudsInArea { get; set; }
            public byte[] UnknownByteData1 { get; set; }
            public byte byte_5C1A20 { get; set; }
            public byte byte_5C1BFA { get; set; }
            public short word_5C1BBC { get; set; }
            public short word_5C1BF8 { get; set; }
            public short VisitedBoneWerks { get; set; }
            public short VisitedBarracks { get; set; }
            public short VisitedFeecoEnder { get; set; }
            public int dword_5C1BE8 { get; set; }

            public void Parse(BinaryReader reader)
            {
                gnFrame = reader.ReadInt32();
                CurrentLevel = reader.ReadInt16();
                CurrentPath = reader.ReadInt16();
                CurrentCam = reader.ReadInt16();
                Unknown1 = reader.ReadInt16();

                ControlledX = reader.ReadInt16();
                ControlledY = reader.ReadInt16();
                ControlledScale = reader.ReadInt16();

                SavedMudokons = reader.ReadInt16();
                KilledMudokons = reader.ReadInt16();
                MudsInArea = reader.ReadInt16();

                UnknownByteData1 = reader.ReadBytes(20);

                byte_5C1A20 = reader.ReadByte();
                byte_5C1BFA = reader.ReadByte();
                word_5C1BBC = reader.ReadInt16();
                word_5C1BF8 = reader.ReadInt16();
                VisitedBoneWerks = reader.ReadInt16();
                VisitedBarracks = reader.ReadInt16();
                VisitedFeecoEnder = reader.ReadInt16();
                dword_5C1BE8 = reader.ReadInt32();
            }

            public void Serialize(BinaryWriter writer)
            {
                writer.Write(gnFrame);
                writer.Write(CurrentLevel);
                writer.Write(CurrentPath);
                writer.Write(CurrentCam);
                writer.Write(Unknown1);

                writer.Write(ControlledX);
                writer.Write(ControlledY);
                writer.Write(ControlledScale);

                writer.Write(SavedMudokons);
                writer.Write(KilledMudokons);
                writer.Write(MudsInArea);

                writer.Write(UnknownByteData1);

                writer.Write(byte_5C1A20);
                writer.Write(byte_5C1BFA);
                writer.Write(word_5C1BBC);
                writer.Write(word_5C1BF8);
                writer.Write(VisitedBoneWerks);
                writer.Write(VisitedBarracks);
                writer.Write(VisitedFeecoEnder);
                writer.Write(dword_5C1BE8);
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class SavedObject
        {
            public short ID { get; set; }
            public byte[] Data { get; set; }

            public override string ToString()
            {
                return $"{AliveAPIDotNet.AliveObject.GetFriendlyName(ID)} | {ID}";
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class RestartPathState
        {
            public WorldInfo World { get; set; } = new WorldInfo();
            public byte[] SwitchStates { get; set; } = new byte[256];
            public SavedObject AbeState { get; set; }
        }

        static Dictionary<int, int> ObjectSizes = new Dictionary<int, int>();

        private List<SavedObject> _objects = new List<SavedObject>();

        // Variables
        public string SaveName { get; set; }
        public string SaveTime { get; set; }
        public RestartPathState RestartPathSave { get; set; } = new RestartPathState();
        public WorldInfo World { get; set; } = new WorldInfo();
        public SavedObject[] Objects { get { return _objects.ToArray(); } } // So property editor lets us expand the list.
        public byte[] SwitchStates { get; set; } = new byte[256];

        public int AccumulatedObjectsCount { get; set; }

        public QuickSave(Stream stream)
        {
            ParseSave(stream);
        }

        void ParseObjects(BinaryReader reader)
        {
            while(true)
            {
                short objectID = reader.ReadInt16();
                if (objectID == 0)
                    break;

                if (ObjectSizes.ContainsKey(objectID))
                {
                    byte[] data = reader.ReadBytes(ObjectSizes[objectID] - 2);
                    _objects.Add(new SavedObject() { ID = objectID, Data = data });
                }
                else
                {
                    MessageBox.Show($"Unknown object type: {objectID}");
                }
            }
        }

        void SerializeObjects(BinaryWriter writer)
        {
            foreach(var o in _objects)
            {
                writer.Write(o.ID);
                writer.BaseStream.Write(o.Data, 0, o.Data.Length);
            }
        }

        string ReadPSXString(BinaryReader reader, int length)
        {
            string r = "";

            for(int i = 0; i < length;i++)
            {
                int t = reader.ReadByte();
                byte b = reader.ReadByte();
                char c = (char)(b - 31);

                if (!char.IsNumber(c) && !char.IsLetter(c))
                {
                    if (b == 70)
                        r += ':';
                    else if (b == 103)
                        r += '"';
                    else
                        r += '?'; // Unknown char
                }
                else
                    r += c;
            }

            return r;
        }

        void WritePSXString(BinaryWriter writer, string s)
        {
            for(int i = 0; i < s.Length;i++)
            {
                writer.BaseStream.WriteByte((byte)0x82);

                char c = s[i];

                if (c == ':')
                    writer.BaseStream.WriteByte((byte)70);
                else if (c == '"')
                    writer.BaseStream.WriteByte((byte)103);
                else
                    writer.BaseStream.WriteByte((byte)(c + 31));
            }
        }

        void ParseSave(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);

            reader.BaseStream.Position = OffsetPSXSaveName;
            SaveName = ReadPSXString(reader, 8);
            reader.BaseStream.Position = OffsetPSXSaveTime;
            SaveTime = ReadPSXString(reader, 6);

            reader.BaseStream.Position = OffsetAccumulatedObjectsCount;
            AccumulatedObjectsCount = reader.ReadInt32();

            reader.BaseStream.Position = OffsetWorldState;
            World.Parse(reader);

            reader.BaseStream.Position = OffsetSwitchState;
            SwitchStates = reader.ReadBytes(256);

            reader.BaseStream.Position = OffsetObjects;
            ParseObjects(reader);

            // Path state
            reader.BaseStream.Position = OffsetPATHAbeState;
            short objectID = reader.ReadInt16();
            byte[] data = reader.ReadBytes(ObjectSizes[objectID] - 2);
            RestartPathSave.AbeState = new SavedObject() { ID = objectID, Data = data };

            reader.BaseStream.Position = OffsetPATHSwitchState;
            RestartPathSave.SwitchStates = reader.ReadBytes(256);

            reader.BaseStream.Position = OffsetPATHWorldState;
            RestartPathSave.World.Parse(reader);
        }

        const int OffsetPSXSaveName = 0x20;
        const int OffsetPSXSaveTime = 0x32;
        const int OffsetAccumulatedObjectsCount = 0x200;
        const int OffsetWorldState = 0x204;
        const int OffsetSwitchState = 0x45C;
        const int OffsetObjects = 0x55C;

        const int OffsetPATHWorldState = 0x244;
        const int OffsetPATHAbeState = 0x284;
        const int OffsetPATHSwitchState = 0x35C;

        public void SerializeSave(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(new byte[8192], 0, 8192);
            ms.Position = 0;

            BinaryWriter writer = new BinaryWriter(ms);

            writer.BaseStream.Position = OffsetPSXSaveName;
            WritePSXString(writer, SaveName);
            writer.BaseStream.Position = OffsetPSXSaveTime;
            WritePSXString(writer, SaveTime);

            writer.BaseStream.Position = OffsetAccumulatedObjectsCount;
            writer.Write(AccumulatedObjectsCount);

            writer.BaseStream.Position = OffsetWorldState;
            World.Serialize(writer);

            writer.BaseStream.Position = OffsetSwitchState;
            writer.Write(SwitchStates);

            writer.BaseStream.Position = OffsetObjects;
            SerializeObjects(writer);

            // Path state
            writer.BaseStream.Position = OffsetPATHAbeState;
            writer.Write(RestartPathSave.AbeState.ID);
            writer.Write(RestartPathSave.AbeState.Data);

            writer.BaseStream.Position = OffsetPATHSwitchState;
            writer.Write(RestartPathSave.SwitchStates);

            writer.BaseStream.Position = OffsetPATHWorldState;
            RestartPathSave.World.Serialize(writer);

            ms.WriteTo(stream);
        }
    }
}