using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_stageInfoEditor_code {
        public byte[] fileBytes = new byte[0];
        public byte[] header = new byte[0];
        public bool FileOpen = false;
        public string FilePath = "";
        public int EntryCount = 0;
        public List<byte[]> MainStageSection = new List<byte[]>();
        public int FilePos = 0;
        public List<string> StageNameList = new List<string>();
        public List<string> c_sta_List = new List<string>();
        public List<string> BTL_NSX_List = new List<string>();
        public List<int> CountOfFiles = new List<int>();
        public List<int> CountOfMeshes = new List<int>();
        public List<int> MainSection_WeatherSettings = new List<int>();
        public List<int> MainSection_lensFlareSettings = new List<int>();
        public List<int> MainSection_EnablelensFlareSettings = new List<int>();
        public List<float> MainSection_X_PositionLightPoint = new List<float>();
        public List<float> MainSection_Y_PositionLightPoint = new List<float>();
        public List<float> MainSection_Z_PositionLightPoint = new List<float>();
        public List<float> MainSection_X_PositionShadow = new List<float>();
        public List<float> MainSection_Y_PositionShadow = new List<float>();
        public List<float> MainSection_Z_PositionShadow = new List<float>();
        public List<float> MainSection_unk1 = new List<float>();
        public List<int> MainSection_ShadowSetting_value1 = new List<int>();
        public List<int> MainSection_ShadowSetting_value2 = new List<int>();
        public List<float> MainSection_PowerLight = new List<float>();
        public List<float> MainSection_PowerSkyColor = new List<float>();
        public List<float> MainSection_PowerGlare = new List<float>();
        public List<float> MainSection_blur = new List<float>();
        public List<float> MainSection_X_PositionGlarePoint = new List<float>();
        public List<float> MainSection_Y_PositionGlarePoint = new List<float>();
        public List<float> MainSection_Z_PositionGlarePoint = new List<float>();
        public List<float> MainSectionGlareVagueness = new List<float>();
        public List<byte[]> MainSection_ColorGlare = new List<byte[]>();
        public List<byte[]> MainSection_ColorSky = new List<byte[]>();
        public List<byte[]> MainSection_ColorRock = new List<byte[]>();
        public List<byte[]> MainSection_ColorGroundEffect = new List<byte[]>();
        public List<byte[]> MainSection_ColorPlayerLight = new List<byte[]>();
        public List<byte[]> MainSection_ColorLight = new List<byte[]>();
        public List<byte[]> MainSection_ColorShadow = new List<byte[]>();
        public List<byte[]> MainSection_ColorUnknown = new List<byte[]>();
        public List<byte[]> MainSection_ColorUnknown2 = new List<byte[]>();
        public List<int> MainSection_EnableGlareSettingValue1 = new List<int>();
        public List<int> MainSection_EnableGlareSettingValue2 = new List<int>();
        public List<int> MainSection_EnableGlareSettingValue3 = new List<int>();
        public List<bool> GlareEnabled = new List<bool>();
        public List<float> MainSection_X_MysteriousPosition = new List<float>();
        public List<float> MainSection_Y_MysteriousPosition = new List<float>();
        public List<float> MainSection_Z_MysteriousPosition = new List<float>();
        public List<float> MainSection_MysteriousGlareValue1 = new List<float>();
        public List<float> MainSection_MysteriousGlareValue2 = new List<float>();
        public List<float> MainSection_MysteriousGlareValue3 = new List<float>();
        public List<float> MainSection_UnknownValue1 = new List<float>();
        public List<float> MainSection_UnknownValue2 = new List<float>();
        public List<float> MainSection_UnknownValue3 = new List<float>();
        public List<List<byte[]>> SecondaryStageSection = new List<List<byte[]>>();
        public List<List<string>> SecondarySectionFilePath = new List<List<string>>();
        public List<int> One_SecondarySectionFilePath = new List<int>();
        public List<int> One_SecondarySectionLoadPath = new List<int>();
        public List<int> One_SecondarySectionCameraValue = new List<int>();
        public List<int> One_SecondarySectionMysteriousValue = new List<int>();
        public List<string> One_SecondarySectionFilePathString = new List<string>();
        public List<string> One_SecondarySectionLoadPathString = new List<string>();
        public List<string> One_SecondarySectionLoadMeshString = new List<string>();
        public List<string> One_SecondarySectionLoadPathDmyString = new List<string>();
        public List<string> One_SecondarySectionLoadDmyString = new List<string>();
        public List<string> One_SecondaryTypeBreakableWall_Effect01 = new List<string>();
        public List<string> One_SecondaryTypeBreakableWall_Effect02 = new List<string>();
        public List<string> One_SecondaryTypeBreakableWall_Effect03 = new List<string>();
        public List<string> One_SecondaryTypeBreakableWall_Sound = new List<string>();
        public List<string> One_SecondaryTypeBreakableObject_Effect01 = new List<string>();
        public List<string> One_SecondaryTypeBreakableObject_Effect02 = new List<string>();
        public List<string> One_SecondaryTypeBreakableObject_Effect03 = new List<string>();
        public List<string> One_SecondaryTypeBreakableObject_path = new List<string>();
        public List<int> One_SecondaryTypeSection = new List<int>();
        public List<int> One_SecondaryConst3C = new List<int>();
        public List<int> One_SecondaryConst78 = new List<int>();
        public List<int> One_SecondaryConstBreakableWallValue1 = new List<int>();
        public List<int> One_SecondaryConstBreakableWallValue2 = new List<int>();
        public List<float> One_SecondaryTypeAnimationSection_speed = new List<float>();
        public List<List<string>> SecondarySectionLoadPath = new List<List<string>>();
        public List<List<string>> SecondarySectionLoadMesh = new List<List<string>>();
        public List<List<string>> SecondarySectionPositionFilePath = new List<List<string>>();
        public List<List<string>> SecondarySectionPosition = new List<List<string>>();
        public List<List<int>> SecondaryTypeSection = new List<List<int>>();
        public List<List<float>> SecondaryTypeAnimationSection_speed = new List<List<float>>();
        public List<List<int>> SecondarySectionCameraValue = new List<List<int>>();
        public List<List<int>> SecondarySectionMysteriousValue = new List<List<int>>();
        public List<List<int>> SecondaryConst3C = new List<List<int>>();
        public List<List<int>> SecondaryConst78 = new List<List<int>>();
        public List<List<int>> SecondaryConstBreakableWallValue1 = new List<List<int>>();
        public List<List<int>> SecondaryConstBreakableWallValue2 = new List<List<int>>();
        public List<List<string>> SecondaryTypeBreakableWall_Effect01 = new List<List<string>>();
        public List<List<string>> SecondaryTypeBreakableWall_Effect02 = new List<List<string>>();
        public List<List<string>> SecondaryTypeBreakableWall_Effect03 = new List<List<string>>();
        public List<List<string>> SecondaryTypeBreakableWall_Sound = new List<List<string>>();
        public List<List<string>> SecondaryTypeBreakableObject_Effect01 = new List<List<string>>();
        public List<List<string>> SecondaryTypeBreakableObject_Effect02 = new List<List<string>>();
        public List<List<string>> SecondaryTypeBreakableObject_Effect03 = new List<List<string>>();
        public List<List<string>> SecondaryTypeBreakableObject_path = new List<List<string>>();
        public List<float> One_SecondaryTypeBreakableObject_Speed01 = new List<float>();
        public List<float> One_SecondaryTypeBreakableObject_Speed02 = new List<float>();
        public List<float> One_SecondaryTypeBreakableObject_Speed03 = new List<float>();
        public List<float> One_SecondaryTypeBreakableWall_volume = new List<float>();
        public List<List<float>> SecondaryTypeBreakableObject_Speed01 = new List<List<float>>();
        public List<List<float>> SecondaryTypeBreakableObject_Speed02 = new List<List<float>>();
        public List<List<float>> SecondaryTypeBreakableObject_Speed03 = new List<List<float>>();
        public List<List<float>> SecondaryTypeBreakableWall_volume = new List<List<float>>();

        public float copied_MainSection_X_MysteriousPosition = 0;
        public float copied_MainSection_Y_MysteriousPosition = 0;
        public float copied_MainSection_Z_MysteriousPosition = 0;
        public float copied_MainSection_X_PositionGlarePoint = 0;
        public float copied_MainSection_Y_PositionGlarePoint = 0;
        public float copied_MainSection_Z_PositionGlarePoint = 0;
        public float copied_MainSection_X_PositionLightPoint = 0;
        public float copied_MainSection_Y_PositionLightPoint = 0;
        public float copied_MainSection_Z_PositionLightPoint = 0;
        public float copied_MainSection_X_PositionShadow = 0;
        public float copied_MainSection_Y_PositionShadow = 0;
        public float copied_MainSection_Z_PositionShadow = 0;
        public float copied_MainSection_PowerGlare = 0;
        public float copied_MainSection_blur = 0;
        public float copied_MainSection_unk1 = 0;
        public float copied_MainSection_PowerLight = 0;
        public float copied_MainSectionGlareVagueness = 0;
        public float copied_MainSection_MysteriousGlareValue1 = 0;
        public float copied_MainSection_MysteriousGlareValue2 = 0;
        public float copied_MainSection_MysteriousGlareValue3 = 0;
        public float copied_MainSection_UnknownValue1 = 0;
        public float copied_MainSection_UnknownValue2 = 0;
        public float copied_MainSection_UnknownValue3 = 0;
        public float copied_MainSection_PowerSkyColor = 0;
        public byte[] copied_MainSection_ColorGlare = new byte[4] { 00, 00, 00, 00 };
        public byte[] copied_MainSection_ColorSky = new byte[4] { 00, 00, 00, 00 };
        public byte[] copied_MainSection_ColorRock = new byte[4] { 00, 00, 00, 00 };
        public byte[] copied_MainSection_ColorGroundEffect = new byte[4] { 00, 00, 00, 00 };
        public byte[] copied_MainSection_ColorPlayerLight = new byte[4] { 00, 00, 00, 00 };
        public byte[] copied_MainSection_ColorLight = new byte[4] { 00, 00, 00, 00 };
        public byte[] copied_MainSection_ColorShadow = new byte[4] { 00, 00, 00, 00 };
        public byte[] copied_MainSection_ColorUnknown = new byte[4] { 00, 00, 00, 00 };
        public byte[] copied_MainSection_ColorUnknown2 = new byte[4] { 00, 00, 00, 00 };
        public int copied_MainSection_WeatherSettings = 0;
        public int copied_MainSection_lensFlareSettings = 0;
        public int copied_MainSection_EnablelensFlareSettings = 0;
        public int copied_MainSection_EnableGlareSettingValue1 = 0;
        public int copied_MainSection_EnableGlareSettingValue2 = 0;
        public int copied_MainSection_EnableGlareSettingValue3 = 0;
        public int copied_MainSection_ShadowSetting_value1 = 0;
        public int copied_MainSection_ShadowSetting_value2 = 0;
        public bool copied_settings = false;
        public void ClearFile() {
            FileOpen = false;
            FilePath = "";
            EntryCount = 0;
            MainStageSection = new List<byte[]>();
            FilePos = 0;
            StageNameList = new List<string>();
            c_sta_List = new List<string>();
            BTL_NSX_List = new List<string>();
            CountOfFiles = new List<int>();
            CountOfMeshes = new List<int>();
            MainSection_ColorUnknown = new List<byte[]>();
            MainSection_unk1 = new List<float>();
            MainSection_ColorUnknown2 = new List<byte[]>();
            MainSection_ColorSky = new List<byte[]>();
            MainSection_WeatherSettings = new List<int>();
            MainSection_lensFlareSettings = new List<int>();
            copied_MainSection_ShadowSetting_value1 = 0;
            copied_MainSection_ShadowSetting_value2 = 0;
            MainSection_EnablelensFlareSettings = new List<int>();
            MainSection_ColorGroundEffect = new List<byte[]>();
            MainSection_ColorPlayerLight = new List<byte[]>();
            MainSection_X_PositionLightPoint = new List<float>();
            MainSection_Y_PositionLightPoint = new List<float>();
            MainSection_Z_PositionLightPoint = new List<float>();
            MainSection_ColorLight = new List<byte[]>();
            MainSection_X_PositionShadow = new List<float>();
            MainSection_Y_PositionShadow = new List<float>();
            MainSection_Z_PositionShadow = new List<float>();
            MainSection_ShadowSetting_value1 = new List<int>();
            MainSection_ColorShadow = new List<byte[]>();
            MainSection_ShadowSetting_value2 = new List<int>();
            MainSection_PowerLight = new List<float>();
            MainSection_PowerGlare = new List<float>();
            MainSection_blur = new List<float>();
            MainSection_X_PositionGlarePoint = new List<float>();
            MainSection_Y_PositionGlarePoint = new List<float>();
            MainSection_Z_PositionGlarePoint = new List<float>();
            MainSectionGlareVagueness = new List<float>();
            MainSection_ColorGlare = new List<byte[]>();
            MainSection_ColorRock = new List<byte[]>();
            MainSection_EnableGlareSettingValue1 = new List<int>();
            MainSection_EnableGlareSettingValue2 = new List<int>();
            MainSection_EnableGlareSettingValue3 = new List<int>();
            GlareEnabled = new List<bool>();
            MainSection_X_MysteriousPosition = new List<float>();
            MainSection_Y_MysteriousPosition = new List<float>();
            MainSection_Z_MysteriousPosition = new List<float>();
            MainSection_MysteriousGlareValue1 = new List<float>();
            MainSection_MysteriousGlareValue2 = new List<float>();
            MainSection_MysteriousGlareValue3 = new List<float>();
            MainSection_UnknownValue1 = new List<float>();
            MainSection_UnknownValue2 = new List<float>();
            MainSection_UnknownValue3 = new List<float>();
            SecondaryStageSection = new List<List<byte[]>>();
            SecondarySectionFilePath = new List<List<string>>();
            One_SecondarySectionFilePath = new List<int>();
            One_SecondarySectionLoadPath = new List<int>();
            One_SecondarySectionCameraValue = new List<int>();
            One_SecondarySectionMysteriousValue = new List<int>();
            One_SecondarySectionFilePathString = new List<string>();
            One_SecondarySectionLoadPathString = new List<string>();
            One_SecondarySectionLoadMeshString = new List<string>();
            One_SecondarySectionLoadPathDmyString = new List<string>();
            One_SecondarySectionLoadDmyString = new List<string>();
            One_SecondaryTypeBreakableWall_Effect01 = new List<string>();
            One_SecondaryTypeBreakableWall_Effect02 = new List<string>();
            One_SecondaryTypeBreakableWall_Effect03 = new List<string>();
            One_SecondaryTypeBreakableWall_Sound = new List<string>();
            One_SecondaryTypeBreakableObject_Effect01 = new List<string>();
            One_SecondaryTypeBreakableObject_Effect02 = new List<string>();
            One_SecondaryTypeBreakableObject_Effect03 = new List<string>();
            One_SecondaryTypeBreakableObject_path = new List<string>();
            MainSection_PowerSkyColor = new List<float>();
            One_SecondaryTypeSection = new List<int>();
            One_SecondaryConst3C = new List<int>();
            One_SecondaryConst78 = new List<int>();
            One_SecondaryConstBreakableWallValue1 = new List<int>();
            One_SecondaryConstBreakableWallValue2 = new List<int>();
            One_SecondaryTypeAnimationSection_speed = new List<float>();
            SecondarySectionLoadPath = new List<List<string>>();
            SecondarySectionLoadMesh = new List<List<string>>();
            SecondarySectionPositionFilePath = new List<List<string>>();
            SecondarySectionPosition = new List<List<string>>();
            SecondaryTypeSection = new List<List<int>>();
            SecondaryTypeAnimationSection_speed = new List<List<float>>();
            SecondarySectionCameraValue = new List<List<int>>();
            SecondarySectionMysteriousValue = new List<List<int>>();
            SecondaryConst3C = new List<List<int>>();
            SecondaryConst78 = new List<List<int>>();
            SecondaryConstBreakableWallValue1 = new List<List<int>>();
            SecondaryConstBreakableWallValue2 = new List<List<int>>();
            SecondaryTypeBreakableWall_Effect01 = new List<List<string>>();
            SecondaryTypeBreakableWall_Effect02 = new List<List<string>>();
            SecondaryTypeBreakableWall_Effect03 = new List<List<string>>();
            SecondaryTypeBreakableWall_Sound = new List<List<string>>();
            SecondaryTypeBreakableObject_Effect01 = new List<List<string>>();
            SecondaryTypeBreakableObject_Effect02 = new List<List<string>>();
            SecondaryTypeBreakableObject_Effect03 = new List<List<string>>();
            SecondaryTypeBreakableObject_path = new List<List<string>>();
            One_SecondaryTypeBreakableObject_Speed01 = new List<float>();
            One_SecondaryTypeBreakableObject_Speed02 = new List<float>();
            One_SecondaryTypeBreakableObject_Speed03 = new List<float>();
            One_SecondaryTypeBreakableWall_volume = new List<float>();
            SecondaryTypeBreakableObject_Speed01 = new List<List<float>>();
            SecondaryTypeBreakableObject_Speed02 = new List<List<float>>();
            SecondaryTypeBreakableObject_Speed03 = new List<List<float>>();
            SecondaryTypeBreakableWall_volume = new List<List<float>>();
            copied_MainSection_X_MysteriousPosition = 0;
            copied_MainSection_Y_MysteriousPosition = 0;
            copied_MainSection_Z_MysteriousPosition = 0;
            copied_MainSection_X_PositionGlarePoint = 0;
            copied_MainSection_Y_PositionGlarePoint = 0;
            copied_MainSection_Z_PositionGlarePoint = 0;
            copied_MainSection_X_PositionLightPoint = 0;
            copied_MainSection_Y_PositionLightPoint = 0;
            copied_MainSection_Z_PositionLightPoint = 0;
            copied_MainSection_X_PositionShadow = 0;
            copied_MainSection_Y_PositionShadow = 0;
            copied_MainSection_Z_PositionShadow = 0;
            copied_MainSection_PowerGlare = 0;
            copied_MainSection_blur = 0;
            copied_MainSection_PowerLight = 0;
            copied_MainSectionGlareVagueness = 0;
            copied_MainSection_MysteriousGlareValue1 = 0;
            copied_MainSection_MysteriousGlareValue2 = 0;
            copied_MainSection_PowerSkyColor = 0;
            copied_MainSection_ColorGlare = new byte[4] { 00, 00, 00, 00 };
            copied_MainSection_ColorSky = new byte[4] { 00, 00, 00, 00 };
            copied_MainSection_ColorRock = new byte[4] { 00, 00, 00, 00 };
            copied_MainSection_ColorGroundEffect = new byte[4] { 00, 00, 00, 00 };
            copied_MainSection_ColorPlayerLight = new byte[4] { 00, 00, 00, 00 };
            copied_MainSection_ColorLight = new byte[4] { 00, 00, 00, 00 };
            copied_MainSection_ColorShadow = new byte[4] { 00, 00, 00, 00 };
            copied_MainSection_ColorUnknown = new byte[4] { 00, 00, 00, 00 };
            copied_MainSection_WeatherSettings = 0;
            copied_MainSection_lensFlareSettings = 0;
            copied_MainSection_EnablelensFlareSettings = 0;
            copied_MainSection_EnableGlareSettingValue1 = 0;
            copied_MainSection_EnableGlareSettingValue2 = 0;
            copied_MainSection_EnableGlareSettingValue3 = 0;
            copied_MainSection_unk1 = 0;
            copied_settings = false;
            fileBytes = new byte[0];
            header = new byte[0];
        }
        public void OpenFile(string FileName = "") {
            if (FileName == "") {
                OpenFileDialog o = new OpenFileDialog();
                {
                    o.DefaultExt = ".xfbin";
                    o.Filter = "*.xfbin|*.xfbin";
                }
                o.ShowDialog();
                FileName = o.FileName;
            }
            ClearFile();
            if (FileName == "" || File.Exists(FileName) == false) return;
            fileBytes = File.ReadAllBytes(FileName);
            FileOpen = true;
            FilePath = FileName;
            FilePos = MainFunctions.b_FindBytes(fileBytes, new byte[4] { 0xF2, 0x03, 0x00, 0x00 }, 0);
            header = MainFunctions.b_ReadByteArray(fileBytes, 0, FilePos + 16);
            EntryCount = fileBytes[FilePos + 4] + fileBytes[FilePos + 5] * 256 + fileBytes[FilePos + 6] * 65536 + fileBytes[FilePos + 7] * 16777216;
            for (int x2 = 0; x2 < EntryCount; x2++) {
                long _ptr = FilePos + 0x10 + 0x130 * x2;
                string STAGE_NAME = "";
                MainStageSection.Add(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr, 0x130));
                long _ptrCharacter3 = fileBytes[_ptr] + fileBytes[_ptr + 1] * 0x100 + fileBytes[_ptr + 2] * 0x10000 + fileBytes[_ptr + 3] * 0x1000000;
                for (int a2 = 0; a2 < 40; a2++) {
                    if (fileBytes[_ptr + _ptrCharacter3 + a2] != 0) {
                        string str2 = STAGE_NAME;
                        char c = (char)fileBytes[_ptr + _ptrCharacter3 + a2];
                        STAGE_NAME = str2 + c;
                    } else {
                        a2 = 40;
                    }
                }
                string c_sta_x = "";
                _ptrCharacter3 = fileBytes[_ptr + 8] + fileBytes[_ptr + 9] * 0x100 + fileBytes[_ptr + 10] * 0x10000 + fileBytes[_ptr + 11] * 0x1000000;
                for (int a2 = 0; a2 < 16; a2++) {
                    if (fileBytes[_ptr + 8 + _ptrCharacter3 + a2] != 0) {
                        string str2 = c_sta_x;
                        char c = (char)fileBytes[_ptr + 8 + _ptrCharacter3 + a2];
                        c_sta_x = str2 + c;
                    } else {
                        a2 = 16;
                    }
                }
                string BTL_NSX_XXXXX = "";
                _ptrCharacter3 = fileBytes[_ptr + 16] + fileBytes[_ptr + 17] * 0x100 + fileBytes[_ptr + 18] * 0x10000 + fileBytes[_ptr + 19] * 0x1000000;
                for (int a2 = 0; a2 < 16; a2++) {
                    if (fileBytes[_ptr + 16 + _ptrCharacter3 + a2] != 0) {
                        string str2 = BTL_NSX_XXXXX;
                        char c = (char)fileBytes[_ptr + 16 + _ptrCharacter3 + a2];
                        BTL_NSX_XXXXX = str2 + c;
                    } else {
                        a2 = 40;
                    }
                }
                int CountFile = fileBytes[_ptr + 24] + fileBytes[_ptr + 25] * 0x100 + fileBytes[_ptr + 26] * 0x10000 + fileBytes[_ptr + 27] * 0x1000000;
                int CountEntries = fileBytes[_ptr + 40] + fileBytes[_ptr + 41] * 0x100 + fileBytes[_ptr + 42] * 0x10000 + fileBytes[_ptr + 43] * 0x1000000;
                int PosPaths = fileBytes[_ptr + 32] + fileBytes[_ptr + 33] * 0x100 + fileBytes[_ptr + 34] * 0x10000 + fileBytes[_ptr + 35] * 0x1000000;
                int PosMeshes = fileBytes[_ptr + 48] + fileBytes[_ptr + 49] * 0x100 + fileBytes[_ptr + 50] * 0x10000 + fileBytes[_ptr + 51] * 0x1000000;
                long _ptrPosPath = FilePos + 0x10 + 32 + (0x130 * x2);
                long _ptrPosMesh = FilePos + 0x10 + 48 + (0x130 * x2);
                CountOfFiles.Add(CountFile);
                CountOfMeshes.Add(CountEntries);
                MainSection_WeatherSettings.Add(fileBytes[_ptr + 56]);
                MainSection_EnablelensFlareSettings.Add(fileBytes[_ptr + 88]);
                MainSection_lensFlareSettings.Add(fileBytes[_ptr + 92]);
                MainSection_ShadowSetting_value1.Add(fileBytes[_ptr + 132]);
                MainSection_ShadowSetting_value2.Add(fileBytes[_ptr + 140]);
                MainSection_ColorGroundEffect.Add(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 60, 4));
                MainSection_ColorUnknown2.Add(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 64, 4));
                MainSection_ColorPlayerLight.Add(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 68, 4));
                MainSection_ColorLight.Add(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 112, 4));
                MainSection_ColorShadow.Add(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 136, 4));
                MainSection_ColorGlare.Add(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 244, 4));
                MainSection_ColorRock.Add(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 268, 4));
                MainSection_ColorSky.Add(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 156, 4));
                MainSection_ColorUnknown.Add(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 116, 4));
                MainSection_PowerSkyColor.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 152, 4), 0));
                MainSection_X_PositionLightPoint.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 100, 4), 0));
                MainSection_unk1.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 144, 4), 0));
                MainSection_Y_PositionLightPoint.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 104, 4), 0));
                MainSection_Z_PositionLightPoint.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 108, 4), 0));
                MainSection_X_PositionShadow.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 120, 4), 0));
                MainSection_Y_PositionShadow.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 124, 4), 0));
                MainSection_Z_PositionShadow.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 128, 4), 0));
                MainSection_X_PositionGlarePoint.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 248, 4), 0));
                MainSection_Y_PositionGlarePoint.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 252, 4), 0));
                MainSection_Z_PositionGlarePoint.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 256, 4), 0));
                MainSection_PowerLight.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 148, 4), 0));
                MainSection_PowerGlare.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 240, 4), 0));
                MainSection_blur.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 200, 4), 0));
                MainSection_EnableGlareSettingValue1.Add(fileBytes[_ptr + 204]);
                MainSection_EnableGlareSettingValue2.Add(fileBytes[_ptr + 224]);
                MainSection_EnableGlareSettingValue3.Add(fileBytes[_ptr + 228]);
                if (fileBytes[_ptr + 228] == 01) {
                    GlareEnabled.Add(true);
                } else {
                    GlareEnabled.Add(false);
                }
                MainSection_X_MysteriousPosition.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 212, 4), 0));
                MainSection_Y_MysteriousPosition.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 216, 4), 0));
                MainSection_Z_MysteriousPosition.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 220, 4), 0));
                MainSection_MysteriousGlareValue1.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 236, 4), 0));
                MainSection_MysteriousGlareValue2.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 264, 4), 0));
                MainSection_MysteriousGlareValue3.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 232, 4), 0));
                MainSection_UnknownValue1.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 164, 4), 0));
                MainSection_UnknownValue2.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 168, 4), 0));
                MainSection_UnknownValue3.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 172, 4), 0));
                MainSectionGlareVagueness.Add(MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptr + 260, 4), 0));

                for (int x3 = 0; x3 < CountFile; x3++) {
                    int _ptrPosPath_extra = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosPath + PosPaths, 4));
                    One_SecondarySectionFilePathString.Add(MainFunctions.b_ReadString(fileBytes, (int)_ptrPosPath + PosPaths + _ptrPosPath_extra, -1));
                    _ptrPosPath = _ptrPosPath + 8;
                }
                for (int x3 = 0; x3 < CountEntries; x3++) {
                    int _ptrPosLoadPath_extra = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes, 4));
                    int _ptrPosLoadMesh_extra = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 8, 4));
                    int _ptrPosLoadPathdmy_extra = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 16, 4));
                    int _ptrPosLoadDmy_extra = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 24, 4));
                    int _ptrPosType_extra = fileBytes[(int)_ptrPosMesh + PosMeshes + 32];
                    int _ptrPosCameraValue_extra = fileBytes[(int)_ptrPosMesh + PosMeshes + 40];
                    int _ptrPosMysteriousValue_extra = fileBytes[(int)_ptrPosMesh + PosMeshes + 44];
                    int _ptrPosConstValue3C = fileBytes[(int)_ptrPosMesh + PosMeshes + 112];
                    int _ptrPosConstValue78 = fileBytes[(int)_ptrPosMesh + PosMeshes + 116];
                    int _ptrPosConstBreakableWallValue1 = fileBytes[(int)_ptrPosMesh + PosMeshes + 128];
                    int _ptrPosConstBreakableWallValue2 = fileBytes[(int)_ptrPosMesh + PosMeshes + 132];
                    float _ptrPosAnimationSpeed_extra = MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 36, 4), 0);
                    int _ptrPosBreakableEffect01 = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 120, 4));
                    int _ptrPosBreakableEffect02 = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 136, 4));
                    int _ptrPosBreakableEffect03 = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 144, 4));
                    int _ptrPosBreakableWallSound = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 160, 4));
                    int _ptrPosBreakableObjectPath = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 56, 4));
                    int _ptrPosBreakableObjectEffect01 = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 64, 4));
                    int _ptrPosBreakableObjectEffect02 = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 80, 4));
                    int _ptrPosBreakableObjectEffect03 = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 96, 4));
                    float _ptrPosAnimationBreakableObject_Speed01_extra = MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 72, 4), 0);
                    float _ptrPosAnimationBreakableObject_Speed02_extra = MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 88, 4), 0);
                    float _ptrPosAnimationBreakableObject_Speed03_extra = MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 104, 4), 0);
                    float _ptrPosAnimationBreakableWall_volume_extra = MainFunctions.b_ReadFloat(MainFunctions.b_ReadByteArray(fileBytes, (int)_ptrPosMesh + PosMeshes + 152, 4), 0);
                    One_SecondarySectionLoadPathString.Add(MainFunctions.b_ReadString(fileBytes, (int)_ptrPosMesh + PosMeshes + _ptrPosLoadPath_extra, -1));
                    One_SecondarySectionLoadMeshString.Add(MainFunctions.b_ReadString(fileBytes, (int)_ptrPosMesh + PosMeshes + _ptrPosLoadMesh_extra + 8, -1));
                    One_SecondarySectionLoadPathDmyString.Add(MainFunctions.b_ReadString(fileBytes, (int)_ptrPosMesh + PosMeshes + _ptrPosLoadPathdmy_extra + 16, -1));
                    One_SecondarySectionLoadDmyString.Add(MainFunctions.b_ReadString(fileBytes, (int)_ptrPosMesh + PosMeshes + _ptrPosLoadDmy_extra + 24, -1));
                    One_SecondaryTypeSection.Add(_ptrPosType_extra);
                    One_SecondarySectionCameraValue.Add(_ptrPosCameraValue_extra);
                    One_SecondarySectionMysteriousValue.Add(_ptrPosMysteriousValue_extra);
                    One_SecondaryTypeAnimationSection_speed.Add(_ptrPosAnimationSpeed_extra);
                    One_SecondaryConst3C.Add(_ptrPosConstValue3C);
                    One_SecondaryConst78.Add(_ptrPosConstValue78);
                    One_SecondaryConstBreakableWallValue1.Add(_ptrPosConstBreakableWallValue1);
                    One_SecondaryConstBreakableWallValue2.Add(_ptrPosConstBreakableWallValue2);
                    One_SecondaryTypeBreakableWall_Effect01.Add(MainFunctions.b_ReadString(fileBytes, (int)_ptrPosMesh + PosMeshes + _ptrPosBreakableEffect01 + 120, -1));
                    One_SecondaryTypeBreakableWall_Effect02.Add(MainFunctions.b_ReadString(fileBytes, (int)_ptrPosMesh + PosMeshes + _ptrPosBreakableEffect02 + 136, -1));
                    One_SecondaryTypeBreakableWall_Effect03.Add(MainFunctions.b_ReadString(fileBytes, (int)_ptrPosMesh + PosMeshes + _ptrPosBreakableEffect03 + 144, -1));
                    One_SecondaryTypeBreakableWall_Sound.Add(MainFunctions.b_ReadString(fileBytes, (int)_ptrPosMesh + PosMeshes + _ptrPosBreakableWallSound + 160, -1));
                    One_SecondaryTypeBreakableObject_path.Add(MainFunctions.b_ReadString(fileBytes, (int)_ptrPosMesh + PosMeshes + _ptrPosBreakableObjectPath + 56, -1));
                    One_SecondaryTypeBreakableObject_Effect01.Add(MainFunctions.b_ReadString(fileBytes, (int)_ptrPosMesh + PosMeshes + _ptrPosBreakableObjectEffect01 + 64, -1));
                    One_SecondaryTypeBreakableObject_Effect02.Add(MainFunctions.b_ReadString(fileBytes, (int)_ptrPosMesh + PosMeshes + _ptrPosBreakableObjectEffect02 + 80, -1));
                    One_SecondaryTypeBreakableObject_Effect03.Add(MainFunctions.b_ReadString(fileBytes, (int)_ptrPosMesh + PosMeshes + _ptrPosBreakableObjectEffect03 + 96, -1));
                    One_SecondaryTypeBreakableObject_Speed01.Add(_ptrPosAnimationBreakableObject_Speed01_extra);
                    One_SecondaryTypeBreakableObject_Speed02.Add(_ptrPosAnimationBreakableObject_Speed02_extra);
                    One_SecondaryTypeBreakableObject_Speed03.Add(_ptrPosAnimationBreakableObject_Speed03_extra);
                    One_SecondaryTypeBreakableWall_volume.Add(_ptrPosAnimationBreakableWall_volume_extra);
                    _ptrPosMesh = _ptrPosMesh + 0xB0;
                }
                SecondarySectionFilePath.Add(One_SecondarySectionFilePathString);

                SecondarySectionLoadPath.Add(One_SecondarySectionLoadPathString);
                SecondarySectionLoadMesh.Add(One_SecondarySectionLoadMeshString);
                SecondarySectionPositionFilePath.Add(One_SecondarySectionLoadPathDmyString);
                SecondarySectionPosition.Add(One_SecondarySectionLoadDmyString);
                SecondaryTypeSection.Add(One_SecondaryTypeSection);
                SecondaryTypeAnimationSection_speed.Add(One_SecondaryTypeAnimationSection_speed);
                SecondarySectionCameraValue.Add(One_SecondarySectionCameraValue);
                SecondarySectionMysteriousValue.Add(One_SecondarySectionMysteriousValue);
                SecondaryConst3C.Add(One_SecondaryConst3C);
                SecondaryConst78.Add(One_SecondaryConst78);
                SecondaryConstBreakableWallValue1.Add(One_SecondaryConstBreakableWallValue1);
                SecondaryConstBreakableWallValue2.Add(One_SecondaryConstBreakableWallValue2);
                SecondaryTypeBreakableWall_Effect01.Add(One_SecondaryTypeBreakableWall_Effect01);
                SecondaryTypeBreakableWall_Effect02.Add(One_SecondaryTypeBreakableWall_Effect02);
                SecondaryTypeBreakableWall_Effect03.Add(One_SecondaryTypeBreakableWall_Effect03);
                SecondaryTypeBreakableWall_Sound.Add(One_SecondaryTypeBreakableWall_Sound);
                SecondaryTypeBreakableWall_volume.Add(One_SecondaryTypeBreakableWall_volume);
                SecondaryTypeBreakableObject_path.Add(One_SecondaryTypeBreakableObject_path);
                SecondaryTypeBreakableObject_Effect01.Add(One_SecondaryTypeBreakableObject_Effect01);
                SecondaryTypeBreakableObject_Effect02.Add(One_SecondaryTypeBreakableObject_Effect02);
                SecondaryTypeBreakableObject_Effect03.Add(One_SecondaryTypeBreakableObject_Effect03);
                SecondaryTypeBreakableObject_Speed01.Add(One_SecondaryTypeBreakableObject_Speed01);
                SecondaryTypeBreakableObject_Speed02.Add(One_SecondaryTypeBreakableObject_Speed02);
                SecondaryTypeBreakableObject_Speed03.Add(One_SecondaryTypeBreakableObject_Speed03);



                One_SecondarySectionFilePathString = new List<string>();
                One_SecondarySectionLoadPathString = new List<string>();
                One_SecondarySectionLoadMeshString = new List<string>();
                One_SecondarySectionLoadPathDmyString = new List<string>();
                One_SecondarySectionLoadDmyString = new List<string>();
                One_SecondaryTypeSection = new List<int>();
                One_SecondarySectionCameraValue = new List<int>();
                One_SecondarySectionMysteriousValue = new List<int>();
                One_SecondaryTypeAnimationSection_speed = new List<float>();
                One_SecondaryConst3C = new List<int>();
                One_SecondaryConst78 = new List<int>();
                One_SecondaryConstBreakableWallValue1 = new List<int>();
                One_SecondaryConstBreakableWallValue2 = new List<int>();
                One_SecondaryTypeBreakableWall_Effect01 = new List<string>();
                One_SecondaryTypeBreakableWall_Effect02 = new List<string>();
                One_SecondaryTypeBreakableWall_Effect03 = new List<string>();
                One_SecondaryTypeBreakableWall_Sound = new List<string>();
                One_SecondaryTypeBreakableWall_volume = new List<float>();
                One_SecondaryTypeBreakableObject_path = new List<string>();
                One_SecondaryTypeBreakableObject_Effect01 = new List<string>();
                One_SecondaryTypeBreakableObject_Effect02 = new List<string>();
                One_SecondaryTypeBreakableObject_Effect03 = new List<string>();
                One_SecondaryTypeBreakableObject_Speed01 = new List<float>();
                One_SecondaryTypeBreakableObject_Speed02 = new List<float>();
                One_SecondaryTypeBreakableObject_Speed03 = new List<float>();
                c_sta_List.Add(c_sta_x);
                StageNameList.Add(STAGE_NAME);
                BTL_NSX_List.Add(BTL_NSX_XXXXX);
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
            File.WriteAllBytes(FilePath, ConvertToFile());
            if (basepath == "")
                MessageBox.Show("File saved to " + FilePath + ".");
        }
        public byte[] ConvertToFile() {
            byte[] fileBytes36 = new byte[0];
            int SectionTotalLength = 0;
            fileBytes36 = header;
            int LengthOfStuff = 0;
            for (int y = 0; y < EntryCount; y++) {
                LengthOfStuff = LengthOfStuff + 0x130;
                for (int x2 = 0; x2 < CountOfFiles[y]; x2++) {
                    LengthOfStuff = 0x130 + LengthOfStuff + 8 + 8 + SecondarySectionFilePath[y][x2].Length;
                }
            }
            for (int x = 0; x < EntryCount; x++) {
                fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, MainStageSection[x]);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CountOfFiles[x]), fileBytes36.Length - 0x118);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(CountOfMeshes[x]), fileBytes36.Length - 0x108);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_WeatherSettings[x]), fileBytes36.Length - 0xF8);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, MainSection_ColorGroundEffect[x], fileBytes36.Length - 0xF4);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, MainSection_ColorUnknown2[x], fileBytes36.Length - 0xF0);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, MainSection_ColorPlayerLight[x], fileBytes36.Length - 0xEC);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_EnablelensFlareSettings[x]), fileBytes36.Length - 0xD8);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_lensFlareSettings[x]), fileBytes36.Length - 0xD4);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_X_PositionLightPoint[x]), fileBytes36.Length - 0xCC);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_Y_PositionLightPoint[x]), fileBytes36.Length - 0xC8);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_Z_PositionLightPoint[x]), fileBytes36.Length - 0xC4);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, MainSection_ColorLight[x], fileBytes36.Length - 0xC0);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, MainSection_ColorUnknown[x], fileBytes36.Length - 0xBC);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_X_PositionShadow[x]), fileBytes36.Length - 0xB8);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_Y_PositionShadow[x]), fileBytes36.Length - 0xB4);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_Z_PositionShadow[x]), fileBytes36.Length - 0xB0);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_ShadowSetting_value1[x]), fileBytes36.Length - 0xAC);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, MainSection_ColorShadow[x], fileBytes36.Length - 0xA8);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_ShadowSetting_value2[x]), fileBytes36.Length - 0xA4);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_unk1[x]), fileBytes36.Length - 0xA0);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_PowerLight[x]), fileBytes36.Length - 0x9C);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_PowerSkyColor[x]), fileBytes36.Length - 0x98);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, MainSection_ColorSky[x], fileBytes36.Length - 0x94);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_blur[x]), fileBytes36.Length - 0x68);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_X_MysteriousPosition[x]), fileBytes36.Length - 0x5C);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_Y_MysteriousPosition[x]), fileBytes36.Length - 0x58);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_Z_MysteriousPosition[x]), fileBytes36.Length - 0x54);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_EnableGlareSettingValue1[x]), fileBytes36.Length - 0x64);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_EnableGlareSettingValue2[x]), fileBytes36.Length - 0x50);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_EnableGlareSettingValue3[x]), fileBytes36.Length - 0x4C);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_MysteriousGlareValue3[x]), fileBytes36.Length - 0x48);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_UnknownValue1[x]), fileBytes36.Length - 0x8C);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_UnknownValue2[x]), fileBytes36.Length - 0x88);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_UnknownValue3[x]), fileBytes36.Length - 0x84);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_MysteriousGlareValue1[x]), fileBytes36.Length - 0x44);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_PowerGlare[x]), fileBytes36.Length - 0x40);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, MainSection_ColorGlare[x], fileBytes36.Length - 0x3C);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_X_PositionGlarePoint[x]), fileBytes36.Length - 0x38);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_Y_PositionGlarePoint[x]), fileBytes36.Length - 0x34);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_Z_PositionGlarePoint[x]), fileBytes36.Length - 0x30);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSectionGlareVagueness[x]), fileBytes36.Length - 0x2C);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(MainSection_MysteriousGlareValue2[x]), fileBytes36.Length - 0x28);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, MainSection_ColorRock[x], fileBytes36.Length - 0x24);
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(LengthOfStuff), fileBytes36.Length - 0x100);
                SectionTotalLength = SectionTotalLength + 0x130;
            };
            int LengthFromSection = 0;
            int extra = 0;
            int TotalLengthOfMeshes = 0;
            for (int x = 0; x < EntryCount; x++) {
                int LengthFromPath = 0;
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes((EntryCount * 0x130) - 0x20 - (x * 0x130) + LengthFromSection + extra), FilePos + 0x10 + 0x20 + (x * 0x130));
                extra = extra + 8 * CountOfFiles[x];
                for (int x2 = 0; x2 < CountOfFiles[x]; x2++) {
                    LengthFromSection = LengthFromSection + SecondarySectionFilePath[x][x2].Length + 8;
                }
                for (int x2 = 0; x2 < CountOfFiles[x]; x2++) {
                    int _ptr = fileBytes36.Length;
                    fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(LengthFromPath + 8 * CountOfFiles[x]), _ptr);
                    LengthFromPath = LengthFromPath + SecondarySectionFilePath[x][x2].Length;
                }
                for (int x2 = 0; x2 < CountOfFiles[x]; x2++) {
                    fileBytes36 = MainFunctions.b_AddString(fileBytes36, SecondarySectionFilePath[x][x2]);
                    fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[8] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                }
            }
            int _ptrMeshes = fileBytes36.Length;
            for (int x = 0; x < EntryCount; x++) {
                for (int x2 = 0; x2 < CountOfMeshes[x]; x2++) {
                    fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[0xB0] { Convert.ToByte(x), Convert.ToByte(x2), 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
                    TotalLengthOfMeshes = TotalLengthOfMeshes + 0xB0;
                }
            }
            int _ptrLastSection = 0;
            for (int x = 0; x < EntryCount; x++) {

                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(_ptrMeshes - header.Length - 0x30 - (x * 0x130) + _ptrLastSection), header.Length + 0x30 + (x * 0x130));

                for (int x3 = 0; x3 < CountOfMeshes[x]; x3++) {
                    _ptrLastSection = _ptrLastSection + 0xB0;
                }
            }
            int TotalLengthOfSection = 0;
            int TotalLengthOfLoadedPaths = 0;
            for (int x = 0; x < EntryCount; x++) {
                for (int x2 = 0; x2 < CountOfMeshes[x]; x2++) {
                    //path
                    fileBytes36 = MainFunctions.b_AddString(fileBytes36, SecondarySectionLoadPath[x][x2]);
                    fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(TotalLengthOfMeshes + TotalLengthOfLoadedPaths - TotalLengthOfSection), _ptrMeshes + TotalLengthOfSection);
                    TotalLengthOfLoadedPaths = TotalLengthOfLoadedPaths + SecondarySectionLoadPath[x][x2].Length + 4;
                    //mesh
                    fileBytes36 = MainFunctions.b_AddString(fileBytes36, SecondarySectionLoadMesh[x][x2]);
                    fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(TotalLengthOfMeshes + TotalLengthOfLoadedPaths - TotalLengthOfSection - 0x08), _ptrMeshes + 0x08 + TotalLengthOfSection);
                    TotalLengthOfLoadedPaths = TotalLengthOfLoadedPaths + SecondarySectionLoadMesh[x][x2].Length + 4;
                    //dmypath
                    //if (SecondarySectionPositionFilePath[x][x2].Length > 1) //BIG BRAIN CC2 MADE IT BUG CAMERA VALUE IF YOU DONT USING IT ALL TIME
                    // {
                    fileBytes36 = MainFunctions.b_AddString(fileBytes36, SecondarySectionPositionFilePath[x][x2]);
                    fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(TotalLengthOfMeshes + TotalLengthOfLoadedPaths - TotalLengthOfSection - 0x10), _ptrMeshes + 0x10 + TotalLengthOfSection);
                    TotalLengthOfLoadedPaths = TotalLengthOfLoadedPaths + SecondarySectionPositionFilePath[x][x2].Length + 4;
                    //}
                    //else
                    //{
                    //    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0), _ptrMeshes + 0x10 + TotalLengthOfSection);
                    //}
                    //dmypos
                    //if (SecondarySectionPosition[x][x2].Length > 1 && SecondarySectionPositionFilePath[x][x2].Length > 1)
                    //{
                    fileBytes36 = MainFunctions.b_AddString(fileBytes36, SecondarySectionPosition[x][x2]);
                    fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(TotalLengthOfMeshes + TotalLengthOfLoadedPaths - TotalLengthOfSection - 0x18), _ptrMeshes + 0x18 + TotalLengthOfSection);
                    TotalLengthOfLoadedPaths = TotalLengthOfLoadedPaths + SecondarySectionPosition[x][x2].Length + 4;
                    //}
                    //else
                    //{
                    //    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0), _ptrMeshes + 0x18 + TotalLengthOfSection);
                    //}
                    //TypeSection
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SecondaryTypeSection[x][x2]), _ptrMeshes + 0x20 + TotalLengthOfSection);
                    //Speed of animation
                    if (SecondaryTypeSection[x][x2] != 00 && SecondaryTypeSection[x][x2] != 04) {
                        fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SecondaryTypeAnimationSection_speed[x][x2]), _ptrMeshes + 0x24 + TotalLengthOfSection);
                    } else {
                        fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0), _ptrMeshes + 0x24 + TotalLengthOfSection);
                    }
                    //Camera value
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SecondarySectionCameraValue[x][x2]), _ptrMeshes + 0x28 + TotalLengthOfSection);
                    //RigidBody value
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SecondarySectionMysteriousValue[x][x2]), _ptrMeshes + 0x2C + TotalLengthOfSection);
                    //Breakable object
                    if (SecondaryTypeSection[x][x2] == 0x0B) {
                        //path
                        fileBytes36 = MainFunctions.b_AddString(fileBytes36, SecondaryTypeBreakableObject_path[x][x2]);
                        fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                        fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(TotalLengthOfMeshes + TotalLengthOfLoadedPaths - TotalLengthOfSection - 0x38), _ptrMeshes + 0x38 + TotalLengthOfSection);
                        TotalLengthOfLoadedPaths = TotalLengthOfLoadedPaths + SecondaryTypeBreakableObject_path[x][x2].Length + 4;

                        if (SecondaryTypeBreakableObject_Effect01[x][x2].Length > 1 && SecondaryTypeBreakableObject_path[x][x2].Length > 1) {
                            //effect1
                            fileBytes36 = MainFunctions.b_AddString(fileBytes36, SecondaryTypeBreakableObject_Effect01[x][x2]);
                            fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(TotalLengthOfMeshes + TotalLengthOfLoadedPaths - TotalLengthOfSection - 0x40), _ptrMeshes + 0x40 + TotalLengthOfSection);
                            TotalLengthOfLoadedPaths = TotalLengthOfLoadedPaths + SecondaryTypeBreakableObject_Effect01[x][x2].Length + 4;

                            //effect1_speed
                            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SecondaryTypeBreakableObject_Speed01[x][x2]), _ptrMeshes + 0x48 + TotalLengthOfSection);
                        } else {
                            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0), _ptrMeshes + 0x40 + TotalLengthOfSection);
                            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0), _ptrMeshes + 0x48 + TotalLengthOfSection);
                        }
                        if (SecondaryTypeBreakableObject_Effect02[x][x2].Length > 1 && SecondaryTypeBreakableObject_path[x][x2].Length > 1) {
                            //effect2
                            fileBytes36 = MainFunctions.b_AddString(fileBytes36, SecondaryTypeBreakableObject_Effect02[x][x2]);
                            fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(TotalLengthOfMeshes + TotalLengthOfLoadedPaths - TotalLengthOfSection - 0x50), _ptrMeshes + 0x50 + TotalLengthOfSection);
                            TotalLengthOfLoadedPaths = TotalLengthOfLoadedPaths + SecondaryTypeBreakableObject_Effect02[x][x2].Length + 4;

                            //effect2_speed
                            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SecondaryTypeBreakableObject_Speed02[x][x2]), _ptrMeshes + 0x58 + TotalLengthOfSection);
                        } else {
                            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0), _ptrMeshes + 0x50 + TotalLengthOfSection);
                            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0), _ptrMeshes + 0x58 + TotalLengthOfSection);
                        }
                        if (SecondaryTypeBreakableObject_Effect03[x][x2].Length > 1 && SecondaryTypeBreakableObject_path[x][x2].Length > 1) {
                            //effect3
                            fileBytes36 = MainFunctions.b_AddString(fileBytes36, SecondaryTypeBreakableObject_Effect03[x][x2]);
                            fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(TotalLengthOfMeshes + TotalLengthOfLoadedPaths - TotalLengthOfSection - 0x60), _ptrMeshes + 0x60 + TotalLengthOfSection);
                            TotalLengthOfLoadedPaths = TotalLengthOfLoadedPaths + SecondaryTypeBreakableObject_Effect03[x][x2].Length + 4;

                            //effect3_speed
                            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SecondaryTypeBreakableObject_Speed03[x][x2]), _ptrMeshes + 0x68 + TotalLengthOfSection);
                        } else {
                            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0), _ptrMeshes + 0x60 + TotalLengthOfSection);
                            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0), _ptrMeshes + 0x68 + TotalLengthOfSection);
                        }

                    } else {
                        fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0), _ptrMeshes + 0x38 + TotalLengthOfSection);
                    }
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0x3C), _ptrMeshes + 0x70 + TotalLengthOfSection);
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0x78), _ptrMeshes + 0x74 + TotalLengthOfSection);
                    //const_values
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SecondaryConstBreakableWallValue1[x][x2]), _ptrMeshes + 0x80 + TotalLengthOfSection);
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SecondaryConstBreakableWallValue2[x][x2]), _ptrMeshes + 0x84 + TotalLengthOfSection);

                    //breakable wall
                    if (SecondaryTypeSection[x][x2] == 7) {
                        //effect1
                        fileBytes36 = MainFunctions.b_AddString(fileBytes36, SecondaryTypeBreakableWall_Effect01[x][x2]);
                        fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                        fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(TotalLengthOfMeshes + TotalLengthOfLoadedPaths - TotalLengthOfSection - 0x78), _ptrMeshes + 0x78 + TotalLengthOfSection);
                        TotalLengthOfLoadedPaths = TotalLengthOfLoadedPaths + SecondaryTypeBreakableWall_Effect01[x][x2].Length + 4;

                        //const_values
                        //fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SecondaryConstBreakableWallValue1[x][x2]), _ptrMeshes + 0x80 + TotalLengthOfSection);
                        //fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SecondaryConstBreakableWallValue2[x][x2]), _ptrMeshes + 0x84 + TotalLengthOfSection);

                        //effect2
                        fileBytes36 = MainFunctions.b_AddString(fileBytes36, SecondaryTypeBreakableWall_Effect02[x][x2]);
                        fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                        fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(TotalLengthOfMeshes + TotalLengthOfLoadedPaths - TotalLengthOfSection - 0x88), _ptrMeshes + 0x88 + TotalLengthOfSection);
                        TotalLengthOfLoadedPaths = TotalLengthOfLoadedPaths + SecondaryTypeBreakableWall_Effect02[x][x2].Length + 4;

                        //effect3
                        fileBytes36 = MainFunctions.b_AddString(fileBytes36, SecondaryTypeBreakableWall_Effect03[x][x2]);
                        fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                        fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(TotalLengthOfMeshes + TotalLengthOfLoadedPaths - TotalLengthOfSection - 0x90), _ptrMeshes + 0x90 + TotalLengthOfSection);
                        TotalLengthOfLoadedPaths = TotalLengthOfLoadedPaths + SecondaryTypeBreakableWall_Effect03[x][x2].Length + 4;

                        //sound_volume
                        fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(SecondaryTypeBreakableWall_volume[x][x2]), _ptrMeshes + 0x98 + TotalLengthOfSection);

                        //sound
                        fileBytes36 = MainFunctions.b_AddString(fileBytes36, SecondaryTypeBreakableWall_Sound[x][x2]);
                        fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                        fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(TotalLengthOfMeshes + TotalLengthOfLoadedPaths - TotalLengthOfSection - 0xA0), _ptrMeshes + 0xA0 + TotalLengthOfSection);
                        TotalLengthOfLoadedPaths = TotalLengthOfLoadedPaths + SecondaryTypeBreakableWall_Sound[x][x2].Length + 4;

                    }
                    TotalLengthOfSection = TotalLengthOfSection + 0xB0;
                }
            }

            int NamePos = fileBytes36.Length - header.Length - 0x10;
            int _ptrName = header.Length;
            for (int x = 0; x < EntryCount; x++) {
                //StageName
                fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(NamePos + 0x10), _ptrName + (x * 0x130));
                fileBytes36 = MainFunctions.b_AddString(fileBytes36, StageNameList[x]);
                fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                NamePos = (NamePos + StageNameList[x].Length + 4);
                if (c_sta_List[x].Length > 1) {
                    //c_sta_x
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(NamePos + 0x08), _ptrName + 0x08 + (x * 0x130));
                    fileBytes36 = MainFunctions.b_AddString(fileBytes36, c_sta_List[x]);
                    fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                    NamePos = (NamePos + c_sta_List[x].Length + 4);
                } else {
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(0), _ptrName + 0x08 + (x * 0x130));
                }
                if (BTL_NSX_List[x].Length > 1) {
                    //BTL_NSX_XXXXX
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(NamePos), _ptrName + 0x10 + (x * 0x130));
                    fileBytes36 = MainFunctions.b_AddString(fileBytes36, BTL_NSX_List[x]);
                    fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                    NamePos = (NamePos + BTL_NSX_List[x].Length + 4);
                } else if (BTL_NSX_List[x].Length == 0) {
                    fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(NamePos), _ptrName + 0x10 + (x * 0x130));
                    fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4] { 0x00, 0x00, 0x00, 0x00 });
                    NamePos = (NamePos + 4);

                }
                NamePos = NamePos - 0x130;
            }
            fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[0x14] { 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x02, 0x00, 0x79, 0x18, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 });

            byte[] Size1 = BitConverter.GetBytes(fileBytes36.Length - header.Length - 0x4);
            byte[] Size2 = BitConverter.GetBytes(fileBytes36.Length - header.Length);
            byte[] Size1Reverse = new byte[4]
            {
                Size1[3],
                Size1[2],
                Size1[1],
                Size1[0]
            };
            byte[] Size2Reverse = new byte[4]
            {
                Size2[3],
                Size2[2],
                Size2[1],
                Size2[0]
            };
            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, Size1Reverse, header.Length - 0x14);
            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, Size2Reverse, header.Length - 0x20);

            fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, BitConverter.GetBytes(EntryCount), header.Length - 0xC);
            return fileBytes36;
        }
    }
}
