using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_RosterEditor_code {
		public bool FileOpen = false;

		public byte[] fileBytes = new byte[] { };

		public string FilePath = "";

		public int EntryCount = 0;

		public List<string> CharacterList = new List<string>();
		public List<int> PageList = new List<int>();
		public List<int> PositionList = new List<int>();
		public List<int> CostumeList = new List<int>();
		public List<string> ChaList = new List<string>();
		public List<string> AccessoryList = new List<string>();
		public List<string> NewIdList = new List<string>();
		public List<byte[]> GibberishBytes = new List<byte[]>();

		public List<int> SearchMaxPositionAndPageIndex() {
			int maxPage = PageList.Max();
			int Pos = 1;
			List<int> NewPositionList = new List<int>();
			List<int> PagePosList = new List<int>();
			for (int x = 0; x < EntryCount; x++) {
				if (PageList[x] == maxPage) {
					NewPositionList.Add(PositionList[x]);
				}
			}
			if (NewPositionList.Contains(27)) {
				maxPage++;
				Pos = 1;

            }
			else {

				Pos = NewPositionList.Max()+1;
			}
			PagePosList.Add(maxPage);
			PagePosList.Add(Pos);
			return PagePosList;
		}
		public int SearchMaxCostumeInPageAndSlotIndex(int page, int pos) {
			List<int> PagePosList = new List<int>();
			for (int x = 0; x < EntryCount; x++) {
				if (PageList[x] == page && PositionList[x] == pos) {
					PagePosList.Add(CostumeList[x]);
				}
			}
			int maxCostume = PagePosList.Max();
			return maxCostume + 1;
		}
		public void OpenFile(string basepath = "") {
			NewFile();
			FileOpen = false;
			OpenFileDialog o = new OpenFileDialog();
			o.DefaultExt = "xfbin";

			if (basepath != "") {
				o.FileName = basepath;
			} else o.ShowDialog();

			if (o.FileName != "" && File.Exists(o.FileName)) {
				FilePath = o.FileName;
				FileOpen = true;
				byte[] FileBytes = File.ReadAllBytes(FilePath);
				fileBytes = FileBytes;
				EntryCount = BitConverter.ToInt16(MainFunctions.b_ReadByteArray(FileBytes, 308, 4), 0);
				for (int x = 0; x < EntryCount; x++) {
					byte[] a = MainFunctions.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x, 4);
					int NamePointer2 = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x, 4));
					string Name = MainFunctions.b_ReadString(FileBytes, NamePointer2 + 0xD8 * x + 320);
					int PageCount = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x + 8, 4));
					int Position = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x + 12, 4));
					int CostumeID = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x + 16, 4));
					int ChaPointer = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x + 24, 4));
					string Cha = MainFunctions.b_ReadString(FileBytes, ChaPointer + 0xD8 * x + 0x140 + 24);
					NamePointer2 = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x + 32, 4));
					string Accessory = MainFunctions.b_ReadString(FileBytes, NamePointer2 + 0x20 + 0x140 + 0xD8 * x);
					NamePointer2 = MainFunctions.b_byteArrayToInt(MainFunctions.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x + 40, 4));
					string NewId = MainFunctions.b_ReadString(FileBytes, NamePointer2 + 0x28 + 0x140 + 0xD8 * x);
					byte[] gib = MainFunctions.b_ReadByteArray(FileBytes, 0x140 + 0xD8 * x + 48, 168);
					CharacterList.Add(Name);
					PageList.Add(PageCount);
					PositionList.Add(Position);
					CostumeList.Add(CostumeID);
					ChaList.Add(Cha);
					AccessoryList.Add(Accessory);
					NewIdList.Add(NewId);
					GibberishBytes.Add(gib);
				}
			}
		}
		public void NewFile() {
			FilePath = "";
			FileOpen = true;
			fileBytes = new byte[] { };
			EntryCount = 0;
			CharacterList = new List<string>();
			PageList = new List<int>();
			PositionList = new List<int>();
			CostumeList = new List<int>();
			ChaList = new List<string>();
			AccessoryList = new List<string>();
			NewIdList = new List<string>();
			GibberishBytes = new List<byte[]>();
		}
		public byte[] ConvertToFile() {
			byte[] actual = new byte[] { };

			int fileStart = XfbinParser.GetFileSectionIndex(fileBytes) + 0x28;
			actual = MainFunctions.b_AddBytes(actual, fileBytes, 0, 0, fileStart + 0x10);
			actual = MainFunctions.b_AddBytes(actual, new byte[0xD8 * EntryCount]);

			for (int x = 0; x < EntryCount; x++) {
				int entryIndex = fileStart + 0x10 + (x * 0xD8);
				actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(actual.Length - entryIndex), entryIndex);
				actual = MainFunctions.b_AddString(actual, CharacterList[x]);
				actual = MainFunctions.b_AddBytes(actual, new byte[0x8 - CharacterList[x].Length]);

				if (ChaList[x] != "") {
					int entryChaIndex = entryIndex + 0x18;
					actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(actual.Length - entryChaIndex), entryChaIndex);
					actual = MainFunctions.b_AddString(actual, ChaList[x]);
					actual = MainFunctions.b_AddBytes(actual, new byte[0x10 - ChaList[x].Length]);
				}

				if (AccessoryList[x] != "") {
					int entryAccIndex = entryIndex + 0x20;
					actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(actual.Length - entryAccIndex), entryAccIndex);
					actual = MainFunctions.b_AddString(actual, AccessoryList[x]);
					actual = MainFunctions.b_AddBytes(actual, new byte[0x10 - AccessoryList[x].Length]);
				}

				if (NewIdList[x] != "") {
					int entryIdIndex = entryIndex + 0x28;
					actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(actual.Length - entryIdIndex), entryIdIndex);
					actual = MainFunctions.b_AddString(actual, NewIdList[x]);
					actual = MainFunctions.b_AddBytes(actual, new byte[0x8 - NewIdList[x].Length]);
				}

				actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(PageList[x]), 0x140 + 0xD8 * x + 8);
				actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(PositionList[x]), 0x140 + 0xD8 * x + 12);
				actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(CostumeList[x]), 0x140 + 0xD8 * x + 16);

				// Add positions and all that crap
				actual = MainFunctions.b_ReplaceBytes(actual, GibberishBytes[x], 0x140 + 0xD8 * x + 48);
			}

			int totalSize = actual.Length - fileStart;

			// Add EOF
			actual = MainFunctions.b_AddBytes(actual, new byte[] { 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x02, 0x00, 0x79, 0x18, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 });

			// Fix sizes
			actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes((short)(EntryCount)), fileStart + 0x2C - 0x28);
			actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(totalSize), fileStart + 0x24 - 0x28, 1);
			actual = MainFunctions.b_ReplaceBytes(actual, BitConverter.GetBytes(totalSize + 4), fileStart + 0x18 - 0x28, 1);

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
