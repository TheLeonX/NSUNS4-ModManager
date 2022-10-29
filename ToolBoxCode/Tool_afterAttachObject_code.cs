using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_afterAttachObject_code {
        public bool FileOpen = false;
        public string FilePath = "";
        public byte[] FileBytes = new byte[0];
        public int EntryCount = 0;
        public List<string> characode1List = new List<string>();
        public List<string> characode2List = new List<string>();
        public List<string> bone1List = new List<string>();
        public List<string> pathList = new List<string>();
        public List<string> meshList = new List<string>();
        public List<string> bone2List = new List<string>();
        public List<float> XPosList = new List<float>();
        public List<float> YPosList = new List<float>();
        public List<float> ZPosList = new List<float>();
        public List<float> XRotList = new List<float>();
        public List<float> YRotList = new List<float>();
        public List<float> ZRotList = new List<float>();
        public List<float> XScaleList = new List<float>();
        public List<float> YScaleList = new List<float>();
        public List<float> ZScaleList = new List<float>();
        public List<int> value1List = new List<int>();
        public List<int> value2List = new List<int>();
        public List<int> value3List = new List<int>();
        public void OpenFile(string basepath = "") {
            OpenFileDialog o = new OpenFileDialog();
            {
                o.DefaultExt = ".xfbin";
                o.Filter = "*.xfbin|*.xfbin";
            }
            if (basepath != "") {
                o.FileName = basepath;
            } else o.ShowDialog();
            if (!(o.FileName != "") || !File.Exists(o.FileName)) {
                return;
            }
            ClearFile();
            FileOpen = true;
            FilePath = o.FileName;
            FileBytes = File.ReadAllBytes(FilePath);
            EntryCount = FileBytes[0x12C] + FileBytes[0x12D] * 256 + FileBytes[0x12E] * 65536 + FileBytes[0x12F] * 16777216;

            for (int x2 = 0; x2 < EntryCount; x2++) {
                long _ptr = 0x138 + 0x68 * x2;
                string Characode1 = "";
                long _ptrCharacter3 = FileBytes[_ptr] + FileBytes[_ptr + 1] * 0x100 + FileBytes[_ptr + 2] * 0x10000 + FileBytes[_ptr + 3] * 0x1000000;
                for (int a = 0; a < 8; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + a] != 0) {
                        string str3 = Characode1;
                        char c = (char)FileBytes[_ptr + _ptrCharacter3 + a];
                        Characode1 = str3 + c;
                    } else {
                        a = 8;
                    }
                }
                string Characode2 = "";
                _ptrCharacter3 = FileBytes[_ptr + 8] + FileBytes[_ptr + 9] * 0x100 + FileBytes[_ptr + 10] * 0x10000 + FileBytes[_ptr + 11] * 0x1000000;
                for (int a = 0; a < 8; a++) {
                    if (FileBytes[_ptr + 8 + _ptrCharacter3 + a] != 0) {
                        string str3 = Characode2;
                        char c = (char)FileBytes[_ptr + 8 + _ptrCharacter3 + a];
                        Characode2 = str3 + c;
                    } else {
                        a = 8;
                    }
                }
                string bone1 = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x10] + FileBytes[_ptr + 0x11] * 0x100 + FileBytes[_ptr + 0x12] * 0x10000 + FileBytes[_ptr + 0x13] * 0x1000000;
                for (int a = 0; a < 40; a++) {
                    if (FileBytes[_ptr + 0x10 + _ptrCharacter3 + a] != 0) {
                        string str3 = bone1;
                        char c = (char)FileBytes[_ptr + 0x10 + _ptrCharacter3 + a];
                        bone1 = str3 + c;
                    } else {
                        a = 40;
                    }
                }
                string path = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x18] + FileBytes[_ptr + 0x19] * 0x100 + FileBytes[_ptr + 0x1A] * 0x10000 + FileBytes[_ptr + 0x1B] * 0x1000000;
                for (int a = 0; a < 40; a++) {
                    if (FileBytes[_ptr + _ptrCharacter3 + 0x18 + a] != 0) {
                        string str3 = path;
                        char c = (char)FileBytes[_ptr + 0x18 + _ptrCharacter3 + a];
                        path = str3 + c;
                    } else {
                        a = 40;
                    }
                }
                string mesh = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x28] + FileBytes[_ptr + 0x29] * 0x100 + FileBytes[_ptr + 0x2A] * 0x10000 + FileBytes[_ptr + 0x2B] * 0x1000000;
                for (int a = 0; a < 40; a++) {
                    if (FileBytes[_ptr + 0x28 + _ptrCharacter3 + a] != 0) {
                        string str3 = mesh;
                        char c = (char)FileBytes[_ptr + 0x28 + _ptrCharacter3 + a];
                        mesh = str3 + c;
                    } else {
                        a = 40;
                    }
                }
                string bone2 = "";
                _ptrCharacter3 = FileBytes[_ptr + 0x30] + FileBytes[_ptr + 0x31] * 0x100 + FileBytes[_ptr + 0x32] * 0x10000 + FileBytes[_ptr + 0x33] * 0x1000000;
                for (int a = 0; a < 40; a++) {
                    if (FileBytes[_ptr + 0x30 + _ptrCharacter3 + a] != 0) {
                        string str3 = bone2;
                        char c = (char)FileBytes[_ptr + 0x30 + _ptrCharacter3 + a];
                        bone2 = str3 + c;
                    } else {
                        a = 40;
                    }
                }
                int value1 = FileBytes[(int)_ptr + 0x20];
                int value2 = FileBytes[(int)_ptr + 0x60];
                int value3 = FileBytes[(int)_ptr + 0x64];
                float XPos = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x38);
                float YPos = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x3C);
                float ZPos = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x40);
                float XRot = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x44);
                float YRot = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x48);
                float ZRot = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x4C);
                float XScale = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x50);
                float YScale = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x54);
                float ZScale = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x58);
                characode1List.Add(Characode1);
                characode2List.Add(Characode2);
                meshList.Add(mesh);
                pathList.Add(path);
                bone1List.Add(bone1);
                bone2List.Add(bone2);
                value1List.Add(value1);
                value2List.Add(value2);
                value3List.Add(value3);
                XPosList.Add(XPos);
                YPosList.Add(YPos);
                ZPosList.Add(ZPos);
                XRotList.Add(XRot);
                YRotList.Add(YRot);
                ZRotList.Add(ZRot);
                XScaleList.Add(XScale);
                YScaleList.Add(YScale);
                ZScaleList.Add(ZScale);

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
            characode1List = new List<string>();
            characode2List = new List<string>();
            bone1List = new List<string>();
            pathList = new List<string>();
            meshList = new List<string>();
            bone2List = new List<string>();
            XPosList = new List<float>();
            YPosList = new List<float>();
            ZPosList = new List<float>();
            XRotList = new List<float>();
            YRotList = new List<float>();
            ZRotList = new List<float>();
            value1List = new List<int>();
            value2List = new List<int>();
            value3List = new List<int>();
            XScaleList = new List<float>();
            YScaleList = new List<float>();
            ZScaleList = new List<float>();
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
            byte[] header = new byte[312]
            {
                0x4E,0x55,0x43,0x43,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xE4,0x00,0x00,0x00,0x03,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x3B,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x22,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x1F,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x30,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x4E,0x75,0x6C,0x6C,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x42,0x69,0x6E,0x61,0x72,0x79,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x50,0x61,0x67,0x65,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x49,0x6E,0x64,0x65,0x78,0x00,0x00,0x62,0x69,0x6E,0x5F,0x6C,0x65,0x2F,0x78,0x36,0x34,0x2F,0x61,0x66,0x74,0x65,0x72,0x41,0x74,0x74,0x61,0x63,0x68,0x4F,0x62,0x6A,0x65,0x63,0x74,0x2E,0x62,0x69,0x6E,0x00,0x00,0x61,0x66,0x74,0x65,0x72,0x41,0x74,0x74,0x61,0x63,0x68,0x4F,0x62,0x6A,0x65,0x63,0x74,0x00,0x50,0x61,0x67,0x65,0x30,0x00,0x69,0x6E,0x64,0x65,0x78,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x79,0x00,0x00,0x00,0x00,0x4A,0x14,0x00,0x00,0x00,0x01,0x00,0x79,0x00,0x00,0x00,0x00,0x4A,0x10,0xE9,0x03,0x00,0x00,0x6A,0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            };
            for (int x4 = 0; x4 < header.Length; x4++) {
                file.Add(header[x4]);
            }
            for (int x3 = 0; x3 < EntryCount * 0x68; x3++) {
                file.Add(0);
            }
            List<int> CharacodePointer = new List<int>();
            List<int> CostumePointer = new List<int>();
            List<int> pathPointer = new List<int>();
            List<int> meshPointer = new List<int>();
            List<int> bone1Pointer = new List<int>();
            List<int> bone2Pointer = new List<int>();

            for (int x2 = 0; x2 < EntryCount; x2++) {
                CharacodePointer.Add(file.Count);
                int nameLength3 = characode1List[x2].Length;
                if (characode1List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)characode1List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < 8; a16++) {
                        file.Add(0);
                    }
                }
                bone1Pointer.Add(file.Count);
                nameLength3 = bone1List[x2].Length;
                if (meshList[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int c15 = 0; c15 < nameLength3; c15++) {
                        file.Add((byte)bone1List[x2][c15]);
                    }
                    for (int c14 = nameLength3; c14 < nameLength3 + 4; c14++) {
                        file.Add(0);
                    }
                }
                CostumePointer.Add(file.Count);
                nameLength3 = characode2List[x2].Length;
                if (characode2List[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add((byte)characode2List[x2][a17]);
                    }
                    for (int a16 = nameLength3; a16 < 8; a16++) {
                        file.Add(0);
                    }
                }

                pathPointer.Add(file.Count);
                nameLength3 = pathList[x2].Length;
                if (pathList[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int b15 = 0; b15 < nameLength3; b15++) {
                        file.Add((byte)pathList[x2][b15]);
                    }
                    for (int b14 = nameLength3; b14 < pathList[x2].Length + 4; b14++) {
                        file.Add(0);
                    }
                }

                meshPointer.Add(file.Count);
                nameLength3 = meshList[x2].Length;
                if (meshList[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int c15 = 0; c15 < nameLength3; c15++) {
                        file.Add((byte)meshList[x2][c15]);
                    }
                    for (int c14 = nameLength3; c14 < meshList[x2].Length + 4; c14++) {
                        file.Add(0);
                    }
                }

                bone2Pointer.Add(file.Count);
                nameLength3 = bone2List[x2].Length;
                if (meshList[x2] == "") {
                    nameLength3 = 0;
                } else {
                    for (int c15 = 0; c15 < nameLength3; c15++) {
                        file.Add((byte)bone2List[x2][c15]);
                    }
                    for (int c14 = nameLength3; c14 < bone2List[x2].Length + 4; c14++) {
                        file.Add(0);
                    }
                }
                int newPointer3 = CharacodePointer[x2] - 312 - 0x68 * x2 - 0;
                byte[] ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (characode1List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[312 + 0x68 * x2 + 0 + a7] = 0;
                    }
                } else {
                    newPointer3 = CharacodePointer[x2] - 312 - 0x68 * x2 - 0;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[312 + 0x68 * x2 + 0 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = CostumePointer[x2] - 312 - 0x68 * x2 - 0x8;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (characode2List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[312 + 0x68 * x2 + 0x8 + a7] = 0;
                    }
                } else {
                    newPointer3 = CostumePointer[x2] - 312 - 0x68 * x2 - 0x8;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[312 + 0x68 * x2 + 0x8 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = bone1Pointer[x2] - 312 - 0x68 * x2 - 0x10;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (bone1List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[312 + 0x68 * x2 + 0x10 + a7] = 0;
                    }
                } else {
                    newPointer3 = bone1Pointer[x2] - 312 - 0x68 * x2 - 0x10;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[312 + 0x68 * x2 + 0x10 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = pathPointer[x2] - 312 - 0x68 * x2 - 0x18;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (pathList[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[312 + 0x68 * x2 + 0x18 + a7] = 0;
                    }
                } else {
                    newPointer3 = pathPointer[x2] - 312 - 0x68 * x2 - 0x18;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[312 + 0x68 * x2 + 0x18 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = pathPointer[x2] - 312 - 0x68 * x2 - 0x18;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (pathList[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[312 + 0x68 * x2 + 0x18 + a7] = 0;
                    }
                } else {
                    newPointer3 = pathPointer[x2] - 312 - 0x68 * x2 - 0x18;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[312 + 0x68 * x2 + 0x18 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = meshPointer[x2] - 312 - 0x68 * x2 - 0x28;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (meshList[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[312 + 0x68 * x2 + 0x28 + a7] = 0;
                    }
                } else {
                    newPointer3 = meshPointer[x2] - 312 - 0x68 * x2 - 0x28;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[312 + 0x68 * x2 + 0x28 + a7] = ptrBytes3[a7];
                    }
                }
                newPointer3 = bone2Pointer[x2] - 312 - 0x68 * x2 - 0x30;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);

                if (bone2List[x2] == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[312 + 0x68 * x2 + 0x30 + a7] = 0;
                    }
                } else {
                    newPointer3 = bone2Pointer[x2] - 312 - 0x68 * x2 - 0x30;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[312 + 0x68 * x2 + 0x30 + a7] = ptrBytes3[a7];
                    }
                }
                // VALUES
                byte[] value1_byte = new byte[4]
                {
                    Convert.ToByte(value1List[x2]),
                    0x00,
                    0x00,
                    0x00
                };
                for (int a8 = 0; a8 < 4; a8++) {
                    file[312 + 0x68 * x2 + 0x20 + a8] = value1_byte[a8];
                }
                byte[] XPos_byte = BitConverter.GetBytes(XPosList[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[312 + 0x68 * x2 + 0x38 + a8] = XPos_byte[a8];
                }
                byte[] YPos_byte = BitConverter.GetBytes(YPosList[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[312 + 0x68 * x2 + 0x3C + a8] = YPos_byte[a8];
                }
                byte[] ZPos_byte = BitConverter.GetBytes(ZPosList[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[312 + 0x68 * x2 + 0x40 + a8] = ZPos_byte[a8];
                }
                byte[] XRot_byte = BitConverter.GetBytes(XRotList[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[312 + 0x68 * x2 + 0x44 + a8] = XRot_byte[a8];
                }
                byte[] YRot_byte = BitConverter.GetBytes(YRotList[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[312 + 0x68 * x2 + 0x48 + a8] = YRot_byte[a8];
                }
                byte[] ZRot_byte = BitConverter.GetBytes(ZRotList[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[312 + 0x68 * x2 + 0x4C + a8] = ZRot_byte[a8];
                }
                byte[] XScale_byte = BitConverter.GetBytes(XScaleList[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[312 + 0x68 * x2 + 0x50 + a8] = XScale_byte[a8];
                }
                byte[] YScale_byte = BitConverter.GetBytes(YScaleList[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[312 + 0x68 * x2 + 0x54 + a8] = YScale_byte[a8];
                }
                byte[] ZScale_byte = BitConverter.GetBytes(ZScaleList[x2]);

                for (int a8 = 0; a8 < 4; a8++) {
                    file[312 + 0x68 * x2 + 0x58 + a8] = ZScale_byte[a8];
                }
                byte[] value2_byte = new byte[4]
                {
                    Convert.ToByte(value2List[x2]),
                    0x00,
                    0x00,
                    0x00
                };
                for (int a6 = 0; a6 < 4; a6++) {
                    file[312 + 0x68 * x2 + 0x60 + a6] = value2_byte[a6];
                }
                byte[] value3_byte = new byte[4]
                {
                    Convert.ToByte(value3List[x2]),
                    0x00,
                    0x00,
                    0x00
                };
                for (int a6 = 0; a6 < 4; a6++) {
                    file[312 + 0x68 * x2 + 0x64 + a6] = value3_byte[a6];
                }
            }
            int FileSize3 = file.Count - 296;
            byte[] sizeBytes3 = BitConverter.GetBytes(FileSize3);
            int FileSize2 = file.Count - 296 + 4;
            byte[] sizeBytes2 = BitConverter.GetBytes(FileSize3 + 4);
            for (int a20 = 0; a20 < 4; a20++) {
                file[292 + a20] = sizeBytes3[3 - a20];
            }
            for (int a19 = 0; a19 < 4; a19++) {
                file[280 + a19] = sizeBytes2[3 - a19];
            }
            byte[] countBytes = BitConverter.GetBytes(EntryCount);
            for (int a18 = 0; a18 < 4; a18++) {
                file[300 + a18] = countBytes[a18];
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
