using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_appearenceAnmEditor_code {
        public bool FileOpen = false;
		public string FilePath = "";
        public byte[] FileBytes;
		public List<byte[]> CharacodeList = new List<byte[]>();
		public List<string> MeshList = new List<string>();
		public List<List<bool>> SlotList = new List<List<bool>>();
		public List<bool> OneSlotList = new List<bool>();
		public List<bool> TypeSectionList = new List<bool>();
		public List<bool> EnableDisableList = new List<bool>();
		public List<bool> NormalStateList = new List<bool>();
		public List<bool> AwakeningStateList = new List<bool>();
		public List<bool> ReverseSectionList = new List<bool>();
		public List<bool> EnableDisableCutNCList = new List<bool>();
		public List<bool> EnableDisableUltList = new List<bool>();
		public List<bool> EnableDisableWinList = new List<bool>();
		public List<bool> EnableDisableArmorBreakList = new List<bool>();
		public List<int> TimingAwakeList = new List<int>();
		public List<float> TransparenceList = new List<float>();
		public int EntryCount = 0;
		public void CloseFile() {
			ClearFile();
			FileOpen = false;
			FilePath = "";
		}
		public void ClearFile() {
			FileBytes = new byte[0];
			CharacodeList = new List<byte[]>();
			MeshList = new List<string>();
			SlotList = new List<List<bool>>();
			OneSlotList = new List<bool>();
			TypeSectionList = new List<bool>();
			EnableDisableList = new List<bool>();
			NormalStateList = new List<bool>();
			AwakeningStateList = new List<bool>();
			ReverseSectionList = new List<bool>();
			EnableDisableCutNCList = new List<bool>();
			EnableDisableUltList = new List<bool>();
			EnableDisableWinList = new List<bool>();
			EnableDisableArmorBreakList = new List<bool>();
			TimingAwakeList = new List<int>();
			TransparenceList = new List<float>();
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
			EntryCount = FileBytes[292] + FileBytes[293] * 0x100 + FileBytes[294] * 0x10000 + FileBytes[295] * 0x1000000;
			for (int x2 = 0; x2 < EntryCount; x2++) {
				long _ptr = 304 + 0xA0 * x2;
				byte[] CharacodeID = new byte[4]
				{
					FileBytes[_ptr],
					FileBytes[_ptr + 1],
					0,
					0
				};
				bool EnableDisableBool = Convert.ToBoolean(MainFunctions.b_ReadInt(FileBytes, (int)_ptr + 0x10));
				bool EnableDisableNormalStateBool = Convert.ToBoolean(MainFunctions.b_ReadInt(FileBytes, (int)_ptr + 0x14));
				int Timing = FileBytes[_ptr + 24] + FileBytes[_ptr + 25] * 0x100 + FileBytes[_ptr + 26] * 0x10000 + FileBytes[_ptr + 27] * 0x1000000;
				bool EnableDisableAwakeningStateBool = Convert.ToBoolean(MainFunctions.b_ReadInt(FileBytes, (int)_ptr + 0x1C));
				bool ReverseSectionBool = Convert.ToBoolean(MainFunctions.b_ReadInt(FileBytes, (int)_ptr + 0x20));
				bool EnableDisableCutNCBool = Convert.ToBoolean(MainFunctions.b_ReadInt(FileBytes, (int)_ptr + 0x24));
				bool EnableDisableUltBool = Convert.ToBoolean(MainFunctions.b_ReadInt(FileBytes, (int)_ptr + 0x28));
				bool TypeMesh = Convert.ToBoolean(MainFunctions.b_ReadInt(FileBytes, (int)_ptr + 0x34));
				float Transparence = MainFunctions.b_ReadFloat(FileBytes, (int)_ptr + 0x38);
				bool EnableDisableWinBool = Convert.ToBoolean(MainFunctions.b_ReadInt(FileBytes, (int)_ptr + 0x3C));
				bool EnableDisableArmorBreakBool = Convert.ToBoolean(MainFunctions.b_ReadInt(FileBytes, (int)_ptr + 0x4C));
				string MeshMaterialName = "";
				long _ptrCharacter3 = FileBytes[_ptr + 8] + FileBytes[_ptr + 9] * 0x100 + FileBytes[_ptr + 10] * 0x10000 + FileBytes[_ptr + 11] * 0x1000000;
				for (int a3 = 0; a3 < 80; a3++) {
					if (FileBytes[_ptr + 8 + _ptrCharacter3 + a3] != 0) {
						string str = MeshMaterialName;
						char c = (char)FileBytes[_ptr + 8 + _ptrCharacter3 + a3];
						MeshMaterialName = str + c;
					} else {
						a3 = 80;
					}
				}
				OneSlotList = new List<bool>();
				for (int i = 0; i < 20; i++) {
					bool SlotBool = Convert.ToBoolean(MainFunctions.b_ReadInt(FileBytes, (int)_ptr + 0x50 + (4 * i)));
					OneSlotList.Add(SlotBool);
				}
				SlotList.Add(OneSlotList);
				CharacodeList.Add(CharacodeID);
				MeshList.Add(MeshMaterialName);
				TypeSectionList.Add(TypeMesh);
				EnableDisableList.Add(EnableDisableBool);
				NormalStateList.Add(EnableDisableNormalStateBool);
				AwakeningStateList.Add(EnableDisableAwakeningStateBool);
				ReverseSectionList.Add(ReverseSectionBool);
				EnableDisableCutNCList.Add(EnableDisableCutNCBool);
				EnableDisableUltList.Add(EnableDisableUltBool);
				EnableDisableWinList.Add(EnableDisableWinBool);
				EnableDisableArmorBreakList.Add(EnableDisableArmorBreakBool);
				TimingAwakeList.Add(Timing);
				TransparenceList.Add(Transparence);
			}
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
		public byte[] ConvertToFile() {
			List<byte> file = new List<byte>();
			byte[] header = new byte[304]
			{
				0x4E, 0x55, 0x43, 0x43, 0x00, 0x00, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xDC, 0x00, 0x00, 0x00, 0x03, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x3B, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x1E, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x1B, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x30, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x4E, 0x75, 0x6C, 0x6C, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x42, 0x69, 0x6E, 0x61, 0x72, 0x79, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x50, 0x61, 0x67, 0x65, 0x00, 0x6E, 0x75, 0x63, 0x63, 0x43, 0x68, 0x75, 0x6E, 0x6B, 0x49, 0x6E, 0x64, 0x65, 0x78, 0x00, 0x00, 0x62, 0x69, 0x6E, 0x5F, 0x6C, 0x65, 0x2F, 0x78, 0x36, 0x34, 0x2F, 0x61, 0x70, 0x70, 0x65, 0x61, 0x72, 0x61, 0x6E, 0x63, 0x65, 0x41, 0x6E, 0x6D, 0x2E, 0x62, 0x69, 0x6E, 0x00, 0x00, 0x61, 0x70, 0x70, 0x65, 0x61, 0x72, 0x61, 0x6E, 0x63, 0x65, 0x41, 0x6E, 0x6D, 0x00, 0x50, 0x61, 0x67, 0x65, 0x30, 0x00, 0x69, 0x6E, 0x64, 0x65, 0x78, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0xA4, 0x1C, 0x00, 0x00, 0x00, 0x01, 0x00, 0x79, 0x00, 0x00, 0x00, 0x00, 0xA4, 0x18, 0xE9, 0x03, 0x00, 0x00, 0xED, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
			};
			for (int x4 = 0; x4 < header.Length; x4++) {
				file.Add(header[x4]);
			}
			for (int x3 = 0; x3 < EntryCount * 160; x3++) {
				file.Add(0);
			}
			List<int> MeshNamePointer = new List<int>();
			for (int x2 = 0; x2 < EntryCount; x2++) {
				MeshNamePointer.Add(file.Count);
				int nameLength3 = MeshList[x2].Length;
				if (MeshList[x2] == "") {
					nameLength3 = 0;
				} else {
					for (int a17 = 0; a17 < nameLength3; a17++) {
						file.Add((byte)MeshList[x2][a17]);
					}
				}
				for (int a16 = nameLength3; a16 < nameLength3 + 1; a16++) {
					file.Add(0);
				}

				int newPointer3 = MeshNamePointer[x2] - 304 - 160 * x2 - 8;
				byte[] ptrBytes3 = BitConverter.GetBytes(newPointer3);
				if (MeshList[x2] == "") {

					for (int a7 = 0; a7 < 4; a7++) {
						file[304 + 160 * x2 + 8 + a7] = 0;
					}
				} else {
					for (int a7 = 0; a7 < 4; a7++) {
						file[304 + 160 * x2 + 8 + a7] = ptrBytes3[a7];
					}
				}
				// VALUES
				byte[] o_a = CharacodeList[x2];
				for (int a8 = 0; a8 < 4; a8++) {
					file[304 + 160 * x2 + a8] = o_a[a8];
				}


				byte o_c = Convert.ToByte(EnableDisableList[x2]);
				file[304 + 160 * x2 + 16] = o_c;
				o_c = Convert.ToByte(NormalStateList[x2]);
				file[304 + 160 * x2 + 20] = o_c;
				byte[] o_d = BitConverter.GetBytes(TimingAwakeList[x2]);
				for (int a8 = 0; a8 < 4; a8++) {
					file[304 + 160 * x2 + 24 + a8] = o_d[a8];
				}
				o_c = Convert.ToByte(AwakeningStateList[x2]);
				file[304 + 160 * x2 + 28] = o_c;
				byte[] o_h = new byte[4] { 0xFF, 0xFF, 0xFF, 0xFF };
				for (int a8 = 0; a8 < 4; a8++) {
					file[304 + 160 * x2 + 44 + a8] = o_h[a8];
					file[304 + 160 * x2 + 48 + a8] = o_h[a8];
				}
				o_c = Convert.ToByte(ReverseSectionList[x2]);
				file[304 + 160 * x2 + 32] = o_c;
				o_c = Convert.ToByte(EnableDisableCutNCList[x2]);
				file[304 + 160 * x2 + 36] = o_c;
				o_c = Convert.ToByte(EnableDisableUltList[x2]);
				file[304 + 160 * x2 + 40] = o_c;
				o_c = Convert.ToByte(TypeSectionList[x2]);
				file[304 + 160 * x2 + 52] = o_c;
				o_d = BitConverter.GetBytes(TransparenceList[x2]);
				for (int a8 = 0; a8 < 4; a8++) {
					file[304 + 160 * x2 + 56 + a8] = o_d[a8];
				}
				o_c = Convert.ToByte(EnableDisableWinList[x2]);
				file[304 + 160 * x2 + 60] = o_c;
				o_c = Convert.ToByte(EnableDisableArmorBreakList[x2]);
				file[304 + 160 * x2 + 76] = o_c;
				byte[] o_b;
				for (int j = 0; j < 20; j++) {
					if (SlotList[x2][j])
						o_b = new byte[4] { 1, 0, 0, 0 };
					else
						o_b = new byte[4] { 0, 0, 0, 0 };
					for (int a8 = 0; a8 < 4; a8++) {
						file[304 + 160 * x2 + 80 + (4 * j) + a8] = o_b[a8];
					}
				}
			}
			int FileSize3 = file.Count - 288;
			byte[] sizeBytes3 = BitConverter.GetBytes(FileSize3);
			byte[] sizeBytes2 = BitConverter.GetBytes(FileSize3 + 4);
			for (int a20 = 0; a20 < 4; a20++) {
				file[284 + a20] = sizeBytes3[3 - a20];
			}
			for (int a19 = 0; a19 < 4; a19++) {
				file[272 + a19] = sizeBytes2[3 - a19];
			}
			byte[] countBytes = BitConverter.GetBytes(EntryCount);
			for (int a18 = 0; a18 < 4; a18++) {
				file[292 + a18] = countBytes[a18];
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
				121,
				24,
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
			for (int x = 0; x < finalBytes.Length; x++) {
				file.Add(finalBytes[x]);
			}
			return file.ToArray();
		}
	}
}
