using PT_Task2_Services;
using System.Collections.Generic;

namespace PT_Task2_Presentation_Model
{
    public class DataModel
    {
        private readonly DataOperator db = DataSalvator.db;
        public IEnumerable<Entry> Data = new List<Entry>();

        public DataModel()
        {
            List<Entry> list = new List<Entry>();
            for (int i = 0; i < db.GetCatalogLength(); i++)
            {
                if (db.DoesEntryExists(i))
                {
                    list.Add(new Entry()
                    {
                        Author = db.GetAuthorOfEntry(i),
                        Title = db.GetTitleOfEntry(i),
                        BookCount = db.GetBookCountByEntry(i),
                        Index = i
                    });
                }
            }
            Data = list;
        }
    }
}