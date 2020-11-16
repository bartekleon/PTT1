using System;

namespace PT_Task1.DataLayer
{
    public interface ILibrary
    {
        public bool CheckIfEntryExists(string title, string author, bool hardback);
        public bool CanIBorrow();
        public bool CanIReserve();
        public bool AmIAdmin();

        public void SelectBook(string title, string author, bool hardback);
        public void SelectBook(string title, string author, bool hardback, Book.BookState bookState);
        public void SelectBook(string title, string author, bool hardback, Book.BookState bookState, string ownerUsername);
        public int CountBooks(string title, string author, bool hardback);
        public void ChangeTheBookStateTo(Book.BookState bookState);
        public void AssignTheBookToTheUser();
        public void AssignTheUserToTheBook();
        public void UnassignTheBook();
        public void AddTheUserToQueue();

        public void SelectUser(string username);

        public void AddBook(string title, string author, bool hardback);
        public void RemoveTheBook();
        public void RemoveAllBooks(string title, string author, bool hardback);
        public void RemoveEntry(int which);
        public void RemoveEntry(string title, string author, bool hardback);
        public void AddEntry(string title, string author, bool hardback);

        class NoSuchBook_Exception : Exception { };
        class NoSuchUser_Exception : Exception { };
        class NoSuchEntry_Exception : Exception { };
        class LimitReached_Exception : Exception { };
        class NotYourBook_Exception : Exception { };

        void PassTheBookDownTheQueue();

        void LogEvent(Event.EventType type);
    }
}
