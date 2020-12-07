using PT_Task1.DataLayer;
using System;
using System.Collections.Generic;

namespace Task1_Test.Instrumentation
{
    internal class Library3 : ILibrary
    {
        public readonly List<User> userList = new List<User>();
        public readonly List<Book> bookList = new List<Book>();
        public readonly List<Event> eventHistory = new List<Event>();

        private Book activeBook;
        private User activeUser;

        private static readonly Random random = new Random();
        private static readonly int scale = 4;

        private static string RandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
            var stringChars = new char[random.Next(25)];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new string(stringChars);
            return finalString;
        }

        public Library3()
        {
            Populate();
        }

        public void Populate()
        {
            string[] titles = new string[random.Next(scale) + 1];
            string[] authors = new string[random.Next(scale) + 1];

            string[,] entries = new string[scale, 3];

            for (int i = 0; i < authors.Length; i++)
            {
                authors[i] = RandomString();
            }

            for (int i = 0; i < titles.Length; i++)
            {
                titles[i] = RandomString();
                int whichAuthor = random.Next(authors.Length);
                bool hardback = (random.Next(2) == 0);

                AddEntry(titles[i], authors[whichAuthor], hardback);

                entries[i, 0] = titles[i];
                entries[i, 1] = authors[whichAuthor];
                entries[i, 2] = hardback.ToString();
            }

            for (int i = 0; i < titles.Length; i++)
            {
                int bookCount = random.Next(scale) + 1;
                for (int j = 0; j < bookCount; j++)
                {
                    AddBook(entries[i, 0], entries[i, 1], (entries[i, 2] == "True"));
                }
            }

            AddUser("White", random.Next(2) == 0, random.Next(2) == 0, random.Next(2) == 0);
            AddUser("Red", random.Next(2) == 0, random.Next(2) == 0, random.Next(2) == 0);
            AddUser("Black", random.Next(2) == 0, random.Next(2) == 0, random.Next(2) == 0);
            AddUser("Blue", random.Next(2) == 0, random.Next(2) == 0, random.Next(2) == 0);
            AddUser("Gold", random.Next(2) == 0, random.Next(2) == 0, random.Next(2) == 0);
        }

