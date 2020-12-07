namespace PT_Task1.DataLayer
{
    internal class Event
    {
        internal readonly CatalogEntry bookAffected;
        internal readonly User actor;
        internal readonly EventType type;

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
