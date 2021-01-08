namespace PT_Task2_Presentation_Model
{
    public class Entry
    {
        private string _Title;
        private string _Author;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                DataSalvator.UpdateRecord(Index, value, "Title");
            }
        }
        public string Author
        {
            get
            {
                return _Author;
            }
            set
            {
                _Author = value;
                DataSalvator.UpdateRecord(Index, value, "Author");
            }
        }
        public int BookCount { get; set; }
        internal int Index;
        public override string ToString()
        {
            return Index + ". " + Title + " " + Author + " " + BookCount;
        }

        public void AddSuchBook()
        {
            DataSalvator.AddBookOfEntry(Index);
        }
        public void RemoveSuchBook()
        {
            DataSalvator.RemoveBookOfEntry(Index);
        }
    }
}
