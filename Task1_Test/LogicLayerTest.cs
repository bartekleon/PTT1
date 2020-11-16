using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT_Task1.DataLayer;
using PT_Task1.LogicLayer;

namespace Task1_Test
{
    [TestClass]
    public class LogicLayerTest
    {
        Library l;
        LibraryService ls;

        [TestInitialize]
        public void BeforeEach()
        {
            l = new Library();
            ls = new LibraryService(l);
            ls.Login("Myself");
        }

        [TestMethod]
        public void RentBook_Test()
        {
            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.RentBook("On the Bright Side", "Hendrik Groen", true));

            ls.RentBook("On the Bright Side", "Hendrik Groen", false);
            l.SelectBook("On the Bright Side", "Hendrik Groen", false, Book.BookState.BORROWED, "Myself");

            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.RentBook("On the Bright Side", "Hendrik Groen", false));
        }

        [TestMethod]
        public void RentBookEdgeCases1_Test()
        {
            ls.Login("Other");
            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.RentBook("On the Bright Side", "Hendrik Groen", false));
        }

        [TestMethod]
        public void RentBookEdgeCases2_Test()
        {
            for (int i = 1; i <= 6; i++)
            {
                ls.RentBook("Pride and Prejudice", "Jane Austin", false);
            }
            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.RentBook("Pride and Prejudice", "Jane Austin", false));
        }

        [TestMethod]
        public void ReturnBook_Test()
        {
            ls.RentBook("Pride and Prejudice", "Jane Austin", false);
            l.SelectBook("Pride and Prejudice", "Jane Austin", false, Book.BookState.BORROWED, "Myself");

            ls.ReturnBook("Pride and Prejudice", "Jane Austin", false);
            Assert.ThrowsException<ILibrary.NoSuchBook_Exception>(()
                => l.SelectBook("Pride and Prejudice", "Jane Austin", false, Book.BookState.BORROWED, "Myself"));
        }

        [TestMethod]
        public void ReturnBook_EdgeCases1_Test()
        {
            ls.Login("Other");

            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.ReturnBook("Pride and Prejudice", "Jane Austin", false));
        }
    }
}
