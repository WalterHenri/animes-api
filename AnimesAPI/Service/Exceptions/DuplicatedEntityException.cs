namespace Animes.Application.Exceptions
{
    public class DuplicatedEntityException : Exception
    {
        public DuplicatedEntityException(string message) : base(message)
        {
        }
    }
}