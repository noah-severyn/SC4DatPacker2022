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
			Assert.AreEqual("6534284A", DBPFUtil.UIntToHexString(1697917002, 8));
			Assert.AreEqual("4A283465", DBPFUtil.UIntToHexString(1244148837, 8));
			Assert.AreEqual("6534284A", DBPFUtil.UIntToHexString(0x6534284A, 8));
			Assert.AreEqual("4A283465", DBPFUtil.UIntToHexString(0x4A283465, 8));
			Assert.AreEqual("4D2", DBPFUtil.UIntToHexString(1234, 3));
			Assert.AreEqual("000004D2", DBPFUtil.UIntToHexString(1234, 8));
		}


		[TestMethod]
		public void Test_013_DBPFUtil_CharsFromByteArray() {
			byte[] dbpfB = new byte[] { 0x44, 0x42, 0x50, 0x46 };
			byte[] dbpfB1 = new byte[] { 68, 66, 80, 70 };
			Assert.AreEqual("DBPF", DBPFUtil.CharsFromByteArray(dbpfB));
			Assert.AreEqual(DBPFUtil.CharsFromByteArray(dbpfB), DBPFUtil.CharsFromByteArray(dbpfB1));
			Assert.AreEqual("DBPF", DBPFUtil.CharsFromByteArray(dbpfB, 0));
			Assert.AreEqual("DBPF", DBPFUtil.CharsFromByteArray(dbpfB, 0, dbpfB.Length));
			Assert.AreEqual("BPF", DBPFUtil.CharsFromByteArray(dbpfB, 1));
			Assert.AreEqual("DB", DBPFUtil.CharsFromByteArray(dbpfB, 0, 2));
			Assert.AreEqual("P", DBPFUtil.CharsFromByteArray(dbpfB, 2, 1));

			
			string lettersS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
			byte[] lettersB = new byte[] { 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E, 0x6F, 0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7A, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F, 0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A };
			Assert.AreEqual(lettersS, DBPFUtil.CharsFromByteArray(lettersB));

			string numbersS = "01213456789";
			byte[] numbersB = new byte[] { 0x30, 0x31, 0x32, 0x31, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 };
			Assert.AreEqual(numbersS, DBPFUtil.CharsFromByteArray(numbersB));

			string specialS = "~`!@#$%^&*()_+-=[]\\{}|;':\",./<>?";
			byte[] specialB = new byte[] {0x7E, 0x60, 0x21, 0x40, 0x23, 0x24, 0x25, 0x5E, 0x26, 0x2A, 0x28, 0x29, 0x5F, 0x2B, 0x2D, 0x3D, 0x5B, 0x5D, 0x5C, 0x7B, 0x7D, 0x7C, 0x3B, 0x27, 0x3A, 0x22, 0x2C, 0x2E, 0x2F, 0x3C, 0x3E, 0x3F};
			Assert.AreEqual(specialS, DBPFUtil.CharsFromByteArray(specialB));

			Assert.ThrowsException<NullReferenceException>(() => DBPFUtil.CharsFromByteArray(null));
		}

		[TestMethod]
		public void Test_050_DBPFTGI_CreateNew() {

		}

		[TestMethod]
		public void Test_052_DBPFTGI_Equals() {
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
		public void Test_054_DBPFTGI_Matches() {
			DBPFTGI tgi_blank = new DBPFTGI(0, 0, 0);
			DBPFTGI tgi_exemplar = new DBPFTGI(0x6534284a, 0, 0);
			DBPFTGI tgi_exemplarRail = new DBPFTGI(0x6534284a, 0xe8347989, 0);
			DBPFTGI tgi_exemplarRail2 = new DBPFTGI(0x6534284a, 0xe8347989, 0x1ab4e56a);

			Assert.IsTrue(tgi_blank.MatchesKnownTGI(DBPFTGI.BLANKTGI));
			Assert.IsTrue(tgi_blank.MatchesKnownTGI(DBPFTGI.NULLTGI));

			Assert.IsTrue(tgi_exemplar.MatchesKnownTGI(DBPFTGI.EXEMPLAR));
			Assert.IsTrue(tgi_exemplarRail.MatchesKnownTGI(DBPFTGI.EXEMPLAR_RAIL));
			Assert.IsTrue(tgi_exemplarRail2.MatchesKnownTGI(DBPFTGI.EXEMPLAR_RAIL));
			Assert.IsTrue(tgi_exemplarRail2.MatchesKnownTGI(DBPFTGI.EXEMPLAR));
			Assert.IsFalse(tgi_exemplar.MatchesKnownTGI(DBPFTGI.COHORT));
			Assert.IsTrue(tgi_exemplar.MatchesKnownTGI(DBPFTGI.NULLTGI));
			Assert.IsFalse(tgi_exemplar.MatchesKnownTGI(DBPFTGI.PNG));
		}

		[TestMethod]
		public void Test_056a_DBPFTGI_ModifyTGIusingDBPFTGI() {
			DBPFTGI exemplar = new DBPFTGI(0x6534284a, 0, 0);
			DBPFTGI exemplar2 = exemplar.ModifyTGI(DBPFTGI.EXEMPLAR_AVENUE);
			Assert.AreEqual("T:1697917002 0x6534284A, G:3413315500 0xCB730FAC, I:0 0x00000000", exemplar2.ToString());
			Assert.AreEqual(exemplar.ToString(), exemplar.ModifyTGI(DBPFTGI.NULLTGI).ToString());
			DBPFTGI exemplar4 = new DBPFTGI(0, 2, 3);
			Assert.AreEqual("T:3395543715 0xCA63E2A3, G:1247710966 0x4A5E8EF6, I:3 0x00000003", exemplar4.ModifyTGI(DBPFTGI.LUA).ToString());
		}

		[TestMethod]
		public void Test_056b_DBPFTGI_ModifyTGIusingUint() {
			DBPFTGI exemplar = new DBPFTGI(0x6534284a, 0, 1000001);
			Assert.AreEqual("T:1697917002 0x6534284A, G:0 0x00000000, I:100 0x00000064", exemplar.ModifyTGI(null, null, 100).ToString());
			Assert.AreEqual("T:100 0x00000064, G:100 0x00000064, I:100 0x00000064", exemplar.ModifyTGI(100, 100, 100).ToString());
		}



		[TestMethod]
		public void Test_101_DBPFFile_ValidDBPF() {
			//DBPFFile dbpf = new DBPFFile("C:\\Users\\Administrator\\Documents\\SimCity 4\\Plugins\\mntoes\\Bournemouth Housing Pack\\Mntoes-Bournemouth Housing Pack.dat");
			DBPFFile dbpf = new DBPFFile("C:\\Users\\Administrator\\Documents\\SimCity 4\\Plugins\\z_DataView - Parks Aura.dat");
			Assert.AreEqual((uint) 0x44425046, dbpf.header.identifier); //1145196614 dec = 44425046 hex = DBPF ascii
			Assert.AreEqual((uint) DBPFUtil.ReverseBytes(1), dbpf.header.majorVersion); //16777216 dec = 1000000 hex
			Assert.AreEqual((uint) 0, dbpf.header.minorVersion);
			Assert.AreEqual((uint) DBPFUtil.ReverseBytes(7), dbpf.header.indexMajorVersion); //117440512 dec = 7000000 hex)
		}

		[TestMethod]
		public void Test_102_DBPFFile_NotValidDBPF() {
			//These should fail : not valid DBPF file
			Exception ex = Assert.ThrowsException<Exception>(() => new DBPFFile("C:\\Users\\Administrator\\Documents\\SimCity 4\\Plugins\\CAS_AutoHistorical_v0.0.2.dll"));
			Assert.IsTrue(ex.Message.Contains("File is not a DBPF file!"));

			//example: Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => account.Debit(debitAmount));
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
