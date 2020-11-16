using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT_Task1.LogicLayer;
using PT_Task1.DataLayer;

namespace Task1_Test
{
    [TestClass]
    public class LogicLayerTest
    {
        [TestMethod]
        public void RentBookTest()
        {
            Library l = new Library();
            LibraryService ls = new LibraryService(l);
            ls.Login("Myself");

            ls.RentBook("On the Bright Side", "Hendrik Groen", false);
            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.RentBook("On the Bright Side", "Hendrik Groen", true));

            ls.RentBook("On the Bright Side", "Hendrik Groen", false);
            Assert.ThrowsException<LibraryService.ServiceException>(() => ls.RentBook("On the Bright Side", "Hendrik Groen", false));

        }
    }
}
