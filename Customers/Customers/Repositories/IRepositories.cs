using Customers.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Customers.Repositories
{
    public interface IRepositories<TEntity>
    {

        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity, TEntity updatedEntity);
        void Delete(TEntity entity);
        Customer Get(int id);

    }
}
