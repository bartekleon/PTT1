using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT_Task1.DataLayer;

namespace Task1_Test
{
    [TestClass]
    public class LibraryTest
    {
        Library library;

        [TestInitialize]
        public void BeforeEach()
        {
            library = new Library();
        }

        [TestMethod]
        public void TestAddBook()
        {
            library.AddBook(new CatalogEntry("hello", "world", true));

            Assert.AreEqual(library.bookList.Count, 1);
        }

        [TestMethod]
        public void TestAddEntry()
        {
            library.AddEntry("hello", "world", true);

            Assert.AreEqual(Catalog.entries.Count, 3);
        }

        [TestMethod]
        public void TestRemoveBook()
        {
            CatalogEntry catalogEntry = new CatalogEntry("hello", "world", true);

            library.AddBook(catalogEntry);
            library.RemoveBook(catalogEntry);

            Assert.AreEqual(library.bookList.Count, 0);
        }

        [TestMethod]
        public void TestCheckIfEntryExists()
        {
            library.AddEntry("hello", "world", true);

            Assert.IsTrue(library.CheckIfEntryExists("hello", "world", true));
            Assert.IsFalse(library.CheckIfEntryExists("hello", "world", false));
        }
    }
}
