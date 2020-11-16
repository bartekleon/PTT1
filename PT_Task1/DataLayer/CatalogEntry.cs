using System;
using System.Collections.Generic;
using System.Text;

namespace PT_Task1.DataLayer
{
    public class CatalogEntry
    {
        public String title { get; private set; }
        public String author { get; private set; }
        public bool hardback { get; private set; }

        public CatalogEntry(String title, String author, bool hardback) {
            this.title = title;
            this.author = author;
            this.hardback = hardback;
        }
    }
}
