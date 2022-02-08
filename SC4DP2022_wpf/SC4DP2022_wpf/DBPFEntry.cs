using System;
using System.Collections.Generic;
using System.Text;


//See: https://github.com/memo33/jDBPFX/blob/master/src/jdbpfx/DBPFEntry.java
namespace SC4DP2022_wpf {
	/// <summary>
	/// An abstract form on an entry of a <see cref="DBPFFile"/>, representing an instance of a subfile that may be contained in a DBPF file.
	/// </summary>
	public class DBPFEntry {
		private DBPFTGI _tgi;
		public DBPFTGI TGI {
			get { return _tgi; }
			set {
				if (value == null) {
					throw new Exception("Null TGI");
				} else {
					_tgi = value;
				}

			}
		}

		private uint _offset;
		public uint offset {
			get { return _offset; }
			set { _offset = value; }
		}

		private uint _size;
		public uint size {
			get { return _size; }
			set { _size = value; }
		}

		private uint _index;
		public uint indexPos {
			get { return _index; }
			set { _index = value; }
		}


		// Constructor
		public DBPFEntry(DBPFTGI tgi) {
			_tgi = tgi;
		}

		/// <summary>
		/// Create a new DBPFEntry object.
		/// </summary>
		/// <param name="tgi">TGI object representing the entry.</param>
		/// <param name="offset">Offset (location) of the entry within the DBPF file</param>
		/// <param name="size">Size of data for the entry, in bits.</param> //TODO - what are the units of entry size?
		/// <param name="index">Entry position in the file. 0-n</param>
		public DBPFEntry(DBPFTGI tgi, uint offset, uint size, uint index) {
			_tgi = tgi;
			_offset = offset;
			_size = size;
			_index = index;
		}

		public override string ToString() {
			StringBuilder sb = new StringBuilder(_tgi.ToString());
			sb.AppendLine($"IndexPosition: {_index}, Offset: {_offset}, Size: {_size}, ");
			return sb.ToString();
		}


	}
}
