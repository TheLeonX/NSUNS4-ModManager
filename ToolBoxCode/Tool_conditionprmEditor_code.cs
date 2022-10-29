using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_conditionprmEditor_code {
        public bool FileOpen = false;
        public string FilePath = "";
        public byte[] fileBytes = new byte[0];
        public int EntryCount = 0;

        public List<string> ConditionName_List = new List<string>();
        public List<int> ConditionDuration_List = new List<int>();
        public List<float> ConditionATK_List = new List<float>();
        public List<float> ConditionDEF_List = new List<float>();
        public List<float> ConditionSPD_List = new List<float>();
        public List<float> ConditionSPT_ATK_List = new List<float>();
        public List<float> ConditionHP_Recover_List = new List<float>();
        public List<float> ConditionPoison_List = new List<float>();
        public List<float> ConditionChakra_Recover_List = new List<float>();
        public List<float> ConditionChakra_Shave_List = new List<float>();
        public List<float> ConditionChakra_Revival_List = new List<float>();
        public List<float> ConditionChakra_Drain_List = new List<float>();
        public List<float> ConditionChakra_List = new List<float>();
        public List<float> ConditionChakra_Usage_List = new List<float>();
        public List<float> ConditionSupport_List = new List<float>();
        public List<float> ConditionTeam_List = new List<float>();
        public List<float> ConditionGuardBreak_List = new List<float>();
        public List<float> ConditionDodge_List = new List<float>();
        public List<bool> ConditionProjectile_List = new List<bool>();
        public List<bool> ConditionAutoDodge_List = new List<bool>();
        public List<bool> ConditionSeal_List = new List<bool>();
        public List<bool> ConditionSleep_List = new List<bool>();
        public List<bool> ConditionStun_List = new List<bool>();
        public List<int> ConditionFlashingType_List = new List<int>();
        public List<float> ConditionR_channel_List = new List<float>();
        public List<float> ConditionG_channel_List = new List<float>();
        public List<float> ConditionB_channel_List = new List<float>();
        public List<float> ConditionUnknown1_List = new List<float>();
        public List<float> ConditionFlashingInterval_List = new List<float>();
        public List<float> ConditionUnknown2_List = new List<float>();
        public List<float> ConditionOpacity_List = new List<float>();
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
            byte[] FileBytes = File.ReadAllBytes(FilePath);
            EntryCount = MainFunctions.b_ReadInt(FileBytes, 0x12C);
            for (int x2 = 0; x2 < EntryCount; x2++) {
                long _ptr = 0x130 + 0xB0 * x2;

                string ConditionName = MainFunctions.b_ReadString(FileBytes, (int)_ptr);
                int ConditionDuration = MainFunctions.b_ReadInt(FileBytes, (int)_ptr + 0x40);
                float ConditionATK = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x44);
                float ConditionDEF = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x48);
                float ConditionSPD = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x4C);
                float ConditionSPT_ATK = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x50);
                float ConditionHP_Recover = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x54);
                float ConditionPoison = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x58);
                float ConditionChakra_Recover = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x5C);
                float ConditionChakra_Shave = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x60);
                float ConditionChakra_Revival = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x64);
                float ConditionChakra_Drain = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x6C);
                float ConditionChakra = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x70);
                float ConditionChakra_Usage = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x74);
                float ConditionSupport = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x78);
                float ConditionTeam = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x7C);
                float ConditionGuardBreak = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x80);
                float ConditionDodge = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x84);
                bool ConditionProjectile = Convert.ToBoolean(MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x88));
                bool ConditionAutoDodge = Convert.ToBoolean(MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x8A));
                bool ConditionSeal = Convert.ToBoolean(MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x8C));
                bool ConditionSleep = Convert.ToBoolean(MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x8E));
                bool ConditionStun = Convert.ToBoolean(MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x90));

                int ConditionFlashingType = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x92);
                float ConditionFlashing_R_channel = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x94);
                float ConditionFlashing_G_channel = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x98);
                float ConditionFlashing_B_channel = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x9C);
                float ConditionFlashing_unknown1 = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0xA0);
                float ConditionFlashing_Interval = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0xA4);
                float ConditionFlashing_unknown2 = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0xA8);
                float ConditionFlashing_Opacity = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0xAC);


                ConditionName_List.Add(ConditionName);
                ConditionDuration_List.Add(ConditionDuration);
                ConditionATK_List.Add(ConditionATK);
                ConditionDEF_List.Add(ConditionDEF);
                ConditionSPD_List.Add(ConditionSPD);
                ConditionSPT_ATK_List.Add(ConditionSPT_ATK);
                ConditionHP_Recover_List.Add(ConditionHP_Recover);
                ConditionPoison_List.Add(ConditionPoison);
                ConditionChakra_Recover_List.Add(ConditionChakra_Recover);
                ConditionChakra_Shave_List.Add(ConditionChakra_Shave);
                ConditionChakra_Revival_List.Add(ConditionChakra_Revival);
                ConditionChakra_Drain_List.Add(ConditionChakra_Drain);
                ConditionChakra_List.Add(ConditionChakra);
                ConditionChakra_Usage_List.Add(ConditionChakra_Usage);
                ConditionSupport_List.Add(ConditionSupport);
                ConditionTeam_List.Add(ConditionTeam);
                ConditionGuardBreak_List.Add(ConditionGuardBreak);
                ConditionDodge_List.Add(ConditionDodge);
                ConditionProjectile_List.Add(ConditionProjectile);
                ConditionAutoDodge_List.Add(ConditionAutoDodge);
                ConditionSeal_List.Add(ConditionSeal);
                ConditionSleep_List.Add(ConditionSleep);
                ConditionStun_List.Add(ConditionStun);
                ConditionFlashingType_List.Add(ConditionFlashingType);
                ConditionR_channel_List.Add(ConditionFlashing_R_channel);
                ConditionG_channel_List.Add(ConditionFlashing_G_channel);
                ConditionB_channel_List.Add(ConditionFlashing_B_channel);
                ConditionUnknown1_List.Add(ConditionFlashing_unknown1);
                ConditionFlashingInterval_List.Add(ConditionFlashing_Interval);
                ConditionUnknown2_List.Add(ConditionFlashing_unknown2);
                ConditionOpacity_List.Add(ConditionFlashing_Opacity);

            }
        }

        public void ClearFile() {

            EntryCount = 0;
            ConditionName_List = new List<string>();
            ConditionDuration_List = new List<int>();
            ConditionATK_List = new List<float>();
            ConditionDEF_List = new List<float>();
            ConditionSPD_List = new List<float>();
            ConditionSPT_ATK_List = new List<float>();
            ConditionHP_Recover_List = new List<float>();
            ConditionPoison_List = new List<float>();
            ConditionChakra_Recover_List = new List<float>();
            ConditionChakra_Shave_List = new List<float>();
            ConditionChakra_Revival_List = new List<float>();
            ConditionChakra_Drain_List = new List<float>();
            ConditionChakra_List = new List<float>();
            ConditionChakra_Usage_List = new List<float>();
            ConditionSupport_List = new List<float>();
            ConditionTeam_List = new List<float>();
            ConditionGuardBreak_List = new List<float>();
            ConditionDodge_List = new List<float>();
            ConditionProjectile_List = new List<bool>();
            ConditionAutoDodge_List = new List<bool>();
            ConditionSeal_List = new List<bool>();
            ConditionSleep_List = new List<bool>();
            ConditionStun_List = new List<bool>();
            ConditionFlashingType_List = new List<int>();
            ConditionR_channel_List = new List<float>();
            ConditionG_channel_List = new List<float>();
            ConditionB_channel_List = new List<float>();
            ConditionUnknown1_List = new List<float>();
            ConditionFlashingInterval_List = new List<float>();
            ConditionUnknown2_List = new List<float>();
            ConditionOpacity_List = new List<float>();
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
            List<byte> file = new List<byte>();
            byte[] header = new byte[0x130]
            {
               0x4E,0x55,0x43,0x43,0x00,0x00,0x00,0x63,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xF4,0x00,0x00,0x00,0x03,0x00,0x63,0x40,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x3B,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x34,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x1A,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x30,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x4E,0x75,0x6C,0x6C,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x42,0x69,0x6E,0x61,0x72,0x79,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x50,0x61,0x67,0x65,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x49,0x6E,0x64,0x65,0x78,0x00,0x00,0x44,0x3A,0x2F,0x4E,0x53,0x57,0x2F,0x70,0x61,0x72,0x61,0x6D,0x2F,0x70,0x6C,0x61,0x79,0x65,0x72,0x2F,0x43,0x6F,0x6E,0x76,0x65,0x72,0x74,0x65,0x72,0x2F,0x62,0x69,0x6E,0x2F,0x63,0x6F,0x6E,0x64,0x69,0x74,0x69,0x6F,0x6E,0x70,0x72,0x6D,0x2E,0x62,0x69,0x6E,0x00,0x00,0x63,0x6F,0x6E,0x64,0x69,0x74,0x69,0x6F,0x6E,0x70,0x72,0x6D,0x00,0x50,0x61,0x67,0x65,0x30,0x00,0x69,0x6E,0x64,0x65,0x78,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x63,0x42,0x00,0x00,0x01,0x47,0x48,0x00,0x00,0x00,0x01,0x00,0x63,0x42,0x00,0x00,0x01,0x47,0x44,0xDC,0x01,0x00,0x00
            };
            for (int x4 = 0; x4 < header.Length; x4++) {
                file.Add(header[x4]);
            }
            for (int x3 = 0; x3 < EntryCount * 0xB0; x3++) {
                file.Add(0);
            }
            int Section_size = 4;
            for (int x2 = 0; x2 < EntryCount; x2++) {
                Section_size += 0xB0;
                int _ptr = 0x130 + 0xB0 * x2;
                for (int a8 = 0; a8 < ConditionName_List[x2].Length; a8++) {
                    file[_ptr + a8] = (byte)ConditionName_List[x2][a8];
                }
                byte[] o_a = BitConverter.GetBytes(ConditionDuration_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x40] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionATK_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x44] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionDEF_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x48] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionSPD_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x4C] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionSPT_ATK_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x50] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionHP_Recover_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x54] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionPoison_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x58] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionChakra_Recover_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x5C] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionChakra_Shave_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x60] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionChakra_Revival_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x64] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionChakra_Drain_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x6C] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionChakra_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x70] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionChakra_Usage_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x74] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionSupport_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x78] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionTeam_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x7C] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionGuardBreak_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x80] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionDodge_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x84] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(Convert.ToInt32(ConditionProjectile_List[x2]));
                for (int a8 = 0; a8 < 2; a8++) {
                    file[_ptr + a8 + 0x88] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(Convert.ToInt32(ConditionAutoDodge_List[x2]));
                for (int a8 = 0; a8 < 2; a8++) {
                    file[_ptr + a8 + 0x8A] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(Convert.ToInt32(ConditionSeal_List[x2]));
                for (int a8 = 0; a8 < 2; a8++) {
                    file[_ptr + a8 + 0x8C] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(Convert.ToInt32(ConditionSleep_List[x2]));
                for (int a8 = 0; a8 < 2; a8++) {
                    file[_ptr + a8 + 0x8E] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(Convert.ToInt32(ConditionStun_List[x2]));
                for (int a8 = 0; a8 < 2; a8++) {
                    file[_ptr + a8 + 0x90] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionFlashingType_List[x2]);
                for (int a8 = 0; a8 < 2; a8++) {
                    file[_ptr + a8 + 0x92] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionR_channel_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x94] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionG_channel_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x98] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionB_channel_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x9C] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionUnknown1_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0xA0] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionFlashingInterval_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0xA4] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionUnknown2_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0xA8] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ConditionOpacity_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0xAC] = o_a[a8];
                }
            }


            byte[] TUJ_sizeBytes1 = BitConverter.GetBytes(Section_size);
            byte[] TUJ_sizeBytes2 = BitConverter.GetBytes(Section_size + 4);
            for (int a20 = 0; a20 < 4; a20++) {
                file[0x11C + a20] = TUJ_sizeBytes2[3 - a20];
            }
            for (int a19 = 0; a19 < 4; a19++) {
                file[0x128 + a19] = TUJ_sizeBytes1[3 - a19];
            }
            byte[] countBytes = BitConverter.GetBytes(EntryCount);
            for (int a18 = 0; a18 < 4; a18++) {
                file[0x12C + a18] = countBytes[a18];
            }

            byte[] finalBytes = new byte[20]
            {
              0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x02,0x00,0x63,0x19,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00
            };
            for (int x = 0; x < finalBytes.Length; x++) {
                file.Add(finalBytes[x]);
            }
            return file.ToArray();
        }
    }
}
