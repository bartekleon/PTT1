using PT_Task1.DataLayer;

namespace PT_Task1.LogicLayer
{
    public class LibraryController

    {
        private LibraryService ls;
        private ILibrary library;

        public LibraryController(LibraryService ls)
        {
            this.ls = ls;
            this.library = ls.library;
        }

        public bool SearchForABook(BookState state)
        {
            try
            {
                library.SelectBook(ls.RememberedTitle, ls.RememberedAuthor, ls.RememeberedHardback, state);
                return true;
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                return false;
            }
        }

        public bool SearchForABook(string title, string author, bool hardback)
        {
            try
            {
                library.SelectBook(title, author, hardback);
                return true;
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                return false;
            }
        }

        public bool SearchForABook(string title, string author, bool hardback, BookState bookstate)
        {
            try
            {
                library.SelectBook(title, author, hardback, bookstate);
                return true;
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                return false;
            }
        }

        public bool SearchForABook(string title, string author, bool hardback, BookState bookstate, string owner)
        {
            try
            {
                library.SelectBook(title, author, hardback, bookstate, owner);
                return true;
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                return false;
            }
        }

        public bool CanIBorrow()
        {
            return library.CanIBorrow();
        }

        public bool CanIReserve()
        {
            return library.CanIReserve();
        }
    }
}
