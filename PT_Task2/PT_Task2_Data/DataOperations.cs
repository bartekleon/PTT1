using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_Task2_Data
{
    public class DataOperations
    {

        private static DB_LinkDataContext context = new DB_LinkDataContext();

        public static string GetTitle(int index) {
            IEnumerable<string> titleFound = from entry in context.Catalog
                                             where entry.entryID == index
                                             select entry.title;
            return titleFound.ToArray()[0];
        }

        public static string GetAuthor(int index)
        {
            IEnumerable<string> authorFound = from entry in context.Catalog
                                             where entry.entryID == index
                                             select entry.author;
            return authorFound.ToArray()[0];
        }

        public static int GetCatalogLength(int index)
        {
            return context.Catalog.Count();
        }

    }
}
