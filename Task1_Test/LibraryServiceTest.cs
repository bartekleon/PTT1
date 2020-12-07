using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT_Task1.DataLayer;
using PT_Task1.LogicLayer;
using Task1_Test.Instrumentation;

namespace Task1_Test
{
    [TestClass]
    public class LibraryServiceTest
    {
        ILibrary fl;
        LibraryService fls;
        LibraryController flc;

        [TestInitialize]
        public void BeforeEach()
        {
            fl = new Library2();
            fls = new LibraryService(fl);
            flc = new LibraryController(fls);
        }

        [TestMethod]
        public void RentBook_Test()
        {
            fls.Login("White");
            Assert.ThrowsException<LibraryService.NoSuchEntry_Exception>(() => fls.RentBook("On the Bright Side", "Hendrik Groen", true));

            fls.RentBook("On the Bright Side", "Hendrik Groen", false);
            Assert.IsTrue(flc.SearchForABook("On the Bright Side", "Hendrik Groen", false, BookState.BORROWED, "White"));

            Assert.ThrowsException<LibraryService.NonExistingBook_Exception>(() => fls.RentBook("On the Bright Side", "Hendrik Groen", false));
        }

        [TestMethod]
        public void RentBook_UserNotAllowedToBorrow_Test()
        {
            fls.Login("Red");
            Assert.ThrowsException<LibraryService.NotAppropriatePermits_Exception>(() => fls.RentBook("On the Bright Side", "Hendrik Groen", false));
            Assert.AreEqual(fl.GetEvents().Count, 0);
        }

        [TestMethod]
        public void RentBook_UserTriesToExceedTheirLimit_Test()
        {
            fls.Login("White");
            for (int i = 1; i <= 6; i++)
            {
                fls.RentBook("Pride and Prejudice", "Jane Austin", false);
                Assert.AreEqual(fl.GetEvents()[i - 1].type, EventType.RENT_A_BOOK);
                Assert.AreEqual(fl.GetEvents()[i - 1].actor.Username, "White");
                Assert.AreEqual(fl.GetEvents()[i - 1].bookAffected.Title, "Pride and Prejudice");
            }
            Assert.ThrowsException<LibraryService.NotAppropriatePermits_Exception>(() => fls.RentBook("Pride and Prejudice", "Jane Austin", false));
        }

        [TestMethod]
        public void RentBook_NonExistingBook_Test()
        {
            fls.Login("White");
            Assert.ThrowsException<LibraryService.NonExistingBook_Exception>(() => fls.RentBook("Harry Potter and the Philosopher's Stone", "J. K. Rowling", true));
            Assert.AreEqual(fl.GetEvents().Count, 0);
        }

        [TestMethod]
        public void ReturnBook_Test()
        {
            fls.Login("White");
            fls.RentBook("Pride and Prejudice", "Jane Austin", false);
            Assert.IsTrue(flc.SearchForABook("Pride and Prejudice", "Jane Austin", false, BookState.BORROWED, "White"));

            fls.ReturnBook("Pride and Prejudice", "Jane Austin", false);
            Assert.AreEqual(fl.GetEvents()[1].type, EventType.BOOK_RETURN);
            Assert.AreEqual(fl.GetEvents()[1].actor.Username, "White");
            Assert.AreEqual(fl.GetEvents()[1].bookAffected.Title, "Pride and Prejudice");

            Assert.IsFalse(flc.SearchForABook("Pride and Prejudice", "Jane Austin", false, BookState.BORROWED, "White"));
        }

        [TestMethod]
        public void ReturnBook_UserNotOwningTheBook_Test()
        {
            fls.Login("Black");
            Assert.ThrowsException<LibraryService.NonExistingBook_Exception>(() => fls.ReturnBook("Pride and Prejudice", "Jane Austin", false));
            Assert.AreEqual(fl.GetEvents().Count, 0);
        }

        [TestMethod]
        public void ReturnBook_MultipleInstances_Test()
        {
            fls.Login("White");
            fls.RentBook("Pride and Prejudice", "Jane Austin", false);

            fls.Login("Black");
            fls.RentBook("Pride and Prejudice", "Jane Austin", false);

            Assert.IsTrue(flc.SearchForABook("Pride and Prejudice", "Jane Austin", false, BookState.BORROWED, "White"));
            Assert.IsTrue(flc.SearchForABook("Pride and Prejudice", "Jane Austin", false, BookState.BORROWED, "Black"));

            fls.ReturnBook("Pride and Prejudice", "Jane Austin", false);

            Assert.AreEqual(fl.GetEvents()[2].type, EventType.BOOK_RETURN);
            Assert.AreEqual(fl.GetEvents()[2].actor.Username, "Black");
            Assert.AreEqual(fl.GetEvents()[2].bookAffected.Title, "Pride and Prejudice");
            Assert.IsTrue(flc.SearchForABook("Pride and Prejudice", "Jane Austin", false, BookState.BORROWED, "White"));
            Assert.IsFalse(flc.SearchForABook("Pride and Prejudice", "Jane Austin", false, BookState.BORROWED, "Black"));
        }

