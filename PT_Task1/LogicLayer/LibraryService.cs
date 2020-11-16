using PT_Task1.DataLayer;
using System;

namespace PT_Task1.LogicLayer
{
    public class LibraryService
    {
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
            }
            catch (ILibrary.NoSuchUser_Exception)
            {
                throw new ServiceException();
            }
        }

        public void RentBook(string title, string author, bool hardback)
        {
            if (!library.CheckIfEntryExists(title, author, hardback)) throw new ServiceException();
            try
            {
                library.SelectBook(title, author, hardback, Book.BookState.AVAILABLE);
                library.AssignTheBookToTheUser();
                library.ChangeActiveBookStateTo(Book.BookState.BORROWED);
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
                library.SelectBook(title, author, hardback, Book.BookState.BORROWED);
                library.UnassignTheBook();
                library.ChangeActiveBookStateTo(Book.BookState.AVAILABLE);
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                throw new ServiceException();
            }
        }

        public class ServiceException : Exception { };
    }
}
