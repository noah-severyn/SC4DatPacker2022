using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SC4DP2022_wpf {
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// Anything prefixed with "c" refers to compressed (e.g. cData = compressedData), and a "d" prefix refers to decompressed (e.g. dData = decompressedData) 
	/// </remarks>
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


		public static uint GetDecompressedSize(byte[] cData) {
			uint? decompressedSize = null;
			if (cData.Length >= 9) {
				uint signature = BitConverter.ToUInt32(cData, 4);
				if (signature == DBPFCompression.QFS) {
					//decompressedSize = 
				}
			}
			//return (uint) decompressedSize;
			throw new NotImplementedException();
		}


		/// <summary>
		/// Decompress the provided data. If data is not compressed, the same data will be returned.
		/// </summary>
		/// <param name="data">Compressed</param>
		/// <returns>Decompressed data</returns>
		public static byte[] Decompress(byte[] cData) {
			//TODO - check if data is compressed FIRST
			//cData = compressedData; dData = decompressedData
			byte[] dData = new byte[GetDecompressedSize(cData)];
			int dPos = 0;
			int cPos = 0;
			int ctrlByte1 = 0;
			while (ctrlByte1 < 0xFC && cPos < cData.Length) {
				ctrlByte1 = cData[cPos] & 0xFF; //this is byte0 = the first byte of the control character
				cPos++;

				// Control Characters 0 to 127
				if (ctrlByte1 >= 0x00 && ctrlByte1 <= 0x7F) {
					int ctrlByte2 = cData[cPos] & 0xFF; //byte1
					cPos++;
					int numberPlainText = ctrlByte1 & 0x03; //Number of characters immediately after the control character that should be read and appended to output.
					Array.Copy(cData, cPos, dData, dPos, numberPlainText);

					dPos += numberPlainText;
					cPos += numberPlainText;
					int copyOffset = ((ctrlByte1 & 0x60) << 3) + ctrlByte2 + 1; //Where to start reading characters when copying from somewhere in the already decoded output. This is given as an offset from the current end of the output buffer, i.e.an offset of 0 means that you should copy the last character in the output and append it to the output. And offset of 1 means that you should copy the second - to - last character.
					int numberToCopyFromOffset = ((ctrlByte1 & 0x1C) >> 2) + 3; //Number of chars that should be copied from somewhere in the already decoded output and added to the end of the output.
					Array.Copy(dData, copyOffset, dData, dPos, numberToCopyFromOffset);
				}

				// Control Characters 128 to 191
				else if (ctrlByte1 >= 0x80 && ctrlByte1 <= 0xBF) {
					int ctrlByte2 = cData[cPos] & 0xFF;
					cPos++;
					int ctrlByte3 = cData[cPos] & 0xFF;
					cPos++;

					int numberOfPlainText = (ctrlByte2 >> 6) & 0x03;
					Array.Copy(cData, cPos, dData, dPos, numberOfPlainText);
					dPos += numberOfPlainText;
					cPos += numberOfPlainText;

					int offset = ((ctrlByte2 & 0x3F) << 8) + (ctrlByte3) + 1;
					int numberToCopyFromOffset = (ctrlByte1 & 0x3F) + 4;
					Array.Copy(dData, offset, dData, dPos, numberToCopyFromOffset);
					dPos += numberToCopyFromOffset;
				}

				// Control Characters 192 to 223
				else if (ctrlByte1 >= 0xC0 && ctrlByte1 <= 0xDF) {
					int numberOfPlainText = (ctrlByte1 & 0x03);
					int ctrlByte2 = cData[cPos] & 0xFF;
					cPos++;
					int ctrlByte3 = cData[cPos] & 0xFF;
					cPos++;
					int ctrlByte4 = cData[cPos] & 0xFF;
					cPos++;
					Array.Copy(cData, cPos, dData, dPos, numberOfPlainText);
					dPos += numberOfPlainText;
					cPos += numberOfPlainText;

					int offset = ((ctrlByte1 & 0x10) << 12) + (ctrlByte2 << 8) + (ctrlByte3) + 1;
					int numberToCopyFromOffset = ((ctrlByte1 & 0x0C) << 6) + (ctrlByte4) + 5;
					Array.Copy(dData, offset, dPos, numberToCopyFromOffset);
					dpos += numberToCopyFromOffset;
				}

				// Control Characters 224 to 251
				else if (ctrlByte1 >= 0xE0 && ctrlByte1 <= 0xFB) {
					// 0xE0 - 0xFB
					int numberOfPlainText = ((ctrlByte1 & 0x1F) << 2) + 4;
					Array.Copy(cData, cPos, dData, dPos, numberOfPlainText);
					dPos += numberOfPlainText;
					cPos += numberOfPlainText;
				}

				// Control Characters 252 to 255
				else {
					int numberOfPlainText = (ctrlByte1 & 0x03);
					Array.Copy(cData, cPos, dData, dPos, numberOfPlainText);
					dPos += numberOfPlainText;
					cPos += numberOfPlainText;
				}


			}
			return dData;
		}

		/*Copies data from source to destination array.<br>
     * The copy is byte by byte from srcPos to destPos and given length.
     * If the destination array is not large enough a new array will be
     * created.  Since the new array will be a different object, callers
     * should always update their reference to the original dest array
     * with the returned value.
     */
		//private byte[] arrayCopyReturnNew(byte[] source, int sourcePos, byte[] destination, int destinationPos, int length) {

		//	//Length handling just in case???
		//	if (destination.Length < destinationPos + length) {
		//		byte[] newDestination = new byte[destinationPos + length];
		//		Array.Copy(destination, newDestination, destination.Length);
		//		destination = newDestination;
		//	}


		//}
	}
}
