using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SC4DP2022_wpf;

namespace SC4DP2022_wpfTests {
	[TestClass]
	public class DBPFUnitTests {

		[TestMethod]
		public void Test_011_DBPFUtil_ReverseBytes() {
			//Example: 1697917002 (0x 65 34 28 4A) returns 1244148837 (0x 4A 28 34 65)
			Assert.AreEqual((uint) 1244148837, DBPFUtil.ReverseBytes(1697917002));
			Assert.AreEqual((uint) 0x4a283465, DBPFUtil.ReverseBytes(0x6534284a));
			Assert.AreEqual((uint) 0, DBPFUtil.ReverseBytes(0));
		}

		[TestMethod]
		public void Test_012_DBPFUtil_UintToHexString() {
			Assert.AreEqual("6534284A", DBPFUtil.UIntToHexString(1697917002,8));
			Assert.AreEqual("4A283465", DBPFUtil.UIntToHexString(1244148837, 8));
			Assert.AreEqual("6534284A", DBPFUtil.UIntToHexString(0x6534284A, 8));
			Assert.AreEqual("4A283465", DBPFUtil.UIntToHexString(0x4A283465, 8));
			Assert.AreEqual("4D2", DBPFUtil.UIntToHexString(1234, 3));
			Assert.AreEqual("000004D2", DBPFUtil.UIntToHexString(1234, 8));
		}

		[TestMethod]
		public void Test_050_DBPFTGI_Equals() {
			DBPFTGI tgi1 = new DBPFTGI(0, 0, 0);
			DBPFTGI tgi2 = new DBPFTGI(0, 0, 0);
			DBPFTGI tgi3 = new DBPFTGI(0xe86b1eef, 0xe86b1eef, 0x286b1f03);
			DBPFTGI tgi4 = new DBPFTGI(3899334383, 3899334383, 678108931);
			Assert.AreEqual(tgi1, tgi2);
			Assert.IsTrue(tgi1.Equals(tgi2));
			Assert.IsTrue(tgi2.Equals(tgi1));
			Assert.AreEqual(tgi3, tgi4);
			Assert.IsTrue(tgi3.Equals(tgi4));
			Assert.IsTrue(tgi4.Equals(tgi3));

			Assert.AreNotEqual(tgi2, tgi3);
			Assert.IsFalse(tgi1.Equals(tgi3));
			Assert.IsFalse(tgi3.Equals(tgi1));

			Assert.IsFalse(tgi1.Equals("string"));
			DBPFEntry e1 = new DBPFEntry(tgi1, 0, 0, 0);
			Assert.IsFalse(tgi1.Equals(e1));
		}

		[TestMethod]
		public void Test_051_DBPFTGI_Matches() {
			Assert.IsTrue(false);
		}



		[TestMethod]
		public void Test_101_ValidDBPF() {
			//These should pass : valid DBPF file
			//DBPFFile dbpf = new DBPFFile("C:\\Users\\Administrator\\Documents\\SimCity 4\\Plugins\\mntoes\\Bournemouth Housing Pack\\Mntoes-Bournemouth Housing Pack.dat");
			DBPFFile dbpf = new DBPFFile("C:\\Users\\Administrator\\Documents\\SimCity 4\\Plugins\\z_DataView - Parks Aura.dat");
			Assert.AreEqual((uint) 0x44425046, dbpf.header.identifier); //1145196614 dec = 44425046 hex = DBPF ascii
			Assert.AreEqual((uint) DBPFUtil.ReverseBytes(1), dbpf.header.majorVersion); //16777216 dec = 1000000 hex
			Assert.AreEqual((uint) 0, dbpf.header.minorVersion);
			Assert.AreEqual((uint) DBPFUtil.ReverseBytes(7), dbpf.header.indexMajorVersion); //117440512 dec = 7000000 hex)
		}

		[TestMethod]
		[Ignore]
		public void Test_102_NotValidDBPF() {
			//These should fail : not valid DBPF file
			Exception ex = Assert.ThrowsException<Exception>(() => new DBPFFile("C:\\Users\\Administrator\\Documents\\SimCity 4\\Plugins\\CAS_AutoHistorical_v0.0.2.dll"));
			Assert.IsTrue(ex.Message.Contains("File is not a DBPF file!"));

		}


		[TestMethod]
		public void Test_110_ParseHeader() {
			Assert.IsTrue(false);
		}


		[TestMethod]
		public void Test_210_ParseIndex() {
			Assert.IsTrue(false);
		}
	}

	
}
