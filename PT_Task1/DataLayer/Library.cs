using System;
using System.Collections.Generic;
using System.Text;

namespace PT_Task1.DataLayer
{
    class Library : ILibrary
    {
        public List<User> userList;
        public List<Book> bookList;
        public List<Event> eventHistory;

        private Book activeBook;
        private User activeUser;

        public void AddBook(CatalogEntry type)
        {
            throw new NotImplementedException();
        }

        public void AddEntry(string title, string author, bool hardback)
        {
            Catalog.entries.Add(new CatalogEntry(title, author, hardback));
        }

        public bool CheckIfEntryExists(string title, string author, bool hardback)
        {
            foreach (CatalogEntry entry in Catalog.entries)
            {
                if (entry.title == title && entry.author == author && entry.hardback == hardback) return true;
            }
            return false;
        }

        public void SelectBook(string title, string author, bool hardback, Book.BookState bookState)
        //TODO what if not?
        {
            foreach (Book book in bookList)
            {
                if (book.description.title == title
                    && book.description.author == author
                    && book.description.hardback == hardback
                    && book.state == bookState) this.activeBook = book;
            } throw new ILibrary.NoSuchBook_Exception();
        }

        public void ChangeActiveBookStateTo(Book.BookState bookState) {
            this.activeBook.state = bookState;    
        }

        public void SelectUser(String username) {
            foreach (User user in userList) {
                if (user.username == username) this.activeUser = user;
            } throw new ILibrary.NoSuchUser_Exception();
        }

        public void RemoveBook(CatalogEntry type)
        {
            throw new NotImplementedException();
        }

        public void RemoveEntry(int which)
        {
            throw new NotImplementedException();
        }

        public void RemoveEntry(string title, string author, bool hardback)
        {
            throw new NotImplementedException();
        }
    }
}
