﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT_Task2_Services;
using PT_Task2_ServiceTest.Instrumentation;

namespace PT_Task2_Test
{
    [TestClass]
    public class ServiceTest

    {
        private readonly DataOperator db = new DataOperator("PT_Task2_ServiceTest\\Instrumentation\\DB.mdf");

        [TestInitialize]
        public void BeforeEach()
        {
            TestingDataGenerator.GenerateData(db);
        }

        [TestMethod]
        public void SQLGettersTest()
        {
            Assert.AreEqual("On the Bright Side", db.GetTitleOfEntry(1));
            Assert.AreEqual(3, db.GetCatalogLength());
        }

        [TestMethod]
        public void SQLInsertTest()
        {
            int count = db.GetCatalogLength();
            db.InsertCatalogEntry("Stranger in a Strange Land", "Robert A. Heinlein", true);
            db.SubmitToDatabase();

            Assert.AreEqual(count + 1, db.GetCatalogLength());
            Assert.AreEqual("Robert A. Heinlein", db.GetAuthorOfEntry(count));
        }
        [TestMethod]
        public void SQLDeleteTest()
        {
            int count = db.GetCatalogLength();
            db.DeleteCatalogEntry(db.LookUpEntryIDByTitle("Harry Potter and the Philosopher's Stone"));
            db.SubmitToDatabase();

            Assert.AreEqual(count - 1, db.GetCatalogLength());
        }

        [TestMethod]
        public void SQLUpdateTest()
        {
            int index = db.LookUpEntryIDByTitle("Pride and Prejudice");
            Assert.AreNotEqual("Definitely not me", db.GetAuthorOfEntry(index));

            db.UpdateCatalogEntryWithAuthor(index, "Definitely not me");
            db.SubmitToDatabase();
            Assert.AreEqual("Definitely not me", db.GetAuthorOfEntry(index));
        }

        [TestMethod]
        public void SQLDeleteAndInsertTest()
        {
            int count = db.GetCatalogLength();
            db.DeleteCatalogEntry(db.LookUpEntryIDByTitle("Harry Potter and the Philosopher's Stone"));
            db.InsertCatalogEntry("Harry Potter and the Philosopher's Stone", "J. K. Bowling", false);
            db.SubmitToDatabase();

            Assert.AreEqual(count, db.GetCatalogLength());
        }

        [TestMethod]
        public void SQLInsertAndDeleteTest()
        {
            int count = db.GetCatalogLength();
            db.InsertCatalogEntry("Harry Potter and the Philosopher's Stone", "J. K. Bowling", false);
            db.DeleteCatalogEntry(db.LookUpEntryIDByTitle("Harry Potter and the Philosopher's Stone"));
            db.SubmitToDatabase();

            Assert.AreEqual(count, db.GetCatalogLength());
        }

        [TestMethod]
        public void SQLInsertBookTest()
        {
            int count = db.GetBookCountByEntry(db.LookUpEntryIDByTitle("Pride and Prejudice"));
            int otherCount = db.GetBookCountByEntry(db.LookUpEntryIDByTitle("On the Bright Side"));
            db.InsertBook(db.LookUpEntryIDByTitle("Pride and Prejudice"));
            db.SubmitToDatabase();

            Assert.AreEqual(count + 1, db.GetBookCountByEntry(db.LookUpEntryIDByTitle("Pride and Prejudice")));
            Assert.AreEqual(otherCount, db.GetBookCountByEntry(db.LookUpEntryIDByTitle("On the Bright Side")));
        }

        [TestMethod]
        public void SQLDeleteBookTest()
        {
            int count = db.GetBookCountByEntry(db.LookUpEntryIDByTitle("Pride and Prejudice"));
            int otherCount = db.GetBookCountByEntry(db.LookUpEntryIDByTitle("On the Bright Side"));
            db.DeleteBook(db.LookUpEntryIDByTitle("Pride and Prejudice"));
            db.DeleteBook(db.LookUpEntryIDByTitle("Pride and Prejudice"));
            db.SubmitToDatabase();

            Assert.AreEqual(count - 2, db.GetBookCountByEntry(db.LookUpEntryIDByTitle("Pride and Prejudice")));
            Assert.AreEqual(otherCount, db.GetBookCountByEntry(db.LookUpEntryIDByTitle("On the Bright Side")));
        }

        [TestMethod]
        public void SQLDeleteAndInsertBookTest()
        {
            int count = db.GetBookCountByEntry(db.LookUpEntryIDByTitle("Pride and Prejudice"));
            int otherCount = db.GetBookCountByEntry(db.LookUpEntryIDByTitle("On the Bright Side"));
            db.DeleteBook(db.LookUpEntryIDByTitle("Pride and Prejudice"));
            db.InsertBook(db.LookUpEntryIDByTitle("Pride and Prejudice"));
            db.SubmitToDatabase();

            Assert.AreEqual(count, db.GetBookCountByEntry(db.LookUpEntryIDByTitle("Pride and Prejudice")));
            Assert.AreEqual(otherCount, db.GetBookCountByEntry(db.LookUpEntryIDByTitle("On the Bright Side")));
        }

        [TestMethod]
        public void SQLInsertAndDeleteBookTest()
        {
            int count = db.GetBookCountByEntry(db.LookUpEntryIDByTitle("Pride and Prejudice"));
            int otherCount = db.GetBookCountByEntry(db.LookUpEntryIDByTitle("On the Bright Side"));
            db.InsertBook(db.LookUpEntryIDByTitle("Pride and Prejudice"));
            db.DeleteBook(db.LookUpEntryIDByTitle("Pride and Prejudice"));
            db.DeleteBook(db.LookUpEntryIDByTitle("Pride and Prejudice"));
            db.SubmitToDatabase();

            Assert.AreEqual(count - 1, db.GetBookCountByEntry(db.LookUpEntryIDByTitle("Pride and Prejudice")));
            Assert.AreEqual(otherCount, db.GetBookCountByEntry(db.LookUpEntryIDByTitle("On the Bright Side")));
        }
    }
}
