namespace PT_Task1.DataLayer
{
    public class CatalogEntry
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public bool Hardback { get; private set; }

        public CatalogEntry(string title, string author, bool hardback)
        {
            this.Title = title;
            this.Author = author;
            this.Hardback = hardback;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            CatalogEntry that = (CatalogEntry)obj;

            return (this.Title == that.Title
                && this.Author == that.Author
                && this.Hardback == that.Hardback);
        }

    }
}
