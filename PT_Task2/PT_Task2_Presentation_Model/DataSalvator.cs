﻿using PT_Task2_Services;

namespace PT_Task2_Presentation_Model
{
    public static class DataSalvator
    {
        internal static DataOperator db = new DataOperator();
        public static bool switchedOn = false;

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

        public static void SaveToDatabase()
        {
            db.SubmitToDatabase();
        }
    }
}
