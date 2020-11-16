using System;

namespace PT_Task1.DataLayer
{
    class MockLibrary : ILibrary
    {
        public void AddBook(CatalogEntry type)
        {
            throw new NotImplementedException();
        }

        public void AddEntry(string title, string author, bool hardback)
        {
            throw new NotImplementedException();
        }

        public void AssignTheBookToTheUser()
        {
            throw new NotImplementedException();
        }

        public void ChangeActiveBookStateTo(Book.BookState bookState)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfEntryExists(string title, string author, bool hardback)
        {
            return false;
        }

        public void ListEntries()
        {
            throw new NotImplementedException();
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

        public void SelectBook(string title, string author, bool hardback, Book.BookState bookState)
        {
            throw new NotImplementedException();
        }

        public void SelectUser(string username)
        {
            throw new NotImplementedException();
        }
    }
}
