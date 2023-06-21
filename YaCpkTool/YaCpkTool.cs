using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using CriCpkMaker;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Controls;

namespace NSUNS4_ModManager.YaCpkTool
{
    class YaCpkTool
    {

        public static void CPK_extract(string extractWhat) {
            CpkMaker cpkMaker = new CpkMaker();
            CAsyncFile cManager = new CAsyncFile();
            if (!cpkMaker.AnalyzeCpkFile(extractWhat)) {
                MessageBox.Show("Error: AnalyzeCpkFile returned false!");
                return;
            }
            CFileData cpkFileData = cpkMaker.FileData;

            string outFileName = Path.GetDirectoryName(extractWhat).Replace('/', '\\') + "\\" + Path.GetFileNameWithoutExtension(extractWhat);
            //MessageBox.Show(outFileName);
            Directory.CreateDirectory(outFileName);
            cpkMaker.StartToExtract(outFileName); // Continues at STEP 4

            int last_p = -1;
            int percent = 0;
            CriCpkMaker.Status status = cpkMaker.Execute();
            while ((status > CriCpkMaker.Status.Stop) && (percent < 100)) {
                percent = (int)Math.Floor(cpkMaker.GetProgress());
                if (percent > last_p) {
                    //Console.CursorLeft = 0;
                    //Console.Write(percent.ToString() + "% " + (true ? "extracted" : "packed") + "...");
                    last_p = percent;
                }
                status = cpkMaker.Execute();
            }
            //Console.WriteLine("");
            //Console.WriteLine("Status = " + status.ToString());


        }

        public static void CPK_repack(string directory, string save_directory = "") {
            CpkMaker cpkMaker = new CpkMaker();
            CAsyncFile cManager = new CAsyncFile();
            EnumCompressCodec compressCodec = EnumCompressCodec.CodecDpk;
            uint dataAlign = 2048;

            cpkMaker.CpkFileMode = CpkMaker.EnumCpkFileMode.ModeFilename;
            cpkMaker.CompressCodec = compressCodec;
            cpkMaker.DataAlign = dataAlign;

            uint i = 0;
            string[] files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
            foreach (string file in files) {
                if (File.Exists(file)) {
                    string localpath = file.Replace(directory, "");
                    localpath = localpath.Replace('\\', '/');
                    if (localpath[0] == '/') { localpath = localpath.Substring(1); }
                    //Console.WriteLine("Local path = \"" + localpath + "\"");
                    cpkMaker.AddFile(file, localpath, i++, (((int)compressCodec == 1) ? false : true), "", "", dataAlign);
                }
            }

            if (save_directory == "") {
                save_directory = directory.Replace('/', '\\') + ".cpk";
            }

            File.Create(save_directory).Close();
            cpkMaker.StartToBuild(save_directory); // Continues at STEP 4

            int last_p = -1;
            int percent = 0;
            CriCpkMaker.Status status = cpkMaker.Execute();
            while ((status > CriCpkMaker.Status.Stop) && (percent < 100)) {
                percent = (int)Math.Floor(cpkMaker.GetProgress());
                if (percent > last_p) {
                    //Console.CursorLeft = 0;
                    //Console.Write(percent.ToString() + "% " + (true ? "extracted" : "packed") + "...");
                    last_p = percent;
                }
                status = cpkMaker.Execute();
            }
        }
    }

    
}
