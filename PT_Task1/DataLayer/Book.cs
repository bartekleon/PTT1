using System;
using System.Collections.Generic;
using System.Text;

namespace PT_Task1.DataLayer
{
    public class Book
    {
        public CatalogEntry description { get; protected set; }
        public BookState state;

        public Book(CatalogEntry entry) {
            this.description = entry;
            this.state = BookState.AVAILABLE;
            this.reservationQueue = new Queue<User>();
        }

        public Queue<User> reservationQueue;

        public enum BookState
        {
            AVAILABLE,
            BORROWED,
            RESERVED
        }
    }
}
