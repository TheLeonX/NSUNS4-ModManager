using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_MovesetCoder_code {
        public int index1;
        public int index2;
        public int index3;
        bool fileOpen = false;
        public string filePath = "";
        byte[] fileBytes;
        //Effect Entry
        bool effectSecFound = false;
        int effectSecLength = 0;
        int effectSecCount = 0;
        List<string> effectSecName = new List<string>();
        List<string> effectSecSkillName = new List<string>();
        List<string> effectSecSkillEntry = new List<string>();
        List<int> effectSecSkillValue = new List<int>();
        List<byte> EffectSection = new List<byte>();
        public string[] sectionnames;
        //Collision Entry
        public bool collisionChanged = false;
        public int collisionSecLength = 0;
        public int collisionSecCount = 0;
        public List<int> collisionSecTypeValue = new List<int>();
        public List<int> collisionSecStateValue = new List<int>();
        public List<int> collisionSecEnablerBoneValue = new List<int>();
        public List<long> collisionSecRadiusValue = new List<long>();
        public List<long> collisionSecYPosValue = new List<long>();
        public List<long> collisionSecZPosValue = new List<long>();
        public List<string> collisionSecBoneName = new List<string>();

        List<byte[]> verSection = new List<byte[]>();
        List<int> anmCount = new List<int>();
        List<List<byte[]>> anmSection = new List<List<byte[]>>();

        public List<int> verList = new List<int>();
        List<int> verLength = new List<int>();
        public List<List<byte[]>> plAnmList = new List<List<byte[]>>();
        public List<List<List<byte[]>>> movementList = new List<List<List<byte[]>>>();
        public void OpenFile(string loadPath = "") {
            if (loadPath == "") {
                OpenFileDialog o = new OpenFileDialog();
                o.ShowDialog();
                if (o.FileName == "" || File.Exists(o.FileName) == false) return;

                filePath = o.FileName;
            } else
                filePath = loadPath;
            fileBytes = File.ReadAllBytes(filePath);

            // Find all ver sections
            int actualver = 0;
            while (actualver != -1) {
                actualver = XfbinParser.FindString(fileBytes, "ver0.000", actualver + 1);
                //MessageBox.Show(actualver.ToString("X2"));
                if (actualver != -1) {
                    verList.Add(actualver);
                    verLength.Add(MainFunctions.b_ReadIntRev(fileBytes, actualver - 4));
                }
            }

            // Add all ver section byte data
            for (int x = 0; x < verList.Count; x++) {
                List<byte> actualSection = new List<byte>();
                int begin = verList[x];
                int end = verLength[x];

                for (int y = 0; y < end; y++) {
                    actualSection.Add(fileBytes[begin + y]);
                }

                verSection.Add(actualSection.ToArray());
                //File.WriteAllBytes(filePath + "_" + x.ToString(), actualSection.ToArray());
            }
            List<string> NamesList = new List<string>();
            //Effect data
            NamesList = XfbinParser.GetNameList(fileBytes);
            int EffectIndex = 0;
            int EffectIndexCounter = -1;
            do {
                EffectIndex++;
                EffectIndexCounter = NamesList[EffectIndex - 1].IndexOf("prm_sklslot");
            }
            while (EffectIndexCounter == -1);
            byte[] EffectEntry = new byte[8]
            {
                    0x00,
                    0x00,
                    0x00,
                    (byte)EffectIndex,
                    0x00,
                    0x63,
                    0x00,
                    0x00
            };
            int EffectPos = XfbinParser.FindBytes(fileBytes, EffectEntry, 0);
            //MessageBox.Show(EffectPos.ToString("X2"));
            if (EffectPos == -1) {
                EffectEntry = new byte[6]
                {
                        0x00,
                        0x00,
                        0x00,
                        (byte)EffectIndex,
                        0x00,
                        0x63
                };
                EffectPos = XfbinParser.FindBytes(fileBytes, EffectEntry, 0);
            }
            if (EffectPos == -1) {
                EffectEntry = new byte[6]
                {
                        0x00,
                        0x00,
                        0x00,
                        (byte)EffectIndex,
                        0x00,
                        0x79
                };
                EffectPos = XfbinParser.FindBytes(fileBytes, EffectEntry, 0);
            }
            if (EffectPos == -1) {
                EffectEntry = new byte[6]
                {
                        0x00,
                        0x00,
                        0x00,
                        (byte)EffectIndex,
                        0x00,
                        0x7A
                };
                EffectPos = XfbinParser.FindBytes(fileBytes, EffectEntry, 0);
            }
            if (EffectPos != -1) {
                EffectPos = EffectPos + 12;
                effectSecFound = true;
                effectSecLength = MainFunctions.b_ReadIntRev(fileBytes, EffectPos - 4);
                int begin = EffectPos;
                int end = effectSecLength;
                effectSecCount = effectSecLength / 0x81;
                for (int z = 0; z < effectSecCount; z++) {
                    int SkillIndex = (z * 0x81) + EffectPos;
                    int SkillNameIndex = (z * 0x81) + 0x40 + EffectPos;
                    int SkillEntryIndex = (z * 0x81) + 0x60 + EffectPos;
                    int SkillValueIndex = (z * 0x81) + 0x80 + EffectPos;
                    string Skill = MainFunctions.b_ReadString2(fileBytes, SkillIndex, 0x20);
                    string SkillName = MainFunctions.b_ReadString2(fileBytes, SkillNameIndex, 0x20);
                    string SkillEntry = MainFunctions.b_ReadString2(fileBytes, SkillEntryIndex, 0x20);
                    int SkillValue = fileBytes[SkillValueIndex];
                    effectSecName.Add(Skill);
                    effectSecSkillName.Add(SkillName);
                    effectSecSkillEntry.Add(SkillEntry);
                    effectSecSkillValue.Add(SkillValue);
                }

                //Hit Collision data
                int HitIndex = 0;
                int HitIndexCounter = -1;
                do {
                    HitIndex++;
                    HitIndexCounter = NamesList[HitIndex - 1].IndexOf("prm_hit");
                }
                while (HitIndexCounter == -1);
                byte[] CollisionEntry = new byte[8]
                {
                    0x00,
                    0x00,
                    0x00,
                    (byte)HitIndex,
                    0x00,
                    0x63,
                    0x00,
                    0x00
                };
                int CollisionPos = XfbinParser.FindBytes(fileBytes, CollisionEntry, 0);
                if (CollisionPos == -1) {
                    CollisionEntry = new byte[6]
                    {
                        0x00,
                        0x00,
                        0x00,
                        (byte)HitIndex,
                        0x00,
                        0x63
                    };
                    CollisionPos = XfbinParser.FindBytes(fileBytes, CollisionEntry, 0);
                }
                if (CollisionPos == -1) {
                    CollisionEntry = new byte[6]
                    {
                        0x00,
                        0x00,
                        0x00,
                        (byte)HitIndex,
                        0x00,
                        0x79
                    };
                    CollisionPos = XfbinParser.FindBytes(fileBytes, CollisionEntry, 0);
                }
                if (CollisionPos == -1) {
                    CollisionEntry = new byte[6]
                    {
                        0x00,
                        0x00,
                        0x00,
                        (byte)HitIndex,
                        0x00,
                        0x7A
                    };
                    CollisionPos = XfbinParser.FindBytes(fileBytes, CollisionEntry, 0);
                }
                if (CollisionPos != -1) {

                    CollisionPos = CollisionPos + 16;
                    collisionSecLength = MainFunctions.b_ReadIntRev(fileBytes, CollisionPos - 8);
                    collisionSecCount = collisionSecLength / 0x5C;
                    for (int z = 0; z < collisionSecCount; z++) {
                        int CollisionTypeIndex = (z * 0x5C) + CollisionPos;
                        int CollisionStateIndex = (z * 0x5C) + 0x4 + CollisionPos;
                        int CollisionLoadBoneIndex = (z * 0x5C) + 0x8 + CollisionPos;
                        int CollisionHitboxIndex = (z * 0x5C) + 0x10 + CollisionPos;
                        int CollisionRadiusValueIndex = (z * 0x5C) + 0x50 + CollisionPos;
                        int CollisionYPosValueIndex = (z * 0x5C) + 0x54 + CollisionPos;
                        int CollisionZPosValueIndex = (z * 0x5C) + 0x58 + CollisionPos;


                        int CollisionType = fileBytes[CollisionTypeIndex];
                        int CollisionState = fileBytes[CollisionStateIndex];
                        int CollisionLoadBone = fileBytes[CollisionLoadBoneIndex];
                        long CollisionRadiusValue = fileBytes[CollisionRadiusValueIndex] + fileBytes[CollisionRadiusValueIndex + 1] * 0x100 + fileBytes[CollisionRadiusValueIndex + 2] * 0x10000 + fileBytes[CollisionRadiusValueIndex + 3] * 0x1000000;
                        long CollisionYPosValue = fileBytes[CollisionYPosValueIndex] + fileBytes[CollisionYPosValueIndex + 1] * 0x100 + fileBytes[CollisionYPosValueIndex + 2] * 0x10000 + fileBytes[CollisionYPosValueIndex + 3] * 0x1000000;
                        long CollisionZPosValue = fileBytes[CollisionZPosValueIndex] + fileBytes[CollisionZPosValueIndex + 1] * 0x100 + fileBytes[CollisionZPosValueIndex + 2] * 0x10000 + fileBytes[CollisionZPosValueIndex + 3] * 0x1000000;
                        string CollisionHitbox = MainFunctions.b_ReadString2(fileBytes, CollisionHitboxIndex, 0x20);

                        collisionSecTypeValue.Add(CollisionType);
                        collisionSecStateValue.Add(CollisionState);
                        collisionSecEnablerBoneValue.Add(CollisionLoadBone);
                        collisionSecRadiusValue.Add(CollisionRadiusValue);
                        collisionSecYPosValue.Add(CollisionYPosValue);
                        collisionSecZPosValue.Add(CollisionZPosValue);
                        collisionSecBoneName.Add(CollisionHitbox);
                    }
                }

            } else {
                MessageBox.Show("Effect section couldn't be found. If you used character prm, send this file in modding group and make sure it's original file from game.\nEffects will not be affected!");

            }
            //MessageBox.Show(EffectPos.ToString("X2"));
            // List all anm sections
            int motDMG = XfbinParser.FindString(fileBytes, "prm_motcmn", 0);
            int awaSec = XfbinParser.FindString(fileBytes, "prm_awa", 0);
            int bossSec = XfbinParser.FindString(fileBytes, "prm_boss", 0);

            int BossIndex = 0;
            if (awaSec != -1 && bossSec != -1) {
                int BossIndexCounter = -1;
                do {
                    BossIndex++;
                    BossIndexCounter = NamesList[BossIndex - 1].IndexOf("prm_boss");
                }
                while (BossIndexCounter == -1);
            }


            if (motDMG == -1 && awaSec != -1 && (BossIndex > 8 || bossSec == -1)) {
                string[] sectionnames_loc =
                {
                    "Awakening",
                    "Base",
                    "Jutsu",
                    "Ultimate Jutsu",
                    "Expansion A",
                    "Expansion B",
                    "Expansion C",
                    "Expansion D",
                    "Expansion E",
                    "Expansion F",
                    "Expansion G",
                    "Expansion H",
                    "Expansion I"
                };
                sectionnames = sectionnames_loc;
            } else if (motDMG != -1 && awaSec != -1 && (BossIndex > 8 || bossSec == -1)) {
                string[] sectionnames_loc =
                {
                    "Awakening",
                    "Base",
                    "Damage animations",
                    "Jutsu",
                    "Ultimate Jutsu",
                    "Expansion A",
                    "Expansion B",
                    "Expansion C",
                    "Expansion D",
                    "Expansion E",
                    "Expansion F",
                    "Expansion G",
                    "Expansion H",
                    "Expansion I"
                };
                sectionnames = sectionnames_loc;
            } else if (motDMG != -1 && awaSec == -1 && (BossIndex > 8 || bossSec == -1)) {
                string[] sectionnames_loc =
                {
                    "Base",
                    "Damage animations",
                    "Jutsu",
                    "Ultimate Jutsu",
                    "Expansion A",
                    "Expansion B",
                    "Expansion C",
                    "Expansion D",
                    "Expansion E",
                    "Expansion F",
                    "Expansion G",
                    "Expansion H",
                    "Expansion I"
                };
                sectionnames = sectionnames_loc;
            } else if (motDMG == -1 && awaSec == -1 && (BossIndex > 8 || bossSec == -1)) {
                string[] sectionnames_loc =
                {
                    "Base",
                    "Jutsu",
                    "Ultimate Jutsu",
                    "Expansion A",
                    "Expansion B",
                    "Expansion C",
                    "Expansion D",
                    "Expansion E",
                    "Expansion G",
                    "Expansion H",
                    "Expansion I"
                };
                sectionnames = sectionnames_loc;
            } else {
                string[] sectionnames_loc =
                {
                    "Expansion A",
                    "Expansion B",
                    "Expansion C",
                    "Expansion D",
                    "Expansion E",
                    "Expansion G",
                    "Expansion H",
                    "Expansion I",
                    "Expansion J",
                    "Expansion K",
                    "Expansion L"
                };
                sectionnames = sectionnames_loc;
            }
            for (int a = 0; a < verList.Count; a++) {

                byte[] actualSection = verSection[a];
                int anmSectionCount = actualSection[0x30];
                int start = 0x40;
                int index = 0x40;

                plAnmList.Add(new List<byte[]>());
                movementList.Add(new List<List<byte[]>>()); //

                //anmSection.Add(new List<byte[]>());
                //anmCount.Add(actualSection[0x30]);

                for (int x = 0; x < anmSectionCount; x++) {
                    // Add this pl_anm's header to plAnmList
                    List<byte> planmheader = new List<byte>();
                    for (int y = 0; y < 0xD4; y++) {
                        planmheader.Add(actualSection[start + y]);
                    }
                    //MessageBox.Show(MainFunctions.b_ReadString(planmheader.ToArray(), 0));
                    plAnmList[a].Add(planmheader.ToArray());
                    movementList[a].Add(new List<byte[]>());

                    index = start + 0x50;
                    byte m_movcount = actualSection[index];
                    //MessageBox.Show("ANM " + x.ToString() + " has " + m_movcount.ToString() + " sections");

                    index = start + 0xD4;

                    // Add each movement section of this pl_anm to the master list
                    for (int y = 0; y < m_movcount; y++) {
                        List<byte> movementsection = new List<byte>();

                        // Default movement section length is 0x40
                        int sectionLength = 0x40;

                        int function = actualSection[index + 0x22] * 0x1 + actualSection[index + 0x23] * 0x100;

                        switch (function) {
                            case 0x83:
                                if (index + 0x40 < actualSection.Length) {
                                    string str = MainFunctions.b_ReadString(actualSection, index + 0x40);
                                    if (str == "SPSKILL_END") sectionLength = 0xA0;
                                }
                                break;
                            case 0x5E:
                            case 0x8A:
                            case 0xC1:
                            case 0xC3:
                            case 0xC6:
                            case 0xC8:
                            case 0xCA:
                            case 0xD1:
                            case 0xD3:
                            case 0xD5:
                            case 0xD7:
                            case 0xD9:
                                sectionLength = 0xA0;
                                break;
                            case 0xA0:
                            case 0xA1:
                            case 0xA2:
                            case 0xA3:
                            case 0xA4:
                            case 0xA5:
                            case 0xA6:
                                if (index + 0x40 < actualSection.Length) {
                                    string str = MainFunctions.b_ReadString(actualSection, index + 0x40);
                                    if (str.Length >= 7 && str.Substring(0, 7) == "SKL_ATK") sectionLength = 0xA0;
                                }
                                break;
                        }

                        // If there's a D (from DAMAGE_ID) in section + 0x40, length is 0xA0
                        if (index + 0x40 < actualSection.Length) {
                            string str = MainFunctions.b_ReadString(actualSection, index + 0x40);
                            if (str.Length > 3 && (str.Substring(0, 3) == "DMG" || str.Substring(0, 3) == "DAM")) {
                                sectionLength = 0xA0;
                            }

                            /*char act1 = (char)actualSection[index + 0x40];
                            if (!Char.IsDigit(act1) && Char.IsUpper(act1))
                            {
                                byte byte1 = actualSection[index + 0x40 + 0x2A]; // 20 
                                byte byte2 = actualSection[index + 0x40 + 0x2C]; // 22

                                if(byte1 == 0x0 && byte2 == 0x0)
                                {

                                }
                            }*/
                        }

                        // If the first letter of the hitbox is caps, then it's a special 0x60 section
                        // char act = (char)actualSection[index];
                        // if (actualSection[index] != 0x0 && Char.IsUpper(act) && !Char.IsDigit(act)) sectionLength = 0x40;

                        //MessageBox.Show("Movement " + y.ToString() + " of ANM " + x.ToString() + " is " + sectionLength.ToString("X2") + " bytes long");
                        for (int z = 0; z < sectionLength; z++) movementsection.Add(actualSection[z + index]);
                        index = index + sectionLength;

                        // Add to master list
                        movementList[a][x].Add(movementsection.ToArray());
                    }

                    start = index;
                }
            }

            fileOpen = true;
        }
        public byte[] GenerateFile() {
            byte[] newBytes = new byte[0];
            byte[] test = new byte[0];
            newBytes = MainFunctions.b_AddBytes(newBytes, fileBytes, 0, 0, verList[0]);

            int verCount = plAnmList.Count;

            for (int x = 0; x < verCount; x++) {
                int countLength = newBytes.Length;

                // Add header of ver
                byte[] header = new byte[0x40];
                header[0x00] = 0x76;
                header[0x01] = 0x65;
                header[0x02] = 0x72;
                header[0x03] = 0x30;
                header[0x04] = 0x2E;
                header[0x05] = 0x30;
                header[0x06] = 0x30;
                header[0x07] = 0x30;
                header[0x30] = (byte)plAnmList[x].Count;
                newBytes = MainFunctions.b_AddBytes(newBytes, header);

                // Add each pl_anm
                for (int y = 0; y < plAnmList[x].Count; y++) {
                    newBytes = MainFunctions.b_AddBytes(newBytes, plAnmList[x][y]);

                    for (int z = 0; z < movementList[x][y].Count; z++) {
                        newBytes = MainFunctions.b_AddBytes(newBytes, movementList[x][y][z]);
                    }
                }

                int totalLen = newBytes.Length - countLength;
                newBytes = MainFunctions.b_ReplaceBytes(newBytes, BitConverter.GetBytes(totalLen), countLength - 0x04, 1);
                newBytes = MainFunctions.b_ReplaceBytes(newBytes, BitConverter.GetBytes(totalLen + 4), countLength - 0x10, 1);

                int start = verList[x];
                int end = start + verLength[x];
                int next = fileBytes.Length;

                if (x < verCount - 1) next = verList[x + 1];

                int totalBytesToAdd = next - end;
                byte[] toaddempty = new byte[totalBytesToAdd];

                int actual = newBytes.Length;
                newBytes = MainFunctions.b_AddBytes(newBytes, toaddempty);

                for (int y = 0; y < totalBytesToAdd; y++) {
                    newBytes[actual + y] = fileBytes[end + y];
                }
            }
            if (effectSecFound) {
                int FirstSize = 0;
                byte[] EffectSections = new byte[0];
                for (int z = 0; z < effectSecCount; z++) {
                    byte[] NewEffectSection = new byte[0x81];
                    byte[] EffectSectionName = new byte[0];
                    EffectSectionName = MainFunctions.b_AddBytes(EffectSectionName, Encoding.ASCII.GetBytes(effectSecName[z]));
                    NewEffectSection = MainFunctions.b_ReplaceBytes(NewEffectSection, EffectSectionName, 0);

                    byte[] EffectSectionSkillName = new byte[0];
                    EffectSectionSkillName = MainFunctions.b_AddBytes(EffectSectionSkillName, Encoding.ASCII.GetBytes(effectSecSkillName[z]));
                    NewEffectSection = MainFunctions.b_ReplaceBytes(NewEffectSection, EffectSectionSkillName, 0x40);

                    byte[] EffectSectionSkillEntry = new byte[0];
                    EffectSectionSkillEntry = MainFunctions.b_AddBytes(EffectSectionSkillEntry, Encoding.ASCII.GetBytes(effectSecSkillEntry[z]));
                    NewEffectSection = MainFunctions.b_ReplaceBytes(NewEffectSection, EffectSectionSkillEntry, 0x60);

                    byte[] EffectSectionSkillValue = new byte[1]
                    {
                        (byte)effectSecSkillValue[z]
                    };

                    NewEffectSection = MainFunctions.b_ReplaceBytes(NewEffectSection, EffectSectionSkillValue, 0x80);
                    EffectSections = MainFunctions.b_AddBytes(EffectSections, NewEffectSection);
                    FirstSize = FirstSize + 0x81;
                }
                int newEffectPos = -1;
                int motDMG = XfbinParser.FindString(newBytes, "prm_motcmn", 0);
                int awaSec = XfbinParser.FindString(newBytes, "prm_awa", 0);
                if (motDMG == -1 && awaSec != -1) {
                    //Effect data
                    byte[] EffectEntry = new byte[8]
                    {
                        0x00,
                        0x00,
                        0x00,
                        0x07,
                        0x00,
                        0x63,
                        0x00,
                        0x00
                    };
                    //MessageBox.Show(EffectPos.ToString("X2"));
                    newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    if (newEffectPos == -1) {
                        EffectEntry = new byte[6]
                        {
                            0x00,
                            0x00,
                            0x00,
                            0x07,
                            0x00,
                            0x63
                        };
                        newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    }
                    if (newEffectPos == -1) {
                        EffectEntry = new byte[6]
                        {
                            0x00,
                            0x00,
                            0x00,
                            0x07,
                            0x00,
                            0x79
                        };
                        newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    }
                    if (newEffectPos == -1) {
                        EffectEntry = new byte[6]
                        {
                            0x00,
                            0x00,
                            0x00,
                            0x07,
                            0x00,
                            0x7A
                        };
                        newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    }
                } else if (motDMG != -1 && awaSec != -1) {
                    //Effect data
                    byte[] EffectEntry = new byte[8]
                    {
                        0x00,
                        0x00,
                        0x00,
                        0x08,
                        0x00,
                        0x63,
                        0x00,
                        0x00
                    };
                    //MessageBox.Show(EffectPos.ToString("X2"));
                    newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    if (newEffectPos == -1) {
                        EffectEntry = new byte[6]
                        {
                        0x00,
                        0x00,
                        0x00,
                        0x08,
                        0x00,
                        0x63
                        };
                        newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    }
                    if (newEffectPos == -1) {
                        EffectEntry = new byte[6]
                        {
                        0x00,
                        0x00,
                        0x00,
                        0x08,
                        0x00,
                        0x79
                        };
                        newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    }
                    if (newEffectPos == -1) {
                        EffectEntry = new byte[6]
                        {
                        0x00,
                        0x00,
                        0x00,
                        0x08,
                        0x00,
                        0x7A
                        };
                        newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    }
                } else if (motDMG != -1 && awaSec == -1) {
                    //Effect data
                    byte[] EffectEntry = new byte[8]
                    {
                        0x00,
                        0x00,
                        0x00,
                        0x07,
                        0x00,
                        0x63,
                        0x00,
                        0x00
                    };
                    //MessageBox.Show(EffectPos.ToString("X2"));
                    newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    if (newEffectPos == -1) {
                        EffectEntry = new byte[6]
                        {
                            0x00,
                            0x00,
                            0x00,
                            0x07,
                            0x00,
                            0x63
                        };
                        newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    }
                    if (newEffectPos == -1) {
                        EffectEntry = new byte[6]
                        {
                            0x00,
                            0x00,
                            0x00,
                            0x07,
                            0x00,
                            0x79
                        };
                        newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    }
                    if (newEffectPos == -1) {
                        EffectEntry = new byte[6]
                        {
                            0x00,
                            0x00,
                            0x00,
                            0x07,
                            0x00,
                            0x7A
                        };
                        newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    }
                } else if (motDMG == -1 && awaSec == -1) {
                    //Effect data
                    byte[] EffectEntry = new byte[8]
                    {
                        0x00,
                        0x00,
                        0x00,
                        0x06,
                        0x00,
                        0x63,
                        0x00,
                        0x00
                    };
                    //MessageBox.Show(EffectPos.ToString("X2"));
                    newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    if (newEffectPos == -1) {
                        EffectEntry = new byte[6]
                        {
                            0x00,
                            0x00,
                            0x00,
                            0x06,
                            0x00,
                            0x63
                        };
                        newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    }
                    if (newEffectPos == -1) {
                        EffectEntry = new byte[6]
                        {
                            0x00,
                            0x00,
                            0x00,
                            0x06,
                            0x00,
                            0x79
                        };
                        newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    }
                    if (newEffectPos == -1) {
                        EffectEntry = new byte[6]
                        {
                            0x00,
                            0x00,
                            0x00,
                            0x06,
                            0x00,
                            0x7A
                        };
                        newEffectPos = XfbinParser.FindBytes(newBytes, EffectEntry, 0);
                    }
                }
                int newLastEffectPos = newEffectPos + 12;
                int newEffectSecLength = MainFunctions.b_ReadIntRev(newBytes, newLastEffectPos - 4);
                List<byte> ChangedEffectSection = new List<byte>();
                for (int j = 0; j < newBytes.Length; j++) {
                    ChangedEffectSection.Add(newBytes[j]);
                }
                for (int j = newLastEffectPos; j < newLastEffectPos + newEffectSecLength; j++) {
                    ChangedEffectSection.RemoveAt(newLastEffectPos);
                }
                for (int j = 0; j < EffectSections.Length; j++) {
                    ChangedEffectSection.Insert(newLastEffectPos + j, EffectSections[j]);
                }
                test = new byte[ChangedEffectSection.Count];
                for (int j = 0; j < ChangedEffectSection.Count; j++) {
                    test[j] = ChangedEffectSection[j];
                }
                byte[] sizeBytes3 = BitConverter.GetBytes(FirstSize);
                byte[] sizeBytes4 = BitConverter.GetBytes(FirstSize + 4);
                test = MainFunctions.b_ReplaceBytes(test, sizeBytes3, newLastEffectPos - 4, 1);
                test = MainFunctions.b_ReplaceBytes(test, sizeBytes4, newLastEffectPos - 16, 1);

                newBytes = test;

                // COLLISION

                if (collisionChanged) {
                    test = new byte[0];
                    FirstSize = 0;
                    byte[] CollisionSections = new byte[0];
                    for (int z = 0; z < collisionSecCount; z++) {
                        byte[] NewCollisionSection = new byte[0x5C];

                        byte[] CollisionType = new byte[1]
                        {
                            (byte)collisionSecTypeValue[z]
                        };
                        NewCollisionSection = MainFunctions.b_ReplaceBytes(NewCollisionSection, CollisionType, 0x00);

                        byte[] CollisionState = new byte[1]
                        {
                            (byte)collisionSecStateValue[z]
                        };
                        NewCollisionSection = MainFunctions.b_ReplaceBytes(NewCollisionSection, CollisionState, 0x04);

                        byte[] CollisionBoneEnabler = new byte[1]
                        {
                            (byte)collisionSecEnablerBoneValue[z]
                        };
                        NewCollisionSection = MainFunctions.b_ReplaceBytes(NewCollisionSection, CollisionBoneEnabler, 0x08);

                        byte[] CollisionOrder = new byte[1]
                        {
                            (byte)(z+1)
                        };
                        NewCollisionSection = MainFunctions.b_ReplaceBytes(NewCollisionSection, CollisionOrder, 0x0C);

                        if (collisionSecEnablerBoneValue[z] == 0) {
                            byte[] CollisionSectionHitbox = new byte[0];
                            CollisionSectionHitbox = MainFunctions.b_AddBytes(CollisionSectionHitbox, Encoding.ASCII.GetBytes(collisionSecBoneName[z]));
                            NewCollisionSection = MainFunctions.b_ReplaceBytes(NewCollisionSection, CollisionSectionHitbox, 0x10);
                        }
                        byte[] CollisionRadius = BitConverter.GetBytes(collisionSecRadiusValue[z]);
                        for (int k = 0; k < 4; k++) {
                            byte[] CollisionRadiusByte = new byte[1]
                            {
                                (byte)CollisionRadius[k]
                            };
                            NewCollisionSection = MainFunctions.b_ReplaceBytes(NewCollisionSection, CollisionRadiusByte, 0x50 + k);
                        }
                        byte[] CollisionYPos = BitConverter.GetBytes(collisionSecYPosValue[z]);
                        for (int k = 0; k < 4; k++) {
                            byte[] CollisionYPosByte = new byte[1]
                            {
                                (byte)CollisionYPos[k]
                            };
                            NewCollisionSection = MainFunctions.b_ReplaceBytes(NewCollisionSection, CollisionYPosByte, 0x54 + k);
                        }
                        byte[] CollisionZPos = BitConverter.GetBytes(collisionSecZPosValue[z]);
                        for (int k = 0; k < 4; k++) {
                            byte[] CollisionYPosByte = new byte[1]
                            {
                                (byte)CollisionZPos[k]
                            };
                            NewCollisionSection = MainFunctions.b_ReplaceBytes(NewCollisionSection, CollisionYPosByte, 0x58 + k);
                        }


                        CollisionSections = MainFunctions.b_AddBytes(CollisionSections, NewCollisionSection);
                        FirstSize = FirstSize + 0x5C;
                    }
                    //Collision data
                    byte[] CollisionEntry = new byte[8]
                    {
                        0x00,
                        0x00,
                        0x00,
                        0x03,
                        0x00,
                        0x63,
                        0x00,
                        0x00
                    };
                    int newCollisionPos = XfbinParser.FindBytes(newBytes, CollisionEntry, 0);
                    if (newCollisionPos == -1) {
                        CollisionEntry = new byte[6]
                        {
                        0x00,
                        0x00,
                        0x00,
                        0x03,
                        0x00,
                        0x63
                        };
                        newCollisionPos = XfbinParser.FindBytes(newBytes, CollisionEntry, 0);
                    }
                    if (newCollisionPos == -1) {
                        CollisionEntry = new byte[6]
                        {
                        0x00,
                        0x00,
                        0x00,
                        0x03,
                        0x00,
                        0x79
                        };
                        newCollisionPos = XfbinParser.FindBytes(newBytes, CollisionEntry, 0);
                    }
                    if (newCollisionPos == -1) {
                        CollisionEntry = new byte[6]
                        {
                        0x00,
                        0x00,
                        0x00,
                        0x03,
                        0x00,
                        0x7A
                        };
                        newCollisionPos = XfbinParser.FindBytes(newBytes, CollisionEntry, 0);
                    }
                    int newLastCollisionPos = newCollisionPos + 16;
                    int newCollisionSecLength = MainFunctions.b_ReadIntRev(newBytes, newLastCollisionPos - 8) - 4;
                    List<byte> ChangedCollisionSection = new List<byte>();
                    for (int j = 0; j < newBytes.Length; j++) {
                        ChangedCollisionSection.Add(newBytes[j]);
                    }
                    for (int j = newLastCollisionPos; j < newLastCollisionPos + newCollisionSecLength; j++) {
                        ChangedCollisionSection.RemoveAt(newLastCollisionPos);
                    }
                    for (int j = 0; j < CollisionSections.Length; j++) {
                        ChangedCollisionSection.Insert(newLastCollisionPos + j, CollisionSections[j]);
                    }
                    test = new byte[ChangedCollisionSection.Count];
                    for (int j = 0; j < ChangedCollisionSection.Count; j++) {
                        test[j] = ChangedCollisionSection[j];
                    }
                    sizeBytes3 = BitConverter.GetBytes(FirstSize + 4);
                    sizeBytes4 = BitConverter.GetBytes(FirstSize + 8);
                    byte[] sizeBytes5 = BitConverter.GetBytes(collisionSecCount);
                    byte[] reversedCount = new byte[4]
                    {
                        sizeBytes5[3],
                        sizeBytes5[2],
                        sizeBytes5[1],
                        sizeBytes5[0]
                    };
                    test = MainFunctions.b_ReplaceBytes(test, reversedCount, newLastCollisionPos - 4, 1);
                    test = MainFunctions.b_ReplaceBytes(test, sizeBytes3, newLastCollisionPos - 8, 1);
                    test = MainFunctions.b_ReplaceBytes(test, sizeBytes4, newLastCollisionPos - 20, 1);

                    newBytes = test;
                }
            }
            return newBytes;
        }
        public void SaveFileAs(string basepath = "") {
            if (fileOpen) {
                SaveFileDialog s = new SaveFileDialog();
                {
                    s.DefaultExt = ".xfbin";
                    s.Filter = "*.xfbin|*.xfbin";
                }
                if (basepath != "")
                    s.FileName = basepath;
                else
                    s.ShowDialog();

                if (s.FileName != "") {
                    filePath = s.FileName;
                    File.WriteAllBytes(filePath, GenerateFile());
                    if (basepath == "")
                        MessageBox.Show("File saved to " + filePath);
                }
            } else {
                MessageBox.Show("You need to open file first!");
            }
        }
    }
}
