using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;



//https://github.com/fbstj/dbpf/blob/c/qfs.c
namespace SC4DP2022_wpf {
	/// <summary>
	/// Implementation of the QFS/RefPack/LZ77 compression and decompression format.
	/// </summary>
	/// <see cref="http://wiki.niotso.org/RefPack"/>
	/// <seealso cref="https://www.wiki.sc4devotion.com/index.php?title=DBPF_Compression"/>
	public class DBPFCompression {
		//Anything prefixed with "c" refers to compressed (e.g. cData = compressedData), and a "d" prefix refers to decompressed (e.g. dData = decompressedData) 
		private const ushort QFS = 0xFB10;
		



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

				ushort signature = BitConverter.ToUInt16(entryData,4); //ToUint32(entryData,2) would otherwise return 0xFB10 0000, but we're only interested in 0xFB10
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


		/// <summary>
		/// Returns the length of the data array in bytes. If data is compressed, the uncompressed size is returned. If data is not compressed, the raw size is returned.
		/// </summary>
		/// <param name="cData">Data to check</param>
		/// <returns>Size of data</returns>

		//https://github.com/Killeroo/SC4Parser/blob/master/SC4Parser/Compression/QFS.cs#L42
		public static uint GetDecompressedSize(byte[] cData) {

			if (IsCompressed(cData)) {
				uint compressedSize = BitConverter.ToUInt32(cData, 0); //first 4 bytes is always the size of header + compressed data

				//read 5 byte header
				byte[] header = new byte[5];
				for (int idx = 0; idx < 5; idx++) {
					header[idx] = cData[idx + 4];
				}

				//first two bytes of header should be QFS identifier 
				//TODO - this check is redundant
				uint signature = (uint) (header[0] | (header[1] << 8));
				if (signature != DBPFCompression.QFS) {
					Trace.WriteLine("Not compressed");
				}

				//next 3 bytes are the decompressed size ... byte shift most significant byte to least
				uint decompressedSize = Convert.ToUInt32((header[2] << 16) + (header[3] << 8) + header[4]);
				return decompressedSize;

			} else {
				return (uint) cData.Length;
			}
		}


