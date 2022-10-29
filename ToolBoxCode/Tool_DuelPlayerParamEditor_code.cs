using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_DuelPlayerParamEditor_code {
		public bool FileOpen = false;
		public string FilePath = "";
		public int EntryCount = 0;
		public List<string> BinPath = new List<string>();
		public List<string> BinName = new List<string>();
		public List<byte[]> Data = new List<byte[]>();
		public List<string> CharaList = new List<string>();
		public List<string[]> CostumeList = new List<string[]>();
		public List<string[]> AwkCostumeList = new List<string[]>();
		public List<string> DefaultAssist1 = new List<string>();
		public List<string> DefaultAssist2 = new List<string>();
		public List<string> AwkAction = new List<string>();
		public List<string[]> ItemList = new List<string[]>();
		public List<byte[]> ItemCount = new List<byte[]>();
		public List<string> Partner = new List<string>();
		public List<byte[]> SettingList = new List<byte[]>();
		public List<byte[]> AwaSettingList = new List<byte[]>();
		public List<byte[]> Setting2List = new List<byte[]>();
		public List<int> EnableAwaSkillList = new List<int>();
		public List<int> VictoryAngleList = new List<int>();
		public List<int> VictoryPosList = new List<int>();
		public List<int> VictoryUnknownList = new List<int>();
		public void OpenFile(string basepath = "") {
			OpenFileDialog o = new OpenFileDialog();
			{
				o.DefaultExt = ".xfbin";
				o.Filter = "*.xfbin|*.xfbin";
			}

			if (basepath == "") {
				o.ShowDialog();
			} else {
				o.FileName = basepath;
			}

			if (!(o.FileName != "") || !File.Exists(o.FileName)) {
				return;
			}
			FileOpen = true;
			EntryCount = 0;
			BinPath.Clear();
			BinName.Clear();
			Data.Clear();
			CharaList.Clear();
			CostumeList.Clear();
			AwkCostumeList.Clear();
			DefaultAssist1.Clear();
			DefaultAssist2.Clear();
			AwkAction.Clear();
			ItemList.Clear();
			ItemCount.Clear();
			Partner.Clear();
			SettingList.Clear();
			Setting2List.Clear();
			EnableAwaSkillList.Clear();
			VictoryAngleList.Clear();
			VictoryPosList.Clear();
			VictoryUnknownList.Clear();
			AwaSettingList.Clear();
			FilePath = o.FileName;
			byte[] FileBytes = File.ReadAllBytes(FilePath);
			EntryCount = MainFunctions.b_byteArrayToIntRev(MainFunctions.b_ReadByteArray(FileBytes, 36, 4)) - 1;
			//if (this.Visible) MessageBox.Show("This file contains " + EntryCount.ToString("X2") + " entries.");
			int Index3 = 128;
			for (int x3 = 0; x3 < EntryCount; x3++) {
				string path = MainFunctions.b_ReadString(FileBytes, Index3);
				BinPath.Add(path);
				Index3 = Index3 + path.Length + 1;
			}
			Index3++;
			for (int x2 = 0; x2 < EntryCount + 2; x2++) {
				string name = MainFunctions.b_ReadString(FileBytes, Index3);
				BinName.Add(name);
				Index3 = Index3 + name.Length + 1;
			}
			BinName.RemoveAt(1);
			BinName.RemoveAt(1);
			int StartOfFile = 68 + MainFunctions.b_byteArrayToIntRev(MainFunctions.b_ReadByteArray(FileBytes, 16, 4));
			for (int x = 0; x < EntryCount; x++) {
				List<byte> data = new List<byte>();
				for (int y = 0; y < 760; y++) {
					data.Add(FileBytes[StartOfFile + 760 * x + 48 * x + y]);
				}
				Data.Add(data.ToArray());
				int _ptr = StartOfFile + 760 * x + 48 * x;
				string characodeid = MainFunctions.b_ReadString(FileBytes, _ptr);
				string[] costumeid = new string[20];
				for (int c2 = 0; c2 < 20; c2++) {
					costumeid[c2] = "";
					string cid = MainFunctions.b_ReadString(FileBytes, _ptr + 8 + 8 * c2);
					if (cid != "") {
						costumeid[c2] = cid;
					}
				}
				string[] awkcostumeid = new string[20];
				for (int c = 0; c < 20; c++) {
					awkcostumeid[c] = "";
					string awkcid = MainFunctions.b_ReadString(FileBytes, _ptr + 168 + 8 * c);
					if (awkcid != "") {
						awkcostumeid[c] = awkcid;
					}
				}
				string defAssist3 = MainFunctions.b_ReadString(FileBytes, _ptr + 420);
				string defAssist2 = MainFunctions.b_ReadString(FileBytes, _ptr + 428);
				string awkaction = MainFunctions.b_ReadString(FileBytes, _ptr + 484);
				string[] itemlist = new string[4];
				byte[] itemcount = new byte[4];
				for (int i = 0; i < 4; i++) {
					itemlist[i] = "";
					itemcount[i] = 0;
					string item = MainFunctions.b_ReadString(FileBytes, _ptr + 516 + 32 * i);
					byte count = FileBytes[_ptr + 546 + 32 * i];
					if (item != "") {
						itemlist[i] = item;
						itemcount[i] = count;
					}
				}
				SettingList.Add(MainFunctions.b_ReadByteArray(FileBytes, _ptr + 448, 36));
				Setting2List.Add(MainFunctions.b_ReadByteArray(FileBytes, _ptr + 500, 16));
				EnableAwaSkillList.Add(FileBytes[_ptr + 0x153]);
				VictoryAngleList.Add(FileBytes[_ptr + 0x1B8]);
				VictoryPosList.Add(FileBytes[_ptr + 0x1B6]);
				VictoryUnknownList.Add(FileBytes[_ptr + 0x1B4]);

				AwaSettingList.Add(MainFunctions.b_ReadByteArray(FileBytes, _ptr + 644, 84));
				string partner = MainFunctions.b_ReadString(FileBytes, _ptr + 328);
				CharaList.Add(characodeid);
				CostumeList.Add(costumeid);
				AwkCostumeList.Add(awkcostumeid);
				DefaultAssist1.Add(defAssist3);
				DefaultAssist2.Add(defAssist2);
				AwkAction.Add(awkaction);
				ItemList.Add(itemlist);
				ItemCount.Add(itemcount);
				Partner.Add(partner);
			}
			Index3++;
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
			FileOpen = false;
			FilePath = "";
		}
		public byte[] ConvertToFile() {
			// Build the header
			int totalLength4 = 0;

			byte[] fileBytes36 = new byte[0];
			fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[127]
			{
				78,
				85,
				67,
				67,
				0,
				0,
				0,
				121,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				73,
				216,
				0,
				0,
				0,
				3,
				0,
				121,
				20,
				2,
				0,
				0,
				0,
				4,
				0,
				0,
				0,
				59,
				0,
				0,
				0,
				219,
				0,
				0,
				39,
				47,
				0,
				0,
				0,
				221,
				0,
				0,
				10,
				71,
				0,
				0,
				0,
				221,
				0,
				0,
				10,
				92,
				0,
				0,
				3,
				104,
				0,
				0,
				0,
				0,
				110,
				117,
				99,
				99,
				67,
				104,
				117,
				110,
				107,
				78,
				117,
				108,
				108,
				0,
				110,
				117,
				99,
				99,
				67,
				104,
				117,
				110,
				107,
				66,
				105,
				110,
				97,
				114,
				121,
				0,
				110,
				117,
				99,
				99,
				67,
				104,
				117,
				110,
				107,
				80,
				97,
				103,
				101,
				0,
				110,
				117,
				99,
				99,
				67,
				104,
				117,
				110,
				107,
				73,
				110,
				100,
				101,
				120,
				0
			});

			int PtrNucc = fileBytes36.Length;
			fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[1]);

			for (int x6 = 0; x6 < EntryCount; x6++) {
				fileBytes36 = MainFunctions.b_AddString(fileBytes36, BinPath[x6]);
				fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[1]);
			}

			int PtrPath = fileBytes36.Length;
			fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[1]);

			for (int x5 = 0; x5 < 1; x5++) {
				fileBytes36 = MainFunctions.b_AddString(fileBytes36, BinName[x5]);
				fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[1]);
			}

			fileBytes36 = MainFunctions.b_AddString(fileBytes36, "Page0");
			fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[1]);
			fileBytes36 = MainFunctions.b_AddString(fileBytes36, "index");
			fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[1]);

			for (int x4 = 1; x4 < EntryCount; x4++) {
				fileBytes36 = MainFunctions.b_AddString(fileBytes36, BinName[x4]);
				fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[1]);
			}

			int PtrName = fileBytes36.Length;
			totalLength4 = PtrName;
			int AddedBytes = 0;

			while (fileBytes36.Length % 4 != 0) {
				AddedBytes++;
				fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[1]);
			}

			// Build bin1
			totalLength4 = fileBytes36.Length;
			fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[48]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				2,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				2,
				0,
				0,
				0,
				3,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				3
			});

			for (int x3 = 1; x3 < EntryCount; x3++) {
				int actualEntry = x3 - 1;
				fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4]
				{
					0,
					0,
					0,
					1
				});
				byte[] xbyte = BitConverter.GetBytes(2 + actualEntry);
				byte[] ybyte = BitConverter.GetBytes(4 + actualEntry);
				fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, xbyte, 1);
				fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, ybyte, 1);
			}

			int PtrSection = fileBytes36.Length;
			fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[16]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				1,
				0,
				0,
				0,
				2,
				0,
				0,
				0,
				3
			});
			for (int x2 = 1; x2 < EntryCount; x2++) {
				int actualEntry2 = x2 - 1;
				fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4]);
				byte[] xbyte2 = BitConverter.GetBytes(4 + actualEntry2);
				fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, xbyte2, 1);
				fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4]
				{
					0,
					0,
					0,
					2
				});
				fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, new byte[4]
				{
					0,
					0,
					0,
					3
				});
			}

			totalLength4 = fileBytes36.Length;

			int PathLength = PtrPath - 127;
			int NameLength = PtrName - PtrPath;
			int Section1Length = PtrSection - PtrName - AddedBytes;
			int FullLength = totalLength4 - 68 + 40;
			int ReplaceIndex8 = 16;
			byte[] buffer8 = BitConverter.GetBytes(FullLength);
			fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			ReplaceIndex8 = 36;
			buffer8 = BitConverter.GetBytes(EntryCount + 1);
			fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			ReplaceIndex8 = 40;
			buffer8 = BitConverter.GetBytes(PathLength);
			fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			ReplaceIndex8 = 44;
			buffer8 = BitConverter.GetBytes(EntryCount + 3);
			fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			ReplaceIndex8 = 48;
			buffer8 = BitConverter.GetBytes(NameLength);
			fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			ReplaceIndex8 = 52;
			buffer8 = BitConverter.GetBytes(EntryCount + 3);
			fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			ReplaceIndex8 = 56;
			buffer8 = BitConverter.GetBytes(Section1Length);
			fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			ReplaceIndex8 = 60;
			buffer8 = BitConverter.GetBytes(EntryCount * 4);
			fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, buffer8, ReplaceIndex8, 1);
			for (int x = 0; x < EntryCount; x++) {
				fileBytes36 = ((x != 0) ? MainFunctions.b_AddBytes(fileBytes36, new byte[48]
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
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					99,
					0,
					0,
					0,
					0,
					2,
					252,
					0,
					0,
					0,
					1,
					0,
					99,
					0,
					0,
					0,
					0,
					2,
					248
				}) : MainFunctions.b_AddBytes(fileBytes36, new byte[40]
				{
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					121,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					99,
					0,
					0,
					0,
					0,
					2,
					252,
					0,
					0,
					0,
					1,
					0,
					99,
					0,
					0,
					0,
					0,
					2,
					248
				}));
				fileBytes36 = MainFunctions.b_AddBytes(fileBytes36, Data[x].ToArray());
				int _ptr = 68 + FullLength + 48 * x + 760 * x;
				fileBytes36 = MainFunctions.b_ReplaceString(fileBytes36, CharaList[x], _ptr, 8);
				for (int i = 0; i < 20; i++) {
					fileBytes36 = MainFunctions.b_ReplaceString(fileBytes36, CostumeList[x][i], _ptr + 8 + 8 * i, 8);
					fileBytes36 = MainFunctions.b_ReplaceString(fileBytes36, AwkCostumeList[x][i], _ptr + 168 + 8 * i, 8);
				}

				fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, new byte[1] { (byte)EnableAwaSkillList[x] }, _ptr + 0x153);
				fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, new byte[1] { (byte)VictoryUnknownList[x] }, _ptr + 0x1B4);
				fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, new byte[1] { (byte)VictoryPosList[x] }, _ptr + 0x1B6);
				fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, new byte[1] { (byte)VictoryAngleList[x] }, _ptr + 0x1B8);
				fileBytes36 = MainFunctions.b_ReplaceString(fileBytes36, DefaultAssist1[x], _ptr + 420, 8);
				fileBytes36 = MainFunctions.b_ReplaceString(fileBytes36, DefaultAssist2[x], _ptr + 428, 8);
				fileBytes36 = MainFunctions.b_ReplaceString(fileBytes36, AwkAction[x], _ptr + 484, 16);
				fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, SettingList[x], _ptr + 448);
				fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, Setting2List[x], _ptr + 500);
				fileBytes36 = MainFunctions.b_ReplaceBytes(fileBytes36, AwaSettingList[x], _ptr + 644);
				fileBytes36 = MainFunctions.b_ReplaceString(fileBytes36, ItemList[x][0], _ptr + 516, 16);
				fileBytes36[_ptr + 546] = ItemCount[x][0];
				fileBytes36 = MainFunctions.b_ReplaceString(fileBytes36, ItemList[x][1], _ptr + 548, 16);
				fileBytes36[_ptr + 578] = ItemCount[x][1];
				fileBytes36 = MainFunctions.b_ReplaceString(fileBytes36, ItemList[x][2], _ptr + 580, 16);
				fileBytes36[_ptr + 610] = ItemCount[x][2];
				fileBytes36 = MainFunctions.b_ReplaceString(fileBytes36, ItemList[x][3], _ptr + 612, 16);
				fileBytes36[_ptr + 642] = ItemCount[x][3];
				fileBytes36 = MainFunctions.b_ReplaceString(fileBytes36, Partner[x], _ptr + 328, 8);
			}
			return MainFunctions.b_AddBytes(fileBytes36, new byte[20]
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
			});
		}

	}
}
