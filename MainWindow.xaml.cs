using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            if (File.Exists(ConfigPath) == false) {
                CreateConfig();
            } else {
                LoadConfig();
            }
            RefreshModList();
        }

        public static string ConfigPath = AppDomain.CurrentDomain.BaseDirectory + "\\config.txt";
        public static string GameRootPath = "[null]";
        public static string GameModsPath = "[null]";
        public List<string> ModName_List = new List<string>();
        public List<string> IconPaths = new List<string>();
        public List<string> AuthorPaths = new List<string>();
        public List<string> DescriptionPaths = new List<string>();
        public List<ModList_class> ModInfoList = new List<ModList_class>();
        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog c = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            c.IsFolderPicker = true;

            if (c.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok) {
                GameRootPath = c.FileName;
            }
            SaveConfig();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) {
            Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog c = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog();
            c.IsFolderPicker = true;

            if (c.ShowDialog() == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok) {
                GameModsPath = c.FileName;
            }
            SaveConfig();
        }

        void CreateConfig() {
            List<string> cfg = new List<string>();
            cfg.Add("[null]");
            cfg.Add("[null]");
            File.WriteAllLines(ConfigPath, cfg.ToArray());
            MessageBox.Show("Config file created.");
        }

        void SaveConfig() {
            List<string> cfg = new List<string>();
            cfg.Add(GameRootPath);
            cfg.Add(GameModsPath);
            File.WriteAllLines(ConfigPath, cfg.ToArray());
            MessageBox.Show("Config file saved.");
        }

        public static void LoadConfig() {
            string[] cfg = File.ReadAllLines(ConfigPath);
            if (cfg.Length > 0) GameRootPath = cfg[0];
            if (cfg.Length > 1) GameModsPath = cfg[1];
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            RefreshModList();
        }

        public void RefreshModList() {
            if (Directory.Exists(GameModsPath)) {
                DirectoryInfo d = new DirectoryInfo(@GameModsPath);
                FileInfo[] Description_Files = d.GetFiles("Description.txt", SearchOption.AllDirectories);
                FileInfo[] Author_Files = d.GetFiles("Author.txt", SearchOption.AllDirectories);
                FileInfo[] Icon_Files = d.GetFiles("Icon.png", SearchOption.AllDirectories);
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
                ModsList.ItemsSource = ModInfoList;
            }
        }

        private void ModsList_Selected(object sender, RoutedEventArgs e) {
            int x = ModsList.SelectedIndex;
            MessageBox.Show(x.ToString());
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
    }
}
