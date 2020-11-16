using System.Collections.Generic;

namespace PT_Task1.DataLayer
{
    public class Book
    {
        public CatalogEntry Description { get; protected set; }

        public BookState state = BookState.AVAILABLE;
        public Queue<User> reservationQueue = new Queue<User>();

        public Book(CatalogEntry entry) {
            this.Description = entry;
        }

        public enum BookState
        {
            AVAILABLE,
            BORROWED,
            RESERVED
        }
    }
}
