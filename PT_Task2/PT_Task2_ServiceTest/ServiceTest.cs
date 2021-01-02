using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT_Task2_Services;

namespace PT_Task2_Test
{
    [TestClass]
    public class ServiceTest

    {
        [TestInitialize]
        public void BeforeEach()
        {
            System.Console.WriteLine("BUM!");
        }

        [TestMethod]
        public void SQLGettersTest()
        {
            Assert.AreEqual(DataOperations.GetTitleOfEntry(1), "On the Bright Side");
            Assert.AreEqual(DataOperations.GetCatalogLength(), 3);
        }

        [TestMethod]
        public void SQLInsertTest()
        {
            int count = DataOperations.GetCatalogLength();
            DataOperations.InsertCatalogEntry("Stranger in a Strange Land", "Robert A. Heinlein", true);
            Assert.AreEqual(count + 1, DataOperations.GetCatalogLength());
        }

        [TestMethod]
        public void SQLDeleteTest()
        {
            int count = DataOperations.GetCatalogLength();
            DataOperations.DeleteCatalogEntry(DataOperations.LookUpEntryIDByTitle("Stranger in a Strange Land"));
            Assert.AreEqual(count - 1, DataOperations.GetCatalogLength());
            //while (DataOperations.GetCatalogLength() != 3) DataOperations.DeleteCatalogEntry();
        }
    }
}
