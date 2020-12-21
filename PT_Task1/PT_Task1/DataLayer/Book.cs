using System.Collections.Generic;

namespace PT_Task1.DataLayer
{
    internal class Book
    {
        public CatalogEntry Description { get; internal set; }

        internal BookState state = BookState.AVAILABLE;
        internal User CurrentOwner;
        internal readonly Queue<User> reservationQueue = new Queue<User>();

        public Book(CatalogEntry entry)
        {
            this.Description = entry;
        }
    }
    public enum BookState
    {
        AVAILABLE,
        BORROWED,
        RESERVED
    }
}
