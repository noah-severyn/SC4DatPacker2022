using System;
using System.Collections.Generic;
using System.Text;


//See: https://github.com/memo33/jDBPFX/blob/master/src/jdbpfx/DBPFEntry.java
namespace SC4DP2022_wpf {
	/// <summary>
	/// An abstract form on an entry of a <see cref="DBPFFile"/>, representing an instance of a subfile that may be contained in a DBPF file
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
		private uint _index;

		public uint IndexPos {
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
		/// <param name="size">Size of data for the entry, in ______</param> //TODO - what are these units?
		/// <param name="index">Entry position in the file. 0-n</param>
		public DBPFEntry(DBPFTGI tgi, uint offset, uint size, uint index) {
			_tgi = tgi;
			_index = index;
		}


	}
}
