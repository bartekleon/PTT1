using System.Collections.Generic;

namespace PT_Task1.DataLayer
{
    public static class Catalog
    {
        public static readonly List<CatalogEntry> entries = new List<CatalogEntry>() {
            new CatalogEntry("Harry Potter and the Philosopher's Stone", "J. K. Rowling", true),
            new CatalogEntry("On the Bright Side", "Hendrik Groen", false)
        };
    }
}
