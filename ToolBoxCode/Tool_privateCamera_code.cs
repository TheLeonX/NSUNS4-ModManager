using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSUNS4_ModManager.ToolBoxCode {
    internal class Tool_privateCamera_code {
        public bool FileOpen = false;
        public string FilePath = "";
        public byte[] fileBytes = new byte[0];
        public int EntryCount = 0;
        public List<privateCamera> privateCameraParam = new List<privateCamera>();

        [Serializable]
        public class privateCamera {
            public float unk1 = -1;
            public float unk2 = -1;
            public float unk3 = -1;
            public float unk4 = -1;
            public float unk5 = -1;
            public float unk6 = -1;
            public float unk7 = -1;
            public float unk8 = -1;
            public float unk9 = -1;
            public float unk10 = -1;
            public float unk11 = -1;
        }
        public void ClearFile() {
            privateCameraParam = new List<privateCamera>();
            EntryCount = 0;
            FileOpen = false;
            FilePath = "";
            fileBytes = new byte[0];

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
            fileBytes = File.ReadAllBytes(o.FileName);

            EntryCount = MainFunctions.b_ReadInt(fileBytes, 0x140);
            for (int x = 0; x < EntryCount; x++) {
                int _ptr = 0x144 + 0x2C * x;
                privateCamera privateCameraEntry = new privateCamera();
                privateCameraEntry.unk1 = MainFunctions.b_ReadFloat(fileBytes, _ptr);
                privateCameraEntry.unk2 = MainFunctions.b_ReadFloat(fileBytes, _ptr + 0x04);
                privateCameraEntry.unk3 = MainFunctions.b_ReadFloat(fileBytes, _ptr + 0x08);
                privateCameraEntry.unk4 = MainFunctions.b_ReadFloat(fileBytes, _ptr + 0x0C);
                privateCameraEntry.unk5 = MainFunctions.b_ReadFloat(fileBytes, _ptr + 0x10);
                privateCameraEntry.unk6 = MainFunctions.b_ReadFloat(fileBytes, _ptr + 0x14);
                privateCameraEntry.unk7 = MainFunctions.b_ReadFloat(fileBytes, _ptr + 0x18);
                privateCameraEntry.unk8 = MainFunctions.b_ReadFloat(fileBytes, _ptr + 0x1C);
                privateCameraEntry.unk9 = MainFunctions.b_ReadFloat(fileBytes, _ptr + 0x20);
                privateCameraEntry.unk10 = MainFunctions.b_ReadFloat(fileBytes, _ptr + 0x24);
                privateCameraEntry.unk11 = MainFunctions.b_ReadFloat(fileBytes, _ptr + 0x28);
                privateCameraParam.Add(privateCameraEntry);
            }
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
            byte[] header = new byte[0x144]
            {
                0x4E,0x55,0x43,0x43,0x00,0x00,0x00,0x63,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x08,0x00,0x00,0x00,0x03,0x00,0x63,0x40,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x3B,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x48,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x1B,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x30,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x4E,0x75,0x6C,0x6C,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x42,0x69,0x6E,0x61,0x72,0x79,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x50,0x61,0x67,0x65,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x49,0x6E,0x64,0x65,0x78,0x00,0x00,0x44,0x3A,0x2F,0x75,0x73,0x65,0x72,0x2F,0x6E,0x61,0x72,0x75,0x74,0x6F,0x4E,0x65,0x78,0x74,0x34,0x5F,0x74,0x72,0x75,0x6E,0x6B,0x2F,0x70,0x61,0x72,0x61,0x6D,0x2F,0x70,0x6C,0x61,0x79,0x65,0x72,0x2F,0x43,0x6F,0x6E,0x76,0x65,0x72,0x74,0x65,0x72,0x2F,0x62,0x69,0x6E,0x2F,0x70,0x72,0x69,0x76,0x61,0x74,0x65,0x43,0x61,0x6D,0x65,0x72,0x61,0x2E,0x62,0x69,0x6E,0x00,0x00,0x70,0x72,0x69,0x76,0x61,0x74,0x65,0x43,0x61,0x6D,0x65,0x72,0x61,0x00,0x50,0x61,0x67,0x65,0x30,0x00,0x69,0x6E,0x64,0x65,0x78,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x63,0x42,0x00,0x00,0x00,0x27,0x90,0x00,0x00,0x00,0x01,0x00,0x63,0x42,0x00,0x00,0x00,0x27,0x8C,0xE6,0x00,0x00,0x00
            };

            file = MainFunctions.b_AddBytes(file, header);
            file = MainFunctions.b_AddBytes(file, new byte[EntryCount * 0x2C]);


            for (int x2 = 0; x2 < EntryCount; x2++) {

                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(privateCameraParam[x2].unk1), 0x144 + 0x2C * x2);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(privateCameraParam[x2].unk2), 0x144 + 0x2C * x2 + 0x4);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(privateCameraParam[x2].unk3), 0x144 + 0x2C * x2 + 0x8);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(privateCameraParam[x2].unk4), 0x144 + 0x2C * x2 + 0xC);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(privateCameraParam[x2].unk5), 0x144 + 0x2C * x2 + 0x10);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(privateCameraParam[x2].unk6), 0x144 + 0x2C * x2 + 0x14);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(privateCameraParam[x2].unk7), 0x144 + 0x2C * x2 + 0x18);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(privateCameraParam[x2].unk8), 0x144 + 0x2C * x2 + 0x1C);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(privateCameraParam[x2].unk9), 0x144 + 0x2C * x2 + 0x20);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(privateCameraParam[x2].unk10), 0x144 + 0x2C * x2 + 0x24);
                file = MainFunctions.b_ReplaceBytes(file, BitConverter.GetBytes(privateCameraParam[x2].unk11), 0x144 + 0x2C * x2 + 0x28);


            }
            int FileSize = file.Length - 0x140;
            byte[] sizeBytes1 = BitConverter.GetBytes(FileSize);
            byte[] sizeBytes2 = BitConverter.GetBytes(FileSize + 4);
            byte[] countBytes = BitConverter.GetBytes(EntryCount);


            file = MainFunctions.b_ReplaceBytes(file, sizeBytes1, 0x13C, 1);
            file = MainFunctions.b_ReplaceBytes(file, sizeBytes2, 0x130, 1);
            file = MainFunctions.b_ReplaceBytes(file, countBytes, 0x140);

            byte[] finalBytes = new byte[20]
            {
                0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x02,0x00,0x63,0x19,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00
            };


            file = MainFunctions.b_AddBytes(file, finalBytes);

            return file;
        }
    }
}
