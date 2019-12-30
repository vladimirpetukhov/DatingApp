namespace DateApp.API.Data {
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DateApp.API.Models;

    public interface IDatingRepository {
        void Add<T> (T entity) where T : class;
        void Delete<T> (T entity) where T : class;
        Task<bool> SaveAll ();
        Task<IEnumerable<User>> GetUsers ();
        Task<IEnumerable<User>> GetUser (int id);
    }
}