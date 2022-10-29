using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_SpSkillCustomizeParamEditor_code {
        public bool FileOpen = false;

        public string FilePath = "";

        public byte[] fileBytes = new byte[0];

        public int EntryCount = 0;

        public List<byte[]> CharacodeList = new List<byte[]>();
        public List<byte[]> spl1_chUsageCountValueList = new List<byte[]>();
        public List<byte[]> spl2_chUsageCountValueList = new List<byte[]>();
        public List<byte[]> spl3_chUsageCountValueList = new List<byte[]>();
        public List<byte[]> spl4_chUsageCountValueList = new List<byte[]>();
        public List<float> spl1_chUsageCountValueListFloat = new List<float>();
        public List<float> spl2_chUsageCountValueListFloat = new List<float>();
        public List<float> spl3_chUsageCountValueListFloat = new List<float>();
        public List<float> spl4_chUsageCountValueListFloat = new List<float>();
        public List<string> spl1_NameList = new List<string>();
        public List<string> spl2_NameList = new List<string>();
        public List<string> spl3_NameList = new List<string>();
        public List<string> spl4_NameList = new List<string>();
        public List<int> spl1_PriorList = new List<int>();
        public List<int> spl2_PriorList = new List<int>();
        public List<int> spl3_PriorList = new List<int>();
        public List<int> spl4_PriorList = new List<int>();

        public List<byte[]> WeirdValuesList = new List<byte[]>();
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
            EntryCount = FileBytes[308] + FileBytes[309] * 256 + FileBytes[310] * 65536 + FileBytes[311] * 16777216;

            for (int x = 0; x < EntryCount; x++) {
                long _ptr = 320 + 112 * x;
                byte[] Characode = new byte[4]
                {
                    FileBytes[_ptr],
                    FileBytes[_ptr + 1],
                    0,
                    0
                };
                byte[] spl1_chUsageCountValue = new byte[4]
                {
                    FileBytes[_ptr + 4],
                    FileBytes[_ptr + 5],
                    FileBytes[_ptr + 6],
                    FileBytes[_ptr + 7]
                };
                byte[] spl2_chUsageCountValue = new byte[4]
                {
                    FileBytes[_ptr + 8],
                    FileBytes[_ptr + 9],
                    FileBytes[_ptr + 10],
                    FileBytes[_ptr + 11]
                };
                byte[] spl3_chUsageCountValue = new byte[4]
                {
                    FileBytes[_ptr + 12],
                    FileBytes[_ptr + 13],
                    FileBytes[_ptr + 14],
                    FileBytes[_ptr + 15]
                };
                byte[] spl4_chUsageCountValue = new byte[4]
                {
                    FileBytes[_ptr + 16],
                    FileBytes[_ptr + 17],
                    FileBytes[_ptr + 18],
                    FileBytes[_ptr + 19]
                };
                float spl1_chUsageCountValueFloat = MainFunctions.b_ReadFloat(spl1_chUsageCountValue, 0);
                float spl2_chUsageCountValueFloat = MainFunctions.b_ReadFloat(spl2_chUsageCountValue, 0);
                float spl3_chUsageCountValueFloat = MainFunctions.b_ReadFloat(spl3_chUsageCountValue, 0);
                float spl4_chUsageCountValueFloat = MainFunctions.b_ReadFloat(spl4_chUsageCountValue, 0);
                int spl1_prior = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 56);
                int spl2_prior = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 72);
                int spl3_prior = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 88);
                int spl4_prior = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)_ptr + 104);

                string Spl1Name = "";
                long _ptrSpl1Name3 = FileBytes[_ptr + 48] + FileBytes[_ptr + 49] * 256 + FileBytes[_ptr + 50] * 65536 + FileBytes[_ptr + 51] * 16777216;
                for (int a2 = 0; a2 < 20; a2++) {
                    if (FileBytes[_ptr + 48 + _ptrSpl1Name3 + a2] != 0) {
                        string str2 = Spl1Name;
                        char c = (char)FileBytes[_ptr + 48 + _ptrSpl1Name3 + a2];
                        Spl1Name = str2 + c;
                    } else {
                        a2 = 20;
                    }
                }
                string Spl2Name = "";
                long _ptrSpl2Name3 = FileBytes[_ptr + 64] + FileBytes[_ptr + 65] * 256 + FileBytes[_ptr + 66] * 65536 + FileBytes[_ptr + 67] * 16777216;
                for (int a2 = 0; a2 < 20; a2++) {
                    if (FileBytes[_ptr + 64 + _ptrSpl2Name3 + a2] != 0) {
                        string str2 = Spl2Name;
                        char c = (char)FileBytes[_ptr + 64 + _ptrSpl2Name3 + a2];
                        Spl2Name = str2 + c;
                    } else {
                        a2 = 20;
                    }
                }
                string Spl3Name = "";
                long _ptrSpl3Name3 = FileBytes[_ptr + 80] + FileBytes[_ptr + 81] * 256 + FileBytes[_ptr + 82] * 65536 + FileBytes[_ptr + 83] * 16777216;
                for (int a2 = 0; a2 < 20; a2++) {
                    if (FileBytes[_ptr + 80 + _ptrSpl3Name3 + a2] != 0) {
                        string str2 = Spl3Name;
                        char c = (char)FileBytes[_ptr + 80 + _ptrSpl3Name3 + a2];
                        Spl3Name = str2 + c;
                    } else {
                        a2 = 20;
                    }
                }
                string Spl4Name = "";
                long _ptrSpl4Name3 = FileBytes[_ptr + 96] + FileBytes[_ptr + 97] * 256 + FileBytes[_ptr + 98] * 65536 + FileBytes[_ptr + 99] * 16777216;
                for (int a2 = 0; a2 < 20; a2++) {
                    if (FileBytes[_ptr + 96 + _ptrSpl4Name3 + a2] != 0) {
                        string str2 = Spl4Name;
                        char c = (char)FileBytes[_ptr + 96 + _ptrSpl4Name3 + a2];
                        Spl4Name = str2 + c;
                    } else {
                        a2 = 20;
                    }
                }
                byte[] WeirdValues = new byte[24]
                {
                    FileBytes[_ptr + 20],
                    FileBytes[_ptr + 21],
                    FileBytes[_ptr + 22],
                    FileBytes[_ptr + 23],
                    FileBytes[_ptr + 24],
                    FileBytes[_ptr + 25],
                    FileBytes[_ptr + 26],
                    FileBytes[_ptr + 27],
                    FileBytes[_ptr + 28],
                    FileBytes[_ptr + 29],
                    FileBytes[_ptr + 30],
                    FileBytes[_ptr + 31],
                    FileBytes[_ptr + 32],
                    FileBytes[_ptr + 33],
                    FileBytes[_ptr + 34],
                    FileBytes[_ptr + 35],
                    FileBytes[_ptr + 36],
                    FileBytes[_ptr + 37],
                    FileBytes[_ptr + 38],
                    FileBytes[_ptr + 39],
                    FileBytes[_ptr + 40],
                    FileBytes[_ptr + 41],
                    FileBytes[_ptr + 42],
                    FileBytes[_ptr + 43]
                };
                CharacodeList.Add(Characode);
                spl1_chUsageCountValueList.Add(spl1_chUsageCountValue);
                spl2_chUsageCountValueList.Add(spl2_chUsageCountValue);
                spl3_chUsageCountValueList.Add(spl3_chUsageCountValue);
                spl4_chUsageCountValueList.Add(spl3_chUsageCountValue);
                spl1_chUsageCountValueListFloat.Add(spl1_chUsageCountValueFloat);
                spl2_chUsageCountValueListFloat.Add(spl2_chUsageCountValueFloat);
                spl3_chUsageCountValueListFloat.Add(spl3_chUsageCountValueFloat);
                spl4_chUsageCountValueListFloat.Add(spl4_chUsageCountValueFloat);
                spl1_PriorList.Add(spl1_prior);
                spl2_PriorList.Add(spl2_prior);
                spl3_PriorList.Add(spl3_prior);
                spl4_PriorList.Add(spl4_prior);

                spl1_NameList.Add(Spl1Name);
                spl2_NameList.Add(Spl2Name);
                spl3_NameList.Add(Spl3Name);
                spl4_NameList.Add(Spl4Name);

                WeirdValuesList.Add(WeirdValues);
            }

        }
        public void CloseFile() {
            ClearFile();
            FileOpen = false;
            FilePath = "";
        }
        public void ClearFile() {
            CharacodeList = new List<byte[]>();
            spl1_chUsageCountValueList = new List<byte[]>();
            spl2_chUsageCountValueList = new List<byte[]>();
            spl3_chUsageCountValueList = new List<byte[]>();
            spl4_chUsageCountValueList = new List<byte[]>();
            spl1_chUsageCountValueListFloat = new List<float>();
            spl2_chUsageCountValueListFloat = new List<float>();
            spl3_chUsageCountValueListFloat = new List<float>();
            spl4_chUsageCountValueListFloat = new List<float>();
            spl1_NameList = new List<string>();
            spl2_NameList = new List<string>();
            spl3_NameList = new List<string>();
            spl4_NameList = new List<string>();
            spl1_PriorList = new List<int>();
            spl2_PriorList = new List<int>();
            spl3_PriorList = new List<int>();
            spl4_PriorList = new List<int>();
            WeirdValuesList = new List<byte[]>();
            EntryCount = 0;
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
            byte[] header = new byte[320]
            {
                0x4E,0x55,0x43,0x43,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xEC,0x00,0x00,0x00,0x03,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x3B,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x26,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x23,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x30,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x4E,0x75,0x6C,0x6C,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x42,0x69,0x6E,0x61,0x72,0x79,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x50,0x61,0x67,0x65,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x49,0x6E,0x64,0x65,0x78,0x00,0x00,0x62,0x69,0x6E,0x5F,0x6C,0x65,0x2F,0x78,0x36,0x34,0x2F,0x73,0x70,0x53,0x6B,0x69,0x6C,0x6C,0x43,0x75,0x73,0x74,0x6F,0x6D,0x69,0x7A,0x65,0x50,0x61,0x72,0x61,0x6D,0x2E,0x62,0x69,0x6E,0x00,0x00,0x73,0x70,0x53,0x6B,0x69,0x6C,0x6C,0x43,0x75,0x73,0x74,0x6F,0x6D,0x69,0x7A,0x65,0x50,0x61,0x72,0x61,0x6D,0x00,0x50,0x61,0x67,0x65,0x30,0x00,0x69,0x6E,0x64,0x65,0x78,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x52,0xC4,0x00,0x00,0x00,0x01,0x00,0x79,0x00,0x00,0x00,0x00,0x52,0xC0,0xE9,0x03,0x00,0x00,0xA1,0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            };
            for (int x4 = 0; x4 < header.Length; x4++) {
                file.Add(header[x4]);
            }
            for (int x3 = 0; x3 < EntryCount * 112; x3++) {
                file.Add(0);
            }
            List<int> Spl1NamePointer = new List<int>();
            List<int> Spl2NamePointer = new List<int>();
            List<int> Spl3NamePointer = new List<int>();
            List<int> Spl4NamePointer = new List<int>();
            byte[] o_d = new byte[0];
            for (int x2 = 0; x2 < EntryCount; x2++) {
                Spl1NamePointer.Add(file.Count);
                int nameLength3 = spl1_NameList[x2].Length;
                if (spl1_NameList[x2] != "") {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)spl1_NameList[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
                        file.Add(0);
                    }
                    o_d = BitConverter.GetBytes(spl1_PriorList[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[320 + 112 * x2 + 56 + a8] = o_d[a8];
                    }
                }

                Spl2NamePointer.Add(file.Count);
                int nameLength4 = spl2_NameList[x2].Length;
                if (spl2_NameList[x2] != "") {
                    for (int a17 = 0; a17 < nameLength4; a17++) {
                        file.Add((byte)spl2_NameList[x2][a17]);
                    }
                    for (int a16 = nameLength4; a16 < nameLength4 + 1; a16++) {
                        file.Add(0);
                    }
                    o_d = BitConverter.GetBytes(spl2_PriorList[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[320 + 112 * x2 + 72 + a8] = o_d[a8];
                    }
                }

                Spl3NamePointer.Add(file.Count);
                int nameLength5 = spl3_NameList[x2].Length;
                if (spl3_NameList[x2] != "") {
                    for (int a17 = 0; a17 < nameLength5; a17++) {
                        file.Add((byte)spl3_NameList[x2][a17]);
                    }
                    for (int a16 = nameLength5; a16 < nameLength5 + 1; a16++) {
                        file.Add(0);
                    }
                    o_d = BitConverter.GetBytes(spl3_PriorList[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[320 + 112 * x2 + 88 + a8] = o_d[a8];
                    }
                }
                Spl4NamePointer.Add(file.Count);
                nameLength5 = spl4_NameList[x2].Length;
                if (spl4_NameList[x2] != "") {
                    for (int a17 = 0; a17 < nameLength5; a17++) {
                        file.Add((byte)spl4_NameList[x2][a17]);
                    }
                    for (int a16 = nameLength5; a16 < nameLength5 + 1; a16++) {
                        file.Add(0);
                    }
                    o_d = BitConverter.GetBytes(spl4_PriorList[x2]);
                    for (int a8 = 0; a8 < 4; a8++) {
                        file[320 + 112 * x2 + 104 + a8] = o_d[a8];
                    }
                }
                int newPointer3 = Spl1NamePointer[x2] - 320 - 112 * x2 - 48;
                byte[] ptrBytes3 = BitConverter.GetBytes(newPointer3);
                if (spl1_NameList[x2] != "") {
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[320 + 112 * x2 + 48 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Spl2NamePointer[x2] - 320 - 112 * x2 - 64;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);
                if (spl2_NameList[x2] != "") {
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[320 + 112 * x2 + 64 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Spl3NamePointer[x2] - 320 - 112 * x2 - 80;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);
                if (spl3_NameList[x2] != "") {
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[320 + 112 * x2 + 80 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = Spl4NamePointer[x2] - 320 - 112 * x2 - 96;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);
                if (spl4_NameList[x2] != "") {
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[320 + 112 * x2 + 96 + a7] = ptrBytes3[a7];
                    }
                }
                // VALUES
                byte[] o_a = CharacodeList[x2];
                for (int a8 = 0; a8 < 4; a8++) {
                    file[320 + 112 * x2 + a8] = o_a[a8];
                }
                byte[] o_b = spl1_chUsageCountValueList[x2];
                for (int a8 = 0; a8 < 4; a8++) {
                    file[320 + 112 * x2 + 4 + a8] = o_b[a8];
                }
                byte[] o_c = spl2_chUsageCountValueList[x2];
                for (int a8 = 0; a8 < 4; a8++) {
                    file[320 + 112 * x2 + 8 + a8] = o_c[a8];
                }

                o_d = spl3_chUsageCountValueList[x2];
                for (int a8 = 0; a8 < 4; a8++) {
                    file[320 + 112 * x2 + 12 + a8] = o_d[a8];
                }
                o_d = spl4_chUsageCountValueList[x2];
                for (int a8 = 0; a8 < 4; a8++) {
                    file[320 + 112 * x2 + 16 + a8] = o_d[a8];
                }
                byte[] o_f = WeirdValuesList[x2];
                for (int a8 = 0; a8 < o_f.Length; a8++) {
                    file[320 + 112 * x2 + 20 + a8] = o_f[a8];
                }
            }
            int FileSize3 = file.Count - 304;
            byte[] sizeBytes3 = BitConverter.GetBytes(FileSize3);
            byte[] sizeBytes2 = BitConverter.GetBytes(FileSize3 + 4);
            for (int a20 = 0; a20 < 4; a20++) {
                file[300 + a20] = sizeBytes3[3 - a20];
            }
            for (int a19 = 0; a19 < 4; a19++) {
                file[288 + a19] = sizeBytes2[3 - a19];
            }
            byte[] countBytes = BitConverter.GetBytes(EntryCount);
            for (int a18 = 0; a18 < 4; a18++) {
                file[308 + a18] = countBytes[a18];
            }
            byte[] finalBytes = new byte[20]
            {
                0,
                0,
                0,
                8,
                0,
                0,
                0,
                2,
                0,
                121,
                24,
                0,
                0,
                0,
                0,
                4,
                0,
                0,
                0,
                0
            };
            for (int x = 0; x < finalBytes.Length; x++) {
                file.Add(finalBytes[x]);
            }
            return file.ToArray();
        }
    }
}