        [TestMethod]
        public void ReserveBook_Test()
        {
            fls.Login("White");
            fls.ReserveBook("Pride and Prejudice", "Jane Austin", false);
            Assert.AreEqual(fl.GetEvents()[0].type, EventType.RESERVATION);
            Assert.AreEqual(fl.GetEvents()[0].actor.Username, "White");
            Assert.AreEqual(fl.GetEvents()[0].bookAffected.Title, "Pride and Prejudice");
            Assert.IsTrue(flc.SearchForABook("Pride and Prejudice", "Jane Austin", false, BookState.RESERVED, "White"));
        }

        [TestMethod]
        public void ReserveBook_UserNotAllowedToReserve_Test()
        {
            fls.Login("Blue");
            Assert.ThrowsException<LibraryService.NonExistingBook_Exception>(() => fls.ReturnBook("Pride and Prejudice", "Jane Austin", false));
        }

        [TestMethod]
        public void ReserveBook_Queue1_Test()
        {
            fls.Login("White");
            fls.ReserveBook("On the Bright Side", "Hendrik Groen", false);

            fls.Login("Black");
            fls.ReserveBook("On the Bright Side", "Hendrik Groen", false);

            fls.Login("White");
            fls.RentReservedBook("On the Bright Side", "Hendrik Groen", false);
            Assert.AreEqual(fl.GetEvents()[2].type, EventType.RENT_A_BOOK);
            Assert.AreEqual(fl.GetEvents()[2].actor.Username, "White");
            Assert.AreEqual(fl.GetEvents()[2].bookAffected.Title, "On the Bright Side");

            fls.ReturnBook("On the Bright Side", "Hendrik Groen", false);
            Assert.IsTrue(flc.SearchForABook("On the Bright Side", "Hendrik Groen", false, BookState.RESERVED, "Black"));
        }

        [TestMethod]
        public void ReserveBook_Queue2_Test()
        {
            fls.Login("Black");
            fls.ReserveBook("On the Bright Side", "Hendrik Groen", false);

            fls.Login("Blue");
            Assert.ThrowsException<LibraryService.NonExistingBook_Exception>(() => fls.RentBook("On the Bright Side", "Hendrik Groen", false));
            Assert.AreEqual(fl.GetEvents().Count, 1);
        }

        [TestMethod]
        public void ReserveBook_Queue3_Test()
        {
            fls.Login("Black");
            fls.ReserveBook("On the Bright Side", "Hendrik Groen", false);
            fls.RentReservedBook("On the Bright Side", "Hendrik Groen", false);

            fls.Login("White");
            Assert.IsTrue(flc.SearchForABook("On the Bright Side", "Hendrik Groen", false, BookState.BORROWED, "Black"));
            fls.ReserveBook("On the Bright Side", "Hendrik Groen", false);

            fls.Login("Black");
            fls.ReturnBook("On the Bright Side", "Hendrik Groen", false);
            Assert.IsTrue(flc.SearchForABook("On the Bright Side", "Hendrik Groen", false, BookState.RESERVED, "White"));
            Assert.AreEqual(fl.GetEvents()[3].type, EventType.BOOK_RETURN);
            Assert.AreEqual(fl.GetEvents()[3].actor.Username, "Black");
            Assert.AreEqual(fl.GetEvents()[3].bookAffected.Title, "On the Bright Side");
            Assert.AreEqual(fl.GetEvents().Count, 4);
        }

        [TestMethod]
        public void ReserveBook_NonExistingBook_Test()
        {
            fls.Login("White");
            Assert.ThrowsException<LibraryService.NonExistingBook_Exception>(() => fls.ReserveBook("Harry Potter and the Philosopher's Stone", "J. K. Rowling", true));
            Assert.AreEqual(fl.GetEvents().Count, 0);
        }

