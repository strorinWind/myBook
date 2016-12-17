using myBOOK.data.Interfaces;


namespace myBOOK.data
{
    public class Factory
    {
        static Factory _default;
        public static Factory Default
        {
            get
            {
                if (_default == null)
                    _default = new Factory();
                return _default;
            }
        }

        //private IUserBooks _userbooks;
        private IRepository _repository;

        public IRepository GetRepository()
        {
            if (_repository == null)
                _repository = new Repository();
            return _repository;
        }
        /*
        public IUserBooks GetUserBooks()
        {
            if (_userbooks == null)
                _userbooks = new PastReadBooks();
            return _userbooks;
        }
        public IUserBooks GetFutureBooks()
        {
            if (_userbooks == null)
                _userbooks = new FutureReadBooks();
            return _userbooks;
        }

        public IUserBooks GetFavouriteBooks()
        {
            if (_userbooks == null)
                _userbooks = new Favourite();
            return _userbooks;
        }
        */
    }
}
