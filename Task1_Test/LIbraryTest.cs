using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT_Task1.DataLayer;

namespace Task1_Test
{
    [TestClass]
    public class LIbraryTest
    {
        [TestMethod]
        public void TestCheckIfEntryExists()
        {
            Library library = new Library();

            library.AddEntry("hello", "world", true);

            Assert.IsTrue(library.CheckIfEntryExists("hello", "world", true));
            Assert.IsFalse(library.CheckIfEntryExists("hello", "world", false));
        }
    }
}
