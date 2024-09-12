namespace TodoApi.Utils
{
    public sealed class PasswordHasher
    {
        private const int saltLenght = 16;
        private const int hashLenght = 32;
        private const int iterations = 10000;

        public string Hash()
        {
            return "";
        }

        public bool Verify()
        {
            return false;
        }
    }
}
