using IWshRuntimeLibrary;
using NSUNS4_ModManager.ToolBoxCode;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using File = System.IO.File;
using WinForms = System.Windows.Forms;
namespace NSUNS4_ModManager {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public class ModList_class {
        public string ModName { get; set; }
    }
    
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            //This code checking if config file exist. If it doesnt exist, it will create config file, otherwise it will load it
            if (File.Exists(ConfigPath) == false) {
                CreateConfig();
            } else {
                LoadConfig();
            }
            //This function will refresh mod list when you started app
            RefreshModList();
        }
        public List<string> CharacterPathList = new List<string>();
        public List<bool> ReplaceCharacterList = new List<bool>();

        public List<string> ImportedCharacodesList = new List<string>();
        public int CharacodeID = 0;

        //This is vanilla files for all supported system files
        string originalChaPath = Directory.GetCurrentDirectory() + "\\systemFiles\\characode.bin.xfbin";
        string originalDppPath = Directory.GetCurrentDirectory() + "\\systemFiles\\duelPlayerParam.xfbin";
        string originalafterAttachObjectPath = Directory.GetCurrentDirectory() + "\\systemFiles\\afterAttachObject.xfbin";
        string originalappearanceAnmPath = Directory.GetCurrentDirectory() + "\\systemFiles\\appearanceAnm.xfbin";
        string originalawakeAuraPath = Directory.GetCurrentDirectory() + "\\systemFiles\\awakeAura.xfbin";
        string originalskillCustomizeParamPath = Directory.GetCurrentDirectory() + "\\systemFiles\\skillCustomizeParam.xfbin";
        string originalspSkillCustomizeParamPath = Directory.GetCurrentDirectory() + "\\systemFiles\\spSkillCustomizeParam.xfbin";
        string originalIconPath = Directory.GetCurrentDirectory() + "\\systemFiles\\player_icon.xfbin";
        string originalCspPath = Directory.GetCurrentDirectory() + "\\systemFiles\\characterSelectParam.xfbin";
        string originalPspPath = Directory.GetCurrentDirectory() + "\\systemFiles\\playerSettingParam.bin.xfbin";
        string originalcmnparamPath = Directory.GetCurrentDirectory() + "\\systemFiles\\cmnparam.xfbin";
        string originaleffectprmPath = Directory.GetCurrentDirectory() + "\\systemFiles\\effectprm.bin.xfbin";
        string originaldamageeffPath = Directory.GetCurrentDirectory() + "\\systemFiles\\damageeff.bin.xfbin";
        string originaldamageprmPath = Directory.GetCurrentDirectory() + "\\systemFiles\\damageprm.bin.xfbin";
        string originalUnlockCharaTotalPath = Directory.GetCurrentDirectory() + "\\systemFiles\\unlockCharaTotal.bin.xfbin";
        string originalMessagePath = Directory.GetCurrentDirectory() + "\\systemFiles\\message";
        string originalBtlcmnPath = Directory.GetCurrentDirectory() + "\\systemFiles\\btlcmn.xfbin";
        string originalseparamPath = Directory.GetCurrentDirectory() + "\\systemFiles\\separam.xfbin";
        string originalspTypeSupportParamPath = Directory.GetCurrentDirectory() + "\\systemFiles\\spTypeSupport.xfbin";
        string originalnuccMaterialDx11Path = Directory.GetCurrentDirectory() + "\\systemFiles\\nuccMaterial_dx11.nsh";

        //This is paths for root folder where will be saved edited files
        public static string datawin32Path = "[null]";
        public static string chaPath = "[null]";
        public static string dppPath = "[null]";
        public static string pspPath = "[null]";
        public static string unlPath = "[null]";
        public static string iconPath = "[null]";
        public static string cspPath = "[null]";
        public static string awakeAuraPath = "[null]";
        public static string ougiFinishPath = "[null]";
        public static string skillCustomizePath = "[null]";
        public static string spSkillCustomizePath = "[null]";
        public static string afterAttachObjectPath = "[null]";
        public static string appearanceAnmPath = "[null]";
        public static string stageInfoPath = "[null]";
        public static string battleParamPath = "[null]";
        public static string episodeParamPath = "[null]";
        public static string episodeMovieParamPath = "[null]";
        public static string messageInfoPath = "[null]";
        public static string cmnparamPath = "[null]";
        public static string effectprmPath = "[null]";
        public static string damageeffPath = "[null]";
        public static string conditionprmPath = "[null]";
        public static string damageprmPath = "[null]";
        public static string spTypeSupportParamPath = "[null]";
        public static string nuccMaterialDx11Path = "[null]";

        public static string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "\\config.txt";
        public static string GameRootPath = "[null]";
        public static string GameModsPath = "[null]";
        public List<string> ModName_List = new List<string>();
        public List<string> IconPaths = new List<string>();
        public List<string> AuthorPaths = new List<string>();
        public List<string> DescriptionPaths = new List<string>();
        public List<string> ModdingAPI_requirement_Paths = new List<string>();
        public List<ModList_class> ModInfoList = new List<ModList_class>();



        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            //This function let you select root folder path and save it in config file
            Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog c = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            c.IsFolderPicker = true;

            if (c.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok) {
                GameRootPath = c.FileName;
                SaveConfig();
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) {
            //This function let you select mod folder path and save it in config file
            Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog c = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            c.IsFolderPicker = true;

            if (c.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok) {
                GameModsPath = c.FileName;
                SaveConfig();
            }
        }
        
        void CreateConfig() {
            //This function creates new config file
            List<string> cfg = new List<string>();
            cfg.Add("[null]");
            cfg.Add("[null]");
            File.WriteAllLines(ConfigPath, cfg.ToArray());
            MessageBox.Show("Config file created.");
        }

        void SaveConfig() {
            //This function saves config file
            List<string> cfg = new List<string>();
            cfg.Add(GameRootPath);
            cfg.Add(GameModsPath);
            File.WriteAllLines(ConfigPath, cfg.ToArray());
            MessageBox.Show("Config file saved.");
        }

        public static void LoadConfig() {
            //This function loads all paths from config file
            string[] cfg = File.ReadAllLines(ConfigPath);
            if (cfg.Length > 0) GameRootPath = cfg[0];
            if (cfg.Length > 1) GameModsPath = cfg[1];
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            RefreshModList();
        }

        public void RefreshModList() {
            //This function refreshes mod list
            if (Directory.Exists(GameModsPath)) {
                DirectoryInfo d = new DirectoryInfo(GameModsPath); //This function getting info about all files in a path
                FileInfo[] Description_Files = d.GetFiles("Description.txt", SearchOption.AllDirectories); //Getting all files with "description.txt" name
                FileInfo[] Author_Files = d.GetFiles("Author.txt", SearchOption.AllDirectories); //Getting all files with "Author.txt" name
                FileInfo[] ModdingAPI_req_Files = d.GetFiles("ModdingAPI.txt", SearchOption.AllDirectories); //Getting all files with "ModdingAPI.txt" name
                FileInfo[] Icon_Files = d.GetFiles("Icon.png", SearchOption.AllDirectories); //Getting all files with "Icon.png" name
                //This will clean all mod info for lists
                ModInfoList.Clear();
                ModName_List.Clear();
                DescriptionPaths.Clear();
                AuthorPaths.Clear();
                IconPaths.Clear();
                ImportedCharacodesList.Clear();
                CharacterPathList.Clear();
                ModdingAPI_requirement_Paths.Clear();
                // each function will get path for all files
                foreach (FileInfo file in Description_Files) {
                    if (file.FullName.Contains("Description.txt")) {
                        DescriptionPaths.Add(file.FullName);
                        ModName_List.Add(System.IO.Path.GetDirectoryName(file.FullName));
                        ModInfoList.Add(new ModList_class() { ModName = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(file.FullName)) });
                    }
                }
                foreach (FileInfo file in Icon_Files) {
                    if (file.FullName.Contains("Icon.png")) {
                        IconPaths.Add(file.FullName);
                    }
                }
                foreach (FileInfo file in Author_Files) {
                    if (file.FullName.Contains("Author.txt")) {
                        AuthorPaths.Add(file.FullName);
                    }
                }
                foreach (FileInfo file in ModdingAPI_req_Files) {
                    if (file.FullName.Contains("ModdingAPI.txt")) {
                        ModdingAPI_requirement_Paths.Add(file.FullName);
                    }
                }
                ModsList.ItemsSource = ModInfoList; // This will add names in mod list
                ModsList.Items.Refresh(); //This will refresh mod list info

                //This function will check what characodes were imported from mod folder
                if (Directory.Exists(GameModsPath)) {
                    FileInfo[] characode_Files = d.GetFiles("characode.txt", SearchOption.AllDirectories);
                    foreach (FileInfo file in characode_Files) {
                        if (file.FullName.Contains("characode.txt")) {
                            ImportedCharacodesList.Add(System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(file.FullName)));
                        }
                    }

                }
            }
        }

        private void ModsList_Selected(object sender, RoutedEventArgs e) {
            //This function refreshes info when you selecting mod. It starting read description, author txts and adding icon
            int x = ModsList.SelectedIndex;
            if (x != -1) {
                ModDescription.Text = File.ReadAllText(DescriptionPaths[x]);
                if (ModDescription.Text == "")
                    ModDescription.Text = "No description";
                var uri = new Uri(IconPaths[x]);
                var bitmap = new BitmapImage(uri);
                ModIcon.Source = bitmap;
                ModAuthor.Content = "Author: " + File.ReadAllText(AuthorPaths[x]);

            }
        }
        public void CopyFiles(string targetPath, string originalDataWin32, string newDataWin32) {
            //This function will create directories and will copy paste files from one path to another path
            if (File.Exists(originalDataWin32)) {
                if (!Directory.Exists(targetPath)) {
                    Directory.CreateDirectory(targetPath);
                }
                File.Copy(originalDataWin32, newDataWin32, true);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) {
            //This is compile mod function
            if (Directory.Exists(GameRootPath) && Directory.Exists(GameModsPath)) {

                List<string> UsedShaders = new List<string>(); //List for used shaders to prevent loading same shader twice
                List<bool> ModdingAPIRequirement_list = new List<bool>(); //List for installing moddingapi in case, if mod requires it
                for (int c = 0; c< ModdingAPI_requirement_Paths.Count; c++) {
                    //This function reading moddingAPI.txt to get boolean value for requirement of moddingAPI
                    ModdingAPIRequirement_list.Add(Convert.ToBoolean(File.ReadAllText(ModdingAPI_requirement_Paths[c])));
                }
                if (ModdingAPIRequirement_list.Contains(true))
                    InstallModdingAPI();
                List<string> CharacodePaths = new List<string>(); //This list was used for saving characode.txt path
                List<string> CharacodesList = new List<string>(); //This list was used for saving characode
                if (Directory.Exists(GameModsPath)) {
                    DirectoryInfo d = new DirectoryInfo(@GameModsPath);
                    FileInfo[] characode_Files = d.GetFiles("characode.txt", SearchOption.AllDirectories);
                    foreach (FileInfo file in characode_Files) {
                        if (file.FullName.Contains("characode.txt")) {
                            CharacodePaths.Add(file.FullName);
                            CharacodesList.Add(System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(file.FullName)));
                            //This function checking for all characodes in mod folder
                        }
                    }

                }
                for (int c = 0; c < CharacodePaths.Count; c++) {
                    //This function adding characode 
                    CharacodeID = Convert.ToInt32(File.ReadAllText(CharacodePaths[c])); // This function getting old Characode ID which was in mod folder
                    CharacterPathList.Add(System.IO.Path.GetDirectoryName(CharacodePaths[c]));
                    
                    //This paths used for saving edited files
                    datawin32Path = GameRootPath + "\\data_win32";
                    chaPath = GameRootPath + "\\data_win32\\spc\\characode.bin.xfbin";
                    dppPath = GameRootPath + "\\data_win32\\spc\\duelPlayerParam.xfbin";
                    pspPath = GameRootPath + "\\data_win32\\spc\\WIN64\\playerSettingParam.bin.xfbin";
                    unlPath = GameRootPath + "\\data_win32\\duel\\WIN64\\unlockCharaTotal.bin.xfbin";
                    cspPath = GameRootPath + "\\data_win32\\ui\\max\\select\\WIN64\\characterSelectParam.xfbin";
                    iconPath = GameRootPath + "\\data_win32\\spc\\WIN64\\player_icon.xfbin";
                    awakeAuraPath = GameRootPath + "\\data_win32\\spc\\WIN64\\awakeAura.xfbin";
                    ougiFinishPath = GameRootPath + "\\data_win32\\rpg\\param\\WIN64\\OugiFinishParam.bin.xfbin";
                    skillCustomizePath = GameRootPath + "\\data_win32\\spc\\WIN64\\skillCustomizeParam.xfbin";
                    spSkillCustomizePath = GameRootPath + "\\data_win32\\spc\\WIN64\\spSkillCustomizeParam.xfbin";
                    afterAttachObjectPath = GameRootPath + "\\data_win32\\spc\\WIN64\\afterAttachObject.xfbin";
                    appearanceAnmPath = GameRootPath + "\\data_win32\\spc\\WIN64\\appearanceAnm.xfbin";
                    stageInfoPath = GameRootPath + "\\data_win32\\stage\\WIN64\\StageInfo.bin.xfbin";
                    battleParamPath = GameRootPath + "\\data_win32\\rpg\\WIN64\\battleParam.xfbin";
                    episodeParamPath = GameRootPath + "\\data_win32\\rpg\\param\\WIN64\\episodeParam.bin.xfbin";
                    episodeMovieParamPath = GameRootPath + "\\data_win32\\rpg\\param\\WIN64\\episodeMovieParam.bin.xfbin";
                    messageInfoPath = GameRootPath + "\\data_win32\\message";
                    cmnparamPath = GameRootPath + "\\data_win32\\sound\\cmnparam.xfbin";
                    effectprmPath = GameRootPath + "\\data_win32\\spc\\effectprm.bin.xfbin";
                    damageeffPath = GameRootPath + "\\data_win32\\spc\\damageeff.bin.xfbin";
                    conditionprmPath = GameRootPath + "\\data_win32\\spc\\conditionprm.bin.xfbin";
                    damageprmPath = GameRootPath + "\\data_win32\\spc\\damageprm.bin.xfbin";
                    spTypeSupportParamPath = GameRootPath + "\\data_win32\\spc\\WIN64\\spTypeSupportParam.xfbin";
                    nuccMaterialDx11Path = GameRootPath + "\\data\\system\\nuccMaterial_dx11.nsh";

                    //This function checking for characode.xfbin, if it exist, it will use it from root folder in data_win32, otherwise it will load vanilla file and will edit it
                    if (!File.Exists(chaPath)) {
                        chaPath = originalChaPath;
                    }
                    Tool_CharacodeEditor_code CharacodeFile = new Tool_CharacodeEditor_code();
                    CharacodeFile.OpenFile(chaPath);
                    //This function checking if mod should replace or add character 
                    bool replace = false;
                    for (int x = 0; x < CharacodeFile.CharacterCount; x++) {
                        if (CharacodeFile.CharacterList[x].Contains(CharacodesList[c])) {
                            replace = true;
                        }

                    }
                    
                    ReplaceCharacterList.Add(replace);


                }
                if (CharacterPathList.Count > 0) {
                    //This function merging mods
                    for (int i = 0; i < CharacterPathList.Count; i++) {
                        //This function was used for checking each characode inside of all mod folders
                        DirectoryInfo d = new DirectoryInfo(CharacterPathList[i]); //Information about all files in characode folder
                        FileInfo[] cha_Files = d.GetFiles("*.txt", SearchOption.AllDirectories); //This function getting info about characode.txt in characode folder
                        FileInfo[] Files = d.GetFiles("*.xfbin", SearchOption.AllDirectories); //This function getting info about all .xfbin files in characode folder
                        FileInfo[] cpk_Files = d.GetFiles("*.cpk", SearchOption.AllDirectories); //This function getting info about all .cpk files in characode folder
                        FileInfo[] Shader_Files = d.GetFiles("*.hlsl", SearchOption.AllDirectories); //This function getting info about all .hlsl shaders in characode folder

                        string gfx_path = GameRootPath + "\\data\\ui\\flash\\OTHER"; //path to vanilla gfx files
                        DirectoryInfo d_gfx = new DirectoryInfo(gfx_path);

                        FileInfo[] gfx_Files = d_gfx.GetFiles("*.gfx", SearchOption.AllDirectories); //This function getting info about all .gfx files in root game folder

                        DirectoryInfo d_or = new DirectoryInfo(datawin32Path);  //Information about all files in data_win32 folder in root game folder
                        List<string> cpk_paths = new List<string>(); //list of paths of cpk files in characode folder
                        List<string> cpk_names = new List<string>(); //list of names of cpk files in characode folder
                        List<string> shader_paths = new List<string>(); //list of paths of shaders in characode folder
                        string dataWinFolder = d_or.Name + "\\"; //data_win32 name
                        int dataWinFolderLength = dataWinFolder.Length; //data_win32 length (just of name)
                        bool originalChaExist = false;
                        string originalPathCharacode = "";
                        int CharacodeID = 0; //Characode ID which will be used for adding new characters

                        //Paths of system files in characode mod folder
                        string ModdppPath = "";
                        string ModcspPath = "";
                        string ModpspPath = "";
                        string ModskillCustomizePath = "";
                        string ModspskillCustomizePath = "";
                        string ModawakeAuraPath = "";
                        string ModiconPath = "";
                        string ModappearanceAnmPath = "";
                        string ModafterAttachObjectPath = "";
                        string ModpartnerSlotParamPath = "";
                        string ModspecialCondParamPath = "";
                        string ModcmnparamPath = "";
                        string ModdamageeffPath = "";
                        string ModeffectprmPath = "";
                        string ModprmPath = "";
                        string Modgfx_charselPath = "";
                        string ModdamageprmPath = "";
                        string ModmessagePathList = "";
                        string ModbtlcmnPath = "";
                        string ModspTypeSupportParamPath = "";
                        //Booleans of system files in characode mod folder (you can replace it on File.Exist() function)
                        bool dppExist = false;
                        bool pspExist = false;
                        bool cspExist = false;
                        bool skillCustomizeExist = false;
                        bool spskillCustomizeExist = false;
                        bool iconExist = false;
                        bool awakeAuraExist = false;
                        bool appearanceAnmExist = false;
                        bool afterAttachObjectExist = false;
                        bool partnerSlotParamExist = false;
                        bool specialCondParamExist = false;
                        bool cmnparamExist = false;
                        bool damageeffExist = false;
                        bool effectprmExist = false;
                        bool prmExist = false;
                        bool gfx_charselExist = false;
                        bool damageprmExist = false;
                        bool btlcmnExist = false;
                        bool spTypeSupportParamExist = false;
                        List<bool> messageExistList = new List<bool>();

                        //Reading all paths for system files in characode mod folder
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains(d.Name + "prm.bin.xfbin")) {
                                prmExist = true;
                                ModprmPath = file.FullName;
                                break;
                            } else {
                                prmExist = false;
                                ModprmPath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("spc\\duelPlayerParam.xfbin")) {
                                dppExist = true;
                                ModdppPath = file.FullName;
                                break;
                            } else {
                                dppExist = false;
                                ModdppPath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("spc\\damageprm.bin.xfbin")) {
                                damageprmExist = true;
                                ModdamageprmPath = file.FullName;
                                break;
                            } else {
                                damageprmExist = false;
                                ModdamageprmPath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("spc\\damageeff.bin.xfbin")) {
                                damageeffExist = true;
                                ModdamageeffPath = file.FullName;
                                break;
                            } else {
                                damageeffExist = false;
                                ModdamageeffPath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("spc\\effectprm.bin.xfbin")) {
                                effectprmExist = true;
                                ModeffectprmPath = file.FullName;
                                break;
                            } else {
                                effectprmExist = false;
                                ModeffectprmPath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("ui\\max\\select\\WIN64\\characterSelectParam.xfbin")) {
                                cspExist = true;
                                ModcspPath = file.FullName;
                                break;
                            } else {
                                cspExist = false;
                                ModcspPath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("spc\\WIN64\\playerSettingParam.bin.xfbin")) {
                                pspExist = true;
                                ModpspPath = file.FullName;
                                break;
                            } else {
                                pspExist = false;
                                ModpspPath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("spc\\WIN64\\skillCustomizeParam.xfbin")) {
                                skillCustomizeExist = true;
                                ModskillCustomizePath = file.FullName;
                                break;
                            } else {
                                skillCustomizeExist = false;
                                ModskillCustomizePath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("spc\\WIN64\\spSkillCustomizeParam.xfbin")) {
                                spskillCustomizeExist = true;
                                ModspskillCustomizePath = file.FullName;
                                break;
                            } else {
                                spskillCustomizeExist = false;
                                ModspskillCustomizePath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("spc\\WIN64\\spTypeSupportParam.xfbin")) {
                                spTypeSupportParamExist = true;
                                ModspTypeSupportParamPath = file.FullName;
                                break;
                            } else {
                                spTypeSupportParamExist = false;
                                ModspTypeSupportParamPath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("spc\\WIN64\\player_icon.xfbin")) {
                                iconExist = true;
                                ModiconPath = file.FullName;
                                break;
                            } else {
                                iconExist = false;
                                ModiconPath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("spc\\WIN64\\awakeAura.xfbin")) {
                                awakeAuraExist = true;
                                ModawakeAuraPath = file.FullName;
                                break;
                            } else {
                                awakeAuraExist = false;
                                ModawakeAuraPath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("spc\\WIN64\\appearanceAnm.xfbin")) {
                                appearanceAnmExist = true;
                                ModappearanceAnmPath = file.FullName;
                                break;
                            } else {
                                appearanceAnmExist = false;
                                ModappearanceAnmPath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("spc\\WIN64\\afterAttachObject.xfbin")) {
                                afterAttachObjectExist = true;
                                ModafterAttachObjectPath = file.FullName;
                                break;
                            } else {
                                afterAttachObjectExist = false;
                                ModafterAttachObjectPath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("partnerSlotParam.xfbin")) {
                                partnerSlotParamExist = true;
                                ModpartnerSlotParamPath = file.FullName;
                                break;
                            } else {
                                partnerSlotParamExist = false;
                                ModpartnerSlotParamPath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("specialCondParam.xfbin")) {
                                specialCondParamExist = true;
                                ModspecialCondParamPath = file.FullName;
                                break;
                            } else {
                                specialCondParamExist = false;
                                ModspecialCondParamPath = "";
                            }
                        }
                        foreach (FileInfo file in cpk_Files) {
                            if (file.FullName.Contains(d.Name)) {
                                cpk_paths.Add(file.FullName);
                                cpk_names.Add(file.Name);
                                break;
                            }
                        }
                        foreach (FileInfo file in Shader_Files) {
                            shader_paths.Add(file.FullName);
                        }
                        foreach (FileInfo file in cha_Files) {
                            if (file.FullName.Contains("characode.txt")) {
                                originalChaExist = true;
                                originalPathCharacode = file.FullName;
                                break;
                            } else {
                                originalChaExist = false;
                                originalPathCharacode = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("sound\\cmnparam.xfbin")) {
                                cmnparamExist = true;
                                ModcmnparamPath = file.FullName;
                                break;
                            } else {
                                cmnparamExist = false;
                                ModcmnparamPath = "";
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("sound\\PC\\btlcmn.xfbin")) {
                                btlcmnExist = true;
                                ModbtlcmnPath = file.FullName;
                                break;
                            } else {
                                btlcmnExist = false;
                                ModbtlcmnPath = "";
                            }
                        }
                        foreach (FileInfo file in gfx_Files) {
                            if (file.FullName.Contains("charsel\\charsel.gfx")) {
                                gfx_charselExist = true;
                                Modgfx_charselPath = file.FullName;
                                break;
                            } else {
                                gfx_charselExist = false;
                                Modgfx_charselPath = "";
                            }
                        }
                        for (int l = 0; l < Program.LANG.Length; l++) {
                            messageExistList.Add(false);
                        }
                        for (int l = 0; l < Program.LANG.Length; l++) {
                            foreach (FileInfo file in Files) {
                                if (file.FullName.Contains("message\\WIN64\\" + Program.LANG[l] + "\\messageInfo.bin.xfbin")) {
                                    messageExistList[l] = true;
                                    ModmessagePathList = file.FullName;
                                    break;
                                } else {
                                    messageExistList[l] = false;
                                }
                            }
                        }
                        //This condition checking if characode.txt exist, so it detector for characode from mod
                        if (originalChaExist) {
                            int OldCharacode = Convert.ToInt32(File.ReadAllText(originalPathCharacode)); //This is old characode ID
                            //characode edits
                            if (!ReplaceCharacterList[i]) { //If character mod don't replace anyone, it adds new characode in file
                                Tool_CharacodeEditor_code CharacodeFile = new Tool_CharacodeEditor_code();
                                if (File.Exists(datawin32Path + "\\spc\\characode.bin.xfbin"))
                                    CharacodeFile.OpenFile(datawin32Path + "\\spc\\characode.bin.xfbin");
                                else {
                                    CharacodeFile.OpenFile(originalChaPath);
                                }
                                CharacodeFile.AddID(d.Name);
                                if (!Directory.Exists(datawin32Path + "\\spc")) {
                                    Directory.CreateDirectory(datawin32Path + "\\spc");
                                }
                                CharacodeFile.SaveFileAs(datawin32Path + "\\spc\\characode.bin.xfbin");
                                for (int z = 0; z < CharacodeFile.CharacterCount; z++) {
                                    if (CharacodeFile.CharacterList[z].Contains(d.Name)) {
                                        CharacodeID = z + 1; //This is new characode ID for character mod
                                        break;
                                    }
                                }
                            } else {
                                CharacodeID = OldCharacode; //If character mod replaced someone, it will use old characode
                            }
                            string root_path = datawin32Path.Replace(d_or.Name, ""); //Old code from toolbox, can be replaced on GameRootPath variable
                            if (shader_paths.Count > 0) { // This function reading nuccMaterial_dx11.nsh and adding new shaders to it in case, if it exist in mod folder

                                byte[] nuccMaterialFile = File.ReadAllBytes(nuccMaterialDx11Path); // This function reading all bytes from nuccMaterial_dx11 file
                                int EntryCount = MainFunctions.b_ReadIntFromTwoBytes(nuccMaterialFile,0x0E); // This function reading shader count from nuccMaterial_dx11 file
                                for (int sh = 0; sh<shader_paths.Count; sh++) {
                                    if (!UsedShaders.Contains(System.IO.Path.GetFileName(shader_paths[sh]))) { //If shaders wasnt added before, it will add it at the end of file and will change count of shaders
                                        nuccMaterialFile = MainFunctions.b_AddBytes(nuccMaterialFile, File.ReadAllBytes(shader_paths[sh]));
                                        EntryCount++;
                                        UsedShaders.Add(System.IO.Path.GetFileName(shader_paths[sh])); //Adding name of shader in list of used shaders
                                    }
                                }
                                nuccMaterialFile = MainFunctions.b_ReplaceBytes(nuccMaterialFile, BitConverter.GetBytes(EntryCount), 0x0E, 0, 2); //Replacing byte of shader's count
                                nuccMaterialFile = MainFunctions.b_ReplaceBytes(nuccMaterialFile, BitConverter.GetBytes(nuccMaterialFile.Length), 0x04, 0); //Replacing size bytes of nuccMaterial_dx11 file
                                File.WriteAllBytes(nuccMaterialDx11Path, nuccMaterialFile);
                            }
                            if (specialCondParamExist) { //If specialCondParam exist in character mod folder, it will read it and change old characode on new characode and will add it to root game folder
                                CopyFiles(root_path + "\\moddingapi\\mods\\" + d.Name, ModspecialCondParamPath, root_path + "\\moddingapi\\mods\\" + d.Name + "\\specialCondParam.xfbin");
                                byte[] specialCondParamFile = File.ReadAllBytes(root_path + "\\moddingapi\\mods\\" + d.Name + "\\specialCondParam.xfbin");
                                specialCondParamFile = MainFunctions.b_ReplaceBytes(specialCondParamFile, BitConverter.GetBytes(CharacodeID), 0x17);
                                File.WriteAllBytes(root_path + "\\moddingapi\\mods\\" + d.Name + "\\specialCondParam.xfbin", specialCondParamFile);

                            }
                            if (partnerSlotParamExist) { //If partnerSlotParamExist exist in character mod folder, it will read it and change old characode on new characode and will add it to root game folder
                                CopyFiles(root_path + "\\moddingapi\\mods\\" + d.Name, ModpartnerSlotParamPath, root_path + "\\moddingapi\\mods\\" + d.Name + "\\partnerSlotParam.xfbin");
                                byte[] partnerSlotParamFile = File.ReadAllBytes(root_path + "\\moddingapi\\mods\\" + d.Name + "\\partnerSlotParam.xfbin");
                                partnerSlotParamFile = MainFunctions.b_ReplaceBytes(partnerSlotParamFile, BitConverter.GetBytes(CharacodeID), 0x17);
                                File.WriteAllBytes(root_path + "\\moddingapi\\mods\\" + d.Name + "\\partnerSlotParam.xfbin", partnerSlotParamFile);
                            }
                            if (cpk_paths.Count > 0) {//If character mod contains cpk archives, it will copy paste them in root game folder (requires to have characode in name of cpk)
                                for (int c = 0; c < cpk_paths.Count; c++) {
                                    CopyFiles(root_path + "\\moddingapi\\mods\\" + d.Name, cpk_paths[c], root_path + "\\moddingapi\\mods\\" + d.Name + "\\" + cpk_names[c]);
                                    if (File.Exists(cpk_paths[c] + ".info")) {
                                        CopyFiles(root_path + "\\moddingapi\\mods\\" + d.Name, cpk_paths[c] + ".info", root_path + "\\moddingapi\\mods\\" + d.Name + "\\" + cpk_names[c] + ".info");

                                    }
                                }
                            }
                            if (specialCondParamExist || partnerSlotParamExist || cpk_paths.Count > 0) { //If anything was added in moddingAPI folder, it will add info.txt file for loading cpk files or conditions for character
                                FileStream ffParameter = new FileStream(root_path + "\\moddingapi\\mods\\" + d.Name + "\\info.txt", FileMode.Create, FileAccess.Write);
                                StreamWriter mm_WriterParameter = new StreamWriter(ffParameter);
                                mm_WriterParameter.BaseStream.Seek(0, SeekOrigin.End);
                                mm_WriterParameter.Write("Exported character " + d.Name + "| |Unknown");
                                mm_WriterParameter.Flush();
                                mm_WriterParameter.Close();
                            }
                            //This code prevents from copy pasting system files
                            foreach (FileInfo file in Files) {
                                if (!file.Name.Contains("characode")
                                    && !file.Name.Contains("cmnparam")
                                    && !file.Name.Contains("duelPlayerParam")
                                    && !file.Name.Contains("awakeAura")
                                    && !file.Name.Contains("appearanceAnm")
                                    && !file.Name.Contains("afterAttachObject")
                                    && !file.Name.Contains("characterSelectParam")
                                    && !file.Name.Contains("playerSettingParam")
                                    && !file.Name.Contains("skillCustomizeParam")
                                    && !file.Name.Contains("spSkillCustomizeParam")
                                    && !file.Name.Contains("player_icon")
                                    && !file.Name.Contains("damageeff")
                                    && !file.Name.Contains("effectprm")
                                    && !file.Name.Contains("conditionprm")
                                    && !file.Name.Contains("damageprm")
                                    && !file.Name.Contains("unlockCharaTotal")
                                    && !file.Name.Contains("messageInfo")
                                    && !file.Name.Contains("btlcmn")
                                    && !file.Name.Contains("spTypeSupportParam")) {
                                    //This code prevents from copy pasting moddingApi files
                                    if (!file.FullName.Contains("moddingapi")) {
                                        //This code loads all files from mod folder
                                        CopyFiles(System.IO.Path.GetDirectoryName(datawin32Path + "\\" + file.FullName.Substring(file.FullName.IndexOf(dataWinFolder) + dataWinFolderLength)), file.FullName, datawin32Path + "\\" + file.FullName.Substring(file.FullName.IndexOf(dataWinFolder) + dataWinFolderLength));

                                    }
                                }
                            }
                            if (dppExist) {
                                //This code merges duelPlayerParam files
                                Tool_DuelPlayerParamEditor_code DppModFile = new Tool_DuelPlayerParamEditor_code();
                                Tool_DuelPlayerParamEditor_code DppOriginalFile = new Tool_DuelPlayerParamEditor_code();
                                DppModFile.OpenFile(ModdppPath); //This code open modded duelPlayerParam (goes in mod folder)
                                if (File.Exists(dppPath)) //This code open vanilla duelPlayerParam or edited duelPlayerParam (goes in root folder)
                                    DppOriginalFile.OpenFile(dppPath);
                                else {
                                    DppOriginalFile.OpenFile(originalDppPath);
                                }
                                if (ReplaceCharacterList[i]) { //This function changes exist entry in duelPlayerParam
                                    for (int c = 0; c < DppOriginalFile.EntryCount; c++) {
                                        if (DppOriginalFile.BinName[c].Contains(d.Name)) { //This function finding entry in duelPlayerParam, 0 index reading only 1st section in modded duelPlayerParam
                                            DppOriginalFile.BinPath[c] = DppModFile.BinPath[0];
                                            DppOriginalFile.BinName[c] = DppModFile.BinName[0];
                                            DppOriginalFile.Data[c] = DppModFile.Data[0];
                                            DppOriginalFile.CharaList[c] = DppModFile.CharaList[0];
                                            DppOriginalFile.CostumeList[c] = DppModFile.CostumeList[0];
                                            DppOriginalFile.AwkCostumeList[c] = DppModFile.AwkCostumeList[0];
                                            
                                            DppOriginalFile.DefaultAssist1[c] = DppModFile.DefaultAssist1[0];
                                            DppOriginalFile.DefaultAssist2[c] = DppModFile.DefaultAssist2[0];
                                            DppOriginalFile.AwkAction[c] = DppModFile.AwkAction[0];
                                            DppOriginalFile.ItemList[c] = DppModFile.ItemList[0];
                                            DppOriginalFile.ItemCount[c] = DppModFile.ItemCount[0];
                                            DppOriginalFile.Partner[c] = DppModFile.Partner[0];
                                            DppOriginalFile.SettingList[c] = DppModFile.SettingList[0];
                                            DppOriginalFile.Setting2List[c] = DppModFile.Setting2List[0];
                                            DppOriginalFile.EnableAwaSkillList[c] = DppModFile.EnableAwaSkillList[0];
                                            DppOriginalFile.VictoryAngleList[c] = DppModFile.VictoryAngleList[0];
                                            DppOriginalFile.VictoryPosList[c] = DppModFile.VictoryPosList[0];
                                            DppOriginalFile.VictoryUnknownList[c] = DppModFile.VictoryUnknownList[0];
                                            DppOriginalFile.AwaSettingList[c] = DppModFile.AwaSettingList[0];
                                        }

                                    }

                                } else {
                                    //This function adding new entry to duelPlayerParam since character mod doesn't replace anyone, so it requires new entry
                                    DppOriginalFile.BinPath.Add(DppModFile.BinPath[0]);
                                    DppOriginalFile.BinName.Add(DppModFile.BinName[0]);
                                    DppOriginalFile.Data.Add(DppModFile.Data[0]);
                                    DppOriginalFile.CharaList.Add(DppModFile.CharaList[0]);
                                    DppOriginalFile.CostumeList.Add(DppModFile.CostumeList[0]);
                                    DppOriginalFile.AwkCostumeList.Add(DppModFile.AwkCostumeList[0]);
                                    DppOriginalFile.DefaultAssist1.Add(DppModFile.DefaultAssist1[0]);
                                    DppOriginalFile.DefaultAssist2.Add(DppModFile.DefaultAssist2[0]);
                                    DppOriginalFile.AwkAction.Add(DppModFile.AwkAction[0]);
                                    DppOriginalFile.ItemList.Add(DppModFile.ItemList[0]);
                                    DppOriginalFile.ItemCount.Add(DppModFile.ItemCount[0]);
                                    DppOriginalFile.Partner.Add(DppModFile.Partner[0]);
                                    DppOriginalFile.SettingList.Add(DppModFile.SettingList[0]);
                                    DppOriginalFile.Setting2List.Add(DppModFile.Setting2List[0]);
                                    DppOriginalFile.EnableAwaSkillList.Add(DppModFile.EnableAwaSkillList[0]);
                                    DppOriginalFile.VictoryAngleList.Add(DppModFile.VictoryAngleList[0]);
                                    DppOriginalFile.VictoryPosList.Add(DppModFile.VictoryPosList[0]);
                                    DppOriginalFile.VictoryUnknownList.Add(DppModFile.VictoryUnknownList[0]);
                                    DppOriginalFile.AwaSettingList.Add(DppModFile.AwaSettingList[0]);
                                    DppOriginalFile.EntryCount++;
                                }
                                //Creating directory in root folder, so we could save edited duelPlayerParam
                                if (!Directory.Exists(datawin32Path + "\\spc")) {
                                    Directory.CreateDirectory(datawin32Path + "\\spc");
                                }
                                //Saving all edits in root folder
                                DppOriginalFile.SaveFileAs(datawin32Path + "\\spc\\duelPlayerParam.xfbin");
                            }
                            if (pspExist) {
                                //This code merges playerSettingParam files
                                Tool_PlayerSettingParamEditor_code PspModFile = new Tool_PlayerSettingParamEditor_code();
                                Tool_PlayerSettingParamEditor_code PspOriginalFile = new Tool_PlayerSettingParamEditor_code();
                                //This code open mod and vanilla/edited files
                                PspModFile.OpenFile(ModpspPath);
                                if (File.Exists(pspPath))
                                    PspOriginalFile.OpenFile(pspPath);
                                else {
                                    PspOriginalFile.OpenFile(originalPspPath);
                                }
                                if (ReplaceCharacterList[i]) { 
                                    for (int y = 0; y < PspModFile.EntryCount; y++) {
                                        bool found = false;
                                        for (int z = 0; z < PspOriginalFile.EntryCount; z++) {
                                            if (PspOriginalFile.CharacterList[z] == PspModFile.CharacterList[y]) { //This code changes exist entry in playerSettingParam
                                                PspOriginalFile.CharacodeList[z] = PspModFile.CharacodeList[y];
                                                PspOriginalFile.OptValueA[z] = PspModFile.OptValueA[y];
                                                PspOriginalFile.OptValueB[z] = PspModFile.OptValueB[y];
                                                PspOriginalFile.OptValueC[z] = PspModFile.OptValueC[y];
                                                PspOriginalFile.c_cha_a_List[z] = PspModFile.c_cha_a_List[y];
                                                PspOriginalFile.c_cha_b_List[z] = PspModFile.c_cha_b_List[y];
                                                PspOriginalFile.OptValueD[z] = PspModFile.OptValueD[y];
                                                PspOriginalFile.OptValueE[z] = PspModFile.OptValueE[y];
                                                found = true;
                                                break;
                                            }
                                        }
                                        if (!found) { //This code add entry in playerSettingParam if it wasn't able to find entry (required for costumes mods)

                                            //This code was used for getting new PresetID, so we wouldnt replace exist Ids
                                            List<int> PresetID_List = new List<int>();
                                            for (int j = 0; j < PspOriginalFile.EntryCount; j++) {
                                                PresetID_List.Add(MainFunctions.b_byteArrayToInt(PspOriginalFile.PresetList[j]));
                                            }
                                            int maxValue = PresetID_List.Max();
                                            int new_presetID = 0;
                                            do {
                                                new_presetID++;
                                            }
                                            while (PresetID_List.Contains(new_presetID));
                                            int old_PresetID = MainFunctions.b_byteArrayToInt(PspModFile.PresetList[y]);
                                            PspModFile.PresetList[y] = BitConverter.GetBytes(new_presetID);
                                            for (int h = 0; h < PspModFile.EntryCount; h++) {
                                                if (PspModFile.OptValueE[h] == old_PresetID)
                                                    PspModFile.OptValueE[h] = new_presetID;
                                            }
                                            //This codes adding info about entry in vanilla/edited file
                                            PspOriginalFile.PresetList.Add(PspModFile.PresetList[y]);
                                            PspOriginalFile.CharacodeList.Add(PspModFile.CharacodeList[y]);
                                            PspOriginalFile.OptValueA.Add(PspModFile.OptValueA[y]);
                                            PspOriginalFile.CharacterList.Add(PspModFile.CharacterList[y]);
                                            PspOriginalFile.OptValueB.Add(PspModFile.OptValueB[y]);
                                            PspOriginalFile.OptValueC.Add(PspModFile.OptValueC[y]);
                                            PspOriginalFile.c_cha_a_List.Add(PspModFile.c_cha_a_List[y]);
                                            PspOriginalFile.c_cha_b_List.Add(PspModFile.c_cha_b_List[y]);
                                            PspOriginalFile.OptValueD.Add(PspModFile.OptValueD[y]);
                                            PspOriginalFile.OptValueE.Add(PspModFile.OptValueE[y]);
                                            PspOriginalFile.EntryCount++;

                                            //This codes adding entries in unlockCharaTotal file so we could unlock entry
                                            Tool_UnlockCharaTotalEditor_code UnlockCharaTotalOriginalFile = new Tool_UnlockCharaTotalEditor_code();
                                            if (File.Exists(unlPath))
                                                UnlockCharaTotalOriginalFile.OpenFile(unlPath);
                                            else {
                                                UnlockCharaTotalOriginalFile.OpenFile(originalUnlockCharaTotalPath);
                                            }
                                            UnlockCharaTotalOriginalFile.AddID_Importer(new_presetID, 1);
                                            if (!Directory.Exists(datawin32Path + "\\duel\\WIN64")) {
                                                Directory.CreateDirectory(datawin32Path + "\\duel\\WIN64");
                                            }
                                            UnlockCharaTotalOriginalFile.SaveFileAs(datawin32Path + "\\duel\\WIN64\\unlockCharaTotal.bin.xfbin");
                                        }
                                    }
                                } else {
                                    //This function was used for adding entry for character which doesn't replace anyone
                                    for (int y = 0; y < PspModFile.EntryCount; y++) {

                                        //This code was used for getting new PresetID, so we wouldnt replace exist Ids
                                        List<int> PresetID_List = new List<int>();
                                        for (int j = 0; j < PspOriginalFile.EntryCount; j++) {
                                            PresetID_List.Add(MainFunctions.b_byteArrayToInt(PspOriginalFile.PresetList[j]));
                                        }
                                        int new_presetID = 0;
                                        do {
                                            new_presetID++;
                                        }
                                        while (PresetID_List.Contains(new_presetID));
                                        int old_PresetID = MainFunctions.b_byteArrayToInt(PspModFile.PresetList[y]);
                                        PspModFile.PresetList[y] = BitConverter.GetBytes(new_presetID);
                                        for (int h = 0; h < PspModFile.EntryCount; h++) {
                                            if (PspModFile.OptValueE[h] == old_PresetID)
                                                PspModFile.OptValueE[h] = new_presetID;
                                        }

                                        //This codes adding info about entry in vanilla/edited file
                                        PspOriginalFile.PresetList.Add(PspModFile.PresetList[y]);
                                        PspOriginalFile.CharacodeList.Add(BitConverter.GetBytes(CharacodeID));
                                        PspOriginalFile.OptValueA.Add(PspModFile.OptValueA[y]);
                                        PspOriginalFile.CharacterList.Add(PspModFile.CharacterList[y]);
                                        PspOriginalFile.OptValueB.Add(PspModFile.OptValueB[y]);
                                        PspOriginalFile.OptValueC.Add(PspModFile.OptValueC[y]);
                                        PspOriginalFile.c_cha_a_List.Add(PspModFile.c_cha_a_List[y]);
                                        PspOriginalFile.c_cha_b_List.Add(PspModFile.c_cha_b_List[y]);
                                        PspOriginalFile.OptValueD.Add(PspModFile.OptValueD[y]);
                                        PspOriginalFile.OptValueE.Add(PspModFile.OptValueE[y]);
                                        PspOriginalFile.EntryCount++;

                                        //This codes adding entries in unlockCharaTotal file so we could unlock entry
                                        Tool_UnlockCharaTotalEditor_code UnlockCharaTotalOriginalFile = new Tool_UnlockCharaTotalEditor_code();
                                        if (File.Exists(unlPath))
                                            UnlockCharaTotalOriginalFile.OpenFile(unlPath);
                                        else {
                                            UnlockCharaTotalOriginalFile.OpenFile(originalUnlockCharaTotalPath);
                                        }
                                        UnlockCharaTotalOriginalFile.AddID_Importer(new_presetID, 1);
                                        if (!Directory.Exists(datawin32Path + "\\duel\\WIN64")) {
                                            Directory.CreateDirectory(datawin32Path + "\\duel\\WIN64");
                                        }
                                        UnlockCharaTotalOriginalFile.SaveFileAs(datawin32Path + "\\duel\\WIN64\\unlockCharaTotal.bin.xfbin");
                                    }
                                }
                                //This code creates directory in root folder
                                if (!Directory.Exists(datawin32Path + "\\spc\\WIN64")) {
                                    Directory.CreateDirectory(datawin32Path + "\\spc\\WIN64");
                                }
                                //This code saves edited file
                                PspOriginalFile.SaveFileAs(datawin32Path + "\\spc\\WIN64\\playerSettingParam.bin.xfbin");
                            }
                            if (cspExist) {
                                //This code merges characterSelectParam files
                                Tool_RosterEditor_code CspModFile = new Tool_RosterEditor_code();
                                Tool_RosterEditor_code CspOriginalFile = new Tool_RosterEditor_code();
                                CspModFile.OpenFile(ModcspPath);
                                if (File.Exists(cspPath))
                                    CspOriginalFile.OpenFile(cspPath);
                                else {
                                    CspOriginalFile.OpenFile(originalCspPath);
                                }
                                //Max page and max position values. Was used for adding characters on new slots
                                int maxPage = CspOriginalFile.SearchMaxPositionAndPageIndex()[0];
                                int maxSlot = CspOriginalFile.SearchMaxPositionAndPageIndex()[1];


                                if (ReplaceCharacterList[i]) {
                                    for (int v = 0; v < CspModFile.EntryCount; v++) {
                                        bool found = false;
                                        for (int c = 0; c < CspOriginalFile.EntryCount; c++) {
                                            if (CspOriginalFile.CharacterList[c] == CspModFile.CharacterList[v]) {
                                                CspOriginalFile.PageList[c] = CspModFile.PageList[v];
                                                CspOriginalFile.PositionList[c] = CspModFile.PositionList[v];
                                                CspOriginalFile.CostumeList[c] = CspModFile.CostumeList[v];
                                                CspOriginalFile.ChaList[c] = CspModFile.ChaList[v];
                                                CspOriginalFile.AccessoryList[c] = CspModFile.AccessoryList[v];
                                                CspOriginalFile.NewIdList[c] = CspModFile.NewIdList[v];
                                                CspOriginalFile.GibberishBytes[c] = CspModFile.GibberishBytes[v];
                                                found = true;
                                            }
                                            //This code required for adding costumes mods, but code isnt made yet for it
                                            //else if (CspOriginalFile.CharacterList[c] != CspModFile.CharacterList[v]
                                            //    && CspOriginalFile.PageList[c] == CspModFile.PageList[v]
                                            //    && CspOriginalFile.PositionList[c] == CspModFile.PositionList[v]
                                            //    && CspOriginalFile.CostumeList[c] == CspModFile.CostumeList[v]) {
                                            //    CspOriginalFile.CharacterList.Add(CspModFile.CharacterList[v]);
                                            //    CspOriginalFile.PageList.Add(CspModFile.PageList[v]);
                                            //    CspOriginalFile.PositionList.Add(CspModFile.PositionList[v]);
                                            //    CspOriginalFile.CostumeList.Add(CspOriginalFile.SearchMaxCostumeInPageAndSlotIndex(CspModFile.PageList[v], CspModFile.PositionList[v]));
                                            //    CspOriginalFile.ChaList.Add(CspModFile.ChaList[v]);
                                            //    CspOriginalFile.AccessoryList.Add(CspModFile.AccessoryList[v]);
                                            //    CspOriginalFile.NewIdList.Add(CspModFile.NewIdList[v]);
                                            //    CspOriginalFile.GibberishBytes.Add(CspModFile.GibberishBytes[v]);
                                            //    CspOriginalFile.EntryCount++;
                                            //}

                                        }
                                        if (!found) {
                                            //If entry wasnt found, it will add entries with unedited positions
                                            CspOriginalFile.CharacterList.Add(CspModFile.CharacterList[v]);
                                            CspOriginalFile.PageList.Add(CspModFile.PageList[v]);
                                            CspOriginalFile.PositionList.Add(CspModFile.PositionList[v]);
                                            CspOriginalFile.CostumeList.Add(CspModFile.CostumeList[v]);
                                            CspOriginalFile.ChaList.Add(CspModFile.ChaList[v]);
                                            CspOriginalFile.AccessoryList.Add(CspModFile.AccessoryList[v]);
                                            CspOriginalFile.NewIdList.Add(CspModFile.NewIdList[v]);
                                            CspOriginalFile.GibberishBytes.Add(CspModFile.GibberishBytes[v]);
                                            CspOriginalFile.EntryCount++;
                                        }
                                    }
                                } else {
                                    //If mod doesn't replace character, it adds new entries in characterSelectParam
                                    for (int v = 0; v < CspModFile.EntryCount; v++) {
                                        CspOriginalFile.CharacterList.Add(CspModFile.CharacterList[v]);
                                        CspOriginalFile.PageList.Add(maxPage);
                                        CspOriginalFile.PositionList.Add(maxSlot);
                                        CspOriginalFile.CostumeList.Add(CspModFile.CostumeList[v]);
                                        CspOriginalFile.ChaList.Add(CspModFile.ChaList[v]);
                                        CspOriginalFile.AccessoryList.Add(CspModFile.AccessoryList[v]);
                                        CspOriginalFile.NewIdList.Add(CspModFile.NewIdList[v]);
                                        CspOriginalFile.GibberishBytes.Add(CspModFile.GibberishBytes[v]);
                                        CspOriginalFile.EntryCount++;
                                    }
                                }
                                //Creates directory for characterSelectParam
                                if (!Directory.Exists(datawin32Path + "\\ui\\max\\select\\WIN64")) {
                                    Directory.CreateDirectory(datawin32Path + "\\ui\\max\\select\\WIN64");
                                }
                                //Saved edited characterSelectParam
                                CspOriginalFile.SaveFileAs(datawin32Path + "\\ui\\max\\select\\WIN64\\characterSelectParam.xfbin");
                                //Changes max page value in charsel.gfx file
                                if (gfx_charselExist) {
                                    byte[] charsel = File.ReadAllBytes(Modgfx_charselPath);
                                    int pos = MainFunctions.b_FindBytes(charsel, new byte[16] { 0x02, 0x68, 0xF7, 0x06, 0x5E, 0xF8, 0x06, 0x24, 0x03, 0x68, 0xF8, 0x06, 0x5E, 0xF9, 0x06, 0x24 }) + 0x10;
                                    charsel[pos] = (byte)maxPage;
                                    File.WriteAllBytes(Modgfx_charselPath, charsel);
                                }
                            }
                            if (skillCustomizeExist) {
                                //This function merges skillCustomizeParamEditor files
                                Tool_SkillCustomizeParamEditor_code skillCustomizeModFile = new Tool_SkillCustomizeParamEditor_code();
                                Tool_SkillCustomizeParamEditor_code skillCustomizeOriginalFile = new Tool_SkillCustomizeParamEditor_code();
                                skillCustomizeModFile.OpenFile(ModskillCustomizePath);
                                if (File.Exists(skillCustomizePath))
                                    skillCustomizeOriginalFile.OpenFile(skillCustomizePath);
                                else {
                                    skillCustomizeOriginalFile.OpenFile(originalskillCustomizeParamPath);
                                }
                                if (ReplaceCharacterList[i]) { //If mod replaces character, it will change exist entry
                                    for (int c = 0; c < skillCustomizeOriginalFile.EntryCount; c++) {
                                        if (MainFunctions.b_byteArrayToInt(skillCustomizeOriginalFile.CharacodeList[c]) == MainFunctions.b_byteArrayToInt(skillCustomizeModFile.CharacodeList[0])) {
                                            skillCustomizeOriginalFile.Skill1List[c] = skillCustomizeModFile.Skill1List[0];
                                            skillCustomizeOriginalFile.Skill2List[c] = skillCustomizeModFile.Skill2List[0];
                                            skillCustomizeOriginalFile.Skill3List[c] = skillCustomizeModFile.Skill3List[0];
                                            skillCustomizeOriginalFile.Skill4List[c] = skillCustomizeModFile.Skill4List[0];
                                            skillCustomizeOriginalFile.Skill5List[c] = skillCustomizeModFile.Skill5List[0];
                                            skillCustomizeOriginalFile.Skill6List[c] = skillCustomizeModFile.Skill6List[0];
                                            skillCustomizeOriginalFile.SkillAwaList[c] = skillCustomizeModFile.SkillAwaList[0];
                                            skillCustomizeOriginalFile.Skill1_ex_List[c] = skillCustomizeModFile.Skill1_ex_List[0];
                                            skillCustomizeOriginalFile.Skill2_ex_List[c] = skillCustomizeModFile.Skill2_ex_List[0];
                                            skillCustomizeOriginalFile.Skill3_ex_List[c] = skillCustomizeModFile.Skill3_ex_List[0];
                                            skillCustomizeOriginalFile.Skill4_ex_List[c] = skillCustomizeModFile.Skill4_ex_List[0];
                                            skillCustomizeOriginalFile.Skill5_ex_List[c] = skillCustomizeModFile.Skill5_ex_List[0];
                                            skillCustomizeOriginalFile.Skill6_ex_List[c] = skillCustomizeModFile.Skill6_ex_List[0];
                                            skillCustomizeOriginalFile.SkillAwa_ex_List[c] = skillCustomizeModFile.SkillAwa_ex_List[0];
                                            skillCustomizeOriginalFile.Skill1_air_List[c] = skillCustomizeModFile.Skill1_air_List[0];
                                            skillCustomizeOriginalFile.Skill2_air_List[c] = skillCustomizeModFile.Skill2_air_List[0];
                                            skillCustomizeOriginalFile.Skill3_air_List[c] = skillCustomizeModFile.Skill3_air_List[0];
                                            skillCustomizeOriginalFile.Skill4_air_List[c] = skillCustomizeModFile.Skill4_air_List[0];
                                            skillCustomizeOriginalFile.Skill5_air_List[c] = skillCustomizeModFile.Skill5_air_List[0];
                                            skillCustomizeOriginalFile.Skill6_air_List[c] = skillCustomizeModFile.Skill6_air_List[0];
                                            skillCustomizeOriginalFile.SkillAwa_air_List[c] = skillCustomizeModFile.SkillAwa_air_List[0];
                                            skillCustomizeOriginalFile.Skill1_CUC_List[c] = skillCustomizeModFile.Skill1_CUC_List[0];
                                            skillCustomizeOriginalFile.Skill2_CUC_List[c] = skillCustomizeModFile.Skill2_CUC_List[0];
                                            skillCustomizeOriginalFile.Skill3_CUC_List[c] = skillCustomizeModFile.Skill3_CUC_List[0];
                                            skillCustomizeOriginalFile.Skill4_CUC_List[c] = skillCustomizeModFile.Skill4_CUC_List[0];
                                            skillCustomizeOriginalFile.Skill5_CUC_List[c] = skillCustomizeModFile.Skill5_CUC_List[0];
                                            skillCustomizeOriginalFile.Skill6_CUC_List[c] = skillCustomizeModFile.Skill6_CUC_List[0];
                                            skillCustomizeOriginalFile.SkillAwa_CUC_List[c] = skillCustomizeModFile.SkillAwa_CUC_List[0];
                                            skillCustomizeOriginalFile.Skill1_CUCC_List[c] = skillCustomizeModFile.Skill1_CUCC_List[0];
                                            skillCustomizeOriginalFile.Skill2_CUCC_List[c] = skillCustomizeModFile.Skill2_CUCC_List[0];
                                            skillCustomizeOriginalFile.Skill3_CUCC_List[c] = skillCustomizeModFile.Skill3_CUCC_List[0];
                                            skillCustomizeOriginalFile.Skill4_CUCC_List[c] = skillCustomizeModFile.Skill4_CUCC_List[0];
                                            skillCustomizeOriginalFile.Skill5_CUCC_List[c] = skillCustomizeModFile.Skill5_CUCC_List[0];
                                            skillCustomizeOriginalFile.Skill6_CUCC_List[c] = skillCustomizeModFile.Skill6_CUCC_List[0];
                                            skillCustomizeOriginalFile.SkillAwa_CUCC_List[c] = skillCustomizeModFile.SkillAwa_CUCC_List[0];
                                            skillCustomizeOriginalFile.Skill1_Priority_List[c] = skillCustomizeModFile.Skill1_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill2_Priority_List[c] = skillCustomizeModFile.Skill2_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill3_Priority_List[c] = skillCustomizeModFile.Skill3_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill4_Priority_List[c] = skillCustomizeModFile.Skill4_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill5_Priority_List[c] = skillCustomizeModFile.Skill5_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill6_Priority_List[c] = skillCustomizeModFile.Skill6_Priority_List[0];
                                            skillCustomizeOriginalFile.SkillAwa_Priority_List[c] = skillCustomizeModFile.SkillAwa_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill1ex_Priority_List[c] = skillCustomizeModFile.Skill1ex_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill2ex_Priority_List[c] = skillCustomizeModFile.Skill2ex_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill3ex_Priority_List[c] = skillCustomizeModFile.Skill3ex_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill4ex_Priority_List[c] = skillCustomizeModFile.Skill4ex_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill5ex_Priority_List[c] = skillCustomizeModFile.Skill5ex_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill6ex_Priority_List[c] = skillCustomizeModFile.Skill6ex_Priority_List[0];
                                            skillCustomizeOriginalFile.SkillAwaex_Priority_List[c] = skillCustomizeModFile.SkillAwaex_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill1air_Priority_List[c] = skillCustomizeModFile.Skill1air_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill2air_Priority_List[c] = skillCustomizeModFile.Skill2air_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill3air_Priority_List[c] = skillCustomizeModFile.Skill3air_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill4air_Priority_List[c] = skillCustomizeModFile.Skill4air_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill5air_Priority_List[c] = skillCustomizeModFile.Skill5air_Priority_List[0];
                                            skillCustomizeOriginalFile.Skill6air_Priority_List[c] = skillCustomizeModFile.Skill6air_Priority_List[0];
                                            skillCustomizeOriginalFile.SkillAwaair_Priority_List[c] = skillCustomizeModFile.SkillAwaair_Priority_List[0];

                                        }
                                    }

                                } else { //If mod doesn't replaces character, it will add new entry
                                    skillCustomizeOriginalFile.CharacodeList.Add(BitConverter.GetBytes(CharacodeID));
                                    skillCustomizeOriginalFile.Skill1List.Add(skillCustomizeModFile.Skill1List[0]);
                                    skillCustomizeOriginalFile.Skill2List.Add(skillCustomizeModFile.Skill2List[0]);
                                    skillCustomizeOriginalFile.Skill3List.Add(skillCustomizeModFile.Skill3List[0]);
                                    skillCustomizeOriginalFile.Skill4List.Add(skillCustomizeModFile.Skill4List[0]);
                                    skillCustomizeOriginalFile.Skill5List.Add(skillCustomizeModFile.Skill5List[0]);
                                    skillCustomizeOriginalFile.Skill6List.Add(skillCustomizeModFile.Skill6List[0]);
                                    skillCustomizeOriginalFile.SkillAwaList.Add(skillCustomizeModFile.SkillAwaList[0]);
                                    skillCustomizeOriginalFile.Skill1_ex_List.Add(skillCustomizeModFile.Skill1_ex_List[0]);
                                    skillCustomizeOriginalFile.Skill2_ex_List.Add(skillCustomizeModFile.Skill2_ex_List[0]);
                                    skillCustomizeOriginalFile.Skill3_ex_List.Add(skillCustomizeModFile.Skill3_ex_List[0]);
                                    skillCustomizeOriginalFile.Skill4_ex_List.Add(skillCustomizeModFile.Skill4_ex_List[0]);
                                    skillCustomizeOriginalFile.Skill5_ex_List.Add(skillCustomizeModFile.Skill5_ex_List[0]);
                                    skillCustomizeOriginalFile.Skill6_ex_List.Add(skillCustomizeModFile.Skill6_ex_List[0]);
                                    skillCustomizeOriginalFile.SkillAwa_ex_List.Add(skillCustomizeModFile.SkillAwa_ex_List[0]);
                                    skillCustomizeOriginalFile.Skill1_air_List.Add(skillCustomizeModFile.Skill1_air_List[0]);
                                    skillCustomizeOriginalFile.Skill2_air_List.Add(skillCustomizeModFile.Skill2_air_List[0]);
                                    skillCustomizeOriginalFile.Skill3_air_List.Add(skillCustomizeModFile.Skill3_air_List[0]);
                                    skillCustomizeOriginalFile.Skill4_air_List.Add(skillCustomizeModFile.Skill4_air_List[0]);
                                    skillCustomizeOriginalFile.Skill5_air_List.Add(skillCustomizeModFile.Skill5_air_List[0]);
                                    skillCustomizeOriginalFile.Skill6_air_List.Add(skillCustomizeModFile.Skill6_air_List[0]);
                                    skillCustomizeOriginalFile.SkillAwa_air_List.Add(skillCustomizeModFile.SkillAwa_air_List[0]);
                                    skillCustomizeOriginalFile.Skill1_CUC_List.Add(skillCustomizeModFile.Skill1_CUC_List[0]);
                                    skillCustomizeOriginalFile.Skill2_CUC_List.Add(skillCustomizeModFile.Skill2_CUC_List[0]);
                                    skillCustomizeOriginalFile.Skill3_CUC_List.Add(skillCustomizeModFile.Skill3_CUC_List[0]);
                                    skillCustomizeOriginalFile.Skill4_CUC_List.Add(skillCustomizeModFile.Skill4_CUC_List[0]);
                                    skillCustomizeOriginalFile.Skill5_CUC_List.Add(skillCustomizeModFile.Skill5_CUC_List[0]);
                                    skillCustomizeOriginalFile.Skill6_CUC_List.Add(skillCustomizeModFile.Skill6_CUC_List[0]);
                                    skillCustomizeOriginalFile.SkillAwa_CUC_List.Add(skillCustomizeModFile.SkillAwa_CUC_List[0]);
                                    skillCustomizeOriginalFile.Skill1_CUCC_List.Add(skillCustomizeModFile.Skill1_CUCC_List[0]);
                                    skillCustomizeOriginalFile.Skill2_CUCC_List.Add(skillCustomizeModFile.Skill2_CUCC_List[0]);
                                    skillCustomizeOriginalFile.Skill3_CUCC_List.Add(skillCustomizeModFile.Skill3_CUCC_List[0]);
                                    skillCustomizeOriginalFile.Skill4_CUCC_List.Add(skillCustomizeModFile.Skill4_CUCC_List[0]);
                                    skillCustomizeOriginalFile.Skill5_CUCC_List.Add(skillCustomizeModFile.Skill5_CUCC_List[0]);
                                    skillCustomizeOriginalFile.Skill6_CUCC_List.Add(skillCustomizeModFile.Skill6_CUCC_List[0]);
                                    skillCustomizeOriginalFile.SkillAwa_CUCC_List.Add(skillCustomizeModFile.SkillAwa_CUCC_List[0]);
                                    skillCustomizeOriginalFile.Skill1_Priority_List.Add(skillCustomizeModFile.Skill1_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill2_Priority_List.Add(skillCustomizeModFile.Skill2_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill3_Priority_List.Add(skillCustomizeModFile.Skill3_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill4_Priority_List.Add(skillCustomizeModFile.Skill4_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill5_Priority_List.Add(skillCustomizeModFile.Skill5_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill6_Priority_List.Add(skillCustomizeModFile.Skill6_Priority_List[0]);
                                    skillCustomizeOriginalFile.SkillAwa_Priority_List.Add(skillCustomizeModFile.SkillAwa_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill1ex_Priority_List.Add(skillCustomizeModFile.Skill1ex_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill2ex_Priority_List.Add(skillCustomizeModFile.Skill2ex_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill3ex_Priority_List.Add(skillCustomizeModFile.Skill3ex_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill4ex_Priority_List.Add(skillCustomizeModFile.Skill4ex_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill5ex_Priority_List.Add(skillCustomizeModFile.Skill5ex_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill6ex_Priority_List.Add(skillCustomizeModFile.Skill6ex_Priority_List[0]);
                                    skillCustomizeOriginalFile.SkillAwaex_Priority_List.Add(skillCustomizeModFile.SkillAwaex_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill1air_Priority_List.Add(skillCustomizeModFile.Skill1air_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill2air_Priority_List.Add(skillCustomizeModFile.Skill2air_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill3air_Priority_List.Add(skillCustomizeModFile.Skill3air_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill4air_Priority_List.Add(skillCustomizeModFile.Skill4air_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill5air_Priority_List.Add(skillCustomizeModFile.Skill5air_Priority_List[0]);
                                    skillCustomizeOriginalFile.Skill6air_Priority_List.Add(skillCustomizeModFile.Skill6air_Priority_List[0]);
                                    skillCustomizeOriginalFile.SkillAwaair_Priority_List.Add(skillCustomizeModFile.SkillAwaair_Priority_List[0]);
                                    skillCustomizeOriginalFile.EntryCount++;
                                }
                                //Creates directory
                                if (!Directory.Exists(datawin32Path + "\\spc\\WIN64")) {
                                    Directory.CreateDirectory(datawin32Path + "\\spc\\WIN64");
                                }
                                //Saves edited skillCustomizeParam file
                                skillCustomizeOriginalFile.SaveFileAs(datawin32Path + "\\spc\\WIN64\\skillCustomizeParam.xfbin");
                            }
                            if (spskillCustomizeExist) {
                                //This function merges spSkillCustomizeParam files
                                Tool_SpSkillCustomizeParamEditor_code spSkillCustomizeModFile = new Tool_SpSkillCustomizeParamEditor_code();
                                Tool_SpSkillCustomizeParamEditor_code spSkillCustomizeOriginalFile = new Tool_SpSkillCustomizeParamEditor_code();
                                spSkillCustomizeModFile.OpenFile(ModspskillCustomizePath);
                                if (File.Exists(spSkillCustomizePath))
                                    spSkillCustomizeOriginalFile.OpenFile(spSkillCustomizePath);
                                else {
                                    spSkillCustomizeOriginalFile.OpenFile(originalspSkillCustomizeParamPath);
                                }
                                if (ReplaceCharacterList[i]) { //If mod replaces character, it will change exist entry
                                    for (int c = 0; c < spSkillCustomizeOriginalFile.EntryCount; c++) {
                                        if (MainFunctions.b_byteArrayToInt(spSkillCustomizeOriginalFile.CharacodeList[c]) == MainFunctions.b_byteArrayToInt(spSkillCustomizeModFile.CharacodeList[0])) {
                                            spSkillCustomizeOriginalFile.spl1_chUsageCountValueList[c] = spSkillCustomizeModFile.spl1_chUsageCountValueList[0];
                                            spSkillCustomizeOriginalFile.spl2_chUsageCountValueList[c] = spSkillCustomizeModFile.spl2_chUsageCountValueList[0];
                                            spSkillCustomizeOriginalFile.spl3_chUsageCountValueList[c] = spSkillCustomizeModFile.spl3_chUsageCountValueList[0];
                                            spSkillCustomizeOriginalFile.spl4_chUsageCountValueList[c] = spSkillCustomizeModFile.spl4_chUsageCountValueList[0];
                                            spSkillCustomizeOriginalFile.spl1_chUsageCountValueListFloat[c] = spSkillCustomizeModFile.spl1_chUsageCountValueListFloat[0];
                                            spSkillCustomizeOriginalFile.spl2_chUsageCountValueListFloat[c] = spSkillCustomizeModFile.spl2_chUsageCountValueListFloat[0];
                                            spSkillCustomizeOriginalFile.spl3_chUsageCountValueListFloat[c] = spSkillCustomizeModFile.spl3_chUsageCountValueListFloat[0];
                                            spSkillCustomizeOriginalFile.spl4_chUsageCountValueListFloat[c] = spSkillCustomizeModFile.spl4_chUsageCountValueListFloat[0];
                                            spSkillCustomizeOriginalFile.spl1_NameList[c] = spSkillCustomizeModFile.spl1_NameList[0];
                                            spSkillCustomizeOriginalFile.spl2_NameList[c] = spSkillCustomizeModFile.spl2_NameList[0];
                                            spSkillCustomizeOriginalFile.spl3_NameList[c] = spSkillCustomizeModFile.spl3_NameList[0];
                                            spSkillCustomizeOriginalFile.spl4_NameList[c] = spSkillCustomizeModFile.spl4_NameList[0];
                                            spSkillCustomizeOriginalFile.spl1_PriorList[c] = spSkillCustomizeModFile.spl1_PriorList[0];
                                            spSkillCustomizeOriginalFile.spl2_PriorList[c] = spSkillCustomizeModFile.spl2_PriorList[0];
                                            spSkillCustomizeOriginalFile.spl3_PriorList[c] = spSkillCustomizeModFile.spl3_PriorList[0];
                                            spSkillCustomizeOriginalFile.spl4_PriorList[c] = spSkillCustomizeModFile.spl4_PriorList[0];
                                            spSkillCustomizeOriginalFile.WeirdValuesList[c] = spSkillCustomizeModFile.WeirdValuesList[0];
                                        }
                                    }
                                } else {//If mod doesn't replaces character, it will add new entry
                                    spSkillCustomizeOriginalFile.CharacodeList.Add(BitConverter.GetBytes(CharacodeID));
                                    spSkillCustomizeOriginalFile.spl1_chUsageCountValueList.Add(spSkillCustomizeModFile.spl1_chUsageCountValueList[0]);
                                    spSkillCustomizeOriginalFile.spl2_chUsageCountValueList.Add(spSkillCustomizeModFile.spl2_chUsageCountValueList[0]);
                                    spSkillCustomizeOriginalFile.spl3_chUsageCountValueList.Add(spSkillCustomizeModFile.spl3_chUsageCountValueList[0]);
                                    spSkillCustomizeOriginalFile.spl4_chUsageCountValueList.Add(spSkillCustomizeModFile.spl4_chUsageCountValueList[0]);
                                    spSkillCustomizeOriginalFile.spl1_chUsageCountValueListFloat.Add(spSkillCustomizeModFile.spl1_chUsageCountValueListFloat[0]);
                                    spSkillCustomizeOriginalFile.spl2_chUsageCountValueListFloat.Add(spSkillCustomizeModFile.spl2_chUsageCountValueListFloat[0]);
                                    spSkillCustomizeOriginalFile.spl3_chUsageCountValueListFloat.Add(spSkillCustomizeModFile.spl3_chUsageCountValueListFloat[0]);
                                    spSkillCustomizeOriginalFile.spl4_chUsageCountValueListFloat.Add(spSkillCustomizeModFile.spl4_chUsageCountValueListFloat[0]);
                                    spSkillCustomizeOriginalFile.spl1_PriorList.Add(spSkillCustomizeModFile.spl1_PriorList[0]);
                                    spSkillCustomizeOriginalFile.spl2_PriorList.Add(spSkillCustomizeModFile.spl2_PriorList[0]);
                                    spSkillCustomizeOriginalFile.spl3_PriorList.Add(spSkillCustomizeModFile.spl3_PriorList[0]);
                                    spSkillCustomizeOriginalFile.spl4_PriorList.Add(spSkillCustomizeModFile.spl4_PriorList[0]);
                                    spSkillCustomizeOriginalFile.spl1_NameList.Add(spSkillCustomizeModFile.spl1_NameList[0]);
                                    spSkillCustomizeOriginalFile.spl2_NameList.Add(spSkillCustomizeModFile.spl2_NameList[0]);
                                    spSkillCustomizeOriginalFile.spl3_NameList.Add(spSkillCustomizeModFile.spl3_NameList[0]);
                                    spSkillCustomizeOriginalFile.spl4_NameList.Add(spSkillCustomizeModFile.spl4_NameList[0]);
                                    spSkillCustomizeOriginalFile.WeirdValuesList.Add(spSkillCustomizeModFile.WeirdValuesList[0]);
                                    spSkillCustomizeOriginalFile.EntryCount++;
                                }
                                //Creates directory
                                if (!Directory.Exists(datawin32Path + "\\spc\\WIN64")) {
                                    Directory.CreateDirectory(datawin32Path + "\\spc\\WIN64");
                                }
                                //Saves edited spSkillCustomizeParam
                                spSkillCustomizeOriginalFile.SaveFileAs(datawin32Path + "\\spc\\WIN64\\spSkillCustomizeParam.xfbin");
                            }
                            if (spTypeSupportParamExist) {
                                //This function merges spTypeSupportParam files
                                Tool_spTypeSupportParamEditor_code spTypeSupportParamModFile = new Tool_spTypeSupportParamEditor_code();
                                Tool_spTypeSupportParamEditor_code spTypeSupportParamOriginalFile = new Tool_spTypeSupportParamEditor_code();
                                spTypeSupportParamModFile.OpenFile(ModspTypeSupportParamPath);
                                if (File.Exists(spTypeSupportParamPath))
                                    spTypeSupportParamOriginalFile.OpenFile(spTypeSupportParamPath);
                                else {
                                    spTypeSupportParamOriginalFile.OpenFile(originalspTypeSupportParamPath);
                                }
                                if (ReplaceCharacterList[i]) { //If mod replaces character, it will change exist entry
                                    for (int c = 0; c < spTypeSupportParamOriginalFile.EntryCount; c++) {
                                        if (spTypeSupportParamOriginalFile.Characode_List[c] == spTypeSupportParamModFile.Characode_List[0]) {
                                            spTypeSupportParamOriginalFile.Type_List[c] = spTypeSupportParamModFile.Type_List[0];
                                            spTypeSupportParamOriginalFile.Mode_List[c] = spTypeSupportParamModFile.Mode_List[0];

                                            spTypeSupportParamOriginalFile.LeftSkillName_List[c] = spTypeSupportParamModFile.LeftSkillName_List[0];
                                            spTypeSupportParamOriginalFile.LeftSkill_unk1_List[c] = spTypeSupportParamModFile.LeftSkill_unk1_List[0];
                                            spTypeSupportParamOriginalFile.LeftSkill_unk2_List[c] = spTypeSupportParamModFile.LeftSkill_unk2_List[0];
                                            spTypeSupportParamOriginalFile.LeftSkill_unk3_List[c] = spTypeSupportParamModFile.LeftSkill_unk3_List[0];
                                            spTypeSupportParamOriginalFile.LeftSkill_Unknown_List[c] = spTypeSupportParamModFile.LeftSkill_Unknown_List[0];
                                            spTypeSupportParamOriginalFile.LeftSkill_EnableInAir_List[c] = spTypeSupportParamModFile.LeftSkill_EnableInAir_List[0];

                                            spTypeSupportParamOriginalFile.RightSkillName_List[c] = spTypeSupportParamModFile.RightSkillName_List[0];
                                            spTypeSupportParamOriginalFile.RightSkill_unk1_List[c] = spTypeSupportParamModFile.RightSkill_unk1_List[0];
                                            spTypeSupportParamOriginalFile.RightSkill_unk2_List[c] = spTypeSupportParamModFile.RightSkill_unk2_List[0];
                                            spTypeSupportParamOriginalFile.RightSkill_unk3_List[c] = spTypeSupportParamModFile.RightSkill_unk3_List[0];
                                            spTypeSupportParamOriginalFile.RightSkill_Unknown_List[c] = spTypeSupportParamModFile.RightSkill_Unknown_List[0];
                                            spTypeSupportParamOriginalFile.RightSkill_EnableInAir_List[c] = spTypeSupportParamModFile.RightSkill_EnableInAir_List[0];

                                            spTypeSupportParamOriginalFile.UpSkillName_List[c] = spTypeSupportParamModFile.UpSkillName_List[0];
                                            spTypeSupportParamOriginalFile.UpSkill_unk1_List[c] = spTypeSupportParamModFile.UpSkill_unk1_List[0];
                                            spTypeSupportParamOriginalFile.UpSkill_unk2_List[c] = spTypeSupportParamModFile.UpSkill_unk2_List[0];
                                            spTypeSupportParamOriginalFile.UpSkill_unk3_List[c] = spTypeSupportParamModFile.UpSkill_unk3_List[0];
                                            spTypeSupportParamOriginalFile.UpSkill_Unknown_List[c] = spTypeSupportParamModFile.UpSkill_Unknown_List[0];
                                            spTypeSupportParamOriginalFile.UpSkill_EnableInAir_List[c] = spTypeSupportParamModFile.UpSkill_EnableInAir_List[0];

                                            spTypeSupportParamOriginalFile.DownSkillName_List[c] = spTypeSupportParamModFile.DownSkillName_List[0];
                                            spTypeSupportParamOriginalFile.DownSkill_unk1_List[c] = spTypeSupportParamModFile.DownSkill_unk1_List[0];
                                            spTypeSupportParamOriginalFile.DownSkill_unk2_List[c] = spTypeSupportParamModFile.DownSkill_unk2_List[0];
                                            spTypeSupportParamOriginalFile.DownSkill_unk3_List[c] = spTypeSupportParamModFile.DownSkill_unk3_List[0];
                                            spTypeSupportParamOriginalFile.DownSkill_Unknown_List[c] = spTypeSupportParamModFile.DownSkill_Unknown_List[0];
                                            spTypeSupportParamOriginalFile.DownSkill_EnableInAir_List[c] = spTypeSupportParamModFile.DownSkill_EnableInAir_List[0];
                                        } else {//If mod replaces character, but it doesnt have entry for it, it will add new entry
                                            spTypeSupportParamOriginalFile.Characode_List.Add(spTypeSupportParamModFile.Characode_List[0]);
                                            spTypeSupportParamOriginalFile.Type_List.Add(spTypeSupportParamModFile.Type_List[0]);
                                            spTypeSupportParamOriginalFile.Mode_List.Add(spTypeSupportParamModFile.Mode_List[0]);
                                            spTypeSupportParamOriginalFile.LeftSkillName_List.Add(spTypeSupportParamModFile.LeftSkillName_List[0]);
                                            spTypeSupportParamOriginalFile.LeftSkill_unk1_List.Add(spTypeSupportParamModFile.LeftSkill_unk1_List[0]);
                                            spTypeSupportParamOriginalFile.LeftSkill_unk2_List.Add(spTypeSupportParamModFile.LeftSkill_unk2_List[0]);
                                            spTypeSupportParamOriginalFile.LeftSkill_unk3_List.Add(spTypeSupportParamModFile.LeftSkill_unk3_List[0]);
                                            spTypeSupportParamOriginalFile.LeftSkill_Unknown_List.Add(spTypeSupportParamModFile.LeftSkill_Unknown_List[0]);
                                            spTypeSupportParamOriginalFile.LeftSkill_EnableInAir_List.Add(spTypeSupportParamModFile.LeftSkill_EnableInAir_List[0]);
                                            spTypeSupportParamOriginalFile.RightSkillName_List.Add(spTypeSupportParamModFile.RightSkillName_List[0]);
                                            spTypeSupportParamOriginalFile.RightSkill_unk1_List.Add(spTypeSupportParamModFile.RightSkill_unk1_List[0]);
                                            spTypeSupportParamOriginalFile.RightSkill_unk2_List.Add(spTypeSupportParamModFile.RightSkill_unk2_List[0]);
                                            spTypeSupportParamOriginalFile.RightSkill_unk3_List.Add(spTypeSupportParamModFile.RightSkill_unk3_List[0]);
                                            spTypeSupportParamOriginalFile.RightSkill_Unknown_List.Add(spTypeSupportParamModFile.RightSkill_Unknown_List[0]);
                                            spTypeSupportParamOriginalFile.RightSkill_EnableInAir_List.Add(spTypeSupportParamModFile.RightSkill_EnableInAir_List[0]);
                                            spTypeSupportParamOriginalFile.UpSkillName_List.Add(spTypeSupportParamModFile.UpSkillName_List[0]);
                                            spTypeSupportParamOriginalFile.UpSkill_unk1_List.Add(spTypeSupportParamModFile.UpSkill_unk1_List[0]);
                                            spTypeSupportParamOriginalFile.UpSkill_unk2_List.Add(spTypeSupportParamModFile.UpSkill_unk2_List[0]);
                                            spTypeSupportParamOriginalFile.UpSkill_unk3_List.Add(spTypeSupportParamModFile.UpSkill_unk3_List[0]);
                                            spTypeSupportParamOriginalFile.UpSkill_Unknown_List.Add(spTypeSupportParamModFile.UpSkill_Unknown_List[0]);
                                            spTypeSupportParamOriginalFile.UpSkill_EnableInAir_List.Add(spTypeSupportParamModFile.UpSkill_EnableInAir_List[0]);
                                            spTypeSupportParamOriginalFile.DownSkillName_List.Add(spTypeSupportParamModFile.DownSkillName_List[0]);
                                            spTypeSupportParamOriginalFile.DownSkill_unk1_List.Add(spTypeSupportParamModFile.DownSkill_unk1_List[0]);
                                            spTypeSupportParamOriginalFile.DownSkill_unk2_List.Add(spTypeSupportParamModFile.DownSkill_unk2_List[0]);
                                            spTypeSupportParamOriginalFile.DownSkill_unk3_List.Add(spTypeSupportParamModFile.DownSkill_unk3_List[0]);
                                            spTypeSupportParamOriginalFile.DownSkill_Unknown_List.Add(spTypeSupportParamModFile.DownSkill_Unknown_List[0]);
                                            spTypeSupportParamOriginalFile.DownSkill_EnableInAir_List.Add(spTypeSupportParamModFile.DownSkill_EnableInAir_List[0]);
                                            spTypeSupportParamOriginalFile.EntryCount++;
                                        }
                                    }
                                } else {//If mod doesn't replaces character, it will add new entry
                                    spTypeSupportParamOriginalFile.Characode_List.Add(CharacodeID);
                                    spTypeSupportParamOriginalFile.Type_List.Add(spTypeSupportParamModFile.Type_List[0]);
                                    spTypeSupportParamOriginalFile.Mode_List.Add(spTypeSupportParamModFile.Mode_List[0]);
                                    spTypeSupportParamOriginalFile.LeftSkillName_List.Add(spTypeSupportParamModFile.LeftSkillName_List[0]);
                                    spTypeSupportParamOriginalFile.LeftSkill_unk1_List.Add(spTypeSupportParamModFile.LeftSkill_unk1_List[0]);
                                    spTypeSupportParamOriginalFile.LeftSkill_unk2_List.Add(spTypeSupportParamModFile.LeftSkill_unk2_List[0]);
                                    spTypeSupportParamOriginalFile.LeftSkill_unk3_List.Add(spTypeSupportParamModFile.LeftSkill_unk3_List[0]);
                                    spTypeSupportParamOriginalFile.LeftSkill_Unknown_List.Add(spTypeSupportParamModFile.LeftSkill_Unknown_List[0]);
                                    spTypeSupportParamOriginalFile.LeftSkill_EnableInAir_List.Add(spTypeSupportParamModFile.LeftSkill_EnableInAir_List[0]);
                                    spTypeSupportParamOriginalFile.RightSkillName_List.Add(spTypeSupportParamModFile.RightSkillName_List[0]);
                                    spTypeSupportParamOriginalFile.RightSkill_unk1_List.Add(spTypeSupportParamModFile.RightSkill_unk1_List[0]);
                                    spTypeSupportParamOriginalFile.RightSkill_unk2_List.Add(spTypeSupportParamModFile.RightSkill_unk2_List[0]);
                                    spTypeSupportParamOriginalFile.RightSkill_unk3_List.Add(spTypeSupportParamModFile.RightSkill_unk3_List[0]);
                                    spTypeSupportParamOriginalFile.RightSkill_Unknown_List.Add(spTypeSupportParamModFile.RightSkill_Unknown_List[0]);
                                    spTypeSupportParamOriginalFile.RightSkill_EnableInAir_List.Add(spTypeSupportParamModFile.RightSkill_EnableInAir_List[0]);
                                    spTypeSupportParamOriginalFile.UpSkillName_List.Add(spTypeSupportParamModFile.UpSkillName_List[0]);
                                    spTypeSupportParamOriginalFile.UpSkill_unk1_List.Add(spTypeSupportParamModFile.UpSkill_unk1_List[0]);
                                    spTypeSupportParamOriginalFile.UpSkill_unk2_List.Add(spTypeSupportParamModFile.UpSkill_unk2_List[0]);
                                    spTypeSupportParamOriginalFile.UpSkill_unk3_List.Add(spTypeSupportParamModFile.UpSkill_unk3_List[0]);
                                    spTypeSupportParamOriginalFile.UpSkill_Unknown_List.Add(spTypeSupportParamModFile.UpSkill_Unknown_List[0]);
                                    spTypeSupportParamOriginalFile.UpSkill_EnableInAir_List.Add(spTypeSupportParamModFile.UpSkill_EnableInAir_List[0]);
                                    spTypeSupportParamOriginalFile.DownSkillName_List.Add(spTypeSupportParamModFile.DownSkillName_List[0]);
                                    spTypeSupportParamOriginalFile.DownSkill_unk1_List.Add(spTypeSupportParamModFile.DownSkill_unk1_List[0]);
                                    spTypeSupportParamOriginalFile.DownSkill_unk2_List.Add(spTypeSupportParamModFile.DownSkill_unk2_List[0]);
                                    spTypeSupportParamOriginalFile.DownSkill_unk3_List.Add(spTypeSupportParamModFile.DownSkill_unk3_List[0]);
                                    spTypeSupportParamOriginalFile.DownSkill_Unknown_List.Add(spTypeSupportParamModFile.DownSkill_Unknown_List[0]);
                                    spTypeSupportParamOriginalFile.DownSkill_EnableInAir_List.Add(spTypeSupportParamModFile.DownSkill_EnableInAir_List[0]);
                                    spTypeSupportParamOriginalFile.EntryCount++;
                                }
                                //Creates directory
                                if (!Directory.Exists(datawin32Path + "\\spc\\WIN64")) {
                                    Directory.CreateDirectory(datawin32Path + "\\spc\\WIN64");
                                }
                                //Saves edited spTypeSupportParam file
                                spTypeSupportParamOriginalFile.SaveFileAs(datawin32Path + "\\spc\\WIN64\\spTypeSupportParam.xfbin");
                            }
                            if (awakeAuraExist) {
                                //This function merges awakeAura files
                                Tool_AwakeAuraEditor awakeAuraModFile = new Tool_AwakeAuraEditor();
                                Tool_AwakeAuraEditor awakeAuraOriginalFile = new Tool_AwakeAuraEditor();
                                awakeAuraModFile.OpenFile(ModawakeAuraPath);
                                if (File.Exists(awakeAuraPath))
                                    awakeAuraOriginalFile.OpenFile(awakeAuraPath);
                                else {
                                    awakeAuraOriginalFile.OpenFile(originalawakeAuraPath);
                                }
                                for (int c = 0; c < awakeAuraOriginalFile.EntryCount; c++) {//This function removes entry from vanilla/edited file which contain mod characode
                                    if (awakeAuraOriginalFile.CharacodeList[c] == d.Name) {
                                        awakeAuraOriginalFile.CharacodeList.RemoveAt(c);
                                        awakeAuraOriginalFile.SkillFileList.RemoveAt(c);
                                        awakeAuraOriginalFile.EffectList.RemoveAt(c);
                                        awakeAuraOriginalFile.MainBoneList.RemoveAt(c);
                                        awakeAuraOriginalFile.SecondBoneList.RemoveAt(c);
                                        awakeAuraOriginalFile.AwakeModeValue_false_List.RemoveAt(c);
                                        awakeAuraOriginalFile.AwakeModeValue_true_List.RemoveAt(c);
                                        awakeAuraOriginalFile.SecondBoneValue_1_List.RemoveAt(c);
                                        awakeAuraOriginalFile.SecondBoneValue_2_List.RemoveAt(c);
                                        awakeAuraOriginalFile.SecondBoneValue_3_List.RemoveAt(c);
                                        awakeAuraOriginalFile.ConstantValue_List.RemoveAt(c);
                                        awakeAuraOriginalFile.EntryCount--;
                                        c--;
                                    }
                                }
                                for (int c = 0; c < awakeAuraModFile.EntryCount; c++) {
                                    if (awakeAuraModFile.CharacodeList[c] == d.Name) { //This function adds entry to vanilla/edited file
                                        awakeAuraOriginalFile.CharacodeList.Add(awakeAuraModFile.CharacodeList[c]);
                                        awakeAuraOriginalFile.SkillFileList.Add(awakeAuraModFile.SkillFileList[c]);
                                        awakeAuraOriginalFile.EffectList.Add(awakeAuraModFile.EffectList[c]);
                                        awakeAuraOriginalFile.MainBoneList.Add(awakeAuraModFile.MainBoneList[c]);
                                        awakeAuraOriginalFile.SecondBoneList.Add(awakeAuraModFile.SecondBoneList[c]);
                                        awakeAuraOriginalFile.AwakeModeValue_false_List.Add(awakeAuraModFile.AwakeModeValue_false_List[c]);
                                        awakeAuraOriginalFile.AwakeModeValue_true_List.Add(awakeAuraModFile.AwakeModeValue_true_List[c]);
                                        awakeAuraOriginalFile.SecondBoneValue_1_List.Add(awakeAuraModFile.SecondBoneValue_1_List[c]);
                                        awakeAuraOriginalFile.SecondBoneValue_2_List.Add(awakeAuraModFile.SecondBoneValue_2_List[c]);
                                        awakeAuraOriginalFile.SecondBoneValue_3_List.Add(awakeAuraModFile.SecondBoneValue_3_List[c]);
                                        awakeAuraOriginalFile.ConstantValue_List.Add(awakeAuraModFile.ConstantValue_List[c]);
                                        awakeAuraOriginalFile.EntryCount++;
                                    }

                                }
                                //Creates directory
                                if (!Directory.Exists(datawin32Path + "\\spc\\WIN64")) {
                                    Directory.CreateDirectory(datawin32Path + "\\spc\\WIN64");
                                }
                                //saves edited awakeAura file
                                awakeAuraOriginalFile.SaveFileAs(datawin32Path + "\\spc\\WIN64\\awakeAura.xfbin");
                            }
                            if (iconExist) {
                                //This function merges player_icon files
                                Tool_IconEditor_code IconModFile = new Tool_IconEditor_code();
                                Tool_IconEditor_code IconOriginalFile = new Tool_IconEditor_code();
                                IconModFile.OpenFile(ModiconPath);
                                if (File.Exists(iconPath))
                                    IconOriginalFile.OpenFile(iconPath);
                                else {
                                    IconOriginalFile.OpenFile(originalIconPath);
                                }
                                for (int c = 0; c < IconOriginalFile.EntryCount; c++) { //This function deletes entries which contain mod characode
                                    if (MainFunctions.b_byteArrayToInt(IconOriginalFile.CharacodeList[c]) == CharacodeID) {
                                        IconOriginalFile.CharacodeList.RemoveAt(c);
                                        IconOriginalFile.CostumeList.RemoveAt(c);
                                        IconOriginalFile.IconList.RemoveAt(c);
                                        IconOriginalFile.AwaIconList.RemoveAt(c);
                                        IconOriginalFile.NameList.RemoveAt(c);
                                        IconOriginalFile.ExNinjutsuList.RemoveAt(c);
                                        IconOriginalFile.EntryCount--;
                                        c--;
                                    }
                                }
                                for (int c = 0; c < IconModFile.EntryCount; c++) { //This function adds entries from mod to vanilla/edited file
                                    if (MainFunctions.b_byteArrayToInt(IconModFile.CharacodeList[c]) == OldCharacode) {
                                        IconOriginalFile.CharacodeList.Add(BitConverter.GetBytes(CharacodeID));
                                        IconOriginalFile.CostumeList.Add(IconModFile.CostumeList[c]);
                                        IconOriginalFile.IconList.Add(IconModFile.IconList[c]);
                                        IconOriginalFile.AwaIconList.Add(IconModFile.AwaIconList[c]);
                                        IconOriginalFile.NameList.Add(IconModFile.NameList[c]);
                                        IconOriginalFile.ExNinjutsuList.Add(IconModFile.ExNinjutsuList[c]);
                                        IconOriginalFile.EntryCount++;
                                    }

                                }
                                //Creates directory
                                if (!Directory.Exists(datawin32Path + "\\spc\\WIN64")) {
                                    Directory.CreateDirectory(datawin32Path + "\\spc\\WIN64");
                                }
                                //saves edited player_icon file
                                IconOriginalFile.SaveFileAs(datawin32Path + "\\spc\\WIN64\\player_icon.xfbin");
                            }
                            if (appearanceAnmExist) {
                                //This function merges appearanceAnm files
                                Tool_appearenceAnmEditor_code AppearanceModFile = new Tool_appearenceAnmEditor_code();
                                Tool_appearenceAnmEditor_code AppearanceOriginalFile = new Tool_appearenceAnmEditor_code();
                                AppearanceModFile.OpenFile(ModappearanceAnmPath);
                                if (File.Exists(appearanceAnmPath))
                                    AppearanceOriginalFile.OpenFile(appearanceAnmPath);
                                else {
                                    AppearanceOriginalFile.OpenFile(originalappearanceAnmPath);
                                }
                                for (int c = 0; c < AppearanceOriginalFile.EntryCount; c++) {//This code removes all entries with mod characode from vanilla/edited file
                                    if (MainFunctions.b_byteArrayToInt(AppearanceOriginalFile.CharacodeList[c]) == CharacodeID) {
                                        AppearanceOriginalFile.CharacodeList.RemoveAt(c);
                                        AppearanceOriginalFile.MeshList.RemoveAt(c);
                                        AppearanceOriginalFile.SlotList.RemoveAt(c);
                                        AppearanceOriginalFile.TypeSectionList.RemoveAt(c);
                                        AppearanceOriginalFile.EnableDisableList.RemoveAt(c);
                                        AppearanceOriginalFile.NormalStateList.RemoveAt(c);
                                        AppearanceOriginalFile.AwakeningStateList.RemoveAt(c);
                                        AppearanceOriginalFile.ReverseSectionList.RemoveAt(c);
                                        AppearanceOriginalFile.EnableDisableCutNCList.RemoveAt(c);
                                        AppearanceOriginalFile.EnableDisableUltList.RemoveAt(c);
                                        AppearanceOriginalFile.EnableDisableWinList.RemoveAt(c);
                                        AppearanceOriginalFile.EnableDisableArmorBreakList.RemoveAt(c);
                                        AppearanceOriginalFile.TimingAwakeList.RemoveAt(c);
                                        AppearanceOriginalFile.TransparenceList.RemoveAt(c);
                                        AppearanceOriginalFile.EntryCount--;
                                        c--;
                                    }
                                }
                                for (int c = 0; c < AppearanceModFile.EntryCount; c++) {//This code adds modded entries to vanilla/edited file
                                    if (MainFunctions.b_byteArrayToInt(AppearanceModFile.CharacodeList[c]) == OldCharacode) {
                                        AppearanceOriginalFile.CharacodeList.Add(BitConverter.GetBytes(CharacodeID));
                                        AppearanceOriginalFile.MeshList.Add(AppearanceModFile.MeshList[c]);
                                        AppearanceOriginalFile.SlotList.Add(AppearanceModFile.SlotList[c]);
                                        AppearanceOriginalFile.TypeSectionList.Add(AppearanceModFile.TypeSectionList[c]);
                                        AppearanceOriginalFile.EnableDisableList.Add(AppearanceModFile.EnableDisableList[c]);
                                        AppearanceOriginalFile.NormalStateList.Add(AppearanceModFile.NormalStateList[c]);
                                        AppearanceOriginalFile.AwakeningStateList.Add(AppearanceModFile.AwakeningStateList[c]);
                                        AppearanceOriginalFile.ReverseSectionList.Add(AppearanceModFile.ReverseSectionList[c]);
                                        AppearanceOriginalFile.EnableDisableCutNCList.Add(AppearanceModFile.EnableDisableCutNCList[c]);
                                        AppearanceOriginalFile.EnableDisableUltList.Add(AppearanceModFile.EnableDisableUltList[c]);
                                        AppearanceOriginalFile.EnableDisableWinList.Add(AppearanceModFile.EnableDisableWinList[c]);
                                        AppearanceOriginalFile.EnableDisableArmorBreakList.Add(AppearanceModFile.EnableDisableArmorBreakList[c]);
                                        AppearanceOriginalFile.TimingAwakeList.Add(AppearanceModFile.TimingAwakeList[c]);
                                        AppearanceOriginalFile.TransparenceList.Add(AppearanceModFile.TransparenceList[c]);
                                        AppearanceOriginalFile.EntryCount++;
                                    }

                                }
                                //Creates directory
                                if (!Directory.Exists(datawin32Path + "\\spc\\WIN64")) {
                                    Directory.CreateDirectory(datawin32Path + "\\spc\\WIN64");
                                }
                                //Saves edited appearanceAnm file
                                AppearanceOriginalFile.SaveFileAs(datawin32Path + "\\spc\\WIN64\\appearanceAnm.xfbin");
                            }
                            if (afterAttachObjectExist) {
                                //This function merges afterAttachObject files
                                Tool_afterAttachObject_code afterAttachObjectModFile = new Tool_afterAttachObject_code();
                                Tool_afterAttachObject_code afterAttachObjectOriginalFile = new Tool_afterAttachObject_code();
                                afterAttachObjectModFile.OpenFile(ModafterAttachObjectPath);
                                if (File.Exists(afterAttachObjectPath))
                                    afterAttachObjectOriginalFile.OpenFile(afterAttachObjectPath);
                                else {
                                    afterAttachObjectOriginalFile.OpenFile(originalafterAttachObjectPath);
                                }
                                for (int c = 0; c < afterAttachObjectOriginalFile.EntryCount; c++) { //This code removes all entries with mod characode from vanilla/edited file
                                    if (afterAttachObjectOriginalFile.characode1List[c] == d.Name) {
                                        afterAttachObjectOriginalFile.characode1List.RemoveAt(c);
                                        afterAttachObjectOriginalFile.characode2List.RemoveAt(c);
                                        afterAttachObjectOriginalFile.pathList.RemoveAt(c);
                                        afterAttachObjectOriginalFile.meshList.RemoveAt(c);
                                        afterAttachObjectOriginalFile.bone1List.RemoveAt(c);
                                        afterAttachObjectOriginalFile.bone2List.RemoveAt(c);
                                        afterAttachObjectOriginalFile.value1List.RemoveAt(c);
                                        afterAttachObjectOriginalFile.value2List.RemoveAt(c);
                                        afterAttachObjectOriginalFile.value3List.RemoveAt(c);
                                        afterAttachObjectOriginalFile.XPosList.RemoveAt(c);
                                        afterAttachObjectOriginalFile.YPosList.RemoveAt(c);
                                        afterAttachObjectOriginalFile.ZPosList.RemoveAt(c);
                                        afterAttachObjectOriginalFile.XRotList.RemoveAt(c);
                                        afterAttachObjectOriginalFile.YRotList.RemoveAt(c);
                                        afterAttachObjectOriginalFile.ZRotList.RemoveAt(c);
                                        afterAttachObjectOriginalFile.XScaleList.RemoveAt(c);
                                        afterAttachObjectOriginalFile.YScaleList.RemoveAt(c);
                                        afterAttachObjectOriginalFile.ZScaleList.RemoveAt(c);
                                        afterAttachObjectOriginalFile.EntryCount--;
                                        c--;
                                    }
                                }
                                for (int c = 0; c < afterAttachObjectModFile.EntryCount; c++) { //This code add modded entries to vanilla/edited file
                                    if (afterAttachObjectModFile.characode1List[c] == d.Name) {
                                        afterAttachObjectOriginalFile.characode1List.Add(afterAttachObjectModFile.characode1List[c]);
                                        afterAttachObjectOriginalFile.characode2List.Add(afterAttachObjectModFile.characode2List[c]);
                                        afterAttachObjectOriginalFile.pathList.Add(afterAttachObjectModFile.pathList[c]);
                                        afterAttachObjectOriginalFile.meshList.Add(afterAttachObjectModFile.meshList[c]);
                                        afterAttachObjectOriginalFile.bone1List.Add(afterAttachObjectModFile.bone1List[c]);
                                        afterAttachObjectOriginalFile.bone2List.Add(afterAttachObjectModFile.bone2List[c]);
                                        afterAttachObjectOriginalFile.value1List.Add(afterAttachObjectModFile.value1List[c]);
                                        afterAttachObjectOriginalFile.value2List.Add(afterAttachObjectModFile.value2List[c]);
                                        afterAttachObjectOriginalFile.value3List.Add(afterAttachObjectModFile.value3List[c]);
                                        afterAttachObjectOriginalFile.XPosList.Add(afterAttachObjectModFile.XPosList[c]);
                                        afterAttachObjectOriginalFile.YPosList.Add(afterAttachObjectModFile.YPosList[c]);
                                        afterAttachObjectOriginalFile.ZPosList.Add(afterAttachObjectModFile.ZPosList[c]);
                                        afterAttachObjectOriginalFile.XRotList.Add(afterAttachObjectModFile.XRotList[c]);
                                        afterAttachObjectOriginalFile.YRotList.Add(afterAttachObjectModFile.YRotList[c]);
                                        afterAttachObjectOriginalFile.ZRotList.Add(afterAttachObjectModFile.ZRotList[c]);
                                        afterAttachObjectOriginalFile.XScaleList.Add(afterAttachObjectModFile.XScaleList[c]);
                                        afterAttachObjectOriginalFile.YScaleList.Add(afterAttachObjectModFile.YScaleList[c]);
                                        afterAttachObjectOriginalFile.ZScaleList.Add(afterAttachObjectModFile.ZScaleList[c]);
                                        afterAttachObjectOriginalFile.EntryCount++;
                                    }

                                }
                                //Creates directory
                                if (!Directory.Exists(datawin32Path + "\\spc\\WIN64")) {
                                    Directory.CreateDirectory(datawin32Path + "\\spc\\WIN64");
                                }
                                //Saves edited afterAttachObject file
                                afterAttachObjectOriginalFile.SaveFileAs(datawin32Path + "\\spc\\WIN64\\afterAttachObject.xfbin");
                            }
                            if (cmnparamExist) {
                                //This function merges cmnparam files
                                Tool_cmnparamEditor_code cmnparamModFile = new Tool_cmnparamEditor_code();
                                Tool_cmnparamEditor_code cmnparamOriginalFile = new Tool_cmnparamEditor_code();
                                cmnparamModFile.OpenFile(ModcmnparamPath);
                                if (File.Exists(cmnparamPath))
                                    cmnparamOriginalFile.OpenFile(cmnparamPath);
                                else {
                                    cmnparamOriginalFile.OpenFile(originalcmnparamPath);
                                }
                                if (ReplaceCharacterList[i]) {
                                    //If mod replaces character, it editing exist entry
                                    bool found = false;
                                    for (int c = 0; c < cmnparamOriginalFile.EntryCount_Player; c++) {
                                        if (cmnparamOriginalFile.Player_Characode_List[c].Contains(d.Name)) {
                                            cmnparamOriginalFile.Player_PlFileName_List[c] = cmnparamModFile.Player_PlFileName_List[0];
                                            cmnparamOriginalFile.Player_PlAwaFileName_List[c] = cmnparamModFile.Player_PlAwaFileName_List[0];
                                            cmnparamOriginalFile.Player_PlAwa2FileName_List[c] = cmnparamModFile.Player_PlAwa2FileName_List[0];
                                            cmnparamOriginalFile.Player_EvName_List[c] = cmnparamModFile.Player_EvName_List[0];
                                            cmnparamOriginalFile.Player_UJEvName_List[c] = cmnparamModFile.Player_UJEvName_List[0];
                                            cmnparamOriginalFile.Player_UJ_1_CutInName_List[c] = cmnparamModFile.Player_UJ_1_CutInName_List[0];
                                            cmnparamOriginalFile.Player_UJ_1_AtkName_List[c] = cmnparamModFile.Player_UJ_1_AtkName_List[0];
                                            cmnparamOriginalFile.Player_UJ_2_CutInName_List[c] = cmnparamModFile.Player_UJ_2_CutInName_List[0];
                                            cmnparamOriginalFile.Player_UJ_2_AtkName_List[c] = cmnparamModFile.Player_UJ_2_AtkName_List[0];
                                            cmnparamOriginalFile.Player_UJ_3_CutInName_List[c] = cmnparamModFile.Player_UJ_3_CutInName_List[0];
                                            cmnparamOriginalFile.Player_UJ_3_AtkName_List[c] = cmnparamModFile.Player_UJ_3_AtkName_List[0];
                                            cmnparamOriginalFile.Player_UJ_Alt_CutInName_List[c] = cmnparamModFile.Player_UJ_Alt_CutInName_List[0];
                                            cmnparamOriginalFile.Player_UJ_Alt_AtkName_List[c] = cmnparamModFile.Player_UJ_Alt_AtkName_List[0];
                                            cmnparamOriginalFile.Player_PartnerCharacode_List[c] = cmnparamModFile.Player_PartnerCharacode_List[0];
                                            cmnparamOriginalFile.Player_PartnerAwaCharacode_List[c] = cmnparamModFile.Player_PartnerAwaCharacode_List[0];
                                            found = true;
                                            break;
                                        }

                                    }
                                    //If mod replaces character, but it wasn't able find entry, it will add new entry
                                    if (!found) {
                                        cmnparamOriginalFile.Player_Characode_List.Add(cmnparamModFile.Player_Characode_List[0]);
                                        cmnparamOriginalFile.Player_PlFileName_List.Add(cmnparamModFile.Player_PlFileName_List[0]);
                                        cmnparamOriginalFile.Player_PlAwaFileName_List.Add(cmnparamModFile.Player_PlAwaFileName_List[0]);
                                        cmnparamOriginalFile.Player_PlAwa2FileName_List.Add(cmnparamModFile.Player_PlAwa2FileName_List[0]);
                                        cmnparamOriginalFile.Player_EvName_List.Add(cmnparamModFile.Player_EvName_List[0]);
                                        cmnparamOriginalFile.Player_UJEvName_List.Add(cmnparamModFile.Player_UJEvName_List[0]);
                                        cmnparamOriginalFile.Player_UJ_1_CutInName_List.Add(cmnparamModFile.Player_UJ_1_CutInName_List[0]);
                                        cmnparamOriginalFile.Player_UJ_1_AtkName_List.Add(cmnparamModFile.Player_UJ_1_AtkName_List[0]);
                                        cmnparamOriginalFile.Player_UJ_2_CutInName_List.Add(cmnparamModFile.Player_UJ_2_CutInName_List[0]);
                                        cmnparamOriginalFile.Player_UJ_2_AtkName_List.Add(cmnparamModFile.Player_UJ_2_AtkName_List[0]);
                                        cmnparamOriginalFile.Player_UJ_3_CutInName_List.Add(cmnparamModFile.Player_UJ_3_CutInName_List[0]);
                                        cmnparamOriginalFile.Player_UJ_3_AtkName_List.Add(cmnparamModFile.Player_UJ_3_AtkName_List[0]);
                                        cmnparamOriginalFile.Player_UJ_Alt_CutInName_List.Add(cmnparamModFile.Player_UJ_Alt_CutInName_List[0]);
                                        cmnparamOriginalFile.Player_UJ_Alt_AtkName_List.Add(cmnparamModFile.Player_UJ_Alt_AtkName_List[0]);
                                        cmnparamOriginalFile.Player_PartnerCharacode_List.Add(cmnparamModFile.Player_PartnerCharacode_List[0]);
                                        cmnparamOriginalFile.Player_PartnerAwaCharacode_List.Add(cmnparamModFile.Player_PartnerAwaCharacode_List[0]);
                                        cmnparamOriginalFile.EntryCount_Player++;
                                    }

                                } else {
                                    //If mod doesnt replaces character, it will add new entry
                                    cmnparamOriginalFile.Player_Characode_List.Add(cmnparamModFile.Player_Characode_List[0]);
                                    cmnparamOriginalFile.Player_PlFileName_List.Add(cmnparamModFile.Player_PlFileName_List[0]);
                                    cmnparamOriginalFile.Player_PlAwaFileName_List.Add(cmnparamModFile.Player_PlAwaFileName_List[0]);
                                    cmnparamOriginalFile.Player_PlAwa2FileName_List.Add(cmnparamModFile.Player_PlAwa2FileName_List[0]);
                                    cmnparamOriginalFile.Player_EvName_List.Add(cmnparamModFile.Player_EvName_List[0]);
                                    cmnparamOriginalFile.Player_UJEvName_List.Add(cmnparamModFile.Player_UJEvName_List[0]);
                                    cmnparamOriginalFile.Player_UJ_1_CutInName_List.Add(cmnparamModFile.Player_UJ_1_CutInName_List[0]);
                                    cmnparamOriginalFile.Player_UJ_1_AtkName_List.Add(cmnparamModFile.Player_UJ_1_AtkName_List[0]);
                                    cmnparamOriginalFile.Player_UJ_2_CutInName_List.Add(cmnparamModFile.Player_UJ_2_CutInName_List[0]);
                                    cmnparamOriginalFile.Player_UJ_2_AtkName_List.Add(cmnparamModFile.Player_UJ_2_AtkName_List[0]);
                                    cmnparamOriginalFile.Player_UJ_3_CutInName_List.Add(cmnparamModFile.Player_UJ_3_CutInName_List[0]);
                                    cmnparamOriginalFile.Player_UJ_3_AtkName_List.Add(cmnparamModFile.Player_UJ_3_AtkName_List[0]);
                                    cmnparamOriginalFile.Player_UJ_Alt_CutInName_List.Add(cmnparamModFile.Player_UJ_Alt_CutInName_List[0]);
                                    cmnparamOriginalFile.Player_UJ_Alt_AtkName_List.Add(cmnparamModFile.Player_UJ_Alt_AtkName_List[0]);
                                    cmnparamOriginalFile.Player_PartnerCharacode_List.Add(cmnparamModFile.Player_PartnerCharacode_List[0]);
                                    cmnparamOriginalFile.Player_PartnerAwaCharacode_List.Add(cmnparamModFile.Player_PartnerAwaCharacode_List[0]);
                                    cmnparamOriginalFile.EntryCount_Player++;
                                }
                                //Creates directory
                                if (!Directory.Exists(datawin32Path + "\\sound")) {
                                    Directory.CreateDirectory(datawin32Path + "\\sound");
                                }
                                //Saves edited cmnparam file
                                cmnparamOriginalFile.SaveFileAs(datawin32Path + "\\sound\\cmnparam.xfbin");
                            }
                            if (prmExist && damageeffExist) {
                                //This function merges damageEff and effectprm files, and fixing prm files with new damageEff ids
                                Tool_damageeffEditor_code damageeffOriginalFile = new Tool_damageeffEditor_code();
                                Tool_damageeffEditor_code damageeffModFile = new Tool_damageeffEditor_code();
                                damageeffModFile.OpenFile(ModdamageeffPath);
                                if (damageeffModFile.EntryCount > 0) {
                                    //This code opening damageeff files
                                    if (File.Exists(damageeffPath))
                                        damageeffOriginalFile.OpenFile(damageeffPath);
                                    else {
                                        damageeffOriginalFile.OpenFile(originaldamageeffPath);
                                    }
                                    List<int> OldEffectIds = new List<int>();
                                    List<int> NewEffectIds = new List<int>();

                                    if (effectprmExist) {
                                        //This code opening effectprm file
                                        Tool_effectprmEditor_code effectprmOriginalFile = new Tool_effectprmEditor_code();
                                        Tool_effectprmEditor_code effectprmModFile = new Tool_effectprmEditor_code();
                                        effectprmModFile.OpenFile(ModeffectprmPath);
                                        if (File.Exists(effectprmPath))
                                            effectprmOriginalFile.OpenFile(effectprmPath);
                                        else {
                                            effectprmOriginalFile.OpenFile(originaleffectprmPath);
                                        }
                                        //This code adds effectprm entries to vanilla/edited files and saves new and olds effectprm ids
                                        for (int j = 0; j < effectprmModFile.EntryCount; j++) {
                                            OldEffectIds.Add(effectprmModFile.EffectPrmID_List[j]);
                                            NewEffectIds.Add(effectprmOriginalFile.EffectPrmID_List.Max() + 1);
                                            effectprmModFile.EffectPrmID_List[j] = effectprmOriginalFile.EffectPrmID_List.Max() + 1;
                                            effectprmOriginalFile.EffectPrmID_List.Add(effectprmModFile.EffectPrmID_List[j]);
                                            effectprmOriginalFile.EffectPrmPath_List.Add(effectprmModFile.EffectPrmPath_List[j]);
                                            effectprmOriginalFile.EffectPrmAnm_List.Add(effectprmModFile.EffectPrmAnm_List[j]);
                                            effectprmOriginalFile.EffectPrmType_List.Add(effectprmModFile.EffectPrmType_List[j]);
                                            effectprmOriginalFile.EntryCount++;
                                        }
                                        //Creates directory
                                        if (!Directory.Exists(datawin32Path + "\\spc")) {
                                            Directory.CreateDirectory(datawin32Path + "\\spc");
                                        }
                                        //Saves edited effectprm file
                                        effectprmOriginalFile.SaveFileAs(datawin32Path + "\\spc\\effectprm.bin.xfbin");
                                    }

                                    List<int> OldHitIds = new List<int>();
                                    List<int> NewHitIds = new List<int>();
                                    //This code changes all effectprm ids in modded damageEff file
                                    for (int c = 0; c < damageeffModFile.EntryCount; c++) {
                                        for (int f = 0; f < OldEffectIds.Count; f++) {
                                            if (damageeffModFile.EffectPrmId_List[c] == OldEffectIds[f])
                                                damageeffModFile.EffectPrmId_List[c] = NewEffectIds[f];
                                        }


                                    }
                                    //This code adding new entries to vanilla/edited damageEff file and changes damageEff ids
                                    for (int c = 0; c < damageeffModFile.EntryCount; c++) {
                                        int maxValue = damageeffOriginalFile.HitId_List.Max();
                                        OldHitIds.Add(damageeffModFile.HitId_List[c]);
                                        NewHitIds.Add(maxValue + 1);
                                        damageeffOriginalFile.HitId_List.Add(maxValue + 1);
                                        if (OldHitIds.Contains(damageeffModFile.ExtraHitId_List[c])) {
                                            for (int s = 0; s < OldHitIds.Count; s++) {
                                                damageeffModFile.ExtraHitId_List[c] = NewHitIds[s];
                                            }
                                        } else {
                                            damageeffOriginalFile.ExtraHitId_List.Add(damageeffModFile.ExtraHitId_List[c]);
                                        }
                                        damageeffOriginalFile.ExtraSoundId_List.Add(damageeffModFile.ExtraSoundId_List[c]);
                                        damageeffOriginalFile.EffectPrmId_List.Add(damageeffModFile.EffectPrmId_List[c]);
                                        damageeffOriginalFile.SoundId_List.Add(damageeffModFile.SoundId_List[c]);
                                        damageeffOriginalFile.Unknown1_List.Add(damageeffModFile.Unknown1_List[c]);
                                        damageeffOriginalFile.Unknown2_List.Add(damageeffModFile.Unknown2_List[c]);
                                        damageeffOriginalFile.ExtraEffectPrmId_List.Add(damageeffModFile.ExtraEffectPrmId_List[c]);
                                        damageeffOriginalFile.EntryCount++;
                                    }
                                    //This code opening prm file of character mod
                                    Tool_MovesetCoder_code PrmFile = new Tool_MovesetCoder_code();
                                    PrmFile.OpenFile(ModprmPath);
                                    //This function checking each movement section 
                                    for (int k1 = 0; k1 < PrmFile.movementList.Count; k1++) {
                                        for (int k2 = 0; k2 < PrmFile.movementList[k1].Count; k2++) {
                                            for (int k3 = 0; k3 < PrmFile.movementList[k1][k2].Count; k3++) {
                                                if (PrmFile.movementList[k1][k2][k3].Length > 0x40) {
                                                    int selectedhit = MainFunctions.b_ReadIntFromTwoBytes(PrmFile.movementList[k1][k2][k3], 0x82);
                                                    for (int g = 0; g < OldHitIds.Count; g++) {
                                                        //This code checking for old damageEff Ids and changing them on new ids
                                                        if (OldHitIds[g] == selectedhit) {
                                                            PrmFile.movementList[k1][k2][k3][0x82] = BitConverter.GetBytes(NewHitIds[g])[0];
                                                            PrmFile.movementList[k1][k2][k3][0x83] = BitConverter.GetBytes(NewHitIds[g])[1];
                                                            
                                                        }

                                                    }

                                                }
                                            }
                                        }
                                    }
                                    //Creates directory
                                    if (!Directory.Exists(datawin32Path + "\\spc")) {
                                        Directory.CreateDirectory(datawin32Path + "\\spc");
                                    }
                                    //Saves edited damageEff file
                                    damageeffOriginalFile.SaveFileAs(datawin32Path + "\\spc\\damageeff.bin.xfbin");
                                    if (!Directory.Exists(System.IO.Path.GetDirectoryName(datawin32Path + "\\" + ModprmPath.Substring(ModprmPath.IndexOf(dataWinFolder) + dataWinFolderLength)))) {
                                        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(datawin32Path + "\\" + ModprmPath.Substring(ModprmPath.IndexOf(dataWinFolder) + dataWinFolderLength)));
                                    }
                                    //Saves edited prm file
                                    PrmFile.SaveFileAs(datawin32Path + "\\" + ModprmPath.Substring(ModprmPath.IndexOf(dataWinFolder) + dataWinFolderLength));
                                }
                                
                            }
                            if (damageprmExist) {
                                //This function merges damageprm files
                                Tool_damageprmEditor_code damageprmModFile = new Tool_damageprmEditor_code();
                                Tool_damageprmEditor_code damageprmOriginalFile = new Tool_damageprmEditor_code();
                                damageprmModFile.OpenFile(ModdamageprmPath);
                                if (File.Exists(damageprmPath))
                                    damageprmOriginalFile.OpenFile(damageprmPath);
                                else {
                                    damageprmOriginalFile.OpenFile(originaldamageprmPath);
                                }
                                for (int c = 0; c < damageprmModFile.EntryCount; c++) {
                                    //This code adding new entries to vanilla/edited file if it doesn't contain section from mod file
                                    if (!damageprmOriginalFile.DamagePrm_NameID_List.Contains(damageprmModFile.DamagePrm_NameID_List[c])) {
                                        damageprmOriginalFile.DamagePrm_NameID_List.Add(damageprmModFile.DamagePrm_NameID_List[c]);
                                        damageprmOriginalFile.DamagePrm_Values_List.Add(damageprmModFile.DamagePrm_Values_List[c]);
                                        damageprmOriginalFile.EntryCount++;
                                    } else {//This code changes settings of exist entries
                                        if (BitConverter.ToString(damageprmOriginalFile.DamagePrm_Values_List[damageprmOriginalFile.DamagePrm_NameID_List.IndexOf(damageprmModFile.DamagePrm_NameID_List[c])]) != BitConverter.ToString(damageprmModFile.DamagePrm_Values_List[c])) {
                                            damageprmOriginalFile.DamagePrm_Values_List[damageprmOriginalFile.DamagePrm_NameID_List.IndexOf(damageprmModFile.DamagePrm_NameID_List[c])] = damageprmModFile.DamagePrm_Values_List[c];
                                        }
                                    }
                                }
                                //Creates directory
                                if (!Directory.Exists(datawin32Path + "\\spc")) {
                                    Directory.CreateDirectory(datawin32Path + "\\spc");
                                }
                                //Saves edited damagePrm file
                                damageprmOriginalFile.SaveFileAs(datawin32Path + "\\spc\\damageprm.bin.xfbin");
                            }
                            if (messageExistList.Contains(true)) {
                                //This function merges all messageInfo files on all languages
                                Tool_MessageInfoEditor_code MessageModFile = new Tool_MessageInfoEditor_code();
                                Tool_MessageInfoEditor_code MessageOriginalFile = new Tool_MessageInfoEditor_code();
                                MessageModFile.OpenFilesStart(ModmessagePathList.Remove(ModmessagePathList.IndexOf("\\WIN64"), ModmessagePathList.Length - ModmessagePathList.IndexOf("\\WIN64")));
                                if (Directory.Exists(messageInfoPath))
                                    MessageOriginalFile.OpenFilesStart(messageInfoPath);
                                else {
                                    MessageOriginalFile.OpenFilesStart(originalMessagePath);
                                }
                                //This cycle checking each language
                                for (int l = 0; l < Program.LANG.Length; l++) {
                                    //This function adding all modded entries to all messageInfo files in all languages
                                    for (int c = 0; c < MessageModFile.CRC32CodesList[l].Count; c++) {
                                        MessageOriginalFile.CRC32CodesList[l].Add(MessageModFile.CRC32CodesList[l][c]);
                                        MessageOriginalFile.MainTextsList[l].Add(MessageModFile.MainTextsList[l][c]);
                                        MessageOriginalFile.ExtraTextsList[l].Add(MessageModFile.ExtraTextsList[l][c]);
                                        MessageOriginalFile.ACBFilesList[l].Add(MessageModFile.ACBFilesList[l][c]);
                                        MessageOriginalFile.CueIDsList[l].Add(MessageModFile.CueIDsList[l][c]);
                                        MessageOriginalFile.VoiceOnlysList[l].Add(MessageModFile.VoiceOnlysList[l][c]);
                                        MessageOriginalFile.EntryCounts[l]++;
                                    }
                                    //Creates directory for each language
                                    if (!Directory.Exists(datawin32Path + "\\message\\WIN64\\" + Program.LANG[l])) {
                                        Directory.CreateDirectory(datawin32Path + "\\message\\WIN64\\" + Program.LANG[l]);
                                    }
                                }
                                //Saves all edited messageInfo files
                                MessageOriginalFile.SaveFilesAs(datawin32Path + "\\message");
                            }
                            if (btlcmnExist) {
                                //This function merges btlcmn files
                                Tool_nus3bankEditor_code BtlcmnModFile = new Tool_nus3bankEditor_code();
                                Tool_nus3bankEditor_code BtlcmnOriginalFile = new Tool_nus3bankEditor_code();
                                BtlcmnModFile.OpenFile(ModbtlcmnPath);
                                if (File.Exists(datawin32Path + "\\sound\\PC\\btlcmn.xfbin"))
                                    BtlcmnOriginalFile.OpenFile(datawin32Path + "\\sound\\PC\\btlcmn.xfbin");
                                else {
                                    BtlcmnOriginalFile.OpenFile(originalBtlcmnPath);
                                }
                                for (int z = 0; z < BtlcmnModFile.TONE_SoundName_List.Count; z++) {
                                    //If sound doesn't exist in vanilla/edited btlcmn file, it will add entries at the end of file
                                    if (!BtlcmnOriginalFile.TONE_SoundName_List.Contains(BtlcmnModFile.TONE_SoundName_List[z])) {
                                        BtlcmnOriginalFile.TONE_SectionType_List.Add(BtlcmnModFile.TONE_SectionType_List[z]);
                                        BtlcmnOriginalFile.TONE_SectionTypeValues_List.Add(BtlcmnModFile.TONE_SectionTypeValues_List[z]);
                                        BtlcmnOriginalFile.TONE_SoundName_List.Add(BtlcmnModFile.TONE_SoundName_List[z]);
                                        BtlcmnOriginalFile.TONE_SoundPos_List.Add(BtlcmnModFile.TONE_SoundPos_List[z]);
                                        BtlcmnOriginalFile.TONE_SoundSize_List.Add(BtlcmnModFile.TONE_SoundSize_List[z]);
                                        BtlcmnOriginalFile.TONE_MainVolume_List.Add(BtlcmnModFile.TONE_MainVolume_List[z]);
                                        BtlcmnOriginalFile.TONE_SoundSettings_List.Add(BtlcmnModFile.TONE_SoundSettings_List[z]);
                                        BtlcmnOriginalFile.TONE_SoundData_List.Add(BtlcmnModFile.TONE_SoundData_List[z]);
                                        BtlcmnOriginalFile.TONE_RandomizerType_List.Add(BtlcmnModFile.TONE_RandomizerType_List[z]);
                                        BtlcmnOriginalFile.TONE_RandomizerLength_List.Add(BtlcmnModFile.TONE_RandomizerLength_List[z]);
                                        BtlcmnOriginalFile.TONE_RandomizerUnk1_List.Add(BtlcmnModFile.TONE_RandomizerUnk1_List[z]);
                                        BtlcmnOriginalFile.TONE_RandomizerSectionCount_List.Add(BtlcmnModFile.TONE_RandomizerSectionCount_List[z]);
                                        BtlcmnOriginalFile.TONE_RandomizerOneSection_ID_List.Add(BtlcmnModFile.TONE_RandomizerOneSection_ID_List[z]);
                                        BtlcmnOriginalFile.TONE_RandomizerOneSection_unk_List.Add(BtlcmnModFile.TONE_RandomizerOneSection_unk_List[z]);
                                        BtlcmnOriginalFile.TONE_RandomizerOneSection_PlayChance_List.Add(BtlcmnModFile.TONE_RandomizerOneSection_PlayChance_List[z]);
                                        BtlcmnOriginalFile.TONE_RandomizerOneSection_SoundID_List.Add(BtlcmnModFile.TONE_RandomizerOneSection_SoundID_List[z]);
                                        BtlcmnOriginalFile.TONE_RandomizerUnk2_List.Add(BtlcmnModFile.TONE_RandomizerUnk2_List[z]);
                                        BtlcmnOriginalFile.TONE_RandomizerUnk3_List.Add(BtlcmnModFile.TONE_RandomizerUnk3_List[z]);
                                        BtlcmnOriginalFile.TONE_RandomizerUnk4_List.Add(BtlcmnModFile.TONE_RandomizerUnk4_List[z]);
                                        BtlcmnOriginalFile.TONE_RandomizerUnk5_List.Add(BtlcmnModFile.TONE_RandomizerUnk5_List[z]);
                                        BtlcmnOriginalFile.TONE_RandomizerUnk6_List.Add(BtlcmnModFile.TONE_RandomizerUnk6_List[z]);
                                        BtlcmnOriginalFile.TONE_OverlaySound_List.Add(BtlcmnModFile.TONE_OverlaySound_List[z]);
                                    }
                                    
                                }
                                //Creates directory
                                if (!Directory.Exists(datawin32Path + "\\sound\\PC\\")) {
                                    Directory.CreateDirectory(datawin32Path + "\\sound\\PC\\");
                                }
                                //Saves edited btlcmn file
                                BtlcmnOriginalFile.SaveFileAs(datawin32Path + "\\sound\\PC\\btlcmn.xfbin");

                                //This function was used for creating new separam file
                                byte[] separamBytes = File.ReadAllBytes(originalseparamPath); //opens vanilla separam file
                                byte[] fileStart = new byte[0];
                                fileStart = MainFunctions.b_AddBytes(fileStart, separamBytes, 0, 0, 0xE2A); //Saves header of vanilla separam file
                                fileStart = MainFunctions.b_AddBytes(fileStart, BitConverter.GetBytes((BtlcmnOriginalFile.TONE_SoundName_List.Count * 0x20 + 6)), 1); //Saves new size for separam
                                fileStart = MainFunctions.b_AddBytes(fileStart, new byte[8] { 0x00, 0x00, 0x00, 0x01, 0x00, 0x79, 0x00, 0x00 });
                                fileStart = MainFunctions.b_AddBytes(fileStart, BitConverter.GetBytes((BtlcmnOriginalFile.TONE_SoundName_List.Count * 0x20 + 2)), 1); //Saves new size for separam
                                fileStart = MainFunctions.b_AddBytes(fileStart, BitConverter.GetBytes(BtlcmnOriginalFile.TONE_SoundName_List.Count), 0, 0, 2); //Saves new count of entries for separam
                                for (int z = 0; z < BtlcmnOriginalFile.TONE_SoundName_List.Count; z++) { //This function adding entries to separam file
                                    byte[] section = new byte[0x20];
                                    string name = BtlcmnOriginalFile.TONE_SoundName_List[z];
                                    if (name.Length > 31)
                                        name = name.Substring(0, 31);
                                    section = MainFunctions.b_ReplaceBytes(section, Encoding.ASCII.GetBytes(name), 0);
                                    fileStart = MainFunctions.b_AddBytes(fileStart, section);
                                }
                                fileStart = MainFunctions.b_AddBytes(fileStart, separamBytes, 0, 0x815C, 0x815C + 0x5DB2); //Saves footer of vanilla separam file
                                File.WriteAllBytes(datawin32Path + "\\sound\\separam.xfbin", fileStart); //Saves edited separam file
                            }
                            //This function removes all .backup files from data_win32
                            DirectoryInfo backup_d = new DirectoryInfo(datawin32Path);
                            FileInfo[] backup_Files = backup_d.GetFiles("*.backup", SearchOption.AllDirectories);
                            foreach (FileInfo file in backup_Files) {
                                file.Delete();

                            }

                        }

                    }
                }
                MessageBox.Show("Finished compiling.");
            }
            
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            if (Directory.Exists(GameRootPath)) {
                //This function was used for cleaning data_win32 and moddingapi folders
                WinForms.DialogResult msg = (WinForms.DialogResult)MessageBox.Show("Are you sure you want to clean your game from mods?", "", MessageBoxButton.OKCancel);
                if (msg == WinForms.DialogResult.OK) {
                    //Removes data_win32 folder
                    if (Directory.Exists(GameRootPath + "\\data_win32")) {
                        Directory.Delete(GameRootPath + "\\data_win32", true);
                    }
                    for (int c = 0; c < ImportedCharacodesList.Count; c++) {
                        //Removes characode folders from moddingapi\mods folder
                        if (Directory.Exists(GameRootPath + "\\moddingapi\\mods\\" + ImportedCharacodesList[c])) {
                            Directory.Delete(GameRootPath + "\\moddingapi\\mods\\" + ImportedCharacodesList[c], true);
                        }
                    }
                    
                    string gfx_path = GameRootPath + "\\data\\ui\\flash\\OTHER";
                    DirectoryInfo d_gfx = new DirectoryInfo(gfx_path);
                    string Modgfx_charselPath = "";
                    FileInfo[] gfx_Files = d_gfx.GetFiles("*.gfx", SearchOption.AllDirectories);

                    foreach (FileInfo file in gfx_Files) {
                        if (file.FullName.Contains("charsel\\charsel.gfx")) {
                            Modgfx_charselPath = file.FullName;
                            break;
                        } else {
                            Modgfx_charselPath = "";
                        }
                    }
                    //Replaces edited charsel.gfx file with vanilla file
                    if (File.Exists(Modgfx_charselPath)) {
                        byte[] charsel = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\systemFiles\\charsel.gfx");
                        File.WriteAllBytes(Modgfx_charselPath, charsel);
                    }
                    //Replaces edited nuccMaterial_dx11.nsh file with vanilla file
                    if (File.Exists(GameRootPath + "\\data\\system\\nuccMaterial_dx11.nsh")) {
                        byte[] nuccMat = File.ReadAllBytes(originalnuccMaterialDx11Path);
                        File.WriteAllBytes(GameRootPath + "\\data\\system\\nuccMaterial_dx11.nsh", nuccMat);
                    }

                }
                
            } else {
                MessageBox.Show("Select root folder of game");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) {
            //string file_exe = GameRootPath + "\\NSUNS4 — копия.exe";
            //if (System.IO.File.Exists(file_exe)) {
            //    object shDesktop = (object)"Desktop";
            //    WshShell shell = new WshShell();
            //    string shortcutAddress = Directory.GetCurrentDirectory() + @"\NSUNS4.lnk";
            //    IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            //    shortcut.Description = "New shortcut for a Notepad";
            //    shortcut.Hotkey = "Ctrl+Shift+N";
            //    shortcut.TargetPath = file_exe;
            //    shortcut.Save();

            //    Process.Start("Steam", "-applaunch 349040 -StraightIntoFreemode");

            //}
            //else
            //    MessageBox.Show("NSUNS4.exe wasn't found");
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) {
            //This function was used for installing moddingAPI
            if (Directory.Exists(GameRootPath)) {
                InstallModdingAPI();
                MessageBox.Show("ModdingAPI Installed");
            } else
                MessageBox.Show("Select root folder of game");
        }
        private static void CopyFilesRecursively(string sourcePath, string targetPath) {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories)) {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories)) {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e) {
            //This function deleting moddingAPI from root folder
            if (Directory.Exists(GameRootPath)) {
                if (File.Exists(GameRootPath+ "\\D3DCompiler_47.dll") && File.Exists(GameRootPath + "\\d3dcompiler_47_o.dll")) {
                    File.Delete(GameRootPath + "\\D3DCompiler_47.dll");
                    if (File.Exists(GameRootPath + "\\d3dcompiler_47_o.dll")) {
                        File.WriteAllBytes(GameRootPath + "\\D3DCompiler_47.dll", File.ReadAllBytes(GameRootPath + "\\d3dcompiler_47_o.dll"));
                        File.Delete(GameRootPath + "\\d3dcompiler_47_o.dll");
                    }
                }
                if (File.Exists(GameRootPath + "\\xinput9_1_0.dll") && File.Exists(GameRootPath + "\\xinput9_1_0_o.dll")) {
                    File.Delete(GameRootPath + "\\xinput9_1_0.dll");
                    File.Delete(GameRootPath + "\\xinput9_1_0_o.dll");
                }
                if (Directory.Exists(GameRootPath + "\\moddingapi")) {
                    Directory.Delete(GameRootPath + "\\moddingapi",true);
                }
                MessageBox.Show("ModdingAPI Deleted");
            } else
                MessageBox.Show("Select root folder of game");
        }

        private void Window_Closed(object sender, EventArgs e) {
            //This function deletes temp files
            if (Directory.Exists(@Directory.GetCurrentDirectory() + "\\temp")) {
                Directory.Delete(@Directory.GetCurrentDirectory() + "\\temp", true);
            }
        }
        public void InstallModdingAPI() {
            //Function for installing ModdingAPI
            if (!Directory.Exists(GameRootPath + "\\moddingapi")) {
                //Downloads moddingAPI from gitHub link which is stored in moddingAPI_link.txt file
                using (WebClient wc = new WebClient()) {
                    wc.Headers.Add("a", "a");
                    if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\temp"))
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\temp");
                    wc.DownloadFile(File.ReadAllText(@Directory.GetCurrentDirectory() + "\\ModdingAPI_link.txt"), @Directory.GetCurrentDirectory() + "\\temp\\ModdingAPI.rar"); ;
                }
                if (!Directory.Exists(@Directory.GetCurrentDirectory() + "\\temp\\ModdingAPI_files"))
                    Directory.CreateDirectory(@Directory.GetCurrentDirectory() + "\\temp\\ModdingAPI_files");
                //Opening RAR file from temp folder and extract all files
                using (var archive = RarArchive.Open(@Directory.GetCurrentDirectory() + "\\temp\\ModdingAPI.rar")) {
                    foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory)) {
                        entry.WriteToDirectory(@Directory.GetCurrentDirectory() + "\\temp\\ModdingAPI_files", new ExtractionOptions() {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
                //Copy pasting all files from temp folder to game root folder
                CopyFilesRecursively(@Directory.GetCurrentDirectory() + "\\temp\\ModdingAPI_files", GameRootPath);
                //Changes config of moddingAPI
                string[] cfg = File.ReadAllLines(GameRootPath + "\\moddingapi\\config.ini");
                string moddingAPI_name = "";
                string EnableConsole = "";
                string EnableModList = "";
                string LangEN = "";
                if (cfg.Length > 0) moddingAPI_name = cfg[0];
                if (cfg.Length > 1) EnableConsole = cfg[1];
                if (cfg.Length > 2) EnableModList = cfg[2];
                if (cfg.Length > 3) LangEN = cfg[3];
                //Disables modList and console
                EnableConsole = "EnableConsole=0";
                EnableModList = "EnableModList=0";
                List<string> cfg_new = new List<string>();
                cfg_new.Add(moddingAPI_name);
                cfg_new.Add(EnableConsole);
                cfg_new.Add(EnableModList);
                cfg_new.Add(LangEN);
                //Saves new config for moddingAPI
                File.WriteAllLines(GameRootPath + "\\moddingapi\\config.ini", cfg_new.ToArray());
            }
            
        }
    }
}
