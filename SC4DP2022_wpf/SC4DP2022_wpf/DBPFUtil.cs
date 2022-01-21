using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SC4DP2022_wpf {
	static class DBPFUtil {
		private static readonly string[] sc4Extensions = { "dat", "sc4lot", "sc4desc", "sc4model" };



		/// <summary>
		/// Filters a list of file paths based on SC4 file extensions.
		/// </summary>
		/// <param name="filesToFilter">List of all files to filter through</param>
		/// <returns>Tuple of List <string> (sc4Files,skippedFiles)</returns>
		public static (List<string>, List<string>) FilterFilesByExtension(List<string> filesToFilter) {
			List<string> sc4Files = new List<string>();
			List<string> skippedFiles = new List<string>();

			string extension;
			foreach (string file in filesToFilter) {
				extension = file.Substring(file.LastIndexOf(".") + 1);
				if (sc4Extensions.Any(extension.Contains)) { //https://stackoverflow.com/a/2912483/10802255
					sc4Files.Add(file);
					Trace.WriteLine(file);
				}
				else {
					skippedFiles.Add(file);
				}
			}

			return (sc4Files, skippedFiles);
		}


		/// <summary>
		/// Reverses the byte order for a uint. See:https://stackoverflow.com/a/18145923/10802255
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static uint ReverseBytes(uint value) {
			return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 | (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
		}

		/// <summary>
		/// See: https://github.com/memo33/jDBPFX/blob/master/src/jdbpfx/util/DBPFUtil.java#L144
		/// </summary>
		/// <param name="value"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		//public static string ToHex(uint value, int length) {
		//	return string.Format("%0" + length + "X" + value);
		//}

	}
}
