using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_nus3bankEditor_code {
        public bool cleaning = false;
        public bool FileOpen = false;
        public string FilePath = "";
        public byte[] fileBytes = new byte[0];
        public bool XfbinHeader = false;
        public int NUS3_Position = 0;
        public int PROP_Position = 0;
        public int BINF_Position = 0;
        public int GRP_Position = 0;
        public int DTON_Position = 0;
        public int TONE_Position = 0;
        public int JUNK_Position = 0;
        public int PACK_Position = 0;
        public byte[] PROP_fileBytes = new byte[0];
        public byte[] BINF_fileBytes = new byte[0];
        public byte[] GRP_fileBytes = new byte[0];
        public byte[] DTON_fileBytes = new byte[0];
        public byte[] JUNK_fileBytes = new byte[0];

        public byte[] PlaySound_bytes = new byte[2] { 0xFF, 0xFF };
        public byte[] Randomizer_bytes = new byte[2] { 0x7F, 0x00 };
        public byte[] EmptySound_bytes = new byte[2] { 0x01, 0x00 };

        public List<int> TONE_SectionType_List = new List<int>();
        public List<byte[]> TONE_SectionTypeValues_List = new List<byte[]>();
        public List<string> TONE_SoundName_List = new List<string>();

        //PlaySound
        public List<int> TONE_SoundPos_List = new List<int>();
        public List<int> TONE_SoundSize_List = new List<int>();
        public List<float> TONE_MainVolume_List = new List<float>();
        public List<byte[]> TONE_SoundSettings_List = new List<byte[]>();

        public List<byte[]> TONE_SoundData_List = new List<byte[]>();
        //Randomizer
        public List<int> TONE_RandomizerType_List = new List<int>();
        public List<int> TONE_RandomizerLength_List = new List<int>();
        public List<int> TONE_RandomizerUnk1_List = new List<int>();
        public List<int> TONE_RandomizerSectionCount_List = new List<int>();
        public List<List<int>> TONE_RandomizerOneSection_ID_List = new List<List<int>>();
        public List<List<int>> TONE_RandomizerOneSection_unk_List = new List<List<int>>();
        public List<List<float>> TONE_RandomizerOneSection_PlayChance_List = new List<List<float>>();
        public List<List<int>> TONE_RandomizerOneSection_SoundID_List = new List<List<int>>();
        public List<float> TONE_RandomizerUnk2_List = new List<float>();
        public List<float> TONE_RandomizerUnk3_List = new List<float>();
        public List<float> TONE_RandomizerUnk4_List = new List<float>();
        public List<float> TONE_RandomizerUnk5_List = new List<float>();
        public List<float> TONE_RandomizerUnk6_List = new List<float>();

        public List<bool> TONE_OverlaySound_List = new List<bool>();

        public int IndexSelectedRow = 0;


        public int EntryCount = 0;

        public int FileID = 0;
        public void OpenFile(string basepath = "") {
            OpenFileDialog o = new OpenFileDialog();
            {
                o.DefaultExt = ".xfbin";
                o.Filter = "XFBIN Container(*.xfbin)|*.xfbin|NUS3BANK Container(*.nus3bank)|*.nus3bank";
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
            FilePath = o.FileName;
            fileBytes = File.ReadAllBytes(FilePath);
            if (MainFunctions.b_ReadString2(fileBytes, 0, 4) == "NUCC")
                XfbinHeader = true;
            else
                XfbinHeader = false;
            NUS3_Position = MainFunctions.b_FindBytes(fileBytes, new byte[4] { 0x4E, 0x55, 0x53, 0x33 });
            PROP_Position = MainFunctions.b_FindBytes(fileBytes, new byte[4] { 0x50, 0x52, 0x4F, 0x50 }, NUS3_Position + 0x50);
            BINF_Position = MainFunctions.b_FindBytes(fileBytes, new byte[4] { 0x42, 0x49, 0x4E, 0x46 }, NUS3_Position + 0x50);
            GRP_Position = MainFunctions.b_FindBytes(fileBytes, new byte[4] { 0x47, 0x52, 0x50, 0x20 }, NUS3_Position + 0x50);
            DTON_Position = MainFunctions.b_FindBytes(fileBytes, new byte[4] { 0x44, 0x54, 0x4F, 0x4E }, NUS3_Position + 0x50);
            TONE_Position = MainFunctions.b_FindBytes(fileBytes, new byte[4] { 0x54, 0x4F, 0x4E, 0x45 }, NUS3_Position + 0x50);
            JUNK_Position = MainFunctions.b_FindBytes(fileBytes, new byte[4] { 0x4A, 0x55, 0x4E, 0x4B }, NUS3_Position + 0x50);
            PACK_Position = MainFunctions.b_FindBytes(fileBytes, new byte[4] { 0x50, 0x41, 0x43, 0x4B }, NUS3_Position + 0x50);

            PROP_fileBytes = MainFunctions.b_ReadByteArray(fileBytes, PROP_Position, MainFunctions.b_ReadInt(fileBytes, PROP_Position + 0x4) + 0x8);
            BINF_fileBytes = MainFunctions.b_ReadByteArray(fileBytes, BINF_Position, MainFunctions.b_ReadInt(fileBytes, BINF_Position + 0x4) + 0x8);
            GRP_fileBytes = MainFunctions.b_ReadByteArray(fileBytes, GRP_Position, MainFunctions.b_ReadInt(fileBytes, GRP_Position + 0x4) + 0x8);
            DTON_fileBytes = MainFunctions.b_ReadByteArray(fileBytes, DTON_Position, MainFunctions.b_ReadInt(fileBytes, DTON_Position + 0x4) + 0x8);
            JUNK_fileBytes = MainFunctions.b_ReadByteArray(fileBytes, JUNK_Position, MainFunctions.b_ReadInt(fileBytes, JUNK_Position + 0x4) + 0x8);

            FileID = MainFunctions.b_ReadInt(BINF_fileBytes, BINF_fileBytes.Length - 0x04);
            EntryCount = MainFunctions.b_ReadInt(fileBytes, TONE_Position + 0x08);

            for (int x = 0; x < EntryCount; x++) {
                long _ptr = TONE_Position + 0x0C + (0x08 * x);
                int TONE_Size = MainFunctions.b_ReadInt(fileBytes, (int)_ptr + 4);
                int newPtr = TONE_Position + 0x08 + MainFunctions.b_ReadInt(fileBytes, (int)_ptr);
                byte[] SectionType = new byte[0];
                byte[] SectionTypeValues = new byte[0];
                byte[] SoundData = new byte[0];
                SectionType = MainFunctions.b_ReadByteArray(fileBytes, newPtr + 0x04, 0x02);
                SectionTypeValues = MainFunctions.b_ReadByteArray(fileBytes, newPtr + 0x06, 0x06);
                if (BitConverter.ToString(SectionType) == BitConverter.ToString(PlaySound_bytes))
                    TONE_SectionType_List.Add(0);
                else if (BitConverter.ToString(SectionType) == BitConverter.ToString(Randomizer_bytes))
                    TONE_SectionType_List.Add(1);
                else
                    TONE_SectionType_List.Add(2);
                TONE_SectionTypeValues_List.Add(SectionTypeValues);

                string SoundName = MainFunctions.b_ReadString2(fileBytes, TONE_Position + 0x08 + MainFunctions.b_ReadInt(fileBytes, (int)_ptr) + 0x0D);
                if (SoundName == "")
                    SoundName = "Empty slot";

                TONE_SoundName_List.Add(SoundName);
                //PlaySound
                int SoundSize = 0;
                int SoundPos = 0;
                float MainVolume = 0;
                byte[] SectionSettings = new byte[0];

                //Randomizer
                int RandomizerType = 0;
                int RandomizerLength = 0;
                int RandomizerUnk1 = -1;
                int RandomizerSectionCount = 0;
                List<int> Randomizer_OneSectionID = new List<int>();
                List<int> Randomizer_OneSection_unk = new List<int>();
                List<float> Randomizer_OneSection_PlayChance = new List<float>();
                List<int> Randomizer_OneSection_SoundID = new List<int>();

                float RandomizerUnk2 = 0;
                float RandomizerUnk3 = 0;
                float RandomizerUnk4 = 0;
                float RandomizerUnk5 = 0;
                float RandomizerUnk6 = 0;
                bool OverlaySound = false;

                if (TONE_SectionType_List[x] == 0) {
                    int newPos = 0;
                    do {
                        newPos++;
                    }
                    while (MainFunctions.b_ReadInt(fileBytes, newPtr + 0x0D + newPos) != 8);
                    newPos += 0xD;
                    newPtr += newPos;
                    SoundPos = MainFunctions.b_ReadInt(fileBytes, newPtr + 0x04);
                    SoundSize = MainFunctions.b_ReadInt(fileBytes, newPtr + 0x08);
                    MainVolume = MainFunctions.b_ReadFloat(fileBytes, newPtr + 0x0C);
                    int index = newPos + 0x10;
                    SectionSettings = MainFunctions.b_ReadByteArray(fileBytes, newPtr + 0x10, TONE_Size - index - 4);
                    OverlaySound = !Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, newPtr + 0x10 + TONE_Size - index - 4));
                    SoundData = MainFunctions.b_ReadByteArray(fileBytes, PACK_Position + 8 + SoundPos, SoundSize);

                } else if (TONE_SectionType_List[x] == 1) {
                    int newPos = 0;
                    do {
                        newPos++;
                    }
                    while (MainFunctions.b_ReadInt(fileBytes, newPtr + 0x0D + newPos) != 1);
                    newPos += 0xD;
                    RandomizerType = MainFunctions.b_ReadInt(fileBytes, newPtr + newPos);
                    RandomizerLength = MainFunctions.b_ReadInt(fileBytes, newPtr + newPos + 0x04);
                    RandomizerUnk1 = MainFunctions.b_ReadInt(fileBytes, newPtr + newPos + 0x08);
                    RandomizerSectionCount = MainFunctions.b_ReadInt(fileBytes, newPtr + newPos + 0x0C);
                    for (int c = 0; c < RandomizerSectionCount; c++) {
                        int SectionID = MainFunctions.b_ReadInt(fileBytes, newPtr + newPos + 0x10 + (0x10 * c));
                        int unk = MainFunctions.b_ReadInt(fileBytes, newPtr + newPos + 0x10 + (0x10 * c) + 0x04);
                        float PlayChance = MainFunctions.b_ReadFloat(fileBytes, newPtr + newPos + 0x10 + (0x10 * c) + 0x08);
                        int SoundID = MainFunctions.b_ReadInt(fileBytes, newPtr + newPos + 0x10 + (0x10 * c) + 0x0C);
                        Randomizer_OneSectionID.Add(SectionID);
                        Randomizer_OneSection_unk.Add(unk);
                        Randomizer_OneSection_PlayChance.Add(PlayChance);
                        Randomizer_OneSection_SoundID.Add(SoundID);
                    }
                    RandomizerUnk2 = MainFunctions.b_ReadFloat(fileBytes, newPtr + newPos + 0x10 + (0x10 * RandomizerSectionCount));
                    RandomizerUnk3 = MainFunctions.b_ReadFloat(fileBytes, newPtr + newPos + 0x10 + (0x10 * RandomizerSectionCount) + 0x04);
                    RandomizerUnk4 = MainFunctions.b_ReadFloat(fileBytes, newPtr + newPos + 0x10 + (0x10 * RandomizerSectionCount) + 0x08);
                    RandomizerUnk5 = MainFunctions.b_ReadFloat(fileBytes, newPtr + newPos + 0x10 + (0x10 * RandomizerSectionCount) + 0x0C);
                    RandomizerUnk6 = MainFunctions.b_ReadFloat(fileBytes, newPtr + newPos + 0x10 + (0x10 * RandomizerSectionCount) + 0x10);
                    OverlaySound = !Convert.ToBoolean(MainFunctions.b_ReadInt(fileBytes, newPtr + newPos + 0x10 + (0x10 * RandomizerSectionCount) + 0x14));

                }
                TONE_SoundPos_List.Add(SoundPos);
                TONE_SoundSize_List.Add(SoundSize);
                TONE_OverlaySound_List.Add(OverlaySound);
                TONE_MainVolume_List.Add(MainVolume);
                TONE_SoundSettings_List.Add(SectionSettings);
                TONE_SoundData_List.Add(SoundData);

                TONE_RandomizerType_List.Add(RandomizerType);
                TONE_RandomizerLength_List.Add(RandomizerLength);
                TONE_RandomizerUnk1_List.Add(RandomizerUnk1);
                TONE_RandomizerSectionCount_List.Add(RandomizerSectionCount);
                TONE_RandomizerOneSection_ID_List.Add(Randomizer_OneSectionID);
                TONE_RandomizerOneSection_unk_List.Add(Randomizer_OneSection_unk);
                TONE_RandomizerOneSection_PlayChance_List.Add(Randomizer_OneSection_PlayChance);
                TONE_RandomizerOneSection_SoundID_List.Add(Randomizer_OneSection_SoundID);
                TONE_RandomizerUnk2_List.Add(RandomizerUnk2);
                TONE_RandomizerUnk3_List.Add(RandomizerUnk3);
                TONE_RandomizerUnk4_List.Add(RandomizerUnk4);
                TONE_RandomizerUnk5_List.Add(RandomizerUnk5);
                TONE_RandomizerUnk6_List.Add(RandomizerUnk6);
                FileOpen = true;
            }
            
        }
        public void ClearFile() {
            cleaning = true;
            FileOpen = false;
            FilePath = "";
            fileBytes = new byte[0];
            XfbinHeader = false;
            NUS3_Position = 0;
            PROP_Position = 0;
            BINF_Position = 0;
            GRP_Position = 0;
            DTON_Position = 0;
            TONE_Position = 0;
            JUNK_Position = 0;
            PACK_Position = 0;
            PROP_fileBytes = new byte[0];
            BINF_fileBytes = new byte[0];
            GRP_fileBytes = new byte[0];
            DTON_fileBytes = new byte[0];
            JUNK_fileBytes = new byte[0];
            TONE_SectionType_List = new List<int>();
            TONE_SectionTypeValues_List = new List<byte[]>();
            TONE_SoundName_List = new List<string>();
            TONE_SoundPos_List = new List<int>();
            TONE_SoundSize_List = new List<int>();
            TONE_MainVolume_List = new List<float>();
            TONE_SoundSettings_List = new List<byte[]>();
            TONE_SoundData_List = new List<byte[]>();
            TONE_RandomizerType_List = new List<int>();
            TONE_RandomizerLength_List = new List<int>();
            TONE_RandomizerUnk1_List = new List<int>();
            TONE_RandomizerSectionCount_List = new List<int>();
            TONE_RandomizerOneSection_ID_List = new List<List<int>>();
            TONE_RandomizerOneSection_unk_List = new List<List<int>>();
            TONE_RandomizerOneSection_PlayChance_List = new List<List<float>>();
            TONE_RandomizerOneSection_SoundID_List = new List<List<int>>();
            TONE_RandomizerUnk2_List = new List<float>();
            TONE_RandomizerUnk3_List = new List<float>();
            TONE_RandomizerUnk4_List = new List<float>();
            TONE_RandomizerUnk5_List = new List<float>();
            TONE_RandomizerUnk6_List = new List<float>();
            TONE_OverlaySound_List = new List<bool>();
            IndexSelectedRow = 0;
            EntryCount = 0;
            FileID = 0;
            cleaning = false;
        }

        public void CloseFile() {
            ClearFile();
            FileOpen = false;
            XfbinHeader = false;
            FilePath = "";
        }

        public void SaveFileAs(string basepath = "") {
            SaveFileDialog s = new SaveFileDialog();
            {
                if (XfbinHeader) {
                    s.DefaultExt = ".xfbin";
                    s.Filter = "XFBIN files|*.xfbin|NUS3BANK files|*.NUS3BANK";
                } else {
                    s.DefaultExt = ".NUS3BANK";
                    s.Filter = "NUS3BANK files|*.NUS3BANK";
                }
            }
            if (basepath == "")
                s.ShowDialog();
            else
                s.FileName = basepath;
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
            string extension = Path.GetExtension(s.FileName);
            File.WriteAllBytes(FilePath, ConvertToFile(extension));
            if (basepath == "")
                MessageBox.Show("File saved to " + FilePath + ".");
        }
        public byte[] ConvertToFile(string extension) {
            byte[] ConvertedFile = new byte[0];
            if (XfbinHeader && extension.Contains("xfbin"))
                ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, MainFunctions.b_ReadByteArray(fileBytes, 0, NUS3_Position));
            int NUS3_Length_ptr = ConvertedFile.Length;
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, Encoding.ASCII.GetBytes("NUS3"));
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, new byte[4]);
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, MainFunctions.b_ReadByteArray(fileBytes, NUS3_Position + 8, 0x30));
            int TONE_Header_Length_ptr = ConvertedFile.Length;
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, Encoding.ASCII.GetBytes("TONE"));
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, new byte[4]);
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, MainFunctions.b_ReadByteArray(fileBytes, JUNK_Position, 0x08));
            int PACK_Header_Length_ptr = ConvertedFile.Length;
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, Encoding.ASCII.GetBytes("PACK"));
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, new byte[4]);
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, PROP_fileBytes);
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, BINF_fileBytes);
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, GRP_fileBytes);
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, DTON_fileBytes);
            int TONE_Length_ptr = ConvertedFile.Length;
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, Encoding.ASCII.GetBytes("TONE"));
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, new byte[4]);
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, BitConverter.GetBytes(TONE_SoundName_List.Count));
            List<int> TONE_ptr = new List<int>();
            List<int> TONE_length = new List<int>();
            List<int> TONE_size = new List<int>();
            for (int x = 0; x < TONE_SoundName_List.Count; x++) {
                TONE_ptr.Add(ConvertedFile.Length);
                ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, new byte[8]);
            }
            byte[] PACK_SECTION = new byte[0];
            for (int x = 0; x < TONE_SoundName_List.Count; x++) {
                byte[] TONE_Section = new byte[0];
                TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[4]);
                if (TONE_SectionType_List[x] == 0) {
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, PlaySound_bytes);
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, TONE_SectionTypeValues_List[x]);
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[1] { (byte)(TONE_SoundName_List[x].Length + 1) });
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, Encoding.ASCII.GetBytes(TONE_SoundName_List[x]));
                    if (TONE_Section.Length % 4 != 0) {
                        do {
                            if (TONE_Section.Length % 4 != 0)
                                TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[1]);
                        }
                        while (TONE_Section.Length % 4 != 0);
                    } else {
                        TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[4]);
                    }
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[4]);
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[4] { 8, 0, 0, 0 });
                    if (TONE_SoundData_List[x].Length > 4) {
                        TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(PACK_SECTION.Length));
                        TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(TONE_SoundData_List[x].Length));
                        TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(TONE_MainVolume_List[x]));
                        PACK_SECTION = MainFunctions.b_AddBytes(PACK_SECTION, TONE_SoundData_List[x]);
                    } else {
                        TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[12]);
                    }
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, TONE_SoundSettings_List[x]);
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(!TONE_OverlaySound_List[x]));
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[3]);
                } else if (TONE_SectionType_List[x] == 1) {
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, Randomizer_bytes);
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, TONE_SectionTypeValues_List[x]);
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[1] { (byte)(TONE_SoundName_List[x].Length + 1) });
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, Encoding.ASCII.GetBytes(TONE_SoundName_List[x]));
                    do {
                        TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[1]);
                    }
                    while (TONE_Section.Length % 4 != 0);
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[4] { 1, 0, 0, 0 });
                    int Randomizer_Length = (TONE_RandomizerSectionCount_List[x] * 0x10) + 0x08;
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[4] { (byte)Randomizer_Length, 0, 0, 0 });
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(TONE_RandomizerUnk1_List[x]));
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(TONE_RandomizerSectionCount_List[x]));
                    for (int c = 0; c < TONE_RandomizerSectionCount_List[x]; c++) {
                        if (c != TONE_RandomizerSectionCount_List[x] - 1)
                            TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(c + 1));
                        else
                            TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[4] { 0, 0, 0, 0 });
                        TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(TONE_RandomizerOneSection_unk_List[x][c]));
                        TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(TONE_RandomizerOneSection_PlayChance_List[x][c]));
                        TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(TONE_RandomizerOneSection_SoundID_List[x][c]));
                    }
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(TONE_RandomizerUnk2_List[x]));
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(TONE_RandomizerUnk3_List[x]));
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(TONE_RandomizerUnk4_List[x]));
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(TONE_RandomizerUnk5_List[x]));
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, BitConverter.GetBytes(TONE_RandomizerUnk6_List[x]));
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[4] { 0, 0, 0, 0 });
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[3]);

                } else if (TONE_SectionType_List[x] == 2) {
                    TONE_Section = MainFunctions.b_AddBytes(TONE_Section, new byte[8] { 0x01, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00 });
                }
                ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, TONE_Section);
                TONE_length.Add(ConvertedFile.Length - TONE_Length_ptr - 0x08 - TONE_Section.Length);
                TONE_size.Add(TONE_Section.Length);
                ConvertedFile = MainFunctions.b_ReplaceBytes(ConvertedFile, BitConverter.GetBytes(TONE_length[x]), TONE_ptr[x]);
                ConvertedFile = MainFunctions.b_ReplaceBytes(ConvertedFile, BitConverter.GetBytes(TONE_size[x]), TONE_ptr[x] + 4);
            }
            ConvertedFile = MainFunctions.b_ReplaceBytes(ConvertedFile, BitConverter.GetBytes(ConvertedFile.Length - TONE_Length_ptr - 0x08), TONE_Length_ptr + 4);
            int Tone_len = ConvertedFile.Length - TONE_Length_ptr - 0x08;
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, JUNK_fileBytes);
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, Encoding.ASCII.GetBytes("PACK"));
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, BitConverter.GetBytes(PACK_SECTION.Length));
            ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, PACK_SECTION);
            int GRP_pos = MainFunctions.b_FindBytes(ConvertedFile, new byte[4] { 0x47, 0x52, 0x50, 0x20 }, NUS3_Length_ptr + 0x50);
            ConvertedFile = MainFunctions.b_ReplaceBytes(ConvertedFile, BitConverter.GetBytes(FileID), GRP_pos - 4);
            ConvertedFile = MainFunctions.b_ReplaceBytes(ConvertedFile, BitConverter.GetBytes(PACK_SECTION.Length), PACK_Header_Length_ptr + 4);
            ConvertedFile = MainFunctions.b_ReplaceBytes(ConvertedFile, BitConverter.GetBytes(Tone_len), TONE_Header_Length_ptr + 4);
            int NUS3_Size = ConvertedFile.Length - NUS3_Length_ptr - 0x08;
            ConvertedFile = MainFunctions.b_ReplaceBytes(ConvertedFile, BitConverter.GetBytes(NUS3_Size), NUS3_Length_ptr + 4);
            if (XfbinHeader && extension.Contains("xfbin")) {
                //ConvertedFile = MainFunctions.b_ReplaceBytes(ConvertedFile, BitConverter.GetBytes(FileID), NUS3_Position - 4);
                ConvertedFile = MainFunctions.b_ReplaceBytes(ConvertedFile, BitConverter.GetBytes(ConvertedFile.Length - NUS3_Length_ptr), NUS3_Length_ptr - 4, 1);
                ConvertedFile = MainFunctions.b_ReplaceBytes(ConvertedFile, BitConverter.GetBytes(ConvertedFile.Length - NUS3_Length_ptr + 4), NUS3_Length_ptr - 0x10, 1);
                ConvertedFile = MainFunctions.b_AddBytes(ConvertedFile, new byte[0x14] { 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x02, 0x00, 0x79, 0x18, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 });
            }
            return ConvertedFile;
        }
    }
}
