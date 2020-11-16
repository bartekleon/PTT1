using System;
using PT_Task1.DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task1_Test
{
    [TestClass]
    public class DataLayerTest
    {
        [TestMethod]
        public void TestMethod1() {
            Book yeiryom = new Book(Catalog.entries[1]);
            Assert.AreEqual(yeiryom.description.author, "Hendrik Groen");
            Assert.AreEqual(yeiryom.state, Book.BookState.AVAILABLE);
        }
    }
}
