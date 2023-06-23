using IWshRuntimeLibrary;
using NSUNS4_ModManager.ToolBoxCode;
using NSUNS4_ModManager.YaCpkTool;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.UI.Xaml.Shapes;
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
            if (Directory.Exists(@Directory.GetCurrentDirectory() + "\\temp")) 
                Directory.Delete(@Directory.GetCurrentDirectory() + "\\temp", true);
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
        string originalspTypeSupportParamPath = Directory.GetCurrentDirectory() + "\\systemFiles\\spTypeSupportParam.xfbin";
        string originalnuccMaterialDx11Path = Directory.GetCurrentDirectory() + "\\systemFiles\\nuccMaterial_dx11.nsh";
        string originalstageInfoPath = Directory.GetCurrentDirectory() + "\\systemFiles\\StageInfo.bin.xfbin";

        //This is paths for root folder where will be saved edited files
        public static string datawin32Path_or = "[null]";
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
        public static bool CleanGame = true;
        public List<string> ModName_List = new List<string>();
        public List<bool> EnableMod_List = new List<bool>();
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
                GameModsPath = GameRootPath + "\\modmanager";
                Directory.CreateDirectory(GameModsPath);
                SaveConfig();
                RefreshModList();
                System.Windows.MessageBox.Show("Config file saved.");
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
            List<string> cfg = new List<string> {
                "[null]",
                "[null]",
                "true"
            };
            File.WriteAllLines(ConfigPath, cfg.ToArray());
            System.Windows.MessageBox.Show("Config file created.");
        }
        void SaveConfig() {
            //This function saves config file
            List<string> cfg = new List<string> {
                GameRootPath,
                GameModsPath,
                CleanGame.ToString()
            };
            File.WriteAllLines(ConfigPath, cfg.ToArray());
        }
        public void LoadConfig() {
            //This function loads all paths from config file
            string[] cfg = File.ReadAllLines(ConfigPath);
            if (cfg.Length > 0) GameRootPath = cfg[0];
            if (cfg.Length > 1) GameModsPath = cfg[1];
            if (cfg.Length > 2) CleanGame = Convert.ToBoolean(cfg[2]);

            GameCleanItem.IsChecked = CleanGame;
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
                        EnableMod_List.Add(true);
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
                if (Directory.Exists(GameModsPath)) {
                    FileInfo[] characode_Files = d.GetFiles("BGM_ID.txt", SearchOption.AllDirectories);
                    foreach (FileInfo file in characode_Files) {
                        if (file.FullName.Contains("BGM_ID.txt")) {
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
                if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\temp\\" + System.IO.Path.GetFileName(ModName_List[x]))) {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\temp\\" + System.IO.Path.GetFileName(ModName_List[x]));
                    if (!File.Exists(Directory.GetCurrentDirectory() + "\\temp\\" + System.IO.Path.GetFileName(ModName_List[x]) + "\\Icon.png"))
                        File.Copy(IconPaths[x], Directory.GetCurrentDirectory() + "\\temp\\" + System.IO.Path.GetFileName(ModName_List[x]) + "\\Icon.png", true);
                }
                var uri = new Uri(Directory.GetCurrentDirectory() + "\\temp\\" + System.IO.Path.GetFileName(ModName_List[x]) + "\\Icon.png");
                var Icon = BitmapFromUri(uri);
                ModIcon.Source = Icon;
                ModAuthor.Content = "Author: " + File.ReadAllText(AuthorPaths[x]);
                ModEnabler.IsChecked = EnableMod_List[x];
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
            if (!Directory.Exists(GameRootPath + "\\moddingapi\\modmanager_assets"))
                Directory.CreateDirectory(GameRootPath + "\\moddingapi\\modmanager_assets");
            else {
                Directory.Delete(GameRootPath + "\\moddingapi\\modmanager_assets", true);
                Directory.CreateDirectory(GameRootPath + "\\moddingapi\\modmanager_assets");

            }


            List<int> SkipCharacode = new List<int>();
            List<int> SkipPSP_PresetID = new List<int> {
                0x14
            };
            List<string> player_icon_entries_list = new List<string>();
            CharacterPathList.Clear();
            if (CleanGame)
                CleanGameAssets(false);
            Tool_MessageInfoEditor_code MessageOriginalFile = new Tool_MessageInfoEditor_code();
            if (Directory.Exists(messageInfoPath))
                MessageOriginalFile.OpenFilesStart(messageInfoPath);
            else {
                MessageOriginalFile.OpenFilesStart(originalMessagePath);
            }
            //This is compile mod function
            if (Directory.Exists(GameRootPath) && Directory.Exists(GameModsPath)) {

                List<string> characterDescription = new List<string>();
                List<string> characterAuthor = new List<string>();
                List<string> characterName = new List<string>();
                List<string> stageDescription = new List<string>();
                List<string> stageAuthor = new List<string>();
                List<string> stageName = new List<string>();
                List<string> UsedShaders = new List<string>(); //List for used shaders to prevent loading same shader twice
                List<bool> ModdingAPIRequirement_list = new List<bool>(); //List for installing moddingapi in case, if mod requires it
                for (int c = 0; c < ModdingAPI_requirement_Paths.Count; c++) {
                    //This function reading moddingAPI.txt to get boolean value for requirement of moddingAPI
                    ModdingAPIRequirement_list.Add(Convert.ToBoolean(File.ReadAllText(ModdingAPI_requirement_Paths[c])));
                }
                if (ModdingAPIRequirement_list.Contains(true))
                    ExtractModdingAPI();
                //InstallModdingAPI();
                List<string> CharacodePaths = new List<string>(); //This list was used for saving characode.txt path
                List<string> CharacodesList = new List<string>(); //This list was used for saving characode
                if (Directory.Exists(GameModsPath)) {
                    DirectoryInfo d = new DirectoryInfo(@GameModsPath);
                    FileInfo[] characode_Files = d.GetFiles("characode.txt", SearchOption.AllDirectories);
                    foreach (FileInfo file in characode_Files) {
                        string ModPath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(file.FullName));
                        int ModIndex = ModName_List.IndexOf(ModPath);
                        bool ModIsEnabled = EnableMod_List[ModIndex];
                        if (file.FullName.Contains("characode.txt") && ModIsEnabled) {
                            CharacodePaths.Add(file.FullName);
                            CharacodesList.Add(System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(file.FullName)));
                            characterDescription.Add(File.ReadAllText(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(file.FullName)) +"\\Description.txt"));
                            characterAuthor.Add(File.ReadAllText(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(file.FullName)) + "\\Author.txt"));
                            characterName.Add(System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(file.FullName))));
                            //This function checking for all characodes in mod folder
                        }
                    }

                }

                List<string> stagePaths = new List<string>(); //This list was used for saving BGM_ID.txt path
                List<string> stageList = new List<string>(); //This list was used for saving stage name
                if (Directory.Exists(GameModsPath)) {
                    DirectoryInfo d = new DirectoryInfo(@GameModsPath);
                    FileInfo[] BGM_Files = d.GetFiles("BGM_ID.txt", SearchOption.AllDirectories);
                    foreach (FileInfo file in BGM_Files) {
                        string ModPath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(file.FullName));
                        int ModIndex = ModName_List.IndexOf(ModPath);
                        bool ModIsEnabled = EnableMod_List[ModIndex];
                        if (file.FullName.Contains("BGM_ID.txt") && ModIsEnabled) {
                            stagePaths.Add(file.FullName);
                            stageList.Add(System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(file.FullName)));
                            stageDescription.Add(File.ReadAllText(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(file.FullName)) + "\\Description.txt"));
                            stageAuthor.Add(File.ReadAllText(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(file.FullName)) + "\\Author.txt"));
                            stageName.Add(System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(file.FullName))));
                            //This function checking for all stages in mod folder
                        }
                    }

                }

                //This paths used for saving edited files
                datawin32Path_or = GameRootPath + "\\data_win32";
                datawin32Path = GameRootPath + "\\data_win32_modmanager\\data";
                chaPath = GameRootPath + "\\data_win32_modmanager\\data\\spc\\characode.bin.xfbin";
                dppPath = GameRootPath + "\\data_win32_modmanager\\data\\spc\\duelPlayerParam.xfbin";
                pspPath = GameRootPath + "\\data_win32_modmanager\\data\\spc\\WIN64\\playerSettingParam.bin.xfbin";
                unlPath = GameRootPath + "\\data_win32_modmanager\\data\\duel\\WIN64\\unlockCharaTotal.bin.xfbin";
                cspPath = GameRootPath + "\\data_win32_modmanager\\data\\ui\\max\\select\\WIN64\\characterSelectParam.xfbin";
                iconPath = GameRootPath + "\\data_win32_modmanager\\data\\spc\\WIN64\\player_icon.xfbin";
                awakeAuraPath = GameRootPath + "\\data_win32_modmanager\\data\\spc\\WIN64\\awakeAura.xfbin";
                ougiFinishPath = GameRootPath + "\\data_win32_modmanager\\data\\rpg\\param\\WIN64\\OugiFinishParam.bin.xfbin";
                skillCustomizePath = GameRootPath + "\\data_win32_modmanager\\data\\spc\\WIN64\\skillCustomizeParam.xfbin";
                spSkillCustomizePath = GameRootPath + "\\data_win32_modmanager\\data\\spc\\WIN64\\spSkillCustomizeParam.xfbin";
                afterAttachObjectPath = GameRootPath + "\\data_win32_modmanager\\data\\spc\\WIN64\\afterAttachObject.xfbin";
                appearanceAnmPath = GameRootPath + "\\data_win32_modmanager\\data\\spc\\WIN64\\appearanceAnm.xfbin";
                stageInfoPath = GameRootPath + "\\data_win32_modmanager\\data\\stage\\WIN64\\StageInfo.bin.xfbin";
                battleParamPath = GameRootPath + "\\data_win32_modmanager\\data\\rpg\\WIN64\\battleParam.xfbin";
                episodeParamPath = GameRootPath + "\\data_win32_modmanager\\data\\rpg\\param\\WIN64\\episodeParam.bin.xfbin";
                episodeMovieParamPath = GameRootPath + "\\data_win32_modmanager\\data\\rpg\\param\\WIN64\\episodeMovieParam.bin.xfbin";
                messageInfoPath = GameRootPath + "\\data_win32_modmanager\\data\\message";
                cmnparamPath = GameRootPath + "\\data_win32_modmanager\\data\\sound\\cmnparam.xfbin";
                effectprmPath = GameRootPath + "\\data_win32_modmanager\\data\\spc\\effectprm.bin.xfbin";
                damageeffPath = GameRootPath + "\\data_win32_modmanager\\data\\spc\\damageeff.bin.xfbin";
                conditionprmPath = GameRootPath + "\\data_win32_modmanager\\data\\spc\\conditionprm.bin.xfbin";
                damageprmPath = GameRootPath + "\\data_win32_modmanager\\data\\spc\\damageprm.bin.xfbin";
                spTypeSupportParamPath = GameRootPath + "\\data_win32_modmanager\\data\\spc\\WIN64\\spTypeSupportParam.xfbin";
                nuccMaterialDx11Path = GameRootPath + "\\data\\system\\nuccMaterial_dx11.nsh";
                for (int c = 0; c < CharacodePaths.Count; c++) {
                    //This function adding characode 
                    CharacodeID = Convert.ToInt32(File.ReadAllText(CharacodePaths[c])); // This function getting old Characode ID which was in mod folder
                    CharacterPathList.Add(System.IO.Path.GetDirectoryName(CharacodePaths[c]));

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
                List<string> StageOriginalList = new List<string> {
                    "STAGE_SD62B",
                    "STAGE_SD62A",
                    "STAGE_SD01B",
                    "STAGE_SD03B",
                    "STAGE_SD03E",
                    "STAGE_SD12A",
                    "STAGE_SI00A",
                    "STAGE_SD01D",
                    "STAGE_SI06A",
                    "STAGE_SD14A",
                    "STAGE_SI01A",
                    "STAGE_SI01A",
                    "STAGE_SD06A",
                    "STAGE_SD07A",
                    "STAGE_SD07B",
                    "STAGE_SD10A",
                    "STAGE_SI09A_NR",
                    "STAGE_SI08A",
                    "STAGE_SD13A",
                    "STAGE_SD15A_NOSNOW",
                    "STAGE_SD17A",
                    "STAGE_SD16A",
                    "STAGE_SD22A",
                    "STAGE_SD25A",
                    "STAGE_SD23A",
                    "STAGE_SD21A",
                    "STAGE_SD19A",
                    "STAGE_SD33A",
                    "STAGE_SD05D",
                    "STAGE_SD04B",
                    "STAGE_SI43A",
                    "STAGE_SI35A",
                    "STAGE_SI33A",
                    "STAGE_SI42B",
                    "STAGE_SI42A",
                    "STAGE_SI44A",
                    "STAGE_SI45A",
                    "STAGE_SI50E",
                    "STAGE_SD60A",
                    "STAGE_SD05B",
                    "STAGE_SI51C",
                    "STAGE_SD70B",
                    "STAGE_SI70A",
                    "STAGE_SI71A"
                };
                List<string> stagesToAddList = new List<string>();
                List<string> stagesImagesToAddList = new List<string>();
                List<string> StageMessageId = new List<string>();
                List<List<string>> StageText = new List<List<string>>();
                List<int> FreeBGM_slot = new List<int> {
                    0x1527840,
                    0x1527880,
                    0x1527940,
                    0x1527A20,
                    0x1527B60,
                    0x1527BE0,
                    0x1527C20,
                    0x1527C40,
                    0x1527CA0,
                    0x1527CE0,
                    0x1527D00,
                    0x1527D20,
                    0x1527DA0,
                    0x1527DC0,
                    0x1527DE0,
                    0x1527E00,
                    0x1527E20,
                    0x1527E40,
                    0x1527E60,
                    0x1527E80,
                    0x1527EA0,
                    0x1527EC0,
                    0x1527EE0,
                    0x1527F00,
                    0x1527F20,
                    0x1527FC0,
                    0x1527FE0,
                    0x1528020,
                    0x1528060,
                    0x1528080,
                    0x15280A0,
                    0x15280C0,
                    0x15280E0,
                    0x1528100,
                    0x1528120,
                    0x1528160,
                    0x1528180,
                    0x15281A0,
                    0x15281E0,
                    0x1528200,
                    0x1528220,
                    0x1528240,
                    0x1528260,
                    0x1528280,
                    0x15282A0,
                    0x15282C0,
                    0x15282E0,
                    0x1528300,
                    0x1528320,
                    0x1528340,
                    0x1528360,
                    0x1528400,
                    0x1528420,
                    0x1528440,
                    0x1528460,
                    0x1528480,
                    0x15284A0,
                    0x15284C0,
                    0x15284E0,
                    0x1528500,
                    0x1528520,
                    0x1528540,
                    0x1528560,
                    0x1528580,
                    0x1528680,
                    0x1528720,
                    0x1528740,
                    0x1528760,
                    0x1528780
                };
                if (stagePaths.Count > 0) {
                    Tool_stageInfoEditor_code stageInfoEditor = new Tool_stageInfoEditor_code();
                    if (File.Exists(stageInfoPath)) //This code open vanilla duelPlayerParam or edited stageInfo (goes in root folder)
                        stageInfoEditor.OpenFile(stageInfoPath);
                    else {
                        stageInfoEditor.OpenFile(originalstageInfoPath);
                    }
                    for (int i = 0; i < stagePaths.Count; i++) {
                        DirectoryInfo d = new DirectoryInfo(System.IO.Path.GetDirectoryName(stagePaths[i])); //Information about all files in stagemod folder
                        FileInfo[] stage_Files = d.GetFiles("*.txt", SearchOption.AllDirectories); //This function getting info about BGM_ID.txt in stagemod folder
                        FileInfo[] stage_png_Files = d.GetFiles("*.png", SearchOption.AllDirectories); //This function getting info about images in stagemod folder
                        FileInfo[] Files = d.GetFiles("*.xfbin", SearchOption.AllDirectories); //This function getting info about all .xfbin files in stagemod folder
                        FileInfo[] cpk_Files = d.GetFiles("*.cpk", SearchOption.AllDirectories); //This function getting info about all .cpk files in stagemod folder
                        FileInfo[] Shader_Files = d.GetFiles("*.hlsl", SearchOption.AllDirectories); //This function getting info about all .hlsl shaders in stagemod folder

                        string gfx_path = GameRootPath + "\\data\\ui\\flash\\OTHER"; //path to vanilla gfx files
                        DirectoryInfo d_gfx = new DirectoryInfo(gfx_path);
                        FileInfo[] gfx_Files = d_gfx.GetFiles("*.gfx", SearchOption.AllDirectories); //This function getting info about all .gfx files in root game folder

                        DirectoryInfo d_or = new DirectoryInfo(datawin32Path);  //Information about all files in data_win32 folder in root game folder
                        DirectoryInfo d_or2 = new DirectoryInfo(datawin32Path_or);  //Information about all files in data_win32 folder in root game folder
                        List<string> cpk_paths = new List<string>(); //list of paths of cpk files in stagemod folder
                        List<string> cpk_names = new List<string>(); //list of names of cpk files in stagemod folder
                        List<string> shader_paths = new List<string>(); //list of paths of shaders in stagemod folder
                        string dataWinFolder = d_or2.Name + "\\"; //data_win32 name
                        int dataWinFolderLength = dataWinFolder.Length; //data_win32 length (just of name)

                        string ModStageInfoPath = "";
                        foreach (FileInfo file in Files) {
                            if (file.FullName.Contains("stage\\WIN64\\stageInfo.bin.xfbin")) {
                                ModStageInfoPath = file.FullName;
                                break;
                            }
                        }

                        foreach (FileInfo file in Shader_Files) {
                            shader_paths.Add(file.FullName);
                        }
                        string ModStageMessagePath = "";
                        foreach (FileInfo file in stage_Files) {
                            if (file.FullName.Contains("stageMessage.txt")) {
                                ModStageMessagePath = file.FullName;
                                break;
                            }
                        }
                        string mod_img_tex_path = "";
                        foreach (FileInfo file in stage_png_Files) {
                            if (file.FullName.Contains("stage_tex.png")) {
                                mod_img_tex_path = file.FullName;
                                break;
                            }
                        }
                        string ModBGMPath = "";
                        foreach (FileInfo file in stage_Files) {
                            if (file.FullName.Contains("BGM_ID.txt")) {
                                ModBGMPath = file.FullName;
                                break;
                            }
                        }
                        foreach (FileInfo file in cpk_Files) {
                            if (file.FullName.Contains(d.Name)) {
                                cpk_paths.Add("\\\\?\\" + file.FullName);
                                cpk_names.Add(file.Name);
                                break;
                            }
                        }
                        foreach (FileInfo file in Files) {
                            if (!file.Name.Contains("StageInfo")
                                && !file.Name.Contains("characode")
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
                        if (shader_paths.Count > 0) { // This function reading nuccMaterial_dx11.nsh and adding new shaders to it in case, if it exist in mod folder

                            byte[] nuccMaterialFile = File.ReadAllBytes(nuccMaterialDx11Path); // This function reading all bytes from nuccMaterial_dx11 file
                            int EntryCount = MainFunctions.b_ReadIntFromTwoBytes(nuccMaterialFile, 0x0E); // This function reading shader count from nuccMaterial_dx11 file
                            for (int sh = 0; sh < shader_paths.Count; sh++) {
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
                        
                        if (File.Exists(ModStageInfoPath)) {
                            Tool_stageInfoEditor_code stageInfoModFile = new Tool_stageInfoEditor_code();
                            stageInfoModFile.OpenFile(ModStageInfoPath); //This code open modded stageInfo (goes in mod folder)

                            int x = 0;
                            if (!StageOriginalList.Contains(stageInfoModFile.StageNameList[x])) {
                                stageInfoEditor.MainStageSection.Add(stageInfoModFile.MainStageSection[x]);
                                stageInfoEditor.StageNameList.Add(stageInfoModFile.StageNameList[x]);
                                stageInfoEditor.c_sta_List.Add(stageInfoModFile.c_sta_List[x]);
                                stageInfoEditor.BTL_NSX_List.Add(stageInfoModFile.BTL_NSX_List[x]);
                                stageInfoEditor.CountOfFiles.Add(stageInfoModFile.CountOfFiles[x]);
                                stageInfoEditor.CountOfMeshes.Add(stageInfoModFile.CountOfMeshes[x]);
                                stageInfoEditor.MainSection_WeatherSettings.Add(stageInfoModFile.MainSection_WeatherSettings[x]);
                                stageInfoEditor.MainSection_lensFlareSettings.Add(stageInfoModFile.MainSection_lensFlareSettings[x]);
                                stageInfoEditor.MainSection_EnablelensFlareSettings.Add(stageInfoModFile.MainSection_EnablelensFlareSettings[x]);
                                stageInfoEditor.MainSection_X_PositionLightPoint.Add(stageInfoModFile.MainSection_X_PositionLightPoint[x]);
                                stageInfoEditor.MainSection_Y_PositionLightPoint.Add(stageInfoModFile.MainSection_Y_PositionLightPoint[x]);
                                stageInfoEditor.MainSection_Z_PositionLightPoint.Add(stageInfoModFile.MainSection_Z_PositionLightPoint[x]);
                                stageInfoEditor.MainSection_X_PositionShadow.Add(stageInfoModFile.MainSection_X_PositionShadow[x]);
                                stageInfoEditor.MainSection_Y_PositionShadow.Add(stageInfoModFile.MainSection_Y_PositionShadow[x]);
                                stageInfoEditor.MainSection_Z_PositionShadow.Add(stageInfoModFile.MainSection_Z_PositionShadow[x]);
                                stageInfoEditor.MainSection_unk1.Add(stageInfoModFile.MainSection_unk1[x]);
                                stageInfoEditor.MainSection_ShadowSetting_value1.Add(stageInfoModFile.MainSection_ShadowSetting_value1[x]);
                                stageInfoEditor.MainSection_ShadowSetting_value2.Add(stageInfoModFile.MainSection_ShadowSetting_value2[x]);
                                stageInfoEditor.MainSection_PowerLight.Add(stageInfoModFile.MainSection_PowerLight[x]);
                                stageInfoEditor.MainSection_PowerSkyColor.Add(stageInfoModFile.MainSection_PowerSkyColor[x]);
                                stageInfoEditor.MainSection_PowerGlare.Add(stageInfoModFile.MainSection_PowerGlare[x]);
                                stageInfoEditor.MainSection_blur.Add(stageInfoModFile.MainSection_blur[x]);
                                stageInfoEditor.MainSection_X_PositionGlarePoint.Add(stageInfoModFile.MainSection_X_PositionGlarePoint[x]);
                                stageInfoEditor.MainSection_Y_PositionGlarePoint.Add(stageInfoModFile.MainSection_Y_PositionGlarePoint[x]);
                                stageInfoEditor.MainSection_Z_PositionGlarePoint.Add(stageInfoModFile.MainSection_Z_PositionGlarePoint[x]);
                                stageInfoEditor.MainSectionGlareVagueness.Add(stageInfoModFile.MainSectionGlareVagueness[x]);
                                stageInfoEditor.MainSection_ColorGlare.Add(stageInfoModFile.MainSection_ColorGlare[x]);
                                stageInfoEditor.MainSection_ColorSky.Add(stageInfoModFile.MainSection_ColorSky[x]);
                                stageInfoEditor.MainSection_ColorRock.Add(stageInfoModFile.MainSection_ColorRock[x]);
                                stageInfoEditor.MainSection_ColorGroundEffect.Add(stageInfoModFile.MainSection_ColorGroundEffect[x]);
                                stageInfoEditor.MainSection_ColorPlayerLight.Add(stageInfoModFile.MainSection_ColorPlayerLight[x]);
                                stageInfoEditor.MainSection_ColorLight.Add(stageInfoModFile.MainSection_ColorLight[x]);
                                stageInfoEditor.MainSection_ColorShadow.Add(stageInfoModFile.MainSection_ColorShadow[x]);
                                stageInfoEditor.MainSection_ColorUnknown.Add(stageInfoModFile.MainSection_ColorUnknown[x]);
                                stageInfoEditor.MainSection_ColorUnknown2.Add(stageInfoModFile.MainSection_ColorUnknown2[x]);
                                stageInfoEditor.MainSection_EnableGlareSettingValue1.Add(stageInfoModFile.MainSection_EnableGlareSettingValue1[x]);
                                stageInfoEditor.MainSection_EnableGlareSettingValue2.Add(stageInfoModFile.MainSection_EnableGlareSettingValue2[x]);
                                stageInfoEditor.MainSection_EnableGlareSettingValue3.Add(stageInfoModFile.MainSection_EnableGlareSettingValue3[x]);
                                stageInfoEditor.GlareEnabled.Add(stageInfoModFile.GlareEnabled[x]);
                                stageInfoEditor.MainSection_X_MysteriousPosition.Add(stageInfoModFile.MainSection_X_MysteriousPosition[x]);
                                stageInfoEditor.MainSection_Y_MysteriousPosition.Add(stageInfoModFile.MainSection_Y_MysteriousPosition[x]);
                                stageInfoEditor.MainSection_Z_MysteriousPosition.Add(stageInfoModFile.MainSection_Z_MysteriousPosition[x]);
                                stageInfoEditor.MainSection_MysteriousGlareValue1.Add(stageInfoModFile.MainSection_MysteriousGlareValue1[x]);
                                stageInfoEditor.MainSection_MysteriousGlareValue2.Add(stageInfoModFile.MainSection_MysteriousGlareValue2[x]);
                                stageInfoEditor.MainSection_MysteriousGlareValue3.Add(stageInfoModFile.MainSection_MysteriousGlareValue3[x]);
                                stageInfoEditor.MainSection_UnknownValue1.Add(stageInfoModFile.MainSection_UnknownValue1[x]);
                                stageInfoEditor.MainSection_UnknownValue2.Add(stageInfoModFile.MainSection_UnknownValue2[x]);
                                stageInfoEditor.MainSection_UnknownValue3.Add(stageInfoModFile.MainSection_UnknownValue3[x]);
                                stageInfoEditor.SecondarySectionFilePath.Add(stageInfoModFile.SecondarySectionFilePath[x]);
                                stageInfoEditor.SecondarySectionLoadPath.Add(stageInfoModFile.SecondarySectionLoadPath[x]);
                                stageInfoEditor.SecondarySectionLoadMesh.Add(stageInfoModFile.SecondarySectionLoadMesh[x]);
                                stageInfoEditor.SecondarySectionPositionFilePath.Add(stageInfoModFile.SecondarySectionPositionFilePath[x]);
                                stageInfoEditor.SecondarySectionPosition.Add(stageInfoModFile.SecondarySectionPosition[x]);
                                stageInfoEditor.SecondaryTypeSection.Add(stageInfoModFile.SecondaryTypeSection[x]);
                                stageInfoEditor.SecondaryTypeAnimationSection_speed.Add(stageInfoModFile.SecondaryTypeAnimationSection_speed[x]);
                                stageInfoEditor.SecondarySectionCameraValue.Add(stageInfoModFile.SecondarySectionCameraValue[x]);
                                stageInfoEditor.SecondarySectionMysteriousValue.Add(stageInfoModFile.SecondarySectionMysteriousValue[x]);
                                stageInfoEditor.SecondaryConst3C.Add(stageInfoModFile.SecondaryConst3C[x]);
                                stageInfoEditor.SecondaryConst78.Add(stageInfoModFile.SecondaryConst78[x]);
                                stageInfoEditor.SecondaryConstBreakableWallValue1.Add(stageInfoModFile.SecondaryConstBreakableWallValue1[x]);
                                stageInfoEditor.SecondaryConstBreakableWallValue2.Add(stageInfoModFile.SecondaryConstBreakableWallValue2[x]);
                                stageInfoEditor.SecondaryTypeBreakableWall_Effect01.Add(stageInfoModFile.SecondaryTypeBreakableWall_Effect01[x]);
                                stageInfoEditor.SecondaryTypeBreakableWall_Effect02.Add(stageInfoModFile.SecondaryTypeBreakableWall_Effect02[x]);
                                stageInfoEditor.SecondaryTypeBreakableWall_Effect03.Add(stageInfoModFile.SecondaryTypeBreakableWall_Effect03[x]);
                                stageInfoEditor.SecondaryTypeBreakableWall_Sound.Add(stageInfoModFile.SecondaryTypeBreakableWall_Sound[x]);
                                stageInfoEditor.SecondaryTypeBreakableObject_Effect01.Add(stageInfoModFile.SecondaryTypeBreakableObject_Effect01[x]);
                                stageInfoEditor.SecondaryTypeBreakableObject_Effect02.Add(stageInfoModFile.SecondaryTypeBreakableObject_Effect02[x]);
                                stageInfoEditor.SecondaryTypeBreakableObject_Effect03.Add(stageInfoModFile.SecondaryTypeBreakableObject_Effect03[x]);
                                stageInfoEditor.SecondaryTypeBreakableObject_path.Add(stageInfoModFile.SecondaryTypeBreakableObject_path[x]);
                                stageInfoEditor.SecondaryTypeBreakableObject_Speed01.Add(stageInfoModFile.SecondaryTypeBreakableObject_Speed01[x]);
                                stageInfoEditor.SecondaryTypeBreakableObject_Speed02.Add(stageInfoModFile.SecondaryTypeBreakableObject_Speed02[x]);
                                stageInfoEditor.SecondaryTypeBreakableObject_Speed03.Add(stageInfoModFile.SecondaryTypeBreakableObject_Speed03[x]);
                                stageInfoEditor.SecondaryTypeBreakableWall_volume.Add(stageInfoModFile.SecondaryTypeBreakableWall_volume[x]);
                                stageInfoEditor.EntryCount++;
                                stagesToAddList.Add(stageInfoModFile.StageNameList[x]);
                                StageMessageId.Add(stageInfoModFile.c_sta_List[x]);
                                StageText.Add(File.ReadAllLines(ModStageMessagePath).ToList());
                                stagesImagesToAddList.Add(mod_img_tex_path);
                            }
                            else {
                                int index = stageInfoEditor.StageNameList.IndexOf(stageInfoModFile.StageNameList[x]);
                                stageInfoEditor.MainStageSection[index] = stageInfoModFile.MainStageSection[x];
                                stageInfoEditor.c_sta_List[index] = stageInfoModFile.c_sta_List[x];
                                stageInfoEditor.BTL_NSX_List[index] = stageInfoModFile.BTL_NSX_List[x];
                                stageInfoEditor.CountOfFiles[index] = stageInfoModFile.CountOfFiles[x];
                                stageInfoEditor.CountOfMeshes[index] = stageInfoModFile.CountOfMeshes[x];
                                stageInfoEditor.MainSection_WeatherSettings[index] = stageInfoModFile.MainSection_WeatherSettings[x];
                                stageInfoEditor.MainSection_lensFlareSettings[index] = stageInfoModFile.MainSection_lensFlareSettings[x];
                                stageInfoEditor.MainSection_EnablelensFlareSettings[index] = stageInfoModFile.MainSection_EnablelensFlareSettings[x];
                                stageInfoEditor.MainSection_X_PositionLightPoint[index] = stageInfoModFile.MainSection_X_PositionLightPoint[x];
                                stageInfoEditor.MainSection_Y_PositionLightPoint[index] = stageInfoModFile.MainSection_Y_PositionLightPoint[x];
                                stageInfoEditor.MainSection_Z_PositionLightPoint[index] = stageInfoModFile.MainSection_Z_PositionLightPoint[x];
                                stageInfoEditor.MainSection_X_PositionShadow[index] = stageInfoModFile.MainSection_X_PositionShadow[x];
                                stageInfoEditor.MainSection_Y_PositionShadow[index] = stageInfoModFile.MainSection_Y_PositionShadow[x];
                                stageInfoEditor.MainSection_Z_PositionShadow[index] = stageInfoModFile.MainSection_Z_PositionShadow[x];
                                stageInfoEditor.MainSection_unk1[index] = stageInfoModFile.MainSection_unk1[x];
                                stageInfoEditor.MainSection_ShadowSetting_value1[index] = stageInfoModFile.MainSection_ShadowSetting_value1[x];
                                stageInfoEditor.MainSection_ShadowSetting_value2[index] = stageInfoModFile.MainSection_ShadowSetting_value2[x];
                                stageInfoEditor.MainSection_PowerLight[index] = stageInfoModFile.MainSection_PowerLight[x];
                                stageInfoEditor.MainSection_PowerSkyColor[index] = stageInfoModFile.MainSection_PowerSkyColor[x];
                                stageInfoEditor.MainSection_PowerGlare[index] = stageInfoModFile.MainSection_PowerGlare[x];
                                stageInfoEditor.MainSection_blur[index] = stageInfoModFile.MainSection_blur[x];
                                stageInfoEditor.MainSection_X_PositionGlarePoint[index] = stageInfoModFile.MainSection_X_PositionGlarePoint[x];
                                stageInfoEditor.MainSection_Y_PositionGlarePoint[index] = stageInfoModFile.MainSection_Y_PositionGlarePoint[x];
                                stageInfoEditor.MainSection_Z_PositionGlarePoint[index] = stageInfoModFile.MainSection_Z_PositionGlarePoint[x];
                                stageInfoEditor.MainSectionGlareVagueness[index] = stageInfoModFile.MainSectionGlareVagueness[x];
                                stageInfoEditor.MainSection_ColorGlare[index] = stageInfoModFile.MainSection_ColorGlare[x];
                                stageInfoEditor.MainSection_ColorSky[index] = stageInfoModFile.MainSection_ColorSky[x];
                                stageInfoEditor.MainSection_ColorRock[index] = stageInfoModFile.MainSection_ColorRock[x];
                                stageInfoEditor.MainSection_ColorGroundEffect[index] = stageInfoModFile.MainSection_ColorGroundEffect[x];
                                stageInfoEditor.MainSection_ColorPlayerLight[index] = stageInfoModFile.MainSection_ColorPlayerLight[x];
                                stageInfoEditor.MainSection_ColorLight[index] = stageInfoModFile.MainSection_ColorLight[x];
                                stageInfoEditor.MainSection_ColorShadow[index] = stageInfoModFile.MainSection_ColorShadow[x];
                                stageInfoEditor.MainSection_ColorUnknown[index] = stageInfoModFile.MainSection_ColorUnknown[x];
                                stageInfoEditor.MainSection_ColorUnknown2[index] = stageInfoModFile.MainSection_ColorUnknown2[x];
                                stageInfoEditor.MainSection_EnableGlareSettingValue1[index] = stageInfoModFile.MainSection_EnableGlareSettingValue1[x];
                                stageInfoEditor.MainSection_EnableGlareSettingValue2[index] = stageInfoModFile.MainSection_EnableGlareSettingValue2[x];
                                stageInfoEditor.MainSection_EnableGlareSettingValue3[index] = stageInfoModFile.MainSection_EnableGlareSettingValue3[x];
                                stageInfoEditor.GlareEnabled[index] = stageInfoModFile.GlareEnabled[x];
                                stageInfoEditor.MainSection_X_MysteriousPosition[index] = stageInfoModFile.MainSection_X_MysteriousPosition[x];
                                stageInfoEditor.MainSection_Y_MysteriousPosition[index] = stageInfoModFile.MainSection_Y_MysteriousPosition[x];
                                stageInfoEditor.MainSection_Z_MysteriousPosition[index] = stageInfoModFile.MainSection_Z_MysteriousPosition[x];
                                stageInfoEditor.MainSection_MysteriousGlareValue1[index] = stageInfoModFile.MainSection_MysteriousGlareValue1[x];
                                stageInfoEditor.MainSection_MysteriousGlareValue2[index] = stageInfoModFile.MainSection_MysteriousGlareValue2[x];
                                stageInfoEditor.MainSection_MysteriousGlareValue3[index] = stageInfoModFile.MainSection_MysteriousGlareValue3[x];
                                stageInfoEditor.MainSection_UnknownValue1[index] = stageInfoModFile.MainSection_UnknownValue1[x];
                                stageInfoEditor.MainSection_UnknownValue2[index] = stageInfoModFile.MainSection_UnknownValue2[x];
                                stageInfoEditor.MainSection_UnknownValue3[index] = stageInfoModFile.MainSection_UnknownValue3[x];
                                stageInfoEditor.SecondarySectionFilePath[index] = stageInfoModFile.SecondarySectionFilePath[x];
                                stageInfoEditor.SecondarySectionLoadPath[index] = stageInfoModFile.SecondarySectionLoadPath[x];
                                stageInfoEditor.SecondarySectionLoadMesh[index] = stageInfoModFile.SecondarySectionLoadMesh[x];
                                stageInfoEditor.SecondarySectionPositionFilePath[index] = stageInfoModFile.SecondarySectionPositionFilePath[x];
                                stageInfoEditor.SecondarySectionPosition[index] = stageInfoModFile.SecondarySectionPosition[x];
                                stageInfoEditor.SecondaryTypeSection[index] = stageInfoModFile.SecondaryTypeSection[x];
                                stageInfoEditor.SecondaryTypeAnimationSection_speed[index] = stageInfoModFile.SecondaryTypeAnimationSection_speed[x];
                                stageInfoEditor.SecondarySectionCameraValue[index] = stageInfoModFile.SecondarySectionCameraValue[x];
                                stageInfoEditor.SecondarySectionMysteriousValue[index] = stageInfoModFile.SecondarySectionMysteriousValue[x];
                                stageInfoEditor.SecondaryConst3C[index] = stageInfoModFile.SecondaryConst3C[x];
                                stageInfoEditor.SecondaryConst78[index] = stageInfoModFile.SecondaryConst78[x];
                                stageInfoEditor.SecondaryConstBreakableWallValue1[index] = stageInfoModFile.SecondaryConstBreakableWallValue1[x];
                                stageInfoEditor.SecondaryConstBreakableWallValue2[index] = stageInfoModFile.SecondaryConstBreakableWallValue2[x];
                                stageInfoEditor.SecondaryTypeBreakableWall_Effect01[index] = stageInfoModFile.SecondaryTypeBreakableWall_Effect01[x];
                                stageInfoEditor.SecondaryTypeBreakableWall_Effect02[index] = stageInfoModFile.SecondaryTypeBreakableWall_Effect02[x];
                                stageInfoEditor.SecondaryTypeBreakableWall_Effect03[index] = stageInfoModFile.SecondaryTypeBreakableWall_Effect03[x];
                                stageInfoEditor.SecondaryTypeBreakableWall_Sound[index] = stageInfoModFile.SecondaryTypeBreakableWall_Sound[x];
                                stageInfoEditor.SecondaryTypeBreakableObject_Effect01[index] = stageInfoModFile.SecondaryTypeBreakableObject_Effect01[x];
                                stageInfoEditor.SecondaryTypeBreakableObject_Effect02[index] = stageInfoModFile.SecondaryTypeBreakableObject_Effect02[x];
                                stageInfoEditor.SecondaryTypeBreakableObject_Effect03[index] = stageInfoModFile.SecondaryTypeBreakableObject_Effect03[x];
                                stageInfoEditor.SecondaryTypeBreakableObject_path[index] = stageInfoModFile.SecondaryTypeBreakableObject_path[x];
                                stageInfoEditor.SecondaryTypeBreakableObject_Speed01[index] = stageInfoModFile.SecondaryTypeBreakableObject_Speed01[x];
                                stageInfoEditor.SecondaryTypeBreakableObject_Speed02[index] = stageInfoModFile.SecondaryTypeBreakableObject_Speed02[x];
                                stageInfoEditor.SecondaryTypeBreakableObject_Speed03[index] = stageInfoModFile.SecondaryTypeBreakableObject_Speed03[x];
                                stageInfoEditor.SecondaryTypeBreakableWall_volume[index] = stageInfoModFile.SecondaryTypeBreakableWall_volume[x];
                            }
                            if (cpk_paths.Count > 0) {//If character mod contains cpk archives, it will copy paste them in root game folder (requires to have characode in name of cpk)
                                


                                for (int c = 0; c < cpk_paths.Count; c++) {
                                    /*Process p = new Process();
                                    // Redirect the output stream of the child process.
                                    p.StartInfo.UseShellExecute = false;
                                    p.StartInfo.CreateNoWindow = true;
                                    p.StartInfo.FileName = "YACpkTool.exe";
                                    p.StartInfo.Arguments = "-X -i \"" + cpk_paths[c] + "\"";
                                    p.Start();
                                    p.WaitForExit();*/

                                    YaCpkTool.YaCpkTool.CPK_extract(System.IO.Path.GetFullPath(cpk_paths[c]));

                                    string file_name = System.IO.Path.GetFileNameWithoutExtension(cpk_paths[c]);

                                    CopyFilesRecursively(System.IO.Path.GetDirectoryName(cpk_paths[c]) + "\\" + file_name, GameRootPath + "\\moddingapi\\modmanager_assets");
                                    if (Directory.Exists(System.IO.Path.GetDirectoryName(cpk_paths[c]) + "\\" + file_name))
                                        Directory.Delete(System.IO.Path.GetDirectoryName(cpk_paths[c]) + "\\" + file_name, true);
                                    /*if (File.Exists(cpk_paths[c] + ".info")) {
                                        CopyFiles(root_path + "\\moddingapi\\mods\\" + d.Name, cpk_paths[c] + ".info", root_path + "\\moddingapi\\mods\\" + d.Name + "\\" + cpk_names[c] + ".info");

                                    }*/
                                }

                            }
                            if (Directory.Exists(System.IO.Path.GetDirectoryName(mod_img_tex_path)+ "\\moddingapi\\mods\\" + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)))){
                                Directory.CreateDirectory(GameRootPath + "\\moddingapi\\mods\\" + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)));
                                //CopyFilesRecursively(System.IO.Path.GetDirectoryName(mod_img_tex_path) + "\\moddingapi\\mods\\" + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)), GameRootPath + "\\moddingapi\\mods\\" + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)));
                                byte[] BGM_patch = MainFunctions.crc32(System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)));
                                BGM_patch = MainFunctions.b_AddBytes(BGM_patch, BitConverter.GetBytes(Convert.ToInt32(File.ReadAllText(ModBGMPath))));
                                BGM_patch = MainFunctions.b_AddBytes(BGM_patch, new byte[8] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00 });
                                if (FreeBGM_slot.Count > 0) {
                                    File.WriteAllBytes(GameRootPath + "\\moddingapi\\mods\\" + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)) + "\\" + FreeBGM_slot[0].ToString("X2") + ".ns4p", BGM_patch);
                                    FreeBGM_slot.RemoveAt(0);
                                }
                                FileStream ffParameter = new FileStream(GameRootPath + "\\moddingapi\\mods\\" + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)) + "\\info.txt", FileMode.Create, FileAccess.Write);
                                StreamWriter mm_WriterParameter = new StreamWriter(ffParameter);
                                mm_WriterParameter.BaseStream.Seek(0, SeekOrigin.End);
                                mm_WriterParameter.Write(stageName[i] + " - " + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)) + "|" + stageDescription[i] + "|" + stageAuthor[i]);
                                mm_WriterParameter.Flush();
                                mm_WriterParameter.Close();
                                File.WriteAllText(GameRootPath + "\\moddingapi\\mods\\" + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)) + "\\clean.txt", "");
                            } else {
                                Directory.CreateDirectory(GameRootPath + "\\moddingapi\\mods\\" + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)));
                                byte[] BGM_patch = MainFunctions.crc32(System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)));
                                BGM_patch = MainFunctions.b_AddBytes(BGM_patch, BitConverter.GetBytes(Convert.ToInt32(File.ReadAllText(ModBGMPath))));
                                BGM_patch = MainFunctions.b_AddBytes(BGM_patch, new byte[8] { 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00 });
                                if (FreeBGM_slot.Count > 0) {
                                    File.WriteAllBytes(GameRootPath + "\\moddingapi\\mods\\" + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)) + "\\" + FreeBGM_slot[0].ToString("X2") + ".ns4p", BGM_patch);
                                    FreeBGM_slot.RemoveAt(0);
                                }
                                File.WriteAllText(GameRootPath + "\\moddingapi\\mods\\" + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)) + "\\clean.txt", "");
                                FileStream ffParameter = new FileStream(GameRootPath + "\\moddingapi\\mods\\" + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)) + "\\info.txt", FileMode.Create, FileAccess.Write);
                                StreamWriter mm_WriterParameter = new StreamWriter(ffParameter);
                                mm_WriterParameter.BaseStream.Seek(0, SeekOrigin.End);
                                mm_WriterParameter.Write(stageName[i] + " - " + System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(mod_img_tex_path)) + "|" + stageDescription[i] + "|" + stageAuthor[i]);
                                mm_WriterParameter.Flush();
                                mm_WriterParameter.Close();
                                
                            }

                        }
                    }
                    if (!Directory.Exists(System.IO.Path.GetDirectoryName(stageInfoPath))) {
                        Directory.CreateDirectory(System.IO.Path.GetDirectoryName(stageInfoPath));
                    }
                    stageInfoEditor.SaveFileAs(stageInfoPath);

                }
                
                if (stagesToAddList.Count > 0) {
                    
                    byte[] stageSel_file = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\systemFiles\\select_stage.xfbin");
                    byte[] stagesel_header = MainFunctions.b_ReadByteArray(stageSel_file, 0, 0x144);
                    byte[] stagesel_body = MainFunctions.b_ReadByteArray(stageSel_file, 0x144, 0xBEB);
                    byte[] stagesel_end = MainFunctions.b_ReadByteArray(stageSel_file, 0xD39, 0x14);
                    byte[] stagesel_xml_add = new byte[0];
                    byte[] stagesel_new_file = new byte[0];
                    for (int st = 0; st < stagesToAddList.Count; st++) {
                        byte[] xml_line = new byte[0x0E] { 0x0D, 0x0A, 0x09, 0x3C, 0x73, 0x74, 0x61, 0x67, 0x65, 0x20, 0x69, 0x64, 0x3D, 0x22 };
                        xml_line = MainFunctions.b_AddBytes(xml_line, Encoding.ASCII.GetBytes((44 + st).ToString()));
                        xml_line = MainFunctions.b_AddBytes(xml_line, new byte[0x0A] { 0x22, 0x20, 0x6E, 0x61, 0x6D, 0x65, 0x69, 0x64, 0x3D, 0x22 });
                        if (StageText[st].Count != 0)
                            xml_line = MainFunctions.b_AddBytes(xml_line, Encoding.ASCII.GetBytes(("c_modmanager_sta_" + st + 1).ToString()));
                        else
                            xml_line = MainFunctions.b_AddBytes(xml_line, Encoding.ASCII.GetBytes(StageMessageId[st])); 
                        xml_line = MainFunctions.b_AddBytes(xml_line, new byte[0x0B] { 0x22, 0x20, 0x73, 0x74, 0x61, 0x67, 0x65, 0x69, 0x64, 0x3D, 0x22 });
                        xml_line = MainFunctions.b_AddBytes(xml_line, Encoding.ASCII.GetBytes((stagesToAddList[st]).ToString()));
                        xml_line = MainFunctions.b_AddBytes(xml_line, new byte[0x0C] { 0x22, 0x20, 0x68, 0x65, 0x6C, 0x6C, 0x3D, 0x22, 0x30, 0x22, 0x2F, 0x3E });
                        stagesel_xml_add = MainFunctions.b_AddBytes(stagesel_xml_add, xml_line);
                        for (int l = 0; l < 12; l++) {
                            //This function adding all modded entries to all messageInfo files in all languages
                            if (StageText[st].Count != 0) {
                                MessageOriginalFile.CRC32CodesList[l].Add(MainFunctions.crc32(("c_modmanager_sta_" + st + 1).ToString()));
                                MessageOriginalFile.MainTextsList[l].Add(Encoding.UTF8.GetBytes(StageText[st][l].Replace(Program.LANG[l] + "=", "")));
                                MessageOriginalFile.ExtraTextsList[l].Add(Encoding.UTF8.GetBytes(StageText[st][l].Replace(Program.LANG[l] + "=", "")));
                                MessageOriginalFile.ACBFilesList[l].Add(-1);
                                MessageOriginalFile.CueIDsList[l].Add(-1);
                                MessageOriginalFile.VoiceOnlysList[l].Add(0);
                                MessageOriginalFile.EntryCounts[l]++;

                            }
                            
                            //Creates directory for each language
                            if (!Directory.Exists(datawin32Path + "\\message\\WIN64\\" + Program.LANG[l])) {
                                Directory.CreateDirectory(datawin32Path + "\\message\\WIN64\\" + Program.LANG[l]);
                            }
                        }
                        if (File.Exists(stagesImagesToAddList[st])) {
                            byte[] st_img_header = new byte[0];
                            byte[] st_img_body = File.ReadAllBytes(stagesImagesToAddList[st]);
                            byte[] st_img_end = new byte[0x14] { 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x02, 0x00, 0x79, 0x18, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 };
                            if (43 + st>9 && 43 + st < 100) {
                                byte[] st_img = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\systemFiles\\st_img_l_10.xfbin");
                                st_img_header = MainFunctions.b_AddBytes(st_img_header, st_img, 0, 0, 0x12C);
                            }
                            else if (43 + st > 99 && 43 + st < 1000) {
                                byte[] st_img = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\systemFiles\\st_img_l_100.xfbin");
                                st_img_header = MainFunctions.b_AddBytes(st_img_header, st_img, 0, 0, 0x12C);

                            }
                            else if (43+st> 1000 && 43+st< 10000) {
                                byte[] st_img = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\systemFiles\\st_img_l_1000.xfbin");
                                st_img_header = MainFunctions.b_AddBytes(st_img_header, st_img, 0, 0, 0x130);
                            }
                            byte[] st_img_new_file = new byte[0];
                            st_img_new_file = MainFunctions.b_AddBytes(st_img_new_file, st_img_header);
                            st_img_new_file = MainFunctions.b_AddBytes(st_img_new_file, st_img_body);
                            st_img_new_file = MainFunctions.b_AddBytes(st_img_new_file, st_img_end);
                            st_img_new_file = MainFunctions.b_ReplaceBytes(st_img_new_file, BitConverter.GetBytes(st_img_body.Length), st_img_header.Length - 4, 1);
                            st_img_new_file = MainFunctions.b_ReplaceBytes(st_img_new_file, BitConverter.GetBytes(st_img_body.Length+4), st_img_header.Length - 16, 1);
                            int index = MainFunctions.b_FindBytes(st_img_header, new byte[0xA] { 0x73, 0x74, 0x5F, 0x69, 0x6D, 0x67, 0x5F, 0x6C, 0x5F, 0x31 }, 0xA8);
                            st_img_new_file = MainFunctions.b_ReplaceBytes(st_img_new_file, Encoding.ASCII.GetBytes("st_img_l_"+(43+st).ToString()), index);
                            if (!Directory.Exists(datawin32Path + "\\ui\\flash\\OTHER\\stagesel\\tex_l")) {
                                Directory.CreateDirectory(datawin32Path + "\\ui\\flash\\OTHER\\stagesel\\tex_l");
                            }
                            File.WriteAllBytes(datawin32Path + "\\ui\\flash\\OTHER\\stagesel\\tex_l\\st_img_l_"+(43 + st).ToString()+".xfbin", st_img_new_file);
                        }

                    }
                    //stagesel_image.gfx
                    byte[] stagesel_image_original = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\systemFiles\\stagesel_image.gfx");
                    byte[] stagesel_image_header = MainFunctions.b_ReadByteArray(stagesel_image_original, 0x00, 0x55);
                    byte[] stagesel_image_header_add = new byte[0];
                    byte[] stagesel_image_body1 = MainFunctions.b_ReadByteArray(stagesel_image_original, 0x55, 0xC0D);
                    byte[] stagesel_image_body1_add = new byte[0];
                    byte[] stagesel_image_body2 = MainFunctions.b_ReadByteArray(stagesel_image_original, 0xC62, 0x45D);
                    byte[] stagesel_image_body2_add = new byte[0];
                    byte[] stagesel_image_end = MainFunctions.b_ReadByteArray(stagesel_image_original, 0x10BF, 0x8DF);
                    byte[] stagesel_image_new_file = new byte[0];
                    for (int st = 0; st < stagesToAddList.Count; st++) {
                        stagesel_image_header_add = MainFunctions.b_AddBytes(stagesel_image_header_add, new byte[2] { (byte)(0x4C + ("stagesel_image_" + stagesToAddList[st] + ".dds").Length), 0xFC });
                        stagesel_image_header_add = MainFunctions.b_AddBytes(stagesel_image_header_add, BitConverter.GetBytes(st + 1), 0, 0, 2);
                        stagesel_image_header_add = MainFunctions.b_AddBytes(stagesel_image_header_add, new byte[] { 0x09, 0x00, 0x0E, 0x00, 0xa8, 0x00, 0x4c, 0x00, 0x00 });
                        stagesel_image_header_add = MainFunctions.b_AddBytes(stagesel_image_header_add, new byte[1] { (byte)("stagesel_image_" + stagesToAddList[st] + ".dds").Length });
                        stagesel_image_header_add = MainFunctions.b_AddBytes(stagesel_image_header_add, Encoding.ASCII.GetBytes("stagesel_image_" + stagesToAddList[st] + ".dds"));
                        stagesel_image_body1_add = MainFunctions.b_AddBytes(stagesel_image_body1_add, new byte[2] { 0x0C, 0xFC });
                        stagesel_image_body1_add = MainFunctions.b_AddBytes(stagesel_image_body1_add, BitConverter.GetBytes(0x55 + ((st + 1) * 2)), 0, 0, 2);
                        stagesel_image_body1_add = MainFunctions.b_AddBytes(stagesel_image_body1_add, BitConverter.GetBytes(st + 1), 0, 0, 2);
                        stagesel_image_body1_add = MainFunctions.b_AddBytes(stagesel_image_body1_add, new byte[0x0E] { 0x00, 0x00, 0x00, 0x00, 0xA8, 0x00, 0x4C, 0x00, 0xBF, 0x00, 0x33, 0x00, 0x00, 0x00 });
                        stagesel_image_body1_add = MainFunctions.b_AddBytes(stagesel_image_body1_add, BitConverter.GetBytes(0x56 + ((st + 1) * 2)), 0, 0, 2);
                        stagesel_image_body1_add = MainFunctions.b_AddBytes(stagesel_image_body1_add, new byte[0x13] { 0x64, 0xC2, 0x33, 0xE6, 0x84, 0x17, 0xC0, 0x02, 0x41, 0xFF, 0xFF, 0xD9, 0x40, 0x00, 0x05, 0x00, 0x00, 0x00, 0x41 });
                        stagesel_image_body1_add = MainFunctions.b_AddBytes(stagesel_image_body1_add, BitConverter.GetBytes(0x55 + ((st + 1) * 2)), 0, 0, 2);
                        stagesel_image_body1_add = MainFunctions.b_AddBytes(stagesel_image_body1_add, new byte[0x1C] { 0xD9, 0x40, 0x00, 0x05, 0x00, 0x00, 0x0C, 0x98, 0x4D, 0x08, 0x00, 0x20, 0x15, 0x93, 0x09, 0xA1, 0x17, 0x63, 0x3E, 0x3A, 0x57, 0xC3, 0xB2, 0x61, 0x1D, 0x34, 0x20, 0x00 });
                        stagesel_image_body2_add = MainFunctions.b_AddBytes(stagesel_image_body2_add, new byte[0x0C] { 0xFF, 0x0A, (byte)(("img_s_" + (43 + st).ToString()).Length + 1), 0x00, 0x00, 0x00, 0x69, 0x6D, 0x67, 0x5F, 0x73, 0x5F });
                        stagesel_image_body2_add = MainFunctions.b_AddBytes(stagesel_image_body2_add, Encoding.ASCII.GetBytes((43 + st).ToString()));
                        stagesel_image_body2_add = MainFunctions.b_AddBytes(stagesel_image_body2_add, new byte[0x0A] { 0x00, 0x85, 0x06, 0x03, 0x01, 0x00, (byte)(0x56 + ((st + 1) * 2)), 0x00, 0x40, 0x00 });
                    }
                    stagesel_image_end = MainFunctions.b_ReplaceBytes(stagesel_image_end, BitConverter.GetBytes(0x57 + (stagesToAddList.Count * 2)), 0x11, 0, 2);
                    stagesel_image_end = MainFunctions.b_ReplaceBytes(stagesel_image_end, BitConverter.GetBytes(0x2B + stagesToAddList.Count), 0x48, 0, 2);
                    stagesel_image_end = MainFunctions.b_ReplaceBytes(stagesel_image_end, BitConverter.GetBytes(0x58 + (stagesToAddList.Count * 2)), 0x53, 0, 2);
                    stagesel_image_end = MainFunctions.b_ReplaceBytes(stagesel_image_end, BitConverter.GetBytes(0x58 + (stagesToAddList.Count * 2)), 0x8B8, 0, 2);

                    stagesel_image_body2 = MainFunctions.b_ReplaceBytes(stagesel_image_body2, BitConverter.GetBytes(0x45B + stagesel_image_body2_add.Length), 0x4C, 0, 2);
                    stagesel_image_body2 = MainFunctions.b_ReplaceBytes(stagesel_image_body2, BitConverter.GetBytes(0x58 + (stagesToAddList.Count * 2)), 0x50, 0, 2);
                    stagesel_image_body2 = MainFunctions.b_ReplaceBytes(stagesel_image_body2, BitConverter.GetBytes(0x2C + stagesToAddList.Count), 0x52, 0, 2);

                    stagesel_image_new_file = MainFunctions.b_AddBytes(stagesel_image_new_file, stagesel_image_header);
                    stagesel_image_new_file = MainFunctions.b_AddBytes(stagesel_image_new_file, stagesel_image_header_add);
                    stagesel_image_new_file = MainFunctions.b_AddBytes(stagesel_image_new_file, stagesel_image_body1);
                    stagesel_image_new_file = MainFunctions.b_AddBytes(stagesel_image_new_file, stagesel_image_body1_add);
                    stagesel_image_new_file = MainFunctions.b_AddBytes(stagesel_image_new_file, stagesel_image_body2);
                    stagesel_image_new_file = MainFunctions.b_AddBytes(stagesel_image_new_file, stagesel_image_body2_add);
                    stagesel_image_new_file = MainFunctions.b_AddBytes(stagesel_image_new_file, stagesel_image_end);
                    stagesel_image_new_file = MainFunctions.b_ReplaceBytes(stagesel_image_new_file, BitConverter.GetBytes(stagesel_image_new_file.Length), 0x04);
                    File.WriteAllBytes(GameRootPath + "\\data\\ui\\flash\\OTHER\\stagesel\\stagesel_image.gfx", stagesel_image_new_file);

                    //stagesel.gfx
                    List<byte[]> stagesel_slot_posList = new List<byte[]> {
                        new byte[] { 0x9B, 0x06, 0x26, 0xC7, 0x00, 0x09, 0x00, 0x1F, 0x10, 0xF3, 0xB1, 0xE0 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0xC2, 0x00, 0x09, 0x00, 0x1F, 0x45, 0xF7, 0xB1, 0xE0 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0xBD, 0x00, 0x09, 0x00, 0x1F, 0x7A, 0xFB, 0xB1, 0xE0 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0xB8, 0x00, 0x09, 0x00, 0x1D, 0x5F, 0xFE, 0xC7, 0x80 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0xB3, 0x00, 0x09, 0x00, 0x1B, 0x94, 0x0B, 0x1E, 0x00 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0xAE, 0x00, 0x09, 0x00, 0x1A, 0x68, 0x1B, 0x1E, 0x00 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0xA9, 0x00, 0x09, 0x00, 0x1C, 0x9E, 0x16, 0xC7, 0x80 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0xA4, 0x00, 0x09, 0x00, 0x1E, 0x84, 0x0F, 0xB1, 0xE0 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x9F, 0x00, 0x09, 0x00, 0x1E, 0xB9, 0x13, 0xB1, 0xE0 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x9A, 0x00, 0x09, 0x00, 0x1E, 0xEE, 0x23, 0xB1, 0xE0 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x95, 0x00, 0x09, 0x00, 0x1F, 0x10, 0xF3, 0xE4, 0x10 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x90, 0x00, 0x09, 0x00, 0x1F, 0x45, 0xF7, 0xE4, 0x10 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x8B, 0x00, 0x09, 0x00, 0x1F, 0x7A, 0xFB, 0xE4, 0x10 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x86, 0x00, 0x09, 0x00, 0x1D, 0x5F, 0xFF, 0x90, 0x40 },
                        new byte[] { 0xBF, 0x06, 0x1A, 0x00, 0x00, 0x00, 0x26, 0x81, 0x00, 0x09, 0x00, 0x19, 0x28, 0x19, 0x04 },
                        new byte[] { 0xBF, 0x06, 0x1A, 0x00, 0x00, 0x00, 0x26, 0x7C, 0x00, 0x09, 0x00, 0x18, 0xD0, 0x39, 0x04 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x77, 0x00, 0x09, 0x00, 0x1C, 0x9E, 0x17, 0x90, 0x40 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x72, 0x00, 0x09, 0x00, 0x1E, 0x84, 0x0F, 0xE4, 0x10 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x6D, 0x00, 0x09, 0x00, 0x1E, 0xB9, 0x13, 0xE4, 0x10 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x68, 0x00, 0x09, 0x00, 0x1E, 0xEE, 0x23, 0xE4, 0x10 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x63, 0x00, 0x09, 0x00, 0x1F, 0x10, 0xF0, 0x16, 0x40 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x5E, 0x00, 0x09, 0x00, 0x1F, 0x45, 0xF4, 0x16, 0x40 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x59, 0x00, 0x09, 0x00, 0x1F, 0x7A, 0xF8, 0x16, 0x40 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x54, 0x00, 0x09, 0x00, 0x1D, 0x5F, 0xF8, 0x59, 0x00 },
                        new byte[] { 0xBF, 0x06, 0x1A, 0x00, 0x00, 0x00, 0x26, 0x4F, 0x00, 0x09, 0x00, 0x19, 0x28, 0x05, 0x90 },
                        new byte[] { 0xBF, 0x06, 0x1A, 0x00, 0x00, 0x00, 0x26, 0x4A, 0x00, 0x09, 0x00, 0x18, 0xD0, 0x25, 0x90 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x45, 0x00, 0x09, 0x00, 0x1C, 0x9E, 0x10, 0x59, 0x00 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x40, 0x00, 0x09, 0x00, 0x1E, 0x84, 0x0C, 0x16, 0x40 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x3B, 0x00, 0x09, 0x00, 0x1E, 0xB9, 0x10, 0x16, 0x40 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x36, 0x00, 0x09, 0x00, 0x1E, 0xEE, 0x20, 0x16, 0x40 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x31, 0x00, 0x09, 0x00, 0x1F, 0x10, 0xF0, 0x48, 0x80 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x2C, 0x00, 0x09, 0x00, 0x1F, 0x45, 0xF4, 0x48, 0x80 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x27, 0x00, 0x09, 0x00, 0x1F, 0x7A, 0xF8, 0x48, 0x80 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x22, 0x00, 0x09, 0x00, 0x1D, 0x5F, 0xF9, 0x22, 0x00 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x1D, 0x00, 0x09, 0x00, 0x1B, 0x94, 0x04, 0x88, 0x00 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x18, 0x00, 0x09, 0x00, 0x1A, 0x68, 0x14, 0x88, 0x00 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x13, 0x00, 0x09, 0x00, 0x1C, 0x9E, 0x11, 0x22, 0x00 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x0E, 0x00, 0x09, 0x00, 0x1E, 0x84, 0x0C, 0x48, 0x80 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x09, 0x00, 0x09, 0x00, 0x1E, 0xB9, 0x10, 0x48, 0x80 },
                        new byte[] { 0xBF, 0x06, 0x1B, 0x00, 0x00, 0x00, 0x26, 0x04, 0x00, 0x09, 0x00, 0x1E, 0xEE, 0x20, 0x48, 0x80 }
                    };

                    int pageCount = (43 + stagesToAddList.Count)/40;
                    byte[] stagesel_gfx_original = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\systemFiles\\stagesel.gfx");
                    byte[] stagesel_gfx_new_file = new byte[0];
                        byte[] stagesel_gfx_header = MainFunctions.b_ReadByteArray(stagesel_gfx_original, 0x00, 0x2EF);
                        byte[] stagesel_gfx_body = MainFunctions.b_ReadByteArray(stagesel_gfx_original, 0x2EF, 0x5c1);
                        byte[] stagesel_gfx_body_add = new byte[0];
                        byte[] stagesel_gfx_end = MainFunctions.b_ReadByteArray(stagesel_gfx_original, 0x8B0, stagesel_gfx_original.Length - 0x8B0);
                    if (stagesToAddList.Count > 1) {

                        for (int st = 1; st < stagesToAddList.Count; st++) {
                            int slot_index = (43 + st) % 40;
                            byte[] slotInfo = stagesel_slot_posList[slot_index];
                            if (slotInfo.Length == 0x0C) {
                                slotInfo = MainFunctions.b_ReplaceBytes(slotInfo, BitConverter.GetBytes(0xDB + (st * 5)), 0x03, 0, 2);
                                string slotName = "mc_stage_image" + (43 + st).ToString();
                                slotInfo = MainFunctions.b_AddBytes(slotInfo, Encoding.ASCII.GetBytes(slotName));
                                slotInfo = MainFunctions.b_AddBytes(slotInfo, new byte[1]);
                                int newLenght = slotName.Length - 0x10;
                                slotInfo = MainFunctions.b_ReplaceBytes(slotInfo, BitConverter.GetBytes(slotInfo[0] + newLenght), 0x00, 0, 1);
                            } else if (slotInfo.Length == 0x0F || slotInfo.Length == 0x10) {
                                slotInfo = MainFunctions.b_ReplaceBytes(slotInfo, BitConverter.GetBytes(0xDB + (st * 5)), 0x07, 0, 2);
                                string slotName = "mc_stage_image" + (43 + st).ToString();
                                slotInfo = MainFunctions.b_AddBytes(slotInfo, Encoding.ASCII.GetBytes(slotName));
                                slotInfo = MainFunctions.b_AddBytes(slotInfo, new byte[1]);
                                int newLenght = slotName.Length - 0x10;
                                slotInfo = MainFunctions.b_ReplaceBytes(slotInfo, BitConverter.GetBytes(slotInfo[2] + newLenght), 0x02, 0, 1);
                            }
                            stagesel_gfx_body_add = MainFunctions.b_AddBytes(stagesel_gfx_body_add, slotInfo);
                        }
                        
                    }
                    stagesel_gfx_body = MainFunctions.b_AddBytes(stagesel_gfx_body, stagesel_gfx_body_add);
                    stagesel_gfx_body = MainFunctions.b_ReplaceBytes(stagesel_gfx_body, BitConverter.GetBytes(0x5BF + stagesel_gfx_body_add.Length), 0x02, 0, 2);
                    int pos = MainFunctions.b_FindBytes(stagesel_gfx_end, new byte[] { 0x68, 0xB1, 0x07, 0x5E, 0xB2, 0x07, 0x24, 0x02 }) + 7;
                    stagesel_gfx_end = MainFunctions.b_ReplaceBytes(stagesel_gfx_end, BitConverter.GetBytes(pageCount), pos, 0, 1);
                    stagesel_gfx_new_file = MainFunctions.b_AddBytes(stagesel_gfx_new_file, stagesel_gfx_header);
                    stagesel_gfx_new_file = MainFunctions.b_AddBytes(stagesel_gfx_new_file, stagesel_gfx_body);
                    stagesel_gfx_new_file = MainFunctions.b_AddBytes(stagesel_gfx_new_file, stagesel_gfx_end);
                    stagesel_gfx_new_file = MainFunctions.b_ReplaceBytes(stagesel_gfx_new_file, BitConverter.GetBytes(stagesel_gfx_new_file.Length), 0x04);
                    File.WriteAllBytes(GameRootPath + "\\data\\ui\\flash\\OTHER\\stagesel\\stagesel.gfx", stagesel_gfx_new_file);

                    //select_stage.xfbin
                    stagesel_xml_add = MainFunctions.b_AddBytes(stagesel_xml_add, new byte[0x0A] { 0x0D, 0x0A, 0x3C, 0x2F, 0x5F, 0x72, 0x6F, 0x6F, 0x74, 0x3E });
                    stagesel_new_file = MainFunctions.b_AddBytes(stagesel_new_file, stagesel_header);
                    stagesel_new_file = MainFunctions.b_ReplaceBytes(stagesel_new_file, BitConverter.GetBytes(stagesel_body.Length + stagesel_xml_add.Length), 0x140, 1);
                    stagesel_new_file = MainFunctions.b_ReplaceBytes(stagesel_new_file, BitConverter.GetBytes(stagesel_body.Length + stagesel_xml_add.Length + 4), 0x134, 1);
                    stagesel_new_file = MainFunctions.b_AddBytes(stagesel_new_file, stagesel_body);
                    stagesel_new_file = MainFunctions.b_AddBytes(stagesel_new_file, stagesel_xml_add);
                    stagesel_new_file = MainFunctions.b_AddBytes(stagesel_new_file, stagesel_end);

                    if (!Directory.Exists(datawin32Path + "\\ui\\max\\select\\"))
                        Directory.CreateDirectory(datawin32Path + "\\ui\\max\\select\\");
                    File.WriteAllBytes(datawin32Path + "\\ui\\max\\select\\select_stage.xfbin", stagesel_new_file);


                   
                }
                





                string Modgfx_charsel_iconsPath = "";
                bool gfx_charsel_iconsExist = false;
                List<string> pl_sound_files = new List<string>();
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
                        DirectoryInfo d_or2 = new DirectoryInfo(datawin32Path_or);  //Information about all files in data_win32 folder in root game folder
                        List<string> cpk_paths = new List<string>(); //list of paths of cpk files in characode folder
                        List<string> cpk_names = new List<string>(); //list of names of cpk files in characode folder
                        List<string> shader_paths = new List<string>(); //list of paths of shaders in characode folder
                        string dataWinFolder = d_or2.Name + "\\"; //data_win32 name
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
                        string ModdamageprmPath = "";
                        string ModmessagePathList = "";
                        string ModbtlcmnPath = "";
                        string ModspTypeSupportParamPath = "";
                        string Modgfx_charselPath = "";
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
                                cpk_paths.Add("\\\\?\\" + file.FullName);
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
                        foreach (FileInfo file in gfx_Files) {
                            if (file.FullName.Contains("charicon_s\\charicon_s.gfx")) {
                                gfx_charsel_iconsExist = true;
                                Modgfx_charsel_iconsPath = file.FullName;
                                break;
                            } else {
                                gfx_charsel_iconsExist = false;
                                Modgfx_charsel_iconsPath = "";
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
                                if (SkipCharacode.Contains(CharacodeFile.CharacterList.Count + 1)) {
                                    do {
                                        CharacodeFile.AddID("2nrt");
                                    }
                                    while (SkipCharacode.Contains(CharacodeFile.CharacterList.Count+1));
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
                            string root_path = GameRootPath;
                            if (shader_paths.Count > 0) { // This function reading nuccMaterial_dx11.nsh and adding new shaders to it in case, if it exist in mod folder

                                byte[] nuccMaterialFile = File.ReadAllBytes(nuccMaterialDx11Path); // This function reading all bytes from nuccMaterial_dx11 file
                                int EntryCount = MainFunctions.b_ReadIntFromTwoBytes(nuccMaterialFile, 0x0E); // This function reading shader count from nuccMaterial_dx11 file
                                for (int sh = 0; sh < shader_paths.Count; sh++) {
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
                                if (!Directory.Exists(GameRootPath + "\\moddingapi\\modmanager_assets"))
                                    Directory.CreateDirectory(GameRootPath + "\\moddingapi\\modmanager_assets");
                                

                                for (int c = 0; c < cpk_paths.Count; c++) {
                                    Process p = new Process();

                                    YaCpkTool.YaCpkTool.CPK_extract(@System.IO.Path.GetFullPath(cpk_paths[c]));
                                    string file_name = System.IO.Path.GetFileNameWithoutExtension(cpk_paths[c]);

                                    CopyFilesRecursively(System.IO.Path.GetDirectoryName(cpk_paths[c])+ "\\" + file_name, GameRootPath + "\\moddingapi\\modmanager_assets");
                                    if (Directory.Exists(System.IO.Path.GetDirectoryName(cpk_paths[c]) +"\\"+ file_name))
                                        Directory.Delete(System.IO.Path.GetDirectoryName(cpk_paths[c]) + "\\" + file_name, true);
                                }
                                
                            }
                            if (specialCondParamExist || partnerSlotParamExist) { //If anything was added in moddingAPI folder, it will add info.txt file for loading cpk files or conditions for character


                                File.WriteAllText(root_path + "\\moddingapi\\mods\\" + d.Name + "\\clean.txt", ""); 
                                FileStream ffParameter = new FileStream(root_path + "\\moddingapi\\mods\\" + d.Name + "\\info.txt", FileMode.Create, FileAccess.Write);
                                StreamWriter mm_WriterParameter = new StreamWriter(ffParameter);
                                mm_WriterParameter.BaseStream.Seek(0, SeekOrigin.End);
                                mm_WriterParameter.Write(characterName[i] + " - " + d.Name +"|" + characterDescription[i] + "|" + characterAuthor[i]);
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
                                    && !file.Name.Contains("StageInfo")
                                    && !file.Name.Contains("select_stage")
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
                                            while (PresetID_List.Contains(new_presetID) || SkipPSP_PresetID.Contains(new_presetID));
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
                                        while (PresetID_List.Contains(new_presetID) || SkipPSP_PresetID.Contains(new_presetID));
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
                                Tool_IconEditor_code IconVanillaFile = new Tool_IconEditor_code();
                                IconModFile.OpenFile(ModiconPath);
                                if (File.Exists(iconPath)) {
                                    IconOriginalFile.OpenFile(iconPath);
                                    IconVanillaFile.OpenFile(originalIconPath);
                                }
                                else {
                                    IconOriginalFile.OpenFile(originalIconPath);
                                    IconVanillaFile.OpenFile(originalIconPath);
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
                                        if (!player_icon_entries_list.Contains(IconModFile.IconList[c]) && !IconVanillaFile.IconList.Contains(IconModFile.IconList[c])) {
                                            player_icon_entries_list.Add(IconModFile.IconList[c]);
                                        }
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
                                MessageModFile.OpenFilesStart(ModmessagePathList.Remove(ModmessagePathList.IndexOf("\\WIN64"), ModmessagePathList.Length - ModmessagePathList.IndexOf("\\WIN64")));
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
                            

                        }

                    }
                    DirectoryInfo backup_d = new DirectoryInfo(datawin32Path);

                    FileInfo[] sound_Files = backup_d.GetFiles("*.xfbin", SearchOption.AllDirectories);

                    foreach (FileInfo file in sound_Files) {
                        if (file.FullName.Contains("_pl.xfbin") && file.FullName.Contains("\\sound\\PC\\") && !pl_sound_files.Contains(file.FullName)) {
                            pl_sound_files.Add(file.FullName);
                        }
                    }
                    if (pl_sound_files.Count > 0) {
                        for (int s = 0; s < pl_sound_files.Count; s++) {
                            //System.Windows.MessageBox.Show(pl_sound_files[s]);
                            Tool_nus3bankEditor_code BtlcmnModFile = new Tool_nus3bankEditor_code();
                            BtlcmnModFile.OpenFile(pl_sound_files[s]);
                            BtlcmnModFile.FileID = 0x325 + s;
                            BtlcmnModFile.SaveFileAs(pl_sound_files[s]);
                        }
                    }
                    //This function removes all .backup files from data_win32
                    FileInfo[] backup_Files = backup_d.GetFiles("*.backup", SearchOption.AllDirectories);
                    foreach (FileInfo file in backup_Files) {
                        file.Delete();

                    }
                }
                if (gfx_charsel_iconsExist) {
                    byte[] charicon_s_filebytes = File.ReadAllBytes(Directory.GetCurrentDirectory()+"\\systemFiles\\charicon_s.gfx");
                    byte[] charicon_s_header = MainFunctions.b_ReadByteArray(charicon_s_filebytes, 0, 0xAB);
                    byte[] charicon_s_body1 = MainFunctions.b_ReadByteArray(charicon_s_filebytes, 0xAB, 0x3669);
                    byte[] charicon_s_body2 = MainFunctions.b_ReadByteArray(charicon_s_filebytes, 0x3714, 0xF20);
                    byte[] charicon_s_end = MainFunctions.b_ReadByteArray(charicon_s_filebytes, 0x4634, 0xA372); //0x08,0x15,0x7D,0xA2C4 - change counts!
                    byte[] charicon_s_newFile = new byte[0];
                    for (int i =0; i<player_icon_entries_list.Count; i++) {
                        string IconName = player_icon_entries_list[i];
                        byte[] charicon_s_extra_files = new byte[0];
                        charicon_s_extra_files = MainFunctions.b_AddBytes(charicon_s_extra_files, BitConverter.GetBytes((0x4C + (IconName + "_charicon_s.dds").Length)),0,0,1);
                        charicon_s_extra_files = MainFunctions.b_AddBytes(charicon_s_extra_files, new byte[0x1] { 0xFC });
                        charicon_s_extra_files = MainFunctions.b_AddBytes(charicon_s_extra_files, BitConverter.GetBytes(4+i),0,0,2);
                        charicon_s_extra_files = MainFunctions.b_AddBytes(charicon_s_extra_files, new byte[0x9] { 0x09, 0x00, 0x0E, 0x00, 0x80, 0x00, 0x80, 0x00, 0x00 });
                        charicon_s_extra_files = MainFunctions.b_AddBytes(charicon_s_extra_files, BitConverter.GetBytes((IconName + "_charicon_s.dds").Length), 0, 0, 1);
                        charicon_s_extra_files = MainFunctions.b_AddBytes(charicon_s_extra_files, Encoding.ASCII.GetBytes(IconName + "_charicon_s.dds"));
                        charicon_s_header = MainFunctions.b_AddBytes(charicon_s_header, charicon_s_extra_files);
                        byte[] charicon_s_section_temp = new byte[0x47] { 0x0C, 0xFC, 0x85, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x80, 0x00, 0xBF, 0x00, 0x33, 0x00, 0x00, 0x00, 0x86, 0x01, 0x65, 0x80, 0x28, 0x05, 0x80, 0x28, 0x00, 0x02, 0x41, 0xFF, 0xFF, 0xD9, 0x40, 0x00, 0x05, 0x00, 0x00, 0x00, 0x41, 0x85, 0x01, 0xD9, 0x40, 0x00, 0x05, 0x00, 0x00, 0x0C, 0xB0, 0x0B, 0x00, 0x00, 0x20, 0x15, 0x96, 0x01, 0x60, 0x17, 0x62, 0x80, 0x3B, 0x54, 0x01, 0xD9, 0x60, 0x0E, 0xDB, 0x00, 0x00 }; //0x02,0x14,0x29 - counts, 0x04 - DDS ID, 0x06 - x1, 0x08 - y1, 0x0A - x2, 0x0C - y2
                        charicon_s_section_temp = MainFunctions.b_ReplaceBytes(charicon_s_section_temp, BitConverter.GetBytes(0x187 + (i * 2)), 0x2, 0,2);
                        charicon_s_section_temp = MainFunctions.b_ReplaceBytes(charicon_s_section_temp, BitConverter.GetBytes(0x188 + (i * 2)), 0x14, 0, 2);
                        charicon_s_section_temp = MainFunctions.b_ReplaceBytes(charicon_s_section_temp, BitConverter.GetBytes(0x187 + (i * 2)), 0x29, 0, 2);
                        charicon_s_section_temp = MainFunctions.b_ReplaceBytes(charicon_s_section_temp, BitConverter.GetBytes(0x4 + i), 0x4, 0, 2);
                        charicon_s_body1 = MainFunctions.b_AddBytes(charicon_s_body1, charicon_s_section_temp);
                        byte[] charicon_s_name = new byte[0];
                        charicon_s_name = MainFunctions.b_AddBytes(charicon_s_name, new byte[2] { 0xFF, 0x0A });
                        charicon_s_name = MainFunctions.b_AddBytes(charicon_s_name, BitConverter.GetBytes(IconName.Length+1));
                        charicon_s_name = MainFunctions.b_AddBytes(charicon_s_name, Encoding.ASCII.GetBytes(IconName));
                        charicon_s_name = MainFunctions.b_AddBytes(charicon_s_name, new byte[6] { 0x00, 0x85, 0x06, 0x03, 0x01, 0x00 });
                        charicon_s_name = MainFunctions.b_AddBytes(charicon_s_name, BitConverter.GetBytes(0x188 + (i * 2)),0,0,2);
                        charicon_s_name = MainFunctions.b_AddBytes(charicon_s_name, new byte[2] { 0x40, 0x00 });
                        charicon_s_body2 = MainFunctions.b_AddBytes(charicon_s_body2, charicon_s_name);
                    }
                    charicon_s_body2 = MainFunctions.b_ReplaceBytes(charicon_s_body2, BitConverter.GetBytes(charicon_s_body2.Length - 4), 0x02, 0, 2);
                    charicon_s_body2 = MainFunctions.b_ReplaceBytes(charicon_s_body2, BitConverter.GetBytes(0x187+ (player_icon_entries_list.Count*2)), 0x06, 0, 2);
                    charicon_s_body2 = MainFunctions.b_ReplaceBytes(charicon_s_body2, BitConverter.GetBytes(0xC0 + player_icon_entries_list.Count), 0x08, 0, 2);
                    charicon_s_end = MainFunctions.b_ReplaceBytes(charicon_s_end, BitConverter.GetBytes(0x188 + (player_icon_entries_list.Count * 2)), 0x08, 0, 2);
                    charicon_s_end = MainFunctions.b_ReplaceBytes(charicon_s_end, BitConverter.GetBytes(0x187 + (player_icon_entries_list.Count * 2)), 0x15, 0, 2);
                    charicon_s_end = MainFunctions.b_ReplaceBytes(charicon_s_end, BitConverter.GetBytes(0x188 + (player_icon_entries_list.Count * 2)), 0x7D, 0, 2);
                    charicon_s_end = MainFunctions.b_ReplaceBytes(charicon_s_end, BitConverter.GetBytes(0x187 + (player_icon_entries_list.Count * 2)), 0xA2C4, 0, 2);
                    charicon_s_newFile = MainFunctions.b_AddBytes(charicon_s_newFile, charicon_s_header);
                    charicon_s_newFile = MainFunctions.b_AddBytes(charicon_s_newFile, charicon_s_body1);
                    charicon_s_newFile = MainFunctions.b_AddBytes(charicon_s_newFile, charicon_s_body2);
                    charicon_s_newFile = MainFunctions.b_AddBytes(charicon_s_newFile, charicon_s_end);
                    charicon_s_newFile = MainFunctions.b_ReplaceBytes(charicon_s_newFile, BitConverter.GetBytes(charicon_s_newFile.Length), 0x04, 0, 4);
                    File.WriteAllBytes(Modgfx_charsel_iconsPath, charicon_s_newFile);
                }
                //Saves all edited messageInfo files
                MessageOriginalFile.SaveFilesAs(datawin32Path + "\\message");

                YaCpkTool.YaCpkTool.CPK_repack(@System.IO.Path.GetFullPath(GameRootPath + "\\moddingapi\\modmanager_assets"));
                YaCpkTool.YaCpkTool.CPK_repack(@System.IO.Path.GetFullPath(GameRootPath + "\\data_win32_modmanager"));

                File.WriteAllBytes(GameRootPath + "\\moddingapi\\mods\\base_game\\modmanager_assets.cpk.info", new byte[4] { 0x20, 0, 0, 0 });
                File.WriteAllBytes(GameRootPath + "\\moddingapi\\mods\\base_game\\data_win32_modmanager.cpk.info", new byte[4] { 0x21, 0, 0, 0 });

                CopyFiles(GameRootPath + "\\moddingapi\\mods\\base_game", GameRootPath + "\\moddingapi\\modmanager_assets.cpk", GameRootPath + "\\moddingapi\\mods\\base_game\\modmanager_assets.cpk");
                CopyFiles(GameRootPath + "\\moddingapi\\mods\\base_game", GameRootPath + "\\data_win32_modmanager.cpk", GameRootPath + "\\moddingapi\\mods\\base_game\\data_win32_modmanager.cpk");

                if (File.Exists(GameRootPath + "\\moddingapi\\modmanager_assets.cpk"))
                    File.Delete(GameRootPath + "\\moddingapi\\modmanager_assets.cpk");
                if (Directory.Exists(GameRootPath + "\\moddingapi\\modmanager_assets"))
                    Directory.Delete(GameRootPath + "\\moddingapi\\modmanager_assets", true);
                if (File.Exists(GameRootPath + "\\data_win32_modmanager.cpk"))
                    File.Delete(GameRootPath + "\\data_win32_modmanager.cpk");
                if (Directory.Exists(GameRootPath + "\\data_win32_modmanager"))
                    Directory.Delete(GameRootPath + "\\data_win32_modmanager", true);

                WinForms.MessageBox.Show("Finished compiling.");
            }


        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            CleanGameAssets();
        }
        public void CleanGameAssets(bool OpenMessage = true) {
            if (Directory.Exists(GameRootPath)) {
                //This function was used for cleaning data_win32 and moddingapi folders
                WinForms.DialogResult msg = new WinForms.DialogResult();
                if (OpenMessage) {
                    msg = (WinForms.DialogResult)System.Windows.MessageBox.Show("Are you sure you want to clean your game from mods?", "", MessageBoxButton.OKCancel);

                }
                if (msg == WinForms.DialogResult.OK || !OpenMessage) {
                    //Removes data_win32 folder
                    /*if (Directory.Exists(GameRootPath + "\\data_win32")) {
                        Directory.Delete(GameRootPath + "\\data_win32", true);
                    }*/
                    if (Directory.Exists(GameRootPath + "\\data_win32_modmanager")) {
                        Directory.Delete(GameRootPath + "\\data_win32_modmanager", true);
                    }
                    for (int c = 0; c < ImportedCharacodesList.Count; c++) {
                        //Removes characode folders from moddingapi\mods folder
                        if (Directory.Exists(GameRootPath + "\\moddingapi\\mods\\" + ImportedCharacodesList[c])) {
                            Directory.Delete(GameRootPath + "\\moddingapi\\mods\\" + ImportedCharacodesList[c], true);
                        }
                    }
                    if (Directory.Exists(GameRootPath + "\\moddingapi")) {
                        DirectoryInfo d_clean = new DirectoryInfo(GameRootPath + "\\moddingapi\\mods\\");
                        FileInfo[] clean_Files = d_clean.GetFiles("clean.txt", SearchOption.AllDirectories);
                        List<string> CleanPaths = new List<string>();

                        foreach (FileInfo file in clean_Files) {
                            if (file.FullName.Contains("clean.txt")) {
                                if (!CleanPaths.Contains(file.FullName))
                                    CleanPaths.Add(file.FullName);
                            }
                        }

                        for (int c = 0; c < CleanPaths.Count; c++) {
                            if (Directory.Exists(System.IO.Path.GetDirectoryName(CleanPaths[c])))
                                Directory.Delete(System.IO.Path.GetDirectoryName(CleanPaths[c]), true);
                        }
                    }
                    if (File.Exists(GameRootPath + "\\moddingapi\\mods\\base_game\\modmanager_assets.cpk"))
                        File.Delete(GameRootPath + "\\moddingapi\\mods\\base_game\\modmanager_assets.cpk");
                    if (File.Exists(GameRootPath + "\\moddingapi\\mods\\base_game\\modmanager_assets.cpk.info"))
                        File.Delete(GameRootPath + "\\moddingapi\\mods\\base_game\\modmanager_assets.cpk.info");
                    if (File.Exists(GameRootPath + "\\moddingapi\\mods\\base_game\\data_win32_modmanager.cpk"))
                        File.Delete(GameRootPath + "\\moddingapi\\mods\\base_game\\data_win32_modmanager.cpk");
                    if (File.Exists(GameRootPath + "\\moddingapi\\mods\\base_game\\data_win32_modmanager.cpk.info"))
                        File.Delete(GameRootPath + "\\moddingapi\\mods\\base_game\\data_win32_modmanager.cpk.info");
                    if (File.Exists(GameRootPath + "\\moddingapi\\mods\\\\modmanager_assets.cpk"))
                        File.Delete(GameRootPath + "\\moddingapi\\mods\\modmanager_assets.cpk");
                    if (Directory.Exists(GameRootPath + "\\moddingapi\\mods\\\\modmanager_assets"))
                        Directory.Delete(GameRootPath + "\\moddingapi\\mods\\modmanager_assets");
                    if (File.Exists(GameRootPath + "\\data_win32_modmanager.cpk"))
                        File.Delete(GameRootPath + "\\data_win32_modmanager.cpk");
                    if (File.Exists(GameRootPath + "\\data_win32_modmanager.cpk.info"))
                        File.Delete(GameRootPath + "\\data_win32_modmanager.cpk.info");
                    string gfx_path = GameRootPath + "\\data\\ui\\flash\\OTHER";
                    DirectoryInfo d_gfx = new DirectoryInfo(gfx_path);
                    string Modgfx_charselPath = "";
                    string Modgfx_charsel_iconPath = "";
                    FileInfo[] gfx_Files = d_gfx.GetFiles("*.gfx", SearchOption.AllDirectories);

                    foreach (FileInfo file in gfx_Files) {
                        if (file.FullName.Contains("charsel\\charsel.gfx")) {
                            Modgfx_charselPath = file.FullName;
                            break;
                        } else {
                            Modgfx_charselPath = "";
                        }
                    }
                    foreach (FileInfo file in gfx_Files) {
                        if (file.FullName.Contains("charicon_s\\charicon_s.gfx")) {
                            Modgfx_charsel_iconPath = file.FullName;
                            break;
                        } else {
                            Modgfx_charsel_iconPath = "";
                        }
                    }
                    //Replaces edited charsel.gfx file with vanilla file
                    if (File.Exists(Modgfx_charselPath)) {
                        byte[] charsel = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\systemFiles\\charsel.gfx");
                        File.WriteAllBytes(Modgfx_charselPath, charsel);
                    }
                    if (File.Exists(Modgfx_charsel_iconPath)) {
                        byte[] charicon_s = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\systemFiles\\charicon_s.gfx");
                        File.WriteAllBytes(Modgfx_charsel_iconPath, charicon_s);
                    }
                    byte[] stagesel_gfx = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\systemFiles\\stagesel.vanilla.gfx");
                    File.WriteAllBytes(GameRootPath+"\\data\\ui\\flash\\OTHER\\stagesel\\stagesel.gfx", stagesel_gfx);
                    byte[] stagesel_image_gfx = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\systemFiles\\stagesel_image.vanilla.gfx");
                    File.WriteAllBytes(GameRootPath + "\\data\\ui\\flash\\OTHER\\stagesel\\stagesel_image.gfx", stagesel_image_gfx);
                    //Replaces edited nuccMaterial_dx11.nsh file with vanilla file
                    if (File.Exists(GameRootPath + "\\data\\system\\nuccMaterial_dx11.nsh")) {
                        byte[] nuccMat = File.ReadAllBytes(originalnuccMaterialDx11Path);
                        File.WriteAllBytes(GameRootPath + "\\data\\system\\nuccMaterial_dx11.nsh", nuccMat);
                    }
                    if (OpenMessage)
                        WinForms.MessageBox.Show("Game was cleaned");
                }

            } else {
                System.Windows.MessageBox.Show("Select root folder of game");
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e) {

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) {
            //This function was used for installing moddingAPI
            if (Directory.Exists(GameRootPath)) {
                //InstallModdingAPI();
                ExtractModdingAPI();
                WinForms.MessageBox.Show("ModdingAPI Installed");
            } else
                WinForms.MessageBox.Show("Select root folder of game");
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
                WinForms.MessageBox.Show("ModdingAPI Deleted");
            } else
                WinForms.MessageBox.Show("Select root folder of game");
        }

        private void Window_Closed(object sender, EventArgs e) {
            //This function deletes temp files
            if (Directory.Exists(@Directory.GetCurrentDirectory() + "\\temp")) {

                Directory.Delete(@Directory.GetCurrentDirectory() + "\\temp", true);
            }
        }
        public static ImageSource BitmapFromUri(Uri source) {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }
        public void ExtractModdingAPI() {
            //Function for installing ModdingAPI
            if (!Directory.Exists(GameRootPath + "\\moddingapi") && Directory.Exists(@Directory.GetCurrentDirectory() + "\\moddingAPI_files")) {
                
                CopyFilesRecursively(@Directory.GetCurrentDirectory() + "\\moddingAPI_files", GameRootPath);
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
        public void InstallModdingAPI() {
            //Function for installing ModdingAPI
            if (!Directory.Exists(GameRootPath + "\\moddingapi")) {
                //Downloads moddingAPI from gitHub link which is stored in moddingAPI_link.txt file\

                //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
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

        private void ModEnabler_Checked(object sender, RoutedEventArgs e) {
            int x = ModsList.SelectedIndex;
            if (x != -1) {
                EnableMod_List[x] = (bool)ModEnabler.IsChecked;
            }
        }


        private void ModsList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e) {
            if (Directory.Exists(GameModsPath)) {
                OpenFileDialog o = new OpenFileDialog();
                {
                    o.DefaultExt = ".NUS4";
                    o.Filter = "Naruto Storm 4 Mod Manager Archives|*.NUS4|ZIP Archives|*.zip";
                }
                o.ShowDialog();
                if (!(o.FileName != "") || !File.Exists(o.FileName)) {
                    return;
                }
                else {
                    bool installed = InstallMod(System.IO.Path.GetExtension(o.FileName), o.FileName, true);
                    RefreshModList();
                    if (installed)
                        System.Windows.MessageBox.Show("Mod Installed Successfully");
                    else
                        System.Windows.MessageBox.Show("Mod doesn't supported by Mod Manager. Ask author of mod to add support for it!");
                }
            }

        }

        private void Button_Click_5(object sender, RoutedEventArgs e) {
            if (Directory.Exists(GameModsPath)) {
                int x = ModsList.SelectedIndex;
                if (x != -1) {
                    if (Directory.Exists(ModName_List[x])) {
                        ModsList.SelectedIndex = -1;
                        Directory.Delete(ModName_List[x], true);
                        ModName_List.RemoveAt(x);
                        ModInfoList.RemoveAt(x);
                        DescriptionPaths.RemoveAt(x);
                        AuthorPaths.RemoveAt(x);
                        IconPaths.RemoveAt(x);
                        ImportedCharacodesList.RemoveAt(x);
                        ModdingAPI_requirement_Paths.RemoveAt(x);
                        EnableMod_List.RemoveAt(x);
                        RefreshModList();
                        System.Windows.MessageBox.Show("Mod Deleted Successfully");
                    }
                } else {
                    System.Windows.MessageBox.Show("Select mod which you want to delete");
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e) {
            SaveConfig();
        }

        private void ModsList_Drop(object sender, System.Windows.DragEventArgs e) {
            if (Directory.Exists(GameModsPath)) {
                if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop)) {
                    // Note that you can have more than one file.
                    string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                    List<bool> installed = new List<bool>();
                    for (int c = 0; c<files.Length; c++) {

                        string ext = System.IO.Path.GetExtension(files[c]);
                        installed.Add(InstallMod(ext, files[c]));
                    }
                    RefreshModList();
                    if (installed.Contains(true) && !installed.Contains(false))
                        System.Windows.MessageBox.Show("Mods Installed Successfully");
                    else if (installed.Contains(true) && installed.Contains(false)) {
                        string names = "";
                        for (int c = 0; c < files.Length; c++) {
                            if (!installed[c]) {
                                names += System.IO.Path.GetFileNameWithoutExtension(files[c]) + "\n";
                            }
                        }
                        System.Windows.MessageBox.Show("Mods Installed Successfully, but some Mods:\n\n" + names+ "\nwere skipped. Ask author of mods to add support for it!");
                    }
                    else if (!installed.Contains(true) && installed.Contains(false)) {
                        string names = "";
                        for (int c = 0; c< files.Length; c++) {
                            if (!installed[c]) {
                                names += System.IO.Path.GetFileNameWithoutExtension(files[c])+"\n";
                            }
                        }
                        System.Windows.MessageBox.Show("Mods:\n\n" + names+ "\ndoesn't supported by Mod Manager. Ask author of mods to add support for it!");
                    }
                        
                }

                
            }
        }

        public bool InstallMod(string ext, string modPath, bool SingleMod = false) {
            if (!Directory.Exists(@Directory.GetCurrentDirectory() + "\\temp\\" + System.IO.Path.GetFileName(modPath)))
                Directory.CreateDirectory(@Directory.GetCurrentDirectory() + "\\temp\\" + System.IO.Path.GetFileName(modPath));
            else {
                Directory.Delete(@Directory.GetCurrentDirectory() + "\\temp\\" + System.IO.Path.GetFileName(modPath),true);
                Directory.CreateDirectory(@Directory.GetCurrentDirectory() + "\\temp\\" + System.IO.Path.GetFileName(modPath));

            }
            if (ext.Contains("rar")) {
                System.Windows.MessageBox.Show("RAR Archives aren't supported anymore due to problem with crashes for some users. If you have an actual fix for it, leave a report on gitHub.");
                //using (var archive = RarArchive.Open(modPath)) {
                //    foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory)) {
                //        entry.WriteToDirectory(@Directory.GetCurrentDirectory() + "\\temp\\" + System.IO.Path.GetFileName(modPath), new ExtractionOptions() {
                //            ExtractFullPath = true,
                //            Overwrite = true
                //        });
                //    }
                //}
            } else if (System.IO.Path.GetExtension(modPath).Contains("zip") || System.IO.Path.GetExtension(modPath).Contains("nus4")) {
                System.IO.Compression.ZipFile.ExtractToDirectory(modPath, @Directory.GetCurrentDirectory() + "\\temp\\" + System.IO.Path.GetFileName(modPath));
                CopyFilesRecursively(@Directory.GetCurrentDirectory() + "\\temp\\" + System.IO.Path.GetFileName(modPath), GameModsPath);
            }
            bool supportedMod = false;
            
            DirectoryInfo d = new DirectoryInfo(@Directory.GetCurrentDirectory() + "\\temp\\" + System.IO.Path.GetFileName(modPath)); //This function getting info about all files in a path
            FileInfo[] Author_Files = d.GetFiles("Author.txt", SearchOption.AllDirectories); //Getting all files with "Author.txt" name
            foreach (FileInfo file in Author_Files) {
                if (file.FullName.Contains("Author.txt")) {
                    supportedMod = true;
                    break;
                }
            }

            if (supportedMod) {
                CopyFilesRecursively(@Directory.GetCurrentDirectory() + "\\temp\\" + System.IO.Path.GetFileName(modPath), GameModsPath);
                return supportedMod;
            }
            else {
                return supportedMod;
            }
        }

        private void ModsList_DragEnter(object sender, System.Windows.DragEventArgs e) {
            e.Effects = System.Windows.DragDropEffects.Move;
        }
    }
}
