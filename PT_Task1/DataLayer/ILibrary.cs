using System;
using System.Collections.Generic;
using System.Text;

namespace PT_Task1.DataLayer
{
    interface ILibrary
    {
        public void RemoveEntry(int which);
        public void RemoveEntry(String title, String author, bool hardback);
        public void AddEntry(String title, String author, bool hardback);
        public bool CheckIfEntryExists(String title, String author, bool hardback);

        public void SelectBook(String title, String author, bool hardback, Book.BookState bookState);
        public void ChangeActiveBookStateTo(Book.BookState bookState);

        public void SelectUser(String username);

        public void AddBook(CatalogEntry type);
        public void RemoveBook(CatalogEntry type);

        class NoSuchBook_Exception : Exception { };
        class NoSuchUser_Exception : Exception { };
    }
}
