namespace TodoApi.Exceptions
{
    public class TodoNotFoundException : Exception
    {
        public TodoNotFoundException() { }
        public TodoNotFoundException(string message)
        : base(message)
        {
        }

        public TodoNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
