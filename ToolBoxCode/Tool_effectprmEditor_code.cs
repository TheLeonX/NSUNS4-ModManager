using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_effectprmEditor_code {
        public bool FileOpen = false;
        public string FilePath = "";
        public byte[] fileBytes = new byte[0];
        public int EntryCount = 0;

        public List<int> EffectPrmID_List = new List<int>();
        public List<int> EffectPrmType_List = new List<int>();
        public List<string> EffectPrmPath_List = new List<string>();
        public List<string> EffectPrmAnm_List = new List<string>();
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
            EntryCount = MainFunctions.b_ReadInt(FileBytes, 0x138);
            for (int x2 = 0; x2 < EntryCount; x2++) {
                long _ptr = 0x13C + 0x88 * x2;

                int EffectId = MainFunctions.b_ReadInt(FileBytes, (int)_ptr);
                int EffectType = MainFunctions.b_ReadInt(FileBytes, (int)_ptr + 0x04);
                string EffectPath = MainFunctions.b_ReadString(FileBytes, (int)_ptr + 0x08);
                string EffectAnm = MainFunctions.b_ReadString(FileBytes, (int)_ptr + 0x48);


                EffectPrmID_List.Add(EffectId);
                EffectPrmType_List.Add(EffectType);
                EffectPrmPath_List.Add(EffectPath);
                EffectPrmAnm_List.Add(EffectAnm);

            }
        }
        public void ClearFile() {

            EntryCount = 0;
            EffectPrmID_List = new List<int>();
            EffectPrmType_List = new List<int>();
            EffectPrmPath_List = new List<string>();
            EffectPrmAnm_List = new List<string>();
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
            byte[] header = new byte[316]
            {
               0x4E,0x55,0x43,0x43,0x00,0x00,0x00,0x63,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x00,0x03,0x00,0x63,0x40,0x00,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x3B,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x44,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x17,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x30,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x4E,0x75,0x6C,0x6C,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x42,0x69,0x6E,0x61,0x72,0x79,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x50,0x61,0x67,0x65,0x00,0x6E,0x75,0x63,0x63,0x43,0x68,0x75,0x6E,0x6B,0x49,0x6E,0x64,0x65,0x78,0x00,0x00,0x44,0x3A,0x2F,0x75,0x73,0x65,0x72,0x2F,0x6E,0x61,0x72,0x75,0x74,0x6F,0x4E,0x65,0x78,0x74,0x34,0x5F,0x74,0x72,0x75,0x6E,0x6B,0x2F,0x70,0x61,0x72,0x61,0x6D,0x2F,0x70,0x6C,0x61,0x79,0x65,0x72,0x2F,0x43,0x6F,0x6E,0x76,0x65,0x72,0x74,0x65,0x72,0x2F,0x62,0x69,0x6E,0x2F,0x65,0x66,0x66,0x65,0x63,0x74,0x70,0x72,0x6D,0x2E,0x62,0x69,0x6E,0x00,0x00,0x65,0x66,0x66,0x65,0x63,0x74,0x70,0x72,0x6D,0x00,0x50,0x61,0x67,0x65,0x30,0x00,0x69,0x6E,0x64,0x65,0x78,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x02,0x00,0x00,0x00,0x03,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x63,0x42,0x00,0x00,0x00,0xF1,0x38,0x00,0x00,0x00,0x01,0x00,0x63,0x42,0x00,0x00,0x00,0xF1,0x34,0xC6,0x01,0x00,0x00
            };
            for (int x4 = 0; x4 < header.Length; x4++) {
                file.Add(header[x4]);
            }
            for (int x3 = 0; x3 < EntryCount * 0x88; x3++) {
                file.Add(0);
            }
            int Section_size = 4;
            for (int x2 = 0; x2 < EntryCount; x2++) {
                Section_size += 0x88;
                int _ptr = 0x13C + 0x88 * x2;
                byte[] o_a = BitConverter.GetBytes(EffectPrmID_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(EffectPrmType_List[x2]);
                for (int a8 = 0; a8 < 4; a8++) {
                    file[_ptr + a8 + 0x04] = o_a[a8];
                }
                for (int a8 = 0; a8 < EffectPrmPath_List[x2].Length; a8++) {
                    file[_ptr + a8 + 0x08] = (byte)EffectPrmPath_List[x2][a8];
                }
                for (int a8 = 0; a8 < EffectPrmAnm_List[x2].Length; a8++) {
                    file[_ptr + a8 + 0x48] = (byte)EffectPrmAnm_List[x2][a8];
                }

            }


            byte[] TUJ_sizeBytes1 = BitConverter.GetBytes(Section_size);
            byte[] TUJ_sizeBytes2 = BitConverter.GetBytes(Section_size + 4);
            for (int a20 = 0; a20 < 4; a20++) {
                file[0x128 + a20] = TUJ_sizeBytes2[3 - a20];
            }
            for (int a19 = 0; a19 < 4; a19++) {
                file[0x134 + a19] = TUJ_sizeBytes1[3 - a19];
            }
            byte[] countBytes = BitConverter.GetBytes(EntryCount);
            for (int a18 = 0; a18 < 4; a18++) {
                file[0x138 + a18] = countBytes[a18];
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
