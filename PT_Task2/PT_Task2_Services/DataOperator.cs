using PT_Task2_Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PT_Task2_Services
{
    public class DataOperator
    {

        protected DB_LinkDataContext context;

        public DataOperator(string pathToDB = null)
        {
            string fullPath = System.IO.Directory.GetCurrentDirectory();
            int carefa = fullPath.IndexOf("\\PT_Task2\\");
            fullPath = fullPath.Substring(0, carefa + 10);

            if (pathToDB == null)
            {
                fullPath += "PT_Task2_Data\\DB.mdf";
            }
            else
            {
                fullPath += pathToDB;
            }
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;";
            connectionString += "AttachDbFilename=";
            connectionString += fullPath;
            connectionString += ";Integrated Security=True;Connect Timeout=30";

            this.context = new DB_LinkDataContext(connectionString);
        }

        public bool DoesEntryExists(int index)
        {
            return context.Catalog.Any(entry => entry.entryID == index);
        }

        public string GetTitleOfEntry(int index)
        {
            IEnumerable<string> titleFound = from entry in context.Catalog
                                             where entry.entryID == index
                                             select entry.title;
            return titleFound.ToArray()[0];
        }

        public string GetAuthorOfEntry(int index)
        {
            IEnumerable<string> authorFound = from entry in context.Catalog
                                              where entry.entryID == index
                                              select entry.author;
            return authorFound.ToArray()[0];
        }

        public int LookUpEntryIDByAuthor(string author, int which = 0)
        {
            IEnumerable<int> entriesFound = from entry in context.Catalog
                                            where entry.author == author
                                            select entry.entryID;
            if (entriesFound.Count() >= which)
            {
                return entriesFound.ToArray()[which];
            }
            else return -1;
        }

        public int LookUpEntryIDByTitle(string title, int which = 0)
        {
            IEnumerable<int> entriesFound = from entry in context.Catalog
                                            where entry.title == title
                                            select entry.entryID;
            if (entriesFound.Count() >= which)
            {
                return entriesFound.ToArray()[which];
            }
            else return -1;
        }

        public int LookUpEntryIDByBoth(string author, string title, int which = 0)
        {
            IEnumerable<int> entriesFound = from entry in context.Catalog
                                            where entry.author == author && entry.title == title
                                            select entry.entryID;
            if (entriesFound.Count() >= which)
            {
                return entriesFound.ToArray()[which];
            }
            else return -1;
        }

        public int LookUpBookIDByEntry(int entryID)
        {
            IEnumerable<int> bookIDsFound = from book in context.Books
                                            where book.entryID == entryID
                                            select book.bookID;
            if (bookIDsFound.Count() != 0)
            {
                return bookIDsFound.ToArray()[0];
            }
            else return -1;
        }

        public int LookUpBookIDByBoth(int entryID, string state)
        {
            IEnumerable<int> bookIDsFound = from book in context.Books
                                            where book.entryID == entryID && book.bookState == state
                                            select book.bookID;
            if (bookIDsFound.Count() != 0)
            {
                return bookIDsFound.ToArray()[0];
            }
            else return -1;
        }

        public int GetCatalogLength()
        {
            return context.Catalog.Count();
        }

        public int GetBookCount()
        {
            return context.Books.Count();
        }

        public int GetBookCountByEntry(int entryID)
        {
            IEnumerable<Books> booksFound = from book in context.Books
                                            where book.entryID == entryID
                                            select book;
            return booksFound.Count();
        }

        public void InsertCatalogEntry(string title, string author, bool hardback)
        {
            if (context.Catalog.Any(entry => entry.title == title && entry.author == author && entry.hardback == hardback))
            {
                return;
            }
            else
            {
                IEnumerable<Catalog> inserts = context.GetChangeSet().Inserts.OfType<Catalog>();
                int maxInsertID;
                try
                {
                    maxInsertID = inserts.Last().entryID;
                }
                catch
                {
                    maxInsertID = -1;
                }
                Catalog newEntry = new Catalog
                {
                    entryID = Math.Max(GetCatalogLength(), maxInsertID + 1),
                    title = title,
                    author = author,
                    hardback = hardback
                };

                context.Catalog.InsertOnSubmit(newEntry);
            }
        }

        public void InsertBook(int entryID)
        {
            if (context.Catalog.Any(entry => entry.entryID == entryID))
            {
                IEnumerable<Books> inserts = context.GetChangeSet().Inserts.OfType<Books>();
                int maxInsertID;
                try
                {
                    maxInsertID = inserts.Last().bookID;
                }
                catch
                {
                    maxInsertID = -1;
                }
                Books newBook = new Books
                {
                    bookID = Math.Max(GetBookCount(), maxInsertID + 1),
                    entryID = entryID,
                    bookState = "available"
                };

                context.Books.InsertOnSubmit(newBook);
            }
        }

        public void UpdateCatalogEntryWithTitle(int entryID, string newTitle)
        {
            Catalog entryToUpdate = context.Catalog.Single(entry => entry.entryID == entryID);
            entryToUpdate.title = newTitle;
        }

        public void UpdateCatalogEntryWithAuthor(int entryID, string newAuthor)
        {
            Catalog entryToUpdate = context.Catalog.Single(entry => entry.entryID == entryID);
            entryToUpdate.author = newAuthor;
        }

        public void UpdateCatalogEntryWithHardback(int entryID, bool newHardback)
        {
            Catalog entryToUpdate = context.Catalog.Single(entry => entry.entryID == entryID);
            entryToUpdate.hardback = newHardback;
        }

        public void UpdateBook(int bookID, string newState)
        {
            Books bookToUpdate = context.Books.Single(book => book.bookID == bookID);
            bookToUpdate.bookState = newState;
        }

        public void DeleteCatalogEntry(int entryID)
        {
            Catalog entryToDelete = context.Catalog.Single(entry => entry.entryID == entryID);
            int count = GetCatalogLength();
            context.Catalog.DeleteOnSubmit(entryToDelete);
        }

        public void DeleteBook(int entryID)
        {
            IEnumerable<Books> booksToDelete = from book in context.Books
                                               where book.entryID == entryID && book.bookState == "available"
                                               select book;
            if (booksToDelete.Count() == 0)
            {
                booksToDelete = from book in context.Books
                                where book.entryID == entryID
                                select book;
            }
            context.Books.DeleteOnSubmit(booksToDelete.First());
        }

        public void TruncateBooks()
        {
            context.Books.DeleteAllOnSubmit(context.Books);

            context.SubmitChanges();
        }

        public void TruncateBoth()
        {
            TruncateBooks();
            context.Catalog.DeleteAllOnSubmit(context.Catalog);

            context.SubmitChanges();
        }

        public void SubmitToDatabase()
        {
            context.SubmitChanges();
        }
    }
}
