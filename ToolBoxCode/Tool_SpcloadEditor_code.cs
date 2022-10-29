using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_SpcloadEditor_code {
        string fileName = "";
        string prmName = "";
        bool fileOpen = false;
        byte[] fileBytes = new byte[0];
        public int entryCount = 0;
        public List<string> pathList = new List<string>();
        public List<string> nameList = new List<string>();
        public List<byte> typeList = new List<byte>();
        public List<byte[]> loadcondList = new List<byte[]>();
        public void OpenFile(string basepath = "") {
            OpenFileDialog o = new OpenFileDialog();
            if (basepath != "") {
                o.FileName = basepath;
            } else {
                o.ShowDialog();
            }

            if (o.FileName == "" || File.Exists(o.FileName) == false) return;
            fileName = o.FileName;

            fileBytes = File.ReadAllBytes(fileName);
            int fileSectionIndex = XfbinParser.GetFileSectionIndex(fileBytes);
            int startIndex = fileSectionIndex + 0x1C;
            int fileIndex = startIndex;

            // Check for NUCC in header
            if (!(fileBytes.Length > 0x44 && MainFunctions.b_ReadString(fileBytes, 0, 4) == "NUCC")) {
                MessageBox.Show("Not a valid .xfbin file.");
                return;
            }

            // Get character name
            prmName = XfbinParser.GetNameList(fileBytes)[0];
            prmName = prmName.Substring(0, prmName.Length - 0x8);

            // Get entry count
            entryCount = fileBytes[fileSectionIndex + 0x1C];

            for (int x = 0; x < entryCount; x++) {
                fileIndex = startIndex + (0x50 * x);

                int strIndex = fileIndex + 0x8;
                string path = MainFunctions.b_ReadString(fileBytes, strIndex);
                pathList.Add(path);

                strIndex = strIndex + 0x20;
                string name = MainFunctions.b_ReadString(fileBytes, strIndex);
                nameList.Add(name);

                strIndex = strIndex + 0x20;
                typeList.Add(fileBytes[strIndex]);

                strIndex = strIndex + 0x8;
                loadcondList.Add(new byte[] { fileBytes[strIndex], fileBytes[strIndex + 1] });
            }

            fileOpen = true;
        }

        void CloseFile() {
            fileName = "";
            prmName = "";
            fileOpen = false;
            fileBytes = new byte[0];
            entryCount = 0;
            pathList.Clear();
            nameList.Clear();
            typeList.Clear();
            loadcondList.Clear();
        }
    }
}
