using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace SC4DP2022_wpf {
	public class DBPFFile {
		public Header header; //TODO - does this need to be sealed? How to modify Header to allow this?
		public FileInfo file; //probably best to use the FileInfo object to better deal with IO errors
		public OrderedDictionary entryMap; //TODO - implement entry map
		//TODO - implement TGI map


		public class Header {
			private uint _identifier;
			private uint _majorVersion;
			private uint _minorVersion;
			private uint _dateCreated;
			private uint _dateModified;
			private uint _indexMajorVersion;
			private uint _indexEntryCount;
			private uint _indexEntryOffset;
			private uint _indexSize;
			public uint identifier {
				get { return _identifier; }
				set {
					uint identifierDbpf = (uint) 0x44425046; //1145196614 decimal = 44425046 hex = DBPF ASCII
					if (value.CompareTo(identifierDbpf) != 0) {
						throw new Exception("File is not a DBPF file!");
					}
					else {
						_identifier = value;
					}
				}
			}
			public uint majorVersion {
				get { return _majorVersion; }
				set {
					if (value != (uint) 0x1000000) { //16777216 decimal = 1000000 hex
						throw new Exception("Unsupported major.minor version. Only 1.0 is supported for SC4 DBPF files.");
					}
					else {
						_majorVersion = value;
					}
				}
			}
			public uint minorVersion {
				get { return _minorVersion; }
				set {
					if (value != (uint) 0) {
						throw new Exception("Unsupported major.minor version. Only 1.0 is supported for SC4 DBPF files.");
					}
					else {
						_minorVersion = value;
					}
				}
			}
			public uint dateCreated {
				get { return _dateCreated; }
				set { _dateCreated = value; }
			}
			public uint dateModified {
				get { return _dateModified; }
				set { _dateModified = value; }
			}
			public uint indexMajorVersion {
				get { return _indexMajorVersion; }
				set {
					if (value != (uint) 0x7000000) { //117440512 decimal = 7000000 hex
						throw new Exception("Unsupported index version. Only 7 is supported for SC4 DBPF files.");
					}
					else {
						_indexMajorVersion = value;
					}
				}
			}
			public uint indexEntryCount {
				get { return _indexEntryCount; }
				set { _indexEntryCount = value; }
			}
			public uint indexEntryOffset {
				get { return _indexEntryOffset; }
				set { _indexEntryOffset = value; }
			}
			public uint indexSize {
				get { return _indexSize; }
				set { _indexSize = value; }
			}

			//empty constructor to prevent automatic creation of blank constructor and assigning default values to fields
			public Header() { }

			//public override string ToString() {
			//	StringBuilder sb = new StringBuilder();
			//	sb.Append($"Version: {majorVersion}.{minorVersion}; ");
			//	sb.Append($"Created: {dateCreated}; "); // TODO - add functions to output these in a readable format - possibly functions in a util class?
			//	sb.Append($"Modified: {dateModified}; "); // TODO - add functions to output these in a readable format - possibly functions in a util class?
			//	sb.Append($"Index Major Version: {indexMajorVersion}; ");
			//	sb.Append($"Index Entry Count: {indexEntryCount}; ");
			//	sb.Append($"Index Offset Location: {indexEntryOffset}; ");
			//	sb.Append($"Index Size: {indexSize}; ");
			//	return sb.ToString();
			//}
		}

		public override string ToString() { //TODO : Implement this?
			return base.ToString();
		}









		public DBPFFile(string filePath) {
			////read first 96 bytes of file
			//byte[] headerBytes = new byte[96];
			//FileStream fs = new FileStream(filePath, FileMode.Open);
			//BinaryReader reader = new BinaryReader(fs);
			//reader.BaseStream.Read(headerBytes, 0, 96);

			//// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-byte-array-to-an-int
			//if (BitConverter.IsLittleEndian) {
			//	Array.Reverse(headerBytes);
			//}
			//Header header = new Header();
			//this.header.identifier = BitConverter.ToUInt32(headerBytes, 92); //1145196614 int = 44425046 hex = DBPF ASCII
			//this.header.majorVersion = BitConverter.ToUInt32(headerBytes, 88);
			//this.header.minorVersion = BitConverter.ToUInt32(headerBytes, 84);
			//this.header.dateCreated = BitConverter.ToUInt32(headerBytes, 68);
			//this.header.dateModified = BitConverter.ToUInt32(headerBytes, 64);
			//this.header.indexMajorVersion = BitConverter.ToUInt32(headerBytes, 60);
			//this.header.indexEntryCount = BitConverter.ToUInt32(headerBytes, 56);
			//this.header.indexEntryOffset = BitConverter.ToUInt32(headerBytes, 52);
			//this.header.indexSize = BitConverter.ToUInt32(headerBytes, 48);


			this.file = new FileInfo(filePath);
			this.header = new Header();

			// Read Header Info
			FileStream fs = new FileStream(file.FullName, FileMode.Open);
			BinaryReader br = new BinaryReader(fs);
			header.identifier = DBPFUtil.ReverseBytes(br.ReadUInt32());
			header.majorVersion = DBPFUtil.ReverseBytes(br.ReadUInt32());
			header.minorVersion = DBPFUtil.ReverseBytes(br.ReadUInt32());
			br.BaseStream.Seek(12, SeekOrigin.Current); //skip 8 unused bytes
			header.dateCreated = DBPFUtil.ReverseBytes(br.ReadUInt32());
			header.dateModified = DBPFUtil.ReverseBytes(br.ReadUInt32());
			header.indexMajorVersion = DBPFUtil.ReverseBytes(br.ReadUInt32());
			header.indexEntryCount = DBPFUtil.ReverseBytes(br.ReadUInt32());
			header.indexEntryOffset = DBPFUtil.ReverseBytes(br.ReadUInt32());
			header.indexSize = DBPFUtil.ReverseBytes(br.ReadUInt32());

			//Read Index Info
			br.BaseStream.Seek(header.indexEntryOffset, SeekOrigin.Begin);
			for (int idx = 0; idx < header.indexEntryCount; idx++) {
				uint typeID = DBPFUtil.ReverseBytes(br.ReadUInt32()); // & (uint)4294967295; TODO - investigate what this does and why it's required
				uint groupID = DBPFUtil.ReverseBytes(br.ReadUInt32()); // & (uint)4294967295; TODO - investigate what this does and why it's required
				uint instanceID = DBPFUtil.ReverseBytes(br.ReadUInt32()); // & (uint)4294967295; TODO - investigate what this does and why it's required
				uint offset = DBPFUtil.ReverseBytes(br.ReadUInt32()); // & (uint)4294967295; TODO - investigate what this does and why it's required
				uint size = DBPFUtil.ReverseBytes(br.ReadUInt32()); // & (uint)4294967295; TODO - investigate what this does and why it's required
				DBPFTGI tgi = new DBPFTGI(typeID, groupID, instanceID);
				DirectDBPFEntry entry = new DirectDBPFEntry(tgi, offset, size, (uint) idx);
			}

			br.Close();
			fs.Close();
		}

		//private void ReadHeader() {
		//	FileStream fs = new FileStream(file.FullName, FileMode.Open);
		//	BinaryReader br = new BinaryReader(fs);
		//	header.identifier = DBPFUtil.ReverseBytes(br.ReadUInt32());
		//	header.majorVersion = DBPFUtil.ReverseBytes(br.ReadUInt32());
		//	header.minorVersion = DBPFUtil.ReverseBytes(br.ReadUInt32());
		//	br.BaseStream.Seek(12, SeekOrigin.Current); //skip 8 unused bytes
		//	header.dateCreated = DBPFUtil.ReverseBytes(br.ReadUInt32());
		//	header.dateModified = DBPFUtil.ReverseBytes(br.ReadUInt32());
		//	header.indexMajorVersion = DBPFUtil.ReverseBytes(br.ReadUInt32());
		//	header.indexEntryCount = DBPFUtil.ReverseBytes(br.ReadUInt32());
		//	header.indexEntryOffset = DBPFUtil.ReverseBytes(br.ReadUInt32());
		//	header.indexSize = DBPFUtil.ReverseBytes(br.ReadUInt32());
		//	br.Close();
		//	fs.Close();
		//}


		public class DirectDBPFEntry : DBPFEntry {
			private readonly uint offset;
			private readonly uint size;
			private readonly uint index;

			//Create a DBPF entry
			public DirectDBPFEntry(DBPFTGI tgi, uint offset, uint size, uint index) : base(tgi) {
				this.offset = offset;
				this.size = size;
				this.index = index;
			}

			//TODO - method equals is unimplemented in memo's code
			//TODO - method hashCode is unimplemented in memo's code
			public override string ToString() {
				throw new NotImplementedException(); //TODO - implement this
			}
			public DBPFFile GetEncolsingDBPFFile() {
				//return DBPFFile.this; //TODO - huh? this doesnt work
				throw new NotImplementedException();
			}

			public string ToDetailString() {
				StringBuilder sb = new StringBuilder(ToString());
				sb.AppendLine($"Offset: {DBPFUtil.ToHex(offset, 8)} Size: {size}");
				return sb.ToString();
			}
		}
	}
}
