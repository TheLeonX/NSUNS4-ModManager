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

        public List<int> Characode_List = new List<int>();
        public List<int> Type_List = new List<int>();
        public List<int> Mode_List = new List<int>();
        public List<string> LeftSkillName_List = new List<string>();
        public List<string> RightSkillName_List = new List<string>();
        public List<string> UpSkillName_List = new List<string>();
        public List<string> DownSkillName_List = new List<string>();
        public List<int> LeftSkill_unk1_List = new List<int>();
        public List<int> LeftSkill_unk2_List = new List<int>();
        public List<int> LeftSkill_unk3_List = new List<int>();
        public List<bool> LeftSkill_EnableInAir_List = new List<bool>();
        public List<bool> LeftSkill_Unknown_List = new List<bool>();
        public List<int> RightSkill_unk1_List = new List<int>();
        public List<int> RightSkill_unk2_List = new List<int>();
        public List<int> RightSkill_unk3_List = new List<int>();
        public List<bool> RightSkill_EnableInAir_List = new List<bool>();
        public List<bool> RightSkill_Unknown_List = new List<bool>();
        public List<int> UpSkill_unk1_List = new List<int>();
        public List<int> UpSkill_unk2_List = new List<int>();
        public List<int> UpSkill_unk3_List = new List<int>();
        public List<bool> UpSkill_EnableInAir_List = new List<bool>();
        public List<bool> UpSkill_Unknown_List = new List<bool>();
        public List<int> DownSkill_unk1_List = new List<int>();
        public List<int> DownSkill_unk2_List = new List<int>();
        public List<int> DownSkill_unk3_List = new List<int>();
        public List<bool> DownSkill_EnableInAir_List = new List<bool>();
        public List<bool> DownSkill_Unknown_List = new List<bool>();
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
                long _ptr = 316 + 0xB0 * x2;
                int Characode = MainFunctions.b_ReadInt(fileBytes, (int)_ptr);
                int Mode = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x04);
                int Type = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x08);

                long _ptrUpSkillName = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x10);
                string UpSkillName = MainFunctions.b_ReadString(fileBytes, (int)_ptr + 0x10 + (int)_ptrUpSkillName);
                bool UpSkillUnknownValue = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x18));
                bool UpSkillEnableInAir = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x1C));
                int UpSkill_unk1 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x24);
                int UpSkill_unk2 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x28);
                int UpSkill_unk3 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x2C);

                long _ptrDownSkillName = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x38);
                string DownSkillName = MainFunctions.b_ReadString(fileBytes, (int)_ptr + 0x38 + (int)_ptrDownSkillName);
                bool DownSkillUnknownValue = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x40));
                bool DownSkillEnableInAir = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x44));
                int DownSkill_unk1 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x4C);
                int DownSkill_unk2 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x50);
                int DownSkill_unk3 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x54);

                long _ptrLeftSkillName = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x60);
                string LeftSkillName = MainFunctions.b_ReadString(fileBytes, (int)_ptr + 0x60 + (int)_ptrLeftSkillName);
                bool LeftSkillUnknownValue = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x68));
                bool LeftSkillEnableInAir = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x6C));
                int LeftSkill_unk1 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x74);
                int LeftSkill_unk2 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x78);
                int LeftSkill_unk3 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x7C);

                long _ptrRightSkillName = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x88);
                string RightSkillName = MainFunctions.b_ReadString(fileBytes, (int)_ptr + 0x88 + (int)_ptrRightSkillName);
                bool RightSkillUnknownValue = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x90));
                bool RightSkillEnableInAir = Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x94));
                int RightSkill_unk1 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0x9C);
                int RightSkill_unk2 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0xA0);
                int RightSkill_unk3 = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 0xA4);



                Characode_List.Add(Characode);
                Type_List.Add(Type);
                Mode_List.Add(Mode);
                LeftSkillName_List.Add(LeftSkillName);
                RightSkillName_List.Add(RightSkillName);
                UpSkillName_List.Add(UpSkillName);
                DownSkillName_List.Add(DownSkillName);
                LeftSkill_unk1_List.Add(LeftSkill_unk1);
                LeftSkill_unk2_List.Add(LeftSkill_unk2);
                LeftSkill_unk3_List.Add(LeftSkill_unk3);
                LeftSkill_EnableInAir_List.Add(LeftSkillEnableInAir);
                LeftSkill_Unknown_List.Add(LeftSkillUnknownValue);
                RightSkill_unk1_List.Add(RightSkill_unk1);
                RightSkill_unk2_List.Add(RightSkill_unk2);
                RightSkill_unk3_List.Add(RightSkill_unk3);
                RightSkill_EnableInAir_List.Add(RightSkillEnableInAir);
                RightSkill_Unknown_List.Add(RightSkillUnknownValue);
                UpSkill_unk1_List.Add(UpSkill_unk1);
                UpSkill_unk2_List.Add(UpSkill_unk2);
                UpSkill_unk3_List.Add(UpSkill_unk3);
                UpSkill_EnableInAir_List.Add(UpSkillEnableInAir);
                UpSkill_Unknown_List.Add(UpSkillUnknownValue);
                DownSkill_unk1_List.Add(DownSkill_unk1);
                DownSkill_unk2_List.Add(DownSkill_unk2);
                DownSkill_unk3_List.Add(DownSkill_unk3);
                DownSkill_EnableInAir_List.Add(DownSkillEnableInAir);
                DownSkill_Unknown_List.Add(DownSkillUnknownValue);

            }
        }
        public void ClearFile() {
            FileOpen = false;
            FilePath = "";
            fileBytes = new byte[0];
            EntryCount = 0;
            Characode_List = new List<int>();
            Type_List = new List<int>();
            Mode_List = new List<int>();
            LeftSkillName_List = new List<string>();
            RightSkillName_List = new List<string>();
            UpSkillName_List = new List<string>();
            DownSkillName_List = new List<string>();
            LeftSkill_unk1_List = new List<int>();
            LeftSkill_unk2_List = new List<int>();
            LeftSkill_unk3_List = new List<int>();
            LeftSkill_EnableInAir_List = new List<bool>();
            LeftSkill_Unknown_List = new List<bool>();
            RightSkill_unk1_List = new List<int>();
            RightSkill_unk2_List = new List<int>();
            RightSkill_unk3_List = new List<int>();
            RightSkill_EnableInAir_List = new List<bool>();
            RightSkill_Unknown_List = new List<bool>();
            UpSkill_unk1_List = new List<int>();
            UpSkill_unk2_List = new List<int>();
            UpSkill_unk3_List = new List<int>();
            UpSkill_EnableInAir_List = new List<bool>();
            UpSkill_Unknown_List = new List<bool>();
            DownSkill_unk1_List = new List<int>();
            DownSkill_unk2_List = new List<int>();
            DownSkill_unk3_List = new List<int>();
            DownSkill_EnableInAir_List = new List<bool>();
            DownSkill_Unknown_List = new List<bool>();
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
                if (UpSkillName_List[x2] != "") {
                    file = MainFunctions.b_AddBytes(file, Encoding.ASCII.GetBytes(UpSkillName_List[x2]));
                    file = MainFunctions.b_AddBytes(file, new byte[1]);
                }
                DownSkillNamePointer.Add(file.Length);
                if (DownSkillName_List[x2] != "") {
                    file = MainFunctions.b_AddBytes(file, Encoding.ASCII.GetBytes(DownSkillName_List[x2]));
                    file = MainFunctions.b_AddBytes(file, new byte[1]);
                }
                LeftSkillNamePointer.Add(file.Length);
                if (LeftSkillName_List[x2] != "") {
                    file = MainFunctions.b_AddBytes(file, Encoding.ASCII.GetBytes(LeftSkillName_List[x2]));
                    file = MainFunctions.b_AddBytes(file, new byte[1]);
                }
                RightSkillNamePointer.Add(file.Length);
                if (RightSkillName_List[x2] != "") {
                    file = MainFunctions.b_AddBytes(file, Encoding.ASCII.GetBytes(RightSkillName_List[x2]));
                    file = MainFunctions.b_AddBytes(file, new byte[1]);
                }



                int newPointer3 = UpSkillNamePointer[x2] - 316 - 0xB0 * x2 - 0x10;
                byte[] ptrBytes3 = BitConverter.GetBytes(newPointer3);
                if (UpSkillName_List[x2] != "") {
                    file = MainFunctions.b_ReplaceBytes(file, ptrBytes3, 316 + 0xB0 * x2 + 0x10);
                }
                newPointer3 = DownSkillNamePointer[x2] - 316 - 0xB0 * x2 - 0x38;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);
                if (DownSkillName_List[x2] != "") {
                    file = MainFunctions.b_ReplaceBytes(file, ptrBytes3, 316 + 0xB0 * x2 + 0x38);
                }
                newPointer3 = LeftSkillNamePointer[x2] - 316 - 0xB0 * x2 - 0x60;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);
                if (LeftSkillName_List[x2] != "") {
                    file = MainFunctions.b_ReplaceBytes(file, ptrBytes3, 316 + 0xB0 * x2 + 0x60);
                }
                newPointer3 = RightSkillNamePointer[x2] - 316 - 0xB0 * x2 - 0x88;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);
                if (RightSkillName_List[x2] != "") {
                    file = MainFunctions.b_ReplaceBytes(file, ptrBytes3, 316 + 0xB0 * x2 + 0x88);
                }
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(Characode_List[x2]), 316 + 0xB0 * x2 + 0x00);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(Mode_List[x2]), 316 + 0xB0 * x2 + 0x04);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(Type_List[x2]), 316 + 0xB0 * x2 + 0x08);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(UpSkill_Unknown_List[x2]), 316 + 0xB0 * x2 + 0x18);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(UpSkill_EnableInAir_List[x2]), 316 + 0xB0 * x2 + 0x1C);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(UpSkill_unk1_List[x2]), 316 + 0xB0 * x2 + 0x24);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(UpSkill_unk2_List[x2]), 316 + 0xB0 * x2 + 0x28);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(UpSkill_unk3_List[x2]), 316 + 0xB0 * x2 + 0x2C);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(DownSkill_Unknown_List[x2]), 316 + 0xB0 * x2 + 0x40);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(DownSkill_EnableInAir_List[x2]), 316 + 0xB0 * x2 + 0x44);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(DownSkill_unk1_List[x2]), 316 + 0xB0 * x2 + 0x4C);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(DownSkill_unk2_List[x2]), 316 + 0xB0 * x2 + 0x50);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(DownSkill_unk3_List[x2]), 316 + 0xB0 * x2 + 0x54);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(LeftSkill_Unknown_List[x2]), 316 + 0xB0 * x2 + 0x68);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(LeftSkill_EnableInAir_List[x2]), 316 + 0xB0 * x2 + 0x6C);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(LeftSkill_unk1_List[x2]), 316 + 0xB0 * x2 + 0x74);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(LeftSkill_unk2_List[x2]), 316 + 0xB0 * x2 + 0x78);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(LeftSkill_unk3_List[x2]), 316 + 0xB0 * x2 + 0x7C);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(RightSkill_Unknown_List[x2]), 316 + 0xB0 * x2 + 0x90);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(RightSkill_EnableInAir_List[x2]), 316 + 0xB0 * x2 + 0x94);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(RightSkill_unk1_List[x2]), 316 + 0xB0 * x2 + 0x9C);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(RightSkill_unk2_List[x2]), 316 + 0xB0 * x2 + 0xA0);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(RightSkill_unk3_List[x2]), 316 + 0xB0 * x2 + 0xA4);

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
