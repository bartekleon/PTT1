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

            library.AddEntry("Harry Potter and the Philosopher's Stone", "J. K. Rowling", true);
            library.AddEntry("On the Bright Side", "Hendrik Groen", false);
            library.AddEntry("Pride and Prejudice", "Jane Austin", false);

            library.AddBook(Catalog.entries[1]);

            library.AddBook(Catalog.entries[2]);
            library.AddBook(Catalog.entries[2]);
            library.AddBook(Catalog.entries[2]);
            library.AddBook(Catalog.entries[2]);
            library.AddBook(Catalog.entries[2]);
            library.AddBook(Catalog.entries[2]);
            library.AddBook(Catalog.entries[2]);

            library.userList.Add(new User("White", true, true));
            library.userList.Add(new User("Red", false, false));
            library.userList.Add(new User("Black", true, true));
            library.userList.Add(new User("Blue", true, false));
            library.userList.Add(new User("Gold", true, true, true));
        }

        [TestMethod]
        public void TestAddBook()
        {
            int temp = library.bookList.Count;
            library.AddBook(new CatalogEntry("hello", "world", true));
            Assert.AreEqual(library.bookList.Count, temp + 1);
        }

        [TestMethod]
        public void TestAddEntry()
        {
            int temp = Catalog.entries.Count;
            library.AddEntry("hello", "world", true);
            Assert.AreEqual(Catalog.entries.Count, temp + 1);
        }

        [TestMethod]
        public void TestRemoveBook()
        {
            CatalogEntry catalogEntry = new CatalogEntry("hello", "world", true);

            library.AddBook(catalogEntry);
            int temp = library.bookList.Count;

            library.SelectBook("hello", "world", true, BookState.AVAILABLE);
            library.RemoveTheBook();

            Assert.AreEqual(library.bookList.Count, temp - 1);
        }

        [TestMethod]
        public void TestRemoveAllBooks()
        {
            CatalogEntry catalogEntry = new CatalogEntry("hello", "world", true);

            library.AddBook(catalogEntry);
            library.AddBook(catalogEntry);
            int temp = library.bookList.Count;

            library.SelectBook("hello", "world", true, BookState.AVAILABLE);
            library.RemoveAllBooks("hello", "world", true);

            Assert.AreEqual(library.bookList.Count, temp - 2);
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
