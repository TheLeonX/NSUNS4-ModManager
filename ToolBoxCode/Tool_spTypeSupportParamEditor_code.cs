using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_spTypeSupportParamEditor_code {
        public bool FileOpen = false;
        public string FilePath = "";
        public byte[] fileBytes = new byte[0];
        public int EntryCount = 0;

        public class spTypeSupportParamEntry {
            public int characode;
            public int type;
            public int mode;
            public string leftSkillName;
            public string rightSkillName;
            public string upSkillName;
            public string downSkillName;
            public int leftSkill_unk1;
            public int leftSkill_unk2;
            public int leftSkill_unk3;
            public bool leftSkill_EnableInAir;
            public bool leftSkill_EnableInGround;
            public bool leftSkill_unk4;
            public int rightSkill_unk1;
            public int rightSkill_unk2;
            public int rightSkill_unk3;
            public bool rightSkill_EnableInAir;
            public bool rightSkill_EnableInGround;
            public bool rightSkill_unk4;
            public int upSkill_unk1;
            public int upSkill_unk2;
            public int upSkill_unk3;
            public bool upSkill_EnableInAir;
            public bool upSkill_EnableInGround;
            public bool upSkill_unk4;
            public int downSkill_unk1;
            public int downSkill_unk2;
            public int downSkill_unk3;
            public bool downSkill_EnableInAir;
            public bool downSkill_EnableInGround;
            public bool downSkill_unk4;
        }

        public List<spTypeSupportParamEntry> spTypeSupportParam = new List<spTypeSupportParamEntry>();

        public void OpenFile(string basepath = "") {
            OpenFileDialog o = new OpenFileDialog();
            {
                o.DefaultExt = ".xfbin";
                o.Filter = "*.xfbin|*.xfbin";
            }
            if (basepath != "") {
                o.FileName = basepath;
            } else {
                o.ShowDialog();
            }
            if (!(o.FileName != "") || !File.Exists(o.FileName)) {
                return;
            }
            ClearFile();
            FileOpen = true;
            FilePath = o.FileName;
            fileBytes = File.ReadAllBytes(FilePath);
            EntryCount = MainFunctions.b_ReadInt(fileBytes, 304);

            for (int x2 = 0; x2 < EntryCount; x2++) {
                spTypeSupportParamEntry entry = new spTypeSupportParamEntry();
                long _ptr = 316 + 0xB0 * x2;
                entry.characode = MainFunctions.b_ReadInt(fileBytes, (int)_ptr);
                entry.mode = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x04);
                entry.type = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x08);

                long _ptrUpSkillName = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x10);
                entry.upSkillName = MainFunctions.b_ReadString(fileBytes, (int)_ptr + 0x10 + (int)_ptrUpSkillName);
                entry.upSkill_EnableInGround = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x18));
                entry.upSkill_EnableInAir = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x1C));
                entry.upSkill_unk4 = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x20));
                entry.upSkill_unk1 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x24);
                entry.upSkill_unk2 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x28);
                entry.upSkill_unk3 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x2C);

                long _ptrDownSkillName = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x38);
                entry.downSkillName = MainFunctions.b_ReadString(fileBytes, (int)_ptr + 0x38 + (int)_ptrDownSkillName);
                entry.downSkill_EnableInGround = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x40));
                entry.downSkill_EnableInAir = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x44));
                entry.downSkill_unk4 = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x48));
                entry.downSkill_unk1 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x4C);
                entry.downSkill_unk2 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x50);
                entry.downSkill_unk3 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x54);

                long _ptrLeftSkillName = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x60);
                entry.leftSkillName = MainFunctions.b_ReadString(fileBytes, (int)_ptr + 0x60 + (int)_ptrLeftSkillName);
                entry.leftSkill_EnableInGround = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x68));
                entry.leftSkill_EnableInAir = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x6C));
                entry.leftSkill_unk4 = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x70));
                entry.leftSkill_unk1 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x74);
                entry.leftSkill_unk2 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x78);
                entry.leftSkill_unk3 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x7C);

                long _ptrRightSkillName = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x88);
                entry.rightSkillName = MainFunctions.b_ReadString(fileBytes, (int)_ptr + 0x88 + (int)_ptrRightSkillName);
                entry.rightSkill_EnableInGround = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x90));
                entry.rightSkill_EnableInAir = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x94));
                entry.rightSkill_unk4 = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x98));
                entry.rightSkill_unk1 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x9C);
                entry.rightSkill_unk2 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0xA0);
                entry.rightSkill_unk3 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0xA4);



                spTypeSupportParam.Add(entry);


            }
        }
        public void ClearFile() {
            FileOpen = false;
            FilePath = "";
            fileBytes = new byte[0];
            EntryCount = 0;
            spTypeSupportParam.Clear();
        }
        public void CloseFile() {
            ClearFile();
            FileOpen = false;
            FilePath = "";
        }
        public void SaveFileAs(string basepath = "") {
            SaveFileDialog s = new SaveFileDialog();
            {
                s.DefaultExt = ".xfbin";
                s.Filter = "*.xfbin|*.xfbin";
            }
            if (basepath != "")
                s.FileName = basepath;
            else
                s.ShowDialog();
            if (!(s.FileName != "")) {
                return;
            }
            if (s.FileName == FilePath) {
                if (File.Exists(FilePath + ".backup")) {
                    File.Delete(FilePath + ".backup");
                }
                File.Copy(FilePath, FilePath + ".backup");
            } else {
                FilePath = s.FileName;
            }
            File.WriteAllBytes(FilePath, ConvertToFile());
            if (basepath == "")
                MessageBox.Show("File saved to " + FilePath + ".");
        }

        public byte[] ConvertToFile() {
            byte[] file = new byte[0];
            byte[] header = new byte[316]
            {
                0x4E,0x55,0x43,0x43,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xE8,0x00,0x00,0x00,0x03,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x3B,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x23,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x20,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x30,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x4E,0x75,0x6C,0x6C,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x42,0x69,0x6E,0x61,0x72,0x79,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x50,0x61,0x67,0x65,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x49,0x6E,0x64,0x65,0x78,0x00,0x00,0x62,0x69,0x6E,0x5F,0x6C,0x65,0x2F,0x78,0x36,0x34,0x2F,0x73,0x70,0x54,0x79,0x70,0x65,0x53,0x75,0x70,0x70,0x6F,0x72,0x74,0x50,0x61,0x72,0x61,0x6D,0x2E,0x62,0x69,0x6E,0x00,0x00,0x73,0x70,0x54,0x79,0x70,0x65,0x53,0x75,0x70,0x70,0x6F,0x72,0x74,0x50,0x61,0x72,0x61,0x6D,0x00,0x50,0x61,0x67,0x65,0x30,0x00,0x69,0x6E,0x64,0x65,0x78,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x3F,0xC4,0x00,0x00,0x00,0x01,0x00,0x79,0x00,0x00,0x00,0x00,0x3F,0xC0,0xE9,0x03,0x00,0x00,0x4E,0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            };

            file = MainFunctions.b_AddBytes(file, header);
            file = MainFunctions.b_AddBytes(file, new byte[0xB0 * EntryCount]);

            List<int> UpSkillNamePointer = new List<int>();
            List<int> DownSkillNamePointer = new List<int>();
            List<int> LeftSkillNamePointer = new List<int>();
            List<int> RightSkillNamePointer = new List<int>();

            for (int x2 = 0; x2 < EntryCount; x2++) {
                UpSkillNamePointer.Add(file.Length);
                if (spTypeSupportParam[x2].upSkillName != "") {
                    file = MainFunctions.b_AddBytes(file, Encoding.ASCII.GetBytes(spTypeSupportParam[x2].upSkillName));
                    file = MainFunctions.b_AddBytes(file, new byte[1]);
                }
                DownSkillNamePointer.Add(file.Length);
                if (spTypeSupportParam[x2].downSkillName != "") {
                    file = MainFunctions.b_AddBytes(file, Encoding.ASCII.GetBytes(spTypeSupportParam[x2].downSkillName));
                    file = MainFunctions.b_AddBytes(file, new byte[1]);
                }
                LeftSkillNamePointer.Add(file.Length);
                if (spTypeSupportParam[x2].leftSkillName != "") {
                    file = MainFunctions.b_AddBytes(file, Encoding.ASCII.GetBytes(spTypeSupportParam[x2].leftSkillName));
                    file = MainFunctions.b_AddBytes(file, new byte[1]);
                }
                RightSkillNamePointer.Add(file.Length);
                if (spTypeSupportParam[x2].rightSkillName != "") {
                    file = MainFunctions.b_AddBytes(file, Encoding.ASCII.GetBytes(spTypeSupportParam[x2].rightSkillName));
                    file = MainFunctions.b_AddBytes(file, new byte[1]);
                }



                int newPointer3 = UpSkillNamePointer[x2] - 316 - 0xB0 * x2 - 0x10;
                byte[] ptrBytes3 = BitConverter.GetBytes(newPointer3);
                if (spTypeSupportParam[x2].upSkillName != "") {
                    file = MainFunctions.b_ReplaceBytes(file, ptrBytes3, 316 + 0xB0 * x2 + 0x10);
                }
                newPointer3 = DownSkillNamePointer[x2] - 316 - 0xB0 * x2 - 0x38;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);
                if (spTypeSupportParam[x2].downSkillName != "") {
                    file = MainFunctions.b_ReplaceBytes(file, ptrBytes3, 316 + 0xB0 * x2 + 0x38);
                }
                newPointer3 = LeftSkillNamePointer[x2] - 316 - 0xB0 * x2 - 0x60;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);
                if (spTypeSupportParam[x2].leftSkillName != "") {
                    file = MainFunctions.b_ReplaceBytes(file, ptrBytes3, 316 + 0xB0 * x2 + 0x60);
                }
                newPointer3 = RightSkillNamePointer[x2] - 316 - 0xB0 * x2 - 0x88;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);
                if (spTypeSupportParam[x2].rightSkillName != "") {
                    file = MainFunctions.b_ReplaceBytes(file, ptrBytes3, 316 + 0xB0 * x2 + 0x88);
                }
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].characode), 316 + 0xB0 * x2 + 0x00);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].mode), 316 + 0xB0 * x2 + 0x04);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].type), 316 + 0xB0 * x2 + 0x08);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].upSkill_EnableInGround), 316 + 0xB0 * x2 + 0x18);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].upSkill_EnableInAir), 316 + 0xB0 * x2 + 0x1C);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].upSkill_unk4), 316 + 0xB0 * x2 + 0x20);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].upSkill_unk1), 316 + 0xB0 * x2 + 0x24);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].upSkill_unk2), 316 + 0xB0 * x2 + 0x28);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].upSkill_unk3), 316 + 0xB0 * x2 + 0x2C);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].downSkill_EnableInGround), 316 + 0xB0 * x2 + 0x40);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].downSkill_EnableInAir), 316 + 0xB0 * x2 + 0x44);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].downSkill_unk4), 316 + 0xB0 * x2 + 0x48);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].downSkill_unk1), 316 + 0xB0 * x2 + 0x4C);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].downSkill_unk2), 316 + 0xB0 * x2 + 0x50);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].downSkill_unk3), 316 + 0xB0 * x2 + 0x54);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].leftSkill_EnableInGround), 316 + 0xB0 * x2 + 0x68);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].leftSkill_EnableInAir), 316 + 0xB0 * x2 + 0x6C);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].leftSkill_unk4), 316 + 0xB0 * x2 + 0x70);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].leftSkill_unk1), 316 + 0xB0 * x2 + 0x74);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].leftSkill_unk2), 316 + 0xB0 * x2 + 0x78);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].leftSkill_unk3), 316 + 0xB0 * x2 + 0x7C);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].rightSkill_EnableInGround), 316 + 0xB0 * x2 + 0x90);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].rightSkill_EnableInAir), 316 + 0xB0 * x2 + 0x94);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].rightSkill_unk4), 316 + 0xB0 * x2 + 0x98);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].rightSkill_unk1), 316 + 0xB0 * x2 + 0x9C);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].rightSkill_unk2), 316 + 0xB0 * x2 + 0xA0);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(spTypeSupportParam[x2].rightSkill_unk3), 316 + 0xB0 * x2 + 0xA4);

            }
            int FileSize = file.Length - 300;
            file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(FileSize), 296, 1);
            file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(FileSize + 4), 284, 1);
            file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(EntryCount), 304);
            byte[] finalBytes = new byte[20]
            {
                0,0,0,8,0,0,0,2,0,121,24,0,0,0,0,4,0,0,0,0
            };
            file = MainFunctions.b_AddBytes(file, finalBytes);
            return file;
        }
    }
}
