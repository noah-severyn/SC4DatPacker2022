using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SC4DP2022_wpf {
	internal class DBPF {
		private static readonly string[] sc4Extensions = { "dat", "sc4lot", "sc4desc", "sc4model" };

		/// <summary>
		/// Returns a tuple of List <string> (sc4FilesskippedFiles) based on SC4 file extensions
		/// </summary>
		/// <param name="filesToFilter"></param>
		/// <returns></returns>
		public (List<string>, List<string>) FilterFilesByExtension(List<string> filesToFilter) {

			//FilteredFiles filteredList = new FilteredFiles();
			List<string> sc4Files = new List<string>();
			List<string> skippedFiles = new List<string>();

			string extension;
			foreach (string file in filesToFilter) {
				extension = file.Substring(file.LastIndexOf(".") + 1);
				if (sc4Extensions.Any(extension.Contains)) { //https://stackoverflow.com/a/2912483/10802255
					sc4Files.Add(file);
				} else {
					skippedFiles.Add(file);
				}
			}

			return (sc4Files,skippedFiles);
		}
	}
}
