using PT_Task1.DataLayer;
using System;

namespace PT_Task1.LogicLayer
{
    public class LibraryService
    {
        private string LoggedInUser;
        public string RememberedTitle { get; private set; }
        public string RememberedAuthor { get; private set; }
        public bool RememeberedHardback { get; private set; }

        public ILibrary library;

        public LibraryService(ILibrary library)
        {
            this.library = library;
        }

        public void Login(string username)
        {
            try
            {
                library.SelectUser(username);
                LoggedInUser = username;
            }
            catch (ILibrary.NoSuchUser_Exception)
            {
                throw new Login_NoSuchUser_Exception();
            }
        }

        private void GetRandomBook()
        {
            library.SelectBook(new Random().Next(library.CountAllBooks()));

            RememberedTitle = library.GetSelectedTitle();
            RememberedAuthor = library.GetSelectedAuthor();
            RememeberedHardback = library.GetSelectedHardback();
        }


        public void RentBook()
        {
            GetRandomBook();
            RentBook(RememberedTitle, RememberedAuthor, RememeberedHardback);
        }

        public void RentBook(string title, string author, bool hardback)
        {
            if (!library.CheckIfEntryExists(title, author, hardback)) throw new NoSuchEntry_Exception();
            if (!library.CanIBorrow()) throw new NotAppropriatePermits_Exception();
            try
            {
                library.SelectBook(title, author, hardback, BookState.AVAILABLE);
                library.AssignTheBookToTheUser();
                library.AssignTheUserToTheBook();
                library.ChangeTheBookStateTo(BookState.BORROWED);
                library.LogEvent(EventType.RENT_A_BOOK);
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                throw new NonExistingBook_Exception();
            }
            catch (ILibrary.LimitReached_Exception)
            {
                throw new LimitBreached_Exception();
            }
        }

        public void RentReservedBook(string title, string author, bool hardback)
        {
            if (!library.CheckIfEntryExists(title, author, hardback)) throw new NoSuchEntry_Exception();
            try
            {
                library.SelectBook(title, author, hardback, BookState.RESERVED, LoggedInUser);
                library.ChangeTheBookStateTo(BookState.BORROWED);
                library.LogEvent(EventType.RENT_A_BOOK);
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                throw new NonExistingBook_Exception();
            }
            catch (ILibrary.LimitReached_Exception)
            {
                throw new LimitBreached_Exception();
            }
        }

        public void ReturnBook()
        {
            ReturnBook(RememberedTitle, RememberedAuthor, RememeberedHardback);
        }
        public void ReturnBook(string title, string author, bool hardback)
        {
            if (!library.CheckIfEntryExists(title, author, hardback)) throw new NoSuchEntry_Exception();
            try
            {
                library.SelectBook(title, author, hardback, BookState.BORROWED, LoggedInUser);
                library.UnassignTheBook();
                library.ChangeTheBookStateTo(BookState.AVAILABLE);
                library.PassTheBookDownTheQueue();
                library.LogEvent(EventType.BOOK_RETURN);
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                throw new NonExistingBook_Exception();
            }
        }


        public void ReserveBook()
        {
            GetRandomBook();
            ReserveBook(RememberedTitle, RememberedAuthor, RememeberedHardback);
        }
        public void ReserveBook(string title, string author, bool hardback)
        {
            if (!library.CheckIfEntryExists(title, author, hardback)) throw new NoSuchEntry_Exception();
            if (!library.CanIReserve()) throw new NotAppropriatePermits_Exception();
            try
            {
                library.SelectBook(title, author, hardback, BookState.AVAILABLE);
                library.ChangeTheBookStateTo(BookState.RESERVED);
                library.AssignTheUserToTheBook();
                library.LogEvent(EventType.RESERVATION);
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                try
                {
                    library.SelectBook(title, author, hardback, BookState.RESERVED);
                    library.AddTheUserToQueue();
                    library.LogEvent(EventType.RESERVATION);
                }
                catch (ILibrary.NoSuchBook_Exception)
                {
                    try
                    {
                        library.SelectBook(title, author, hardback, BookState.BORROWED);
                        library.AddTheUserToQueue();
                        library.LogEvent(EventType.RESERVATION);
                    }
                    catch (ILibrary.NoSuchBook_Exception)
                    {
                        throw new NonExistingBook_Exception();
                    }
                }
            }
        }

        public void AddCatalogEntry(string title, string author, bool hardback)
        {
            if (!library.AmIAdmin()) throw new NotAppropriatePermits_Exception();
            if (!library.CheckIfEntryExists(title, author, hardback)) library.AddEntry(title, author, hardback);
        }

        public void RemoveCatalogEntry(string title, string author, bool hardback)
        {
            if (!library.AmIAdmin()) throw new NotAppropriatePermits_Exception();
            if (library.CheckIfEntryExists(title, author, hardback))
            {
                try
                {
                    library.SelectBook(title, author, hardback);
                    for (int i = 0; i < library.CountBooks(title, author, hardback); i++)
                    {
                        library.LogEvent(EventType.REMOVE_A_BOOK);
                    }
                }
                catch (ILibrary.NoSuchBook_Exception) { }

                library.RemoveAllBooks(title, author, hardback);
                library.RemoveEntry(title, author, hardback);
            }
        }

        public void AddBook(string title, string author, bool hardback)
        {
            if (!library.AmIAdmin()) throw new NotAppropriatePermits_Exception();
            AddCatalogEntry(title, author, hardback);
            library.AddBook(title, author, hardback);
            library.SelectBook(title, author, hardback, BookState.AVAILABLE);
            library.LogEvent(EventType.ADD_A_BOOK);
        }

        public void RemoveBook(string title, string author, bool hardback)
        {
            if (!library.AmIAdmin()) throw new NotAppropriatePermits_Exception();
            try
            {
                library.SelectBook(title, author, hardback, BookState.AVAILABLE);
                library.LogEvent(EventType.REMOVE_A_BOOK);
                library.RemoveTheBook();
            }
            catch (ILibrary.NoSuchBook_Exception)
            {
                throw new NonExistingBook_Exception();
            }
        }

        public class ServiceException : Exception { };
        public class Login_NoSuchUser_Exception : ServiceException { };
        public class NoSuchEntry_Exception : ServiceException { };
        public class NotAppropriatePermits_Exception : ServiceException { };
        public class NonExistingBook_Exception : ServiceException { };
        public class LimitBreached_Exception : ServiceException { };
    }
}
