using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_SkillCustomizeParamEditor_code {
        public bool FileOpen = false;
        public string FilePath = "";
        public byte[] FileBytes = new byte[0];
        public int EntryCount = 0;
        public List<byte[]> CharacodeList = new List<byte[]>();
        public List<string> Skill1List = new List<string>();
        public List<string> Skill2List = new List<string>();
        public List<string> Skill3List = new List<string>();
        public List<string> Skill4List = new List<string>();
        public List<string> Skill5List = new List<string>();
        public List<string> Skill6List = new List<string>();
        public List<string> SkillAwaList = new List<string>();
        public List<string> Skill1_ex_List = new List<string>();
        public List<string> Skill2_ex_List = new List<string>();
        public List<string> Skill3_ex_List = new List<string>();
        public List<string> Skill4_ex_List = new List<string>();
        public List<string> Skill5_ex_List = new List<string>();
        public List<string> Skill6_ex_List = new List<string>();
        public List<string> SkillAwa_ex_List = new List<string>();
        public List<string> Skill1_air_List = new List<string>();
        public List<string> Skill2_air_List = new List<string>();
        public List<string> Skill3_air_List = new List<string>();
        public List<string> Skill4_air_List = new List<string>();
        public List<string> Skill5_air_List = new List<string>();
        public List<string> Skill6_air_List = new List<string>();
        public List<string> SkillAwa_air_List = new List<string>();
        public List<float> Skill1_CUC_List = new List<float>();
        public List<float> Skill2_CUC_List = new List<float>();
        public List<float> Skill3_CUC_List = new List<float>();
        public List<float> Skill4_CUC_List = new List<float>();
        public List<float> Skill5_CUC_List = new List<float>();
        public List<float> Skill6_CUC_List = new List<float>();
        public List<float> SkillAwa_CUC_List = new List<float>();
        public List<float> Skill1_CUCC_List = new List<float>();
        public List<float> Skill2_CUCC_List = new List<float>();
        public List<float> Skill3_CUCC_List = new List<float>();
        public List<float> Skill4_CUCC_List = new List<float>();
        public List<float> Skill5_CUCC_List = new List<float>();
        public List<float> Skill6_CUCC_List = new List<float>();
        public List<float> SkillAwa_CUCC_List = new List<float>();
        public List<int> Skill1_Priority_List = new List<int>();
        public List<int> Skill2_Priority_List = new List<int>();
        public List<int> Skill3_Priority_List = new List<int>();
        public List<int> Skill4_Priority_List = new List<int>();
        public List<int> Skill5_Priority_List = new List<int>();
        public List<int> Skill6_Priority_List = new List<int>();
        public List<int> SkillAwa_Priority_List = new List<int>();
        public List<int> Skill1ex_Priority_List = new List<int>();
        public List<int> Skill2ex_Priority_List = new List<int>();
        public List<int> Skill3ex_Priority_List = new List<int>();
        public List<int> Skill4ex_Priority_List = new List<int>();
        public List<int> Skill5ex_Priority_List = new List<int>();
        public List<int> Skill6ex_Priority_List = new List<int>();
        public List<int> SkillAwaex_Priority_List = new List<int>();
        public List<int> Skill1air_Priority_List = new List<int>();
        public List<int> Skill2air_Priority_List = new List<int>();
        public List<int> Skill3air_Priority_List = new List<int>();
        public List<int> Skill4air_Priority_List = new List<int>();
        public List<int> Skill5air_Priority_List = new List<int>();
        public List<int> Skill6air_Priority_List = new List<int>();
        public List<int> SkillAwaair_Priority_List = new List<int>();
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
            FileBytes = File.ReadAllBytes(FilePath);
            EntryCount = FileBytes[0x130] + FileBytes[0x131] * 256 + FileBytes[0x132] * 65536 + FileBytes[0x133] * 16777216;

            for (int x2 = 0; x2 < EntryCount; x2++) {
                long _ptr = 0x13C + 0x190 * x2;
                string Skill1 = "";
                long _ptrCharacter3 = FileBytes[_ptr + 0x40] + FileBytes[_ptr + 0x41] * 0x100 + FileBytes[_ptr + 0x42] * 0x10000 + FileBytes[_ptr + 0x43] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + 0x40 + _ptrCharacter3 + a] != 0) {
                        string str3 = Skill1;
                        char c = (char)FileBytes[_ptr + 0x40 + _ptrCharacter3 + a];
                        Skill1 = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill1_ex = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x50] + FileBytes[_ptr + 0x51] * 0x100 + FileBytes[_ptr + 0x52] * 0x10000 + FileBytes[_ptr + 0x53] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + 0x50 + _ptrCharacter3 + a] != 0) {
                        string str3 = Skill1_ex;
                        char c = (char)FileBytes[_ptr + 0x50 + _ptrCharacter3 + a];
                        Skill1_ex = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill1_air = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x60] + FileBytes[_ptr + 0x61] * 0x100 + FileBytes[_ptr + 0x62] * 0x10000 + FileBytes[_ptr + 0x63] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + 0x60 + _ptrCharacter3 + a] != 0) {
                        string str3 = Skill1_air;
                        char c = (char)FileBytes[_ptr + 0x60 + _ptrCharacter3 + a];
                        Skill1_air = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill2 = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x70] + FileBytes[_ptr + 0x71] * 0x100 + FileBytes[_ptr + 0x72] * 0x10000 + FileBytes[_ptr + 0x73] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0x70 + a] != 0) {
                        string str3 = Skill2;
                        char c = (char)FileBytes[_ptr + 0x70 + _ptrCharacter3 + a];
                        Skill2 = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill2_ex = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x80] + FileBytes[_ptr + 0x81] * 0x100 + FileBytes[_ptr + 0x82] * 0x10000 + FileBytes[_ptr + 0x83] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0x80 + a] != 0) {
                        string str3 = Skill2_ex;
                        char c = (char)FileBytes[_ptr + 0x80 + _ptrCharacter3 + a];
                        Skill2_ex = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill2_air = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x90] + FileBytes[_ptr + 0x91] * 0x100 + FileBytes[_ptr + 0x92] * 0x10000 + FileBytes[_ptr + 0x93] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0x90 + a] != 0) {
                        string str3 = Skill2_air;
                        char c = (char)FileBytes[_ptr + 0x90 + _ptrCharacter3 + a];
                        Skill2_air = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill3 = "";
                _ptrCharacter3 = FileBytes[_ptr + 0xA0] + FileBytes[_ptr + 0xA1] * 0x100 + FileBytes[_ptr + 0xA2] * 0x10000 + FileBytes[_ptr + 0xA3] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0xA0 + a] != 0) {
                        string str3 = Skill3;
                        char c = (char)FileBytes[_ptr + 0xA0 + _ptrCharacter3 + a];
                        Skill3 = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill3_ex = "";
                _ptrCharacter3 = FileBytes[_ptr + 0xB0] + FileBytes[_ptr + 0xB1] * 0x100 + FileBytes[_ptr + 0xB2] * 0x10000 + FileBytes[_ptr + 0xB3] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0xB0 + a] != 0) {
                        string str3 = Skill3_ex;
                        char c = (char)FileBytes[_ptr + 0xB0 + _ptrCharacter3 + a];
                        Skill3_ex = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill3_air = "";
                _ptrCharacter3 = FileBytes[_ptr + 0xC0] + FileBytes[_ptr + 0xC1] * 0x100 + FileBytes[_ptr + 0xC2] * 0x10000 + FileBytes[_ptr + 0xC3] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0xC0 + a] != 0) {
                        string str3 = Skill3_air;
                        char c = (char)FileBytes[_ptr + 0xC0 + _ptrCharacter3 + a];
                        Skill3_air = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill4 = "";
                _ptrCharacter3 = FileBytes[_ptr + 0xD0] + FileBytes[_ptr + 0xD1] * 0x100 + FileBytes[_ptr + 0xD2] * 0x10000 + FileBytes[_ptr + 0xD3] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0xD0 + a] != 0) {
                        string str3 = Skill4;
                        char c = (char)FileBytes[_ptr + 0xD0 + _ptrCharacter3 + a];
                        Skill4 = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill4_ex = "";
                _ptrCharacter3 = FileBytes[_ptr + 0xE0] + FileBytes[_ptr + 0xE1] * 0x100 + FileBytes[_ptr + 0xE2] * 0x10000 + FileBytes[_ptr + 0xE3] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0xE0 + a] != 0) {
                        string str3 = Skill4_ex;
                        char c = (char)FileBytes[_ptr + 0xE0 + _ptrCharacter3 + a];
                        Skill4_ex = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill4_air = "";
                _ptrCharacter3 = FileBytes[_ptr + 0xF0] + FileBytes[_ptr + 0xF1] * 0x100 + FileBytes[_ptr + 0xF2] * 0x10000 + FileBytes[_ptr + 0xF3] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0xF0 + a] != 0) {
                        string str3 = Skill4_air;
                        char c = (char)FileBytes[_ptr + 0xF0 + _ptrCharacter3 + a];
                        Skill4_air = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill5 = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x100] + FileBytes[_ptr + 0x101] * 0x100 + FileBytes[_ptr + 0x102] * 0x10000 + FileBytes[_ptr + 0x103] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0x100 + a] != 0) {
                        string str3 = Skill5;
                        char c = (char)FileBytes[_ptr + 0x100 + _ptrCharacter3 + a];
                        Skill5 = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill5_ex = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x110] + FileBytes[_ptr + 0x111] * 0x100 + FileBytes[_ptr + 0x112] * 0x10000 + FileBytes[_ptr + 0x113] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0x110 + a] != 0) {
                        string str3 = Skill5_ex;
                        char c = (char)FileBytes[_ptr + 0x110 + _ptrCharacter3 + a];
                        Skill5_ex = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill5_air = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x120] + FileBytes[_ptr + 0x121] * 0x100 + FileBytes[_ptr + 0x122] * 0x10000 + FileBytes[_ptr + 0x123] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0x120 + a] != 0) {
                        string str3 = Skill5_air;
                        char c = (char)FileBytes[_ptr + 0x120 + _ptrCharacter3 + a];
                        Skill5_air = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill6 = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x130] + FileBytes[_ptr + 0x131] * 0x100 + FileBytes[_ptr + 0x132] * 0x10000 + FileBytes[_ptr + 0x133] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0x130 + a] != 0) {
                        string str3 = Skill6;
                        char c = (char)FileBytes[_ptr + 0x130 + _ptrCharacter3 + a];
                        Skill6 = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill6_ex = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x140] + FileBytes[_ptr + 0x141] * 0x100 + FileBytes[_ptr + 0x142] * 0x10000 + FileBytes[_ptr + 0x143] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0x140 + a] != 0) {
                        string str3 = Skill6_ex;
                        char c = (char)FileBytes[_ptr + 0x140 + _ptrCharacter3 + a];
                        Skill6_ex = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string Skill6_air = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x150] + FileBytes[_ptr + 0x151] * 0x100 + FileBytes[_ptr + 0x152] * 0x10000 + FileBytes[_ptr + 0x153] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0x150 + a] != 0) {
                        string str3 = Skill6_air;
                        char c = (char)FileBytes[_ptr + 0x150 + _ptrCharacter3 + a];
                        Skill6_air = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string SkillAwa = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x160] + FileBytes[_ptr + 0x161] * 0x100 + FileBytes[_ptr + 0x162] * 0x10000 + FileBytes[_ptr + 0x163] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0x160 + a] != 0) {
                        string str3 = SkillAwa;
                        char c = (char)FileBytes[_ptr + 0x160 + _ptrCharacter3 + a];
                        SkillAwa = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string SkillAwa_ex = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x170] + FileBytes[_ptr + 0x171] * 0x100 + FileBytes[_ptr + 0x172] * 0x10000 + FileBytes[_ptr + 0x173] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0x170 + a] != 0) {
                        string str3 = SkillAwa_ex;
                        char c = (char)FileBytes[_ptr + 0x170 + _ptrCharacter3 + a];
                        SkillAwa_ex = str3 + c;
                    } else {
                        a = 30;
                    }
                }
                string SkillAwa_air = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x180] + FileBytes[_ptr + 0x181] * 0x100 + FileBytes[_ptr + 0x182] * 0x10000 + FileBytes[_ptr + 0x183] * 0x1000000;
                for (int a = 0; a < 30; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0x180 + a] != 0) {
                        string str3 = SkillAwa_air;
                        char c = (char)FileBytes[_ptr + 0x180 + _ptrCharacter3 + a];
                        SkillAwa_air = str3 + c;
                    } else {
                        a = 30;
                    }
                }

                float Skill1_CUC_value = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x4);
                float Skill1_CUCC_value = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x8);
                float Skill2_CUC_value = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0xC);
                float Skill2_CUCC_value = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x10);
                float Skill3_CUC_value = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x14);
                float Skill3_CUCC_value = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x18);
                float Skill4_CUC_value = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x1C);
                float Skill4_CUCC_value = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x20);
                float Skill5_CUC_value = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x24);
                float Skill5_CUCC_value = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x28);
                float Skill6_CUC_value = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x2C);
                float Skill6_CUCC_value = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x30);
                float SkillAwa_CUC_value = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x34);
                float SkillAwa_CUCC_value = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x38);
                int Skill1_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x48);
                int Skill1ex_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x58);
                int Skill1air_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x68);
                int Skill2_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x78);
                int Skill2ex_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x88);
                int Skill2air_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x98);
                int Skill3_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0xa8);
                int Skill3ex_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0xb8);
                int Skill3air_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0xc8);
                int Skill4_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0xD8);
                int Skill4ex_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0xE8);
                int Skill4air_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0xF8);
                int Skill5_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x108);
                int Skill5ex_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x118);
                int Skill5air_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x128);
                int Skill6_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x138);
                int Skill6ex_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x148);
                int Skill6air_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x158);
                int SkillAwa_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x168);
                int SkillAwaex_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x178);
                int SkillAwaair_Prior_value = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 0x188);

                byte[] Characode = new byte[4]
                {
                    FileBytes[_ptr],
                    FileBytes[_ptr + 1],
                    FileBytes[_ptr + 2],
                    FileBytes[_ptr + 3]
                };
                CharacodeList.Add(Characode);
                Skill1List.Add(Skill1);
                Skill1_ex_List.Add(Skill1_ex);
                Skill1_air_List.Add(Skill1_air);
                Skill2List.Add(Skill2);
                Skill2_ex_List.Add(Skill2_ex);
                Skill2_air_List.Add(Skill2_air);
                Skill3List.Add(Skill3);
                Skill3_ex_List.Add(Skill3_ex);
                Skill3_air_List.Add(Skill3_air);
                Skill4List.Add(Skill4);
                Skill4_ex_List.Add(Skill4_ex);
                Skill4_air_List.Add(Skill4_air);
                Skill5List.Add(Skill5);
                Skill5_ex_List.Add(Skill5_ex);
                Skill5_air_List.Add(Skill5_air);
                Skill6List.Add(Skill6);
                Skill6_ex_List.Add(Skill6_ex);
                Skill6_air_List.Add(Skill6_air);
                SkillAwaList.Add(SkillAwa);
                SkillAwa_ex_List.Add(SkillAwa_ex);
                SkillAwa_air_List.Add(SkillAwa_air);
                Skill1_CUC_List.Add(Skill1_CUC_value);
                Skill1_CUCC_List.Add(Skill1_CUCC_value);
                Skill2_CUC_List.Add(Skill2_CUC_value);
                Skill2_CUCC_List.Add(Skill2_CUCC_value);
                Skill3_CUC_List.Add(Skill3_CUC_value);
                Skill3_CUCC_List.Add(Skill3_CUCC_value);
                Skill4_CUC_List.Add(Skill4_CUC_value);
                Skill4_CUCC_List.Add(Skill4_CUCC_value);
                Skill5_CUC_List.Add(Skill5_CUC_value);
                Skill5_CUCC_List.Add(Skill5_CUCC_value);
                Skill6_CUC_List.Add(Skill6_CUC_value);
                Skill6_CUCC_List.Add(Skill6_CUCC_value);
                SkillAwa_CUC_List.Add(SkillAwa_CUC_value);
                SkillAwa_CUCC_List.Add(SkillAwa_CUCC_value);

                Skill1_Priority_List.Add(Skill1_Prior_value);
                Skill2_Priority_List.Add(Skill2_Prior_value);
                Skill3_Priority_List.Add(Skill3_Prior_value);
                Skill4_Priority_List.Add(Skill4_Prior_value);
                Skill5_Priority_List.Add(Skill5_Prior_value);
                Skill6_Priority_List.Add(Skill6_Prior_value);
                SkillAwa_Priority_List.Add(SkillAwa_Prior_value);
                Skill1ex_Priority_List.Add(Skill1ex_Prior_value);
                Skill2ex_Priority_List.Add(Skill2ex_Prior_value);
                Skill3ex_Priority_List.Add(Skill3ex_Prior_value);
                Skill4ex_Priority_List.Add(Skill4ex_Prior_value);
                Skill5ex_Priority_List.Add(Skill5ex_Prior_value);
                Skill6ex_Priority_List.Add(Skill6ex_Prior_value);
                SkillAwaex_Priority_List.Add(SkillAwaex_Prior_value);
                Skill1air_Priority_List.Add(Skill1air_Prior_value);
                Skill2air_Priority_List.Add(Skill2air_Prior_value);
                Skill3air_Priority_List.Add(Skill3air_Prior_value);
                Skill4air_Priority_List.Add(Skill4air_Prior_value);
                Skill5air_Priority_List.Add(Skill5air_Prior_value);
                Skill6air_Priority_List.Add(Skill6air_Prior_value);
                SkillAwaair_Priority_List.Add(Skill6air_Prior_value);

            }
        }
        public void CloseFile() {
            ClearFile();
            FileOpen = false;
            FilePath = "";
        }
        public void ClearFile() {
            FileBytes = new byte[0];
            EntryCount = 0;
            CharacodeList = new List<byte[]>();
            Skill1List = new List<string>();
            Skill2List = new List<string>();
            Skill3List = new List<string>();
            Skill4List = new List<string>();
            Skill5List = new List<string>();
            Skill6List = new List<string>();
            SkillAwaList = new List<string>();
            Skill1_ex_List = new List<string>();
            Skill2_ex_List = new List<string>();
            Skill3_ex_List = new List<string>();
            Skill4_ex_List = new List<string>();
            Skill5_ex_List = new List<string>();
            Skill6_ex_List = new List<string>();
            SkillAwa_ex_List = new List<string>();
            Skill1_air_List = new List<string>();
            Skill2_air_List = new List<string>();
            Skill3_air_List = new List<string>();
            Skill4_air_List = new List<string>();
            Skill5_air_List = new List<string>();
            Skill6_air_List = new List<string>();
            SkillAwa_air_List = new List<string>();
            Skill1_CUC_List = new List<float>();
            Skill2_CUC_List = new List<float>();
            Skill3_CUC_List = new List<float>();
            Skill4_CUC_List = new List<float>();
            Skill5_CUC_List = new List<float>();
            Skill6_CUC_List = new List<float>();
            SkillAwa_CUC_List = new List<float>();
            Skill1_CUCC_List = new List<float>();
            Skill2_CUCC_List = new List<float>();
            Skill3_CUCC_List = new List<float>();
            Skill4_CUCC_List = new List<float>();
            Skill5_CUCC_List = new List<float>();
            Skill6_CUCC_List = new List<float>();
            SkillAwa_CUCC_List = new List<float>();
            Skill1_Priority_List = new List<int>();
            Skill2_Priority_List = new List<int>();
            Skill3_Priority_List = new List<int>();
            Skill4_Priority_List = new List<int>();
            Skill5_Priority_List = new List<int>();
            Skill6_Priority_List = new List<int>();
            SkillAwa_Priority_List = new List<int>();
            Skill1ex_Priority_List = new List<int>();
            Skill2ex_Priority_List = new List<int>();
            Skill3ex_Priority_List = new List<int>();
            Skill4ex_Priority_List = new List<int>();
            Skill5ex_Priority_List = new List<int>();
            Skill6ex_Priority_List = new List<int>();
            SkillAwaex_Priority_List = new List<int>();
            Skill1air_Priority_List = new List<int>();
            Skill2air_Priority_List = new List<int>();
            Skill3air_Priority_List = new List<int>();
            Skill4air_Priority_List = new List<int>();
            Skill5air_Priority_List = new List<int>();
            Skill6air_Priority_List = new List<int>();
            SkillAwaair_Priority_List = new List<int>();
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
            byte[] header = new byte[316]
            {
                0x4E,0x55,0x43,0x43,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xE8,0x00,0x00,0x00,0x03,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x3B,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x24,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x21,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x30,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x4E,0x75,0x6C,0x6C,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x42,0x69,0x6E,0x61,0x72,0x79,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x50,0x61,0x67,0x65,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x49,0x6E,0x64,0x65,0x78,0x00,0x00,0x62,0x69,0x6E,0x5F,0x6C,0x65,0x2F,0x78,0x36,0x34,0x2F,0x73,0x6B,0x69,0x6C,0x6C,0x43,0x75,0x73,0x74,0x6F,0x6D,0x69,0x7A,0x65,0x50,0x61,0x72,0x61,0x6D,0x2E,0x62,0x69,0x6E,0x00,0x00,0x73,0x6B,0x69,0x6C,0x6C,0x43,0x75,0x73,0x74,0x6F,0x6D,0x69,0x7A,0x65,0x50,0x61,0x72,0x61,0x6D,0x00,0x50,0x61,0x67,0x65,0x30,0x00,0x69,0x6E,0x64,0x65,0x78,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x01,0x6D,0x34,0x00,0x00,0x00,0x01,0x00,0x79,0x00,0x00,0x00,0x01,0x6D,0x30,0xE9,0x03,0x00,0x00,0xD4,0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            };
            for (int x4 = 0; x4 < header.Length; x4++) {
                file.Add(header[x4]);
            }
            for (int x3 = 0; x3 < EntryCount * 400; x3++) {
                file.Add(0);
            }
            List<int> Skill1Pointer = new List<int>();
            List<int> Skill2Pointer = new List<int>();
            List<int> Skill3Pointer = new List<int>();
            List<int> Skill4Pointer = new List<int>();
            List<int> Skill5Pointer = new List<int>();
            List<int> Skill6Pointer = new List<int>();
            List<int> SkillAwaPointer = new List<int>();
            List<int> Skill1_ex_Pointer = new List<int>();
            List<int> Skill2_ex_Pointer = new List<int>();
            List<int> Skill3_ex_Pointer = new List<int>();
            List<int> Skill4_ex_Pointer = new List<int>();
            List<int> Skill5_ex_Pointer = new List<int>();
            List<int> Skill6_ex_Pointer = new List<int>();
            List<int> SkillAwa_ex_Pointer = new List<int>();
            List<int> Skill1_air_Pointer = new List<int>();
            List<int> Skill2_air_Pointer = new List<int>();
            List<int> Skill3_air_Pointer = new List<int>();
            List<int> Skill4_air_Pointer = new List<int>();
            List<int> Skill5_air_Pointer = new List<int>();
            List<int> Skill6_air_Pointer = new List<int>();
            List<int> SkillAwa_air_Pointer = new List<int>();

            for (int x2 = 0; x2 < EntryCount; x2++) {
                Skill1Pointer.Add(file.Count);
                int nameLength3 = Skill1List[x2].Length;
                byte[] Skill1_prior_byte = new byte[0];

                if (Skill1List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill1List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill1_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x48 + a8] = Skill1_prior_byte[a8];
                    }

                }
                Skill2Pointer.Add(file.Count);
                nameLength3 = Skill2List[x2].Length;
                if (Skill2List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill2List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill2_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x78 + a8] = Skill1_prior_byte[a8];
                    }
                }
                Skill3Pointer.Add(file.Count);
                nameLength3 = Skill3List[x2].Length;
                if (Skill3List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill3List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill3_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0xA8 + a8] = Skill1_prior_byte[a8];
                    }

                }
                Skill4Pointer.Add(file.Count);
                nameLength3 = Skill4List[x2].Length;
                if (Skill4List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill4List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill4_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0xD8 + a8] = Skill1_prior_byte[a8];
                    }

                }
                Skill5Pointer.Add(file.Count);
                nameLength3 = Skill5List[x2].Length;
                if (Skill5List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill5List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill5_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x108 + a8] = Skill1_prior_byte[a8];
                    }

                }
                Skill6Pointer.Add(file.Count);
                nameLength3 = Skill6List[x2].Length;
                if (Skill6List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill6List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill6_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x138 + a8] = Skill1_prior_byte[a8];
                    }

                }
                SkillAwaPointer.Add(file.Count);
                nameLength3 = SkillAwaList[x2].Length;
                if (SkillAwaList[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)SkillAwaList[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(SkillAwa_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x168 + a8] = Skill1_prior_byte[a8];
                    }

                }
                Skill1_ex_Pointer.Add(file.Count);
                nameLength3 = Skill1_ex_List[x2].Length;
                if (Skill1_ex_List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill1_ex_List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill1ex_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x58 + a8] = Skill1_prior_byte[a8];
                    }

                }
                Skill2_ex_Pointer.Add(file.Count);
                nameLength3 = Skill2_ex_List[x2].Length;
                if (Skill2_ex_List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill2_ex_List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }

                    Skill1_prior_byte = BitConverter.GetBytes(Skill2ex_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x88 + a8] = Skill1_prior_byte[a8];
                    }
                }
                Skill3_ex_Pointer.Add(file.Count);
                nameLength3 = Skill3_ex_List[x2].Length;
                if (Skill3_ex_List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill3_ex_List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill3ex_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0xB8 + a8] = Skill1_prior_byte[a8];
                    }

                }
                Skill4_ex_Pointer.Add(file.Count);
                nameLength3 = Skill4_ex_List[x2].Length;
                if (Skill4_ex_List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill4_ex_List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill4ex_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0xE8 + a8] = Skill1_prior_byte[a8];
                    }

                }
                Skill5_ex_Pointer.Add(file.Count);
                nameLength3 = Skill5_ex_List[x2].Length;
                if (Skill5_ex_List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill5_ex_List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill5ex_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x118 + a8] = Skill1_prior_byte[a8];
                    }

                }
                Skill6_ex_Pointer.Add(file.Count);
                nameLength3 = Skill6_ex_List[x2].Length;
                if (Skill6_ex_List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill6_ex_List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill6ex_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x148 + a8] = Skill1_prior_byte[a8];
                    }

                }
                SkillAwa_ex_Pointer.Add(file.Count);
                nameLength3 = SkillAwa_ex_List[x2].Length;
                if (SkillAwa_ex_List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)SkillAwa_ex_List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(SkillAwaex_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x178 + a8] = Skill1_prior_byte[a8];
                    }


                }
                Skill1_air_Pointer.Add(file.Count);
                nameLength3 = Skill1_air_List[x2].Length;
                if (Skill1_air_List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill1_air_List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill1air_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x68 + a8] = Skill1_prior_byte[a8];
                    }
                }
                Skill2_air_Pointer.Add(file.Count);
                nameLength3 = Skill2_air_List[x2].Length;
                if (Skill2_air_List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill2_air_List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }

                    Skill1_prior_byte = BitConverter.GetBytes(Skill2air_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x98 + a8] = Skill1_prior_byte[a8];
                    }
                }
                Skill3_air_Pointer.Add(file.Count);
                nameLength3 = Skill3_air_List[x2].Length;
                if (Skill3_air_List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill3_air_List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill3air_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0xC8 + a8] = Skill1_prior_byte[a8];
                    }
                }
                Skill4_air_Pointer.Add(file.Count);
                nameLength3 = Skill4_air_List[x2].Length;
                if (Skill4_air_List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill4_air_List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill4air_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0xF8 + a8] = Skill1_prior_byte[a8];
                    }

                }
                Skill5_air_Pointer.Add(file.Count);
                nameLength3 = Skill5_air_List[x2].Length;
                if (Skill5_air_List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill5_air_List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill5air_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x128 + a8] = Skill1_prior_byte[a8];
                    }

                }
                Skill6_air_Pointer.Add(file.Count);
                nameLength3 = Skill6_air_List[x2].Length;
                if (Skill6_air_List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)Skill6_air_List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(Skill6air_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x158 + a8] = Skill1_prior_byte[a8];
                    }

                }
                SkillAwa_air_Pointer.Add(file.Count);
                nameLength3 = SkillAwa_air_List[x2].Length;
                if (SkillAwa_air_List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)SkillAwa_air_List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    Skill1_prior_byte = BitConverter.GetBytes(SkillAwaair_Priority_List[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[316 + 400 * x2 + 0x188 + a8] = Skill1_prior_byte[a8];
                    }
                }
                int newPointer3 = Skill1Pointer[x2] - 316 - 400 * x2 - 0x40;
                byte[] ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill1List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x40 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill1Pointer[x2] - 316 - 400 * x2 - 0x40;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x40 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Skill1_ex_Pointer[x2] - 316 - 400 * x2 - 0x50;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill1_ex_List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x50 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill1_ex_Pointer[x2] - 316 - 400 * x2 - 0x50;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x50 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Skill1_air_Pointer[x2] - 316 - 400 * x2 - 0x60;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill1_air_List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x60 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill1_air_Pointer[x2] - 316 - 400 * x2 - 0x60;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x60 + a7] = ptrBytes3[a7];
                    }
                }

                newPointer3 = Skill2Pointer[x2] - 316 - 400 * x2 - 0x70;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill2List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x70 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill2Pointer[x2] - 316 - 400 * x2 - 0x70;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x70 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Skill2_ex_Pointer[x2] - 316 - 400 * x2 - 0x80;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill2_ex_List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x80 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill2_ex_Pointer[x2] - 316 - 400 * x2 - 0x80;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x80 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Skill2_air_Pointer[x2] - 316 - 400 * x2 - 0x90;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill2_air_List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x90 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill2_air_Pointer[x2] - 316 - 400 * x2 - 0x90;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x90 + a7] = ptrBytes3[a7];
                    }
                }
                //
                newPointer3 = Skill3Pointer[x2] - 316 - 400 * x2 - 0xA0;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill3List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0xA0 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill3Pointer[x2] - 316 - 400 * x2 - 0xA0;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0xA0 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Skill3_ex_Pointer[x2] - 316 - 400 * x2 - 0xB0;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill3_ex_List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0xB0 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill3_ex_Pointer[x2] - 316 - 400 * x2 - 0xB0;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0xB0 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Skill3_air_Pointer[x2] - 316 - 400 * x2 - 0xC0;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill3_air_List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0xC0 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill3_air_Pointer[x2] - 316 - 400 * x2 - 0xC0;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0xC0 + a7] = ptrBytes3[a7];
                    }
                }

                //
                newPointer3 = Skill4Pointer[x2] - 316 - 400 * x2 - 0xD0;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill4List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0xD0 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill4Pointer[x2] - 316 - 400 * x2 - 0xD0;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0xD0 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Skill4_ex_Pointer[x2] - 316 - 400 * x2 - 0xE0;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill4_ex_List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0xE0 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill4_ex_Pointer[x2] - 316 - 400 * x2 - 0xE0;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0xE0 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Skill4_air_Pointer[x2] - 316 - 400 * x2 - 0xF0;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill4_air_List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0xF0 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill4_air_Pointer[x2] - 316 - 400 * x2 - 0xF0;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0xF0 + a7] = ptrBytes3[a7];
                    }
                }
                //
                newPointer3 = Skill5Pointer[x2] - 316 - 400 * x2 - 0x100;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill5List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x100 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill5Pointer[x2] - 316 - 400 * x2 - 0x100;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x100 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Skill5_ex_Pointer[x2] - 316 - 400 * x2 - 0x110;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill5_ex_List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x110 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill5_ex_Pointer[x2] - 316 - 400 * x2 - 0x110;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x110 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Skill5_air_Pointer[x2] - 316 - 400 * x2 - 0x120;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill5_air_List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x120 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill5_air_Pointer[x2] - 316 - 400 * x2 - 0x120;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x120 + a7] = ptrBytes3[a7];
                    }
                }
                //
                newPointer3 = Skill6Pointer[x2] - 316 - 400 * x2 - 0x130;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill6List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x130 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill6Pointer[x2] - 316 - 400 * x2 - 0x130;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x130 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Skill6_ex_Pointer[x2] - 316 - 400 * x2 - 0x140;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill6_ex_List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x140 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill6_ex_Pointer[x2] - 316 - 400 * x2 - 0x140;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x140 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Skill6_air_Pointer[x2] - 316 - 400 * x2 - 0x150;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (Skill6_air_List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x150 + a7] = 0;
                    }
                } else {
                    newPointer3 = Skill6_air_Pointer[x2] - 316 - 400 * x2 - 0x150;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x150 + a7] = ptrBytes3[a7];
                    }
                }
                //
                newPointer3 = SkillAwaPointer[x2] - 316 - 400 * x2 - 0x160;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (SkillAwaList[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x160 + a7] = 0;
                    }
                } else {
                    newPointer3 = SkillAwaPointer[x2] - 316 - 400 * x2 - 0x160;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x160 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = SkillAwa_ex_Pointer[x2] - 316 - 400 * x2 - 0x170;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (SkillAwa_ex_List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x170 + a7] = 0;
                    }
                } else {
                    newPointer3 = SkillAwa_ex_Pointer[x2] - 316 - 400 * x2 - 0x170;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x170 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = SkillAwa_air_Pointer[x2] - 316 - 400 * x2 - 0x180;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (SkillAwa_air_List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x180 + a7] = 0;
                    }
                } else {
                    newPointer3 = SkillAwa_air_Pointer[x2] - 316 - 400 * x2 - 0x180;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[316 + 400 * x2 + 0x180 + a7] = ptrBytes3[a7];
                    }
                }
                // VALUES
                byte[] o_a = CharacodeList[x2];
                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + a8] = o_a[a8];
                }
                byte[] Skill1_CUC_byte = BitConverter.GetBytes(Skill1_CUC_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + 0x4 + a8] = Skill1_CUC_byte[a8];
                }
                byte[] Skill1_CUCC_byte = BitConverter.GetBytes(Skill1_CUCC_List[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + 0x8 + a8] = Skill1_CUCC_byte[a8];
                }
                byte[] Skill2_CUC_byte = BitConverter.GetBytes(Skill2_CUC_List[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + 0xC + a8] = Skill2_CUC_byte[a8];
                }
                byte[] Skill2_CUCC_byte = BitConverter.GetBytes(Skill2_CUCC_List[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + 0x10 + a8] = Skill2_CUCC_byte[a8];
                }
                byte[] Skill3_CUC_byte = BitConverter.GetBytes(Skill3_CUC_List[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + 0x14 + a8] = Skill3_CUC_byte[a8];
                }
                byte[] Skill3_CUCC_byte = BitConverter.GetBytes(Skill3_CUCC_List[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + 0x18 + a8] = Skill3_CUCC_byte[a8];
                }
                byte[] Skill4_CUC_byte = BitConverter.GetBytes(Skill4_CUC_List[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + 0x1C + a8] = Skill4_CUC_byte[a8];
                }
                byte[] Skill4_CUCC_byte = BitConverter.GetBytes(Skill4_CUCC_List[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + 0x20 + a8] = Skill4_CUCC_byte[a8];
                }
                byte[] Skill5_CUC_byte = BitConverter.GetBytes(Skill5_CUC_List[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + 0x24 + a8] = Skill5_CUC_byte[a8];
                }
                byte[] Skill5_CUCC_byte = BitConverter.GetBytes(Skill5_CUCC_List[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + 0x28 + a8] = Skill5_CUCC_byte[a8];
                }
                byte[] Skill6_CUC_byte = BitConverter.GetBytes(Skill6_CUC_List[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + 0x2C + a8] = Skill6_CUC_byte[a8];
                }
                byte[] Skill6_CUCC_byte = BitConverter.GetBytes(Skill6_CUCC_List[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + 0x30 + a8] = Skill6_CUCC_byte[a8];
                }
                byte[] SkillAwa_CUC_byte = BitConverter.GetBytes(SkillAwa_CUC_List[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + 0x34 + a8] = SkillAwa_CUC_byte[a8];
                }
                byte[] SkillAwa_CUCC_byte = BitConverter.GetBytes(SkillAwa_CUCC_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[316 + 400 * x2 + 0x38 + a8] = SkillAwa_CUCC_byte[a8];
                }




            }
            int FileSize3 = file.Count - 300;
            byte[] sizeBytes3 = BitConverter.GetBytes(FileSize3);
            byte[] sizeBytes2 = BitConverter.GetBytes(FileSize3 + 4);
            for (int a20 = 0; a20 < 4; a20++) {
                file[296 + a20] = sizeBytes3[3 - a20];
            }
            for (int a19 = 0; a19 < 4; a19++) {
                file[284 + a19] = sizeBytes2[3 - a19];
            }
            byte[] countBytes = BitConverter.GetBytes(EntryCount);
            for (int a18 = 0; a18 < 4; a18++) {
                file[304 + a18] = countBytes[a18];
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
