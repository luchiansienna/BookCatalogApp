namespace BookCatalog.Services.Exceptions
{
    public class DependantItemsFoundException : Exception
    {
        public DependantItemsFoundException()
        {
        }

        public DependantItemsFoundException(string message) : base(message)
        {
        }
    }
}
