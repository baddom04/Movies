namespace Movies.DataAccess.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {

        }
        public EntityNotFoundException(string msg) : base(msg)
        {

        }
    }
}