        [TestMethod]
        public void AddAndRemoveBooks_Test()
        {
            fls.Login("Gold");
            Assert.IsFalse(flc.SearchForABook("Harry Potter and the Philosopher's Stone", "J. K. Rowling", true, BookState.AVAILABLE));

            int temp = fl.CountAllBooks();
            fls.AddBook("Harry Potter and the Philosopher's Stone", "J. K. Rowling", true);
            Assert.AreEqual(fl.GetEvents()[0].type, EventType.ADD_A_BOOK);
            Assert.AreEqual(fl.GetEvents()[0].actor.Username, "Gold");
            Assert.AreEqual(fl.GetEvents()[0].bookAffected.Title, "Harry Potter and the Philosopher's Stone");

            Assert.IsTrue(flc.SearchForABook("Harry Potter and the Philosopher's Stone", "J. K. Rowling", true, BookState.AVAILABLE));
            Assert.AreEqual(fl.CountAllBooks(), temp + 1);

            fls.RemoveBook("On the Bright Side", "Hendrik Groen", false);
            Assert.AreEqual(fl.GetEvents()[1].type, EventType.REMOVE_A_BOOK);
            Assert.AreEqual(fl.GetEvents()[1].actor.Username, "Gold");
            Assert.AreEqual(fl.GetEvents()[1].bookAffected.Title, "On the Bright Side");
            Assert.IsFalse(flc.SearchForABook("On the Bright Side", "Hendrik Groen", false, BookState.AVAILABLE));
        }

        [TestMethod]
        public void AddAndRemoveEntries_Test()
        {
            fls.Login("Gold");
            Assert.IsFalse(fl.CheckIfEntryExists("The Tempest", "William Shakespeare", false));
            int temp = fl.CountAllBooks();

            fls.AddCatalogEntry("The Tempest", "William Shakespeare", false);
            Assert.IsTrue(fl.CheckIfEntryExists("The Tempest", "William Shakespeare", false));

            fls.AddBook("The Tempest", "William Shakespeare", false);
            fls.AddBook("The Tempest", "William Shakespeare", false);

            Assert.AreEqual(fl.CountAllBooks(), temp + 2);

            fls.RemoveCatalogEntry("The Tempest", "William Shakespeare", false);

            Assert.AreEqual(fl.GetEvents()[2].type, EventType.REMOVE_A_BOOK);
            Assert.AreEqual(fl.GetEvents()[2].actor.Username, "Gold");
            Assert.AreEqual(fl.GetEvents()[2].bookAffected.Title, "The Tempest");

            Assert.AreEqual(fl.GetEvents()[3].type, EventType.REMOVE_A_BOOK);
            Assert.AreEqual(fl.GetEvents()[3].actor.Username, "Gold");
            Assert.AreEqual(fl.GetEvents()[3].bookAffected.Title, "The Tempest");

            Assert.AreEqual(fl.CountAllBooks(), temp);
            Assert.IsFalse(fl.CheckIfEntryExists("The Tempest", "William Shakespeare", false));
        }
    }

    [TestClass]
    public class LibraryServiceTest_RandomData
    {
        Library rl;
        LibraryService rls;
        LibraryController rlc;

        [TestInitialize]
        public void BeforeEach()
        {
            rl = new Library();
            RandomDataGenerator.Populate(rl);
            rls = new LibraryService(rl);
            rlc = new LibraryController(rls);
        }

        [TestMethod]
        public void RentBook_Test()
        {
            rls.Login("White");
            Assert.IsFalse(rlc.SearchForABook(BookState.BORROWED));

            for (int i = 0; i < 10; i++)
            {
                if (rlc.CanIBorrow())
                {
                    rls.RentBook();
                    Assert.IsTrue(rlc.SearchForABook(BookState.BORROWED));
                }
                else
                {
                    Assert.ThrowsException<LibraryService.NotAppropriatePermits_Exception>(() => rls.RentBook());
                }
            }
        }

        [TestMethod]
        public void ReturnBook_Test()
        {
            rls.Login("Black");
            Assert.ThrowsException<LibraryService.NonExistingBook_Exception>(() => rls.ReturnBook());

            for (int i = 0; i < 10; i++)
            {
                if (rlc.CanIBorrow())
                {
                    rls.RentBook();
                    rls.ReturnBook();
                    Assert.IsFalse(rlc.SearchForABook(BookState.BORROWED));
                }
            }
        }

        [TestMethod]
        public void ReserveBook_Test()
        {
            rls.Login("Gold");
            Assert.IsFalse(rlc.SearchForABook(BookState.RESERVED));
            for (int i = 0; i < 10; i++)
            {
                if (rlc.CanIReserve())
                {
                    rls.ReserveBook();
                    Assert.IsTrue(rlc.SearchForABook(BookState.RESERVED));
                }
                else
                {
                    Assert.ThrowsException<LibraryService.NotAppropriatePermits_Exception>(() => rls.ReserveBook());
                }

            }
        }

    }
}
