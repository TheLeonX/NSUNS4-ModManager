using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_MessageInfoEditor_code {
        public List<string> FilePaths = new List<string>();
        public List<byte[]> FileBytesList = new List<byte[]>();
        public List<int> EntryCounts = new List<int>();
        public List<List<byte[]>> CRC32CodesList = new List<List<byte[]>>();
        public List<List<byte[]>> MainTextsList = new List<List<byte[]>>();
        public List<List<byte[]>> ExtraTextsList = new List<List<byte[]>>();
        public List<List<int>> ACBFilesList = new List<List<int>>();
        public List<List<int>> CueIDsList = new List<List<int>>();
        public List<List<int>> VoiceOnlysList = new List<List<int>>();
        public List<bool> OpenedFile = new List<bool>();
        public void OpenFilesStart(string basepath = "") {
            ClearFiles();
            FolderBrowserDialog f = new FolderBrowserDialog();
            if (basepath != "") {
                f.SelectedPath = basepath;
            } else {
                f.ShowDialog();
            }

            bool exist = false;
            for (int i = 0; i < Program.LANG.Length; i++) {
                string path = f.SelectedPath + "\\WIN64\\" + Program.LANG[i] + "\\messageInfo.bin.xfbin";
                if (File.Exists(path)) {
                    exist = true;
                    OpenedFile.Add(true);
                    FilePaths.Add(path);
                } else {
                    OpenedFile.Add(false);
                    FilePaths.Add("");
                }
            }
            if (!exist) {
                MessageBox.Show("No files were found.");
                OpenedFile.Clear();
                FilePaths.Clear();
                return;
            }

            OpenFiles(FilePaths);
            
        }

        public void OpenFiles(List<string> Paths) {
            for (int i = 0; i < Paths.Count; i++) {
                OpenFile(Paths[i]);
            }
        }
        public void OpenFile(string basepath) {

            List<byte[]> CRC32Codes = new List<byte[]>();
            List<byte[]> MainTexts = new List<byte[]>();
            List<byte[]> ExtraTexts = new List<byte[]>();
            List<int> ACBFiles = new List<int>();
            List<int> CueIDs = new List<int>();
            List<int> VoiceOnlys = new List<int>();
            if (basepath != "") {
                byte[] FileBytes = File.ReadAllBytes(basepath);
                FileBytesList.Add(FileBytes);
                int EntryCount = FileBytes[288] + FileBytes[289] * 256 + FileBytes[290] * 65536 + FileBytes[291] * 16777216;
                EntryCounts.Add(EntryCount);
                for (int x2 = 0; x2 < EntryCount; x2++) {
                    long _ptr = 300 + 40 * x2;
                    byte[] CRC32Code = MainFunctions.b_ReadByteArray(FileBytes, (int)_ptr, 4);
                    long _ptrIcon3 = FileBytes[_ptr + 8] + FileBytes[_ptr + 9] * 256 + FileBytes[_ptr + 10] * 65536 + FileBytes[_ptr + 11] * 16777216;
                    byte[] ExtraText = MainFunctions.b_ReadByteArrayOfString(FileBytes, (int)(_ptr + 8 + _ptrIcon3));
                    _ptrIcon3 = FileBytes[_ptr + 16] + FileBytes[_ptr + 17] * 256 + FileBytes[_ptr + 18] * 65536 + FileBytes[_ptr + 19] * 16777216;
                    byte[] MainText = MainFunctions.b_ReadByteArrayOfString(FileBytes, (int)(_ptr + 16 + _ptrIcon3));
                    int ACBFile = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)(_ptr + 30));
                    int CueID = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)(_ptr + 32));
                    int VoiceOnly = MainFunctions.b_ReadIntFromTwoBytes(FileBytes, (int)(_ptr + 34));
                    CRC32Codes.Add(CRC32Code);
                    ExtraTexts.Add(ExtraText);
                    MainTexts.Add(MainText);
                    ACBFiles.Add(ACBFile);
                    CueIDs.Add(CueID);
                    VoiceOnlys.Add(VoiceOnly);
                }
                CRC32CodesList.Add(CRC32Codes);
                ExtraTextsList.Add(ExtraTexts);
                MainTextsList.Add(MainTexts);
                ACBFilesList.Add(ACBFiles);
                CueIDsList.Add(CueIDs);
                VoiceOnlysList.Add(VoiceOnlys);

            } else {
                List<byte[]> emptyList = new List<byte[]>();
                List<int> emptyIntList = new List<int>();
                EntryCounts.Add(0);
                FileBytesList.Add(new byte[0]);
                CRC32CodesList.Add(emptyList);
                ExtraTextsList.Add(emptyList);
                MainTextsList.Add(emptyList);
                ACBFilesList.Add(emptyIntList);
                CueIDsList.Add(emptyIntList);
                VoiceOnlysList.Add(emptyIntList);
            }

        }

        public void ClearFiles() {
            FilePaths = new List<string>();
            FileBytesList = new List<byte[]>();
            EntryCounts = new List<int>();
            CRC32CodesList = new List<List<byte[]>>();
            MainTextsList = new List<List<byte[]>>();
            ExtraTextsList = new List<List<byte[]>>();
            ACBFilesList = new List<List<int>>();
            CueIDsList = new List<List<int>>();
            VoiceOnlysList = new List<List<int>>();
            OpenedFile = new List<bool>();
        }
        public void SaveFile(int index, List<string> filePath) {
            if (filePath[index] != "") {
                if (File.Exists(filePath[index])) {
                    if (File.Exists(filePath[index] + ".backup")) {
                        File.Delete(filePath[index] + ".backup");
                    }
                    File.Copy(filePath[index], filePath[index] + ".backup");
                }
                File.WriteAllBytes(filePath[index], ConvertToFile(index));
            }
        }
        public void SaveFilesAs(string basepath = "") {
            bool foundFile = false;
            for (int i = 0; i < OpenedFile.Count; i++) {
                if (OpenedFile[i]) {
                    foundFile = true;
                }
            }
            if (!foundFile) {
                MessageBox.Show("No file loaded...");
            } else {
                FolderBrowserDialog s = new FolderBrowserDialog();
                if (basepath == "")
                    s.ShowDialog();
                else
                    s.SelectedPath = basepath;
                List<string> newPaths = new List<string>();
                for (int i = 0; i < OpenedFile.Count; i++) {
                    string path = "";
                    if (OpenedFile[i]) {
                        path = s.SelectedPath + "\\WIN64\\" + Program.LANG[i] + "\\messageInfo.bin.xfbin";
                        Directory.CreateDirectory(s.SelectedPath + "\\WIN64\\" + Program.LANG[i]);
                        newPaths.Add(path);
                        SaveFile(i, newPaths);
                    } else {
                        newPaths.Add("");
                    }
                }
                if (basepath == "")
                    MessageBox.Show("Files succsesfully saved!");
            }
        }
        public byte[] ConvertToFile(int ListIndex) {
            List<byte> file = new List<byte>();
            byte[] header = new byte[0];
            header = MainFunctions.b_AddBytes(header, FileBytesList[ListIndex], 0, 0, 300);
            for (int x4 = 0; x4 < header.Length; x4++) {
                file.Add(header[x4]);
            }
            for (int x3 = 0; x3 < EntryCounts[ListIndex] * 40; x3++) {
                file.Add(0);
            }

            List<int> MainTextPointer = new List<int>();
            List<int> ExtraTextPointer = new List<int>();

            for (int x2 = 0; x2 < EntryCounts[ListIndex]; x2++) {
                MainTextPointer.Add(file.Count);
                int nameLength3 = MainTextsList[ListIndex][x2].Length;
                if (Encoding.UTF8.GetString(MainTextsList[ListIndex][x2]) == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add(MainTextsList[ListIndex][x2][a17]);
                    }

                    for (int a16 = 0; a16 < 1; a16++) {
                        file.Add(0);
                    }
                }
                ExtraTextPointer.Add(file.Count);
                nameLength3 = ExtraTextsList[ListIndex][x2].Length;
                if (Encoding.UTF8.GetString(ExtraTextsList[ListIndex][x2]) == "") {
                    nameLength3 = 0;
                } else {
                    for (int a17 = 0; a17 < nameLength3; a17++) {
                        file.Add(ExtraTextsList[ListIndex][x2][a17]);
                    }

                    for (int a16 = 0; a16 < 1; a16++) {
                        file.Add(0);
                    }
                }
                int newPointer3 = MainTextPointer[x2] - 300 - 40 * x2 - 16;
                byte[] ptrBytes3 = BitConverter.GetBytes(newPointer3);
                if (Encoding.UTF8.GetString(MainTextsList[ListIndex][x2]) == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[300 + 40 * x2 + 16 + a7] = 0;
                    }
                } else {
                    newPointer3 = MainTextPointer[x2] - 300 - 40 * x2 - 16;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[300 + 40 * x2 + 16 + a7] = ptrBytes3[a7];
                    }
                }

                //-----


                newPointer3 = ExtraTextPointer[x2] - 300 - 40 * x2 - 8;
                ptrBytes3 = BitConverter.GetBytes(newPointer3);
                if (Encoding.UTF8.GetString(ExtraTextsList[ListIndex][x2]) == "") {

                    for (int a7 = 0; a7 < 4; a7++) {
                        file[300 + 40 * x2 + 8 + a7] = 0;
                    }
                } else {
                    newPointer3 = ExtraTextPointer[x2] - 300 - 40 * x2 - 8;
                    ptrBytes3 = BitConverter.GetBytes(newPointer3);
                    for (int a7 = 0; a7 < 4; a7++) {
                        file[300 + 40 * x2 + 8 + a7] = ptrBytes3[a7];
                    }
                }




                // VALUES
                byte[] o_a = CRC32CodesList[ListIndex][x2];
                for (int a8 = 0; a8 < 4; a8++) {
                    file[300 + 40 * x2 + 0 + a8] = o_a[a8];
                }
                o_a = new byte[2] { 0xFF, 0xFF };
                for (int a8 = 0; a8 < 2; a8++) {
                    file[300 + 40 * x2 + 28 + a8] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(ACBFilesList[ListIndex][x2]);
                for (int a8 = 0; a8 < 2; a8++) {
                    file[300 + 40 * x2 + 30 + a8] = o_a[a8];
                }
                o_a = BitConverter.GetBytes(CueIDsList[ListIndex][x2]);
                for (int a8 = 0; a8 < 2; a8++) {
                    file[300 + 40 * x2 + 32 + a8] = o_a[a8];
                }
                file[300 + 40 * x2 + 34] = (byte)VoiceOnlysList[ListIndex][x2];
            }
            int FileSize3 = file.Count - 284;
            byte[] sizeBytes3 = BitConverter.GetBytes(FileSize3);
            byte[] sizeBytes2 = BitConverter.GetBytes(FileSize3 + 4);
            for (int a20 = 0; a20 < 4; a20++) {
                file[280 + a20] = sizeBytes3[3 - a20];
            }
            for (int a19 = 0; a19 < 4; a19++) {
                file[268 + a19] = sizeBytes2[3 - a19];
            }
            byte[] countBytes = BitConverter.GetBytes(EntryCounts[ListIndex]);
            for (int a18 = 0; a18 < 4; a18++) {
                file[288 + a18] = countBytes[a18];
            }
            byte[] finalBytes = new byte[20]
            {
                0x00,0x00,0x00,0x08,0x00,0x00,0x00,0x02,0x00,0x79,0x77,0x77,0x00,0x00,0x00,0x04,0x00,0x00,0x00,0x00
            };
            for (int x = 0; x < finalBytes.Length; x++) {
                file.Add(finalBytes[x]);
            }
            return file.ToArray();
        }
    }
}
