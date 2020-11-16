using System.Collections.Generic;

namespace PT_Task1.DataLayer
{
    class Event
    {
        public List<Book> booksAffected;
        public User actor;

        public enum EventType {
            RESERVATION,
            RENT_A_BOOK,
            BOOK_RETURN,
            ADD_A_BOOK,
            REMOVE_A_BOOK
        };
    }
}
