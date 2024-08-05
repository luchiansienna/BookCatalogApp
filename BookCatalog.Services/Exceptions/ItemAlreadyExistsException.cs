namespace BookCatalog.Services.Exceptions
{
    public class ItemAlreadyExistsException : Exception
    {
        public ItemAlreadyExistsException()
        {
        }

        public ItemAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
