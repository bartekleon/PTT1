using System;

namespace PT_Task1.DataLayer
{
    public interface ILibrary
    {
        public void RemoveEntry(int which);
        public void RemoveEntry(string title, string author, bool hardback);
        public void AddEntry(string title, string author, bool hardback);
        public bool CheckIfEntryExists(string title, string author, bool hardback);

        public void SelectBook(string title, string author, bool hardback, Book.BookState bookState);
        public void ChangeActiveBookStateTo(Book.BookState bookState);
        public void AssignTheBookToTheUser();

        public void SelectUser(string username);

        public void AddBook(CatalogEntry type);
        public void RemoveBook(CatalogEntry type);

        class NoSuchBook_Exception : Exception { };
        class NoSuchUser_Exception : Exception { };
        class LimitReached_Exception : Exception { };
    }
}
