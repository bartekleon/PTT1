namespace PT_Task2_ServiceTest.Instrumentation
{
    internal class TestingDataGenerator
    {
        public static void GenerateData(PT_Task2_Services.DataOperator db)
        {
            db.TruncateBoth();
            db.SubmitToDatabase();

            db.InsertCatalogEntry("Harry Potter and the Philosopher's Stone", "J. K. Rowling", true);
            db.InsertCatalogEntry("On the Bright Side", "Hendrik Groen", false);
            db.InsertCatalogEntry("Pride and Prejudice", "Jane Austen", false);

            db.SubmitToDatabase();

            db.InsertBook(1);
            for (int i = 0; i < 6; i++)
            {
                db.InsertBook(2);
            }
            db.SubmitToDatabase();
        }
    }
}
