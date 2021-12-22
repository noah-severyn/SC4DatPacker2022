using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SC4DP2022_wpf;

namespace SC4DP2022_wpfTests {
	[TestClass]
	public class UnitTest1 {
		[TestMethod]
		public void Test_001_ValidDBPF() {
			//These should pass : valid DBPF file
			DBPFFile dbpf = new DBPFFile("C:\\Users\\Administrator\\Documents\\SimCity 4\\Plugins\\mntoes\\Bournemouth Housing Pack\\Mntoes-Bournemouth Housing Pack.dat");
			Assert.AreEqual((uint) 0x44425046, dbpf.HeaderIdentifier); //1145196614 dec = 44425046 hex = DBPF ascii
			Assert.AreEqual((uint) 0x1000000, dbpf.HeaderMajorVersion); //16777216 dec = 1000000 hex
			Assert.AreEqual((uint) 0x0, dbpf.HeaderMinorVersion);
			Assert.AreEqual((uint) 0x7000000, dbpf.HeaderIndexMajorVersion); //117440512 dec = 7000000 hex)
			}

		[TestMethod]
		public void Test_020_NotValidDBPF() {
			//These should fail : not valid DBPF file
			Exception ex = Assert.ThrowsException<Exception>(() => new DBPFFile("C:\\Users\\Administrator\\Documents\\SimCity 4\\Plugins\\CAS_AutoHistorical_v0.0.2.dll"));
			Assert.IsTrue(ex.Message.Contains("File is not a DBPF file!"));
			
			
			
		}
	}
}
