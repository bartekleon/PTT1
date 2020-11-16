using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT_Task1.DataLayer;
using PT_Task1.LogicLayer;

namespace Task1_Test
{
    [TestClass]
    public class LogicServiceTest
    {
        Library l;
        LibraryService ls;

        [TestInitialize]
        public void BeforeEach()
        {
            l = new Library();
            ls = new LibraryService(l);
        }

        [TestMethod]
        public void RentBook_Test()
        {
            ls.Login("White");
            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.RentBook("On the Bright Side", "Hendrik Groen", true));

            ls.RentBook("On the Bright Side", "Hendrik Groen", false);
            l.SelectBook("On the Bright Side", "Hendrik Groen", false, Book.BookState.BORROWED, "White");

            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.RentBook("On the Bright Side", "Hendrik Groen", false));
        }

        [TestMethod]
        public void RentBook_UserNotAllowedToBorrow_Test()
        {
            ls.Login("Red");
            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.RentBook("On the Bright Side", "Hendrik Groen", false));
        }

        [TestMethod]
        public void RentBook_UserTriesToExceedTheirLimit_Test()
        {
            ls.Login("White");
            for (int i = 1; i <= 6; i++)
            {
                ls.RentBook("Pride and Prejudice", "Jane Austin", false);
            }
            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.RentBook("Pride and Prejudice", "Jane Austin", false));
        }

        [TestMethod]
        public void RentBook_NonExistingBook_Test()
        {
            ls.Login("White");
            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.RentBook("Harry Potter and the Philosopher's Stone", "J. K. Rowling", true));
        }

        [TestMethod]
        public void ReturnBook_Test()
        {
            ls.Login("White");
            ls.RentBook("Pride and Prejudice", "Jane Austin", false);
            l.SelectBook("Pride and Prejudice", "Jane Austin", false, Book.BookState.BORROWED, "White");

            ls.ReturnBook("Pride and Prejudice", "Jane Austin", false);
            Assert.ThrowsException<ILibrary.NoSuchBook_Exception>(()
                => l.SelectBook("Pride and Prejudice", "Jane Austin", false, Book.BookState.BORROWED, "White"));
        }

        [TestMethod]
        public void ReturnBook_UserNotOwningTheBook_Test()
        {
            ls.Login("Black");
            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.ReturnBook("Pride and Prejudice", "Jane Austin", false));
        }

        [TestMethod]
        public void ReturnBook_MultipleInstances_Test()
        {
            ls.Login("White");
            ls.RentBook("Pride and Prejudice", "Jane Austin", false);

            ls.Login("Black");
            ls.RentBook("Pride and Prejudice", "Jane Austin", false);

            l.SelectBook("Pride and Prejudice", "Jane Austin", false, Book.BookState.BORROWED, "White");
            l.SelectBook("Pride and Prejudice", "Jane Austin", false, Book.BookState.BORROWED, "Black");

            ls.ReturnBook("Pride and Prejudice", "Jane Austin", false);
            l.SelectBook("Pride and Prejudice", "Jane Austin", false, Book.BookState.BORROWED, "White");
            Assert.ThrowsException<ILibrary.NoSuchBook_Exception>(() =>
                l.SelectBook("Pride and Prejudice", "Jane Austin", false, Book.BookState.BORROWED, "Black"));
        }

        [TestMethod]
        public void ReserveBook_Test()
        {
            ls.Login("White");
            ls.ReserveBook("Pride and Prejudice", "Jane Austin", false);
            l.SelectBook("Pride and Prejudice", "Jane Austin", false, Book.BookState.RESERVED, "White");
        }

        [TestMethod]
        public void ReserveBook_UserNotAllowedToReserve_Test()
        {
            ls.Login("Blue");
            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.ReturnBook("Pride and Prejudice", "Jane Austin", false));
        }

        [TestMethod]
        public void ReserveBook_Queue1_Test()
        {
            ls.Login("White");
            ls.ReserveBook("On the Bright Side", "Hendrik Groen", false);

            ls.Login("Black");
            ls.ReserveBook("On the Bright Side", "Hendrik Groen", false);

            ls.Login("White");
            ls.RentReservedBook("On the Bright Side", "Hendrik Groen", false);
            ls.ReturnBook("On the Bright Side", "Hendrik Groen", false);
            l.SelectBook("On the Bright Side", "Hendrik Groen", false, Book.BookState.RESERVED, "Black");
        }

        [TestMethod]
        public void ReserveBook_Queue2_Test()
        {
            ls.Login("Black");
            ls.ReserveBook("On the Bright Side", "Hendrik Groen", false);

            ls.Login("Blue");
            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.RentBook("On the Bright Side", "Hendrik Groen", false));
        }

        [TestMethod]
        public void ReserveBook_Queue3_Test()
        {
            ls.Login("Black");
            ls.ReserveBook("On the Bright Side", "Hendrik Groen", false);
            ls.RentReservedBook("On the Bright Side", "Hendrik Groen", false);

            ls.Login("White");
            l.SelectBook("On the Bright Side", "Hendrik Groen", false, Book.BookState.BORROWED, "Black");
            ls.ReserveBook("On the Bright Side", "Hendrik Groen", false);

            ls.Login("Black");
            ls.ReturnBook("On the Bright Side", "Hendrik Groen", false);
            l.SelectBook("On the Bright Side", "Hendrik Groen", false, Book.BookState.RESERVED, "White");
        }

        [TestMethod]
        public void ReserveBook_NonExistingBook_Test()
        {
            ls.Login("White");
            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.ReserveBook("Harry Potter and the Philosopher's Stone", "J. K. Rowling", true));
        }


    }
}
