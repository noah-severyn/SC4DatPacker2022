using System;
using System.Collections.Generic;
using System.Text;


//See: https://github.com/memo33/jDBPFX/blob/master/src/jdbpfx/DBPFEntry.java
namespace SC4DP2022_wpf {
	/// <summary>
	/// An absract form on an entry of a DBPFFile, representing an instance of a subfile that may be contained in a DBPF file
	/// </summary>
	class DBPFEntry {
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
		/// 
		/// </summary>
		/// <param name="tgi"></param>
		/// <param name="offset"></param>
		/// <param name="size"></param>
		/// <param name="index">Entry position in the file. 0-n</param>
		public DBPFEntry(DBPFTGI tgi, uint offset, uint size, uint index) {
			_tgi = tgi;
			_index = index;
		}


	}
}
