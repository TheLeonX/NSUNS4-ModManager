using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_CharacodeEditor_code {
		public bool FileOpen = false;
		public string FilePath = "";
		public byte[] fileBytes = new byte[0];
		public List<string> CharacterList = new List<string>();
		public int CharacterCount = 0;
		public void OpenFile(string path = "") {
			OpenFileDialog o = new OpenFileDialog();
			o.DefaultExt = "xfbin";

			if (path == "") o.ShowDialog();
			else o.FileName = path;

			if (!(o.FileName != "") || !File.Exists(o.FileName)) {
				return;
			}

			FilePath = o.FileName;
			fileBytes = File.ReadAllBytes(FilePath);

			// Check for NUCC in header
			if (!(fileBytes.Length > 0x44 && MainFunctions.b_ReadString(fileBytes, 0, 4) == "NUCC")) {
				return;
			}

			if (XfbinParser.GetNameList(fileBytes)[0] == "characode") {
				int fileStart = XfbinParser.GetFileSectionIndex(fileBytes);
				CharacterCount = MainFunctions.b_ReadInt(fileBytes, fileStart + 0x1C);

				CharacterList = new List<string>();
				for (int x = 0; x < CharacterCount; x++) {
					string character = MainFunctions.b_ReadString(fileBytes, fileStart + 0x20 + (x * 8));
					CharacterList.Add(character);
				}

				FileOpen = true;
				//if (this.Visible) MessageBox.Show("Characode contains " + CharacterCount + " character IDs.");
			} else {
				MessageBox.Show("Please select a valid characode file.");
				FilePath = "";
				fileBytes = new byte[0];
				FileOpen = false;
			}
		}
		public byte[] ConvertToFile() {
			byte[] actual = new byte[0];
			int startOfFile = XfbinParser.GetFileSectionIndex(fileBytes);
			for (int x = 0; x < startOfFile + 0x20; x++) actual = MainFunctions.b_AddBytes(actual, new byte[] { fileBytes[x] });

			actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(CharacterCount), startOfFile + 0x20 - 0x4);
			actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes((CharacterCount * 8) + 0x4), startOfFile + 0x20 - 0x8, 1);
			actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes((CharacterCount * 8) + 0x8), startOfFile + 0x20 - 0x8 - 0xC, 1);

			for (int x = 0; x < CharacterCount; x++) {
				actual = MainFunctions.b_AddBytes(actual, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
				actual = MainFunctions.b_ReplaceString(actual, CharacterList[x], startOfFile + 0x20 + (0x8 * x));
			}

			byte[] finalBytes = new byte[20]
			{
				0,
				0,
				0,
				8,
				0,
				0,
				0,
				2,
				0,
				99,
				0,
				0,
				0,
				0,
				0,
				4,
				0,
				0,
				0,
				0
			};

			actual = MainFunctions.b_AddBytes(actual, finalBytes);
			return actual;
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

		public void CloseFile() {
			CharacterList.Clear();
			CharacterCount = 0;
			FilePath = "";
			fileBytes = new byte[0];
			FileOpen = false;
		}
	}
}
