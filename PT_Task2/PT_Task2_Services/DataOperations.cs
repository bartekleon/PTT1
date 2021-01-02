using PT_Task2_Data;
using System.Collections.Generic;
using System.Linq;

namespace PT_Task2_Services
{
    public class DataOperations
    {

        protected static DB_LinkDataContext context = new DB_LinkDataContext();

        public static string GetTitleOfEntry(int index)
        {
            IEnumerable<string> titleFound = from entry in context.Catalog
                                             where entry.entryID == index
                                             select entry.title;
            return titleFound.ToArray()[0];
        }

        public static string GetAuthorOfEntry(int index)
        {
            IEnumerable<string> authorFound = from entry in context.Catalog
                                              where entry.entryID == index
                                              select entry.author;
            return authorFound.ToArray()[0];
        }

        public static int LookUpEntryIDByAuthor(string author, int which = 0)
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

        public static int LookUpEntryIDByTitle(string title, int which = 0)
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

        public static int LookUpEntryIDByBoth(string author, string title, int which = 0)
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

        public static int GetCatalogLength()
        {
            return context.Catalog.Count();
        }

        public static int GetBookCount()
        {
            return context.Books.Count();
        }

        public static int GetBookCountByEntry(int entryID)
        {
            IEnumerable<Books> booksFound = from book in context.Books
                                            where book.entryID == entryID
                                            select book;
            return booksFound.Count();
        }

        public static void InsertCatalogEntry(string title, string author, bool hardback)
        {
            if (context.Catalog.Any(entry => entry.title == title && entry.author == author && entry.hardback == hardback))
            {
                return;
            }
            else
            {
                Catalog newEntry = new Catalog
                {
                    entryID = GetCatalogLength(),
                    title = title,
                    author = author,
                    hardback = hardback
                };

                context.Catalog.InsertOnSubmit(newEntry);
            }
            context.SubmitChanges();
        }

        public static void InsertBook(int entryID)
        {
            if (context.Catalog.Any(entry => entry.entryID == entryID))
            {
                Books newBook = new Books
                {
                    bookID = GetBookCount(),
                    entryID = entryID,
                    bookState = "available"
                };

                context.Books.InsertOnSubmit(newBook);
            }
            context.SubmitChanges();
        }

        public static void DeleteCatalogEntry(int entryID)
        {
            Catalog entryToDelete = context.Catalog.Single(entry => entry.entryID == entryID);
            context.Catalog.DeleteOnSubmit(entryToDelete);
            context.SubmitChanges();
        }

        public static void DeleteBook(int entryID)
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
            context.SubmitChanges();
        }

    }
}
