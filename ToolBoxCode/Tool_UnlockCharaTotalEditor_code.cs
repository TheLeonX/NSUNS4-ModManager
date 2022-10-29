using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NSUNS4_ModManager.ToolBoxCode {
    class Tool_UnlockCharaTotalEditor_code {
		public bool FileOpen = false;

		public string FilePath = "";

		public byte[] FileBytes = new byte[0];

		public List<List<byte>> EntryList = new List<List<byte>>();

		public int EntryCount = 0;
		public void AddID_Importer(int PresetID, int type) {
			byte[] presetID = BitConverter.GetBytes(PresetID);
			byte[] sectionBytes = new byte[12]
			{
				presetID[0],
				presetID[1],
				0,
				0,
				1,
				0,
				0,
				0,
				(byte)type,
				0,
				0,
				0
			};
			EntryList.Add(sectionBytes.ToList());
			EntryCount++;
		}
		public void OpenFile(string basepath = "") {
			if (FileOpen) {
				CloseFile();
			}
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
			FileOpen = true;
			FilePath = o.FileName;
			FileBytes = File.ReadAllBytes(FilePath);
			EntryCount = FileBytes[300] + FileBytes[301] * 256 + FileBytes[302] * 65536 + FileBytes[303] * 16777216;
			EntryList = new List<List<byte>>();
			for (int x = 0; x < EntryCount; x++) {
				List<byte> character = new List<byte>();
				for (int a = 0; a < 12; a++) {
					byte b = FileBytes[312 + x * 12 + a];
					character.Add(b);
				}
				EntryList.Add(character);
				string toAdd = "";
				for (int c = 0; c < 12; c++) {
					toAdd = toAdd + character[c].ToString("X2") + " ";
				}
			}
			//MessageBox.Show("UnlockCharaTotal contains " + EntryCount + " unlock sections.");
		}
		public byte[] ConvertToFile() {
			List<byte> file = new List<byte>();
			byte[] header = new byte[312]
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
				0,
				228,
				0,
				0,
				0,
				3,
				0,
				121,
				0,
				0,
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
				2,
				0,
				0,
				0,
				33,
				0,
				0,
				0,
				4,
				0,
				0,
				0,
				30,
				0,
				0,
				0,
				4,
				0,
				0,
				0,
				48,
				0,
				0,
				0,
				4,
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
				0,
				0,
				98,
				105,
				110,
				95,
				108,
				101,
				47,
				120,
				54,
				52,
				47,
				117,
				110,
				108,
				111,
				99,
				107,
				67,
				104,
				97,
				114,
				97,
				84,
				111,
				116,
				97,
				108,
				46,
				98,
				105,
				110,
				0,
				0,
				117,
				110,
				108,
				111,
				99,
				107,
				67,
				104,
				97,
				114,
				97,
				84,
				111,
				116,
				97,
				108,
				0,
				80,
				97,
				103,
				101,
				48,
				0,
				105,
				110,
				100,
				101,
				120,
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
				3,
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
				3,
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
				121,
				0,
				0,
				0,
				0,
				5,
				252,
				0,
				0,
				0,
				1,
				0,
				121,
				0,
				0,
				0,
				0,
				5,
				248,
				233,
				3,
				0,
				0,
				0,
				0,
				0,
				0,
				8,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			for (int x3 = 0; x3 < header.Length; x3++) {
				file.Add(header[x3]);
			}
			byte[] Size3 = BitConverter.GetBytes(EntryCount * 12 + 20);
			Array.Reverse(Size3);
			file[280] = Size3[0];
			file[281] = Size3[1];
			file[282] = Size3[2];
			file[283] = Size3[3];
			Size3 = BitConverter.GetBytes(EntryCount * 12 + 16);
			Array.Reverse(Size3);
			file[292] = Size3[0];
			file[293] = Size3[1];
			file[294] = Size3[2];
			file[295] = Size3[3];
			Size3 = BitConverter.GetBytes(EntryCount);
			for (int a18 = 0; a18 < 4; a18++) {
				file[300 + a18] = Size3[a18];
			}

			for (int x2 = 0; x2 < EntryCount; x2++) {
				for (int a = 0; a < 12; a++) {
					file.Add(EntryList[x2][a]);
				}
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
			EntryList.Clear();
			EntryCount = 0;
			FilePath = "";
			FileBytes = new byte[0];
			FileOpen = false;
		}
	}
}
