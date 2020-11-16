namespace PT_Task1.DataLayer
{
    public class CatalogEntry
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public bool Hardback { get; private set; }

        public CatalogEntry(string title, string author, bool hardback) {
            this.Title = title;
            this.Author = author;
            this.Hardback = hardback;
        }
    }
}
