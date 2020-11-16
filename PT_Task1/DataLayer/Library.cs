using System.Collections.Generic;

namespace PT_Task1.DataLayer
{
    public class Library : ILibrary
    {
        public readonly List<User> userList = new List<User>();
        public readonly List<Book> bookList = new List<Book>();
        public readonly List<Event> eventHistory = new List<Event>();

        private Book activeBook;
        private User activeUser;

        public Library()
        {
            this.AddEntry("Harry Potter and the Philosopher's Stone", "J. K. Rowling", true);
            this.AddEntry("On the Bright Side", "Hendrik Groen", false);
            this.AddEntry("Pride and Prejudice", "Jane Austin", false);

            this.AddBook(Catalog.entries[1]);

            this.AddBook(Catalog.entries[2]);
            this.AddBook(Catalog.entries[2]);
            this.AddBook(Catalog.entries[2]);
            this.AddBook(Catalog.entries[2]);
            this.AddBook(Catalog.entries[2]);
            this.AddBook(Catalog.entries[2]);
            this.AddBook(Catalog.entries[2]);

            this.userList.Add(new User("White", true, true));
            this.userList.Add(new User("Red", false, false));
            this.userList.Add(new User("Black", true, true));
            this.userList.Add(new User("Blue", true, false));
            this.userList.Add(new User("Gold", true, true, true));
        }

        public void AddBook(CatalogEntry entry)
        {
            bookList.Add(new Book(entry));
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

        public void SelectBook(string title, string author, bool hardback, Book.BookState bookState)
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

        public void SelectBook(string title, string author, bool hardback, Book.BookState bookState, string ownerUsername)
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
                    break;
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

        public void ChangeTheBookStateTo(Book.BookState bookState)
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
            if (this.activeBook.reservationQueue.Count > 0 && this.activeBook.state == Book.BookState.AVAILABLE)
            {
                this.activeBook.CurrentOwner = this.activeBook.reservationQueue.Dequeue();
                this.activeBook.state = Book.BookState.RESERVED;

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
            return (this.activeUser.isAdmin);
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

        public void LogEvent(Event.EventType type)
        {
            eventHistory.Add(new Event(activeBook.Description, activeUser, type));
        }
    }
}
