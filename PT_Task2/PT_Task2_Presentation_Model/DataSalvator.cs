using PT_Task2_Services;

namespace PT_Task2_Presentation_Model
{
    public static class DataSalvator
    {
        internal static DataOperator db = new DataOperator();
        public static bool switchedOn = true;

        internal static void UpdateRecord(int entryID, string newContent, string whichField)
        {
            if (switchedOn)
            {
                switch (whichField)
                {
                    case "Title":
                        db.UpdateCatalogEntryWithTitle(entryID, newContent);
                        break;
                    case "Author":
                        db.UpdateCatalogEntryWithAuthor(entryID, newContent);
                        break;
                }
            }
        }
        internal static void AddBookOfEntry(int entryId)
        {
            if (switchedOn) db.InsertBook(entryId);
        }
        internal static void RemoveBookOfEntry(int entryId)
        {
            if (switchedOn) db.DeleteBook(entryId);
        }

        public static Entry NewEntry()
        {
            int index = db.InsertCatalogEntry("", "", true);
            if (!switchedOn) db.DeleteCatalogEntry(index);
            return new Entry()
            {
                Index = index,
                Author = "",
                Title = "",
                BookCount = 0
            };
        }
        public static void DeleteEntry(Entry entry)
        {
            if (switchedOn) db.DeleteCatalogEntry(entry.Index);
        }

        public static void FlushChanges()
        {
            if (switchedOn) db.RefreshTheDatabase();
        }

        public static void SaveToDatabase()
        {
            if (switchedOn) db.SubmitToDatabase();
        }
    }
}
