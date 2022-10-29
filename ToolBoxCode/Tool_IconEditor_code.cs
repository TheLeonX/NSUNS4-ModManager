using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_IconEditor_code {
        public bool FileOpen = false;
        public string FilePath = "";
        public byte[] fileBytes = new byte[0];
        public int EntryCount = 0;


        public List<byte[]> CharacodeList = new List<byte[]>();
        public List<byte[]> CostumeList = new List<byte[]>();
        public List<string> NameList = new List<string>();
        public List<string> ExNinjutsuList = new List<string>();
        public List<string> IconList = new List<string>();
        public List<string> AwaIconList = new List<string>();
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
            EntryCount = FileBytes[288] + FileBytes[289] * 256 + FileBytes[290] * 65536 + FileBytes[291] * 16777216;

            for (int x2 = 0; x2 < EntryCount; x2++) {
                long _ptr = 300 + 40 * x2;
                byte[] Characode = new byte[4]
                {
                    FileBytes[_ptr],
                    FileBytes[_ptr + 1],
                    0,
                    0
                };
                byte[] Costume = new byte[4]
                {
                    FileBytes[_ptr + 4],
                    FileBytes[_ptr + 5],
                    FileBytes[_ptr + 6],
                    FileBytes[_ptr + 7]
                };
                string Icon = "";
                long _ptrIcon3 = FileBytes[_ptr + 8] + FileBytes[_ptr + 9] * 256 + FileBytes[_ptr + 10] * 65536 + FileBytes[_ptr + 11] * 16777216;
                for (int a2 = 0; a2 < 8; a2++) {
                    if (FileBytes[_ptr + 8 + _ptrIcon3 + a2] != 0) {
                        string str2 = Icon;
                        char c = (char)FileBytes[_ptr + 8 + _ptrIcon3 + a2];
                        Icon = str2 + c;
                    } else {
                        a2 = 8;
                    }
                }
                string AwaIcon = "";
                long _ptrAwaIcon3 = FileBytes[_ptr + 16] + FileBytes[_ptr + 17] * 256 + FileBytes[_ptr + 18] * 65536 + FileBytes[_ptr + 19] * 16777216;
                for (int a1 = 0; a1 < 16; a1++) {
                    if (FileBytes[_ptr + 16 + _ptrAwaIcon3 + a1] != 0) {
                        string str1 = AwaIcon;
                        char c = (char)FileBytes[_ptr + 16 + _ptrAwaIcon3 + a1];
                        AwaIcon = str1 + c;
                    } else {
                        a1 = 16;
                    }
                }
                string Name = "";
                long _ptrName3 = FileBytes[_ptr + 24] + FileBytes[_ptr + 25] * 256 + FileBytes[_ptr + 26] * 65536 + FileBytes[_ptr + 27] * 16777216;
                for (int a3 = 0; a3 < 8; a3++) {
                    if (FileBytes[_ptr + 24 + _ptrName3 + a3] != 0) {
                        string str3 = Name;
                        char c = (char)FileBytes[_ptr + 24 + _ptrName3 + a3];
                        Name = str3 + c;
                    } else {
                        a3 = 8;
                    }
                }
                string ExNinjutsu = "";
                long _ptrExNinjutsu3 = FileBytes[_ptr + 32] + FileBytes[_ptr + 33] * 256 + FileBytes[_ptr + 34] * 65536 + FileBytes[_ptr + 35] * 16777216;
                for (int a4 = 0; a4 < 8; a4++) {
                    if (FileBytes[_ptr + 32 + _ptrExNinjutsu3 + a4] != 0) {
                        string str4 = ExNinjutsu;
                        char c = (char)FileBytes[_ptr + 32 + _ptrExNinjutsu3 + a4];
                        ExNinjutsu = str4 + c;
                    } else {
                        a4 = 8;
                    }
                }



                CharacodeList.Add(Characode);
                CostumeList.Add(Costume);
                IconList.Add(Icon);
                AwaIconList.Add(AwaIcon);
                NameList.Add(Name);
                ExNinjutsuList.Add(ExNinjutsu);

            }
        }
        public void ClearFile() {
            CharacodeList = new List<byte[]>();
            CostumeList = new List<byte[]>();
            NameList = new List<string>();
            ExNinjutsuList = new List<string>();
            IconList = new List<string>();
            AwaIconList = new List<string>();

            EntryCount = 0;
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
            byte[] header = new byte[300]
            {
                0x4E,0x55,0x43,0x43,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xD8,0x00,0x00,0x00,0x03,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x3B,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x1C,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x19,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x30,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x4E,0x75,0x6C,0x6C,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x42,0x69,0x6E,0x61,0x72,0x79,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x50,0x61,0x67,0x65,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x49,0x6E,0x64,0x65,0x78,0x00,0x00,0x62,0x69,0x6E,0x5F,0x6C,0x65,0x2F,0x78,0x36,0x34,0x2F,0x70,0x6C,0x61,0x79,0x65,0x72,0x5F,0x69,0x63,0x6F,0x6E,0x2E,0x62,0x69,0x6E,0x00,0x00,0x70,0x6C,0x61,0x79,0x65,0x72,0x5F,0x69,0x63,0x6F,0x6E,0x00,0x50,0x61,0x67,0x65,0x30,0x00,0x69,0x6E,0x64,0x65,0x78,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x6B,0x64,0x00,0x00,0x00,0x01,0x00,0x79,0x00,0x00,0x00,0x00,0x6B,0x60,0xE9,0x03,0x00,0x00,0x9B,0x01,0x00,0x00,0x08,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            };
            for (int x4 = 0; x4 < header.Length; x4++) {
                file.Add(header[x4]);
            }
            for (int x3 = 0; x3 < EntryCount * 40; x3++) {
                file.Add(0);
            }
            List<int> IconPointer = new List<int>();
            List<int> AwaIconPointer = new List<int>();
            List<int> NamePointer = new List<int>();
            List<int> ExNinjutsuPointer = new List<int>();

            for (int x2 = 0; x2 < EntryCount; x2++) {
                IconPointer.Add(file.Count);
                int nameLength3 = IconList[x2].Length;
                if (IconList[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)IconList[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < 8; a16++) {
                        file.Add(0);
                    }
                }

                AwaIconPointer.Add(file.Count);
                nameLength3 = AwaIconList[x2].Length;
                if (AwaIconList[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)AwaIconList[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < 16; a16++) {
                        file.Add(0);
                    }
                }

                NamePointer.Add(file.Count);
                nameLength3 = NameList[x2].Length;
                if (NameList[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int b15 = 0; b15 < nameLength3; b15++) {
                        file.Add((byte)NameList[x2][b15]);
                    }
                    for (int b14 = nameLength3; b14 < 8; b14++) {
                        file.Add(0);
                    }
                }

                ExNinjutsuPointer.Add(file.Count);
                nameLength3 = ExNinjutsuList[x2].Length;
                if (ExNinjutsuList[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int c15 = 0; c15 < nameLength3; c15++) {
                        file.Add((byte)ExNinjutsuList[x2][c15]);
                    }
                    for (int c14 = nameLength3; c14 < 8; c14++) {
                        file.Add(0);
                    }
                }
                int newPointer3 = IconPointer[x2] - 300 - 40 * x2 - 8;
                byte[] ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (IconList[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[300 + 40 * x2 + 8 + a7] = 0;
                    }
                } else {
                    newPointer3 = IconPointer[x2] - 300 - 40 * x2 - 8;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[300 + 40 * x2 + 8 + a7] = ptrBytes3[a7];
                    }
                }
                if (AwaIconList[x2] == "") {
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[300 + 40 * x2 + 16 + a7] = 0;
                    }
                } else {
                    newPointer3 = AwaIconPointer[x2] - 300 - 40 * x2 - 16;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[300 + 40 * x2 + 16 + a7] = ptrBytes3[a7];
                    }
                }
                if (NameList[x2] == "") {
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[300 + 40 * x2 + 24 + a7] = 0;
                    }
                } else {
                    newPointer3 = NamePointer[x2] - 300 - 40 * x2 - 24;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[300 + 40 * x2 + 24 + a7] = ptrBytes3[a7];
                    }
                }
                if (ExNinjutsuList[x2] == "") {

                    newPointer3 = ExNinjutsuPointer[x2] - 300 - 40 * x2 - 32;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[300 + 40 * x2 + 32 + a7] = 0;
                    }
                } else {
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[300 + 40 * x2 + 32 + a7] = ptrBytes3[a7];
                    }
                }

                // VALUES
                byte[] o_a = CharacodeList[x2];
                for (int a8 = 0; a8 < 4; a8++) {
                    file[300 + 40 * x2 + a8] = o_a[a8];
                }
                byte[] o_b = CostumeList[x2];
                for (int a6 = 0; a6 < 4; a6++) {
                    file[300 + 40 * x2 + 4 + a6] = o_b[a6];
                }
            }
            int FileSize3 = file.Count - 284;
            byte[] sizeBytes3 = BitConverter.GetBytes(FileSize3);
            int FileSize2 = file.Count - 268 + 4;
            byte[] sizeBytes2 = BitConverter.GetBytes(FileSize3 + 4);
            for (int a20 = 0; a20 < 4; a20++) {
                file[280 + a20] = sizeBytes3[3 - a20];
            }
            for (int a19 = 0; a19 < 4; a19++) {
                file[268 + a19] = sizeBytes2[3 - a19];
            }
            byte[] countBytes = BitConverter.GetBytes(EntryCount);
            for (int a18 = 0; a18 < 4; a18++) {
                file[288 + a18] = countBytes[a18];
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
