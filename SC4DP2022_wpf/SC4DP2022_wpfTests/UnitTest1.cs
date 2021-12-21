using Microsoft.VisualStudio.TestTools.UnitTesting;
using SC4DP2022_wpf;

namespace SC4DP2022_wpfTests {
	[TestClass]
	public class UnitTest1 {
		[TestMethod]
		public void TestMethod1() {
			DBPFFile dbpf = new DBPFFile("C:\\Users\\Administrator\\Documents\\SimCity 4\\Plugins\\CAS_AutoHistorical_v0.0.2.dat");

			int result = dbpf.AddTwo(9, 5);
			Assert.AreEqual(14, result,"9+5 should equal 14");
		}
	}
}
