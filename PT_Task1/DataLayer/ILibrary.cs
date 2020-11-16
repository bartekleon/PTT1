using System;

namespace PT_Task1.DataLayer
{
    public interface ILibrary
    {
        public bool CheckIfEntryExists(string title, string author, bool hardback);
        public bool CanIBorrow();
        public bool CanIReserve();

        public void SelectBook(string title, string author, bool hardback, Book.BookState bookState);
        public void SelectBook(string title, string author, bool hardback, Book.BookState bookState, string ownerUsername);
        public void ChangeTheBookStateTo(Book.BookState bookState);
        public void AssignTheBookToTheUser();
        public void AssignTheUserToTheBook();
        public void UnassignTheBook();
        public void AddTheUserToQueue();

        public void SelectUser(string username);

        public void AddBook(CatalogEntry type);
        public void RemoveBook(CatalogEntry type);
        public void RemoveEntry(int which);
        public void RemoveEntry(string title, string author, bool hardback);
        public void AddEntry(string title, string author, bool hardback);

        class NoSuchBook_Exception : Exception { };
        class NoSuchUser_Exception : Exception { };
        class LimitReached_Exception : Exception { };
        class NotYourBook_Exception : Exception { };

        void PassTheBookDownTheQueue();
    }
}
