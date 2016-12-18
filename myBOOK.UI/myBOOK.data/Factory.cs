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

        private IRepository _repository;

        public IRepository GetRepository()
        {
            if (_repository == null)
                _repository = new Repository();
            return _repository;
        }
    }
}
