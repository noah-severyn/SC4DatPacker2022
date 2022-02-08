using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SC4DP2022_wpf {
	public class DBPFCompression {
		private const uint QFS = 0xFB10;

		private uint _compressedSize;
		public uint compressedSize {
			get { return _compressedSize; }
			//set { _compressedSize = value; }
		}
		
		private uint _decompressedSize;
		public uint decompressedSize {
			get { return _decompressedSize; }
			//set { _decompressedSize = value; }
		}

		private bool isCompressed;
		public bool isCompressed_ {
			get { return isCompressed; }
			//set { isCompressed = value; }
		}

		//https://github.com/memo33/jDBPFX/blob/master/src/jdbpfx/util/DBPFPackager.java#L50
		public static bool isCompressed(byte[] entryData) {
			if (entryData.Length > 6) {

				uint signature = BitConverter.ToUInt32(entryData, 4);
				if (signature == DBPFCompression.QFS) {

					string fileType = 
				}
			}
			return false;
		}

	}
}
