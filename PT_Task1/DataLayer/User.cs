using System.Collections.Generic;

namespace PT_Task1.DataLayer
{
    public class User
    {
        public string Username { get; private set; }

        public int BorrowLimit { get; private set; } = 0;
        public readonly List<Book> borrowedBooks = new List<Book>();

        public int ReserveLimit { get; private set; } = 0;
        public readonly List<Book> reservedBooks = new List<Book>();

        public User(string username, bool canBorrow, bool canReserve)
        {
            this.Username = username;

            if (canBorrow)
            {
                this.BorrowLimit = 6;

                if (canReserve)
                {
                    this.ReserveLimit = 3;
                }
            }
        }
    }
}
