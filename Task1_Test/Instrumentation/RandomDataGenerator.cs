using PT_Task1.DataLayer;
using System;

namespace PT_Task1.LogicLayer
{
    class RandomDataGenerator
    {
        private static Random random = new Random();
        private static readonly int scale = 8;

        private static string RandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ";
            var stringChars = new char[random.Next(25)];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        public static void Populate(ILibrary l)
        {
            string[] titles = new string[random.Next(scale) + 1];
            string[] authors = new string[random.Next(scale) + 1];

            string[,] entries = new string[scale, 3];

            for (int i = 0; i < titles.Length; i++)
            {
                int whichAuthor = random.Next(authors.Length);
                bool hardback = (random.Next(2) == 0);

                l.AddEntry(titles[i], authors[whichAuthor], hardback);

                entries[i, 0] = titles[i];
                entries[i, 1] = authors[whichAuthor];
                entries[i, 2] = hardback.ToString();
            }

            for (int i = 0; i < titles.Length; i++)
            {
                int bookCount = random.Next(scale);
                for (int j = 0; j < bookCount; j++)
                {
                    l.AddBook(entries[i, 0], entries[i, 1], (entries[i, 2] == "True"));
                }
            }

            l.AddUser("White", random.Next(2) == 0, random.Next(2) == 0, random.Next(2) == 0);
            l.AddUser("Red", random.Next(2) == 0, random.Next(2) == 0, random.Next(2) == 0);
            l.AddUser("Black", random.Next(2) == 0, random.Next(2) == 0, random.Next(2) == 0);
            l.AddUser("Blue", random.Next(2) == 0, random.Next(2) == 0, random.Next(2) == 0);
            l.AddUser("Gold", random.Next(2) == 0, random.Next(2) == 0, random.Next(2) == 0);
        }
    }
}
