using PT_Task1.DataLayer;

namespace PT_Task1.LogicLayer
{
    class FixedDataGenerator
    {
        public static void Populate(ILibrary l)
        {
            l.AddEntry("Harry Potter and the Philosopher's Stone", "J. K. Rowling", true);
            l.AddEntry("On the Bright Side", "Hendrik Groen", false);
            l.AddEntry("Pride and Prejudice", "Jane Austin", false);

            l.AddBook("On the Bright Side", "Hendrik Groen", false);

            for (int i = 0; i < 7; i++)
            {
                l.AddBook("Pride and Prejudice", "Jane Austin", false);
            }

            l.AddUser("White", true, true);
            l.AddUser("Red", false, false);
            l.AddUser("Black", true, true);
            l.AddUser("Blue", true, false);
            l.AddUser("Gold", true, true, true);
        }
    }
}
