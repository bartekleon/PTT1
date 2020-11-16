using System;
using System.Collections.Generic;
using System.Text;

namespace PT_Task1.DataLayer
{
    public class User
    {
        public String username { get; private set; }

        public int borrowLimit { get; private set; }
        public List<Book> borrowedBooks;

        public int reserveLimit { get; private set; }
        public List<Book> reservedBooks;

        public User(String username, bool canBorrow, bool canReserve) {
            this.username = username;

            if (canBorrow) {
                this.borrowLimit = 6;
                this.borrowedBooks = new List<Book>();
            } else {
                this.borrowLimit = 0;
            }

            if (canReserve)
            {
                this.reserveLimit = 3;
                this.reservedBooks = new List<Book>();
            }
            else {
                this.reserveLimit = 0;
            }
        }
    }
}