        public void AddBook(CatalogEntry entry)
        {
        }
        public void AddBook(string title, string author, bool hardback)
        {
            bookList.Add(new Book(FindEntry(title, author, hardback)));
        }
        public void AddEntry(string title, string author, bool hardback)
        {
            Catalog.entries.Add(new CatalogEntry(title, author, hardback));
        }
        public bool CheckIfEntryExists(string title, string author, bool hardback)
        {
            foreach (CatalogEntry entry in Catalog.entries)
            {
                if (entry.Title == title && entry.Author == author && entry.Hardback == hardback) return true;
            }
            return false;
        }
        public void SelectBook(int which)
        {
            if (which < bookList.Count) this.activeBook = bookList[which];
        }
        public void SelectBook(string title, string author, bool hardback)
        {
            foreach (Book book in bookList)
            {
                if (book.Description.Title == title
                    && book.Description.Author == author
                    && book.Description.Hardback == hardback)
                {
                    this.activeBook = book;
                    return;
                }
            }
            throw new ILibrary.NoSuchBook_Exception();
        }
        public void SelectBook(string title, string author, bool hardback, BookState bookState)
        {
            foreach (Book book in bookList)
            {
                if (book.Description.Title == title
                    && book.Description.Author == author
                    && book.Description.Hardback == hardback
                    && book.state == bookState)
                {
                    this.activeBook = book;
                    return;
                }
            }
            throw new ILibrary.NoSuchBook_Exception();
        }
        public void SelectBook(string title, string author, bool hardback, BookState bookState, string ownerUsername)
        {
            foreach (Book book in bookList)
            {
                if (book.Description.Title == title
                    && book.Description.Author == author
                    && book.Description.Hardback == hardback
                    && book.state == bookState
                    && book.CurrentOwner.Username == ownerUsername)
                {
                    this.activeBook = book;
                    return;
                }
            }
            throw new ILibrary.NoSuchBook_Exception();
        }
        public int CountBooks(string title, string author, bool hardback)
        {
            int result = 0;
            foreach (Book book in bookList)
            {
                if (book.Description.Title == title
                    && book.Description.Author == author
                    && book.Description.Hardback == hardback) result++;
            }
            return result;
        }
        public void SelectUser(string username)
        {
            foreach (User user in userList)
            {
                if (user.Username == username)
                {
                    this.activeUser = user;
                    return;
                }
            }
            throw new ILibrary.NoSuchUser_Exception();
        }
        public void RemoveTheBook()
        {
            foreach (Book book in bookList)
            {
                if (book == this.activeBook)
                {
                    bookList.Remove(book);
                    return;
                }
            }
        }
        public void RemoveAllBooks(string title, string author, bool hardback)
        {
            bookList.RemoveAll(entry => entry.Description.Equals(new CatalogEntry(title, author, hardback)));
        }
        public void RemoveEntry(int which)
        {
            Catalog.entries.RemoveAt(which);
        }
        public void RemoveEntry(string title, string author, bool hardback)
        {
            foreach (CatalogEntry entry in Catalog.entries)
            {
                if (entry.Title == title && entry.Author == author && entry.Hardback == hardback)
                {
                    Catalog.entries.Remove(entry);
                    break;
                }
            }
        }
        public void ChangeTheBookStateTo(BookState bookState)
        {
            this.activeBook.state = bookState;
        }
        public void AssignTheBookToTheUser()
        {
            this.activeUser.borrowedBooks.Add(activeBook);
        }
        public void AssignTheUserToTheBook()
        {
            this.activeBook.CurrentOwner = this.activeUser;
        }
        public void UnassignTheBook()
        {
            if (this.activeBook.CurrentOwner != this.activeUser) throw new ILibrary.NotYourBook_Exception();
            this.activeUser.borrowedBooks.Remove(activeBook);
            this.activeBook.CurrentOwner = null;
        }
        public void AddTheUserToQueue()
        {
            this.activeBook.reservationQueue.Enqueue(activeUser);
        }
        public void PassTheBookDownTheQueue()
        {
            if (this.activeBook.reservationQueue.Count > 0 && this.activeBook.state == BookState.AVAILABLE)
            {
                this.activeBook.CurrentOwner = this.activeBook.reservationQueue.Dequeue();
                this.activeBook.state = BookState.RESERVED;
            }
        }
        public bool CanIBorrow()
        {
            return (this.activeUser.BorrowLimit > this.activeUser.borrowedBooks.Count);
        }
        public bool CanIReserve()
        {
            return (this.activeUser.ReserveLimit > this.activeUser.reservedBooks.Count);
        }
        public bool AmIAdmin()
        {
            return true;
        }
        private CatalogEntry FindEntry(string title, string author, bool hardback)
        {
            foreach (CatalogEntry entry in Catalog.entries)
            {
                if (entry.Title == title
                    && entry.Author == author
                    && entry.Hardback == hardback) return entry;
            }
            throw new ILibrary.NoSuchEntry_Exception();
        }
        public void LogEvent(EventType type)
        {
        }
        public void AddUser(string username, bool canBorrow, bool canReserve)
        {
            this.userList.Add(new User(username, canBorrow, canReserve));
        }
        public void AddUser(string username, bool canBorrow, bool canReserve, bool isAdmin)
        {
            this.userList.Add(new User(username, canBorrow, canReserve, isAdmin));
        }
        public string GetSelectedTitle()
        {
            return this.activeBook.Description.Title;
        }
        public string GetSelectedAuthor()
        {
            return this.activeBook.Description.Author;
        }
        public bool GetSelectedHardback()
        {
            return this.activeBook.Description.Hardback;
        }
        public int CountAllBooks()
        {
            return this.bookList.Count;
        }
        public int CountAllUsers()
        {
            return this.userList.Count;
        }
        public int CountAllEntries()
        {
            return 12;
        }
        List<Event> ILibrary.GetEvents()
        {
            return eventHistory;
        }
    }
}
