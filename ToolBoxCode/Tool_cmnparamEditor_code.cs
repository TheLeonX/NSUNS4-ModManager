using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_cmnparamEditor_code {
        public bool FileOpen = false;
        public string FilePath = "";
        public byte[] fileBytes = new byte[0];

        public int EntryCount_TUJ = 0;
        public List<float> TUJ_value_List = new List<float>();
        public List<string> TUJ_ev_List = new List<string>();
        public List<string> TUJ_CutIn_List = new List<string>();
        public List<string> TUJ_Atk_List = new List<string>();
        public List<string> TUJ_Name1_List = new List<string>();
        public List<string> TUJ_Name2_List = new List<string>();

        public int EntryCount_Player = 0;
        public List<string> Player_Characode_List = new List<string>();
        public List<string> Player_PlFileName_List = new List<string>();
        public List<string> Player_PlAwaFileName_List = new List<string>();
        public List<string> Player_PlAwa2FileName_List = new List<string>();
        public List<string> Player_EvName_List = new List<string>();
        public List<string> Player_UJEvName_List = new List<string>();
        public List<string> Player_UJ_1_CutInName_List = new List<string>();
        public List<string> Player_UJ_1_AtkName_List = new List<string>();
        public List<string> Player_UJ_2_CutInName_List = new List<string>();
        public List<string> Player_UJ_2_AtkName_List = new List<string>();
        public List<string> Player_UJ_3_CutInName_List = new List<string>();
        public List<string> Player_UJ_3_AtkName_List = new List<string>();
        public List<string> Player_UJ_Alt_CutInName_List = new List<string>();
        public List<string> Player_UJ_Alt_AtkName_List = new List<string>();
        public List<string> Player_PartnerCharacode_List = new List<string>();
        public List<string> Player_PartnerAwaCharacode_List = new List<string>();
        public void ClearFile() {

            EntryCount_TUJ = 0;
            TUJ_value_List = new List<float>();
            TUJ_ev_List = new List<string>();
            TUJ_CutIn_List = new List<string>();
            TUJ_Atk_List = new List<string>();
            TUJ_Name1_List = new List<string>();
            TUJ_Name2_List = new List<string>();

            EntryCount_Player = 0;
            Player_Characode_List = new List<string>();
            Player_PlFileName_List = new List<string>();
            Player_PlAwaFileName_List = new List<string>();
            Player_PlAwa2FileName_List = new List<string>();
            Player_EvName_List = new List<string>();
            Player_UJEvName_List = new List<string>();
            Player_UJ_1_CutInName_List = new List<string>();
            Player_UJ_1_AtkName_List = new List<string>();
            Player_UJ_2_CutInName_List = new List<string>();
            Player_UJ_2_AtkName_List = new List<string>();
            Player_UJ_3_CutInName_List = new List<string>();
            Player_UJ_3_AtkName_List = new List<string>();
            Player_UJ_Alt_CutInName_List = new List<string>();
            Player_UJ_Alt_AtkName_List = new List<string>();
            Player_PartnerCharacode_List = new List<string>();
            Player_PartnerAwaCharacode_List = new List<string>();
        }

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
            EntryCount_TUJ = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, 340);
            int PlayerSPosition = 0;
            for (int x2 = 0; x2 < EntryCount_TUJ; x2++) {
                long _ptr = 342 + 0xA4 * x2;

                float valueTUJ = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr);
                string EvName = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x04);
                string CutInName = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x24);
                string AtkName = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x44);
                string Name1 = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x64);
                string Name2 = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x84);


                TUJ_value_List.Add(valueTUJ);
                TUJ_ev_List.Add(EvName);
                TUJ_CutIn_List.Add(CutInName);
                TUJ_Atk_List.Add(AtkName);
                TUJ_Name1_List.Add(Name1);
                TUJ_Name2_List.Add(Name2);
                PlayerSPosition = (int)_ptr + 0xA4;

            }
            PlayerSPosition += 0x30;
            EntryCount_Player = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, PlayerSPosition);
            for (int x2 = 0; x2 < EntryCount_Player; x2++) {
                long _ptr = PlayerSPosition + 0x02 + (0x3E4 * x2);

                string characode = MainFunctions.b_ReadString2(FileBytes, (int)_ptr);
                string PlFileName = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x20);
                string PlAwaFileName = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x60);
                string PlAwa2FileName = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x80);
                string EvFileName = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x200);
                string UJEvFileName = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x280);
                string UJ_1_CutIn_Name = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x2A0);
                string UJ_1_Atk_Name = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x2C0);
                string UJ_2_CutIn_Name = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x2E0);
                string UJ_2_Atk_Name = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x300);
                string UJ_3_CutIn_Name = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x320);
                string UJ_3_Atk_Name = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x340);
                string UJ_Alt_CutIn_Name = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x360);
                string UJ_Alt_Atk_Name = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x380);
                string partnerCharacode = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x3A0);
                string partnerAwaCharacode = MainFunctions.b_ReadString2(FileBytes, (int)_ptr + 0x3C0);


                Player_Characode_List.Add(characode);
                Player_PlFileName_List.Add(PlFileName);
                Player_PlAwaFileName_List.Add(PlAwaFileName);
                Player_PlAwa2FileName_List.Add(PlAwa2FileName);
                Player_EvName_List.Add(EvFileName);
                Player_UJEvName_List.Add(UJEvFileName);
                Player_UJ_1_CutInName_List.Add(UJ_1_CutIn_Name);
                Player_UJ_1_AtkName_List.Add(UJ_1_Atk_Name);
                Player_UJ_2_CutInName_List.Add(UJ_2_CutIn_Name);
                Player_UJ_2_AtkName_List.Add(UJ_2_Atk_Name);
                Player_UJ_3_CutInName_List.Add(UJ_3_CutIn_Name);
                Player_UJ_3_AtkName_List.Add(UJ_3_Atk_Name);
                Player_UJ_Alt_CutInName_List.Add(UJ_Alt_CutIn_Name);
                Player_UJ_Alt_AtkName_List.Add(UJ_Alt_Atk_Name);
                Player_PartnerCharacode_List.Add(partnerCharacode);
                Player_PartnerAwaCharacode_List.Add(partnerAwaCharacode);

            }
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
            byte[] header = new byte[342]
            {
                0x4E,0x55,0x43,0x43,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x10,0x00,0x00,0x00,0x03,0x00,0x79,0x45,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x3B,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x29,0x00,0x00,0x00,0x05,0x00,0x00,0x00,0x25,0x00,0x00,0x00,0x05,0x00,0x00,0x00,0x3C,0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x4E,0x75,0x6C,0x6C,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x42,0x69,0x6E,0x61,0x72,0x79,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x50,0x61,0x67,0x65,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x49,0x6E,0x64,0x65,0x78,0x00,0x00,0x62,0x69,0x6E,0x2F,0x70,0x61,0x69,0x72,0x5F,0x73,0x70,0x6C,0x5F,0x73,0x6E,0x64,0x2E,0x62,0x69,0x6E,0x00,0x62,0x69,0x6E,0x2F,0x70,0x6C,0x61,0x79,0x65,0x72,0x5F,0x73,0x6E,0x64,0x2E,0x62,0x69,0x6E,0x00,0x00,0x70,0x61,0x69,0x72,0x5F,0x73,0x70,0x6C,0x5F,0x73,0x6E,0x64,0x00,0x50,0x61,0x67,0x65,0x30,0x00,0x69,0x6E,0x64,0x65,0x78,0x00,0x70,0x6C,0x61,0x79,0x65,0x72,0x5F,0x73,0x6E,0x64,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x1D,0x7E,0x00,0x00,0x00,0x01,0x00,0x79,0x00,0x00,0x00,0x00,0x1D,0x7A,0x2E,0x00
            };
            for (int x4 = 0; x4 < header.Length; x4++) {
                file.Add(header[x4]);
            }
            for (int x3 = 0; x3 < EntryCount_TUJ * 0xA4; x3++) {
                file.Add(0);
            }
            int TUJ_Section_size = 2;
            int Player_Section_size = 2;
            int PlayerSectionPos = 0;
            for (int x2 = 0; x2 < EntryCount_TUJ; x2++) {
                TUJ_Section_size += 0xA4;
                int _ptr = 342 + 0xA4 * x2;
                byte[] o_a = BitConverter.GetBytes(TUJ_value_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8] = o_a[a8];
                }
                if (TUJ_ev_List[x2] != "") {

                    for (int a7 = 0; a7 < TUJ_ev_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x4] = (byte)TUJ_ev_List[x2][a7];
                    }
                }
                if (TUJ_CutIn_List[x2] != "") {

                    for (int a7 = 0; a7 < TUJ_CutIn_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x24] = (byte)TUJ_CutIn_List[x2][a7];
                    }
                }
                if (TUJ_Atk_List[x2] != "") {

                    for (int a7 = 0; a7 < TUJ_Atk_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x44] = (byte)TUJ_Atk_List[x2][a7];
                    }
                }
                if (TUJ_Name1_List[x2] != "") {

                    for (int a7 = 0; a7 < TUJ_Name1_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x64] = (byte)TUJ_Name1_List[x2][a7];
                    }
                }
                if (TUJ_Name2_List[x2] != "") {

                    for (int a7 = 0; a7 < TUJ_Name2_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x84] = (byte)TUJ_Name2_List[x2][a7];
                    }
                }
                PlayerSectionPos = _ptr + 0xA4;
            }

            byte[] header2 = new byte[0x32]
            {
                0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x02,0x00,0x79,0x18,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x03,0x77,0x16,0x00,0x00,0x00,0x01,0x00,0x79,0x00,0x00,0x00,0x03,0x77,0x12,0xE4,0x00
            };
            for (int x4 = 0; x4 < header2.Length; x4++) {
                file.Add(header2[x4]);
            }
            for (int x3 = 0; x3 < EntryCount_Player * 0x3E4; x3++) {
                file.Add(0);
            }

            PlayerSectionPos += 0x32;
            for (int x2 = 0; x2 < EntryCount_Player; x2++) {
                Player_Section_size += 0x3E4;
                int _ptr = PlayerSectionPos + 0x3E4 * x2;
                if (Player_Characode_List[x2] != "" && Player_Characode_List[x2] != "NULL") {

                    for (int a7 = 0; a7 < Player_Characode_List[x2].Length; a7++) {
                        file[_ptr + a7] = (byte)Player_Characode_List[x2][a7];
                    }
                } else {
                    continue;
                }

                if (Player_PlFileName_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_PlFileName_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x20] = (byte)Player_PlFileName_List[x2][a7];
                        file[_ptr + a7 + 0x40] = (byte)Player_PlFileName_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x20] = (byte)st_Null[a7];
                        file[_ptr + a7 + 0x40] = (byte)st_Null[a7];
                    }
                }

                if (Player_PlAwaFileName_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_PlAwaFileName_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x60] = (byte)Player_PlAwaFileName_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x60] = (byte)st_Null[a7];
                    }
                }
                if (Player_PlAwa2FileName_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_PlAwa2FileName_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x80] = (byte)Player_PlAwa2FileName_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x80] = (byte)st_Null[a7];
                    }
                }
                string st1_Null = "NULL";
                for (int a7 = 0; a7 < st1_Null.Length; a7++) {
                    file[_ptr + a7 + 0xA0] = (byte)st1_Null[a7];
                    file[_ptr + a7 + 0xC0] = (byte)st1_Null[a7];
                    file[_ptr + a7 + 0xE0] = (byte)st1_Null[a7];
                    file[_ptr + a7 + 0x100] = (byte)st1_Null[a7];
                    file[_ptr + a7 + 0x120] = (byte)st1_Null[a7];
                    file[_ptr + a7 + 0x140] = (byte)st1_Null[a7];
                    file[_ptr + a7 + 0x160] = (byte)st1_Null[a7];
                    file[_ptr + a7 + 0x180] = (byte)st1_Null[a7];
                    file[_ptr + a7 + 0x1A0] = (byte)st1_Null[a7];
                    file[_ptr + a7 + 0x1C0] = (byte)st1_Null[a7];
                    file[_ptr + a7 + 0x1E0] = (byte)st1_Null[a7];
                }
                if (Player_EvName_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_EvName_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x200] = (byte)Player_EvName_List[x2][a7];
                        file[_ptr + a7 + 0x220] = (byte)Player_EvName_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x200] = (byte)st_Null[a7];
                        file[_ptr + a7 + 0x220] = (byte)st_Null[a7];
                    }
                }
                for (int a7 = 0; a7 < st1_Null.Length; a7++) {
                    file[_ptr + a7 + 0x240] = (byte)st1_Null[a7];
                    file[_ptr + a7 + 0x260] = (byte)st1_Null[a7];
                }
                if (Player_UJEvName_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_UJEvName_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x280] = (byte)Player_UJEvName_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x280] = (byte)st_Null[a7];
                    }
                }
                if (Player_UJ_1_CutInName_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_UJ_1_CutInName_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x2A0] = (byte)Player_UJ_1_CutInName_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x2A0] = (byte)st_Null[a7];
                    }
                }
                if (Player_UJ_1_AtkName_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_UJ_1_AtkName_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x2C0] = (byte)Player_UJ_1_AtkName_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x2C0] = (byte)st_Null[a7];
                    }
                }
                if (Player_UJ_2_CutInName_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_UJ_2_CutInName_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x2E0] = (byte)Player_UJ_2_CutInName_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x2E0] = (byte)st_Null[a7];
                    }
                }
                if (Player_UJ_2_AtkName_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_UJ_2_AtkName_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x300] = (byte)Player_UJ_2_AtkName_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x300] = (byte)st_Null[a7];
                    }
                }
                if (Player_UJ_3_CutInName_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_UJ_3_CutInName_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x320] = (byte)Player_UJ_3_CutInName_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x320] = (byte)st_Null[a7];
                    }
                }
                if (Player_UJ_3_AtkName_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_UJ_3_AtkName_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x340] = (byte)Player_UJ_3_AtkName_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x340] = (byte)st_Null[a7];
                    }
                }
                if (Player_UJ_Alt_CutInName_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_UJ_Alt_CutInName_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x360] = (byte)Player_UJ_Alt_CutInName_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x360] = (byte)st_Null[a7];
                    }
                }
                if (Player_UJ_Alt_AtkName_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_UJ_Alt_AtkName_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x380] = (byte)Player_UJ_Alt_AtkName_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x380] = (byte)st_Null[a7];
                    }
                }
                if (Player_PartnerCharacode_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_PartnerCharacode_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x3A0] = (byte)Player_PartnerCharacode_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x3A0] = (byte)st_Null[a7];
                    }
                }
                if (Player_PartnerAwaCharacode_List[x2] != "") {

                    for (int a7 = 0; a7 < Player_PartnerAwaCharacode_List[x2].Length; a7++) {
                        file[_ptr + a7 + 0x3C0] = (byte)Player_PartnerAwaCharacode_List[x2][a7];
                    }
                } else {
                    string st_Null = "NULL";
                    for (int a7 = 0; a7 < st_Null.Length; a7++) {
                        file[_ptr + a7 + 0x3C0] = (byte)st_Null[a7];
                    }
                }
            }
            byte[] TUJ_sizeBytes1 = BitConverter.GetBytes(TUJ_Section_size);
            byte[] TUJ_sizeBytes2 = BitConverter.GetBytes(TUJ_Section_size + 4);
            for (int a20 = 0; a20 < 4; a20++) {
                file[0x144 + a20] = TUJ_sizeBytes2[3 - a20];
            }
            for (int a19 = 0; a19 < 4; a19++) {
                file[0x150 + a19] = TUJ_sizeBytes1[3 - a19];
            }
            byte[] countBytes = BitConverter.GetBytes(EntryCount_TUJ);
            for (int a18 = 0; a18 < 2; a18++) {
                file[0x154 + a18] = countBytes[a18];
            }

            byte[] Player_sizeBytes1 = BitConverter.GetBytes(Player_Section_size);
            byte[] Player_sizeBytes2 = BitConverter.GetBytes(Player_Section_size + 4);
            for (int a20 = 0; a20 < 4; a20++) {
                file[PlayerSectionPos - 0x12 + a20] = Player_sizeBytes2[3 - a20];
            }
            for (int a19 = 0; a19 < 4; a19++) {
                file[PlayerSectionPos - 0x6 + a19] = Player_sizeBytes1[3 - a19];
            }
            countBytes = BitConverter.GetBytes(EntryCount_Player);
            for (int a18 = 0; a18 < 2; a18++) {
                file[PlayerSectionPos - 0x2 + a18] = countBytes[a18];
            }

            byte[] finalBytes = new byte[20]
            {
               0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x02,0x00,0x79,0x18,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00
            };
            for (int x = 0; x < finalBytes.Length; x++) {
                file.Add(finalBytes[x]);
            }
            return file.ToArray();
        }
    }
}
