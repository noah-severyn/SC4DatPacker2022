using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace SC4DP2022_wpf {
	public class DBPFFile {


		public class Header {
			private uint identifier;
			private uint majorVersion;
			private uint minorVersion;
			private uint dateCreated;
			private uint dateModified;
			private uint indexMajorVersion;
			private uint indexEntryCount;
			private uint indexEntryOffset;
			private uint indexSize;

			//empty constructor to prevent automatic creation of blank constructor and assigning default values to fields
			private Header() { }


			public uint GetMajorVersion() {
				return majorVersion;
			}
			public uint GetMinorVersion() {
				return minorVersion;
			}
			public uint GetDateCreated() {
				return dateCreated;
			}
			public uint GetDateModified() {
				return dateModified;
			}
			public uint GetIndexMajorVersion() {
				return indexMajorVersion;
			}
			public uint GetIndexEntryCount() {
				return indexEntryCount;
			}
			public uint GetIndexOffsetLocation() {
				return indexEntryOffset;
			}
			public uint GetIndexSize() {
				return indexSize;
			}

			public override string ToString() {
				StringBuilder sb = new StringBuilder();
				sb.Append($"Version: {majorVersion}.{minorVersion}; ");
				sb.Append($"Created: {dateCreated}; ");
				sb.Append($"Modified: {dateModified}; ");
				sb.Append($"Index Major Version: {indexMajorVersion}; ");
				sb.Append($"Index Entry Count: {indexEntryCount}; ");
				sb.Append($"Index Offset Location: {indexEntryOffset}; ");
				sb.Append($"Index Size: {indexSize}; ");
				return sb.ToString();

			}








		}

		//public uint HeaderIdentifier {
		//	get { return identifier; }
		//	set {
		//		uint identifierDbpf = (uint) 0x44425046; //1145196614 dec = 44425046 hex = DBPF ascii
		//		if (value.CompareTo(identifierDbpf) != 0) {
		//			throw new Exception("File is not a DBPF file!");
		//		}
		//		else {
		//			identifier = value;
		//		}
		//	}
		//}


		//public uint HeaderMajorVersion {
		//	get { return majorVersion; }
		//	set {
		//		if (value != (uint) 0x1000000) { //16777216 dec = 1000000 hex
		//			throw new Exception("Unsupported major.minor version. Only 1.0 is supported for SC4 DBPF files.");
		//		}
		//		else {
		//			majorVersion = value;
		//		}
		//	}
		//}


		//public uint HeaderMinorVersion {
		//	get { return minorVersion; }
		//	set {
		//		if (value != (uint) 0) {
		//			throw new Exception("Unsupported major.minor version. Only 1.0 is supported for SC4 DBPF files.");
		//		}
		//		else {
		//			minorVersion = value;
		//		}
		//		minorVersion = value;
		//	}
		//}


		//public uint HeaderDateCreated {
		//	get { return dateCreated; }
		//	set { dateCreated = value; }
		//}


		//public uint HeaderDateModified {
		//	get { return dateModified; }
		//	set { dateModified = value; }
		//}


		//public uint HeaderIndexMajorVersion {
		//	get { return indexMajorVersion; }
		//	set {
		//		if (value != (uint) 0x7000000) { //117440512 dec = 7000000 hex
		//			throw new Exception("Unsupported major.minor version. Only 1.0 is supported for SC4 DBPF files.");
		//		}
		//		else {
		//			indexMajorVersion = value;
		//		}
		//	}
		//}


		// Default Constructor
		//public DBPFHeader() {
		//identifier = (uint) 44425046; //DBPF in hex
		//majorVersion = (uint) 1;
		//minorVersion = (uint) 0;
		//dateCreated = (uint) DateTimeOffset.Now.ToUnixTimeSeconds();
		//dateModified = (uint) DateTimeOffset.Now.ToUnixTimeSeconds();
		//indexMajorVersion = (uint) 7;
		////indexEntryCount;
		////indexEntryOffset;
		////indexSize;
		//holeEntryCount = (uint) 0;
		//holeOffset = (uint) 0;
		//holeSize = (uint) 0;
		//indexMinorVersion = (uint) 0;
		//}

		// Constructor with supplied values
		//public DBPFHeader(uint[] headerInfo) {
		//	//check if input is the proper length of 96 bytes
		//	if (headerInfo.Length != 18) {
		//		throw new Exception($"Incorrect header size supplied to the DBPFHeader class. Provided {headerInfo.Length}, expected 18.");
		//	}
		//	identifier = headerInfo[0];
		//	majorVersion = headerInfo[1];
		//	minorVersion = headerInfo[2];
		//	dateCreated = headerInfo[6];
		//	dateModified = headerInfo[7];
		//	indexMajorVersion = headerInfo[8];
		//	indexEntryCount = headerInfo[9];
		//	indexEntryOffset = headerInfo[10];
		//	indexSize = headerInfo[11];
		//	holeEntryCount = headerInfo[12];
		//	holeOffset = headerInfo[13];
		//	holeSize = headerInfo[14];
		//	indexMinorVersion = headerInfo[15];
		//}

		public override string ToString() { //TODO : Implement this?
			return base.ToString();
		}
		//}









		public DBPFFile(string filePath) {
			//read first 96 bytes of file
			byte[] headerBytes = new byte[96];
			FileStream fs = new FileStream(filePath, FileMode.Open);
			BinaryReader reader = new BinaryReader(fs);
			reader.BaseStream.Read(headerBytes, 0, 96);

			// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-byte-array-to-an-int
			if (BitConverter.IsLittleEndian) {
				Array.Reverse(headerBytes);
			}
			//DBPFHeader header = new DBPFHeader();
			this.HeaderIdentifier = BitConverter.ToUInt32(headerBytes, 92); //1145196614 int = 44425046 hex = DBPF ascii
			System.Diagnostics.Debug.WriteLine("HeaderIdentifier: " + this.HeaderIdentifier);
			this.HeaderMajorVersion = BitConverter.ToUInt32(headerBytes, 88);
			System.Diagnostics.Debug.WriteLine("HeaderMajorVersion: " + this.HeaderMajorVersion);
			this.HeaderMinorVersion = BitConverter.ToUInt32(headerBytes, 84);
			System.Diagnostics.Debug.WriteLine("HeaderMinorVersion: " + this.HeaderMinorVersion);
			this.HeaderDateCreated = BitConverter.ToUInt32(headerBytes, 68);
			this.HeaderDateModified = BitConverter.ToUInt32(headerBytes, 64);
			this.HeaderIndexMajorVersion = BitConverter.ToUInt32(headerBytes, 60);
			this.HeaderIndexEntryCount = BitConverter.ToUInt32(headerBytes, 56);
			this.HeaderIndexEntryOffset = BitConverter.ToUInt32(headerBytes, 52);
			this.HeaderIndexSize = BitConverter.ToUInt32(headerBytes, 48);
			this.HeaderHoleEntryCount = BitConverter.ToUInt32(headerBytes, 44);
			this.HeaderHoleOffset = BitConverter.ToUInt32(headerBytes, 40);
			this.HeaderHoleSize = BitConverter.ToUInt32(headerBytes, 36);
			this.HeaderIndexMinorVersion = BitConverter.ToUInt32(headerBytes, 32);
		}

	}
}
