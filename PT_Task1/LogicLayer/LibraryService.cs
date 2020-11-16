using PT_Task1.DataLayer;
using System;

namespace PT_Task1.LogicLayer
{
    public class LibraryService
    {
        private string LoggedInUser;

        public LibraryService(ILibrary library)
        {
            this.library = library;
        }

        public ILibrary library;

        public void Login(string username)
        {
            try
            {
                library.SelectUser(username);
                LoggedInUser = username;
            }
            catch (ILibrary.NoSuchUser_Exception)
            {
                throw new ServiceException();
            }
        }

        public void RentBook(string title, string author, bool hardback)
        {
            if (!library.CheckIfEntryExists(title, author, hardback)) throw new ServiceException();
            if (!library.CanIBorrow()) throw new ServiceException();
            try
            {
                library.SelectBook(title, author, hardback, Book.BookState.AVAILABLE);
                library.AssignTheBookToTheUser();
                library.AssignTheUserToTheBook();
                library.ChangeTheBookStateTo(Book.BookState.BORROWED);
                library.LogEvent(Event.EventType.RENT_A_BOOK);
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                throw new ServiceException();
            }
            catch (ILibrary.LimitReached_Exception)
            {
                throw new ServiceException();
            }
        }

        public void RentReservedBook(string title, string author, bool hardback)
        {
            if (!library.CheckIfEntryExists(title, author, hardback)) throw new ServiceException();
            try
            {
                library.SelectBook(title, author, hardback, Book.BookState.RESERVED, LoggedInUser);
                library.ChangeTheBookStateTo(Book.BookState.BORROWED);
                library.LogEvent(Event.EventType.RENT_A_BOOK);
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                throw new ServiceException();
            }
            catch (ILibrary.LimitReached_Exception)
            {
                throw new ServiceException();
            }
        }

        public void ReturnBook(string title, string author, bool hardback)
        {
            if (!library.CheckIfEntryExists(title, author, hardback)) throw new ServiceException();
            try
            {
                library.SelectBook(title, author, hardback, Book.BookState.BORROWED, LoggedInUser);
                library.UnassignTheBook();
                library.ChangeTheBookStateTo(Book.BookState.AVAILABLE);
                library.PassTheBookDownTheQueue();
                library.LogEvent(Event.EventType.BOOK_RETURN);
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                throw new ServiceException();
            }
        }

        public void ReserveBook(string title, string author, bool hardback)
        {
            if (!library.CheckIfEntryExists(title, author, hardback)) throw new ServiceException();
            if (!library.CanIReserve()) throw new ServiceException();
            try
            {
                library.SelectBook(title, author, hardback, Book.BookState.AVAILABLE);
                library.ChangeTheBookStateTo(Book.BookState.RESERVED);
                library.AssignTheUserToTheBook();
                library.LogEvent(Event.EventType.RESERVATION);
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                try
                {
                    library.SelectBook(title, author, hardback, Book.BookState.RESERVED);
                    library.AddTheUserToQueue();
                    library.LogEvent(Event.EventType.RESERVATION);
                }
                catch (ILibrary.NoSuchBook_Exception)
                {
                    try
                    {
                        library.SelectBook(title, author, hardback, Book.BookState.BORROWED);
                        library.AddTheUserToQueue();
                        library.LogEvent(Event.EventType.RESERVATION);
                    }
                    catch (ILibrary.NoSuchBook_Exception)
                    {
                        throw new ServiceException();
                    }
                }
            }
        }

        public void AddCatalogEntry(string title, string author, bool hardback)
        {
            if (!library.AmIAdmin()) throw new ServiceException();
            if (!library.CheckIfEntryExists(title, author, hardback)) library.AddEntry(title, author, hardback);
        }

        public void RemoveCatalogEntry(string title, string author, bool hardback)
        {
            if (!library.AmIAdmin()) throw new ServiceException();
            if (library.CheckIfEntryExists(title, author, hardback))
            {
                library.RemoveAllBooks(title, author, hardback);
                library.RemoveEntry(title, author, hardback);
            }
        }

        public void AddBook(string title, string author, bool hardback)
        {
            if (!library.AmIAdmin()) throw new ServiceException();
            AddCatalogEntry(title, author, hardback);
            library.AddBook(title, author, hardback);
            library.SelectBook(title, author, hardback, Book.BookState.AVAILABLE);
            library.LogEvent(Event.EventType.ADD_A_BOOK);
        }

        public void RemoveBook(string title, string author, bool hardback)
        {
            if (!library.AmIAdmin()) throw new ServiceException();
            try
            {
                library.SelectBook(title, author, hardback, Book.BookState.AVAILABLE);
                library.LogEvent(Event.EventType.REMOVE_A_BOOK);
                library.RemoveTheBook();
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                throw new ServiceException();
            }
        }

        public class ServiceException : Exception { };
    }
}
