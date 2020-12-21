using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT_Task2_Services;

namespace PT_Task2_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(DataOperations.GetTitle(1), "On the Bright Side");
            Assert.AreEqual(DataOperations.GetCatalogLength(), 3);
        }
    }
}
