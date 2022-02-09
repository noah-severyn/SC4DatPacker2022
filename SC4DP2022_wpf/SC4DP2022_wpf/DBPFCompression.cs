using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SC4DP2022_wpf {
	public class DBPFCompression {
		private const uint QFS = 0xFB10;

		


		//https://www.wiki.sc4devotion.com/index.php?title=DBPF_Compression
		/*first 4 bytes : size of following header + compressed data
		 * 5th byte		: offset 0 = compression id = 0x10FB --- offset 2 = uncompressed file size
		 *				  offset 5 start of compressed file data
		 * 
		 * Reading compressed data:
		 *		- read the control character
		 *		- depending on control character, read 0-3 more bytes that may be part of the control character
		 *		- inspect control character to determine *how many* characters should be read and *from where*
		 *			- read 0-n characters from the source and append them to the output (n = *how many* from above)
		 *			- or, copy 0-n characters from somwehere in the output to the end of the output (n = *from where* from above)
		 * 
		 */

		/// <summary>
		/// Check if the data is compressed.
		/// </summary>
		/// <param name="entryData">Data to check</param>
		/// <returns>TRUE if data is compressed; FALSE otherwise</returns>
		public static bool IsCompressed(byte[] entryData) {
			if (entryData.Length > 6) {

				uint signature = BitConverter.ToUInt32(entryData, 4);
				if (signature == DBPFCompression.QFS) {
					//Memo's message: "there is an s3d file in SC1.dat which would otherwise return true on uncompressed data; this workaround is not fail proof"
					//https://github.com/memo33/jDBPFX/blob/fa2535c51de80df48a7f62b79a376e25274998c0/src/jdbpfx/util/DBPFPackager.java#L54
					string fileType = DBPFUtil.CharsFromByteArray(entryData, 0, 4);
					if (fileType.Equals("3DMD")) {
						return false;
					}
					return true;
				}
			}
			return false;
		}


		public static uint GetDecompressedSize(byte[] data) {
			uint? decompressedSize = null;
			if (data.Length >=9) {
				uint signature = BitConverter.ToUInt32(data, 4);
				if (signature == DBPFCompression.QFS) {

				}
			}
			return (uint) decompressedSize;
		}
	}
}
