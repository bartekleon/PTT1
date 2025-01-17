﻿using System;
using System.Collections.Generic;

namespace PT_Task1.DataLayer
{
    public interface ILibrary
    {
        public bool CheckIfEntryExists(string title, string author, bool hardback);
        public bool CanIBorrow();
        public bool CanIReserve();
        public bool AmIAdmin();

        public void SelectBook(int which);
        public void SelectBook(string title, string author, bool hardback);
        public void SelectBook(string title, string author, bool hardback, BookState bookState);
        public void SelectBook(string title, string author, bool hardback, BookState bookState, string ownerUsername);
        public int CountBooks(string title, string author, bool hardback);
        public int CountAllBooks();
        public void ChangeTheBookStateTo(BookState bookState);
        public void AssignTheBookToTheUser();
        public void AssignTheUserToTheBook();
        public void UnassignTheBook();
        public void AddTheUserToQueue();

        public string GetSelectedTitle();
        public string GetSelectedAuthor();
        public bool GetSelectedHardback();

        public void SelectUser(string username);
        public void AddUser(string username, bool canBorrow, bool canReserve);
        public void AddUser(string username, bool canBorrow, bool canReserve, bool isAdmin);
        public int CountAllUsers();

        public void AddBook(string title, string author, bool hardback);
        public void AddBook(CatalogEntry entry);
        public void RemoveTheBook();
        public void RemoveAllBooks(string title, string author, bool hardback);
        public void RemoveEntry(int which);
        public void RemoveEntry(string title, string author, bool hardback);
        public void AddEntry(string title, string author, bool hardback);
        public int CountAllEntries();

        class NoSuchBook_Exception : Exception { };
        class NoSuchUser_Exception : Exception { };
        class NoSuchEntry_Exception : Exception { };
        class LimitReached_Exception : Exception { };
        class NotYourBook_Exception : Exception { };

        public void PassTheBookDownTheQueue();

        public void LogEvent(EventType type);

        internal List<Event> GetEvents();
    }
}
