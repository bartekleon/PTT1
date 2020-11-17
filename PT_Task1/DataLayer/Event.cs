namespace PT_Task1.DataLayer
{
    public class Event
    {
        public readonly CatalogEntry bookAffected;
        public readonly User actor;
        public readonly EventType type;

        public Event(CatalogEntry entry, User actor, EventType type)
        {
            this.bookAffected = entry;
            this.actor = actor;
            this.type = type;
        }
    }
    public enum EventType
    {
        RESERVATION,
        RENT_A_BOOK,
        BOOK_RETURN,
        ADD_A_BOOK,
        REMOVE_A_BOOK
    };
}
