using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace SC4DP2022_wpf {
	internal class DBPF {
		private static readonly string[] sc4Extensions = { "dat", "sc4lot", "sc4desc", "sc4model" };

		/// <summary>
		/// Filters a list of file paths based on SC4 file extensions.
		/// </summary>
		/// <param name="filesToFilter">List of all files to filter through</param>
		/// <returns>Tuple of List <string> (sc4Files,skippedFiles)</returns>
		public (List<string>, List<string>) FilterFilesByExtension(List<string> filesToFilter) {
			List<string> sc4Files = new List<string>();
			List<string> skippedFiles = new List<string>();

			string extension;
			foreach (string file in filesToFilter) {
				extension = file.Substring(file.LastIndexOf(".") + 1);
				if (sc4Extensions.Any(extension.Contains)) { //https://stackoverflow.com/a/2912483/10802255
					sc4Files.Add(file);
					Trace.WriteLine(file);
					Trace.WriteLine("   " + isDBPF(file));
				} else {
					skippedFiles.Add(file);
				}
			}

			return (sc4Files,skippedFiles);
		}

		//https://www.wiki.sc4devotion.com/index.php?title=DBPF
		public bool isDBPF (string filePath) {
			byte[] identifierFile = new byte[4];
			byte[] identifierDbpf = new byte[] { 0x44, 0x42, 0x50, 0x46 };
			FileStream fs = new FileStream(filePath, FileMode.Open);
			BinaryReader reader = new BinaryReader(fs);
			reader.BaseStream.Read(identifierFile, 0, 4);
			return identifierFile.Equals(identifierDbpf); //this still always returns false. try to figure out why
		}
	}
}
