using System;
using PT_Task1.DataLayer;

namespace PT_Task1.LogicLayer
{
    class LibraryService
    {
        public LibraryService(ILibrary library) {
            this.library = library;
        }

        public ILibrary library;

        public void Login(string username) {
            
        }

        public void RentBook(string title, string author, bool hardback) {
            if (!library.CheckIfEntryExists(title, author, hardback)) throw new NoSuchEntry_Exception();
            try
            {
                library.SelectBook(title, author, hardback, Book.BookState.AVAILABLE);
                library.ChangeActiveBookStateTo(Book.BookState.BORROWED);
            }
            catch (ILibrary.NoSuchBook_Exception) {
                throw new NoSuchBook_Exception();
            };
        }

        class NoSuchEntry_Exception : Exception { };
        class NoSuchBook_Exception : Exception { };
    }
}
