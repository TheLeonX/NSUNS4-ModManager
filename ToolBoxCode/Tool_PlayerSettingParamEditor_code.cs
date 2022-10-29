using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_PlayerSettingParamEditor_code {
		public bool FileOpen = false;
		public string FilePath = "";
		public byte[] FileBytes;

		public List<byte[]> PresetList = new List<byte[]>();
		public List<byte[]> CharacodeList = new List<byte[]>();
		public List<int> OptValueA = new List<int>();
		public List<string> CharacterList = new List<string>();
		public List<int> OptValueB = new List<int>();
		public List<int> OptValueC = new List<int>();
		public List<string> c_cha_a_List = new List<string>();
		public List<string> c_cha_b_List = new List<string>();
		public List<int> OptValueD = new List<int>();
		public List<int> OptValueE = new List<int>();

		public int EntryCount = 0;
		public void ClearFile() {
			FileBytes = new byte[0];
			PresetList = new List<byte[]>();
			CharacodeList = new List<byte[]>();
			OptValueA = new List<int>();
			CharacterList = new List<string>();
			OptValueB = new List<int>();
			OptValueC = new List<int>();
			c_cha_a_List = new List<string>();
			c_cha_b_List = new List<string>();
			OptValueD = new List<int>();
			OptValueE = new List<int>();
			EntryCount = 0;
		}
		public void OpenFile(string basepath = "") {
			OpenFileDialog o = new OpenFileDialog();
			{
				o.DefaultExt = ".xfbin";
				o.Filter = "*.xfbin|*.xfbin";
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
			FileOpen = true;
			FilePath = o.FileName;

			FileBytes = File.ReadAllBytes(FilePath);
			EntryCount = FileBytes[304] + FileBytes[305] * 0x100 + FileBytes[306] * 0x10000 + FileBytes[307] * 0x1000000;
			for (int x2 = 0; x2 < EntryCount; x2++) {
				long _ptr = 316 + 0x38 * x2;
				byte[] PresetID = new byte[4]
				{
					FileBytes[_ptr],
					FileBytes[_ptr + 1],
					0,
					0
				};
				byte[] CharacodeID = new byte[4]
				{
					FileBytes[_ptr + 4],
					FileBytes[_ptr + 5],
					0,
					0
				};
				int OptA = FileBytes[_ptr + 8] + FileBytes[_ptr + 9] * 0x100 + FileBytes[_ptr + 10] * 0x10000 + FileBytes[_ptr + 11] * 0x1000000;
				string CharacterID = "";
				long _ptrCharacter3 = FileBytes[_ptr + 16] + FileBytes[_ptr + 17] * 0x100 + FileBytes[_ptr + 18] * 0x10000 + FileBytes[_ptr + 19] * 0x1000000;
				for (int a3 = 0; a3 < 16; a3++) {
					if (FileBytes[_ptr + 16 + _ptrCharacter3 + a3] != 0) {
						string str = CharacterID;
						char c = (char)FileBytes[_ptr + 16 + _ptrCharacter3 + a3];
						CharacterID = str + c;
					} else {
						a3 = 16;
					}
				}
				int OptB = FileBytes[_ptr + 24] + FileBytes[_ptr + 25] * 0x100 + FileBytes[_ptr + 26] * 0x10000 + FileBytes[_ptr + 27] * 0x1000000;
				int OptC = FileBytes[_ptr + 28] + FileBytes[_ptr + 29] * 0x100 + FileBytes[_ptr + 30] * 0x10000 + FileBytes[_ptr + 31] * 0x1000000;
				string c_cha_a = "";
				_ptrCharacter3 = FileBytes[_ptr + 32] + FileBytes[_ptr + 33] * 0x100 + FileBytes[_ptr + 34] * 0x10000 + FileBytes[_ptr + 35] * 0x1000000;
				for (int a2 = 0; a2 < 16; a2++) {
					if (FileBytes[_ptr + 32 + _ptrCharacter3 + a2] != 0) {
						string str2 = c_cha_a;
						char c = (char)FileBytes[_ptr + 32 + _ptrCharacter3 + a2];
						c_cha_a = str2 + c;
					} else {
						a2 = 16;
					}
				}
				string c_cha_b = "";
				_ptrCharacter3 = FileBytes[_ptr + 40] + FileBytes[_ptr + 41] * 0x100 + FileBytes[_ptr + 42] * 0x10000 + FileBytes[_ptr + 43] * 0x1000000;
				for (int a = 0; a < 16; a++) {
					if (FileBytes[_ptr + 40 + _ptrCharacter3 + a] != 0) {
						string str3 = c_cha_b;
						char c = (char)FileBytes[_ptr + 40 + _ptrCharacter3 + a];
						c_cha_b = str3 + c;
					} else {
						a = 16;
					}
				}
				int OptD = FileBytes[_ptr + 48] + FileBytes[_ptr + 49] * 0x100 + FileBytes[_ptr + 50] * 0x10000 + FileBytes[_ptr + 51] * 0x1000000;
				int OptE = FileBytes[_ptr + 52] + FileBytes[_ptr + 53] * 0x100 + FileBytes[_ptr + 54] * 0x10000 + FileBytes[_ptr + 55] * 0x1000000;
				PresetList.Add(PresetID);
				CharacodeList.Add(CharacodeID);
				OptValueA.Add(OptA);
				CharacterList.Add(CharacterID);
				OptValueB.Add(OptB);
				OptValueC.Add(OptC);
				c_cha_a_List.Add(c_cha_a);
				c_cha_b_List.Add(c_cha_b);
				OptValueD.Add(OptD);
				OptValueE.Add(OptE);
			}
		}
		public byte[] ConvertToFile() {
			byte[] actual = new byte[0];

			actual = MainFunctions.b_AddBytes(actual, FileBytes, 0, 0, XfbinParser.GetFileSectionIndex(FileBytes) + 0x2C);
			actual = MainFunctions.b_AddBytes(actual, new byte[EntryCount * 0x38]);
			actual = MainFunctions.b_AddBytes(actual, new byte[0xC]);

			for (int x = 0; x < EntryCount; x++) {
				int entry = XfbinParser.GetFileSectionIndex(FileBytes) + 0x38 + (0x38 * x);

				int characterPointer = actual.Length - (entry + 0x10);
				actual = MainFunctions.b_AddString(actual, CharacterList[x]);
				actual = MainFunctions.b_AddBytes(actual, new byte[0x8 - CharacterList[x].Length]);
				actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(characterPointer), entry + 0x10);

				if (x != 0) {
					int cha_a_pointer = actual.Length - (entry + 0x20);
					actual = MainFunctions.b_AddString(actual, c_cha_a_List[x]);
					actual = MainFunctions.b_AddBytes(actual, new byte[0x10 - c_cha_a_List[x].Length]);
					actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(cha_a_pointer), entry + 0x20);

					int cha_b_pointer = actual.Length - (entry + 0x28);
					actual = MainFunctions.b_AddString(actual, c_cha_b_List[x]);
					actual = MainFunctions.b_AddBytes(actual, new byte[0x10 - c_cha_b_List[x].Length]);
					actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(cha_b_pointer), entry + 0x28);
				}
			}

			for (int x = 0; x < EntryCount; x++) {
				int entry = XfbinParser.GetFileSectionIndex(FileBytes) + 0x38 + (0x38 * x);
				actual = MainFunctions.b_ReplaceBytes(actual, PresetList[x], entry);
				actual = MainFunctions.b_ReplaceBytes(actual, CharacodeList[x], entry + 0x4);
				actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(OptValueA[x]), entry + 0x8);
				actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(OptValueB[x]), entry + 0x18);
				actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(OptValueC[x]), entry + 0x1C);
				actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(OptValueD[x]), entry + 0x30);
				actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(OptValueE[x]), entry + 0x34);
			}

			int fileSize = actual.Length - (XfbinParser.GetFileSectionIndex(FileBytes) + 0x28);
			int startFile = XfbinParser.GetFileSectionIndex(FileBytes) + 0x24;
			actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(fileSize), startFile, 1);
			actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(fileSize + 0x4), startFile - 0xC, 1);
			actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(EntryCount), startFile + 0x8);
			actual = MainFunctions.b_ReplaceBytes(actual, new byte[] { 0x8 }, startFile + 0xC);
			actual = MainFunctions.b_AddBytes(actual, new byte[] { 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x02, 0x00, 0x79, 0x18, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 });

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
	}
}
