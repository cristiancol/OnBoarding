namespace BancoOnBoarding.Repository.Interfaces
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> Get();
        T Get(int id);
        void Add(T data);
        void Delete(int id);
        void Update(T data);
        void Save();
        IEnumerable<T> Filter(Func<T, bool> predicate);
    }
}