		/// <summary>
		/// Decompress the provided data. If data is not compressed, the same data will be returned.
		/// </summary>
		/// <param name="data">Compressed</param>
		/// <returns>Decompressed data</returns>
		/// <see cref="https://www.wiki.sc4devotion.com/index.php?title=DBPF_Compression"/>
		/// <see cref="https://github.com/Killeroo/SC4Parser/blob/master/SC4Parser/Compression/QFS.cs"/>
		public static byte[] Decompress(byte[] cData) {
			//If data is not compressed do not run it through the algorithm otherwise it will return junk data
			if (!IsCompressed(cData)) {
				return cData;
			}

			byte[] dData = new byte[GetDecompressedSize(cData)]; //Set destination array of decompressed data
			int dPos = 0;

			byte ctrlByte1 = 0; //The control character determines which type of decompression algorithm needs to be performed
			int cPos = 9; //bytes 0-1 are header identifier, 2-4 are uncompressed size, 5-8 are unused by SC4

			//Keep decoding while cPos is within cData and the ctrlByte is not 0xFC
			while (cPos < cData.Length && ctrlByte1 < 0xFC) {
				ctrlByte1 = cData[cPos]; //this is byte0 = the first byte of the control character
				cPos++;

				// Control Characters 0 to 127 (2 byte length CC)
				if (ctrlByte1 >= 0x00 && ctrlByte1 <= 0x7F) {
					byte ctrlByte2 = cData[cPos]; //byte1 (sets the 2nd byte of the two byte control character)
					cPos++;

					//Copy from the source array to the destination array
					int numberPlainText = ctrlByte1 & 0x03; //Number of characters immediately after the control character that should be read and appended to output.
					LZCompliantCopy(ref cData, cPos, ref dData, dPos, numberPlainText);

					//Copy characters already in the destination array to the current position in the destination array
					cPos += numberPlainText;
					dPos += numberPlainText;
					int offset = ((ctrlByte1 & 0x60) << 3) + ctrlByte2 + 1; //Where to start reading characters when copying from somewhere in the already decoded output. This is given as an offset from the current end of the output buffer, i.e.an offset of 0 means that you should copy the last character in the output and append it to the output. And offset of 1 means that you should copy the second - to - last character.
					int length = ((ctrlByte1 & 0x1C) >> 2) + 3; //Number of chars that should be copied from somewhere in the already decoded output and added to the end of the output.
					LZCompliantCopy(ref dData, dPos - offset, ref dData, dPos, length);
					dPos += length;
				}

				// Control Characters 128 to 191 (3 byte length CC)
				else if (ctrlByte1 >= 0x80 && ctrlByte1 <= 0xBF) {
					byte ctrlByte2 = cData[cPos];
					cPos++;
					byte ctrlByte3 = cData[cPos];
					cPos++;

					//Copy from the source array to the destination array
					int numberOfPlainText = (ctrlByte2 >> 6) & 0x03;
					LZCompliantCopy(ref cData, cPos, ref dData, dPos, numberOfPlainText);
					cPos += numberOfPlainText; 
					dPos += numberOfPlainText;
					
					//Copy characters already in the destination array to the current position in the destination array
					int offset = ((ctrlByte2 & 0x3F) << 8) + (ctrlByte3) + 1;
					int length = (ctrlByte1 & 0x3F) + 4;
					LZCompliantCopy(ref dData, dPos-offset, ref dData, dPos, length);
					dPos += length;
				}

				// Control Characters 192 to 223 (4 byte length CC)
				else if (ctrlByte1 >= 0xC0 && ctrlByte1 <= 0xDF) {
					byte ctrlByte2 = cData[cPos];
					cPos++;
					byte ctrlByte3 = cData[cPos];
					cPos++;
					byte ctrlByte4 = cData[cPos];
					cPos++;

					//Copy from the source array to the destination array
					int numberOfPlainText = (ctrlByte1 & 0x03);
					LZCompliantCopy(ref cData, cPos, ref dData, dPos, numberOfPlainText);
					cPos += numberOfPlainText;
					dPos += numberOfPlainText;

					//Copy characters already in the destination array to the current position in the destination array
					int offset = ((ctrlByte1 & 0x10) << 12) + (ctrlByte2 << 8) + (ctrlByte3) + 1;
					int length = ((ctrlByte1 & 0x0C) << 6) + (ctrlByte4) + 5;
					LZCompliantCopy(ref dData, dPos - offset, ref dData, dPos, length);
					dPos += length;
				}

				// Control Characters 224 to 251 (1 byte length CC)
				else if (ctrlByte1 >= 0xE0 && ctrlByte1 <= 0xFB) {
					int numberOfPlainText = ((ctrlByte1 & 0x1F) << 2) + 4;
					LZCompliantCopy(ref cData, cPos, ref dData, dPos, numberOfPlainText);
					cPos += numberOfPlainText;
					dPos += numberOfPlainText;
				}

				// Control Characters 252 to 255 (1 byte length CC)
				else {
					int numberOfPlainText = (ctrlByte1 & 0x03);
					LZCompliantCopy(ref cData, cPos, ref dData, dPos, numberOfPlainText);
					cPos += numberOfPlainText;
					dPos += numberOfPlainText;
				}
			}
			return dData;
		}


		/// <summary>
		/// Recursive method for LZ compliant copying of data between arrays.
		/// </summary>
		/// <param name="source">Array to copy from</param>
		/// <param name="sourceOffset">Location in array to start copying from</param>
		/// <param name="destination">Array to copy to</param>
		/// <param name="destinationOffset">Position in array to start copying to</param>
		/// <param name="length">Length of data to copy</param>
		/// <remarks>
		/// With QFS (LZ77), a LZ compatible array copy method is required to copy data one byte at a time between arrays. Within the LZ compatible algorithms, it is legal to copy data to the destination array that would overrun the length of the destination array. The solution is to copy one byte at a time.
		/// </remarks>
		private static void LZCompliantCopy(ref byte[] source, int sourceOffset, ref byte[] destination, int destinationOffset, int length) {
			if (length > 0) {
				Array.Copy(source, sourceOffset, destination, destinationOffset, length);

				length -= 1;
				sourceOffset++;
				destinationOffset++;
				LZCompliantCopy(ref source, sourceOffset, ref destination, destinationOffset, length);
				//for (int idx = 0; idx < length; idx++) {
				//	destination[destinationOffset + idx] = source[sourceOffset + idx];
				//}
			}
		}
	}
}
