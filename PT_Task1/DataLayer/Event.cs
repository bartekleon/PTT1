using System.Collections.Generic;

namespace PT_Task1.DataLayer
{
    public class Event
    {
        public readonly List<Book> booksAffected = new List<Book>();
        public User actor;

        public enum EventType
        {
            RESERVATION,
            RENT_A_BOOK,
            BOOK_RETURN,
            ADD_A_BOOK,
            REMOVE_A_BOOK
        };
    }
}
