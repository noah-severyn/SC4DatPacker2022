using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace SC4DP2022_wpf {
	internal class DBPFFile {
		

		private class DBPFHeader {
			private uint identifier;
			public uint HeaderIdentifier {
				get { return identifier; }
				set {
					uint identifierDbpf = (uint) 44425046; //DBPF in hex
					if (value.CompareTo(identifierDbpf) != 0) {
						throw new Exception("File is not a DBPF file!");
					}
					else {
						identifier = value;
					}
				}
			}

			private uint majorVersion;
			public uint HeaderMajorVersion {
				get { return majorVersion; }
				set {
					if (value != (uint) 1) {
						throw new Exception("Unsupported major.minor version. Only 1.0 is supported for SC4 DBPF files.");
					}
					else {
						majorVersion = value;
					}
				}
			}

			private uint minorVersion;
			public uint HeaderMinorVersion {
				get { return minorVersion; }
				set {
					if (value != (uint) 0) {
						throw new Exception("Unsupported major.minor version. Only 1.0 is supported for SC4 DBPF files.");
					}
					else {
						minorVersion = value;
					}
					minorVersion = value;
				}
			}

			private uint dateCreated;
			public uint HeaderDateCreated {
				get { return dateCreated; }
				set { dateCreated = value; }
			}

			private uint dateModified;
			public uint HeaderDateModified {
				get { return dateModified; }
				set { dateModified = value; }
			}

			private uint indexMajorVersion;
			public uint HeaderIndexMajorVersion {
				get { return indexMajorVersion; }
				set { indexMajorVersion = value; }
			}

			private uint indexEntryCount;
			public uint HeaderIndexEntryCount {
				get { return indexEntryCount; }
				set { indexEntryCount = value; }
			}

			private uint indexEntryOffset;
			public uint HeaderIndexEntryOffset {
				get { return indexEntryOffset; }
				set { indexEntryOffset = value; }
			}

			private uint indexSize;
			public uint HeaderIndexSize {
				get { return indexSize; }
				set { indexSize = value; }
			}

			private uint holeEntryCount;
			public uint HeaderHoleEntryCount {
				get { return holeEntryCount; }
				set { holeEntryCount = value; }
			}

			private uint holeOffset;
			public uint HeaderHoleOffset {
				get { return holeOffset; }
				set { holeOffset = value; }
			}

			private uint holeSize;
			public uint HeaderHoleSize {
				get { return holeSize; }
				set { holeSize = value; }
			}

			private uint indexMinorVersion;

			public uint HeaderIndexMinorVersion {
				get { return indexMinorVersion; }
				set { indexMinorVersion = value; }
			}


			// Default Constructor
			public DBPFHeader() {
				identifier = (uint) 44425046; //DBPF in hex
				majorVersion = (uint) 1;
				minorVersion = (uint) 0;
				dateCreated = (uint) DateTimeOffset.Now.ToUnixTimeSeconds();
				dateModified = (uint) DateTimeOffset.Now.ToUnixTimeSeconds();
				indexMajorVersion = (uint) 7;
				//indexEntryCount;
				//indexEntryOffset;
				//indexSize;
				holeEntryCount = (uint) 0;
				holeOffset = (uint) 0;
				holeSize = (uint) 0;
				indexMinorVersion = (uint) 0;
			}

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
		}











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
			DBPFHeader header = new DBPFHeader();
			header.HeaderIdentifier = BitConverter.ToUInt32(headerBytes, 0);
			System.Diagnostics.Debug.WriteLine("HeaderIdentifier: " + header.HeaderIdentifier);
			header.HeaderMajorVersion = BitConverter.ToUInt32(headerBytes, 4);
			System.Diagnostics.Debug.WriteLine("HeaderMajorVersion: " + header.HeaderMajorVersion);
			header.HeaderMinorVersion = BitConverter.ToUInt32(headerBytes, 8);
			System.Diagnostics.Debug.WriteLine("HeaderMinorVersion: " + header.HeaderMinorVersion);
		}


		




	}
}
